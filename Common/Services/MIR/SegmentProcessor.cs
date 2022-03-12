﻿using System;
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
            var baseSegments = new List<BaseSegment>();
            var generalSegments = segmentList
                .Where(s => s.SegmentType != SegmentType.A14FT);
            var a14FTSegments = segmentList
                .Where(s => s.SegmentType == SegmentType.A14FT)
                .ToList();

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
                case SegmentType.CustomerRemarks: segmentInstance = new CustomerRemarkSegment();
                                         segmentTypeDefinition = typeof(CustomerRemarkFieldDefinition);
                                         break;
                case SegmentType.Passenger: segmentInstance = new PassengerSegment();
                                         segmentTypeDefinition = typeof(PassengerFieldDefinition);
                                         break;
                case SegmentType.FareValue: segmentInstance = new FareValueSegment();
                                         segmentTypeDefinition = typeof(FareValueFieldDefinition);
                                         break;
                case SegmentType.A14FT: return null;
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
            SegmentType segmentType = SegmentType.None;
            if (line.StartsWith("T5"))
            {
                segmentType = SegmentType.Header;
            }

            if (line.StartsWith("A00"))
            {
                segmentType = SegmentType.CustomerRemarks;
            }

            if (line.StartsWith("A02"))
            {
                segmentType = SegmentType.Passenger;
            }

            if (line.StartsWith("A07"))
            {
                segmentType = SegmentType.FareValue;
            }

            if (line.StartsWith("A14FT"))
            {
                segmentType = SegmentType.A14FT;
            }

            return segmentType;
        }
    }
}
