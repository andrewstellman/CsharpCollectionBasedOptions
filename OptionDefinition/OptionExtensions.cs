using System;
using System.Collections.Generic;
using System.Linq;

namespace Options
{
    /// <summary>
    /// Provides extension methods that return Options.
    /// </summary>
    public static class OptionExtensions
    {
        /// <summary>
        /// Gets a value from a dictionary if available.
        /// </summary>
        /// <returns>Some(value) if available, or None if not.</returns>
        /// <param name="dict">The dictionary to access.</param>
        /// <param name="key">The key to get.</param>
        /// <typeparam name="TKey">The key type.</typeparam>
        /// <typeparam name="TValue">The value type.</typeparam>
        public static Option<TValue> GetOrElse<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) =>
             dict.ContainsKey(key) ? Option<TValue>.Some(dict[key]) : Option<TValue>.None;

        /// <summary>
        /// Gets the first item in a sequence.
        /// </summary>
        /// <returns>Some(item) if the sequence is not empty, None if it is.</returns>
        /// <param name="seq">Seq.</param>
        /// <typeparam name="T">The list type parameter.</typeparam>
        public static Option<T> FirstOption<T>(this IEnumerable<T> seq)
        {
            try
            {
                return Option<T>.Some(seq.First());
            }
            catch (InvalidOperationException)
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Gets the last item in a sequence.
        /// </summary>
        /// <returns>Some(item) if the sequence is not empty, None if it is.</returns>
        /// <param name="seq">Seq.</param>
        /// <typeparam name="T">The list type parameter.</typeparam>
        public static Option<T> LastOption<T>(this IEnumerable<T> seq)
        {
            try
            {
                return Option<T>.Some(seq.Last());
            }
            catch (InvalidOperationException)
            {
                return Option<T>.None;
            }
        }

        /// <summary>
        /// Find an item in a sequence.
        /// </summary>
        /// <returns>Some(item) if found, None if not found.</returns>
        /// <param name="seq">The sequence to search.</param>
        /// <param name="f">Selector function to find the item.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static Option<T> FindOption<T>(this IEnumerable<T> seq, Func<T, bool> f)
        {
            try
            {
                var found = seq.Where(f);
                return Option<T>.Some(found.First());
            }
            catch (InvalidOperationException)
            {
                return Option<T>.None;
            }
        }

    }
}
