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
                    SegmentType segmentType = SegmentType.None;
                    if (line.StartsWith("T5"))
                        segmentType = SegmentType.Header;
                    if (line.StartsWith("A00"))
                        segmentType = SegmentType.CustomerRemarks;
                    if (line.StartsWith("A02"))
                        segmentType = SegmentType.Passenger;
                    if (line.StartsWith("A07"))
                        segmentType = SegmentType.FareValue;

                    if (segmentType != SegmentType.None)
                    {
                        var segmentControl = BuildSegment(lines, segmentType, currentLine);
                        segmentsList.Add(segmentControl.LineSegment);
                        currentLine += segmentControl.LineBreaks;
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
            // Adding pipes to remove carriage returns.
            var segmentLines = lines.Skip(currentLine).Take(lineBreakOcurrences)
                .Aggregate((startLine, nextLine) => $"{startLine}|{nextLine}");

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
