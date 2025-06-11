// Ignore Spelling: Cmdlet

using Humanizer;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Net.NetworkInformation;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// The parameter comparer class implementing key comparison for the <see cref="BoundParameter" /> class.
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
        /// True if <paramref name="name" /> is a valid PowerShell parameter name; otherwise, false.
        /// </returns>
        /// <remarks>
        /// A valid PowerShell parameter name must:
        /// <list type="bullet">
        /// <item> Not be null, empty, or all whitespace; </item>
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
