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
    /// Implements the 'ValidateDirectoryPath' validate arguments attribute.
    /// </summary>
    public class ValidateDirectoryPath : ValidateArgumentsAttribute
    {
        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid directory literal path value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throws if, after removing the PSProvider, the directory path left is null, empty, or all whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if the PowerShell directory path has no value (does not exist).
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var path = Convert.ToString(arguments, CultureInfo.InvariantCulture);

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentNullException(nameof(arguments), $"Directory Path {path} is null, empty, or all whitespace");
            }
            else
            {
                TestPathCommand directoryPath = new()
                {
                    LiteralPath = [path],
                    PathType = TestPathType.Container
                };

                if (!directoryPath.Invoke().Cast<object>().Any())
                {
                    throw new ArgumentException($"Directory Path {path} does not exist or is invalid", nameof(arguments));
                }
            }
        }

        #endregion Protected Methods
    }
}
