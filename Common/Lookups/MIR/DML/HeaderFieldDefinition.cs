using System;
using Common.Models;

namespace Common.Lookups
{
    public static class HeaderFieldDefinition
    {
        public static FieldDefinition T50BID =>
            new FieldDefinition
            {
                Name = "T50BID",
                SegmentPosition = 0,
                Length = 2,
            };
        public static FieldDefinition T50TRC =>
            new FieldDefinition
            {
                Name = "T50BID",
                SegmentPosition = 2,
                Length = 2,
            };
        public static FieldDefinition T50SPC =>
            new FieldDefinition
            {
                Name = "T50SPC",
                SegmentPosition = 4,
                Length = 4,
            };
        public static FieldDefinition T50MIR =>
            new FieldDefinition
            {
                Name = "T50MIR",
                SegmentPosition = 8,
                Length = 2,
            };
        public static FieldDefinition T50SZE =>
            new FieldDefinition
            {
                Name = "T50SZE",
                SegmentPosition = 10,
                Length = 5,
            };
        public static FieldDefinition T50SEQ =>
            new FieldDefinition
            {
                Name = "T50SEQ",
                SegmentPosition = 15,
                Length = 5,
            };
        public static FieldDefinition T50CRE =>
            new FieldDefinition
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
                new FieldDefinition
                {
                    Name = "T50ISS",
                    SegmentPosition = 32,
                    Length = 29,
                    NestedFields = new FieldDefinition[3]{
                        new FieldDefinition{
                            Name = "T50ISC",
                            SegmentPosition = 32,
                            Length = 2,
                        },
                        new FieldDefinition{
                            Name = "T50ISA",
                            SegmentPosition = 34,
                            Length = 3,
                        },
                        new FieldDefinition{
                            Name = "T50ISN",
                            SegmentPosition = 37,
                            Length = 24,
                        },
                    }
                };
        public static FieldDefinition T50DFT =>
                new FieldDefinition
                {
                    Name = "T50DFT",
                    SegmentPosition = 61,
                    Length = 7,
                };
        public static FieldDefinition T50LNI =>
            new FieldDefinition
            {
                Name = "T50LNI",
                SegmentPosition = 68,
                Length = 12,
                NestedFields = new FieldDefinition[2] {
                    new FieldDefinition {
                        Name = "T50INP",
                        SegmentPosition = 68,
                        Length = 6,
                    },
                    new FieldDefinition {
                        Name = "T50OUT",
                        SegmentPosition = 74,
                        Length = 6,
                    },
                }
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50CO1 =>
                new FieldDefinition
                {
                    Name = "T50CO1",
                    SegmentPosition = 80,
                    Length = 1,
                };
        #endregion
        public static FieldDefinition T50BPC =>
            new FieldDefinition
            {
                Name = "T50BPC",
                SegmentPosition = 81,
                Length = 4,
            };
        public static FieldDefinition T50TPC =>
            new FieldDefinition
            {
                Name = "T50TPC",
                SegmentPosition = 85,
                Length = 4,
            };
        public static FieldDefinition T50AAN =>
            new FieldDefinition
            {
                Name = "T50AAN",
                SegmentPosition = 89,
                Length = 9,
            };
        public static FieldDefinition T50RCL =>
            new FieldDefinition
            {
                Name = "T50RCL",
                SegmentPosition = 98,
                Length = 6,
            };
        public static FieldDefinition T50ORL =>
            new FieldDefinition
            {
                Name = "T50ORL",
                SegmentPosition = 104,
                Length = 6,
            };
        public static FieldDefinition T50OCC =>
            new FieldDefinition
            {
                Name = "T50OCC",
                SegmentPosition = 110,
                Length = 2,
            };
        public static FieldDefinition T50OAM =>
            new FieldDefinition
            {
                Name = "T50OAM",
                SegmentPosition = 112,
                Length = 1,
            };
        public static FieldDefinition T50AGS =>
            new FieldDefinition
            {
                Name = "T50AGS",
                SegmentPosition = 113,
                Length = 6,
            };
        public static FieldDefinition T50SBI =>
            new FieldDefinition
            {
                Name = "T50SBI",
                SegmentPosition = 119,
                Length = 1,
            };
        public static FieldDefinition T50AGT =>
            new FieldDefinition
            {
                Name = "T50AGT",
                SegmentPosition = 120,
                Length = 4,
                NestedFields = new FieldDefinition[2]
                {
                    new FieldDefinition{
                        Name = "T50SIN",
                        SegmentPosition = 120,
                        Length = 2
                    },
                    new FieldDefinition{
                        Name = "T50DTY",
                        SegmentPosition = 122,
                        Length = 2
                    },
                }
            };
        public static FieldDefinition T50PNR =>
            new FieldDefinition
            {
                Name = "T50PNR",
                SegmentPosition = 124,
                Length = 7,
            };
        public static FieldDefinition T50EHT =>
            new FieldDefinition
            {
                Name = "T50EHT",
                SegmentPosition = 131,
                Length = 3,
            };
        public static FieldDefinition T50DTL =>
            new FieldDefinition
            {
                Name = "T50DTL",
                SegmentPosition = 134,
                Length = 7,
            };
        public static FieldDefinition T50NMC =>
            new FieldDefinition
            {
                Name = "T50NMC",
                SegmentPosition = 141,
                Length = 3,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C02 =>
            new FieldDefinition
            {
                Name = "T50MIR",
                SegmentPosition = 144,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50CUR =>
            new FieldDefinition
            {
                Name = "T50CUR",
                SegmentPosition = 145,
                Length = 3,
            };
        public static FieldDefinition T50FAR =>
            new FieldDefinition
            {
                Name = "T50FAR",
                SegmentPosition = 148,
                Length = 12,
            };
        public static FieldDefinition T50DML =>
            new FieldDefinition
            {
                Name = "T50DML",
                SegmentPosition = 160,
                Length = 1,
            };
        public static FieldDefinition T50CUR2 =>
            new FieldDefinition
            {
                Name = "T50CUR2",
                SegmentPosition = 161,
                Length = 3,
            };
        public static FieldDefinition T501TX =>
            new FieldDefinition
            {
                Name = "T501TX",
                SegmentPosition = 164,
                Length = 8,
            };
        public static FieldDefinition T501TC =>
            new FieldDefinition
            {
                Name = "T501TC",
                SegmentPosition = 172,
                Length = 2,
            };
        public static FieldDefinition T502TX =>
            new FieldDefinition
            {
                Name = "T502TX",
                SegmentPosition = 174,
                Length = 8,
            };
        public static FieldDefinition T502TC =>
            new FieldDefinition
            {
                Name = "T502TC",
                SegmentPosition = 182,
                Length = 2,
            };
        public static FieldDefinition T503TX =>
            new FieldDefinition
            {
                Name = "T501TC",
                SegmentPosition = 184,
                Length = 8,
            };
        public static FieldDefinition T503TC =>
            new FieldDefinition
            {
                Name = "T503TC",
                SegmentPosition = 192,
                Length = 2,
            };
        public static FieldDefinition T504TX =>
            new FieldDefinition
            {
                Name = "T504TX",
                SegmentPosition = 194,
                Length = 8,
            };
        public static FieldDefinition T504TC =>
            new FieldDefinition
            {
                Name = "T504TC",
                SegmentPosition = 202,
                Length = 2,
            };
        public static FieldDefinition T505TX =>
            new FieldDefinition
            {
                Name = "T505TX",
                SegmentPosition = 204,
                Length = 8,
            };
        public static FieldDefinition T505TC =>
            new FieldDefinition
            {
                Name = "T505TC",
                SegmentPosition = 212,
                Length = 2,
            };
        public static FieldDefinition T50CMM =>
            new FieldDefinition
            {
                Name = "T50CMM",
                SegmentPosition = 214,
                Length = 12,
            };
        public static FieldDefinition T50COM =>
            new FieldDefinition
            {
                Name = "T50COM",
                SegmentPosition = 214,
                Length = 8,
            };
        public static FieldDefinition T50RTE =>
            new FieldDefinition
            {
                Name = "T50RTE",
                SegmentPosition = 222,
                Length = 4,
            };
        public static FieldDefinition T50ITC =>
            new FieldDefinition
            {
                Name = "T50ITC",
                SegmentPosition = 226,
                Length = 15,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C03 =>
            new FieldDefinition
            {
                Name = "T50C03",
                SegmentPosition = 241,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50IND =>
            new FieldDefinition
            {
                Name = "T50IND",
                SegmentPosition = 242,
                Length = 23,
                NestedFields = new FieldDefinition[19]
                {
                    new FieldDefinition{Name="", SegmentPosition = 242, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 243, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 244, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 245, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 246, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 247, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 248, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 249, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 250, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 251, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 251, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 252, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 253, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 254, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 255, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 256, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 257, Length = 1},
                    new FieldDefinition{Name="", SegmentPosition = 258, Length = 3},
                    new FieldDefinition{Name="", SegmentPosition = 262, Length = 3},
                }
            };
        public static FieldDefinition T50DMI =>
            new FieldDefinition
            {
                Name = "T50DMI",
                SegmentPosition = 265,
                Length = 2,
            };
        public static FieldDefinition T50DST =>
            new FieldDefinition
            {
                Name = "T50DST",
                SegmentPosition = 267,
                Length = 1,
            };
        public static FieldDefinition T50DPC =>
            new FieldDefinition
            {
                Name = "T50DPC",
                SegmentPosition = 268,
                Length = 4,
            };
        public static FieldDefinition T50DSQ =>
            new FieldDefinition
            {
                Name = "T50DSQ",
                SegmentPosition = 272,
                Length = 5,
            };
        public static FieldDefinition T50DLN =>
            new FieldDefinition
            {
                Name = "T50DLN",
                SegmentPosition = 277,
                Length = 6,
            };
        public static FieldDefinition T50SMI =>
            new FieldDefinition
            {
                Name = "T50SMI",
                SegmentPosition = 283,
                Length = 2,
            };
        public static FieldDefinition T50SHP =>
            new FieldDefinition
            {
                Name = "T50SHP",
                SegmentPosition = 289,
                Length = 4,
            };
        #region CARRIAGE RETURN
        public static FieldDefinition T50C04 =>
            new FieldDefinition
            {
                Name = "T50C04",
                SegmentPosition = 293,
                Length = 1,
            };
        #endregion
        public static FieldDefinition T50CNT =>
            new FieldDefinition
            {
                Name = "T50CNT",
                SegmentPosition = 294,
                Length = 48,
                NestedFields = new FieldDefinition[16]
                {
                    new FieldDefinition{Name="T50CRN", SegmentPosition = 294, Length = 3},
                    new FieldDefinition{Name="T50CPN", SegmentPosition = 297, Length = 3},
                    new FieldDefinition{Name="T50PGN", SegmentPosition = 300, Length = 3},
                    new FieldDefinition{Name="T50FFN", SegmentPosition = 303, Length = 3},
                    new FieldDefinition{Name="T50ARN", SegmentPosition = 306, Length = 3},
                    new FieldDefinition{Name="T50WLN", SegmentPosition = 309, Length = 3},
                    new FieldDefinition{Name="T50SDN", SegmentPosition = 312, Length = 3},
                    new FieldDefinition{Name="T50FBN", SegmentPosition = 315, Length = 3},
                    new FieldDefinition{Name="T50EXC", SegmentPosition = 318, Length = 3},
                    new FieldDefinition{Name="T50PYN", SegmentPosition = 321, Length = 3},
                    new FieldDefinition{Name="T50PHN", SegmentPosition = 324, Length = 3},
                    new FieldDefinition{Name="T50ADN", SegmentPosition = 327, Length = 3},
                    new FieldDefinition{Name="T50MSN", SegmentPosition = 330, Length = 3},
                    new FieldDefinition{Name="T50RRN", SegmentPosition = 333, Length = 3},
                    new FieldDefinition{Name="T50AXN", SegmentPosition = 336, Length = 3},
                    new FieldDefinition{Name="T50LSN", SegmentPosition = 339, Length = 3},
                }
            };
        public static FieldDefinition T50C05 =>
            new FieldDefinition
            {
                Name = "T50C05",
                SegmentPosition = 342,
                Length = 1,
            };
        public static FieldDefinition T50C06 =>
            new FieldDefinition
            {
                Name = "T50C06",
                SegmentPosition = 343,
                Length = 1,
            };
        public static int CarriageReturnNumber => 6;

    }
}
