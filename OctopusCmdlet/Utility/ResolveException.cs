using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

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
        /// Returns an <see cref="Exception" /> if <paramref name="errorCode" /> is non-zero; otherwise, returns null.
        /// </returns>
        public static Exception? ResolveExceptionCommand(int errorCode, string? message = null, Exception? innerException = null)
        {
            Type? exceptionType = Marshal.GetExceptionForHR(errorCode)?.GetType();

            return exceptionType != null ? Activator.CreateInstance(exceptionType, message, innerException) as Exception : null;
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
        public static Exception ResolveExceptionCommand(ErrorCategory category, string? message = null, Exception? innerException = null)
        {
            switch (category)
            {
                case ErrorCategory.CloseError:
                    return new PSObjectDisposedException(message, innerException);

                case ErrorCategory.ConnectionError:
                    return new CommunicationException(message, innerException);

                case ErrorCategory.DeadlockDetected:
                    return new AbandonedMutexException(message, innerException);

                case ErrorCategory.InvalidArgument:
                    return new PSArgumentException(message, innerException);

                case ErrorCategory.InvalidData:
                    return new InvalidDataException(message, innerException);

                case ErrorCategory.InvalidResult:
                    return new ApplicationFailedException(message, innerException);

                case ErrorCategory.InvalidType:
                    return new PSInvalidCastException(message, innerException);

                case ErrorCategory.LimitsExceeded:
                    return new PSArgumentOutOfRangeException(message, innerException);

                case ErrorCategory.MetadataError:
                    return new MetadataException(message, innerException);

                case ErrorCategory.NotEnabled:
                    return new PSNotSupportedException(message, innerException);

                case ErrorCategory.NotImplemented:
                    return new PSNotImplementedException(message, innerException);

                case ErrorCategory.NotInstalled:
                    return new CommandNotFoundException(message, innerException);

                case ErrorCategory.FromStdErr:
                case ErrorCategory.NotSpecified:
                    return new Exception(message, innerException);

                case ErrorCategory.ObjectNotFound:
                    return new FileNotFoundException(message, innerException);

                case ErrorCategory.OperationStopped:
                    return new PipelineStoppedException(message, innerException);

                case ErrorCategory.OperationTimeout:
                    return new TimeoutException(message, innerException);

                case ErrorCategory.ParserError:
                    return new ParseException(message, innerException);

                case ErrorCategory.ProtocolError:
                    return new WebException(message, innerException);

                case ErrorCategory.QuotaExceeded:
                    return new QuotaExceededException(message, innerException);

                case ErrorCategory.ResourceBusy:
                    return new ServerTooBusyException(message, innerException);

                case ErrorCategory.AuthenticationError:
                case ErrorCategory.PermissionDenied:
                case ErrorCategory.ResourceExists:
                    return new UnauthorizedAccessException(message, innerException);

                case ErrorCategory.ResourceUnavailable:
                    return new ItemNotFoundException(message, innerException);

                case ErrorCategory.SecurityError:
                    return new PSSecurityException(message, innerException);

                case ErrorCategory.SyntaxError:
                    return new ParseException(message, innerException);

                case ErrorCategory.DeviceError:
                case ErrorCategory.OpenError:
                case ErrorCategory.ReadError:
                case ErrorCategory.WriteError:
                    return new IOException(message, innerException);

                default:
                    return new PSInvalidOperationException(message, innerException);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        /// <inheritdoc />
        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            BoundParameter bp = new(MyInvocation.BoundParameters);
            var sessionState = InitialSessionState.CreateDefault();

            if (Stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{CmdletName} is Stopping in 'BeginProcessing'");
                return;
            }
            else if (Force.IsPresent && !bp.HasParameter("Confirm"))
            {
                var description = sessionState.Variables.Where(v => v.Name.Equals("ConfirmPreference", StringComparison.OrdinalIgnoreCase)).Select(e => e.Description).FirstOrDefault();
                sessionState.Variables.Remove("ConfirmPreference", typeof(ConfirmImpact));
                sessionState.Variables.Add(new SessionStateVariableEntry("ConfirmPreference", ConfirmImpact.None, description ?? string.Empty));
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
                    WriteObject(ResolveException.ResolveExceptionCommand(ErrorCode, Message, InnerException));
                }
            }
            else
            {
                if (ShouldProcess(Category.ToString(), CmdletName))
                {
                    WriteObject(ResolveException.ResolveExceptionCommand(Category, Message, InnerException));
                }
            }
        }

        /// <inheritdoc />
        /// <exception cref="PipelineStoppedException">
        /// Always throws.
        /// </exception>
        protected override void StopProcessing()
        {
            base.StopProcessing();

            ErrorRecord er = NewErrorRecord.NewErrorRecordCommand(
                new PipelineStoppedException($"{CmdletName} : PipelineStoppedException : Pipeline stopping because 'StopProcessing' called"),
                FormatErrorId.FormatErrorIdCommand(new PipelineStoppedException()),
                ErrorCategory.OperationStopped,
                this);

            WriteFatal pipelineStopped = new();
            pipelineStopped.WriteFatalCommand(er);
        }

        #endregion Protected Methods
    }
}
