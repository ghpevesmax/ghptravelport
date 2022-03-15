using Common.Lookups;

namespace Common.Models
{
    public class A16CarSegment : BaseSegment
    {
        public A16CarSegment()
        {
            Type = SegmentType.A16Car;
        }
        public string A16SEC { get; set; }
        public string A16TYP { get; set; }
        public string A16CAR { get; set; }
        public string A16OD { get; set; }
        public string A16OD_A16RG { get; set; }
        public string A16CF { get; set; }
    }
}
