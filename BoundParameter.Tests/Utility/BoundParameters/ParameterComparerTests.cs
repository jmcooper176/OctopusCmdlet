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
using OctopusCmdlet.Utility.BoundParameters;

namespace BoundParameterDictionary.Tests.Utility.BoundParameters
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
            ReadOnlySpan<char> alternate = default;

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
            ReadOnlySpan<char> alternate = default;
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
        public void GetHashCode_EmptyKeyValuePair_ResultIsInstanceOfTypeInt()
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
            ReadOnlySpan<char> alternate = default;

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
            const string? name = null;

            // Act
            var result = ParameterComparer.ValidateParameterName(name);

            // Assert
            Assert.IsFalse(result);
        }

        #endregion Public Methods
    }
}
