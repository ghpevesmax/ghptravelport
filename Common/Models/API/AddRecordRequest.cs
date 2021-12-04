namespace Common.Models
{
    public class AddRecordRequest
    {
        //[JsonProperty(PropertyName="cliente")]
        public string Pasajero { get; set; }

        //[JsonProperty(PropertyName="proveedor")]
        public string Proveedor { get; set; }

        //[JsonProperty(PropertyName="titular")]
        public string Titular { get; set; }

        //[JsonProperty(PropertyName="clave")]
        public string Clave { get; set; }

        //[JsonProperty(PropertyName="sdfdfsdf")]
        public double Total { get; set; }

        //[JsonProperty(PropertyName="iva")]
        public double IVA { get; set; }

        public string UID { get; set; }

        public int IdCliente { get; set; }
        public string Concepto { get; set; }
        public double CargoPorServicio { get; set; }
        public int IdUsuario { get; set; }
    }
}
