/* ****************************************************************************
BSD-3-CLAUSE (a/k/a MODIFIED BSD) LICENSE

Copyright (c) 2025 John Merryweather Cooper

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are
met:

1. Redistributions of source code must retain the above copyright
   notice, this list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright
   notice, this list of conditions and the following disclaimer in the
   documentation and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
“AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
**************************************************************************** */

// Ignore Spelling: cmdlet

using Microsoft.PowerShell.Commands;

using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateApplicationPath' validate arguments attribute.
    /// </summary>
    public class ValidateApplicationPathAttribute : ValidateArgumentsAttribute
    {
        #region Private Fields

        /// <summary>
        /// Constant string representing the PowerShell PSProvider separator.
        /// </summary>
        private const string PSPROVIDER_SEPARATOR = ":\\";

        #endregion Private Fields

        #region Public Constructors

        public ValidateApplicationPathAttribute()
        {
            ValidatorName = nameof(ValidateApplicationPathAttribute);

            All = false;
            ListImported = false;
            ShowCommandInfo = false;
            Syntax = false;
            UseAbbreviationExpansion = false;
            UseFuzzyMatching = false;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the validator gets all commands that are installed on the computer, including
        /// cmdlets, aliases, functions, filters, scripts, and applications
        /// </summary>
        public bool All { get; set; }

        public object[] ArgumentList { get; set; }

        public CommandOrigin CommandOrigin { get; set; }

        public ICommandRuntime CommandRuntime { get; set; }

        [ValidateSet("Alias", "Function", "Filter", "Cmdlet", "ExternalScript", "Application", "Script", "Configuration", "All")]
        public CommandTypes CommandType { get; set; }

        public ModuleSpecification[] FullyQualifiedModule { get; set; }

        public uint FuzzyMinimumDistance { get; set; }

        public bool ListImported { get; set; }

        [SupportsWildcards]
        public string[] Module { get; set; }

        [SupportsWildcards]
        public string[] Noun { get; set; }

        [SupportsWildcards]
        public string[] ParameterName { get; set; }

        [SupportsWildcards]
        public PSTypeName[] ParameterType { get; set; }

        public bool ShowCommandInfo { get; set; }

        public bool Syntax { get; set; }

        public int TotalCount { get; set; }

        public bool UseAbbreviationExpansion { get; set; }

        public bool UseFuzzyMatching { get; set; }

        [SupportsWildcards]
        public string[] Verb { get; set; }

        #endregion Public Properties

        #region Internal Properties

        internal string ValidatorName { get; }

        #endregion Internal Properties

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
                    All = All,
                    ListImported = ListImported,
                    Name = [name],
                    ShowCommandInfo = ShowCommandInfo,
                    Syntax = Syntax,
                    UseAbbreviationExpansion = UseAbbreviationExpansion,
                    UseFuzzyMatching = UseFuzzyMatching,
                };

                if (ArgumentList != null && ArgumentList.Length > 0)
                {
                    getCommand.ArgumentList = ArgumentList;
                }

                getCommand.CommandType = All ? CommandTypes.All : CommandTypes.Cmdlet;

                if (FullyQualifiedModule != null && FullyQualifiedModule.Length > 0 && Module == null)
                {
                    getCommand.FullyQualifiedModule = FullyQualifiedModule;
                }

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
