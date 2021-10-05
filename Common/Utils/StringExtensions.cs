using System;

namespace Common.Utils
{
    public static class StringExtensions
    {
        public static string AddApiErrorExtension(this string s) => 
            s + StringConstants.ApiErrorFileExtension;

        public static string AddDupExtension(this string s) => 
            s + StringConstants.DupFileExtension;

        public static string AddMIRFileExtension(this string s) =>
            s + StringConstants.MIRFileExtension;

        public static string AddProcessExtension(this string s) => 
            s + StringConstants.ProcessFileExtension;

        public static string AddProcessedExtension(this string s) => 
            s + StringConstants.ProcessedFileExtension;

        public static string AddStageExtension(this string s) => 
            s + StringConstants.StageExtension;

        public static string AddTicks(this string s) => 
            s + "-" + DateTime.UtcNow.TimeOfDay.Ticks.ToString();

        public static string RemoveMIRFileExtension(this string s) => 
            s.Replace(StringConstants.MIRFileExtension, string.Empty);

        public static string RemoveProcessExtension(this string s) => 
            s.Replace(StringConstants.ProcessFileExtension, string.Empty);

        public static string RemoveStageExtension(this string s) => 
            s.Replace(StringConstants.StageExtension, string.Empty);

    }
}
