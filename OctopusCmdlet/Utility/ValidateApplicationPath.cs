using Microsoft.PowerShell.Commands;

using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateApplicationPath' validate arguments attribute.
    /// </summary>
    public class ValidateApplicationPath : ValidateArgumentsAttribute
    {
        #region Private Fields

        /// <summary>
        /// Constant string representing the PowerShell PSProvider separator.
        /// </summary>
        private const string PSPROVIDER_SEPARATOR = ":\\";

        #endregion Private Fields

        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid application literal path value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throws if, after removing the PSProvider, the application name left is null, empty, or all whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if the PowerShell application name has no value (does not exist).
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var applicationName = Convert.ToString(arguments, CultureInfo.InvariantCulture);
            var name = RemovePSProvider(applicationName);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(arguments), $"Application Name {name} is null, empty, or all whitespace");
            }
            else
            {
                GetCommandCommand getCommand = new()
                {
                    Name = [name],
                    CommandType = CommandTypes.Application
                };

                if (!getCommand.Invoke().Cast<CommandInfo>().Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Application {name} not found in 'CommandInfo'", nameof(arguments));
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

                var commandPath = index >= 0
                    ? path[(index + PSPROVIDER_SEPARATOR.Length)..]
                    : path;
                return Path.GetFileName(commandPath);
            }
        }

        #endregion Private Methods
    }
}
