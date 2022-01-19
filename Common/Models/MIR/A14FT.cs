using Common.Utils;
using System;

namespace Common.Models
{
    public class A14FT
    {
        public A14FT() { }
        public A14FT(A14FTSegment segment) 
        {
            if (segment != null)
            {
                if (!segment.IdCliente.IsNullOrEmpty())
                {
                    IdCliente = Convert.ToInt32(segment.IdCliente.Trim()); 
                }
                if (!segment.Concepto.IsNullOrEmpty())
                {
                    Concepto = segment.Concepto.Trim(); 
                }
                if (!segment.CargoPorServicio.IsNullOrEmpty())
                {
                    CargoPorServicio = Convert.ToDouble(segment.CargoPorServicio.Trim()); 
                }
                if (!segment.IdUsuario.IsNullOrEmpty())
                {
                    IdUsuario = Convert.ToInt32(segment.IdUsuario.Trim());
                }
            }
        }
        public int IdCliente { get; set; }
        public string Concepto { get; set; }
        public double CargoPorServicio { get; set; }
        public int IdUsuario { get; set; }
    }
}
