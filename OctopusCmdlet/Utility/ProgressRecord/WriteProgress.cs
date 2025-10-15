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
using System.Management.Automation;

namespace OctopusCmdlet.Utility.ProgressRecord
{
    /// <summary>
    /// Implements the <c> Write-Progress </c><see cref="PowerShell" /><see cref="Cmdlet" /> extensions.
    /// </summary>
    [Cmdlet(VerbsCommunications.Write, "Progress")]
    [CmdletBinding]
    [OutputType(typeof(void))]
    public class WriteProgress : PSCmdlet
    {
        #region Public Methods

        public virtual void WriteProgressCommand(System.Management.Automation.ProgressRecord progressRecord)
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
            var pr = new System.Management.Automation.ProgressRecord(activityId);

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
            var pr = new System.Management.Automation.ProgressRecord(activityId);

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
            System.Management.Automation.ProgressRecord pr = new(activityId, activity, statusDescription);

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
            System.Management.Automation.ProgressRecord pr = new(activityId, activity, statusDescription);

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

        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteProgress" /> class.
        /// </summary>
        public WriteProgress()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating this <see cref="Cmdlet" /> name.
        /// </summary>
        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Protected Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            DefaultProcessing.InitializeBeginProcessing(CmdletName, MyInvocation.BoundParameters, SessionState, Stopping);
        }

        /// <inheritdoc />
        /// <exception cref="PipelineStoppedException">
        /// Always throws when <see cref="StopProcessing" /> is called.
        /// </exception>
        protected override void StopProcessing()
        {
            base.StopProcessing();

            DefaultProcessing.InitializeStopProcessing(CmdletName, this, MyInvocation.ScriptLineNumber);
        }

        #endregion Protected Methods
    }
}
