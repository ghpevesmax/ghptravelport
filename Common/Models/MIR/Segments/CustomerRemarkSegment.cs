using Common.Lookups;

namespace Common.Models
{
    public class CustomerRemarkSegment : BaseSegment
    {
        public CustomerRemarkSegment()
        {
            Type = SegmentType.Header;
        }
        public string A00SEC { get; set; }
        public string A00CUS { get; set; }
    }
}
