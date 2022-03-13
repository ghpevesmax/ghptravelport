using Common.Utils;
using System;
using System.Linq;

namespace Common.Models
{
    public class A14FT
    {
        public A14FT() { }
        public A14FT(A14FTSegment segment) 
        {
            if (segment != null)
            {
                if (!segment.ClientId.IsNullOrEmpty())
                {
                    ClientId = Convert.ToInt32(segment.ClientId.Trim());
                }

                if (!segment.UserId.IsNullOrEmpty())
                {
                    UserId = Convert.ToInt32(segment.UserId.Trim());
                }

                if (!segment.InvoiceTypeId.IsNullOrEmpty())
                {
                    InvoiceTypeId = segment.InvoiceTypeId.Trim();
                }

                if (!segment.InvoicePaymentMethod.IsNullOrEmpty())
                {
                    InvoicePaymentMethod = segment.InvoicePaymentMethod.Trim();
                }

                if (!segment.InvoicePaymentType.IsNullOrEmpty())
                {
                    InvoicePaymentType = segment.InvoicePaymentType.Trim(); 
                }

                if (!segment.InvoiceUseTypeId.IsNullOrEmpty())
                {
                    InvoiceUseTypeId = segment.InvoiceUseTypeId.Trim();
                }

                if (segment.InvoiceServiceAmounts?.Any() == true)
                {
                    InvoiceAmounts = segment.InvoiceServiceAmounts
                        .Select(_ => Convert.ToDouble(_.Trim()))
                        .ToArray();
                }

                if (segment.InvoiceLines?.Any() == true)
                {
                    InvoiceLines = segment.InvoiceLines
                        .Select(_ => _.Trim())
                        .ToArray();
                }

                if (segment.FtMarkups?.Any() == true)
                {
                    FtMarkups = segment.FtMarkups
                        .Select(_ => Convert.ToDouble(_.Trim()))
                        .ToArray();
                }
            }
        }
        public int ClientId { get; set; }
        public double[] FtMarkups { get; set; } = Array.Empty<double>();
        public string[] InvoiceLines { get; set; } = Array.Empty<string>();
        public double[] InvoiceAmounts { get; set; } = Array.Empty<double>();
        public string InvoicePaymentMethod { get; set; }
        public string InvoicePaymentType { get; set; }
        public string InvoiceTypeId { get; set; }
        public string InvoiceUseTypeId { get; set; }
        public int UserId { get; set; }
    }
}
