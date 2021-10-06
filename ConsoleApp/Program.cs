using Common.Services;
using System;
using System.Text;
using System.Threading.Tasks;
using TextCopy;

namespace ConsoleApp
{
    class Program
    {
        public static string FileName => "ATD_AX_GAL.MIR";
        public static bool IsApiTest => true;

        [STAThread]
        static async Task Main(string[] args)
        {
            if (IsApiTest)
            {
                var passenger = new Common.Models.Entities.Passenger { PassengerName = "Test" };
                var cost = new Common.Models.Entities.Cost { Total = 1000, PrimaryTaxAmount = 160 };
                var provider = new Common.Models.Entities.Provider { ProviderName = "Another Test" };
                var PNR = new string('a', 10);

                try
                {
                    await RestClientService.SendRequest(passenger, cost, provider, PNR);
                    Console.Write(string.Format("API request success"));
                }
                catch (Exception ex)
                {
                    Console.Write(string.Format("API request failed"));
                    Console.Write(ex);
                }
            }
            else
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
            }
            Console.ReadLine();
        }
    }
}
