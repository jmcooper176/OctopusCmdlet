using System;
using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Warning")]
    [OutputType(typeof(void))]
    public class WriteWarning : PSCmdlet
    {
        #region Public Methods

        public void WriteWarningCommand(string message)
        {
            base.WriteWarning(message);
        }

        public void WriteWarningCommand(string format, params object?[] arguments)
        {
            WriteWarningCommand(null, format, arguments);
        }

        public void WriteWarningCommand(IFormatProvider? provider, string format, params object?[] arguments)
        {
            WriteWarningCommand(string.Format(provider ?? CultureInfo.InvariantCulture, format, arguments));
        }

        #endregion Public Methods
    }
}
