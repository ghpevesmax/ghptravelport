using Newtonsoft.Json;
using System;

namespace Common.Models
{
    public class ApiReservationDetailsRequest
    {
        public double CargoPorServicio { get; set; }

        [JsonProperty(PropertyName = "clave")]
        public string PNR { get; set; }

        [JsonProperty(PropertyName = "idCliente")]
        public int ClientId { get; set; }

        [JsonProperty(PropertyName = "idUsuario")]
        public int UserId { get; set; }

        [JsonProperty(PropertyName = "metodoPago")]
        public string InvoicePayment { get; set; }

        [JsonProperty(PropertyName = "formaPago")]
        public string InvoicePaymentMethod { get; set; }

        [JsonProperty(PropertyName = "usoCfdi")]
        public string InvoiceUseTypeId { get; set; }

        [JsonProperty(PropertyName = "iva")]
        public double IVA { get; set; }

        [JsonProperty(PropertyName = "proveedor")]
        public string ProviderName { get; set; }
        
        [JsonProperty(PropertyName = "tipoDocumento")]
        public string InvoiceTypeId { get; set; }
        public double Total { get; set; }

        public Car[] Cars { get; set; } = Array.Empty<Car>();
        public Hotel[] Hotels { get; set; } = Array.Empty<Hotel>();
        public ApiFtMarkup[] FtMarkups { get; set; } = Array.Empty<ApiFtMarkup>();
        public ApiPassenger[] Passengers { get; set; } = Array.Empty<ApiPassenger>();
        public ApiInvoiceLine[] InvoiceLines { get; set; } = Array.Empty<ApiInvoiceLine>();
        public ApiInvoiceAmount[] InvoiceAmounts { get; set; } = Array.Empty<ApiInvoiceAmount>();
    }

    public class ApiPassenger
    {
        public string Name { get; set; }

        public byte IsMain { get; set; }

        public string TicketNumber { get; set; }
    }

    public class ApiInvoiceAmount
    {
        public double Amount { get; set; }
    }

    public class ApiInvoiceLine
    {
        public string Line { get; set; }
    }

    public class ApiFtMarkup
    {
        public double Amount { get; set; }
    }
}
