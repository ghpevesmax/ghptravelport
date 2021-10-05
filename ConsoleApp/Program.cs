using Common.Services;
using System;
using System.Text;
using TextCopy;

namespace ConsoleApp
{
    class Program
    {
        public static string FileName => "ATD_AX_GAL.MIR";

        [STAThread]
        static void Main(string[] args)
        {

            var lines = FileProcessor.GetLinesFromFile(FileName);
            var segmentList = FileProcessor.BuildFileSegments(lines);
            var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);
            var clipboard = new StringBuilder();
            foreach (var segment in MIRSegments)
            {
                clipboard.AppendLine(segment.ToString());
                Console.Write(segment.ToString());
            }
            ClipboardService.SetText(clipboard.ToString());
            Console.Write(Environment.SystemDirectory);
            Console.ReadLine();
        }
    }
}
