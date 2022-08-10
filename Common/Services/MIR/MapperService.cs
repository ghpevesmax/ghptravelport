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
            var apiFtMarkups = MapToApiFtMarkup(a14FT.FtMarkups);
            var apiInvoiceAmounts = MapToApiInvoiceAmount(a14FT.InvoiceAmounts);

            return new ApiReservationDetailsRequest
            {
                PNR = PNR,
                Total = cost.Total,
                UserId = a14FT.UserId,
                ProviderName = provider,
                FtMarkups = apiFtMarkups,
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

        public static ApiFtMarkup[] MapToApiFtMarkup(double[] amounts)
        {
            return amounts
                .Select(_ => new ApiFtMarkup { Amount = _ })
                .ToArray();
        }

        public static void MapToSegment(A14FTSegment segment, RawSegment rawSegment)
        {
            var a14ftValue = rawSegment.SegmentString
                .Replace("A14FT-", string.Empty);

            if (a14ftValue.StartsWith("IdCliente-"))
            {
                segment.ClientId = a14ftValue.Replace("IdCliente-", string.Empty);
            }

            if (a14ftValue.StartsWith("IdUsuario-"))
            {
                segment.UserId = a14ftValue.Replace("IdUsuario-", string.Empty);
            }

            if (a14ftValue.StartsWith("FormaPago-"))
            {
                segment.InvoicePaymentMethod = a14ftValue.Replace("FormaPago-", string.Empty);
            }

            if (a14ftValue.StartsWith("MetodoPago-"))
            {
                segment.InvoicePaymentType = a14ftValue.Replace("MetodoPago-", string.Empty);
            }

            if (a14ftValue.StartsWith("TipoDocumento-"))
            {
                segment.InvoiceTypeId = a14ftValue.Replace("TipoDocumento-", string.Empty);
            }

            if (a14ftValue.StartsWith("UsoCFDI-"))
            {
                segment.InvoiceUseTypeId = a14ftValue.Replace("UsoCFDI-", string.Empty);
            }

            if (a14ftValue.StartsWith("TipoDocumento-"))
            {
                segment.InvoiceTypeId = a14ftValue.Replace("TipoDocumento-", string.Empty);
            }

            if (a14ftValue.StartsWith("CargoServicio-"))
            {
                var cargo = a14ftValue.Replace("CargoServicio-", string.Empty);
                segment.InvoiceServiceAmounts.Add(cargo);
            }

            if (a14ftValue.StartsWith("Concepto-"))
            {
                var concepto = a14ftValue.Replace("Concepto-", string.Empty);
                segment.InvoiceLines.Add(concepto);
            }

            if (a14ftValue.StartsWith("FtMarkup-"))
            {
                var ftMarkup = a14ftValue.Replace("FtMarkup-", string.Empty);
                segment.FtMarkups.Add(ftMarkup);
            }
        }

        public static void OrderSubSegments(A14FTSegment segment)
        {
            segment.InvoiceLines = segment.InvoiceLines
                .OrderSplit("-");

            segment.InvoiceServiceAmounts = segment.InvoiceServiceAmounts
                .OrderSplit("-");

            segment.FtMarkups = segment.FtMarkups
                .OrderSplit("-");
        }
    }
}
