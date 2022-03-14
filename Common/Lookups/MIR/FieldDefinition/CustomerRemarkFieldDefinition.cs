using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A00 Customer remark section
    /// </summary>
    public static class CustomerRemarkFieldDefinition
    {
        public static FieldDefinition A00SEC =>
            new FieldDefinition
            {
                Name = "A00SEC",
                SegmentPosition = 0,
                Length = 3,
            };
        public static FieldDefinition A00CUS =>
            new FieldDefinition
            {
                Name = "A00CUS",
                SegmentPosition = 3,
                Length = 43,
                IsVariableLength = true,
            };
        public static FieldDefinition A00C01 =>
            new FieldDefinition
            {
                Name = "A00C01",
                SegmentPosition = 46,
                Length = 1,
            };
        public static FieldDefinition A00C02 =>
            new FieldDefinition
            {
                Name = "A00C02",
                SegmentPosition = 47,
                Length = 1,
            };
        public static int CarriageReturnNumber => 2;
    }
}
