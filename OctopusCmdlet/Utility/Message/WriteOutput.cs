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

using OctopusCmdlet.Utility.ErrorRecord;
using OctopusCmdlet.Utility.Validator;

using System.Management.Automation;
using System.Security;
using System.Text;

namespace OctopusCmdlet.Utility.Message
{
    /// <summary>
    /// Implements a unification of <c> WriteOutput </c> and <c> Write-Host </c> in the <c> Write-Output </c><see cref="PowerShell" /><see cref="Cmdlet" />.
    /// </summary>
    [Cmdlet(VerbsCommunications.Write, "Output")]
    [CmdletBinding]
    [OutputType(typeof(void))]
    public class WriteOutput : PSCmdlet
    {
        #region Public Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteOutput" /> class.
        /// </summary>
        public WriteOutput()
        {
            const string DEFAULT_OFS = " ";

            FormatErrorId = new();

            BackgroundColor = Console.BackgroundColor;
            CmdletName = MyInvocation.MyCommand.Name;
            Column = 0;
            Encoding = Encoding.Default;
            ForegroundColor = Console.ForegroundColor;
            Row = 0;

            Separator = DEFAULT_OFS;
            SessionState.PSVariable.Set("OFS", Separator);
        }

        public SwitchParameter Append { get; set; }

