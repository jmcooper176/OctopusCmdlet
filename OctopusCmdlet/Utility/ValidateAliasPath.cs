using Microsoft.PowerShell.Commands;

using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateAliasPath' validate arguments attribute.
    /// </summary>
    public class ValidateAliasPath : ValidateArgumentsAttribute
    {
        #region Private Fields

        /// <summary>
        /// Constant string representing the PSProvider name.
        /// </summary>
        private const string PSPROVIDER_NAME = "Alias";

        /// <summary>
        /// Constant string representing the PowerShell PSProvider separator.
        /// </summary>
        private const string PSPROVIDER_SEPARATOR = ":\\";

        #endregion Private Fields

        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid alias literal path value.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// Throws if, after removing the PSProvider, the alias name left is null, empty, or all whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws if the PowerShell alias name has no value (does not exist).
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var path = Convert.ToString(arguments, CultureInfo.InvariantCulture);
            var name = RemovePSProvider(path, PSPROVIDER_NAME);

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(arguments), $"Alias Name {name} is null, empty, or all whitespace");
            }
            else
            {
                GetCommandCommand getFunction = new();
                getFunction.Name = [name];
                getFunction.CommandType = CommandTypes.Alias

                if (!getFunction.Invoke().Cast<CommandInfo>().Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
                {
                    throw new ArgumentException($"Alias {name} not found in 'CommandInfo'", nameof(arguments));
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
        /// <param name="psProviderName">
        /// Specifies the PSProvider name that is a prefix of <paramref name="path" /> followed by a PSProvider separator.
        /// </param>
        /// <returns>
        /// Returns a variable name string; otherwise <see cref="string.Empty" />.
        /// </returns>
        private string RemovePSProvider(string? path, string? psProviderName)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                return string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(psProviderName))
            {
                return string.Empty;
            }
            else
            {
                int index = path.IndexOf(PSPROVIDER_SEPARATOR);

                return path.StartsWith(psProviderName, StringComparison.OrdinalIgnoreCase) && index >= 0
                    ? path[(index + PSPROVIDER_SEPARATOR.Length)..]
                    : path;
            }
        }

        #endregion Private Methods
    }
}
