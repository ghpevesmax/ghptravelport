using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.Lookups;
using Common.Utils;

namespace Common.Services
{
    public class SegmentProcessor
    {
        public static List<BaseSegment> GenerateAllSegments(List<RawSegment> segmentList)
        {
            var baseSegments = new List<BaseSegment>();
            var a14FTSegments = segmentList
                .Where(s => s.SegmentType == SegmentType.A14FT)
                .ToList();
            var generalSegments = segmentList
                .Where(_ => _.SegmentType != SegmentType.A14FT);

            foreach (var segment in generalSegments)
            {
                var generalSegment = GenerateGeneralSegment(segment);
                baseSegments.Add(generalSegment);
            }

            var a14FTSegment = GenerateA14FTSegment(a14FTSegments);
            if (a14FTSegment != null)
            {
                baseSegments.Add(a14FTSegment);
            }

            return baseSegments;
        }

        private static BaseSegment GenerateGeneralSegment(RawSegment rawStringSegment)
        {
            BaseSegment segmentInstance = null;
            Type segmentTypeDefinition = null;
            var rawSegment = rawStringSegment.SegmentString;

            switch (rawStringSegment.SegmentType)
            {
                case SegmentType.Header: segmentInstance = new HeaderSegment();
                                         segmentTypeDefinition = typeof(HeaderFieldDefinition);
                                         break;
                case SegmentType.CustomerRemarks: 
                                         segmentInstance = new CustomerRemarkSegment();
                                         segmentTypeDefinition = typeof(CustomerRemarkFieldDefinition);
                                         break;
                case SegmentType.Passenger: 
                                         segmentInstance = new PassengerSegment();
                                         segmentTypeDefinition = typeof(PassengerFieldDefinition);
                                         break;
                case SegmentType.FareValue: 
                                         segmentInstance = new FareValueSegment();
                                         segmentTypeDefinition = typeof(FareValueFieldDefinition);
                                         break;
                case SegmentType.A16Hotel: 
                                         segmentInstance = new A16HotelSegment();
                                         segmentTypeDefinition = typeof(HotelFieldDefinition);
                                         break;
                case SegmentType.A16Car: 
                                         segmentInstance = new A16CarSegment();
                                         segmentTypeDefinition = typeof(CarFieldDefinition);
                                         break;
                default: return null;
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

                var parentSegmentPropName = segmentProperty.Name.Contains("_") ? segmentProperty.Name.Split("_").First() : segmentProperty.Name;
                var childSegmentPropName = segmentProperty.Name.Contains("_") ? segmentProperty.Name.Split("_").Last() : string.Empty;

                var dmlPropValue = segmentDMLPropertyValues.FirstOrDefault(pv => pv.Name.StartsWith(parentSegmentPropName));
                if (dmlPropValue != null)
                {
                    var segmentPropValue = "";
                    var fieldDefinition = dmlPropValue.Value as FieldDefinition;

                    if (fieldDefinition.HasNestedFields && !string.IsNullOrEmpty(childSegmentPropName))
                    {
                        var childFieldDefinition = fieldDefinition.NestedFields.FirstOrDefault(_ => _.Name == childSegmentPropName);
                        if (childFieldDefinition != null)
                        {
                            fieldDefinition = childFieldDefinition;
                        }
                        else
                        {
                            continue;
                        }
                    }

                    if(fieldDefinition.IsComplexField && fieldDefinition.HasCodeId)
                    {
                        var segmentChunks = rawSegment.Split("|");
                        var complexField = string.Empty;

                        if (fieldDefinition.IsOptionalContained)
                        {
                            complexField = segmentChunks
                                .FirstOrDefault(_ => _
                                    .StartsWith(fieldDefinition.CodeId)
                                );

                            if (!complexField.IsNullOrEmpty())
                            {
                                segmentPropValue = complexField
                                    .Replace(fieldDefinition.CodeId, string.Empty)
                                    .Substring(fieldDefinition.SegmentPosition);
                            }
                        }
                        else
                        {
                            foreach (var chunk in segmentChunks)
                            {
                                var optionalDelimitedChunks = chunk.Split(fieldDefinition.Delimitator);
                                complexField = optionalDelimitedChunks
                                    .FirstOrDefault(_ => _
                                        .Contains(fieldDefinition.CodeId)
                                    );

                                if (!complexField.IsNullOrEmpty())
                                {
                                    complexField = complexField
                                        .Replace(fieldDefinition.CodeId, string.Empty);
                                    
                                    if (fieldDefinition.IsNestedDelimitatorDriven)
                                    {
                                        complexField = complexField.Split(fieldDefinition.NestedDelimitator).First();
                                        complexField = complexField.Substring(0, complexField.Length - fieldDefinition.CropIndex);
                                    }

                                    segmentPropValue = complexField;
                                    break;
                                }
                            }

                        }                    
                    }
                    else
                    {
                        var rawSegmentStartPosition = GetRawSegmentStartPosition(fieldDefinition, rawSegmentLastPosition);
                        rawSegmentLastPosition = rawSegmentStartPosition + fieldDefinition.Length;

                        if (rawSegment.Length >= rawSegmentLastPosition)
                        {
                            var propValue = rawSegment.Substring(rawSegmentStartPosition, fieldDefinition.Length);
                            if (fieldDefinition.IsOptionalField)
                            {
                                if (fieldDefinition.HasCodeId)
                                {
                                    if (string.IsNullOrEmpty(fieldDefinition.CodeId) || propValue != fieldDefinition.CodeId)
                                    {
                                        rawSegmentLastPosition = rawSegmentStartPosition;
                                        previousSegmentPropIsNotPresent = true;
                                        continue;
                                    }
                                }
                            }

                            segmentPropValue = propValue;
                        } 
                    }

                    SetPropertyValue(segmentInstance, segmentProperty, segmentPropValue);
                }
            }
            return segmentInstance;
        }

