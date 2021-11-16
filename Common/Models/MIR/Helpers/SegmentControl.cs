namespace Common.Models
{
    public class SegmentControl
    {
        /// <summary>
        /// Defines the total amount of line breaks that compunds a segment.
        /// </summary>
        public int LineBreaks { get; set; }
        public RawSegment LineSegment { get; set; }
    }
}
