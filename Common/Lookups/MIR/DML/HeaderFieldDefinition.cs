using System;
using Common.Models;

namespace Common.Lookups
{
    public static class HeaderFieldDefinition
    {
        public static FieldDefinition T50BID =>
            new()
            {
                Name = "T50BID",
                SegmentPosition = 0,
                Length = 2,
            };
        public static FieldDefinition T50TRC =>
            new()
            {
                Name = "T50BID",
                SegmentPosition = 2,
                Length = 2,
            };
        public static FieldDefinition T50SPC =>
            new()
            {
                Name = "T50SPC",
                SegmentPosition = 4,
                Length = 4,
            };
        public static FieldDefinition T50MIR =>
            new()
            {
                Name = "T50MIR",
                SegmentPosition = 8,
                Length = 2,
            };
        public static FieldDefinition T50SZE =>
            new()
            {
                Name = "T50SZE",
                SegmentPosition = 10,
                Length = 5,
            };
        public static FieldDefinition T50SEQ =>
            new()
            {
                Name = "T50SEQ",
                SegmentPosition = 15,
                Length = 5,
            };
        public static FieldDefinition T50CRE =>
            new()
            {
                Name = "T50CRE",
                SegmentPosition = 20,
                Length = 12,
                NestedFields = new FieldDefinition[2]{
                    new FieldDefinition{
                        Name = "T50DTE",
                        SegmentPosition = 20,
                        Length = 7,
                    },
                    new FieldDefinition{
                        Name = "T50TME",
                        SegmentPosition = 27,
                        Length = 5,
                    },
                }
            };
        public static FieldDefinition T50ISS =>
                new()
                {
                    Name = "T50ISS",
                    SegmentPosition = 32,
                    Length = 29,
                    NestedFields = new FieldDefinition[3]{
                        new()
                        {
                            Name = "T50ISC",
                            SegmentPosition = 32,
                            Length = 2,
                        },
                        new(){
                            Name = "T50ISA",
                            SegmentPosition = 34,
                            Length = 3,
                        },
                        new(){
                            Name = "T50ISN",
                            SegmentPosition = 37,
                            Length = 24,
                        },
                    }
                };
        public static FieldDefinition T50DFT =>
                new()
                {
                    Name = "T50DFT",
                    SegmentPosition = 61,
                    Length = 7,
                };
        public static FieldDefinition T50LNI =>
            new()
            {
                Name = "T50LNI",
                SegmentPosition = 68,
                Length = 12,
                NestedFields = new FieldDefinition[2] {
                    new() {
                        Name = "T50INP",
                        SegmentPosition = 68,
                        Length = 6,
                    },
                    new() {
                        Name = "T50OUT",
                        SegmentPosition = 74,
                        Length = 6,
                    },
                }
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50CO1 =>
                new()
                {
                    Name = "T50CO1",
                    SegmentPosition = 80,
                    Length = 1,
                };
        #endregion
        public static FieldDefinition T50BPC =>
            new()
            {
                Name = "T50BPC",
                SegmentPosition = 81,
                Length = 4,
            };
        public static FieldDefinition T50TPC =>
            new()
            {
                Name = "T50TPC",
                SegmentPosition = 85,
                Length = 4,
            };
        public static FieldDefinition T50AAN =>
            new()
            {
                Name = "T50AAN",
                SegmentPosition = 89,
                Length = 9,
            };
        public static FieldDefinition T50RCL =>
            new()
            {
                Name = "T50RCL",
                SegmentPosition = 98,
                Length = 6,
            };
        public static FieldDefinition T50ORL =>
            new()
            {
                Name = "T50ORL",
                SegmentPosition = 104,
                Length = 6,
            };
        public static FieldDefinition T50OCC =>
            new()
            {
                Name = "T50OCC",
                SegmentPosition = 110,
                Length = 2,
            };
        public static FieldDefinition T50OAM =>
            new()
            {
                Name = "T50OAM",
                SegmentPosition = 112,
                Length = 1,
            };
        public static FieldDefinition T50AGS =>
            new()
            {
                Name = "T50AGS",
                SegmentPosition = 113,
                Length = 6,
            };
        public static FieldDefinition T50SBI =>
            new()
            {
                Name = "T50SBI",
                SegmentPosition = 119,
                Length = 1,
            };
        public static FieldDefinition T50AGT =>
            new()
            {
                Name = "T50AGT",
                SegmentPosition = 120,
                Length = 4,
                NestedFields = new FieldDefinition[2]
                {
                    new(){
                        Name = "T50SIN",
                        SegmentPosition = 120,
                        Length = 2
                    },
                    new(){
                        Name = "T50DTY",
                        SegmentPosition = 122,
                        Length = 2
                    },
                }
            };
        public static FieldDefinition T50PNR =>
            new()
            {
                Name = "T50PNR",
                SegmentPosition = 124,
                Length = 7,
            };
        public static FieldDefinition T50EHT =>
            new()
            {
                Name = "T50EHT",
                SegmentPosition = 131,
                Length = 3,
            };
        public static FieldDefinition T50DTL =>
            new()
            {
                Name = "T50DTL",
                SegmentPosition = 134,
                Length = 7,
            };
        public static FieldDefinition T50NMC =>
            new()
            {
                Name = "T50NMC",
                SegmentPosition = 141,
                Length = 3,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C02 =>
            new()
            {
                Name = "T50MIR",
                SegmentPosition = 144,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50CUR =>
            new()
            {
                Name = "T50CUR",
                SegmentPosition = 145,
                Length = 3,
            };
        public static FieldDefinition T50FAR =>
            new()
            {
                Name = "T50FAR",
                SegmentPosition = 148,
                Length = 12,
            };
        public static FieldDefinition T50DML =>
            new()
            {
                Name = "T50DML",
                SegmentPosition = 160,
                Length = 1,
            };
        public static FieldDefinition T50CUR2 =>
            new()
            {
                Name = "T50CUR2",
                SegmentPosition = 161,
                Length = 3,
            };
        public static FieldDefinition T501TX =>
            new()
            {
                Name = "T501TX",
                SegmentPosition = 164,
                Length = 8,
            };
        public static FieldDefinition T501TC =>
            new()
            {
                Name = "T501TC",
                SegmentPosition = 172,
                Length = 2,
            };
        public static FieldDefinition T502TX =>
            new()
            {
                Name = "T502TX",
                SegmentPosition = 174,
                Length = 8,
            };
        public static FieldDefinition T502TC =>
            new()
            {
                Name = "T502TC",
                SegmentPosition = 182,
                Length = 2,
            };
        public static FieldDefinition T503TX =>
            new()
            {
                Name = "T501TC",
                SegmentPosition = 184,
                Length = 8,
            };
        public static FieldDefinition T503TC =>
            new()
            {
                Name = "T503TC",
                SegmentPosition = 192,
                Length = 2,
            };
        public static FieldDefinition T504TX =>
            new()
            {
                Name = "T504TX",
                SegmentPosition = 194,
                Length = 8,
            };
        public static FieldDefinition T504TC =>
            new()
            {
                Name = "T504TC",
                SegmentPosition = 202,
                Length = 2,
            };
        public static FieldDefinition T505TX =>
            new()
            {
                Name = "T505TX",
                SegmentPosition = 204,
                Length = 8,
            };
        public static FieldDefinition T505TC =>
            new()
            {
                Name = "T505TC",
                SegmentPosition = 212,
                Length = 2,
            };
        public static FieldDefinition T50CMM =>
            new()
            {
                Name = "T50CMM",
                SegmentPosition = 214,
                Length = 12,
            };
        public static FieldDefinition T50COM =>
            new()
            {
                Name = "T50COM",
                SegmentPosition = 214,
                Length = 8,
            };
        public static FieldDefinition T50RTE =>
            new()
            {
                Name = "T50RTE",
                SegmentPosition = 222,
                Length = 4,
            };
        public static FieldDefinition T50ITC =>
            new()
            {
                Name = "T50ITC",
                SegmentPosition = 226,
                Length = 15,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C03 =>
            new()
            {
                Name = "T50C03",
                SegmentPosition = 241,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50IND =>
            new()
            {
                Name = "T50IND",
                SegmentPosition = 242,
                Length = 23,
                NestedFields = new FieldDefinition[19]
                {
                    new(){Name="", SegmentPosition = 242, Length = 1},
                    new(){Name="", SegmentPosition = 243, Length = 1},
                    new(){Name="", SegmentPosition = 244, Length = 1},
                    new(){Name="", SegmentPosition = 245, Length = 1},
                    new(){Name="", SegmentPosition = 246, Length = 1},
                    new(){Name="", SegmentPosition = 247, Length = 1},
                    new(){Name="", SegmentPosition = 248, Length = 1},
                    new(){Name="", SegmentPosition = 249, Length = 1},
                    new(){Name="", SegmentPosition = 250, Length = 1},
                    new(){Name="", SegmentPosition = 251, Length = 1},
                    new(){Name="", SegmentPosition = 251, Length = 1},
                    new(){Name="", SegmentPosition = 252, Length = 1},
                    new(){Name="", SegmentPosition = 253, Length = 1},
                    new(){Name="", SegmentPosition = 254, Length = 1},
                    new(){Name="", SegmentPosition = 255, Length = 1},
                    new(){Name="", SegmentPosition = 256, Length = 1},
                    new(){Name="", SegmentPosition = 257, Length = 1},
                    new(){Name="", SegmentPosition = 258, Length = 3},
                    new(){Name="", SegmentPosition = 262, Length = 3},
                }
            };
        public static FieldDefinition T50DMI =>
            new()
            {
                Name = "T50DMI",
                SegmentPosition = 265,
                Length = 2,
            };
        public static FieldDefinition T50DST =>
            new()
            {
                Name = "T50DST",
                SegmentPosition = 267,
                Length = 1,
            };
        public static FieldDefinition T50DPC =>
            new()
            {
                Name = "T50DPC",
                SegmentPosition = 268,
                Length = 4,
            };
        public static FieldDefinition T50DSQ =>
            new()
            {
                Name = "T50DSQ",
                SegmentPosition = 272,
                Length = 5,
            };
        public static FieldDefinition T50DLN =>
            new()
            {
                Name = "T50DLN",
                SegmentPosition = 277,
                Length = 6,
            };
        public static FieldDefinition T50SMI =>
            new()
            {
                Name = "T50SMI",
                SegmentPosition = 283,
                Length = 2,
            };
        public static FieldDefinition T50SHP =>
            new()
            {
                Name = "T50SHP",
                SegmentPosition = 289,
                Length = 4,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C04 =>
            new()
            {
                Name = "T50C04",
                SegmentPosition = 293,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50CNT =>
            new()
            {
                Name = "T50CNT",
                SegmentPosition = 294,
                Length = 48,
                NestedFields = new FieldDefinition[16]
                {
                    new(){Name="T50CRN", SegmentPosition = 294, Length = 3},
                    new(){Name="T50CPN", SegmentPosition = 297, Length = 3},
                    new(){Name="T50PGN", SegmentPosition = 300, Length = 3},
                    new(){Name="T50FFN", SegmentPosition = 303, Length = 3},
                    new(){Name="T50ARN", SegmentPosition = 306, Length = 3},
                    new(){Name="T50WLN", SegmentPosition = 309, Length = 3},
                    new(){Name="T50SDN", SegmentPosition = 312, Length = 3},
                    new(){Name="T50FBN", SegmentPosition = 315, Length = 3},
                    new(){Name="T50EXC", SegmentPosition = 318, Length = 3},
                    new(){Name="T50PYN", SegmentPosition = 321, Length = 3},
                    new(){Name="T50PHN", SegmentPosition = 324, Length = 3},
                    new(){Name="T50ADN", SegmentPosition = 327, Length = 3},
                    new(){Name="T50MSN", SegmentPosition = 330, Length = 3},
                    new(){Name="T50RRN", SegmentPosition = 333, Length = 3},
                    new(){Name="T50AXN", SegmentPosition = 336, Length = 3},
                    new(){Name="T50LSN", SegmentPosition = 339, Length = 3},
                }
            };
        public static FieldDefinition T50C05 =>
            new()
            {
                Name = "T50C05",
                SegmentPosition = 342,
                Length = 1,
            };
        public static FieldDefinition T50C06 =>
            new()
            {
                Name = "T50C06",
                SegmentPosition = 343,
                Length = 1,
            };
        public static int CarriageReturnNumber => 6;

    }
}
