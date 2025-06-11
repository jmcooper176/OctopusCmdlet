// Ignore Spelling: Cmdlet

using System.Diagnostics.CodeAnalysis;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Fatal")]
    [OutputType(typeof(void))]
    public class WriteFatal : PSCmdlet
    {
        #region Public Constructors

        public WriteFatal()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Internal Properties

        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Public Methods

        [DoesNotReturn]
        public void WriteFatalCommand(
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
            this.ThrowTerminatingError(errorRecord);
        }

        [DoesNotReturn]
        public void WriteFatalCommand<TException>(
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

            WriteFatalCommand(er);
        }

        [DoesNotReturn]
        public void WriteFatalCommand(
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

            WriteFatalCommand(er);
        }

        #endregion Public Methods
    }
}
