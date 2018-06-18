using Janus;
using Janus.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Janus.Test
{
    public class RepositoryTest
    {
        [Fact(DisplayName = "init")]
        public void Test0()
        {
            var repo = new Repository();
            var b = repo.GetState(repo.Current);
            Assert.Empty(b.Entities);
            Assert.Empty(b.Values);
            Assert.Empty(b.OwnerShips);
            Assert.Empty(b.RelationShips);
        }

        [Fact(DisplayName = "commit CreateEntity")]
        public void Test1()
        {
            var repo = new Repository();
            repo.CreateEntity("Type0");
            repo.CreateEntity("Type0");
            repo.CreateEntity("Type0");
            repo.Commit("1st commit");
            var state1 = repo.GetState(repo.Current);
            Assert.Equal(3, state1.Entities.Count);
            Assert.Empty(state1.Values);
            Assert.Empty(state1.OwnerShips);
            Assert.Empty(state1.RelationShips);
            repo.CreateEntity("Type1");
            repo.CreateEntity("Type1");
            repo.Commit("2nd commit");
            var state2 = repo.GetState(repo.Current);
            Assert.Equal(5, state2.Entities.Count);
            Assert.Empty(state2.Values);
            Assert.Empty(state2.OwnerShips);
            Assert.Empty(state2.RelationShips);
        }

        [Fact(DisplayName = "commit CreateValue")]
        public void Test2()
        {
            var repo = new Repository();
            var e1 = repo.CreateEntity("Type0");
            var e2 = repo.CreateEntity("Type0");
            var v1 = repo.CreateValue(e1, new RelationShipDescriptor { Name = "Property1" }, "some data1");
            var v2 = repo.CreateValue(e1, new RelationShipDescriptor { Name = "Property1" }, "some data2");
            var v3 = repo.CreateValue(e1, new RelationShipDescriptor { Name = "Property2" }, "some data3");
            repo.Commit("1st commit");
            var state1 = repo.GetState(repo.Current);
            Assert.Equal(2, state1.Values.Count);
            Assert.Equal("some data2", state1.Values.ElementAt(0).Data);
            Assert.Equal("some data3", state1.Values.ElementAt(1).Data);
            Assert.Equal(2, state1.OwnerShips.Count);
        }

        [Fact(DisplayName = "commit SetRelation")]
        public void Test3()
        {
            var repo = new Repository();
            var e1 = repo.CreateEntity();
            var e2 = repo.CreateEntity();
            var e3 = repo.CreateEntity();
            var e4 = repo.CreateEntity();
            var e5 = repo.CreateEntity();
            var e6 = repo.CreateEntity();

            repo.SetRelation(e1, e1, new RelationShipDescriptor { Name = "Property1" });
            Assert.Single(repo.Staging.RelationShips);
            repo.SetRelation(e1, e2, new RelationShipDescriptor { Name = "Property1" });
            Assert.Single(repo.Staging.RelationShips);
            repo.SetRelation(e3, e4, new RelationShipDescriptor { Name = "Property1" });
            Assert.Equal(2, repo.Staging.RelationShips.Count);
            repo.SetRelation(e3, e4, new RelationShipDescriptor { Name = "Property2" });
            Assert.Equal(3, repo.Staging.RelationShips.Count);

            var unregisteredEntity = new Entity { Id = "992" };
            Assert.Throws<Exception>(() =>
            {
                repo.SetRelation(
                    unregisteredEntity,
                    e4,
                    new RelationShipDescriptor { Name = "Property2" }
                );
            });

            Assert.Throws<Exception>(() =>
            {
                repo.SetRelation(
                    e4,
                    unregisteredEntity,
                    new RelationShipDescriptor { Name = "Property2" }
                );
            });
            Repository.Save(repo);
        }
    }
}
