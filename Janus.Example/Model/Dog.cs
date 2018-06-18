using Janus.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Janus.Example.model
{
    [Entity]
    public class Dog
    {
        public string Name { get; internal set; }
    }
}
