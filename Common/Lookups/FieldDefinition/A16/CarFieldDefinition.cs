using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A00 Customer remark section
    /// </summary>
    public static class CarFieldDefinition
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
        public static FieldDefinition A16CAR =>
            new()
            {
                Name = "A16CAR",
                SegmentPosition = 13,
                Length = 12,
            };
        public static FieldDefinition A16OD =>
            new()
            {
                Name = "A16OD",
                IsOptionalField = true,
                CodeId = "OD-",
                SegmentPosition = 139,
                Length = 3,
                NestedFields = new FieldDefinition[]
                {
                    new(){ Name = "A16RG", IsOptionalField = true,
                        CodeId = "RG-", Delimitator = "/",
                        NestedDelimitator = "-",
                        CropIndex = 2
                    }
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
