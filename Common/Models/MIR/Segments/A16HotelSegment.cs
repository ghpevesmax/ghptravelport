using Common.Lookups;

namespace Common.Models
{
    public class A16HotelSegment : BaseSegment
    {
        public A16HotelSegment()
        {
            Type = SegmentType.A16Hotel;
        }
        public string A16SEC { get; set; }
        public string A16TYP { get; set; }
        public string A16NME { get; set; }
        public string A16OD { get; set; }
        public string A16OD_A16RG { get; set; }
        public string A16CF { get; set; }
    }
}
