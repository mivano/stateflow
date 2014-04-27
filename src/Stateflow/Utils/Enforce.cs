using System;
using System.Collections.Generic;

namespace Stateflow.Utils
{
    /// <summary>
    /// Enforce validations
    /// </summary>
    public static class Enforce
    {
        /// <summary>
        /// Validates that the value is not null.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        public static T ArgumentNotNull<T>(T argument, string description)
            where T : class
        {
            if (argument == null)
                throw new ArgumentNullException(description);

            return argument;
        }

        /// <summary>
        /// Validates if the value is the greater than zero.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        public static T ArgumentGreaterThanZero<T>(T argument, string description)
        {
            if (System.Convert.ToInt32(argument) < 1)
                throw new ArgumentOutOfRangeException(description);

            return argument;
        }

        /// <summary>
        /// Validates if the date is initialized.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">DateTime has not been initialized</exception>
        public static DateTime ArgumentDateIsInitialized(DateTime argument, string description)
        {
            if (argument == DateTime.MinValue)
                throw new ArgumentException("DateTime has not been initialized");

            return argument;
        }

        /// <summary>
        /// Determines whether the dictionary contains the key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="search">The search.</param>
        /// <param name="key">The key.</param>
        /// <param name="description">The description.</param>
        /// <returns></returns>
        /// <exception cref="System.Collections.Generic.KeyNotFoundException"></exception>
        public static Dictionary<T, K> ContainsKey<T, K>(Dictionary<T, K> search,
            T key,
            string description)
        {
            if (search.ContainsKey(key) == false)
            {
                throw new KeyNotFoundException(description);
            }

            return search;
        }

        /// <summary>
        /// Validates the specified condition.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="message">The message.</param>
        /// <exception cref="System.ArgumentException"></exception>
        public static void That(bool condition, string message)
        {
            if (condition == false)
            {
                throw new ArgumentException(message);
            }
        }
    }
}