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
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility
{
    public static partial class WriteLog
    {
        #region Public Methods

        /// <summary>
        /// Write <paramref name="message" /> to <paramref name="logger" /> at level <paramref name="level" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="message">
        /// Specifies the message text to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use.
        /// </param>
        [LoggerMessage(EventId = 1, EventName = "WriteLog", Message = "Logged {message}")]
        public static partial void GenericWriteLog(this ILogger logger, string message, LogLevel level);

        /// <summary>
        /// Write <paramref name="errorRecord" /> to <paramref name="logger" /> at level <see cref="LogLevel.Critical" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="errorRecord">
        /// Specifies the <see cref="ErrorRecord" /> to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Critical" />.
        /// </param>
        [LoggerMessage(EventId = 5, EventName = "WriteCriticalErrorRecordLog", Level = LogLevel.Critical, Message = "Terminating Error {errorRecord}", SkipEnabledCheck = true)]
        public static partial void WriteCriticalLog(this ILogger logger, ErrorRecord errorRecord, LogLevel level = LogLevel.Critical);

        /// <summary>
        /// Write <paramref name="message" /> to <paramref name="logger" /> at level <see cref="LogLevel.Debug" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="message">
        /// Specifies the message text to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Debug" />.
        /// </param>
        [LoggerMessage(EventId = 3, EventName = "WriteDebugLog", Level = LogLevel.Debug, Message = "Output {message}")]
        public static partial void WriteDebugLog(this ILogger logger, string message, LogLevel level = LogLevel.Debug);

        /// <summary>
        /// Write <paramref name="errorRecord" /> to <paramref name="logger" /> at level <see cref="LogLevel.Error" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="errorRecord">
        /// Specifies the <see cref="ErrorRecord" /> to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Error" />.
        /// </param>
        [LoggerMessage(EventId = 5, EventName = "WriteErrorRecordLog", Level = LogLevel.Error, Message = "Non-terminating Error {errorRecord}", SkipEnabledCheck = true)]
        public static partial void WriteErrorLog(this ILogger logger, ErrorRecord errorRecord, LogLevel level = LogLevel.Error);

        /// <summary>
        /// Write <paramref name="informationRecord" /> to <paramref name="logger" /> at level <see cref="LogLevel.Information" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="informationRecord">
        /// Specifies the <see cref="InformationRecord" /> to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Information" />.
        /// </param>
        [LoggerMessage(EventId = 4, EventName = "WriteInformationRecordLog", Level = LogLevel.Information, Message = "Output {informationRecord}")]
        public static partial void WriteInformationLog(this ILogger logger, InformationRecord informationRecord, LogLevel level = LogLevel.Information);

        /// <summary>
        /// Write <paramref name="messageData" /> and <paramref name="tags" /> to <paramref name="logger" /> at level <see cref="LogLevel.Information" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="messageData">
        /// Specifies the <see cref="object" /> instance <see cref="object.ToString" /> to log.
        /// </param>
        /// <param name="tags">
        /// Specifies the array of tags to log. Maybe <see langref="null" />.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Information" />.
        /// </param>
        [LoggerMessage(EventId = 41, EventName = "WriteInformationMessageDataTagsLog", Level = LogLevel.Information, Message = "Output {messageData} with Tags {tags}")]
        public static partial void WriteInformationLog(this ILogger logger, object messageData, string[]? tags = null, LogLevel level = LogLevel.Information);

        /// <summary>
        /// Write <paramref name="messageData" /> at level <see cref="LogLevel.Information" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="messageData">
        /// Specifies the <see cref="object" /> instance <see cref="object.ToString" /> to log.
        /// </param>
        /// <param name="source">
        /// Specifies the source location for the <paramref name="messageData" />
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Information" />.
        /// </param>
        [LoggerMessage(EventId = 42, EventName = "WriteInformationMessageDataSourceLog", Level = LogLevel.Information, Message = "Output {messageData} from {source}")]
        public static partial void WriteInformationLog(
            this ILogger logger,
            object messageData,
            string source,
            LogLevel level = LogLevel.Information);

        /// <summary>
        /// Write <paramref name="messageData" /> at level <see cref="LogLevel.Information" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="messageData">
        /// Specifies the <see cref="object" /> instance <see cref="object.ToString" /> to log.
        /// </param>
        /// <param name="source">
        /// Specifies the source location for the <paramref name="messageData" />
        /// </param>
        /// <param name="computer">
        /// Specifies the machine name or host name generating <paramref name="messageData" />.
        /// </param>
        /// <param name="managedThreadId">
        /// Specifies the managed <see cref="Thread" /> Id of <paramref name="processId" /> generating <paramref name="messageData" />.
        /// </param>
        /// <param name="nativeThreadId">
        /// Specifies the native <see cref="Thread" /> Id of <paramref name="processId" /> generating <paramref name="messageData" />.
        /// </param>
        /// <param name="processId">
        /// Specifies the <see cref="Process.Id" /> generating <paramref name="messageData" />.
        /// </param>
        /// <param name="timeGenerated">
        /// Specifies the <c> UTC </c><see cref="DateTime" /><paramref name="messageData" /> was generated.
        /// </param>
        /// <param name="user">
        /// Specifies the current user for <paramref name="processId" />.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Information" />.
        /// </param>
        [LoggerMessage(EventId = 43, EventName = "WriteInformationAllLog", Level = LogLevel.Information, Message = "{computer}({processId}:{managedThreadId}:{nativeThreadId}) [{timeGenerated}] : {user} at {source} generated {messageData}")]
        public static partial void WriteInformationLog(
            this ILogger logger,
            object messageData,
            string source,
            string computer,
            int managedThreadId,
            int nativeThreadId,
            int processId,
            DateTime timeGenerated,
            string user,
            LogLevel level = LogLevel.Information);

        /// <summary>
        /// Write <paramref name="message" /> to <paramref name="logger" /> at level <see cref="LogLevel.Trace" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="message">
        /// Specifies the message text to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Trace" />.
        /// </param>
        [LoggerMessage(EventId = 2, EventName = "WriteTraceLog", Level = LogLevel.Trace, Message = "Output {message}")]
        public static partial void WriteTraceLog(this ILogger logger, string message, LogLevel level = LogLevel.Trace);

        /// <summary>
        /// Write <paramref name="message" /> to <paramref name="logger" /> at level <see cref="LogLevel.Warning" />.
        /// </summary>
        /// <param name="logger">
        /// Specifies the extension method <see cref="ILogger" /> to use.
        /// </param>
        /// <param name="message">
        /// Specifies the message text to log.
        /// </param>
        /// <param name="level">
        /// Specifies the <see cref="LogLevel" /> to use. Defaults to <see cref="LogLevel.Warning" />.
        /// </param>
        [LoggerMessage(EventId = 6, EventName = "WriteWarningLog", Level = LogLevel.Critical, Message = "Output {message}")]
        public static partial void WriteWarningLog(this ILogger logger, string message, LogLevel level = LogLevel.Warning);

        #endregion Public Methods
    }
}
