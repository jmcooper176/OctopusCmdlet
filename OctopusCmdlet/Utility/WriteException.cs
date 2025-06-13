using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Exception")]
    [OutputType(typeof(void))]
    public class WriteException : PSCmdlet
    {
    }
}
