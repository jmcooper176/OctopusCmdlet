// Ignore Spelling: Cmdlet

using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Debug")]
    [OutputType(typeof(void))]
    public class WriteDebug : PSCmdlet
    {
        #region Public Methods

        public void WriteDebugCommand(string message)
        {
            base.WriteDebug(message);
        }

        public void WriteDebugCommand(string message, ErrorCategory category)
        {
            WriteDebugCommand("{0} : {1}", message, category);
        }

        public void WriteDebugCommand(string format, params object?[] arguments)
        {
            WriteDebugCommand(null, format, arguments);
        }

        public void WriteDebugCommand(IFormatProvider? provider, string format, params object?[] arguments)
        {
            WriteDebugCommand(string.Format(provider ?? CultureInfo.CurrentCulture, format, arguments));
        }

        public void WriteDebugCommand(object? value)
        {
            WriteDebugCommand("{0}", value);
        }

        public void WriteDebugCommand(object? value, ErrorCategory category)
        {
            WriteDebugCommand("{0} : {1}", value, category);
        }

        public void WriteDebugCommand(bool condition, object? value, ErrorCategory category)
        {
            if (condition)
            {
                WriteDebugCommand(value, category);
            }
        }

        public void WriteDebugCommand(bool condition, object? value)
        {
            if (condition)
            {
                WriteDebugCommand(value);
            }
        }

        public void WriteDebugCommand(bool condition, string message)
        {
            if (condition)
            {
                WriteDebugCommand(message);
            }
        }

        public void WriteDebugCommand(bool condition, string message, ErrorCategory category)
        {
            if (condition)
            {
                WriteDebugCommand(message, category);
            }
        }

        #endregion Public Methods
    }
}
