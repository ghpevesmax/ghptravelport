namespace Common.Utils
{
    public static class StringConstants
    {
        public static readonly string BrandName = "ghptravelport";
        public static readonly string Stage = "stage";
        
        public static readonly string ResourceFileName = ".maak";

        public static readonly string ApiErrorFileExtension = ".aerr";
        public static readonly string DupFileExtension = ".dup";
        public static readonly string MIRFileExtension = ".MIR";
        public static readonly string MIRPathExtension = "*.MIR";
        public static readonly string MIRStagePathExtension = "*.MIR.stage";
        public static readonly string ProcessFileExtension = ".process";
        public static readonly string ProcessedFileExtension = ".processed";
        public static readonly string StageExtension = ".stage";

        public static readonly string BasePath = @$"C:\{BrandName}";
        public static readonly string ResourceFileNamePath = $@"{BasePath}\{ResourceFileName}";
        public static readonly string StagePath = @$"C:\{BrandName}\{Stage}";
    }
}
