using Moq;

using System.Management.Automation;

namespace NewErrorRecord.Tests.Utility
{
    [TestClass]
    public class NewErrorRecordTests
    {
        #region Public Methods

        [TestMethod]
        public void NewErrorRecordCommand_ExceptionNoExtra_ExpectedErrorIdEqualsActualErrorId()
        {
            // Arrange
            Exception exception = new InvalidOperationException("This is a test exception message");
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string expected = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                exception,
                category,
                expected,
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
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                expected,
                category,
                errorId,
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
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-2";
            object? expected = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                exception,
                category,
                errorId,
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
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-2";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var actual = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                exception,
                category,
                errorId,
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
            string message = "This is a test message";
            ErrorCategory expected = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                expected,
                errorId,
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
            string message = "This is a test message";
            ErrorCategory errorCategory = default(global::System.Management.Automation.ErrorCategory);
            string expected = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                errorCategory,
                expected,
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
            string expected = "This is a test message";
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                expected,
                category,
                errorId,
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
            string message = "This is a test message";
            ErrorCategory errorCategory = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-1";
            object? expected = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                errorCategory,
                errorId,
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
            string message = "This is a test message";
            ErrorCategory category = default(global::System.Management.Automation.ErrorCategory);
            string errorId = "Test-NewErrorRecordCommand-1";
            object? targetObject = null;
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var actual = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand<PSInvalidOperationException>(
                message,
                category,
                errorId,
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
            ErrorRecord errorRecord = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? recommendedAction = "Test recommended action";
            string? expected = "Processing Record";
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
            ErrorRecord errorRecord = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? recommendedAction = "Test recommended action";
            string? categoryActivity = "Processing Record";
            string? expected = "Invalid Operation";
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
            ErrorRecord errorRecord = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? recommendedAction = "Test recommended action";
            string? categoryActivity = "Processing Record";
            string? categoryReason = "Invalid Operation";
            string? expected = "InputObject";
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
            ErrorRecord errorRecord = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? recommendedAction = "Test recommended action";
            string? categoryActivity = "Processing Record";
            string? categoryReason = "Invalid Operation";
            string? categoryTargetName = "InputObject";
            string? expected = "System.String";

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
            ErrorRecord errorRecord = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? expected = "Test recommended action";
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var result = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
            ErrorRecord expected = OctopusCmdlet.Utility.NewErrorRecord.NewErrorRecordCommand(
                new InvalidOperationException("This is a test exception message"),
                ErrorCategory.InvalidOperation,
                "Test-InvalidOperation-3",
                null);
            string? recommendedAction = null;
            string? categoryActivity = null;
            string? categoryReason = null;
            string? categoryTargetName = null;
            string? categoryTargetType = null;

            // Act
            var actual = OctopusCmdlet.Utility.NewErrorRecord.UpdateErrorRecordCommand(
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
