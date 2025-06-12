// Ignore Spelling: Cmdlet

using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Information")]
    [OutputType(typeof(void))]
    public class WriteInformation : PSCmdlet
    {
        #region Public Methods

        public void WriteInformationCommand(
            object messageData,
            [CallerFilePath] string? source = null,
            string? computer = null,
            int managedThreadId = 0,
            int nativeThreadId = 0,
            int processId = 0,
            DateTime? timeGenerated = null,
            string? user = null)
        {
            InformationRecord ir = new(messageData, source)
            {
                Computer = computer ?? Environment.MachineName,
                ProcessId = (uint)(processId > 0 ? processId : Environment.ProcessId),
                ManagedThreadId = (uint)(managedThreadId > 0 ? managedThreadId : Thread.CurrentThread.ManagedThreadId),
                NativeThreadId = (uint)(nativeThreadId > 0 ? nativeThreadId : Environment.CurrentManagedThreadId),
                TimeGenerated = timeGenerated ?? DateTime.UtcNow,
                User = user ?? (OperatingSystem.IsWindows() ? WindowsIdentity.GetCurrent().Name : Environment.GetEnvironmentVariable("USER"))
            };

            base.WriteInformation(ir);
        }

        #endregion Public Methods
    }
}
