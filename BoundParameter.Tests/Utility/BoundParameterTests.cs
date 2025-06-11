using Markdig.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OctopusCmdlet.Utility;

using System;
using System.Collections.Generic;

namespace BoundParameter.Tests.Utility
{
    [TestClass]
    public class BoundParameterTests
    {
        #region Public Methods

        [TestMethod]
        public void Add_EmptyKeyValue_ThrowArgumentException()
        {
            // Arrange
            OctopusCmdlet.Utility.BoundParameter boundParameter = new(new Dictionary<string, object>());
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => boundParameter.Add(key, value));
        }

        [TestMethod]
        public void Add_EmptyKeyValuePair_ThrowArgumentException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentException>(() => boundParameter.Add(item));
        }

        [TestMethod]
        public void Clear_EmptyIDictionaryClear_BoundParameterCountIsLessThanOne()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());

            // Act
            boundParameter.Clear();

            // Assert
            Assert.IsTrue(boundParameter.Count < 1);
        }

        [TestMethod]
        public void Contains_EmptyBoundParameterKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Contains(item));
        }

        [TestMethod]
        public void ContainsKey_EmptyBoundParameterKey_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.ContainsKey(key));
        }

        [TestMethod]
        public void ContainsValue_EmptyBoundParameterValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            object value = new();

            // Act
            var result = boundParameter.ContainsValue(value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterArrayZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            Array array = new object[0];
            int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterKeyValuePairZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            KeyValuePair<string, object>[] array = boundParameter.ToArray();
            int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void CopyTo_EmptyBoundParameterStringArrayZeroIndex_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string[] array = [];
            int index = 0;

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.CopyTo(array, index));
        }

        [TestMethod]
        public void EnsureCapacity_EmptyBoundParameterZeroExpectedCapacity_ExpectedAndActualCapacityAreEqual()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            int expected = 0;

            // Act
            var actual = boundParameter.EnsureCapacity(expected);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetAlternateLookup_EmptyBoundParameterReadOnlySpanCharAlternateLookup_ResultIsInstanceOfTypeAlternateLookup()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());

            // Act
            var result = boundParameter.GetAlternateLookup<ReadOnlySpan<char>>();

            // Assert
            Assert.IsInstanceOfType<Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>>>(result);
        }

        [TestMethod]
        public void GetEnumerator_EmptyBoundParameter_ResultIsInstanceOfIEnumerator()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());

            // Act
            var result = boundParameter.GetEnumerator();

            // Assert
            Assert.IsInstanceOfType<IEnumerator<KeyValuePair<string, object>>>(result);
        }

        [TestMethod]
        public void HasParameter_EmptyBoundParameterEmptyName_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string name = string.Empty;

            // Act
            var result = boundParameter.HasParameter(name);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void HasParameter_EmptyBoundParameterNameValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string name = string.Empty;
            object value = new();

            // Act
            var result = boundParameter.HasParameter(name, value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterKey_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;

            // Act
            var result = boundParameter.Remove(key);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterKeyOutValue_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;

            // Act
            var result = boundParameter.Remove(key, out object? value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Remove_EmptyBoundParameterKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Remove(item));
        }

        [TestMethod]
        public void TrimExcess_EmptyBoundParameterTrimExcess_ZeroEqualsCapacity()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());

            // Act
            boundParameter.TrimExcess();

            // Assert
            Assert.AreEqual(0, boundParameter.Capacity);
        }

        [TestMethod]
        public void TrimExcess_EmptyBoundParameterTrimExcessZero_ExpectedIsActualCapacity()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            int expected = 0;

            // Act
            boundParameter.TrimExcess(expected);

            // Assert
            Assert.AreEqual(expected, boundParameter.Capacity);
        }

        [TestMethod]
        public void TryAdd_EmptyBoundParameterKeyValue_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.TryAdd(key, value));
        }

        [TestMethod]
        public void TryGetAlternateLookup_EmptyBoundParameterAlternateLookupReadOnlySpanChar_ResultIsNotNull()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());

            // Act
            var result = boundParameter.TryGetAlternateLookup<ReadOnlySpan<char>>(out Dictionary<string, object>.AlternateLookup<ReadOnlySpan<char>> lookup);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TryGetValue_EmptyBoundParameterKey_ResultIsFalse()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;

            // Act
            var result = boundParameter.TryGetValue(key, out object? value);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Update_EmptyBoundParameterKeyValue_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            string key = string.Empty;
            object value = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Update(key, value));
        }

        [TestMethod]
        public void Update_EmptyBoundParameterKeyValuePair_ThrowsArgumentNullException()
        {
            // Arrange
            var boundParameter = new OctopusCmdlet.Utility.BoundParameter(new Dictionary<string, object>());
            KeyValuePair<string, object> item = new();

            // Act and Assert
            Assert.ThrowsException<ArgumentNullException>(() => boundParameter.Update(item));
        }

        #endregion Public Methods
    }
}
