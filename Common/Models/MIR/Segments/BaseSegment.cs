using Common.Lookups;
using Common.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class BaseSegment
    {
        public SegmentType Type { get; set; }
        override public string ToString()
        {
            StringBuilder sb = new();
            var propertyValues = PropertiesService.GetDynamicPropertyValues(this);
            foreach (var propValue in propertyValues)
            {
                dynamic propVal;
                if(propValue.Name == "Type")
                {
                    propVal = Enum.GetName(typeof(SegmentType), propValue.Value);
                }
                else if(propValue.Value is List<string>)
                {
                    foreach(var str in propValue.Value as List<string>)
                    {
                        AppendLine(sb, str, propValue.Name);
                    }
                }
                else
                {
                    propVal = (string)propValue.Value;
                    AppendLine(sb, propVal, propValue.Name);
                }
            }
            return sb.ToString();
        }

        internal static void AppendLine(StringBuilder sb, string propVal, string propValName)
        {
            sb.AppendLine($"{propValName}: {propVal}");
        }
    }
}
