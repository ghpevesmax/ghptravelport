using Common.Lookups;

namespace Common.Models
{
    public class PassengerSegment : BaseSegment
    {
        public PassengerSegment()
        {
            Type = SegmentType.Passenger;
        }
        public string A02SEC { get; set; }
        public string A02NME { get; set; }
        public string A02TIN_A02TKT { get; set; }
    }
}
