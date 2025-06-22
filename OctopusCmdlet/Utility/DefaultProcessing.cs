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
using Microsoft.ApplicationInsights.DataContracts;

using Octopus.Client.Model.Forms;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Management.Automation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using SessionState = System.Management.Automation.SessionState;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements <c> Defaultt Processing </c> for the <see cref="PowerShell" /> protected methods
    /// <see cref="Cmdlet.BeginProcessing" />, <see cref="Cmdlet.ProcessRecord" />, <see cref="Cmdlet.StopProcessing" />, and <see cref="Cmdlet.EndProcessing" />.
    /// </summary>
    public static class DefaultProcessing
    {
        #region Public Methods

        /// <summary>
        /// Get the value of <see cref="SessionState.PSVariable" /><paramref name="name" /> or the <paramref name="defaultValue" />.
        /// </summary>
        /// <typeparam name="TPreference">
        /// Specifies the enumeration type of the <paramref name="name" /><see cref="PSVariable" />--usually
        /// <see cref="ActionPreference" /> or <see cref="ConfirmImpact" />.
        /// </typeparam>
        /// <param name="sessionState">
        /// Specifies the <see cref="SessionState" /> to use.
        /// </param>
        /// <param name="name">
        /// Specifies the name of the <see cref="SessionState.PSVariable" /> to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// Specifies the default value to return if <paramref name="name" /> is not found.
        /// </param>
        /// <returns>
        /// If <paramref name="name" /><see cref="PSVariable" /> is found returns the value; otherwise, returns <paramref name="defaultValue" />.
        /// </returns>
        /// <remarks>
        /// If <paramref name="name" /> is <see langref="null" />, empty, or all whitespace, returns <see langref="null" />.
        /// </remarks>
        public static TPreference? GetPreferenceVariable<TPreference>(this SessionState sessionState, string name, TPreference defaultValue)
            where TPreference : Enum
        {
            return (TPreference?)sessionState.GetPreferenceVariable<TPreference>(name) ?? defaultValue;
        }

        /// <summary>
        /// Get the value of <see cref="SessionState.PSVariable" /><paramref name="name" />; otherwise, <see langref="null" />.
        /// </summary>
        /// <typeparam name="TPreference">
        /// Specifies the enumeration type of the <paramref name="name" /><see cref="PSVariable" />--usually
        /// <see cref="ActionPreference" /> or <see cref="ConfirmImpact" />.
        /// </typeparam>
        /// <param name="sessionState">
        /// Specifies the <see cref="SessionState" /> to use.
        /// </param>
        /// <param name="name">
        /// Specifies the name of the <see cref="SessionState.PSVariable" /> to retrieve.
        /// </param>
        /// <returns>
        /// If <paramref name="name" /><see cref="PSVariable" /> is found returns the value; otherwise, returns <see langref="null" />.
        /// </returns>
        /// <remarks>
        /// If <paramref name="name" /> is <see langref="null" />, empty, or all whitespace, returns <see langref="null" />.
        /// </remarks>
        public static TPreference? GetPreferenceVariable<TPreference>(this SessionState sessionState, string name)
            where TPreference : Enum
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return default;
            }
            else
            {
                return (TPreference?)sessionState.PSVariable.GetValue(name, null);
            }
        }

        /// <summary>
        /// Get the value of <see cref="SessionState.PSVariable" /><paramref name="name" />; otherwise, <see langref="null" />.
        /// </summary>
        /// <param name="sessionState">
        /// Specifies the <see cref="SessionState" /> to use.
        /// </param>
        /// <param name="name">
        /// Specifies the name of the <see cref="SessionState.PSVariable" /> to retrieve.
        /// </param>
        /// <returns>
        /// If <paramref name="name" /><see cref="PSVariable" /> is found returns the value; otherwise, returns <see langref="null" />.
        /// </returns>
        /// <remarks>
        /// If <paramref name="name" /> is <see langref="null" />, empty, or all whitespace, returns <see langref="null" />.
        /// </remarks>
        public static object? GetSessionStateVariable(this SessionState sessionState, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }
            else
            {
                return sessionState.PSVariable.GetValue(name, null);
            }
        }

        /// <summary>
        /// Get the value of <see cref="SessionState.PSVariable" /><paramref name="name" /> or the <paramref name="defaultValue" />.
        /// </summary>
        /// <param name="sessionState">
        /// Specifies the <see cref="SessionState" /> to use.
        /// </param>
        /// <param name="name">
        /// Specifies the name of the <see cref="SessionState.PSVariable" /> to retrieve.
        /// </param>
        /// <param name="defaultValue">
        /// Specifies the default value to return if <paramref name="name" /> is not found.
        /// </param>
        /// <returns>
        /// If <paramref name="name" /><see cref="PSVariable" /> is found returns the value; otherwise, returns the <paramref name="defaultValue" />.
        /// </returns>
        /// <remarks>
        /// If <paramref name="name" /> is <see langref="null" />, empty, or all whitespace, returns <see langref="null" />.
        /// </remarks>
        public static object? GetSessionStateVariable(this SessionState sessionState, string name, object defaultValue)
        {
            return sessionState.GetSessionStateVariable(name) ?? defaultValue;
        }

        /// <summary>
        /// Default process for <see cref="Cmdlet.BeginProcessing" />
        /// </summary>
        /// <param name="cmdletName">
        /// Specifies the <see cref="Cmdlet" /> name.
        /// </param>
        /// <param name="stopping">
        /// Specifies the <c> Stopping </c> property of the <see cref="Cmdlet" />.
        /// </param>
        /// <param name="preInvoke">
        /// Specifies the delegate to optionally call to perform additional <see cref="Cmdlet.BeginProcessing" /> actions. Maybe <see langref="null" />.
        /// </param>
        public static void InitializeBeginProcessing(string cmdletName, bool stopping, Action? preInvoke = null)
        {
            if (stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{cmdletName} is Stopping in 'BeginProcessing'");
                return;
            }
            else
            {
                preInvoke?.Invoke();
            }
        }

        /// <summary>
        /// Default process for <see cref="Cmdlet.BeginProcessing" />
        /// </summary>
        /// <param name="cmdletName">
        /// Specifies the <see cref="Cmdlet" /> name.
        /// </param>
        /// <param name="boundParameters">
        /// Specifies the <see cref="IDictionary{TKey, TValue}" /> representing the <see cref="Cmdlet" /> bound parameters.
        /// </param>
        /// <param name="sessionState">
        /// Specifies the <see cref="SessionState" /> property of the <see cref="Cmdlet" />.
        /// </param>
        /// <param name="stopping">
        /// Specifies the <c> Stopping </c> property of the <see cref="Cmdlet" />.
        /// </param>
        /// <param name="force">
        /// Specifies the value of the <c> Force </c> parameter of the <see cref="Cmdlet" />. Defaults to <see langref="false" />.
        /// </param>
        /// <param name="preInvoke">
        /// Specifies the delegate to optionally call to perform additional <see cref="Cmdlet.BeginProcessing" /> actions. Maybe <see langref="null" />.
        /// </param>
        public static void InitializeBeginProcessing(
            string cmdletName,
            IDictionary<string, object> boundParameters,
            SessionState sessionState,
            bool stopping,
            bool force = false,
            Action? preInvoke = null)
        {
            BoundParameterDictionary bp = [.. boundParameters];

            sessionState.SetPreferenceVariable<ConfirmImpact>(() => force && !bp.HasParameter("Confirm"), "ConfirmPreference", ConfirmImpact.None);

            DefaultProcessing.InitializeBeginProcessing(cmdletName, stopping, preInvoke);
        }

        /// <summary>
        /// Default process for <see cref="Cmdlet.EndProcessing" />
        /// </summary>
        /// <param name="cmdletName">
        /// Specifies the <see cref="Cmdlet" /> name.
        /// </param>
        /// <param name="stopping">
        /// Specifies the <c> Stopping </c> property of the <see cref="Cmdlet" />.
        /// </param>
        /// <param name="dispose">
        /// Specifies the cleanup delegate for the <see cref="Cmdlet" />.
        /// </param>
        public static void InitializeEndProcessing(string cmdletName, bool stopping, Action dispose)
        {
            dispose.Invoke();

            if (stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{cmdletName} is Stopping in 'EndProcessing'");
                return;
            }
        }

        /// <summary>
        /// Default process for <see cref="Cmdlet.ProcessRecord" />
        /// </summary>
        /// <param name="cmdletName">
        /// Specifies the <see cref="Cmdlet" /> name.
        /// </param>
        /// <param name="stopping">
        /// Specifies the <c> Stopping </c> property of the <see cref="Cmdlet" />.
        /// </param>
        /// <param name="processRecord">
        /// Specifies the record processing delegate for the <see cref="Cmdlet" />.
        /// </param>
        public static void InitializeProcessRecord(string cmdletName, bool stopping, Action processRecord)
        {
            if (stopping)
            {
                WriteWarning pipelineStopping = new();
                pipelineStopping.WriteWarningCommand($"{cmdletName} is Stopping in 'EndProcessing'");
                return;
            }
            else
            {
                processRecord.Invoke();
            }
        }

        /// <summary>
        /// Default process for <see cref="Cmdlet.StopProcessing" />
        /// </summary>
        /// <param name="cmdletName">
        /// Specifies the <see cref="Cmdlet" /> name.
        /// </param>
        /// <param name="cmdlet">
        /// Specifies the <see cref="Cmdlet" /> instance that is stopping.
        /// </param>
        /// <param name="lineNumber">
        /// Specifies the <see cref="Cmdlet" /> line number where <see cref="Cmdlet.StopProcessing" /> has been called.
        /// </param>
        /// <param name="dispose">
        /// Specifies the cleanup delegate for the <see cref="Cmdlet" />. Maybe <see langref="null" />.
        /// </param>
        /// <exception cref="PipelineStoppedException">
        /// Always throws when <see cref="Cmdlet.StopProcessing" /> is called.
        /// </exception>
        [DoesNotReturn]
        public static void InitializeStopProcessing(string cmdletName, Cmdlet cmdlet, [CallerLineNumber] int lineNumber = 0, Action? dispose = null)
        {
            dispose?.Invoke();

            NewErrorRecord stopProcessingErr = new();
            FormatErrorId pipelineStoppedEx = new();

            var er = stopProcessingErr.NewErrorRecordCommand(
                new PipelineStoppedException($"{cmdletName} : PipelineStoppedException : Pipeline stopping because 'StopProcessing' called"),
                pipelineStoppedEx.FormatErrorIdCommand(typeof(PipelineStoppedException), "StopProcessing", lineNumber),
                ErrorCategory.OperationStopped,
                cmdlet);
            WriteFatal operationStopped = new();
            operationStopped.WriteFatalCommand(er);
        }

        /// <summary>
        /// </summary>
        /// <param name="sessionState">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool RemoveSessionStateVariable(this SessionState sessionState, string name)
        {
            bool result = sessionState.TestSessionStateVariable(name);

            if (result)
            {
                sessionState.PSVariable.Remove(name);
            }

            return result;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TPreference">
        /// </typeparam>
        /// <param name="sessionState">
        /// </param>
        /// <param name="predicate">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <param name="value">
        /// </param>
        public static void SetPreferenceVariable<TPreference>(this SessionState sessionState, Func<bool> predicate, string name, TPreference value)
            where TPreference : Enum
        {
            sessionState.SetSessionStateVariable(predicate, name, value);
        }

        /// <summary>
        /// </summary>
        /// <param name="sessionState">
        /// </param>
        /// <param name="predicate">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <param name="value">
        /// </param>
        public static void SetSessionStateVariable(this SessionState sessionState, Func<bool> predicate, string name, object value)
        {
            if (predicate.Invoke() && !string.IsNullOrWhiteSpace(name))
            {
                sessionState.PSVariable.Set(name, value);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sessionState">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool TestSessionStateVariable(this SessionState sessionState, string name)
        {
            return !string.IsNullOrWhiteSpace(name) && sessionState.PSVariable.Get(name) != null;
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TPreference">
        /// </typeparam>
        /// <param name="sessionState">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool TryGetPreferenceVariable<TPreference>(this SessionState sessionState, string name, [MaybeNullWhen(false)] out TPreference value)
                    where TPreference : Enum
        {
            if (sessionState.TestSessionStateVariable(name))
            {
                value = sessionState.GetPreferenceVariable<TPreference>(name);
                return true;
            }
            else
            {
                value = default;
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sessionState">
        /// </param>
        /// <param name="name">
        /// </param>
        /// <param name="value">
        /// </param>
        /// <returns>
        /// </returns>
        public static bool TryGetSessionStateVariable(this SessionState sessionState, string name, [MaybeNullWhen(false)] out object value)
        {
            if (sessionState.TestSessionStateVariable(name))
            {
                value = sessionState.GetSessionStateVariable(name);
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }

        #endregion Public Methods
    }
}
