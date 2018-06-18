using Janus;
using Janus.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Janus
{
    public class Repository
    {
        public static Repository GlobalRepository = new Repository();

        public Diff Current { get; private set; }
        public Diff Staging { get; private set; }

        private HashSet<Diff> history = new HashSet<Diff> { new Diff() };
        private HashSet<RelationShip<Diff, Diff>> Relations = new HashSet<RelationShip<Diff, Diff>>();
        // add Tags
        // add branches

        public Repository()
        {
            Current = new Diff();
            Staging = new Diff();
            history = new HashSet<Diff> { Current };
        }

        public static void Save(Repository repo, string path= @"C:\Users\guipa\Desktop\repo.json")
        {
            string output = JsonConvert.SerializeObject(repo);
            File.WriteAllText(path, output);     
        }

        public static Repository Load(string path) {
            var output =File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Repository>(output);
        }

        public Diff Commit(string comment)
        {
            var newDiff = new Diff();
            Staging.Comment = comment;
            RemoveOrphanValues(Staging);
            var result = Staging.Print();
            history.Add(Staging);
            Current = Staging;
            Relations.Add(new RelationShip<Diff, Diff>(newDiff, Current));
            Staging = newDiff;
            return Current;
        }

        public void Rebase(Diff from, Diff onto)
        {
            throw new NotImplementedException();
        }

        public void Merge(Diff from, Diff onto, string comment)
        {
            throw new NotImplementedException();
        }

        private void RemoveOrphanValues(Diff diff)
        {
            diff.Values.RemoveWhere(v => diff.OwnerShips.FirstOrDefault(o => o.Source == v) == null);
        }

        int _idIndex = 0;
        int _idValue = 0;

        public Entity CreateEntity(String type = null)
        {
            var entity = new Entity { Id = (_idIndex++).ToString(), Type = type };
            Staging.Entities.Add(entity);
            return entity;
        }

        public Value CreateValue(Entity entity, RelationShipDescriptor property, string data, string type = "")
        {
            var value = new Value { Data = data, Id = (_idValue++).ToString() };
            Staging.OwnerShips.Add(
                new RelationShip<Value, Entity, RelationShipDescriptor>(
                    value,
                    entity,
                    property
                 )
            );
            Staging.Values.Add(value);
            return value;
        }

        public Diff GetState(Diff diff = null)
        {
            var path = Fusion.GetPathToRoot<Diff>(Relations, diff ?? Staging);
            return Fusion.FusionDiff(path.ToArray());
        }

        public void SetRelation(Entity source, Entity target, RelationShipDescriptor property)
        {
            // Fixme optim:
            var state = GetState(Staging);
            if (!state.Entities.Any(e => e == source))
            {
                throw new Exception($"missing entity {source.Id}");
            }
            if(!state.Entities.Any(e => e == target)){
                throw new Exception($"missing entity {target.Id}");
            }
            Staging.RelationShips.Add(new RelationShip<Entity, Entity, RelationShipDescriptor>(source, target, property));
        }
    }
}
