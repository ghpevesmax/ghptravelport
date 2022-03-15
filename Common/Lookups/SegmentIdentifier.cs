namespace Common.Lookups
{
    public static class SegmentIdentifier
    {
        public static readonly string A14FT = "A14FT";
        public static readonly string A16Car = "A16B";
        public static readonly string A16Hotel = "A16A";
        public static readonly string CustomerRemarks = "A00";
        public static readonly string FareValue = "A07";
        public static readonly string Header ="T5";
        public static readonly string Passenger = "A02";

        public static readonly string[] AllIds = new[] 
        {
            A14FT, A16Car, A16Hotel, CustomerRemarks,
            FareValue, Header, Passenger 
        };
    }
}