        private static A14FTSegment GenerateA14FTSegment(IList<RawSegment> rawSegments)
        {
            if (rawSegments != null && rawSegments.Any(_ => _.SegmentType != SegmentType.A14FT))
            {
                return null;
            }

            var a14ftSegment = new A14FTSegment();
            foreach (var rawSegment in rawSegments)
            {
                MapperService.MapToSegment(a14ftSegment, rawSegment);
            }
            MapperService.OrderSubSegments(a14ftSegment);

            return a14ftSegment;
        }

        public static SegmentType GetSegmentType(string line)
        {
            var segmentType = SegmentType.None;

            if (line.StartsWith(SegmentIdentifier.Header))
            {
                segmentType = SegmentType.Header;
            }

            if (line.StartsWith(SegmentIdentifier.CustomerRemarks))
            {
                segmentType = SegmentType.CustomerRemarks;
            }

            if (line.StartsWith(SegmentIdentifier.Passenger))
            {
                segmentType = SegmentType.Passenger;
            }

            if (line.StartsWith(SegmentIdentifier.FareValue))
            {
                segmentType = SegmentType.FareValue;
            }

            if (line.StartsWith(SegmentIdentifier.A14FT))
            {
                segmentType = SegmentType.A14FT;
            }

            if (line.StartsWith(SegmentIdentifier.A16Hotel))
            {
                segmentType = SegmentType.A16Hotel;
            }

            if (line.StartsWith(SegmentIdentifier.A16Car))
            {
                segmentType = SegmentType.A16Car;
            }

            return segmentType;
        }

        internal static int GetRawSegmentStartPosition(FieldDefinition fieldDefinition, int lastPosition)
        {
            if(fieldDefinition.IsOptionalField && fieldDefinition.SegmentPosition == default)
            {
                return lastPosition;
            }
            else
            {
                return fieldDefinition.SegmentPosition;
            }
        }

        internal static void SetPropertyValue(BaseSegment segmentInstance, Property segmentProperty, string segmentPropValue)
        {
            if (segmentProperty.CanWrite)
                PropertiesService.SetPropertyValue(segmentInstance, segmentProperty.Name, segmentPropValue);
        }
    }
}
