// Ignore Spelling: Cmdlet

using System.Management.Automation;
using System.Runtime.CompilerServices;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommon.Format, "ErrorId", DefaultParameterSetName = "UsingException")]
    [OutputType(typeof(string))]
    public class FormatErrorId : PSCmdlet
    {
        #region Public Constructors

        public FormatErrorId()
        {
            Caller = null;
            CmdletName = MyInvocation.MyCommand.Name;
            Count = 0;
            LineNumber = 0;
        }

        #endregion Public Constructors

        #region Public Properties

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
        public static string FormatErrorIdCommand(Exception exception, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return $"{memberName}-{exception.GetType().Name}-{lineNumber}";
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
        public static string FormatErrorIdCommand(ErrorRecord errorRecord, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return FormatErrorId.FormatErrorIdCommand(errorRecord.Exception, memberName, lineNumber);
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
        public static string FormatErrorIdCommand(ErrorCategory errorCategory, [CallerMemberName] string? memberName = null, [CallerLineNumber] int lineNumber = 0)
        {
            return $"{memberName}-{errorCategory}-{lineNumber}";
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
                        forErrorCategory.WriteProgressCommand(1, $"Processing ErrorCategory {Count++}");
                        WriteObject(FormatErrorId.FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;

                case "UsingErrorRecord":
                    foreach (var item in ErrorRecord)
                    {
                        Utility.WriteProgress forErrorRecord = new();
                        forErrorRecord.WriteProgressCommand(1, $"Processing ErrorRecord {Count++}");
                        WriteObject(FormatErrorId.FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;

                default:
                    foreach (var item in Exception)
                    {
                        Utility.WriteProgress forException = new();
                        forException.WriteProgressCommand(1, $"Processing Exception {Count++}");
                        WriteObject(FormatErrorId.FormatErrorIdCommand(item, Caller, LineNumber));
                    }

                    break;
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
