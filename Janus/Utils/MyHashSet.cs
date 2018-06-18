using System;
using System.Collections.Generic;
using System.Text;

namespace Janus.Utils
{
    /// <summary>
    /// custom HasSet: add exisisting item update it 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MyHashSet<T>:HashSet<T>
    {
        public MyHashSet() : base()
        {

        }
        public MyHashSet(IEqualityComparer<T> comparer) :base(comparer)
        {

        }
        public new bool Add(T item)
        {
            if (this.Contains(item))
            {
                Remove(item);
            }
            return base.Add(item);
        }
    }
}
