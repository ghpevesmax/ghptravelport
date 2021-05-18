using Common.Lookups;

namespace Common.Models
{
    public class FareValueSegment : BaseSegment
    {
        public FareValueSegment()
        {
            Type = SegmentType.FareValue;
        }
        public string A07SEC { get; set; }
        public string A07FSI { get; set; }
        public string A07CRB { get; set; }
        public string A07TBF { get; set; }
        public string A07CRT { get; set; }
        public string A07TTA { get; set; }
        public string A07CRE { get; set; }
        public string A07EQV { get; set; }
        public string A07NRI { get; set; }
        public string A07NRT { get; set; }
        public string A07CUR { get; set; }
        public string A07TI1 { get; set; }
        public string A07TT1 { get; set; }
        public string A07TC1 { get; set; }
        public string A07TI2 { get; set; }
        public string A07TT2 { get; set; }
        public string A07TC2 { get; set; }
        public string A07TI3 { get; set; }
        public string A07TT3 { get; set; }
        public string A07TC3 { get; set; }
        public string A07TI4 { get; set; }
        public string A07TT4 { get; set; }
        public string A07TC4 { get; set; }
        public string A07TI5 { get; set; }
        public string A07TT5 { get; set; }
        public string A07TC5 { get; set; }

    }
}
