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
using System.Text;

namespace OctopusCmdlet.Utility
{
    public static class NullOrEmpty
    {
        #region Public Methods

        /// <summary>
        /// Determines whether <paramref name="obj" /> is <see langref="null" />.
        /// </summary>
        /// <param name="obj">
        /// Specifies the object under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="obj" /> is <see langref="null" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull(object? obj) => obj == null;

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" /> or empty; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull(string? value) => NullOrEmpty.IsNull(value);

        /// <summary>
        /// Determines whether <paramref name="array" /> is <see langref="null" />.
        /// </summary>
        /// <param name="array">
        /// Specifies the <see cref="Array" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="array" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull(Array? array) => array?.Cast<object?>().All(e => e == null) != false;

        /// <summary>
        /// Determines whether <paramref name="list" /> is <see langref="null" />.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="list" />.
        /// </typeparam>
        /// <param name="list">
        /// Specifies the <see cref="IEnumerable{T}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="list" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull<T>(IEnumerable<T?> list) => list?.All(e => e == null) != false;

        /// <summary>
        /// Determines whether <paramref name="collection" /> is <see langref="null" />.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="collection" />.
        /// </typeparam>
        /// <param name="collection">
        /// Specifies the <see cref="ICollection{T}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="collection" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull<T>(ICollection<T?> collection) => collection?.All(e => e == null) != false;

        /// <summary>
        /// Determines whether <paramref name="dictionary" /> is <see langref="null" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// Specifies the element type for the keys <paramref name="dictionary" />.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Specifies the element type for the values <paramref name="dictionary" />.
        /// </typeparam>
        /// <param name="dictionary">
        /// Specifies the <see cref="IDictionary{TKey, TValue}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="dictionary" /> is <see langref="null" /> or all of its values are
        /// <see langref="null" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull<TKey, TValue>(IDictionary<TKey, TValue?> dictionary) => dictionary?.All(k => k.Value == null) != false;

        /// <summary>
        /// Determines whether <paramref name="pair" /> is <see langref="null" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// Specifies the element type for the key <paramref name="pair" />.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Specifies the element type for the value <paramref name="pair" />.
        /// </typeparam>
        /// <param name="pair">
        /// Specifies the <see cref="KeyValuePair{TKey, TValue}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="pair" /> is <see langref="null" /> or its value is <see langref="null" />;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNull<TKey, TValue>(KeyValuePair<TKey, TValue?> pair) => NullOrEmpty.IsNull(pair.Value);

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all <see cref="Ascii" /> characters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all <see cref="Ascii" />
        /// characters; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrAscii(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsAscii(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all <see cref="Ascii" /> decimal digit.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all <see cref="Ascii" />
        /// decimal digit; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrAsciiDigit(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsAsciiDigit(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all members of a Unicode range of characters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <param name="minimumInclusive">
        /// Specifies the minimum or floor Unicode character in the range inclusive.
        /// </param>
        /// <param name="maximumInclusive">
        /// Specifies the maximum or ceiling Unicode character in the range inclusive.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all member of a Unicode range
        /// of characters between [ <paramref name="minimumInclusive" />, <paramref name="maximumInclusive" />]; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrBetween(string? value, char minimumInclusive, char maximumInclusive) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsBetween(c, minimumInclusive, maximumInclusive));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode control characters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode control characters;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrControl(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsControl(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode decimal digits.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode decimal digits;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrDigit(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsDigit(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" /> or empty.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" /> or empty; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrEmpty(string? value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// Determines whether <paramref name="array" /> is <see langref="null" /> or empty.
        /// </summary>
        /// <param name="array">
        /// Specifies the <see cref="Array" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="array" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" /> or <paramref name="array" /> has no elements; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrEmpty(Array? array) => NullOrEmpty.IsNull(array) || array?.Length < 1;

        /// <summary>
        /// Determines whether <paramref name="list" /> is <see langref="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="list" />.
        /// </typeparam>
        /// <param name="list">
        /// Specifies the <see cref="IEnumerable{T}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="list" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" /> or <paramref name="list" /> has no elements; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrEmpty<T>(IEnumerable<T?> list) => NullOrEmpty.IsNull(list) || list?.Count() < 1;

        /// <summary>
        /// Determines whether <paramref name="collection" /> is <see langref="null" /> or empty.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="collection" />.
        /// </typeparam>
        /// <param name="collection">
        /// Specifies the <see cref="ICollection{T}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="collection" /> is <see langref="null" /> or all of its elements are
        /// <see langref="null" /> or <paramref name="collection" /> has no elements; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrEmpty<T>(ICollection<T?> collection) => NullOrEmpty.IsNull(collection) || collection?.Count < 1;

