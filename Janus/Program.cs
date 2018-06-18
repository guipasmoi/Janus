using Janus;
using Janus.Utils;
using System;

namespace Janus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Diff d = new Diff();
            var e1 = new Entity { Id = "0", Type = "Type0" };
            var e2 = new Entity { Id = "1", Type = "Type1" };
            d.Entities.Add(e1);
            d.Entities.Add(e2);
            var r1 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
                    e1,
                    e1,
                    new RelationShipDescriptor { Name = "Self" }
                );
            var r2 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
        e1,
        e1,
        new RelationShipDescriptor { Name = "Toto" }
    );
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r2);
            var v1 = new Value { Id = "0", Type = "int", Data = "42" };
            var v2 = new Value { Id = "1", Type = "int", Data = "007" };
            d.Values.Add(v1);
            d.Values.Add(v2);
            var o = new RelationShip<Value, Entity, RelationShipDescriptor>(v1, e1, new RelationShipDescriptor { Name = "MyProperty" });
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(new RelationShip<Value, Entity, RelationShipDescriptor>(v2, e1, new RelationShipDescriptor { Name = "MyProperty2" }));

            d.Print();
            Console.ReadLine();

        }
    }
}
