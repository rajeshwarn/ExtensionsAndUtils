using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey> (this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }

        public static T FindByIndex<T>(this IEnumerable<T> collection, int index)
        {
            return collection.Skip(index).Take(1).FirstOrDefault();
        }

        public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (predicate == null) throw new ArgumentNullException("predicate");

            var i = 0;
            foreach (var item in source)
            {
                if (predicate(item)) return i;
                i++;
            }

            return -1;
        }

        public static bool Non<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T> list)
        {
            return list != null && list.Any();
        }

        public static bool SafeAny<T>(this IEnumerable<T> list, Func<T,bool> predicate)
        {
            return list != null && list.Any(predicate);
        }
    }
}