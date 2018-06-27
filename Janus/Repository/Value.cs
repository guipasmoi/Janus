namespace Janus
{
    public class Value
    {
        public string Type { get; set; }
        public string Id { get; set; }
        public string Data { get; set; }

        // override object.Equals
        public override bool Equals(object obj)
        {
            var v = obj as Value;
            return Data.Equals(v?.Data);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {

            return Data.GetHashCode();
        }
    }
}