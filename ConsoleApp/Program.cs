using Common.Helpers;
using Common.Lookups;
using Common.Models;
using Common.Services;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TextCopy;

namespace ConsoleApp
{
    class Program
    {
        public static string FileName => @"C:\Users\Laelo\OneDrive\Documentos\Development\DesarrollosInformativos\Travelport\R4\2 Adt aereo hotel y auto.MIR";
        //public static string FileName => @"C:\Users\Laelo\OneDrive\Documentos\Development\DesarrollosInformativos\Travelport\R4\1 Pax 1 segmento.MIR";
        public static bool IsApiTest => false;
        public static SegmentType? SegmentTypeTest => SegmentType.Passenger;

        [STAThread]
        static async Task Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);
            if (IsApiTest)
            {
                var passenger = new List<Passenger> { new Passenger { PassengerName = "Test" } };
                var cost = new Cost { Total = 1000, PrimaryTaxAmount = 160 };
                var provider = "Another Test";
                var PNR = new string('a', 10);

                try
                {
                    var publicIpAddress = await NetworkHelper.GetPublicIpAddress();
                    Console.WriteLine($"Public IP request {publicIpAddress}");
                    await RestClientService.SendRequest(passenger, cost, provider, PNR);
                    Console.WriteLine("API request success");
                }
                catch(HttpRequestException ex)
                {
                    Console.WriteLine($"Could not connect to service on { RestClientService.ServiceUrl }");
                    Console.WriteLine("Original exception:\n\n" + ex);

                }
                catch (ApiException ex)
                {
                    Console.WriteLine("API request failed");
                    if(!new System.Net.HttpStatusCode[] { System.Net.HttpStatusCode.NotFound, System.Net.HttpStatusCode.Unauthorized }.Contains(ex.StatusCode))
                    {
                        // Extract the details of the error
                        var errors = await ex.GetContentAsAsync<Dictionary<string, string>>();
                        // Combine the errors into a string
                        var message = string.Join("; ", errors.Values);
                        // Throw a normal exception
                        //throw new Exception(message);
                        Console.WriteLine("Message:\n" + message);
                    }
                    Console.WriteLine("Original exception:\n" + ex);
                }
            }
            else
            {
                var lines = FileHelper.GetLinesFromFile(FileName);
                var segmentList = FileProcessor.BuildFileSegments(lines);
                var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);
                var clipboard = new StringBuilder();

                if(SegmentTypeTest.HasValue)
                {
                    MIRSegments = MIRSegments
                        .Where(_ => _ != null && _.Type == SegmentTypeTest)
                        .ToList();
                }

                foreach (var segment in MIRSegments)
                {
                    clipboard.AppendLine(segment.ToString());
                    Console.Write(segment.ToString());
                }
                ClipboardService.SetText(clipboard.ToString());
            }
            Console.ReadLine();
        }
    }
}
