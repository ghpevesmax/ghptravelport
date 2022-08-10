using Common.Helpers;
using Common.Lookups;
using Common.Models;
using Common.Services;
using Common.Utils;
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
    static class Program
    {
        public static string FileName => @"C:\Users\User\Documentos\Travelport\R5\Ejemplo-con-remarks.MIR";
        public static bool IsApiTest => false;
        public static SegmentType? SegmentTypeTest => null;

        private static ApiReservationDetailsRequest SetTestData()
        {

            var passengers = new List<Passenger> { new Passenger { PassengerName = "Test" } };
            var a14ft = new A14FT
            {
                UserId = 1234560,
                ClientId = 123456,
                InvoiceTypeId = "T",
                InvoiceUseTypeId = "03",
                InvoicePaymentType = "PPD",
                InvoicePaymentMethod = "03",
                FtMarkups = new[] { 100.50, 200.50, 300.50 },
                InvoiceLines = new[] { "Concepto-P01", "Concepto-P02" },
                InvoiceAmounts = new[] { 1000.79, 2000.79, 3000.79 }
            };
            var cost = new Cost { Total = 1000, PrimaryTaxAmount = 160 };
            var provider = "Another Test";
            var PNR = new string('a', 10);
            var hotels = new Hotel[] {
                    new() {
                        Amount = 123.50,
                        Name = "Hilton",
                        Currency = "USD",
                        ConfirmationNumber = "123456789",
                    }
                };
            var cars = new Car[] {
                    new() {
                        Amount = 123.50,
                        Name = "Hilton",
                        Currency = "USD",
                        ConfirmationNumber = "123456789",
                    }
                };

            var apiRequest = MapperService.MapToApi(passengers, cost, provider, PNR, a14ft, cars, hotels);
            return apiRequest;
        }

        private static ApiReservationDetailsRequest SetTestData(List<BaseSegment> mirSegments)
        {
            var passengerSegments = mirSegments.All(SegmentType.Passenger)
                            .Select(_ => _ as PassengerSegment);
            var hotelSegments = mirSegments.All(SegmentType.A16Hotel)
                .Select(_ => _ as A16HotelSegment);
            var carSegments = mirSegments.All(SegmentType.A16Car)
                .Select(_ => _ as A16CarSegment);
            var a14FTSegment = mirSegments.First(SegmentType.A14FT) as A14FTSegment;
            var headerSegment = mirSegments.First(SegmentType.Header) as HeaderSegment;
            var taxSegment = mirSegments.First(SegmentType.FareValue) as FareValueSegment;

            var a14FT = new A14FT(a14FTSegment);
            var PNR = headerSegment.T50RCL.Trim();
            var provider = headerSegment.T50ISS.Trim();
            var cost = MapperService.MapFromSegment(taxSegment);
            var cars = carSegments.Select(MapperService.MapFromSegment);
            var hotels = hotelSegments.Select(MapperService.MapFromSegment);
            var passengers = MapperService.MapFromSegment(passengerSegments);

            var apiRequest = MapperService.MapToApi(passengers, cost, provider, PNR, a14FT, cars, hotels);
            return apiRequest;
        }

        [STAThread]
        static async Task Main(string[] args)
        {
            Console.WriteLine(Environment.CurrentDirectory);

            var lines = FileHelper.GetLinesFromFile(FileName);
            var segmentList = FileProcessor.BuildFileSegments(lines);
            var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);
            var clipboard = new StringBuilder();

            if (SegmentTypeTest.HasValue)
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

            ApiReservationDetailsRequest apiRequest;

            if (IsApiTest)
            {
                apiRequest = SetTestData(); 
            }
            else
            {
                apiRequest = SetTestData(MIRSegments);
            }

            try
            {
                var publicIpAddress = await NetworkHelper.GetPublicIpAddress();
                Console.WriteLine($"Public IP request {publicIpAddress}");
                await RestClientService.SendRequest(apiRequest);
                Console.WriteLine("API request success");
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Could not connect to service on {RestClientService.ServiceUrl}");
                Console.WriteLine("Original exception:\n\n" + ex);

            }
            catch (ApiException ex)
            {
                Console.WriteLine("API request failed");
                if (!new System.Net.HttpStatusCode[] { System.Net.HttpStatusCode.NotFound, System.Net.HttpStatusCode.Unauthorized }.Contains(ex.StatusCode))
                {
                    string message = string.Empty;
                    // Extract the details of the error
                    try
                    {
                        var errors = await ex.GetContentAsAsync<Dictionary<string, string>>();
                        message = string.Join("; ", errors.Values);
                    }
                    catch (Exception)
                    {
                        message = ex.Content;
                    }
                    Console.WriteLine("Message:\n" + message);
                }
                Console.WriteLine("Original exception:\n" + ex);
            }
            Console.ReadLine();
        }
    }
}
