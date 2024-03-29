﻿using Common.Lookups;
using System;
using System.Collections.Generic;

namespace Common.Models
{
    public class A14FTSegment : BaseSegment
    {
        public const string ClientIdKey = "IDCLIENTE";
        public const string UserIdKey = "IDUSUARIO";
        public const string InvoicePaymentMethodKey = "FORMAPAGO";
        public const string InvoicePaymentTypeKey = "METODOPAGO";
        public const string InvoiceTypeIdKey = "TIPODOCUMENTO";
        public const string InvoiceUseTypeIdKey = "USOCFDI";
        public const string InvoiceServiceAmountKey = "CARGOSERVICIO";
        public const string InvoiceLineKey = "CONCEPTO";
        public const string MarkupKey = "MARKUP";

        public A14FTSegment()
        {
            Type = SegmentType.A14FT;
        }
        public string ClientId { get; set; }
        public IList<string> Markups { get; set; } = new List<string>();
        public IList<string> InvoiceLines { get; set; } = new List<string>();
        public string InvoicePaymentMethod { get; set; }
        public string InvoicePaymentType { get; set; }
        public IList<string> InvoiceServiceAmounts { get; set; } = new List<string>();
        public string InvoiceUseTypeId { get; set; }
        public string InvoiceTypeId { get; set; }
        public string UserId { get; set; }
    }
}
