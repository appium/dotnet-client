using System;

namespace OpenQA.Selenium.Appium
{
    public static class GuardClauses
    {
        /// <summary>
        /// The given value must not be null, otherwise an <see cref="ArgumentNullException"/>
        /// will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value to test</param>
        /// <param name="paramName">
        /// The name of the parameter which is being checked. This is to help identify
        /// the parameter which did not meet the requirement. Use nameof() to ensure
        /// proper refactoring support.
        /// </param>
        public static T RequireNotNull<T>(this T value, string paramName) where T : class
        {
            return value ?? throw new ArgumentNullException(paramName);
        }

        /// <summary>
        /// The given value must be greater than or equal to 0 (zero), otherwise
        /// an <see cref="ArgumentOutOfRangeException"/> will be thrown.
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <param name="paramName">
        /// The name of the parameter which is being checked. This is to help identify
        /// the parameter which did not meet the requirement. Use nameof() to ensure
        /// proper refactoring support.
        /// </param>
        public static int RequireIsPositive(this int value, string paramName)
        {
            if (value >= 0)
                return value;

            throw new ArgumentOutOfRangeException(paramName, "Must be 0 (zero) or greater");
        }

        /// <summary>
        /// The given value must be between 0 and 1, otherwise an <see cref="ArgumentOutOfRangeException"/>
        /// will be thrown.
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <param name="paramName">
        /// The name of the parameter which is being checked. This is to help identify
        /// the parameter which did not meet the requirement. Use nameof() to ensure
        /// proper refactoring support.
        /// </param>
        /// <returns></returns>
        public static double RequirePercentage(this double value, string paramName)
        {
            if (value > 0 && value < 1)
                return value;

            throw new ArgumentOutOfRangeException(paramName, "Must be a value between 0 and 1");
        }
    }
}
