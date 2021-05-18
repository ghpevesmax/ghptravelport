using Common.Lookups;

namespace Common.Models
{
    public class HeaderSegment: BaseSegment
    {
        public HeaderSegment()
        {
            Type = SegmentType.Header;
        }
        public string T50BID { get; set; }
        public string T50TRC { get; set; }
        public string T50SPC { get; set; }
        public string T50MIR { get; set; }
        public string T50SZE { get; set; }
        public string T50SEQ { get; set; }
        public string T50CRE { get; set; }
        /* Children fields
         * new MIRFieldDefinition{
                        Name = "T50DTE",
                        SegmentPosition = 20,
                        Length = 7,
                    },
                    new MIRFieldDefinition{
                        Name = "T50TME",
                        SegmentPosition = 27,
                        Length = 5,
                    },
         */
        public string T50ISS { get; set; }
        /* Children fields
         * new MIRFieldDefinition{
                Name = "T50ISC",
                SegmentPosition = 32,
                Length = 2,
            },
            new MIRFieldDefinition{
                Name = "T50ISA",
                SegmentPosition = 34,
                Length = 3,
            },
            new MIRFieldDefinition
            {
                Name = "T50ISN",
                SegmentPosition = 37,
                Length = 24,
            },
         */
        public string T50DFT { get; set; }
        public string T50LNI { get; set; }
        /* Children fields
         * NestedFields = new MIRFieldDefinition[2] {
                    new MIRFieldDefinition {
                        Name = "T50INP",
                        SegmentPosition = 68,
                        Length = 6,
                    },
                    new MIRFieldDefinition {
                        Name = "T50OUT",
                        SegmentPosition = 74,
                        Length = 6,
                    },
                }
         */
        public string T50BPC { get; set; }
        public string T50TPC { get; set; }
        public string T50AAN { get; set; }
        public string T50RCL { get; set; }
        public string T50ORL { get; set; }
        public string T50OCC { get; set; }
        public string T50OAM { get; set; }
        public string T50AGS { get; set; }
        public string T50SBI { get; set; }
        public string T50AGT { get; set; }
        /* Children fields
         * NestedFields = new MIRFieldDefinition[2]
                {
                    new MIRFieldDefinition{
                        Name = "T50SIN",
                        SegmentPosition = 120,
                        Length = 2
                    },
                    new MIRFieldDefinition{
                        Name = "T50DTY",
                        SegmentPosition = 122,
                        Length = 2
                    },
                }
         */
        public string T50PNR { get; set; }
        public string T50EHT { get; set; }
        public string T50DTL { get; set; }
        public string T50NMC { get; set; }
        public string T50CUR { get; set; }
        public string T50FAR { get; set; }
        public string T50DML { get; set; }
        public string T50CUR2 { get; set; }
        public string T501TX { get; set; }
        public string T501TC { get; set; }
        public string T502TX { get; set; }
        public string T502TC { get; set; }
        public string T503TX { get; set; }
        public string T503TC { get; set; }
        public string T504TX { get; set; }
        public string T504TC { get; set; }
        public string T505TX { get; set; }
        public string T505TC { get; set; }
        public string T50CMM { get; set; }
        public string T50COM { get; set; }
        public string T50RTE { get; set; }
        public string T50ITC { get; set; }
        public string T50IND { get; set; }
        /* Children fields
         * NestedFields = new MIRFieldDefinition[19]
                {
                    new MIRFieldDefinition{Name="", SegmentPosition = 242, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 243, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 244, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 245, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 246, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 247, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 248, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 249, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 250, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 251, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 251, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 252, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 253, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 254, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 255, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 256, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 257, Length = 1},
                    new MIRFieldDefinition{Name="", SegmentPosition = 258, Length = 3},
                    new MIRFieldDefinition{Name="", SegmentPosition = 262, Length = 3},
                }
         */
        public string T50DMI { get; set; }
        public string T50DST { get; set; }
        public string T50DPC { get; set; }
        public string T50DSQ { get; set; }
        public string T50DLN { get; set; }
        public string T50SMI { get; set; }
        public string T50SHP { get; set; }
        public string T50CNT { get; set; }
        /* Children fields
         * NestedFields = new MIRFieldDefinition[16]
                {
                    new MIRFieldDefinition{Name="T50CRN", SegmentPosition = 294, Length = 3},
                    new MIRFieldDefinition{Name="T50CPN", SegmentPosition = 297, Length = 3},
                    new MIRFieldDefinition{Name="T50PGN", SegmentPosition = 300, Length = 3},
                    new MIRFieldDefinition{Name="T50FFN", SegmentPosition = 303, Length = 3},
                    new MIRFieldDefinition{Name="T50ARN", SegmentPosition = 306, Length = 3},
                    new MIRFieldDefinition{Name="T50WLN", SegmentPosition = 309, Length = 3},
                    new MIRFieldDefinition{Name="T50SDN", SegmentPosition = 312, Length = 3},
                    new MIRFieldDefinition{Name="T50FBN", SegmentPosition = 315, Length = 3},
                    new MIRFieldDefinition{Name="T50EXC", SegmentPosition = 318, Length = 3},
                    new MIRFieldDefinition{Name="T50PYN", SegmentPosition = 321, Length = 3},
                    new MIRFieldDefinition{Name="T50PHN", SegmentPosition = 324, Length = 3},
                    new MIRFieldDefinition{Name="T50ADN", SegmentPosition = 327, Length = 3},
                    new MIRFieldDefinition{Name="T50MSN", SegmentPosition = 330, Length = 3},
                    new MIRFieldDefinition{Name="T50RRN", SegmentPosition = 333, Length = 3},
                    new MIRFieldDefinition{Name="T50AXN", SegmentPosition = 336, Length = 3},
                    new MIRFieldDefinition{Name="T50LSN", SegmentPosition = 339, Length = 3},
                }
         */
    }
}
