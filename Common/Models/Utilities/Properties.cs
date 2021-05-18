using System;

namespace Common.Models
{
    public class Property
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool CanWrite { get; set; }
    }

    public class PropertyValue : Property
    {
        public dynamic Value { get; set; }
    }
}
