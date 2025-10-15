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

using System.Diagnostics;
using System.Management.Automation;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace OctopusCmdlet.Utility.InformationRecord
{
    /// <summary>
    /// Implements the <c> New-InformationRecord </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsCommon.New, "InformationRecord", ConfirmImpact = ConfirmImpact.Low, SupportsShouldProcess = true)]
    [OutputType(typeof(System.Management.Automation.InformationRecord))]
    public class NewInformationRecord : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewInformationRecord" /> class.
        /// </summary>
        public NewInformationRecord()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Public Methods

        #region Public Properties

        /// <summary>
        /// Gets or sets a value specifying whether to override <c> CommandPreference </c> by setting it to <see cref="ConfirmImpact.None" />.
        /// </summary>
        public SwitchParameter Force { get; set; }

        #endregion Public Properties

        #region Public Methods

        public virtual System.Management.Automation.InformationRecord NewInformationRecordCommand(
                            object messageData,
            [CallerFilePath] string? source = null)
        {
            var managedThreadId = Convert.ToUInt32(Environment.CurrentManagedThreadId);

            return new System.Management.Automation.InformationRecord(messageData, source)
            {
                Computer = Dns.GetHostName() ?? Environment.MachineName,
                ManagedThreadId = managedThreadId,
                NativeThreadId = GetNativeThreadId(managedThreadId),
                ProcessId = Convert.ToUInt32(Environment.ProcessId),
                TimeGenerated = DateTime.UtcNow,
                User = OperatingSystem.IsWindows() ? WindowsIdentity.GetCurrent(ifImpersonating: false)?.Name : Environment.UserName
            };
        }

        public virtual System.Management.Automation.InformationRecord NewInformationRecordCommand(
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

        #endregion Public Methods

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

            DefaultProcessing.InitializeBeginProcessing(CmdletName, MyInvocation.BoundParameters, SessionState, Stopping, Force.IsPresent);
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

        private static uint GetNativeThreadId(uint managedThreadId)
        {
            return Process.GetCurrentProcess().Threads.Cast<ProcessThread>().Where(t => t.Id == managedThreadId).Select(t => Convert.ToUInt32(t.Id)).FirstOrDefault();
        }

        #endregion Protected Methods
    }
}
