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
    /// Implements the 'ValidateScriptPath' validate arguments attribute.
    /// </summary>
    public class ValidateScriptPath : ValidateArgumentsAttribute
    {
        #region Private Fields

        /// <summary>
        /// Constant string representing the PowerShell PSProvider separator.
        /// </summary>
        private const string PSPROVIDER_SEPARATOR = ":\\";

        #endregion Private Fields

        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid script literal path value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throws if, after removing the PSProvider, the script name left is null, empty, or all whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if the PowerShell script name has no value (does not exist).
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var scriptName = Convert.ToString(arguments, CultureInfo.InvariantCulture);
            var name = RemovePSProvider(scriptName);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(arguments), $"Script Name {name} is null, empty, or all whitespace");
            }
            else
            {
                GetCommandCommand getCommand = new()
                {
                    Name = [name],
                    CommandType = CommandTypes.Script
                };

                if (!getCommand.Invoke().Cast<CommandInfo>().Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Script {name} not found in 'CommandInfo'", nameof(arguments));
                }
            }
        }

        #endregion Protected Methods

        #region Private Methods

        /// <summary>
        /// Remove the PSProvider suffix from a path.
        /// </summary>
        /// <param name="path">
        /// Specifies the fully qualified path to modify.
        /// </param>
        /// <returns>
        /// Returns a variable name string; otherwise <see cref="string.Empty" />.
        /// </returns>
        private string RemovePSProvider(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }
            else
            {
                int index = path.IndexOf(PSPROVIDER_SEPARATOR);

                return index >= 0
                    ? path[(index + PSPROVIDER_SEPARATOR.Length)..]
                    : path;
            }
        }

        #endregion Private Methods
    }
}
