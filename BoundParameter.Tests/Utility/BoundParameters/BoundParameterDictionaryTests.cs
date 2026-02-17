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
namespace BoundParameterDictionary.Tests.Utility.BoundParameters
{
    /// <summary>
    /// The bound parameter dictionary_ tests.
    /// </summary>
    [TestClass]
    public class BoundParameterDictionaryUnitAndIntegrationTests
    {
        #region Private Methods

        /// <summary>
        /// The <see cref="OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary" /> class factory using a
        /// <see cref="Dictionary{TKey, TValue}" /> for initialization.
        /// </summary>
        /// <returns>
        /// A <see cref="OctopusCmdlet.Utility.BoundParameterDictionary(Dictionary{TKey, TValue})" />.
        /// </returns>
        private static OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary ClassUnderTestFactory()
        {
            return [.. new Dictionary<string, object>()];
        }

        /// <summary>
        /// The <see cref="OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary" /> class factory using a
        /// <see cref="Dictionary{TKey, TValue}" /> for initialization.
        /// </summary>
        /// <returns>
        /// A <see cref="OctopusCmdlet.Utility.BoundParameterDictionary(Dictionary{TKey, TValue})" />.
        /// </returns>
        private static OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary ClassUnderTestFactory(IEqualityComparer<string> comparer)
        {
            return new OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary(new Dictionary<string, object>(), comparer);
        }

        /// <summary>
        /// The <see cref="OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary" /> class factory using a
        /// <see cref="IEnumerable{T}" /> for initialization.
        /// </summary>
        /// <returns>
        /// A <see cref="OctopusCmdlet.Utility.BoundParameterDictionary(IEnumerable{KeyValuePair{TKey, TValue}})" />.
        /// </returns>
        private static OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary ClassUnderTestFactoryList()
        {
            return new([]);
        }

        /// <summary>
        /// The <see cref="OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary" /> class factory using a
        /// <see cref="IEnumerable{T}" /> and a <see cref="IEqualityComparer{T}" /> for initialization.
        /// </summary>
        /// <param name="comparer">
        /// </param>
        /// <returns>
        /// A <see cref="OctopusCmdlet.Utility.BoundParameterDictionary(IEnumerable{KeyValuePair{TKey, TValue}})" />.
        /// </returns>
        private static OctopusCmdlet.Utility.BoundParameters.BoundParameterDictionary ClassUnderTestFactoryList(IEqualityComparer<string> comparer)
        {
            return new([], comparer);
        }

        #endregion Private Methods

        #region Public Methods

        [TestMethod]
        public void Add_EmptyKeyValue_ThrowArgumentException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => boundParameter.Add(key, value));
        }

        [TestMethod]
        public void Add_EmptyKeyValuePair_ThrowArgumentException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => boundParameter.Add(item));
        }

        [TestMethod]
        public void Clear_EmptyIDictionaryClear_BoundParameterDictionaryCountIsLessThanOne()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();

            // Act
            boundParameter.Clear();

            // Assert
            Assert.IsTrue(boundParameter.Count < 1);
        }

        [TestMethod]
        public void Contains_EmptyBoundParameterDictionaryKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Contains(item));
        }

        [TestMethod]
        public void ContainsKey_EmptyBoundParameterDictionaryKey_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.ContainsKey(key));
        }

        [TestMethod]
        public void ContainsValue_EmptyBoundParameterDictionaryValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            object value = new();

            // Act
            var result = boundParameter.ContainsValue(value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterDictionaryArrayZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            Array array = Array.Empty<object>();
            const int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterDictionaryKeyValuePairZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            KeyValuePair<string, object>[] array = [.. boundParameter];
            const int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterDictionaryStringArrayZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string[] array = [];
            const int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void EnsureCapacity_EmptyBoundParameterDictionaryZeroExpectedCapacity_ExpectedAndActualCapacityAreEqual()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            const int expected = 0;

            // Act
            var actual = boundParameter.EnsureCapacity(expected);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAlternateLookup_EmptyBoundParameterDictionaryReadOnlySpanCharAlternateLookup_ResultIsInstanceOfTypeAlternateLookup()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();

            // Act
            var result = boundParameter.GetAlternateLookup<ReadOnlySpan<char>>();

            // Assert
            Assert.IsInstanceOfType<Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>>>(result);
        }

        [TestMethod]
        public void GetEnumerator_EmptyBoundParameterDictionary_ResultIsInstanceOfIEnumerator()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();

            // Act
            var result = boundParameter.GetEnumerator();

            // Assert
            Assert.IsInstanceOfType<IEnumerator<KeyValuePair<string, object>>>(result);
        }

        [TestMethod]
        public void HasParameter_EmptyBoundParameterDictionaryEmptyName_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string name = string.Empty;

            // Act
            var result = boundParameter.HasParameter(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasParameter_EmptyBoundParameterDictionaryNameValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string name = string.Empty;
            object value = new();

            // Act
            var result = boundParameter.HasParameter(name, value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterDictionaryKey_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;

            // Act
            var result = boundParameter.Remove(key);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterDictionaryKeyOutValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;

            // Act
            var result = boundParameter.Remove(key, out object? value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterDictionaryKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Remove(item));
        }

        [TestMethod]
        public void TrimExcess_EmptyBoundParameterDictionaryTrimExcess_ZeroEqualsCapacity()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();

            // Act
            boundParameter.TrimExcess();

            // Assert
            Assert.AreEqual(0, boundParameter.Capacity);
        }

        [TestMethod]
        public void TrimExcess_EmptyBoundParameterDictionaryTrimExcessZero_ExpectedIsActualCapacity()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            const int expected = 0;

            // Act
            boundParameter.TrimExcess(expected);

            // Assert
            Assert.AreEqual(expected, boundParameter.Capacity);
        }

        [TestMethod]
        public void TryAdd_EmptyBoundParameterDictionaryKeyValue_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.TryAdd(key, value));
        }

        [TestMethod]
        public void TryGetAlternateLookup_EmptyBoundParameterDictionaryAlternateLookupReadOnlySpanChar_ResultIsTrue()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();

            // Act
            var result = boundParameter.TryGetAlternateLookup(out Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> lookup);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TryGetValue_EmptyBoundParameterDictionaryKey_ResultIsFalse()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;

            // Act
            var result = boundParameter.TryGetValue(key, out object? value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_EmptyBoundParameterDictionaryKeyValue_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Update(key, value));
        }

        [TestMethod]
        public void Update_EmptyBoundParameterDictionaryKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = ClassUnderTestFactory();
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Update(item));
        }

        #endregion Public Methods
    }
}
