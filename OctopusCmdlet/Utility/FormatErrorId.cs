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

using Octopus.Client.Model.Forms;

using System.Diagnostics;
using System.Management.Automation;
using System.Runtime.CompilerServices;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the <c> Format-ErrorId </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsCommon.Format, "ErrorId", DefaultParameterSetName = "UsingException")]
    [CmdletBinding]
    [OutputType(typeof(string))]
    public class FormatErrorId : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="FormatErrorId" /> class.
        /// </summary>
        public FormatErrorId()
        {
            ActivityId = Environment.ProcessId;
            Caller = null;
            CmdletName = MyInvocation.MyCommand.Name;
            Count = 0;
            LineNumber = 0;
        }

        #endregion Public Constructors

        #region Public Properties

        [ValidateRange(ValidateRangeKind.Positive)]
        public int ActivityId { get; set; }

        [AllowNull()]
        public string? Caller { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingErrorCategory", ValueFromPipelineByPropertyName = true)]
        [ValidateSet(
            "AuthenticationError",
            "CloseError",
            "ConnectionError",
            "DeadlockDetected",
            "DeviceError",
            "FromStdErr",
            "InvalidArgument",
            "InvalidData",
            "InvalidOperation",
            "InvalidResult",
            "InvalidType",
            "LimitsExceeded",
            "MetadataError",
            "NotEnabled",
            "NotImplemented",
            "NotInstalled",
            "NotSpecified",
            "ObjectNotFound",
            "OpenError",
            "OperationStopped",
            "OperationTimeout",
            "ParseError",
            "PermissionDenied",
            "ProtocolError",
            "QuotaExceeded",
            "ReadError",
            "ResourceBusy",
            "ResourceExists",
            "ResourceUnavailable",
            "SecurityError",
            "SyntaxError",
            "WriteError",
            IgnoreCase = true)]
        public ErrorCategory[] Category { get; set; } = [ErrorCategory.NotSpecified];

        [Parameter(Mandatory = true, ParameterSetName = "UsingErrorRecord", ValueFromPipelineByPropertyName = true)]
        public ErrorRecord[] ErrorRecord { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingException", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Exception[] Exception { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingExceptionType", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public Type[] ExceptionType { get; set; }

        [ValidateRange(ValidateRangeKind.NonNegative)]
        public int LineNumber { get; set; }

        #endregion Public Properties

        #region Public Methods

        /// <summary>
        /// </summary>
        /// <param name="exception">
        /// </param>
        /// <param name="memberName">
        /// </param>
        /// <param name="lineNumber">
        /// </param>
        /// <returns>
        /// </returns>
        public virtual string FormatErrorIdCommand(Exception exception, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return FormatErrorIdCommand(exception.GetType(), memberName, lineNumber);
        }

        public virtual string FormatErrorIdCommand(Type exceptionType, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return $"{memberName}-{exceptionType.Name}-{lineNumber}";
        }

        /// <summary>
        /// </summary>
        /// <param name="errorRecord">
        /// </param>
        /// <param name="memberName">
        /// </param>
        /// <param name="lineNumber">
        /// </param>
        /// <returns>
        /// </returns>
        public virtual string FormatErrorIdCommand(ErrorRecord errorRecord, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return FormatErrorIdCommand(errorRecord.Exception, memberName, lineNumber);
        }

        /// <summary>
        /// </summary>
        /// <param name="errorCategory">
        /// </param>
        /// <param name="memberName">
        /// </param>
        /// <param name="lineNumber">
        /// </param>
        /// <returns>
        /// </returns>
        public virtual string FormatErrorIdCommand(ErrorCategory errorCategory, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            ResolveException exception = new();

            if (errorCategory == ErrorCategory.NotSpecified)
            {
                Utility.WriteWarning notSpecified = new();
                notSpecified.WriteWarning($"{CmdletName} : Not best practice to specify {errorCategory}");
            }

            return FormatErrorIdCommand(exception.ResolveExceptionCommand(errorCategory), memberName, lineNumber);
        }

        #endregion Public Methods

        #region Internal Properties

        /// <summary>
        /// </summary>
        internal string CmdletName { get; }

        /// <summary>
        /// </summary>
        internal int Count { get; set; }

        #endregion Internal Properties

        #region Protected Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            DefaultProcessing.InitializeBeginProcessing(CmdletName, MyInvocation.BoundParameters, SessionState, Stopping);
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{CmdletName} is Stopping in 'ProcessRecord'");
                return;
            }

            switch (ParameterSetName)
            {
                case "UsingErrorCategory":
                    foreach (var item in Category)
                    {
                        Utility.WriteProgress forErrorCategory = new();
                        ProgressRecord pr = new(ActivityId, CmdletName, $"Processing ErrorCategory {Count++}");
                        forErrorCategory.WriteProgress(pr);
                        WriteObject(FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;

                case "UsingErrorRecord":
                    foreach (var item in ErrorRecord)
                    {
                        Utility.WriteProgress forErrorRecord = new();
                        ProgressRecord pr = new(ActivityId, CmdletName, $"Processing ErrorRecord {Count++}");
                        forErrorRecord.WriteProgressCommand(pr);
                        WriteObject(FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;

                case "UsingExceptionType":
                    foreach (var item in ExceptionType)
                    {
                        Utility.WriteProgress forErrorRecord = new();
                        ProgressRecord pr = new(ActivityId, CmdletName, $"Processing ExceptionType {Count++}");
                        forErrorRecord.WriteProgressCommand(pr);
                        WriteObject(FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;

                default:
                    foreach (var item in Exception)
                    {
                        Utility.WriteProgress forException = new();
                        ProgressRecord pr = new(ActivityId, CmdletName, $"Processing Exception {Count++}");
                        forException.WriteProgressCommand(pr);
                        WriteObject(FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;
            }
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
