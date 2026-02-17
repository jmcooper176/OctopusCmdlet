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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility.ProgressRecord
{
    /// <summary>
    /// Implements the <c> Trace-Progress </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsDiagnostic.Trace, "Progress")]
    [CmdletBinding]
    [OutputType(typeof(void))]
    public class TraceProgress : WriteProgress
    {
        #region Public Methods

        public void TraceProgressCommand(System.Management.Automation.ProgressRecord progressRecord)
        {
            System.Management.Automation.InformationRecord ir = new(
                $"{progressRecord.Activity} {progressRecord.StatusDescription}",
                progressRecord.ActivityId.ToString(CultureInfo.CurrentCulture));
            base.WriteInformation(ir);
            base.WriteProgressCommand(progressRecord);
        }

        public void TraceProgressCommand(int activityId, ProgressRecordType recordType = ProgressRecordType.Processing, string? currentOperation = null, int percentComplete = -1, int secondsRemaining = -1, int parentActivityId = 0)
        {
            string activity = $"ID {activityId} in Progress";
            string statusDescription = "Progress ->";

            if (percentComplete >= 0)
            {
                statusDescription = $"{percentComplete}% completed";
            }
            else if (secondsRemaining >= 0)
            {
                statusDescription = $"Estimate {secondsRemaining} seconds until completion";
            }
            else if (!string.IsNullOrWhiteSpace(currentOperation))
            {
                statusDescription = $"Current Operation {currentOperation} Processing:";
            }

            System.Management.Automation.InformationRecord ir = new(
                $"{activity} {statusDescription}",
                activityId.ToString(CultureInfo.CurrentCulture));
            base.WriteInformation(ir);
            base.WriteProgressCommand(activityId, 0.0, recordType, currentOperation, parentActivityId);
        }

        #endregion Public Methods
    }
}
