namespace Janus
{
    public class RelationShip<TSource, TTarget>
    {
        public TSource Source { get; }
        public TTarget Target { get; }

        public RelationShip(TSource source, TTarget target)
        {
            Source = source;
            Target = target;
        }
    }

    public class RelationShip<TSource, TTarget, TValue> : RelationShip<TSource,TTarget>
    {
        public
            RelationShip(TSource source, TTarget target, TValue value)
            : base(source, target)
        {
            Value = value;
        }
        public TValue Value { get; }

        public override string ToString()
        {
            return Source.ToString() + " " + Value.ToString();
        }
    }
}