using System;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Verbose")]
    [OutputType(typeof(void))]
    public class WriteVerbose : PSCmdlet
    {
        #region Public Methods

        public void WriteVerboseCommand(string message)
        {
            base.WriteVerbose(message);
        }

        #endregion Public Methods
    }
}
