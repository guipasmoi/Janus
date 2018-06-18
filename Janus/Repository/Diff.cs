using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Janus.utils;
using Janus.Utils;

namespace Janus
{
    public partial class Diff
    {
        public string Hash { get; set; }
        public string Comment { get; set; }
        public HashSet<Entity> Entities { get; }
        public HashSet<Value> Values { get; }
        public MyHashSet<RelationShip<Value, Entity, RelationShipDescriptor>> OwnerShips { get; }
        public MyHashSet<RelationShip<Entity, Entity, RelationShipDescriptor>> RelationShips { get; }

        public Diff(string comment = "")
        {
            Hash = Guid.NewGuid().ToString();
            Comment = comment;
            Entities = new HashSet<Entity>();
            Values = new HashSet<Value>();
            OwnerShips = new MyHashSet<RelationShip<Value, Entity, RelationShipDescriptor>>(
               new CustomComparer<Value, Entity, RelationShipDescriptor>(false, true, true)
            );
            RelationShips = new MyHashSet<RelationShip<Entity, Entity, RelationShipDescriptor>>(
                new CustomComparer<Entity, Entity, RelationShipDescriptor>(true, false, true)
            );
        }

        public override bool Equals(object obj)
        {
            return Hash.Equals((obj as Diff)?.Hash);
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode();
        }
    }
}
