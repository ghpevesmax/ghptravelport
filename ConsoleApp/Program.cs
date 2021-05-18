using System;
using System.Collections.Generic;
using System.Linq;
using Common.Models;
using Common.Services;

namespace ConsoleApp
{
    class Program
    {
        public string FileName => "ATD_AX_GAL.MIR";

        static void Main(string[] args)
        {
            var lines = FileProcessor.GetLinesFromFile("ATD_AX_GAL.MIR");
            var segmentList = FileProcessor.BuildFileSegments(lines);
            var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);
            foreach(var segment in MIRSegments)
                Console.Write(segment.ToString());
            Console.ReadLine();
        }
    }
}
