using System;
using System.Collections.Generic;
using System.Text;

namespace Janus.utils
{
    public class CustomComparer<TSource, TTarget, TValue> : IEqualityComparer<RelationShip<TSource, TTarget, TValue>>
    {
        private readonly bool checkSource;
        private readonly bool checkTarget;
        private readonly bool checkValue;

        public CustomComparer(bool checkSource, bool checkTarget, bool checkValue)
        {
            this.checkSource = checkSource;
            this.checkTarget = checkTarget;
            this.checkValue = checkValue;
        }

        public bool Equals(RelationShip<TSource, TTarget, TValue> x, RelationShip<TSource, TTarget, TValue> y)
        {
            return ( !this.checkSource || (x.Source.Equals(y.Source))) &&
                   ( !this.checkTarget || (x.Target.Equals(y.Target))) &&
                   ( !this.checkValue || (x.Value.Equals(y.Value)));
        }

        public int GetHashCode(RelationShip<TSource, TTarget, TValue> obj)
        {
            var a = checkSource ? 101 * obj.Source.GetHashCode() : 0;
            var b = checkTarget ? 127 * obj.Target.GetHashCode() : 0;
            var c = checkValue ? 181 * obj.Value.GetHashCode() : 0;
            return a + b + c;
        }
    }
}