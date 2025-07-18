﻿/* ****************************************************************************
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
using OctopusCmdlet.Trigger;

using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Error")]
    [OutputType(typeof(void))]
    public class WriteError : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteError" /> class.
        /// </summary>
        public WriteError()
        {
            CmdletName = MyInvocation.MyCommand.Name;
            NewErrorRecord = new();
        }

        #endregion Public Constructors

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating this <see cref="Cmdlet" /> name.
        /// </summary>
        internal string CmdletName { get; }

        /// <summary>
        /// Gets a value indicating the <see cref="NewErrorRecord" /> instance to use.
        /// </summary>
        internal NewErrorRecord NewErrorRecord { get; }

        #endregion Internal Properties

        #region Public Methods

        public virtual void WriteErrorCommand(
            ErrorRecord errorRecord,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null)
        {
            var er = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);
            base.WriteError(er);
        }

        public virtual void WriteErrorCommand<TException>(
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

        public virtual void WriteErrorCommand(
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
