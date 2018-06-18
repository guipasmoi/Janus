using Janus.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Janus.Example.model
{
    [Entity]
    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }
        
        public int NAAAAAAA { get; set; }
        public Dog Friend { get; set; }
        public Dog Dog { get; set; }
    }
}
