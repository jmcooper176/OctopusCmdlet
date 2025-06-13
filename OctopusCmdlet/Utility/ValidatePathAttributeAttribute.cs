using Microsoft.PowerShell.Commands;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateDirectoryPathAttribute' validate arguments attribute.
    /// </summary>
    public class ValidatePathAttributeAttribute : ValidateArgumentsAttribute
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value specifying items that this validator omits. The of this parameter qualifies 'path'.
        /// <para> Enter a path element or pattern. </para>
        /// </summary>
        /// <remarks>
        /// Wildcard characters are permitted.
        /// </remarks>
        [SupportsWildcards]
        [AllowNull]
        public string[]? Exclude { get; set; }

        /// <summary>
        /// Gets or sets a value specifying a filter in the format or language of the provider. The value of this parameter
        /// qualifies 'path'.
        /// <para> The syntax of the filter, including the use of wildcard characters, depends on the provider. </para>
        /// </summary>
        /// <remarks>
        /// Filters are more efficient than other parameters, because the provider applies them when it retrieves the objects
        /// instead of having PowerShell filter the objects after they have been retrieved.
        /// </remarks>
        [SupportsWildcards]
        [AllowNull]
        [AllowEmptyString]
        public string? Filter { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the paths that this validator tests. The value of this parameter qualifies 'path'.
        /// <para> Enter a path element or pattern. </para>
        /// </summary>
        /// <remarks>
        /// Wildcard characters are permitted.
        /// </remarks>
        [SupportsWildcards]
        [AllowNull]
        public string[]? Include { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this validator tests the syntax of the path, regardless of whether the elements
        /// of the path exist.
        /// <list type="bullet">
        /// <item>
        /// This validator does not throw <see cref="ValidationMetadataException" /> if the path syntax is valid; otherwise,
        /// <see cref="ValidationMetadataException" /> is thrown for non-syntactical path(s); OR
        /// </item>
        /// <item>
        /// If the path being validated includes a drive specification, the validator throws
        /// <see cref="ValidationMetadataException" /> when the drive does not exist.
        /// </item>
        /// </list>
        /// </summary>
        /// <remarks>
        /// Unlike <see cref="Cmdlet" /> 'Test-Path', this validator does validate directory and file name for invalid characters.
        /// This issue for 'Test-Path' has existed since .NET Core 2.1 and has not yet been re-mediated.
        /// <para>
        /// Note:  This parameter is incompatible with <see cref="TestPathType.Container" /> and <see cref="TestPathType.Leaf" />
        /// because of historical reasons.
        /// </para>
        /// </remarks>
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the type of the final element in the path. This validator throw
        /// <see cref="ValidationMetadataException" /> if 'path' is not of the specified type; otherwise,
        /// <see cref="ValidationMetadataException" /> is not thrown if the last element of the 'path' is of the specified type. The
        /// acceptable values are:
        /// <list type="bullet">
        /// <item>
        /// <term> Any </term>
        /// <description> Either a container or a leaf; </description>
        /// </item>
        /// <item>
        /// <term> Container </term>
        /// <description> An element that contains other elements, such as a directory or registry key; OR </description>
        /// </item>
        /// <item>
        /// <term> Leaf </term>
        /// <description> An element that does not contain other elements, such as a file. </description>
        /// </item>
        /// </list>
        /// </summary>
        [ValidateSet("Any", "Container", "Leaf", IgnoreCase = true)]
        public TestPathType PathType { get; set; } = TestPathType.Any;

        #endregion Public Properties

        #region Protected Methods

        /// <inheritdoc />
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            List<string> path = [];
            char[] wildcards = { '*', '?' };
            GetPSProviderCommand provider = new();

            if (arguments is null)
            {
                throw new ValidationMetadataException($"Parameter '{nameof(arguments)}' is null", new PSArgumentNullException(nameof(arguments)));
            }
            else if (arguments is Array elements && elements.Length > 0)
            {
                var elementArray = elements.Cast<string>().Where(e => !string.IsNullOrWhiteSpace(e)).ToArray();
                Array.ForEach(elementArray, e => path.Add(provider.GetUnresolvedProviderPathFromPSPath(e)));
            }
            else if (arguments is string element && !string.IsNullOrWhiteSpace(element))
            {
                var elementWithProvider = provider.GetUnresolvedProviderPathFromPSPath(element);
                path.Add(element);
            }
            else
            {
                var argumentsType = arguments.GetType().FullName;

                throw new ValidationMetadataException($"Parameter 'arguments' is null or has type {argumentsType} which is unsupported", new PSInvalidCastException());
            }

            TestPathCommand testPath = new()
            {
                PathType = IsValid ? TestPathType.Any : PathType,
            };

            if (path.Any(e => e.Any(c => wildcards.Contains(c))))
            {
                testPath.Path = [.. path];

                provider.PSProvider = ["FileSystem"];

                if (provider.Invoke().Cast<bool>().Any(r => r))
                {
                    if (Exclude != null && Exclude.Length > 0)
                    {
                        testPath.Exclude = Exclude;
                    }

                    if (!string.IsNullOrWhiteSpace(Filter))
                    {
                        testPath.Filter = Filter;
                    }

                    if (Include != null && Include.Length > 0)
                    {
                        testPath.Include = Include;
                    }
                }
            }
            else
            {
                testPath.LiteralPath = [.. path];
            }

            if (testPath.Invoke().Cast<bool>().Any(r => !r))
            {
                string resultMessage = IsValid ? $"Path {path} is invalid" : $"Path {path} does not exist or is invalid";

                throw new ValidationMetadataException(resultMessage, new ArgumentException(nameof(arguments)));
            }
        }

        #endregion Protected Methods

        #region Private Methods

        private string? InitializePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return null;
            }
            else
            {
                path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
                return Path.GetFullPath(path);
            }
        }

        private bool TestPathSyntax(string? path)
        {
            path = InitializePath(path);

            if (string.IsNullOrWhiteSpace(path))
            {
                return false;
            }
            else
            {
                if (path.Any(c => Path.GetInvalidPathChars().Contains(c)))
                {
                    return false;
                }

                var fileName = Path.GetFileName(path);

                return !string.IsNullOrWhiteSpace(fileName) && !fileName.Any(c => Path.GetInvalidFileNameChars().Contains(c));
            }
        }

        #endregion Private Methods
    }
}
