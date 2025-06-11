// Ignore Spelling: Cmdlet

using Microsoft.PowerShell.Commands;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Security;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OctopusCmdlet.Utility
{
    [Cmdlet(VerbsCommunications.Write, "Output")]
    [OutputType(typeof(void))]
    public class WriteOutput : PSCmdlet
    {
        #region Public Constructors

        public WriteOutput()
        {
            CmdletName = MyInvocation.MyCommand.Name;
        }

        #endregion Public Constructors

        #region Internal Properties

        internal string CmdletName { get; }

        #endregion Internal Properties

        #region Public Constructors

        /// <summary>
        /// </summary>
        /// <param name="inputObject">
        /// </param>
        /// <param name="backgroundColor">
        /// </param>
        /// <param name="foregroundColor">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="IOException">
        /// </exception>
        public static void WriteOutputCommand(
            PSObject[] inputObject,
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor,
            bool noNewLine = false,
            object? separator = null)
        {
            bool first = true;
            separator = noNewLine ? (separator ?? ' ') : Environment.NewLine;

            try
            {
                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;

                foreach (var item in inputObject)
                {
                    if (first)
                    {
                        Console.Write(item);
                        first = false;
                    }
                    else
                    {
                        Console.Write(separator);
                        Console.Write(item);
                    }
                }

                if (!noNewLine)
                {
                    Console.WriteLine();
                }
            }
            catch (IOException ex)
            {
                WriteError errorLogger = new();
                errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, null);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static void WriteOutputCommand(
            Stream stream,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(stream);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
                else if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, stream);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="writer">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="SecurityException">
        /// </exception>
        /// <exception cref="IOException">
        /// </exception>
        public static void WriteOutputCommand(
            StreamWriter writer,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                Console.SetOut(writer);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, writer);
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, writer);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotSpecified, writer);
                }
            }

            try
            {
                if (noEnumerate)
                {
                    if (noNewLine)
                    {
                        Console.Write(inputObject);
                    }
                    else
                    {
                        Console.WriteLine(inputObject);
                    }
                }
                else
                {
                    bool first = true;
                    separator = noNewLine ? (separator ?? ' ') : Environment.NewLine;

                    foreach (var item in inputObject)
                    {
                        if (first)
                        {
                            Console.Write(item);
                            first = false;
                        }
                        else
                        {
                            Console.Write(separator);
                            Console.Write(item);
                        }
                    }

                    if (!noNewLine)
                    {
                        Console.WriteLine();
                    }
                }
            }
            catch (IOException ex)
            {
                WriteError errorLogger = new();
                errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, writer);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        public static void WriteStandardErrorCommand(
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(Console.OpenStandardError(), inputObject, noEnumerate, noNewLine, separator);
        }

        /// <summary>
        /// </summary>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        public static void WriteStandardOutputCommand(
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(Console.OpenStandardInput(), inputObject, noEnumerate, noNewLine, separator);
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// </exception>
        /// <exception cref="IOException">
        /// </exception>
        /// <exception cref="SecurityException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is UnauthorizedAccessException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, path);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is DirectoryNotFoundException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is PathTooLongException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                }
                else if (ex is IOException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SyntaxError, path);
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, this);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void WriteOutputCommand(
            Stream stream,
            Encoding encoding,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(stream, encoding);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    if (stream == null)
                    {
                        errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, stream);
                    }
                    else
                    {
                        errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, encoding);
                    }
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="append">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// </exception>
        /// <exception cref="IOException">
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// </exception>
        /// <exception cref="SecurityException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            bool append,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path, append);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is UnauthorizedAccessException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, path);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is DirectoryNotFoundException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is IOException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SyntaxError, path);
                }
                else if (ex is PathTooLongException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, this);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="options">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            FileStreamOptions options,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path, options);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="stream">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="bufferSize">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        public void WriteOutputCommand(
            Stream stream,
            Encoding encoding,
            int bufferSize,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(stream, encoding, bufferSize);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    if (stream == null)
                    {
                        errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, stream);
                    }
                    else
                    {
                        errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, encoding);
                    }
                }
                else if (ex is ArgumentOutOfRangeException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, bufferSize);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="append">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="UnauthorizedAccessException">
        /// </exception>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// </exception>
        /// <exception cref="IOException">
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// </exception>
        /// <exception cref="SecurityException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            bool append,
            Encoding encoding,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path, append, encoding);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is UnauthorizedAccessException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, this);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is DirectoryNotFoundException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ObjectNotFound, path);
                }
                else if (ex is IOException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SyntaxError, path);
                }
                else if (ex is PathTooLongException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, this);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="options">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            Encoding encoding,
            FileStreamOptions options,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path, encoding, options);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteError errorLogger = new();

                if (ex is ArgumentException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
                else if (ex is ArgumentNullException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, options);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="append">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="bufferSize">
        /// </param>
        /// <param name="inputObject">
        /// </param>
        /// <param name="noEnumerate">
        /// </param>
        /// <param name="noNewLine">
        /// </param>
        /// <param name="separator">
        /// </param>
        /// <exception cref="ArgumentException">
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        /// <exception cref="IOException">
        /// </exception>
        /// <exception cref="SecurityException">
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// </exception>
        public void WriteOutputCommand(
            string path,
            bool append,
            Encoding encoding,
            int bufferSize,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            try
            {
                using StreamWriter sw = new(path, append, encoding, bufferSize);
                WriteOutputCommand(sw, inputObject, noEnumerate, noNewLine, separator);
            }
            catch (Exception ex)
            {
                WriteFatal errorLogger = new();

                if (ex is ArgumentException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                }
                else if (ex is ArgumentNullException)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                    }
                    else
                    {
                        errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, encoding);
                    }
                }
                else if (ex is ArgumentOutOfRangeException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, bufferSize);
                }
                else if (ex is IOException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SyntaxError, path);
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, this);
                }
                else if (ex is UnauthorizedAccessException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, path);
                }
                else if (ex is DirectoryNotFoundException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ObjectNotFound, path);
                }
                else if (ex is PathTooLongException)
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                }
                else
                {
                    errorLogger.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        #endregion Public Constructors
    }
}
