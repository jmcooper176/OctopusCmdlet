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

using OctopusCmdlet.Utility.ErrorRecord;
using OctopusCmdlet.Utility.Message;

using System.Management.Automation;

namespace OctopusCmdlet.Space
{
    /// <summary>
    /// Implements the <c> Get-SpaceRepository </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsCommon.Get, "SpaceRepository", DefaultParameterSetName = "UsingRepository")]
    [OutputType(typeof(IOctopusSpaceRepository), typeof(IOctopusSpaceAsyncRepository), ParameterSetName = ["UsingRepository", "UsingAsyncRepository"])]
    public class GetSpaceRepository : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GetSpaceRepository" /> class.
        /// </summary>
        public GetSpaceRepository()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating the <see cref="IOctopusAsyncRepository" /> to use ForSpace( <see cref="SpaceResource" />).
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingAsyncRepository", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public IOctopusAsyncRepository AsyncRepository { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="IOctopusRepository" /> to use <c> ForSpace( <see cref="SpaceResource" />) </c>.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "UsingRepository", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public IOctopusRepository Repository { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the <see cref="SpaceResource" /> to pass to <c> ForSpace( <see cref="SpaceResource" />) </c>.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNull]
        public SpaceResource Space { get; set; }

        #endregion Public Properties

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating this <see cref="Cmdlet" /> name.
        /// </summary>
        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Get the <see cref="IOctopusSpaceAsyncRepository" /> for <see cref="SpaceResource" /><see cref="Space" />.
        /// </summary>
        /// <param name="repository">
        /// Specifies the <see cref="IOctopusAsyncRepository" /> to use.
        /// </param>
        /// <param name="space">
        /// Specifies the <see cref="SpaceResource" /> to use.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOctopusSpaceAsyncRepository" />
        /// </returns>
        public virtual IOctopusSpaceAsyncRepository GetSpaceAsyncRepositoryCommand(IOctopusAsyncRepository repository, SpaceResource space)
        {
            return repository.ForSpace(space);
        }

        /// <summary>
        /// Get the <see cref="IOctopusSpaceRepository" /> for <see cref="SpaceResource" /><see cref="Space" />.
        /// </summary>
        /// <param name="repository">
        /// Specifies the <see cref="IOctopusRepository" /> to use.
        /// </param>
        /// <param name="space">
        /// Specifies the <see cref="SpaceResource" /> to use.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOctopusSpaceRepository" />
        /// </returns>
        public virtual IOctopusSpaceRepository GetSpaceRepositoryCommand(IOctopusRepository repository, SpaceResource space)
        {
            return repository.ForSpace(space);
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
                WriteObject(GetSpaceAsyncRepositoryCommand(AsyncRepository, Space));
            }
            else
            {
                WriteObject(GetSpaceRepositoryCommand(Repository, Space));
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
