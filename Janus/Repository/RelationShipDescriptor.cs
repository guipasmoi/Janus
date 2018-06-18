namespace Janus
{
    public class RelationShipDescriptor
    {
        public string Name { get; set; }
        
        public override bool Equals(object obj)
        {
            return Name.Equals((obj as RelationShipDescriptor)?.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}