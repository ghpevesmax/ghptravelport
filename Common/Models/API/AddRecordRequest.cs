namespace Common.Models
{
    public class AddRecordRequest
    {
        //[JsonProperty(PropertyName="cliente")]
        public string Cliente { get; set; }

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
    }
}
