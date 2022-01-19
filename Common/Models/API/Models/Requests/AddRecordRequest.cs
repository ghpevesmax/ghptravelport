using Newtonsoft.Json;

namespace Common.Models
{
    public class AddRecordRequest
    {
        public string Pasajero { get; set; }

        public string Proveedor { get; set; }

        public string Titular { get; set; }

        public string Clave { get; set; }

        public double Total { get; set; }


        [JsonProperty(PropertyName = "iva")]
        public double IVA { get; set; }

        public string Uid { get; set; }

        public int IdCliente { get; set; }
        public string Concepto { get; set; }
        public double CargoPorServicio { get; set; }
        public int IdUsuario { get; set; }
    }
}
