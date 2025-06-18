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

// Ignore Spelling: cmdlet Ignore Spelling: Cmdlet

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
