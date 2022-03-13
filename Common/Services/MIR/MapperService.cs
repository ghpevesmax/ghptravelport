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

        public static ApiPassenger[] MapToApi(IEnumerable<Passenger> passengers)
        {
            return passengers.Select(_ => new ApiPassenger
            {
                Name = _.PassengerName,
                TicketNumber = _.TicketNumber,
                IsMain = passengers.First().Equals(_)
            }).ToArray();
        }

        public static ApiReservationDetailsRequest MapToApi(IEnumerable<Passenger> passengers, Cost cost, string provider, string PNR, A14FT a14FT)
        {
            var apiPassengers = MapToApi(passengers);

            return new ApiReservationDetailsRequest
            {
                PNR = PNR,
                ProviderName = provider,
                Total = cost.Total,
                IVA = cost.PrimaryTaxAmount,
                ClientId = a14FT.ClientId,
                InvoiceLines = a14FT.InvoiceLines,
                InvoiceAmounts = a14FT.InvoiceServiceAmounts,
                UserId = a14FT.UserId,
                FtMarkups = a14FT.FtMarkups,
                Passengers = apiPassengers,
                InvoiceTypeId = a14FT.InvoiceTypeId,
                InvoicePaymentMethod = a14FT.InvoicePaymentMethod,
                InvoicePayment = a14FT.InvoicePaymentType,
                InvoiceUseTypeId = a14FT.InvoiceUseTypeId,
            };
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
