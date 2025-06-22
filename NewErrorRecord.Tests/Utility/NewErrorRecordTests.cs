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
using Moq;

using OctopusCmdlet.Utility;

using System.Management.Automation;

namespace NewErrorRecord.Tests.Utility
{
    [TestClass]
    public class NewErrorRecordTests
    {
        #region Public Constructors

        public NewErrorRecordTests()
        {
            FormatErrorId = new();
            NewErrorRecord = new();
        }

        #endregion Public Constructors

        #region Internal Properties

        internal OctopusCmdlet.Utility.FormatErrorId FormatErrorId { get; }

        internal OctopusCmdlet.Utility.NewErrorRecord NewErrorRecord { get; }

        #endregion Internal Properties

        #region Public Methods

        [TestMethod]
        public void NewErrorRecordCommand_ExceptionNoExtra_ExpectedErrorIdEqualsActualErrorId()
        {
            // Arrange
            Exception exception = new InvalidOperationException("This is a test exception message");
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string expected = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand(
                exception: exception,
                expected,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.FullyQualifiedErrorId;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_ExceptionNoExtra_ExpectedExceptionEqualsActualException()
        {
            // Arrange
            Exception expected = new InvalidOperationException("This is a test exception message");
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand(
                expected,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.Exception;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_ExceptionNoExtra_ExpectedTargetObjectEqualsActualTargetObject()
        {
            // Arrange
            Exception exception = new InvalidOperationException("This is a test exception message");
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-2";
            object? expected = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand(
                exception,
                errorId,
                category,
                expected,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.TargetObject;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_ExceptionNoExtra_ReturnTypeErrorRecord()
        {
            // Arrange
            Exception exception = new InvalidOperationException("This is a test exception message");
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var actual = NewErrorRecord.NewErrorRecordCommand(
                exception,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            // Assert
            Assert.IsInstanceOfType<ErrorRecord>(actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_MessageNoExtra_ExpectedErrorCategoryEqualsActualErrorCategory()
        {
            // Arrange
            const string message = "This is a test message";
            const ErrorCategory expected = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                errorId,
                expected,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.CategoryInfo.Category;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_MessageNoExtra_ExpectedErrorIdEqualsActualErrorId()
        {
            // Arrange
            const string message = "This is a test message";
            const ErrorCategory errorCategory = default(global::System.Management.Automation.ErrorCategory);
            const string expected = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                expected,
                errorCategory,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.FullyQualifiedErrorId;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_MessageNoExtra_ExpectedMessageEqualsActualMessage()
        {
            // Arrange
            const string expected = "This is a test message";
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                expected,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.Exception.Message;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_MessageNoExtra_ExpectedTargetObjectEqualsActualTargetObject()
        {
            // Arrange
            const string message = "This is a test message";
            const ErrorCategory errorCategory = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-1";
            object? expected = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                errorId,
                errorCategory,
                expected,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.TargetObject;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void NewErrorRecordCommand_MessageNoExtra_ReturnTypeErrorRecord()
        {
            // Arrange
            const string message = "This is a test message";
            const ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            const string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var actual = NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            // Assert
            Assert.IsInstanceOfType<ErrorRecord>(actual);
            this.mockRepository.VerifyAll();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ExpectedCategoryActivityEqualsActualCategoryActivity()
        {
            // Arrange
            ErrorRecord errorRecord = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? recommendedAction = "Test recommended action";
            const string? expected = "Processing Record";
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                expected,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.CategoryInfo.Activity;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ExpectedCategoryReasonEqualsActualCategoryReason()
        {
            // Arrange
            ErrorRecord errorRecord = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? recommendedAction = "Test recommended action";
            const string? categoryActivity = "Processing Record";
            const string? expected = "Invalid Operation";
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                expected,
                categoryTargetName,
                categoryTargetType);
            var actual = result.CategoryInfo.Reason;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ExpectedCategoryTargetNameEqualsActualCategoryTargetName()
        {
            // Arrange
            ErrorRecord errorRecord = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? recommendedAction = "Test recommended action";
            const string? categoryActivity = "Processing Record";
            const string? categoryReason = "Invalid Operation";
            const string? expected = "InputObject";
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                expected,
                categoryTargetType);
            var actual = result.CategoryInfo.TargetName;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ExpectedCategoryTargetTypeEqualsActualCategoryTargetType()
        {
            // Arrange
            ErrorRecord errorRecord = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? recommendedAction = "Test recommended action";
            const string? categoryActivity = "Processing Record";
            const string? categoryReason = "Invalid Operation";
            const string? categoryTargetName = "InputObject";
            const string? expected = "System.String";

            // Act
            var result = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                expected);
            var actual = result.CategoryInfo.TargetType;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ExpectedRecommendedActionEqualsActualRecommendAction()
        {
            // Arrange
            ErrorRecord errorRecord = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? expected = "Test recommended action";
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var result = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                expected,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            var actual = result.ErrorDetails.RecommendedAction;

            // Assert
            Assert.AreEqual(expected, actual);
            this.mockRepository.VerifyAll();
        }

        [TestMethod]
        public void UpdateErrorRecordCommand_ErrorRecordNoExtra_ReturnTypeErrorRecord()
        {
            // Arrange
            ErrorRecord expected = NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                "Test-InvalidOperation-3",
                ErrorCategory.InvalidOperation,
                null);
            const string? recommendedAction = null;
            const string? categoryActivity = null;
            const string? categoryReason = null;
            const string? categoryTargetName = null;
            const string? categoryTargetType = null;

            // Act
            var actual = NewErrorRecord.UpdateErrorRecordCommand(
                expected,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            // Assert
            Assert.IsInstanceOfType<ErrorRecord>(actual);
            this.mockRepository.VerifyAll();
        }

        #endregion Public Methods

        #region Private Fields

        private MockRepository mockRepository;

        #endregion Private Fields
    }
}