        /// <summary>
        /// Determines whether <paramref name="dictionary" /> is <see langref="null" /> or empty.
        /// </summary>
        /// <typeparam name="TKey">
        /// Specifies the element type for the keys <paramref name="dictionary" />.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Specifies the element type for the values <paramref name="dictionary" />.
        /// </typeparam>
        /// <param name="dictionary">
        /// Specifies the <see cref="IDictionary{TKey, TValue}" /> under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="dictionary" /> is <see langref="null" /> or all of its elements' values are
        /// <see langref="null" /> or <paramref name="dictionary" /> has no elements; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrEmpty<TKey, TValue>(IDictionary<TKey, TValue?> dictionary) => NullOrEmpty.IsNull(dictionary) || dictionary?.Count < 1;

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode letters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode letters; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrLetter(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsLetter(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode letters or decimal digits.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode letters or decimal
        /// digits; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrLetterOrDigit(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsLetterOrDigit(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode lower case letters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode lower case letters;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrLower(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsLower(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode numbers (including
        /// hexadecimal digits).
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode numbers; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrNumber(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsNumber(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode punctuation.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode punctuation;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrPunctuation(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsPunctuation(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode separators.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode separators;
        /// otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrSeparator(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsSeparator(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode symbols.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode symbols; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrSymbol(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsSymbol(c));

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all Unicode upper case characters.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all Unicode upper case
        /// characters; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrUpper(string? value) => NullOrEmpty.IsNullOrEmpty(value) || NullOrEmpty.IsNullOrValue(value, c => char.IsUpper(c));

        /// <summary>
        /// Determines whether <paramref name="array" /> is <see langref="null" />, empty, or all elements satisfy <paramref name="predicate" />.
        /// </summary>
        /// <param name="array">
        /// Specifies the <see cref="Array" /> under test.
        /// </param>
        /// <param name="predicate">
        /// Specifies the <see cref="Func{T, TResult}" /> defining the test for value.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="array" /> is <see langref="null" />, empty, or all elements satisfy
        /// <paramref name="predicate" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrValue(Array? array, Func<object?, bool> predicate) => NullOrEmpty.IsNullOrEmpty(array) || array?.Cast<object>().All(e => predicate.Invoke(e)) != false;

        /// <summary>
        /// Determines whether <paramref name="list" /> is <see langref="null" />, empty, or all elements satisfy <paramref name="predicate" />.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="list" />.
        /// </typeparam>
        /// <param name="list">
        /// Specifies the <see cref="IEnumerable{T}" /> under test.
        /// </param>
        /// <param name="predicate">
        /// Specifies the <see cref="Func{T, TResult}" /> defining the test for value.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="list" /> is <see langref="null" />, empty, or all elements satisfy
        /// <paramref name="predicate" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrValue<T>(IEnumerable<T?> list, Func<T?, bool> predicate) => NullOrEmpty.IsNullOrEmpty(list) || list?.All(e => predicate.Invoke(e)) != false;

        /// <summary>
        /// Determines whether <paramref name="collection" /> is <see langref="null" />, empty, or all elements satisfy <paramref name="predicate" />.
        /// </summary>
        /// <typeparam name="T">
        /// Specifies the element type for <paramref name="collection" />.
        /// </typeparam>
        /// <param name="collection">
        /// Specifies the <see cref="ICollection{T}" /> under test.
        /// </param>
        /// <param name="predicate">
        /// Specifies the <see cref="Func{T, TResult}" /> defining the test for value.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="collection" /> is <see langref="null" />, empty, or all elements satisfy
        /// <paramref name="predicate" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrValue<T>(ICollection<T?> collection, Func<T?, bool> predicate) => NullOrEmpty.IsNullOrEmpty(collection) || collection?.All(e => predicate.Invoke(e)) != false;

        /// <summary>
        /// Determines whether <paramref name="dictionary" /> is <see langref="null" />, empty, or all elements satisfy <paramref name="predicate" />.
        /// </summary>
        /// <typeparam name="TKey">
        /// Specifies the element type for the keys <paramref name="dictionary" />.
        /// </typeparam>
        /// <typeparam name="TValue">
        /// Specifies the element type for the values <paramref name="dictionary" />.
        /// </typeparam>
        /// <param name="dictionary">
        /// Specifies the <see cref="IDictionary{TKey, TValue}" /> under test.
        /// </param>
        /// <param name="predicate">
        /// Specifies the <see cref="Func{T, TResult}" /> defining the test for value where <c> T </c> is of type
        /// <see cref="KeyValuePair{TKey, TValue}" /> and <c> TResult </c> is of type <see cref="bool" />.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="dictionary" /> is <see langref="null" />, empty, or all elements satisfy
        /// <paramref name="predicate" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrValue<TKey, TValue>(IDictionary<TKey, TValue?> dictionary, Func<KeyValuePair<TKey, TValue?>, bool> predicate) => NullOrEmpty.IsNullOrEmpty(dictionary) || dictionary?.All(k => predicate.Invoke(k)) != false;

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all elements satisfy <paramref name="predicate" />.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <param name="predicate">
        /// Specifies the <see cref="Func{T, TResult}" /> defining the test for value.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all elements satisfy
        /// <paramref name="predicate" />; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrValue(string? value, Func<char, bool> predicate) => string.IsNullOrEmpty(value) || value?.All(c => predicate.Invoke(c)) != false;

        /// <summary>
        /// Determines whether <paramref name="value" /> is <see langref="null" />, empty, or all whitespace.
        /// </summary>
        /// <param name="value">
        /// Specifies the string under test.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if <paramref name="value" /> is <see langref="null" />, empty, or all whitespace; otherwise, <see langref="false" />.
        /// </returns>
        public static bool IsNullOrWhiteSpace(string? value) => string.IsNullOrWhiteSpace(value);

        #endregion Public Methods
    }
}