        [ValidateSet("Black", "DarkBlue", "DarkGreen", "DarkCyan", "DarkRed", "DarkMagenta", "DarkYellow", "Gray", "DarkGray", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White", IgnoreCase = true)]
        public ConsoleColor BackgroundColor { get; set; }

        [ValidateRange(-1, 2147483647)]
        public int BufferSize { get; set; }

        [ValidateConsoleColumnAttribute]
        public int Column { get; set; }

        [ValidateSet("ASCII", "BigEndianUnicode", "Default", "Latin1", "Unicode", "UTF32", "UTF8", IgnoreCase = true)]
        public Encoding? Encoding { get; set; }

        [ValidateSet("Black", "DarkBlue", "DarkGreen", "DarkCyan", "DarkRed", "DarkMagenta", "DarkYellow", "Gray", "DarkGray", "Blue", "Green", "Cyan", "Red", "Magenta", "Yellow", "White", IgnoreCase = true)]
        public ConsoleColor ForegroundColor { get; set; }

        [Parameter(Mandatory = true, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
        public PSObject[] InputObject { get; set; }

        public SwitchParameter LeaveOpen { get; set; }

        public FileMode Mode { get; set; }

        public SwitchParameter NoEnumerate { get; set; }

        public SwitchParameter NoNewLine { get; set; }

        public FileStreamOptions Options { get; set; }

        public string Path { get; set; }

        [ValidateConsoleRowAttribute]
        public int Row { get; set; }

        public object? Separator { get; set; }

        public Stream Stream { get; set; }

        #endregion Public Constructors

        #region Internal Properties

        /// <summary>
        /// Gets a value indicating this <see cref="Cmdlet" /> name.
        /// </summary>
        internal string CmdletName { get; }

        internal FormatErrorId FormatErrorId { get; }

        #endregion Internal Properties

        #region Public Methods

        /// <summary>
        /// Get the first available of: current drive, the system drive, or drive 'C:'.
        /// </summary>
        /// <returns>
        /// Returns the drive letter.
        /// </returns>
        public string GetCurrentDrive()
        {
            try
            {
                return System.IO.Path.GetPathRoot(Directory.GetCurrentDirectory()) ?? Environment.GetEnvironmentVariable("SystemDrive") ?? "C:";
            }
            catch (Exception ex)
            {
                WriteError logException = new();

                if (ex is ArgumentException)
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, nameof(System.IO.Path.GetPathRoot));
                }
                else if (ex is UnauthorizedAccessException)
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, nameof(Directory.GetCurrentDirectory));
                }
                else if (ex is NotSupportedException)
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotImplemented, nameof(Directory.GetCurrentDirectory));
                }
                else if (ex is ArgumentNullException)
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, nameof(Environment.GetEnvironmentVariable));
                }
                else if (ex is SecurityException)
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, nameof(Environment.GetEnvironmentVariable));
                }
                else
                {
                    logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotSpecified, null);
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Tests the drive for <paramref name="path" /> to see it is both 'Ready' and has free space.
        /// </summary>
        /// <param name="path">
        /// Specifies that relative or absolute path for the current drive. This path should be resolvable to a full, absolute path.
        /// </param>
        /// <returns>
        /// <see langref="true" /> if the disk in <paramref name="path" /> is full; otherwise, <see langref="false" />.
        /// </returns>
        public bool TestDiskFull(string path)
        {
            WriteError logException = new();

            if (System.IO.Path.Exists(path))
            {
                try
                {
                    path = System.IO.Path.GetFullPath(path);

                    DriveInfo drive = new(System.IO.Path.GetPathRoot(path) ?? GetCurrentDrive());

                    if (drive.IsReady)
                    {
                        return drive.AvailableFreeSpace == 0L;
                    }
                    else
                    {
                        logException.WriteErrorCommand<System.Management.Automation.DriveNotFoundException>($"Disk {drive.Name} is not ready", FormatErrorId.FormatErrorIdCommand(ErrorCategory.ResourceUnavailable), ErrorCategory.ResourceUnavailable, path);
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    if (ex is ArgumentException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                        return false;
                    }
                    else if (ex is SecurityException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, path);
                        return true;
                    }
                    else if (ex is ArgumentNullException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                        return false;
                    }
                    else if (ex is NotSupportedException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotImplemented, path);
                        return true;
                    }
                    else if (ex is PathTooLongException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                        return false;
                    }
                    else if (ex is UnauthorizedAccessException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, path);
                        return true;
                    }
                    else if (ex is IOException)
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ReadError, path);
                        return true;
                    }
                    else
                    {
                        logException.WriteErrorCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotSpecified, path);
                        return true;
                    }
                }
            }
            else
            {
                logException.WriteErrorCommand<FileNotFoundException>($"Path {path} does not exist", FormatErrorId.FormatErrorIdCommand(ErrorCategory.ObjectNotFound), ErrorCategory.ObjectNotFound, path);
                return true;
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Console" /> with foreground color <paramref name="foregroundColor" />.
        /// </summary>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Console" />.
        /// </param>
        /// <param name="foregroundColor">
        /// Specifies the foreground <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects displayed on the <see cref="Console" />.
        /// </param>
        /// <remarks>
        /// The currently set <see cref="Console.BackgroundColor" /> will be used for the background color.
        /// </remarks>
        public virtual void WriteOutputCommand(
            PSObject[] inputObject,
            ConsoleColor foregroundColor,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(inputObject, Console.BackgroundColor, foregroundColor, noNewLine, separator);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Console" /> with foreground color <paramref name="foregroundColor" />.
        /// </summary>
        /// <param name="column">
        /// Specifies the zero-based buffer column to start writing from.
        /// </param>
        /// <param name="row">
        /// Specifies the zero-based buffer row to start writing from.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Console" />.
        /// </param>
        /// <param name="foregroundColor">
        /// Specifies the foreground <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects displayed on the <see cref="Console" />.
        /// </param>
        /// <remarks>
        /// The currently set <see cref="Console.BackgroundColor" /> will be used for the background color.
        /// </remarks>
        public virtual void WriteOutputCommand(
            int column,
            int row,
            PSObject[] inputObject,
            ConsoleColor foregroundColor,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(column, row, inputObject, Console.BackgroundColor, foregroundColor, noNewLine, separator);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Console" /> with background color
        /// <paramref name="backgroundColor" /> and foreground color <paramref name="foregroundColor" />.
        /// </summary>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Console" />.
        /// </param>
        /// <param name="backgroundColor">
        /// Specifies the background <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="foregroundColor">
        /// Specifies the foreground <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects displayed on the <see cref="Console" />.
        /// </param>
        /// <exception cref="IOException">
        /// Throws if an I/O error has occurred.
        /// </exception>
        public virtual void WriteOutputCommand(
            PSObject[] inputObject,
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor,
            bool noNewLine = false,
            object? separator = null)
        {
            bool first = true;
            separator = noNewLine ? separator ?? ' ' : Environment.NewLine;
            object? targetObject = inputObject.FirstOrDefault();

            try
            {
                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;

                foreach (var item in inputObject)
                {
                    targetObject = item;

                    if (first)
                    {
                        Console.Write(targetObject);
                        first = false;
                    }
                    else
                    {
                        Console.Write(separator);
                        Console.Write(targetObject);
                    }
                }

                if (!noNewLine)
                {
                    Console.WriteLine();
                }
            }
            catch (IOException ex)
            {
                WriteFatal errorLogger = new();
                errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, targetObject);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Console" /> with background color
        /// <paramref name="backgroundColor" /> and foreground color <paramref name="foregroundColor" />.
        /// </summary>
        /// <param name="column">
        /// Specifies the zero-based buffer column to start writing from.
        /// </param>
        /// <param name="row">
        /// Specifies the zero-based buffer row to start writing from.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Console" />.
        /// </param>
        /// <param name="backgroundColor">
        /// Specifies the background <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="foregroundColor">
        /// Specifies the foreground <see cref="ConsoleColor" /> color. There is no default. The acceptable values are:
        /// <list type="bullet">
        /// <item> Black </item>
        /// <item> DarkBlue </item>
        /// <item> DarkGreen </item>
        /// <item> DarkCyan </item>
        /// <item> DarkRed </item>
        /// <item> DarkMagenta </item>
        /// <item> DarkYellow </item>
        /// <item> Gray </item>
        /// <item> DarkGray </item>
        /// <item> Blue </item>
        /// <item> Green </item>
        /// <item> Cyan </item>
        /// <item> Red </item>
        /// <item> Magenta </item>
        /// <item> Yellow </item>
        /// <item> White </item>
        /// </list>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects displayed on the <see cref="Console" />.
        /// </param>
        /// <exception cref="IOException">
        /// Throws if an I/O error has occurred.
        /// </exception>
        public virtual void WriteOutputCommand(
            int column,
            int row,
            PSObject[] inputObject,
            ConsoleColor backgroundColor,
            ConsoleColor foregroundColor,
            bool noNewLine = false,
            object? separator = null)
        {
            bool first = true;
            separator = noNewLine ? separator ?? ' ' : Environment.NewLine;
            object? targetObject = inputObject.FirstOrDefault();
            WriteFatal errorLogger = new();

            try
            {
                if (column >= 0 && column < Console.BufferWidth && row >= 0 && row < Console.BufferHeight)
                {
                    Console.SetCursorPosition(column, row);
                }
                else if (column < 0 || column >= Console.BufferWidth)
                {
                    errorLogger.WriteFatalCommand<ArgumentOutOfRangeException>($"Column {column} is out of range [0, {Console.BufferWidth})", FormatErrorId.FormatErrorIdCommand(ErrorCategory.LimitsExceeded), ErrorCategory.LimitsExceeded, column);
                }
                else
                {
                    errorLogger.WriteFatalCommand<ArgumentOutOfRangeException>($"Row {row} is out of range [0, {Console.BufferHeight})", FormatErrorId.FormatErrorIdCommand(ErrorCategory.LimitsExceeded), ErrorCategory.LimitsExceeded, row);
                }

                Console.BackgroundColor = backgroundColor;
                Console.ForegroundColor = foregroundColor;

                foreach (var item in inputObject)
                {
                    targetObject = item;

                    if (first)
                    {
                        Console.Write(targetObject);
                        first = false;
                    }
                    else
                    {
                        Console.Write(separator);
                        Console.Write(targetObject);
                    }
                }

                if (!noNewLine)
                {
                    Console.WriteLine();
                }
            }
            catch (IOException ex)
            {
                errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, targetObject);
            }
            finally
            {
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <paramref name="stream" />.
        /// </summary>
        /// <param name="stream">
        /// Specifies the <see cref="Stream" /> to write <paramref name="inputObject" /> objects to.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <paramref name="stream" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        /// <param name="leaveOpen">
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="bufferSize">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when the created <see cref="StreamWriter" /> is <see langref="null" />.
        /// </exception>
        /// <exception cref="SecurityException">
        /// Throws when the caller does not have the permissions.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws when <paramref name="bufferSize" /> is less than -1.
        /// </exception>
        /// <exception cref="IOException">
        /// Throws if an I/O error has occurred.
        /// </exception>
        public virtual void WriteOutputCommand(
            Stream stream,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null,
            bool leaveOpen = false,
            Encoding? encoding = null,
            int bufferSize = -1)
        {
            try
            {
                using var sw = new StreamWriter(stream, encoding ?? Encoding.Default, bufferSize, leaveOpen);
                Console.SetOut(sw);
            }
            catch (Exception ex)
            {
                WriteFatal errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    if (stream == null)
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, stream);
                    }
                    else
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, encoding);
                    }
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, stream);
                }
                else if (ex is ArgumentOutOfRangeException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, bufferSize);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
                }
                else
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.NotSpecified, stream);
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
                    separator = noNewLine ? separator ?? ' ' : Environment.NewLine;

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
                WriteFatal errorLogger = new();
                errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, stream);
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and <see cref="FileMode" /><paramref name="mode" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="mode">
        /// Specifies on of the enumeration values of <see cref="FileMode" /> that determines how to open or create the file.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the underlying <see cref="FileStream" />.
        /// </param>
        /// <param name="encoding">
        /// </param>
        /// <param name="bufferSize">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when either the created <see cref="FileStream" /> or the <paramref name="encoding" /> is <see langref="null" />.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws when <paramref name="bufferSize" /> is less than -1.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws when the underlying <see cref="FileStream" /> is not writable..
        /// </exception>
        public virtual void WriteOutputCommand(
            string path,
            FileMode mode,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null,
            Encoding? encoding = default,
            int bufferSize = -1)
        {
            try
            {
                using FileStream fs = new(path, mode);
                WriteOutputCommand(fs, inputObject, noEnumerate, noNewLine, separator, leaveOpen: false, encoding, bufferSize);
            }
            catch (Exception ex)
            {
                WriteFatal errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    if (encoding == null)
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, encoding);
                    }
                    else
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, null);
                    }
                }
                else if (ex is ArgumentOutOfRangeException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, bufferSize);
                }
                else if (ex is ArgumentException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
                else
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">
        /// Specifies the <see cref="Stream" /> to write to.
        /// </param>
        /// <param name="encoding">
        /// Specifies the character encoding to use.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Stream" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            Stream stream,
            Encoding encoding,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(stream, inputObject, noEnumerate, noNewLine, separator, leaveOpen: true, encoding);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and switch <paramref name="append" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="append">
        /// If <see langref="true" />, <see cref="FileMode.Append" /> will be selected; otherwise, <see cref="FileMode.Create" />
        /// will be selected.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the underlying <paramref name="FileStream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            string path,
            bool append,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(path, append ? FileMode.Append : FileMode.Create, inputObject, noEnumerate, noNewLine, separator);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and <see cref="FileStreamOptions" /><paramref name="options" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="options">
        /// Specifies a <see cref="FileStreamOptions" /> object that specifies the configuration options for the underlying <see cref="FileStream" />.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the underlying <paramref name="FileStream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            string path,
            FileStreamOptions options,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(path, Encoding.UTF8, options, inputObject, noEnumerate, noNewLine, separator, leaveOpen: true);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="Stream" />.
        /// </summary>
        /// <param name="stream">
        /// Specifies the <see cref="Stream" /> to write to.
        /// </param>
        /// <param name="encoding">
        /// Specifies the character encoding to use.
        /// </param>
        /// <param name="bufferSize">
        /// Specifies the buffer size in bytes.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="Stream" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            Stream stream,
            Encoding encoding,
            int bufferSize,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(stream, inputObject, noEnumerate, noNewLine, separator, leaveOpen: true, encoding, bufferSize);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and the switch <paramref name="append" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="append">
        /// If <see langref="true" />, <see cref="FileMode.Append" /> will be selected; otherwise, <see cref="FileMode.Create" />
        /// will be selected.
        /// </param>
        /// <param name="encoding">
        /// Specifies the character encoding to use.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the underlying <paramref name="FileStream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            string path,
            bool append,
            Encoding encoding,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(path, append ? FileMode.Append : FileMode.Create, inputObject, noEnumerate, noNewLine, separator, encoding);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and <see cref="FileStreamOptions" /><paramref name="options" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="encoding">
        /// Specifies the character encoding to use.
        /// </param>
        /// <param name="options">
        /// Specifies a <see cref="FileStreamOptions" /> object that specifies the configuration options for the underlying <see cref="FileStream" />.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <see cref="FileStream" /> which writes to <paramref name="stream" />.
        /// </param>
        /// <param name="leaveOpen">
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Throws when <paramref name="options" /> is <see langref="null" />.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Throws when either:
        /// <list type="bullet">
        /// <item> <paramref name="path" /> is <see langref="null" />, empty, or contains only whitespace; </item>
        /// <item> <paramref name="path" /> contains one or more invalid characters; OR </item>
        /// <item> <paramref name="path" /> refers to a non-file device, such as `CON:`, `COM1:`, or `LPT1:`, in an NTFS environment. </item>
        /// </list>
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// Throws if <paramref name="path" /> refers to a non-file device, such as `CON:`, `COM1:`, or `LPT1:`, in an NTFS environment.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        /// Throws when <see cref="FileMode" /> is set to <see cref="FileMode.Truncate" /> or <see cref="FileMode.Open" /> and the
        /// file path <paramref name="path" /> does not exist. The file must already exist in these modes.
        /// </exception>
        /// <exception cref="IOException">
        /// Throws when:
        /// <list type="bullet">
        /// <item>
        /// An I/O error, such as specifying <see cref="FileMode.CreateNew" /> when the file path specified by
        /// <paramref name="path" /> already exists, occurs;
        /// </item>
        /// <item> The underlying <see cref="FileStream" /> has been closed; </item>
        /// <item>
        /// The disk was full (when <see cref="FileStreamOptions.PreallocationSize" /> was provided and <paramref name="path" /> was
        /// pointing to a regular file); OR
        /// </item>
        /// <item>
        /// The file was too large (when <see cref="FileStreamOptions.PreallocationSize" /> was provided and
        /// <paramref name="path" /> was pointing to a regular file).
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="SecurityException">
        /// Throws when the caller does not have the required permissions.
        /// </exception>
        /// <exception cref="UnauthorizedAccessException">
        /// Throws when:
        /// <list type="bullet">
        /// <item>
        /// The <see cref="FileStreamOptions.Access" /> requested is not permitted by the operating system for the specified
        /// <paramref name="path" />, such as when <see cref="FileStreamOptions.Access" /> is <see cref="FileAccess.Write" /> or
        /// <see cref="FileAccess.ReadWrite" /> and the file or directory is set for read-only access; OR
        /// </item>
        /// <item>
        /// <see cref="FileOptions.Encrypted" /> is specified for <see cref="FileStreamOptions.Options" />, but file encryption is
        /// not supported on the current platform.
        /// </item>
        /// </list>
        /// </exception>
        /// <exception cref="PathTooLongException">
        /// Throws when either the specified <paramref name="path" /> or the file name portion of <paramref name="path" />, or both
        /// exceed the system-defined maximum length.
        /// </exception>
        public virtual void WriteOutputCommand(
            string path,
            Encoding encoding,
            FileStreamOptions options,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null,
            bool leaveOpen = false)
        {
            try
            {
                using FileStream fs = new(path, options);
                WriteOutputCommand(fs, inputObject, noEnumerate, noNewLine, separator, leaveOpen, encoding);
            }
            catch (Exception ex)
            {
                WriteFatal errorLogger = new();

                if (ex is ArgumentNullException)
                {
                    if (string.IsNullOrEmpty(path))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                    }
                    else
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, options);
                    }
                }
                else if (ex is ArgumentException)
                {
                    if (string.IsNullOrWhiteSpace(path))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.InvalidArgument, path);
                    }
                    else if (path.Any(c => System.IO.Path.GetInvalidPathChars().Contains(c)))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ParserError, path);
                    }
                    else
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.DeviceError, path);
                    }
                }
                else if (ex is NotSupportedException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.DeviceError, path);
                }
                else if (ex is FileNotFoundException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ObjectNotFound, options.Mode);
                }
                else if (ex is IOException)
                {
                    if (System.IO.Path.Exists(path) && options.Mode == FileMode.CreateNew)
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ResourceExists, path);
                    }
                    else if (options.PreallocationSize > 0 && System.IO.Path.Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.Normal) && TestDiskFull(path))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.QuotaExceeded, path);
                    }
                    else if (options.PreallocationSize > 0 && System.IO.Path.Exists(path) && File.GetAttributes(path).HasFlag(FileAttributes.Normal))
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.QuotaExceeded, path);
                    }
                    else
                    {
                        errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.ResourceUnavailable, null);
                    }
                }
                else if (ex is SecurityException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.SecurityError, null);
                }
                else if (ex is UnauthorizedAccessException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.PermissionDenied, path);
                }
                else if (ex is PathTooLongException)
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.LimitsExceeded, path);
                }
                else
                {
                    errorLogger.WriteFatalCommand(ex, FormatErrorId.FormatErrorIdCommand(ex), ErrorCategory.WriteError, path);
                }
            }
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="FileStream" /> initialized with file path
        /// <paramref name="path" /> and the switch <paramref name="append" />.
        /// </summary>
        /// <param name="path">
        /// Specifies a relative or absolute path for the file that the current <see cref="FileStream" /> will encapsulate.
        /// </param>
        /// <param name="append">
        /// If <see langref="true" />, <see cref="FileMode.Append" /> will be selected; otherwise, <see cref="FileMode.Create" />
        /// will be selected.
        /// </param>
        /// <param name="encoding">
        /// Specifies the character encoding to use.
        /// </param>
        /// <param name="bufferSize">
        /// Specifies the buffer size in bytes.
        /// </param>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="FileStream" /> which writes to <paramref name="path" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        public virtual void WriteOutputCommand(
            string path,
            bool append,
            Encoding encoding,
            int bufferSize,
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            WriteOutputCommand(path, append ? FileMode.Append : FileMode.Create, inputObject, noEnumerate, noNewLine, separator, encoding, bufferSize);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="TextWriter" /><see cref="Console.OpenStandardError()" />.
        /// </summary>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="TextWriter" /> which writes to <see cref="Console.OpenStandardError()" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        public virtual void WriteStandardErrorCommand(
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            var bufferSize = Console.BufferHeight * Console.BufferWidth;
            WriteOutputCommand(Console.OpenStandardError(), inputObject, noEnumerate, noNewLine, separator, leaveOpen: true, Encoding.Latin1, bufferSize);
        }

        /// <summary>
        /// Writes the specified <paramref name="inputObject" /> objects to the <see cref="TextWriter" /><see cref="Console.OpenStandardOutput()" />.
        /// </summary>
        /// <param name="inputObject">
        /// Specifies the objects to write to the <see cref="TextWriter" /> which writes to <see cref="Console.OpenStandardOutput()" />.
        /// </param>
        /// <param name="noEnumerate">
        /// By default, this <see cref="Cmdlet" /> always enumerates its output. The `NoEnumerate` parameter suppresses the default
        /// behavior, and prevents this <see cref="Cmdlet" /> from enumerating output.
        /// <para>
        /// The `NoEnumerate` parameter is only useful within a pipeline. Trying to see the effects of `NoEnumerate` in the console
        /// is problematic because <see cref="PowerShell" /> adds `Out-Default` to the end of every command line, which results in
        /// enumeration. But if you pipe this <see cref="Cmdlet" /> with `NoEnumerate` to another <see cref="Cmdlet" />, the
        /// downstream <see cref="Cmdlet" /> receives the collection object, not the enumerated items of the collection.
        /// </para>
        /// </param>
        /// <param name="noNewLine">
        /// The string representations of the input objects are concatenated to form the output. No spaces or newlines are inserted
        /// between output strings. No newline is added after the last output string.
        /// </param>
        /// <param name="separator">
        /// Specifies a separator string to insert between objects written to the <paramref name="stream" />.
        /// </param>
        public virtual void WriteStandardOutputCommand(
            PSObject[] inputObject,
            bool noEnumerate = false,
            bool noNewLine = false,
            object? separator = null)
        {
            var bufferSize = Console.BufferHeight * Console.BufferWidth;
            WriteOutputCommand(Console.OpenStandardInput(), inputObject, noEnumerate, noNewLine, separator, leaveOpen: true, Encoding.Latin1, bufferSize);
        }

        #endregion Public Methods

        #region Protected Methods

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
