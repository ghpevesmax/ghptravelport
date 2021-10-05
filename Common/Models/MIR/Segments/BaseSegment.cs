using Common.Lookups;
using Common.Services;
using System;
using System.Text;

namespace Common.Models
{
    public class BaseSegment
    {
        public SegmentType Type { get; set; }
        override public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            var propertyValues = PropertiesService.GetDynamicPropertyValues(this);
            foreach (var propValue in propertyValues)
            {
                var propVal = propValue.Name == "Type" ? Enum.GetName(typeof(SegmentType), propValue.Value) : (string)propValue.Value;
                sb.AppendLine(string.Format("{0}: {1}", propValue.Name, propVal));
            }
            return sb.ToString();
        }
    }
}
