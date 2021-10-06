using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.Lookups;

namespace Common.Services
{
    public class SegmentProcessor
    {
        public static List<BaseSegment> GenerateAllSegments(List<RawSegment> segmentList)
        {
            var segments = new List<BaseSegment>();
            foreach(var segment in segmentList)
                segments.Add(
                    GenerateSegment(segment)
                );
            return segments;
        }

        private static BaseSegment GenerateSegment(RawSegment rawStringSegment)
        {
            BaseSegment segmentInstance = null;
            Type segmentTypeDefinition = null;
            var rawSegment = rawStringSegment.SegmentString;

            switch (rawStringSegment.SegmentType)
            {
                case SegmentType.Header: segmentInstance = new HeaderSegment();
                                         segmentTypeDefinition = typeof(HeaderFieldDefinition);
                                         break;
                case SegmentType.CustomerRemarks: segmentInstance = new CustomerRemarkSegment();
                                         segmentTypeDefinition = typeof(CustomerRemarkFieldDefinition);
                                         break;
                case SegmentType.Passenger: segmentInstance = new PassengerSegment();
                                         segmentTypeDefinition = typeof(PassengerFieldDefinition);
                                         break;
                case SegmentType.FareValue: segmentInstance = new FareValueSegment();
                                         segmentTypeDefinition = typeof(FareValueFieldDefinition);
                                         break;
            }

            var previousSegmentPropIsNotPresent = false;
            var rawSegmentLastPosition = 0;
            var segmentProperties = PropertiesService.GetDynamicProperties(segmentInstance);
            var segmentDMLPropertyValues = PropertiesService.GetDynamicPropertyValues(segmentTypeDefinition);
            foreach(var segmentProperty in segmentProperties)
            {
                if(previousSegmentPropIsNotPresent)
                {
                    previousSegmentPropIsNotPresent = false;
                    continue;
                }

                var segmentDMLPropertyValue = segmentDMLPropertyValues.FirstOrDefault(pv => pv.Name == segmentProperty.Name);
                if (segmentDMLPropertyValue != null)
                {
                    var segmentPropValue = "";
                    var fieldDefinition = segmentDMLPropertyValue.Value as FieldDefinition;

                    var rawSegmentStartPosition = fieldDefinition.IsOptionalField ? rawSegmentLastPosition : fieldDefinition.SegmentPosition;
                    rawSegmentLastPosition = rawSegmentStartPosition + fieldDefinition.Length;

                    if (rawSegment.Length >= rawSegmentLastPosition)
                    {
                        var propValue = rawSegment.Substring(rawSegmentStartPosition, fieldDefinition.Length);
                        if(fieldDefinition.IsOptionalField)
                        {
                            if (fieldDefinition.HasOptionalCodeId)
                            {
                                if (string.IsNullOrEmpty(fieldDefinition.OptionalCodeId) || propValue != fieldDefinition.OptionalCodeId)
                                {
                                    rawSegmentLastPosition = rawSegmentStartPosition;
                                    previousSegmentPropIsNotPresent = true;
                                    continue;
                                } 
                            }
                        }

                        segmentPropValue = propValue;
                        if (segmentProperty.CanWrite)
                            PropertiesService.SetPropertyValue(segmentInstance, segmentProperty.Name, segmentPropValue);
                    }
                }
            }
            return segmentInstance;
        }
    }
}
