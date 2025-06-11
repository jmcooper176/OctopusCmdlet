using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Error")]
    [OutputType(typeof(void))]
    public class WriteError : PSCmdlet
    {
        #region Public Methods

        public void WriteErrorCommand(
            ErrorRecord errorRecord,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null)
        {
            errorRecord = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            base.WriteError(errorRecord);
        }

        public void WriteErrorCommand<TException>(
            string message,
            string errorId,
            ErrorCategory category,
            object? targetObject,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null) where TException : Exception
        {
            var er = NewErrorRecord.NewErrorRecordCommand<TException>(
                message,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            WriteErrorCommand(
                er,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
        }

        public void WriteErrorCommand(
            Exception exception,
            string errorId,
            ErrorCategory category,
            object? targetObject,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null)
        {
            var er = NewErrorRecord.NewErrorRecordCommand(
                exception,
                errorId,
                category,
                targetObject,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            WriteErrorCommand(
                er,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
        }

        #endregion Public Methods
    }
}
