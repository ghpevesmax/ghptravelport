using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A16 Customer remark section
    /// </summary>
    public static class HotelFieldDefinition
    {
        public static FieldDefinition A16SEC =>
            new()
            {
                Name = "A16SEC",
                SegmentPosition = 0,
                Length = 3,
            };
        public static FieldDefinition A16TYP =>
            new()
            {
                Name = "A16TYP",
                SegmentPosition = 3,
                Length = 1,
            };
        public static FieldDefinition A16NME =>
            new()
            {
                Name = "A16NME",
                SegmentPosition = 36,
                Length = 20,
            };
        public static FieldDefinition A16OD =>
            new()
            {
                Name = "A16OD",
                IsOptionalField = true,
                CodeId = "OD-",
                SegmentPosition = 120,
                Length = 3,
                NestedFields = new FieldDefinition[]
                {
                    new(){ Name = "A16RG", IsOptionalField = true, CodeId = "RG-", Delimitator = "/" },
                }
            };
        public static FieldDefinition A16CF =>
            new()
            {
                Name = "A16CF",
                IsOptionalContained = true,
                CodeId = "CF:",
                SegmentPosition = 2,
            };
    }
}
