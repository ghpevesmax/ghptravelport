using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Common.Models;
using Common.Lookups;

namespace Common.Services
{
    public class FileProcessor
    {
        public static List<RawSegment> BuildFileSegments(IEnumerable<string> lines)
        {
            var segmentsList = new List<RawSegment>();
            if (lines != null && lines.Any())
            {
                var currentLine = 0;
                while (currentLine < lines.Count())
                {
                    var line = lines.ElementAt(currentLine);
                    var segmentType = SegmentProcessor.GetSegmentType(line);

                    if (segmentType != SegmentType.None)
                    {
                        var segmentControl = BuildSegment(lines, segmentType, currentLine);
                        segmentsList.Add(segmentControl.LineSegment);
                        currentLine += segmentControl.LineBreaks > 0 ? segmentControl.LineBreaks : 1;
                    }
                    else
                        currentLine++;
                }
            }
            return segmentsList;
        }

        private static SegmentControl BuildSegment(IEnumerable<string> lines, SegmentType segmentType, int currentLine)
        {
            int lineBreakOcurrences = 0;
            switch (segmentType)
            {
                case SegmentType.Header: lineBreakOcurrences = HeaderFieldDefinition.CarriageReturnNumber;
                                         break;
                case SegmentType.CustomerRemarks: lineBreakOcurrences = CustomerRemarkFieldDefinition.CarriageReturnNumber;
                                         break;
                case SegmentType.Passenger: lineBreakOcurrences = PassengerFieldDefinition.CarriageReturnNumber;
                                         break;
                case SegmentType.FareValue: lineBreakOcurrences = FareValueFieldDefinition.CarriageReturnNumber;
                                         break;
            }
            var segmentLines = lines.ElementAt(currentLine);
            if (lineBreakOcurrences > 0)
            {
                // Adding pipes to remove carriage returns.
                segmentLines = lines.Skip(currentLine).Take(lineBreakOcurrences)
                    .Aggregate((startLine, nextLine) => $"{startLine}|{nextLine}"); 
            }

            return new SegmentControl {
                LineSegment = new RawSegment {
                    SegmentString = segmentLines,
                    SegmentType = segmentType
                },
                LineBreaks = lineBreakOcurrences,
            };
        }
    }
}
