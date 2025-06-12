// Ignore Spelling: Cmdlet

using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Debug")]
    [OutputType(typeof(void))]
    public class WriteDebug : PSCmdlet
    {
        #region Public Constructors

        public WriteDebug()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Public Properties

        [AllowNull]
        [AllowEmptyCollection]
        public object?[]? Arguments { get; set; }

        public ErrorCategory Category { get; set; }

        public bool Condition { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingFormat", ValueFromPipelineByPropertyName = true)]
        public string Format { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingMessage", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public string Message { get; set; }

        public Func<bool> Predicate { get; set; }

        [AllowNull]
        public IFormatProvider? Provider { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "UsingValue", ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public object? Value { get; set; }

        #endregion Public Properties

        #region Internal Properties

        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Public Methods

        public static void WriteDebugCommand(string message, ErrorCategory category)
        {
            Utility.WriteDebug.WriteDebugCommand(null, "{0} : {1}", message, category);
        }

        public static void WriteDebugCommand(string format, params object?[] arguments)
        {
            Utility.WriteDebug.WriteDebugCommand(null, format, arguments);
        }

        public static void WriteDebugCommand(IFormatProvider? provider, string format, params object?[] arguments)
        {
            Utility.WriteDebug.WriteDebugCommand(string.Format(provider ?? CultureInfo.InvariantCulture, format, arguments));
        }

        public static void WriteDebugCommand(object? value)
        {
            Utility.WriteDebug.WriteDebugCommand(null, "{0}", value);
        }

        public static void WriteDebugCommand(object? value, ErrorCategory category)
        {
            Utility.WriteDebug.WriteDebugCommand(null, "{0} : {1}", value, category);
        }

        public static void WriteDebugCommand(bool condition, object? value, ErrorCategory category)
        {
            if (condition)
            {
                Utility.WriteDebug.WriteDebugCommand(value, category);
            }
        }

        public static void WriteDebugCommand(bool condition, object? value)
        {
            if (condition)
            {
                Utility.WriteDebug.WriteDebugCommand(value);
            }
        }

        public static void WriteDebugCommand(bool condition, string message, ErrorCategory category)
        {
            if (condition)
            {
                Utility.WriteDebug.WriteDebugCommand(message, category);
            }
        }

        public void WriteDebugCommand(string message)
        {
            base.WriteDebug(message);
        }

        public void WriteDebugCommand(bool condition, string message)
        {
            if (condition)
            {
                this.WriteDebugCommand(message);
            }
        }

        #endregion Public Methods

        #region Protected Methods

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
        }

        protected override void StopProcessing()
        {
            base.StopProcessing();
        }

        #endregion Protected Methods
    }
}
