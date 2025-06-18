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

// Ignore Spelling: cmdlet Ignore Spelling: Cmdlet

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
