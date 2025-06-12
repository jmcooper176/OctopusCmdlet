using System.Globalization;
using System.Management.Automation;

namespace OctopusCmdlet.Utility
{
    /// <summary>
    /// Implements the 'ValidateConsoleColumn' validate arguments attribute.
    /// </summary>
    public class ValidateConsoleColumn : ValidateArgumentsAttribute
    {
        #region Protected Methods

        /// <summary>
        /// Verifies the value of <paramref name="arguments" /> is a valid <see cref="Console" /> column value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Throws if <paramref name="arguments" /> is less than zero or greater than <see cref="Console.BufferWidth" />
        /// </exception>
        protected override void Validate(object arguments, EngineIntrinsics engineIntrinsics)
        {
            var column = Convert.ToInt32(arguments, CultureInfo.InvariantCulture);

            if (column < 0 || column >= Console.BufferWidth)
            {
                throw new ArgumentOutOfRangeException(nameof(arguments), arguments, $"Column {column} must be in the range [0, {Console.BufferWidth})");
            }
        }

        #endregion Protected Methods
    }
}
