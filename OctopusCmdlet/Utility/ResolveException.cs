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
using System.Management.Automation.Runspaces;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceModel;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the Resolve-Exception <see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(
        VerbsDiagnostic.Resolve, "Exception",
        ConfirmImpact = ConfirmImpact.Low,
        DefaultParameterSetName = "UsingErrorCode",
        SupportsShouldProcess = true)]
    [OutputType(typeof(Exception))]
    public class ResolveException : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResolveException" /> class.
        /// </summary>
        public ResolveException()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating the <see cref="ErrorCategory" /> to resolve to an <see cref="Exception" />.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingErrorCategory", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
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
        public ErrorCategory Category { get; set; }

        /// <summary>
        /// Gets or sets a value indicting the error code (HResult) to resolve to an <see cref="Exception" />.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingErrorCode", ValueFromPipelineByPropertyName = true)]
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether "ConfirmPreference" should be overridden.
        /// </summary>
        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Gets or sets a value indicating an inner <see cref="Exception" /> to associate.
        /// </summary>
        public Exception? InnerException { get; set; }

        /// <summary>
        /// Gets or sets a value indicating a message to associate.
        /// </summary>
        public string? Message { get; set; }

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating the name of this <see cref="Cmdlet" />.
        /// </summary>
        internal string CmdletName { get; set; }

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Resolve to an <see cref="Exception" /> via <paramref name="errorCode" />.
        /// </summary>
        /// <param name="errorCode">
        /// Specifies the error code or HResult to resolve.
        /// </param>
        /// <param name="message">
        /// Specifies a message to associate.
        /// </param>
        /// <param name="innerException">
        /// Specifies an inner <see cref="Exception" /> to associate.
        /// </param>
        /// <returns>
        /// Returns an <see cref="Exception" /> if <paramref name="errorCode" /> is non-zero; otherwise, returns <see langref="null" />.
        /// </returns>
        public virtual Exception? ResolveExceptionCommand(int errorCode, string? message = null, Exception? innerException = null)
        {
            Exception? exception = Marshal.GetExceptionForHR(errorCode);

            return exception != null ? Activator.CreateInstance(exception.GetType(), message, innerException) as Exception : null;
        }

        /// <summary>
        /// Resolve to an <see cref="Exception" /> via <paramref name="category" />.
        /// </summary>
        /// <param name="category">
        /// Specifies the <see cref="ErrorCategory" /> to resolve.
        /// </param>
        /// <param name="message">
        /// Specifies a message to associate.
        /// </param>
        /// <param name="innerException">
        /// Specifies an inner <see cref="Exception" /> to associate.
        /// </param>
        /// <returns>
        /// Returns an <see cref="Exception" />.
        /// </returns>
        public virtual Exception ResolveExceptionCommand(ErrorCategory category, string? message = null, Exception? innerException = null)
        {
            return category switch
            {
                ErrorCategory.CloseError => new PSObjectDisposedException(message, innerException),
                ErrorCategory.ConnectionError => new CommunicationException(message, innerException),
                ErrorCategory.DeadlockDetected => new AbandonedMutexException(message, innerException),
                ErrorCategory.InvalidArgument => new PSArgumentException(message, innerException),
                ErrorCategory.InvalidData => new InvalidDataException(message, innerException),
                ErrorCategory.InvalidResult => new ApplicationFailedException(message, innerException),
                ErrorCategory.InvalidType => new PSInvalidCastException(message, innerException),
                ErrorCategory.LimitsExceeded => new PSArgumentOutOfRangeException(message, innerException),
                ErrorCategory.MetadataError => new MetadataException(message, innerException),
                ErrorCategory.NotEnabled => new PSNotSupportedException(message, innerException),
                ErrorCategory.NotImplemented => new PSNotImplementedException(message, innerException),
                ErrorCategory.NotInstalled => new CommandNotFoundException(message, innerException),
                ErrorCategory.FromStdErr or ErrorCategory.NotSpecified => new Exception(message, innerException),
                ErrorCategory.ObjectNotFound => new FileNotFoundException(message, innerException),
                ErrorCategory.OperationStopped => new PipelineStoppedException(message, innerException),
                ErrorCategory.OperationTimeout => new TimeoutException(message, innerException),
                ErrorCategory.ParserError => new ParseException(message, innerException),
                ErrorCategory.ProtocolError => new WebException(message, innerException),
                ErrorCategory.QuotaExceeded => new QuotaExceededException(message, innerException),
                ErrorCategory.ResourceBusy => new ServerTooBusyException(message, innerException),
                ErrorCategory.AuthenticationError or ErrorCategory.PermissionDenied or ErrorCategory.ResourceExists => new UnauthorizedAccessException(message, innerException),
                ErrorCategory.ResourceUnavailable => new ItemNotFoundException(message, innerException),
                ErrorCategory.SecurityError => new PSSecurityException(message, innerException),
                ErrorCategory.SyntaxError => new ParseException(message, innerException),
                ErrorCategory.DeviceError or ErrorCategory.OpenError or ErrorCategory.ReadError or ErrorCategory.WriteError => new IOException(message, innerException),
                _ => new PSInvalidOperationException(message, innerException),
            };
        }

        #endregion Public Methods

        #region Protected Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            BoundParameterDictionary bp = [.. MyInvocation.BoundParameters];

            if (Stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{CmdletName} is Stopping in 'BeginProcessing'");
                return;
            }
            else if (Force.IsPresent && !bp.HasParameter("Confirm"))
            {
                SessionState.PSVariable.Set("ConfirmPreference", ConfirmImpact.None);
            }
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
            else if (ParameterSetName.Equals("UsingErrorCode", StringComparison.OrdinalIgnoreCase))
            {
                if (ShouldProcess($"0x{ErrorCode:X8}", CmdletName))
                {
                    WriteObject(ResolveExceptionCommand(ErrorCode, Message, InnerException));
                }
            }
            else
            {
                if (ShouldProcess(Category.ToString(), CmdletName))
                {
                    WriteObject(ResolveExceptionCommand(Category, Message, InnerException));
                }
            }
        }

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
