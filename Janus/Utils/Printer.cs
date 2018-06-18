using Janus;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Janus.Utils
{
    public static class Printer
    {
        public static string Print<TSource, TTarget>(this RelationShip<TSource, TTarget> relation)
        {
            return $"{relation.Source} -> {relation.Target}";
        }

        public static string Print(this Diff diff)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"-----------------------------------------------------------");
            sb.AppendLine($"Diff: {diff.Hash}");
            sb.AppendLine($"  -Comment: {diff.Comment}");
            //sb.AppendLine($"  -Entities:");
            //sb.AppendLine($"      * {Print(diff.Entities)}");
            //sb.AppendLine($"  -Values:");
            //sb.AppendLine($"      * {Print(diff.Values)}");

            foreach (var e in diff.Entities)
            {
                sb.AppendLine($"     *{e.Id} {e.Type}");
                foreach (var r in diff.OwnerShips.Where(o => o.Target == e))
                {
                    sb.AppendLine($"       -{r.Value.Name} {r.Source.Data}");
                }
            }
            sb.AppendLine($"  -Relation:");
            foreach (var e in diff.RelationShips)
            {
                sb.AppendLine($"     *{e.Source.Id} --({e.Value.Name})-->{e.Target.Id}");
            }
            sb.AppendLine($"-----------------------------------------------------------");
            var res = sb.ToString();
            Console.WriteLine(res);
            return res;
        }

        private static string Print(HashSet<Entity> entities)
        {
            return String.Join(",", entities.Select(e=>e.Id));
        }

        private static string Print(HashSet<Value> values)
        {
            return String.Join(",", values.Select(e => e.Data));
        }
    }
}
