using Newtonsoft.Json;
using System;

namespace Common.Models
{
    public class ApiReservationDetailsRequest
    {
        public double CargoPorServicio { get; set; }
        public double[] CargoPorServicios { get; set; } = Array.Empty<double>();

        [JsonProperty(PropertyName = "clave")]
        public string PNR { get; set; }
        public string[] Conceptos { get; set; } = Array.Empty<string>();
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

        [JsonProperty(PropertyName = "pasajeros")]
        public ApiPassenger[] Passengers { get; set; } = Array.Empty<ApiPassenger>();

        [JsonProperty(PropertyName = "proveedor")]
        public string ProviderName { get; set; }

        [JsonProperty(PropertyName = "tipoDocumento")]
        public string InvoiceTypeId { get; set; }
        public double Total { get; set; }
        public string Uid { get; set; }
    }

    public class ApiPassenger
    {
        [JsonProperty(PropertyName = "nombre")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "titular")]
        public bool IsMain { get; set; }

        [JsonProperty(PropertyName = "claveBoleto")]
        public string TicketNumber { get; set; }
    }
}
