using Common.Models;
using System;

namespace Common.Lookups
{
    /// <summary>
    /// Represents the A00 Customer remark section
    /// </summary>
    public static class PassengerFieldDefinition
    {
        public static FieldDefinition A02SEC =>
            new()
            {
                Name = "A02SEC",
                SegmentPosition = 0,
                Length = 3,
            };
        public static FieldDefinition A02NME =>
            new()
            {
                Name = "A02NME",
                SegmentPosition = 3,
                Length = 33,
            };
        public static FieldDefinition A02TRN =>
            new()
            {
                Name = "A02TRN",
                SegmentPosition = 36,
                Length = 11,
            };
        public static FieldDefinition A02TIN =>
            new()
            {
                Name = "A02TIN",
                SegmentPosition = 47,
                Length = 22,
                NestedFields = new FieldDefinition[4]
                {
                    new(){ Name = "A02YIN", SegmentPosition = 47, Length = 1 },
                    new(){ Name = "A02TKT", SegmentPosition = 48, Length = 10 },
                    new(){ Name = "A02NBK", SegmentPosition = 58, Length = 2 },
                    new(){ Name = "A02INV", SegmentPosition = 60, Length = 9 },
                }
            };
        public static int CarriageReturnNumber => 3;
    }
}
