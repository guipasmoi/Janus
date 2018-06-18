using Janus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Janus.Utils
{
    public class Fusion
    {
        public static Diff FusionDiff(params Diff[] diffs)
        {
            return diffs.Aggregate(new Diff(), (d1, d2) =>
             {
                 var result = new Diff();
                 result.Entities.UnionWith(d1.Entities);
                 result.Entities.UnionWith(d2.Entities);
                 result.Values.UnionWith(d1.Values);
                 result.Values.UnionWith(d2.Values);
                 result.OwnerShips.UnionWith(d1.OwnerShips);
                 result.OwnerShips.UnionWith(d2.OwnerShips);
                 result.RelationShips.UnionWith(d1.RelationShips);
                 result.RelationShips.UnionWith(d2.RelationShips);
                 return result;
             });
        }

        public static IEnumerable<NodeType> GetPathToRoot<NodeType>(ISet<RelationShip<NodeType, NodeType>> relations, NodeType node)
        {
            var result = new List<NodeType>();
            NodeType current = node;
            while (current != null)
            {
                yield return current;
                var relationship = relations.FirstOrDefault(r => r.Source.Equals(current));
                current = (relationship != null) ? relationship.Target: default(NodeType);
            };
          
        }
    }
}
