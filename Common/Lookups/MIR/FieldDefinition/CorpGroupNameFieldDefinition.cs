using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A00 Customer remark section
    /// </summary>
    public static class CorpGroupNameFieldDefinition
    {
        public static FieldDefinition A01SEC =>
            new FieldDefinition
            {
                Name = "A01SEC",
                SegmentPosition = 0,
                Length = 3,
            };
        public static FieldDefinition A01CPI =>
            new FieldDefinition
            {
                Name = "A01CPI",
                SegmentPosition = 3,
                Length = 33,
            };
        public static FieldDefinition A01C01 =>
            new FieldDefinition
            {
                Name = "A01C01",
                SegmentPosition = 36,
                Length = 1,
            };
        public static FieldDefinition A01C02 =>
            new FieldDefinition
            {
                Name = "A01C02",
                SegmentPosition = 37,
                Length = 1,
            };
        public static int CarriageReturnNumber => 2;
    }
}
