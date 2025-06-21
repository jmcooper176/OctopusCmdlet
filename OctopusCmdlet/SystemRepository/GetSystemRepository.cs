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
using Octopus.Client;
using Octopus.Client.Model;

using OctopusCmdlet.Utility;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.SystemRepository
{
    /// <summary>
    /// Implements the <c> Get-SystemRepository </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SystemRepository", DefaultParameterSetName = "UsingRepository")]
    [OutputType(typeof(IOctopusSpaceRepository), typeof(IOctopusSpaceAsyncRepository), ParameterSetName = ["UsingRepository", "UsingAsyncRepository"])]
    public class GetSystemRepository : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSystemRepository" /> class.
        /// </summary>
        public GetSystemRepository()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating the <see cref="IOctopusAsyncRepository" /> to use <c> ForSystem() </c>.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingAsyncRepository", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public IOctopusAsyncRepository AsyncRepository { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="IOctopusRepository" /> to use <c> ForSystem() </c>.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingRepository", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public IOctopusRepository Repository { get; set; }

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating this <see cref="Cmdlet" /> name.
        /// </summary>
        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Get the <see cref="IOctopusSystemAsyncRepository" />.
        /// </summary>
        /// <param name="repository">
        /// Specifies the <see cref="IOctopusAsyncRepository" /> to use.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOctopusSystemAsyncRepository" />
        /// </returns>
        public virtual IOctopusSystemAsyncRepository GetSystemAsyncRepositoryCommand(IOctopusAsyncRepository repository)
        {
            return repository.ForSystem();
        }

        /// <summary>
        /// Get the <see cref="IOctopusSystemRepository" />.
        /// </summary>
        /// <param name="repository">
        /// Specifies the <see cref="IOctopusRepository" /> to use.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOctopusSystemRepository" />
        /// </returns>
        public virtual IOctopusSystemRepository GetSystemRepositoryCommand(IOctopusRepository repository)
        {
            return repository.ForSystem();
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

            if (ParameterSetName == "UsingAsyncRepository")
            {
                WriteObject(GetSystemAsyncRepositoryCommand(AsyncRepository));
            }
            else
            {
                WriteObject(GetSystemRepositoryCommand(Repository));
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
