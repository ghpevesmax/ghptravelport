using Common.Models;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Services
{
    public static class MapperService
    {
        public static IEnumerable<Passenger> MapFromSegment(IEnumerable<PassengerSegment> segments)
        {
            return segments.Select(MapFromSegment);
        }

        public static Cost MapFromSegment(FareValueSegment segment)
        {
            return new Cost
            {
                Total = Convert.ToDouble(segment.A07TTA.Trim()),
                PrimaryTaxAmount = Convert.ToDouble(segment.A07TT1.Trim())
            };
        }

        public static Passenger MapFromSegment(PassengerSegment segment)
        {
            return new Passenger
            {
                PassengerName = segment.A02NME.Trim(),
                TicketNumber = segment.A02TIN_A02TKT.Trim()
            };
        }

        public static Hotel MapFromSegment(A16HotelSegment segment)
        {
            var amount = segment.A16OD_A16RG.Substring(3);
            var currency = segment.A16OD_A16RG.Substring(0, 3);
            return new Hotel
            {
                Currency = currency.Trim(),
                Name = segment.A16NME.Trim(),
                Amount = Convert.ToDouble(amount.Trim()),
                ConfirmationNumber = segment.A16CF.Trim()
            };
        }

        public static Car MapFromSegment(A16CarSegment segment)
        {
            var amount = segment.A16OD_A16RG.Substring(3);
            var currency = segment.A16OD_A16RG.Substring(0, 3);
            return new Car
            {
                Currency = currency.Trim(),
                Name = segment.A16CAR.Trim(),
                Amount = Convert.ToDouble(amount.Trim()),
                ConfirmationNumber = segment.A16CF.Trim()
            };
        }

        public static ApiPassenger[] MapToApi(IEnumerable<Passenger> passengers)
        {
            var passengerList = passengers.ToList();
            return passengerList
                .Select(_ => new ApiPassenger
                {
                    Name = _.PassengerName,
                    TicketNumber = _.TicketNumber,
                    IsMain = (byte)(passengerList.IndexOf(_) == 0 ? 1: 0)
                }).ToArray();
        }

        public static ApiInvoiceLine[] MapToApi(string[] invoiceLines)
        {
            return invoiceLines.Select(_ => new ApiInvoiceLine
            {
                Line = _.Trim()
            }).ToArray();
        }

        public static ApiReservationDetailsRequest MapToApi(IEnumerable<Passenger> passengers, Cost cost, string provider, string PNR, 
            A14FT a14FT, IEnumerable<Car> cars, IEnumerable<Hotel> hotels)
        {
            var apiPassengers = MapToApi(passengers);
            var apiInvoiceLines = MapToApi(a14FT.InvoiceLines);
            var apiMarkups = MapToApiMarkup(a14FT.Markups);
            var apiInvoiceAmounts = MapToApiInvoiceAmount(a14FT.InvoiceAmounts);

            return new ApiReservationDetailsRequest
            {
                PNR = PNR,
                Total = cost.Total,
                UserId = a14FT.UserId,
                ProviderName = provider,
                FtMarkups = apiMarkups,
                ClientId = a14FT.ClientId,
                Passengers = apiPassengers,
                IVA = cost.PrimaryTaxAmount,
                InvoiceLines = apiInvoiceLines,
                InvoiceTypeId = a14FT.InvoiceTypeId,
                InvoiceAmounts = apiInvoiceAmounts,
                InvoicePayment = a14FT.InvoicePaymentType,
                InvoiceUseTypeId = a14FT.InvoiceUseTypeId,
                InvoicePaymentMethod = a14FT.InvoicePaymentMethod,
                Cars = cars.ToArray(),
                Hotels = hotels.ToArray()
        };
        }

        public static ApiInvoiceAmount[] MapToApiInvoiceAmount(double[] amounts)
        {
            return amounts
                .Select(_ => new ApiInvoiceAmount { Amount = _ })
                .ToArray();
        }

        public static ApiMarkup[] MapToApiMarkup(double[] amounts)
        {
            return amounts
                .Select(_ => new ApiMarkup { Amount = _ })
                .ToArray();
        }

        public static void MapToSegment(A14FTSegment segment, RawSegment rawSegment)
        {
            var a14ft = rawSegment.SegmentString
                .Replace("A14FT-", string.Empty)
                .Replace("a14FT-", string.Empty);

            var keyDelimIndex = a14ft.IndexOf('-');
            var a14FtKey = a14ft[..keyDelimIndex].ToUpper();
            var a14FtValue = a14ft[(keyDelimIndex + 1)..];

            if (a14FtKey.StartsWith(A14FTSegment.ClientIdKey))
            {
                segment.ClientId = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.UserIdKey))
            {
                segment.UserId = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoicePaymentMethodKey))
            {
                segment.InvoicePaymentMethod = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoicePaymentTypeKey))
            {
                segment.InvoicePaymentType = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoiceTypeIdKey))
            {
                segment.InvoiceTypeId = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoiceUseTypeIdKey))
            {
                segment.InvoiceUseTypeId = a14FtValue;
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoiceServiceAmountKey))
            {
                segment.InvoiceServiceAmounts.Add(a14FtValue);
            }

            if (a14FtKey.StartsWith(A14FTSegment.InvoiceLineKey))
            {
                segment.InvoiceLines.Add(a14FtValue);
            }

            if (a14FtKey.StartsWith(A14FTSegment.MarkupKey))
            {
                segment.Markups.Add(a14FtValue);
            }
        }

        public static void OrderSubSegments(A14FTSegment segment)
        {
            segment.InvoiceLines = segment.InvoiceLines
                .OrderSplit("-");

            segment.InvoiceServiceAmounts = segment.InvoiceServiceAmounts
                .OrderSplit("-");

            segment.Markups = segment.Markups
                .OrderSplit("-");
        }
    }
}
