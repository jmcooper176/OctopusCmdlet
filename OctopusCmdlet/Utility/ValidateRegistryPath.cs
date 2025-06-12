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
    /// Implements the 'ValidateRegistryPath' validate arguments attribute.
    /// </summary>
    public class ValidateRegistryPath : ValidateArgumentsAttribute
    {
        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid registry literal path value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throws if, after removing the PSProvider, the registry path left is null, empty, or all whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if the PowerShell registry path has no value (does not exist).
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var path = Convert.ToString(arguments, CultureInfo.InvariantCulture);

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(arguments), $"Registry Path {path} is null, empty, or all whitespace");
            }
            else if (!path.StartsWith("HKLM", StringComparison.OrdinalIgnoreCase) && !path.StartsWith("HKCU", StringComparison.OrdinalIgnoreCase))
            {
                throw new NotSupportedException($"Registry Path {path} must be prefixed with either 'HKLM' or 'HKCU'");
            }
            else
            {
                TestPathCommand registryPath = new()
                {
                    LiteralPath = [path],
                    PathType = TestPathType.Any
                };

                if (!registryPath.Invoke().Cast<object>().Any())
                {
                    throw new ArgumentException($"Registry Path {path} does not exist or is invalid", nameof(arguments));
                }
            }
        }

        #endregion Protected Methods
    }
}
