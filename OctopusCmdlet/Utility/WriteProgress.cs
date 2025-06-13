using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Progress")]
    [OutputType(typeof(void))]
    public class WriteProgress : PSCmdlet
    {
        #region Public Methods

        public virtual void WriteProgressCommand(ProgressRecord progressRecord)
        {
            base.WriteProgress(progressRecord);
        }

        public virtual void WriteProgressCommand(
            int activityId,
            double percentComplete,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            string? currentOperation = null,
            int parentActivityId = 0)
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

            pr.RecordType = recordType;
            pr.PercentComplete = percentComplete <= 0.005 || pr.RecordType == ProgressRecordType.Completed ? 0 : Convert.ToInt32(percentComplete * 100);
            pr.SecondsRemaining = -1;

            WriteProgressCommand(pr);
        }

        public virtual void WriteProgressCommand(
            int activityId,
            TimeSpan remaining,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            string? currentOperation = null,
            int parentActivityId = 0)
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

            pr.PercentComplete = -1;
            pr.RecordType = recordType;
            pr.SecondsRemaining = remaining == TimeSpan.Zero || pr.RecordType == ProgressRecordType.Completed ? 0 : Convert.ToInt32(remaining.TotalSeconds);

            WriteProgressCommand(pr);
        }

        public virtual void WriteProgressCommand(
            int activityId,
            string activity,
            string statusDescription,
            double percentComplete,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            string? currentOperation = null,
            int parentActivityId = 0)
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

            pr.RecordType = recordType;
            pr.PercentComplete = percentComplete <= 0.005 || pr.RecordType == ProgressRecordType.Completed ? 0 : Convert.ToInt32(percentComplete * 100);
            pr.SecondsRemaining = -1;

            WriteProgressCommand(pr);
        }

        public virtual void WriteProgressCommand(
            int activityId,
            string activity,
            string statusDescription,
            TimeSpan remaining,
            ProgressRecordType recordType = ProgressRecordType.Processing,
            string? currentOperation = null,
            int parentActivityId = 0)
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

            pr.PercentComplete = -1;
            pr.RecordType = recordType;
            pr.SecondsRemaining = remaining == TimeSpan.Zero || pr.RecordType == ProgressRecordType.Completed ? 0 : Convert.ToInt32(remaining.TotalSeconds);

            WriteProgressCommand(pr);
        }

        #endregion Public Methods
    }
}
