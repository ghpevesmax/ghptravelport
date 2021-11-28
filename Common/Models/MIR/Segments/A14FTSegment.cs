using Common.Lookups;

namespace Common.Models
{
    public class A14FTSegment : BaseSegment
    {
        public A14FTSegment()
        {
            Type = SegmentType.A14FT;
        }
        public string IdCliente { get; set; }
        public string Concepto { get; set; }
        public string CargoPorServicio { get; set; }
        public string IdUsuario { get; set; }
    }
}
