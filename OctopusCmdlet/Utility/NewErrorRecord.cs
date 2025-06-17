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
/* *************************************************************************************
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
*************************************************************************************** */

// Ignore Spelling: cmdlet

using System.Management.Automation;
using System.Management.Automation.Language;
using System.Management.Automation.Runspaces;
using System.Resources;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Class implementing the 'New-ErrorRecord' PowerShell cmdlet.
    /// </summary>
    [Cmdlet(
        VerbsCommon.New,
        "ErrorRecord",
        ConfirmImpact = ConfirmImpact.Low,
        DefaultParameterSetName = "UsingException",
        SupportsShouldProcess = true)]
    [OutputType(typeof(ErrorRecord))]
    public class NewErrorRecord : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="NewErrorRecord" /> class.
        /// </summary>
        public NewErrorRecord()
        {
            CmdletName = MyInvocation.MyCommand.Name;
            Count = 0;
        }

        #endregion Public Constructors

        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating the <see cref="ErrorCategory" /> to assign to the <see cref="ErrorRecord" />.
        /// </summary>
        /// <remarks>
        /// You can also pipe an <see cref="ErrorCategory" /> to 'New-ErrorRecord'.
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingException", ValueFromPipelineByPropertyName = true)]
        [Parameter(Mandatory = true, ParameterSetName = "UsingMessage", ValueFromPipelineByPropertyName = true)]
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
        public ErrorCategory Category { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the action that caused the error represented by the <see cref="ErrorRecord" />.
        /// </summary>
        [AllowNull]
        public string? CategoryActivity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating how or why the 'CategoryActivity' caused the error represented by the <see cref="ErrorRecord" />.
        /// </summary>
        [AllowNull]
        public string? CategoryReason { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the name of the 'TargetObject' that was being processed when the error occurred
        /// represented by the <see cref="ErrorRecord" />.
        /// </summary>
        [AllowNull]
        public string? CategoryTargetName { get; set; }

        /// <summary>
        /// Gets or sets a value specifying type full name of the 'TargetObject' that was being processed when the error occurred
        /// represented by the <see cref="ErrorRecord" />.
        /// </summary>
        [AllowNull]
        public string? CategoryTargetType { get; set; }

        /// <summary>
        /// Gets or sets a value specifying an ID string to identify the error represented by the <see cref="ErrorRecord" />.
        /// </summary>
        /// <remarks>
        /// The string should be unique to the error. You can also pipe a string to 'New-ErrorRecord'.
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingException", ValueFromPipelineByPropertyName = true)]
        [Parameter(Mandatory = true, ParameterSetName = "UsingMessage", ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string ErrorId { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the <see cref="ErrorRecord" /> that represents the error.
        /// </summary>
        /// <remarks>
        /// Update the properties of the <see cref="ErrorRecord" /> to describe the error. You can also pipe an
        /// <see cref="ErrorRecord" /> to 'New-ErrorRecord'
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingErrorRecord", ValueFromPipelineByPropertyName = true)]
        public ErrorRecord ErrorRecord { get; set; }

        /// <summary>
        /// Gets or sets a value specifying an <see cref="Exception" /> object that represents the error.
        /// </summary>
        /// <remarks>
        /// You can also pipe an <see cref="Exception" /> to 'New-ErrorRecord'.
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingException", ValueFromPipelineByPropertyName = true)]
        public Exception Exception { get; set; }

        public SwitchParameter Force { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the message text of the error.
        /// </summary>
        /// <remarks>
        /// You can also pipe message strings to 'New-ErrorRecord'.
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingMessage", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        [ValidateNotNullOrEmpty]
        public string[] Message { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the action the user should take to resolve or prevent the error.
        /// </summary>
        [AllowNull]
        public string? RecommendedAction { get; set; }

        /// <summary>
        /// Gets or sets a value specifying the object that was being processed when the error occurred.
        /// </summary>
        /// <remarks>
        /// Pass the object, a variable containing the object, or a command that gets the object. You can also pipe an object to 'New-ErrorRecord'.
        /// </remarks>
        [Parameter(Mandatory = true, ParameterSetName = "UsingException", ValueFromPipelineByPropertyName = true)]
        [Parameter(Mandatory = true, ParameterSetName = "UsingMessage", ValueFromPipelineByPropertyName = true)]
        [AllowNull]
        public object? TargetObject { get; set; }

        /// <summary>
        /// Gets a value indicating the 'Name' of this cmdlet.
        /// </summary>
        internal string CmdletName { get; }

        /// <summary>
        /// Gets or sets a value indicating the 'Message' index currently being processed.
        /// </summary>
        internal int Count { get; set; }

        internal PowerShell? Shell { get; set; }

        #endregion Public Properties

        #region Protected Methods

        protected override void BeginProcessing()
        {
            base.BeginProcessing();

            InitialSessionState sessionState = InitialSessionState.CreateDefault();

            if (Force.IsPresent && !MyInvocation.BoundParameters.ContainsKey("Confirm"))
            {
                sessionState.Variables.Remove("ConfirmPreference", typeof(ConfirmImpact));
                sessionState.Variables.Add(new SessionStateVariableEntry("ConfirmPreference", ConfirmImpact.None, ""));
            }

            Shell = PowerShell.Create(sessionState);
        }

        /// <inheritdoc />
        protected override void EndProcessing()
        {
            base.EndProcessing();

            Dispose();
        }

        /// <inheritdoc />
        protected override void ProcessRecord()
        {
            base.ProcessRecord();

            if (Stopping)
            {
                StopProcessing();
            }
            else
            {
                ErrorRecord? er;

                switch (ParameterSetName)
                {
                    case "UsingErrorRecord":
                        if (ShouldProcess(ErrorRecord.ToString(), CmdletName))
                        {
                            er = UpdateErrorRecordCommand(
                                ErrorRecord,
                                RecommendedAction,
                                CategoryActivity,
                                CategoryReason,
                                CategoryTargetName,
                                CategoryTargetType);
                            WriteObject(er);
                        }

                        break;

                    case "UsingMessage":
                        foreach (var item in Message)
                        {
                            ProgressRecord pr = new(Environment.ProcessId, "Creating 'ErrorRecord'", $"Count {Count++} 'ErrorRecord'");
                            WriteProgress(pr);

                            if (ShouldProcess(item, CmdletName))
                            {
                                er = NewErrorRecordCommand<PSInvalidOperationException>(
                                item,
                                ErrorId,
                                Category == ErrorCategory.NotSpecified ? ErrorCategory.InvalidOperation : Category,
                                TargetObject,
                                RecommendedAction,
                                CategoryActivity,
                                CategoryReason,
                                CategoryTargetName,
                                CategoryTargetType);
                                WriteObject(er);
                            }
                        }

                        break;

                    default:
                        if (ShouldProcess(Exception.ToString(), CmdletName))
                        {
                            er = NewErrorRecordCommand(
                                Exception,
                                ErrorId,
                                Category,
                                TargetObject,
                                RecommendedAction,
                                CategoryActivity,
                                CategoryReason,
                                CategoryTargetName,
                                CategoryTargetType);
                            WriteObject(er);
                        }

                        break;
                }
            }
        }

        /// <inheritdoc />
        protected override void StopProcessing()
        {
            base.StopProcessing();

            Dispose();

            var er = NewErrorRecord.NewErrorRecordCommand<PipelineStoppedException>(
                $"{CmdletName} : PipelineStoppedException : 'StopProcessing' called",
                FormatErrorId.FormatErrorIdCommand(new PipelineStoppedException()),
                ErrorCategory.OperationStopped,
                this);
            WriteFatal operationStopped = new();
            operationStopped.WriteFatalCommand(er);
        }

        #endregion Protected Methods

        #region Public Methods

        /// <summary>
        /// Method implementing creating a new <see cref="ErrorRecord" /> in the case where parameter set name 'UsingMessage' is
        /// being processed.
        /// </summary>
        /// <typeparam name="TException">
        /// Specifies the <see cref="Exception" /> to create to hold the message.
        /// </typeparam>
        /// <param name="message">
        /// Specifies the message text to assign to <typeparamref name="TException" /> during creation.
        /// </param>
        /// <param name="errorId">
        /// Specifies the unique ID string to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="category">
        /// Specifies the <see cref="ErrorCategory" /> to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="targetObject">
        /// Specifies the object being processed when the error occurred.
        /// </param>
        /// <param name="recommendedAction">
        /// Specifies the action the user should take to resolve or prevent the error.
        /// </param>
        /// <param name="categoryActivity">
        /// Specifies the action that caused the error.
        /// </param>
        /// <param name="categoryReason">
        /// Specifies how or why the activity caused the error.
        /// </param>
        /// <param name="categoryTargetName">
        /// Specifies the name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="categoryTargetType">
        /// Specifies the type full name of the object that was being processed when the error occurred.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="ErrorRecord" /> on success; otherwise null.
        /// </returns>
        public static ErrorRecord NewErrorRecordCommand<TException>(
            string message,
            string errorId,
            ErrorCategory category,
            object? targetObject = null,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null
            ) where TException : Exception
        {
            var exception = (Exception?)Activator.CreateInstance(typeof(TException), message) ?? new InvalidOperationException(message);
            var er = NewErrorRecord.NewErrorRecordCommand(
                    exception,
                    errorId,
                    category,
                    targetObject,
                    recommendedAction,
                    categoryActivity,
                    categoryReason,
                    categoryTargetName,
                    categoryTargetType,
                    message);

            return er;
        }

        /// <summary>
        /// Method implementing creating a new <see cref="ErrorRecord" /> in the case where parameter set name 'UsingException' is
        /// being processed.
        /// </summary>
        /// <param name="exception">
        /// Specifies an <see cref="Exception" /> object that represents the error.
        /// </param>
        /// <param name="errorId">
        /// Specifies the unique ID string to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="category">
        /// Specifies the <see cref="ErrorCategory" /> to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="targetObject">
        /// Specifies the object being processed when the error occurred.
        /// </param>
        /// <param name="recommendedAction">
        /// Specifies the action the user should take to resolve or prevent the error.
        /// </param>
        /// <param name="categoryActivity">
        /// Specifies the action that caused the error.
        /// </param>
        /// <param name="categoryReason">
        /// Specifies how or why the activity caused the error.
        /// </param>
        /// <param name="categoryTargetName">
        /// Specifies the name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="categoryTargetType">
        /// Specifies the type full name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="message">
        /// Specifies the message to set in <see cref="ErrorDetails" />, or <see cref="Exception.Message" /> if
        /// <paramref name="message" /> is null.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="ErrorRecord" />.
        /// </returns>
        public static ErrorRecord NewErrorRecordCommand(
            Exception exception,
            string errorId,
            ErrorCategory category,
            object? targetObject = null,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null,
            string? message = null
            )
        {
            ErrorRecord errorRecord = new(exception, errorId, category, targetObject);

            var er = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            er.ErrorDetails = new(message ?? exception.Message);

            return er;
        }

        /// <summary>
        /// Method implementing creating a new <see cref="ErrorRecord" /> in the case where parameter set name 'UsingException' is
        /// being processed.
        /// </summary>
        /// <param name="exception">
        /// Specifies an <see cref="Exception" /> object that represents the error.
        /// </param>
        /// <param name="errorId">
        /// Specifies the unique ID string to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="category">
        /// Specifies the <see cref="ErrorCategory" /> to assign to the new <see cref="ErrorRecord" />.
        /// </param>
        /// <param name="targetObject">
        /// Specifies the object being processed when the error occurred.
        /// </param>
        /// <param name="recommendedAction">
        /// Specifies the action the user should take to resolve or prevent the error.
        /// </param>
        /// <param name="categoryActivity">
        /// Specifies the action that caused the error.
        /// </param>
        /// <param name="categoryReason">
        /// Specifies how or why the activity caused the error.
        /// </param>
        /// <param name="categoryTargetName">
        /// Specifies the name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="categoryTargetType">
        /// Specifies the type full name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="cmdlet">
        /// Specifies the <see cref="Cmdlet" /> for retrieving the localized message template.
        /// </param>
        /// <param name="resourceId">
        /// Specifies the name of the localized message template resource.
        /// </param>
        /// <param name="resourceArguments">
        /// Specifies zero or more arguments to instantiate the <paramref name="resourceId" /> message template.
        /// </param>
        /// <returns>
        /// Returns a new <see cref="ErrorRecord" />.
        /// </returns>
        public static ErrorRecord NewErrorRecordCommand(
            Exception exception,
            string errorId,
            ErrorCategory category,
            object? targetObject = null,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null,
            Cmdlet? cmdlet = null,
            string? resourceId = null,
            params object[] resourceArguments
            )
        {
            ErrorRecord errorRecord = new(exception, errorId, category, targetObject);

            var er = NewErrorRecord.UpdateErrorRecordCommand(
                errorRecord,
                recommendedAction,
                categoryActivity,
                categoryReason,
                categoryTargetName,
                categoryTargetType);

            if (cmdlet != null)
            {
                ResourceManager rm = new(cmdlet.GetType());
                er.ErrorDetails = new ErrorDetails(cmdlet, rm.BaseName, resourceId, resourceArguments);
            }

            return er;
        }

        /// <summary>
        /// Implements updating <see cref="ErrorRecord.CategoryInfo" /> and <see cref="ErrorRecord.ErrorDetails" /> for an <see cref="ErrorRecord" />.
        /// </summary>
        /// <param name="errorRecord">
        /// Specifies the <see cref="ErrorRecord" /> to update.
        /// </param>
        /// <param name="recommendedAction">
        /// Specifies the action the user should take to resolve or prevent the error.
        /// </param>
        /// <param name="categoryActivity">
        /// Specifies the action that caused the error.
        /// </param>
        /// <param name="categoryReason">
        /// Specifies how or why the activity caused the error.
        /// </param>
        /// <param name="categoryTargetName">
        /// Specifies the name of the object that was being processed when the error occurred.
        /// </param>
        /// <param name="categoryTargetType">
        /// Specifies the type full name of the object that was being processed when the error occurred.
        /// </param>
        /// <returns>
        /// <param name="displayScriptPosition"> Specifies the position for the invocation or error. </param> Returns an updated <paramref name="errorRecord" />.
        /// </returns>
        public static ErrorRecord UpdateErrorRecordCommand(
            ErrorRecord errorRecord,
            string? recommendedAction = null,
            string? categoryActivity = null,
            string? categoryReason = null,
            string? categoryTargetName = null,
            string? categoryTargetType = null,
            IScriptExtent? displayScriptPosition = null
            )
        {
            var category = errorRecord.CategoryInfo.Category.ToString();
            var activity = categoryActivity ?? "Processing pipeline record in 'ProcessRecord'";
            var reason = categoryReason ?? $"Unknown failure during '{activity}' caused '{category}' error";
            var action = recommendedAction ?? $"Resolve or prevent Reason '{reason}' during Activity '{activity}' causing '{category}' error";

            errorRecord.CategoryInfo.TargetName = categoryTargetName ?? "TargetObject";
            errorRecord.CategoryInfo.TargetType = categoryTargetType ?? errorRecord.TargetObject?.GetType().FullName ?? "System.Object";
            errorRecord.CategoryInfo.Activity = activity;
            errorRecord.CategoryInfo.Reason = reason;

            if (errorRecord.ErrorDetails != null)
            {
                errorRecord.ErrorDetails.RecommendedAction = action;
            }

            errorRecord.InvocationInfo.DisplayScriptPosition = displayScriptPosition ?? errorRecord.InvocationInfo.DisplayScriptPosition;

            return errorRecord;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs class-defined tasks associated with freeing, releasing, or resetting managed or unmanaged resources.
        /// </summary>
        /// <param name="disposing">
        /// If true, disposal of managed resources is carried out; otherwise, disposal of managed resources is not carried out.
        /// </param>
        /// <remarks>
        /// If <see cref="disposedValue" /> is false, set managed and unmanaged resources to null and set
        /// <see cref="disposedValue" /> to true.
        /// </remarks>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Shell?.Dispose();
                }

                Shell = null;
                disposedValue = true;
            }
        }

        #endregion Public Methods

        #region Private Fields

        private bool disposedValue;

        #endregion Private Fields
    }
}
