using System;
using System.Collections.Generic;
using System.Text;

namespace Interface4
{
    interface IGenericFilter<T>
    {
        IEnumerable<T> ApplyFilter(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }

    interface IDummyFilter<T> : IGenericFilter<T>
    {
        IEnumerable<T> IGenericFilter<T>.ApplyFilter(IEnumerable<T> collection, Func<T, bool> predicate)
        {
            return default;
        }
    }

    public class GenericFilterExample : IGenericFilter<int>, IDummyFilter<int>
    {
    }
}
