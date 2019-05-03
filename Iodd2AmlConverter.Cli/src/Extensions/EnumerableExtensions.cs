using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Iodd2AmlConverter.Cli.Extensions
{
    
    /// <summary>
    /// Extension methods for enumerable types.
    /// </summary>
    public static class EnumerableExtensions
    {

        /// <summary>
        /// Returns the enumerable as array.
        /// </summary>
        /// <typeparam name="T">The type which is hold by the enumerable.</typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns>The enumerable as array.</returns>
        public static IEnumerable<T> Memorize<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.GetType().IsArray ? enumerable : enumerable.ToArray();
        }

        /// <summary>
        /// Maps a function on a pairwise iteration over an enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type which is hold by the source enumerable.</typeparam>
        /// <typeparam name="TResult">The type of the enumerable which is returned by the function.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="callbackFunc">The mapping function.</param>
        /// <returns>The result enumerable after the callback function was mapped on the enumerable elements.</returns>
        public static IEnumerable<TResult> Pairwise<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> callbackFunc)
        {
            if(source == null)
                throw new ArgumentNullException(nameof(source));

            if(callbackFunc == null)
                throw new ArgumentNullException(nameof(callbackFunc));

            return PairwiseImpl(source, callbackFunc);
        }

        /// <summary>
        /// Maps a function on a pairwise iteration over an enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type which is hold by the source enumerable.</typeparam>
        /// <typeparam name="TResult">The type of the enumerable which is returned by the function.</typeparam>
        /// <param name="source">The source enumerable.</param>
        /// <param name="callbackFunc">The mapping function.</param>
        /// <returns>The result enumerable after the callback function was mapped on the enumerable elements.</returns>
        private static IEnumerable<TResult> PairwiseImpl<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TSource, TResult> callbackFunc)
        {
            Debug.Assert(source != null);
            Debug.Assert(callbackFunc != null);

            using (var enumerator = source.GetEnumerator())
            {
                if(!enumerator.MoveNext())
                    yield break;

                var previous = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    yield return callbackFunc(previous, enumerator.Current);
                    previous = enumerator.Current;
                }
            }
        }

    }
    
}