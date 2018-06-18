using System;
using System.Collections.Generic;
using System.Text;

namespace Janus.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class IgnoreEntityAttributeAttribute : Attribute
    {

    }
}
