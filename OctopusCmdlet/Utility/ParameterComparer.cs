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

using System.Diagnostics.CodeAnalysis;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// The parameter comparer class implementing key comparison for the <see cref="BoundParameterDictionary" /> class.
    /// </summary>
    public class ParameterComparer : EqualityComparer<string>, IAlternateEqualityComparer<ReadOnlySpan<char>, string>, IAlternateEqualityComparer<KeyValuePair<string, object>, string>
    {
        #region Public Methods

        /// <summary>
        /// Validates that a string is a valid PowerShell parameter name.
        /// </summary>
        /// <param name="name">
        /// Specifies the PowerShell parameter name to test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="name" /> is a valid PowerShell parameter name; otherwise, <see langref="false" />.
        /// </returns>
        /// <remarks>
        /// A valid PowerShell parameter name must:
        /// <list type="bullet">
        /// <item> Not be <see langref="null" />, empty, or all whitespace; </item>
        /// <item> Not contain any whitespace; </item>
        /// <item> Not contain any symbols; </item>
        /// <item> Not equal any PowerShell reserved word; </item>
        /// <item> Not start with a decimal digit; AND </item>
        /// <item> Valid if it is all letters or decimal digits OR all other tests pass. </item>
        /// </list>
        /// </remarks>
        public static bool ValidateParameterName(string? name)
        {
            List<string> reservedWords =
            [
                "Begin",
                "Break",
                "Catch",
                "Class",
                "Continue",
                "Data",
                "Default",
                "Do",
                "DynamicParam",
                "Else",
                "ElseIf",
                "End",
                "Exit",
                "Filter",
                "Finally",
                "For",
                "ForEach",
                "From",
                "Function",
                "If",
                "In",
                "InlineScript",
                "New",
                "Param",
                "Process",
                "Return",
                "Switch",
                "Throw",
                "Trap",
                "Try",
                "Until",
                "Using",
                "Var",
                "While"
            ];

            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            else
            {
                return !name.Any(c => char.IsWhiteSpace(c))
                    && (name.Any(c => char.IsSymbol(c) && c != '_'))
                    && (reservedWords.Any(r => r.Equals(name, StringComparison.OrdinalIgnoreCase)))
                    && (name.FirstOrDefault(c => char.IsDigit(c)) != default
                    && (name.All(c => char.IsLetterOrDigit(c)) || true));
            }
        }

        /// <inheritdoc />
        public string Create(ReadOnlySpan<char> alternate)
        {
            return alternate.ToString();
        }

        /// <inheritdoc />
        public string Create(KeyValuePair<string, object> alternate)
        {
            return alternate.Key;
        }

        /// <inheritdoc />
        public override bool Equals(string? x, string? y)
        {
            if (ParameterComparer.ValidateParameterName(x) && ParameterComparer.ValidateParameterName(y))
            {
                return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
            }
            else
            {
                return false;
            }
        }

        /// <inheritdoc />
        public bool Equals(ReadOnlySpan<char> alternate, string other)
        {
            return this.Equals(x: this.Create(alternate), y: other);
        }

        /// <inheritdoc />
        public bool Equals(KeyValuePair<string, object> alternate, string other)
        {
            return this.Equals(x: this.Create(alternate), y: other);
        }

        /// <inheritdoc />
        public override int GetHashCode([DisallowNull] string obj)
        {
            return string.GetHashCode(obj, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc />
        public int GetHashCode(ReadOnlySpan<char> alternate)
        {
            return this.GetHashCode(obj: this.Create(alternate));
        }

        /// <inheritdoc />
        public int GetHashCode(KeyValuePair<string, object> alternate)
        {
            return this.GetHashCode(obj: this.Create(alternate));
        }

        #endregion Public Methods
    }
}
