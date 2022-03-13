using Newtonsoft.Json;
using System;

namespace Common.Models
{
    public class ApiReservationDetailsRequest
    {
        public double CargoPorServicio { get; set; }
        public double[] InvoiceAmounts { get; set; } = Array.Empty<double>();

        [JsonProperty(PropertyName = "clave")]
        public string PNR { get; set; }
        public string[] InvoiceLines { get; set; } = Array.Empty<string>();
        public double[] FtMarkups { get; set; } = Array.Empty<double>();

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

        public ApiPassenger[] Passengers { get; set; } = Array.Empty<ApiPassenger>();

        [JsonProperty(PropertyName = "proveedor")]
        public string ProviderName { get; set; }
        public string InvoiceTypeId { get; set; }
        public double Total { get; set; }
    }

    public class ApiPassenger
    {
        public string Name { get; set; }

        public bool IsMain { get; set; }

        public string TicketNumber { get; set; }
    }
}
