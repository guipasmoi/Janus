using Janus;
using Janus.Utils;
using System;
using System.Linq;
using Xunit;

namespace Janus.Test
{
    public class DiffTest
    {
        [Fact(DisplayName = "Diff are unique")]
        public void Test0()
        {
            var uniqueDiffList = Enumerable.Range(1, 50).Select(i => new Diff($"diff number {i}"));
            Assert.True(uniqueDiffList.Count() == 50, "it should create unique Diff");
            var a = new Diff { Hash = "22" };
            var b = new Diff { Hash = "22" };
            Assert.Equal(a, b);
            Assert.True(false,"because fuck u");
        }

        [Fact(DisplayName = "Relation ships between entities")]
        public void Test1()
        {
            Diff d = new Diff();
            Assert.True(d.RelationShips.Count == 0, "you must start empty");
            var e1 = new Entity { Id = "0", Type = "Type1" };
            var e2 = new Entity { Id = "1", Type = "Type2" };
            var r1 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
                e1,
                e1,
                new RelationShipDescriptor { Name = "Prop1" }
            );
            var r2 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
                e1,
                e1,
                new RelationShipDescriptor { Name = "Prop1" }
            );
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r2);
            var a1 = d.Print();
            Assert.True(d.RelationShips.Count == 1, "you cant add the the relationship twice");
            var r3 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
               e1,
               e2,
               new RelationShipDescriptor { Name = "Prop1" }
           );
            d.RelationShips.Add(r3);
          
            Assert.True(d.RelationShips.Count == 1, "you cant add the the relationship twice");
            Assert.True(d.RelationShips.First().Target == e2, "the relation ship should be updated");
            var r4 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
              e1,
              e2,
              new RelationShipDescriptor { Name = "Prop2" }
            );
            d.RelationShips.Add(r4);
            var a2 = d.Print();
            Assert.True(d.RelationShips.Count == 2, "the relationship should be added");

        }

        [Fact(DisplayName = "OwnerShip of Value")]
        public void Test2()
        {
            Diff d = new Diff();
            var e = new Entity { Id = "0", Type = "AType" };
            d.Entities.Add(e);
            var r1 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
                e,
                e,
                new RelationShipDescriptor { Name = "Self" }
            );
            var r2 = new RelationShip<Entity, Entity, RelationShipDescriptor>(
                e,
                e,
                new RelationShipDescriptor { Name = "Toto" }
            );
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r1);
            d.RelationShips.Add(r2);
            var v1 = new Value { Id = "0", Type = "int", Data = "42" };
            var v2 = new Value { Id = "1", Type = "int", Data = "007" };
            var o = new RelationShip<Value, Entity, RelationShipDescriptor>(v1, e,
                new RelationShipDescriptor { Name = "MyProperty" });
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(o);
            d.OwnerShips.Add(new RelationShip<Value, Entity, RelationShipDescriptor>(v2, e,
                new RelationShipDescriptor { Name = "MyProperty2" }));
        }
    }
}