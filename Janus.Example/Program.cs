
using Janus.Attributes;
using Janus.Example.model;
using Janus.Utils;
using System;
using System.Linq;
using System.Reflection;

namespace Janus.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = Repository.GlobalRepository;
            repo.SetState = (diff) =>
            {
                foreach (var entity in diff.Entities)
                {
                    var match = EntityAttribute.map.FirstOrDefault(a => a.Value.Id == entity.Id);
                    if (match.Value == null)
                    {
                        throw new Exception("unexpected");
                    }
                    foreach (var ownershop in diff.OwnerShips.Where(o => o.Target == entity))
                    {
                        var entityType = Type.GetType(match.Value.Type);
                        switch (ownershop.Source.Type)
                        {
                            case "System.String":
                                {
                                    PropertyInfo piShared = entityType.GetProperty(ownershop.Value.Name);
                                    piShared.SetValue(match.Key, ownershop.Source.Data);
                                }
                                break;
                            case "System.Int32":
                                {
                                    PropertyInfo piShared = entityType.GetProperty(ownershop.Value.Name);
                                    piShared.SetValue(match.Key, Int32.Parse(ownershop.Source.Data));
                                }
                                break;
                            default:
                                throw new NotImplementedException();
                        }
                    }

                    foreach (var relationShip in diff.RelationShips.Where(o => o.Target == entity))
                    {
                        var matchTarget = EntityAttribute.map.FirstOrDefault(a => a.Value.Id == relationShip.Source.Id);
                        var entityType = Type.GetType(matchTarget.Value.Type);
                        PropertyInfo piShared = entityType.GetProperty(relationShip.Value.Name);
                        piShared.SetValue(matchTarget.Key, match.Key);
                    }
                }


            };

            Person paul = new Person() { Name = "paul", Age = 22 };
            Person marco = new Person() { Name = "marco", Age = 45 };
            Person polo = new Person() { Name = "polo", Age = 57, Dog = new Dog { Name = "punk" } };
            var diff1 = repo.Commit("feat: my 1st commit");
            paul.Name = "notpaul";
            polo.Age = 23;
            paul.Dog = polo.Dog;
            polo.Dog = new Dog { Name = "graphic" };
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Dog = polo.Dog;
            polo.Friend = polo.Dog;
            var diff2 = repo.Commit("feat: my 2nd commit");
            repo.Checkout(diff1);
            Console.WriteLine("press enter");
            Console.ReadLine();
        }
    }
}
