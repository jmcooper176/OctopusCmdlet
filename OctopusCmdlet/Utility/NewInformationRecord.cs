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
using System.Diagnostics;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Management.Automation;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommon.New, "InformationRecord")]
    [OutputType(typeof(InformationRecord))]
    public class NewInformationRecord : PSCmdlet
    {
        #region Public Methods

        public virtual InformationRecord NewInformationRecordCommand(
            object messageData,
            [CallerFilePath] string? source = null)
        {
            var managedThreadId = Convert.ToUInt32(Environment.CurrentManagedThreadId);

            return new InformationRecord(messageData, source)
            {
                Computer = Dns.GetHostName() ?? Environment.MachineName,
                ManagedThreadId = managedThreadId,
                NativeThreadId = NewInformationRecord.GetNativeThreadId(managedThreadId),
                ProcessId = Convert.ToUInt32(Process.GetCurrentProcess().Id),
                TimeGenerated = DateTime.UtcNow,
                User = OperatingSystem.IsWindows() ? WindowsIdentity.GetCurrent(ifImpersonating: false)?.Name : Environment.UserName
            };
        }

        public virtual InformationRecord NewInformationRecordCommand(
            object messageData,
            string computer,
            uint managedThreadId,
            uint nativeThreadId,
            uint processId,
            DateTime timeGenerated,
            string user,
            [CallerFilePath] string? source = null)
        {
            var ir = NewInformationRecordCommand(messageData, source);
            ir.Computer = computer;
            ir.ManagedThreadId = ir.ManagedThreadId != managedThreadId ? managedThreadId : ir.ManagedThreadId;
            ir.NativeThreadId = ir.NativeThreadId != nativeThreadId ? nativeThreadId : ir.NativeThreadId;
            ir.ProcessId = ir.ProcessId != processId ? processId : ir.ProcessId;
            ir.TimeGenerated = ir.TimeGenerated != timeGenerated ? timeGenerated : ir.TimeGenerated;
            ir.User = ir.User != user ? user : ir.User;
            return ir;
        }

        #endregion Public Methods

        #region Private Methods

        private static uint GetNativeThreadId(uint managedThreadId)
        {
            return Process.GetCurrentProcess().Threads.Cast<ProcessThread>().Where(t => t.Id == managedThreadId).Select(t => Convert.ToUInt32(t.Id)).FirstOrDefault();
        }

        #endregion Private Methods

        #region Protected Methods

        /// <inheritdoc />
        /// <exception cref="PipelineStoppedException">
        /// Always throws when <see cref="StopProcessing" /> is called.
        /// </exception>
        protected override void StopProcessing()
        {
            base.StopProcessing();

            NewErrorRecord stopProcessingErr = new();
            FormatErrorId pipelineStoppedEx = new();

            var er = stopProcessingErr.NewErrorRecordCommand(
                new PipelineStoppedException($"{CmdletName} : PipelineStoppedException : Pipeline stopping because 'StopProcessing' called"),
                pipelineStoppedEx.FormatErrorIdCommand(typeof(PipelineStoppedException)),
                ErrorCategory.OperationStopped,
                this);
            WriteFatal operationStopped = new();
            operationStopped.WriteFatalCommand(er);
        }

        #endregion Protected Methods
    }
}
