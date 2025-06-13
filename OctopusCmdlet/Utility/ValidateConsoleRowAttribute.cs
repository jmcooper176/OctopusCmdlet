using System;
using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateConsoleRow' validate arguments attribute.
    /// </summary>
    public class ValidateConsoleRowAttribute : ValidateArgumentsAttribute
    {
        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid <see cref="Console" /> row value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if <paramref name="arguments" /> is less than zero or greater than <see cref="Console.BufferHeight" />
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var row = Convert.ToInt32(arguments, CultureInfo.InvariantCulture);

            if (row < 0 || row >= Console.BufferHeight)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), arguments, $"Row {row} must be in the range [0, {Console.BufferHeight})");
            }
        }

        #endregion Protected Methods
    }
}
