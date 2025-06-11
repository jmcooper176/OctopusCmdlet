using System;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Progress")]
    [OutputType(typeof(void))]
    public class WriteProgress : PSCmdlet
    {
        #region Public Methods

        public void WriteProgressCommand(ProgressRecord progressRecord)
        {
            base.WriteProgress(progressRecord);
        }

        public void WriteProgressCommand(
            int activityId,
            string? currentOperation = null,
            int parentActivityId = 0,
            int percentComplete = -1,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            int secondsRemaining = -1)
        {
            var pr = new ProgressRecord(activityId);

            if (!string.IsNullOrWhiteSpace(currentOperation))
            {
                pr.CurrentOperation = currentOperation;
            }

            if (parentActivityId > 0)
            {
                pr.ParentActivityId = parentActivityId;
            }

            if (percentComplete >= -1)
            {
                pr.PercentComplete = percentComplete;
            }

            pr.RecordType = recordType;

            if (secondsRemaining >= -1)
            {
                pr.SecondsRemaining = secondsRemaining;
            }

            WriteProgressCommand(pr);
        }

        public void WriteProgressCommand(
            int activityId,
            string activity,
            string statusDescription,
            string? currentOperation = null,
            int parentActivityId = 0,
            int percentComplete = -1,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            int secondsRemaining = -1)
        {
            var pr = new ProgressRecord(activityId, activity, statusDescription);

            if (!string.IsNullOrWhiteSpace(currentOperation))
            {
                pr.CurrentOperation = currentOperation;
            }

            if (parentActivityId > 0)
            {
                pr.ParentActivityId = parentActivityId;
            }

            if (percentComplete >= -1)
            {
                pr.PercentComplete = percentComplete;
            }

            pr.RecordType = recordType;

            if (secondsRemaining >= -1)
            {
                pr.SecondsRemaining = secondsRemaining;
            }

            WriteProgressCommand(pr);
        }

        #endregion Public Methods
    }
}
