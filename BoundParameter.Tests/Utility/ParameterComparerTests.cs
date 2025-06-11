using OctopusCmdlet.Utility;

namespace BoundParameter.Tests.Utility
{
    [TestClass]
    public class ParameterComparerTests
    {
        #region Public Methods

        [TestMethod]
        public void Create_EmptyKeyValuePair_ResultIsNullOrEmpty()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            KeyValuePair<string, object> alternate = new();

            // Act
            var result = parameterComparer.Create(alternate);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Create_EmptyReadOnlySpanChar_ResultIsNullOrEmpty()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            ReadOnlySpan<char> alternate = default(global::System.ReadOnlySpan<global::System.Char>);

            // Act
            var result = parameterComparer.Create(alternate);

            // Assert
            Assert.IsTrue(string.IsNullOrEmpty(result));
        }

        [TestMethod]
        public void Equals_EmptyLeftKeyValuePairRightString_ResultIsFalse()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            KeyValuePair<string, object> alternate = new();
            string other = string.Empty;

            // Act
            var result = parameterComparer.Equals(alternate, other);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_EmptyLeftReadOnlySpanCharRightString_ResultIsFalse()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            ReadOnlySpan<char> alternate = default(global::System.ReadOnlySpan<global::System.Char>);
            string other = string.Empty;

            // Act
            var result = parameterComparer.Equals(alternate, other);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Equals_NullXAndY_ResultIsFalse()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            string? x = null;
            string? y = null;

            // Act
            var result = parameterComparer.Equals(x, y);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetHashCode_EmptyKeyValuePair_ResultIsInstanceofTypeInt()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            KeyValuePair<string, object> alternate = new();

            // Act
            var result = parameterComparer.GetHashCode(alternate);

            // Assert
            Assert.IsInstanceOfType<int>(result);
        }

        [TestMethod]
        public void GetHashCode_EmptyReadOnlySpanChar_ResultIsInstanceOfTypeInt()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            ReadOnlySpan<char> alternate = default(global::System.ReadOnlySpan<global::System.Char>);

            // Act
            var result = parameterComparer.GetHashCode(alternate);

            // Assert
            Assert.IsInstanceOfType<int>(result);
        }

        [TestMethod]
        public void GetHashCode_EmptyString_ResultIsInstanceOfTypeInt()
        {
            // Arrange
            var parameterComparer = new ParameterComparer();
            string obj = string.Empty;

            // Act
            var result = parameterComparer.GetHashCode(obj);

            // Assert
            Assert.IsInstanceOfType<int>(result);
        }

        [TestMethod]
        public void ValidateParameterName_NullName_ResultIsFalse()
        {
            // Arrange
            string? name = null;

            // Act
            var result = ParameterComparer.ValidateParameterName(name);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion Public Methods
    }
}
