using System;
using System.Collections.Generic;

namespace Airtable.ApiClient.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<List<T>> Split<T>(this List<T> list, int chunkSize)
        {
            if (chunkSize < 1)
                throw new ArgumentOutOfRangeException(nameof(chunkSize), "chunkSize must be greater than 0");

            return SplitIterator();

            IEnumerable<List<T>> SplitIterator()
            {
                if (list.Count <= chunkSize)
                {
                    yield return list;
                    yield break;
                }

                for (int i = 0; i < list.Count; i += chunkSize)
                {
                    yield return list.GetRange(i, Math.Min(chunkSize, list.Count - 1));
                }
            }
        }
    }
}
