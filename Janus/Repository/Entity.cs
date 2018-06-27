using System.Collections.Generic;
using System.Linq;

namespace Janus
{
    public class Entity
    {
        public string Type { get; set; }
        public string Id { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            return Id.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}