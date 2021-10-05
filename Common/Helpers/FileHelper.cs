using Common.Utils;
using System.IO;

namespace Common.Helpers
{
    public static class FileHelper
    {
        public static string MoveFileToApiError(string sourceFileFullName)
        {
            string newFileFullName;
            var fileDestName = sourceFileFullName
                .RemoveProcessExtension()
                .AddApiErrorExtension();

            if (!File.Exists(fileDestName))
            {
                newFileFullName = sourceFileFullName
                    .RemoveProcessExtension()
                    .AddApiErrorExtension();

                Directory.Move(sourceFileFullName, newFileFullName);
            }
            else
            {
                newFileFullName = sourceFileFullName
                    .RemoveProcessExtension()
                    .RemoveMIRFileExtension()
                    .AddTicks()
                    .AddMIRFileExtension()
                    .AddApiErrorExtension();
                Directory.Move(sourceFileFullName, newFileFullName);
            }
            return newFileFullName;
        }

        public static string MoveFileToProcess(string sourceFileFullName)
        {
            string newFileFullName;
            var fileDestName = sourceFileFullName
                .RemoveStageExtension()
                .AddProcessExtension();

            if (!File.Exists(fileDestName))
            {
                newFileFullName = sourceFileFullName
                    .RemoveStageExtension()
                    .AddProcessExtension();
                Directory.Move(sourceFileFullName, newFileFullName);
            }
            else
            {
                newFileFullName = sourceFileFullName
                    .RemoveStageExtension()
                    .RemoveMIRFileExtension()
                    .AddTicks()
                    .AddMIRFileExtension()
                    .AddProcessExtension();
                Directory.Move(sourceFileFullName, newFileFullName);
            }
            return newFileFullName;
        }

        public static string MoveFileToProcessed(string sourceFileFullName)
        {
            string newFileFullName;
            var fileDestName = sourceFileFullName
                .RemoveProcessExtension()
                .AddProcessedExtension();

            if (!File.Exists(fileDestName))
            {
                newFileFullName = sourceFileFullName
                    .RemoveProcessExtension()
                    .AddProcessedExtension();

                Directory.Move(sourceFileFullName, newFileFullName);
            }
            else
            {
                newFileFullName = sourceFileFullName
                    .RemoveProcessExtension()
                    .RemoveMIRFileExtension()
                    .AddTicks()
                    .AddMIRFileExtension()
                    .AddProcessedExtension();
                Directory.Move(sourceFileFullName, newFileFullName);
            }
            return newFileFullName;
        }
    }
}
