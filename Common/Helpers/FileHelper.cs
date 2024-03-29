﻿using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class FileHelper
    {
        public static void AddLogEntry(string line, string workingPath)
        {
            var lines = new List<string> { line };
            File.AppendAllLines(@$"{workingPath}\Log-{DateTime.Now.ToShortDateString().Replace('/','-')}.txt", lines);
        }

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

        public static IEnumerable<string> GetLinesFromFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                return File.ReadLines(fileName);
            }
            else
            {
                return Enumerable.Empty<string>();
            }
        }

        public static Models.AuthResource GetAuthResources()
        {
            if (!AuthFileExists())
            {
                return null;
            }

            var lines = GetLinesFromFile(StringConstants.ResourceFileNamePath);
            if (!lines.Any())
            {
                return null;
            }

            return new Models.AuthResource
            {
                Uid = lines.FirstOrDefault(),
                Token = lines.Skip(1).LastOrDefault()
            };
        }

        public static bool AuthFileExists()
        {
            return File.Exists(StringConstants.ResourceFileNamePath);
        }

        public static async Task SetAuthResourceAsync(Models.AuthResource authResource)
        {
            if (AuthFileExists())
            {
                File.Delete(@$"{StringConstants.ResourceFileNamePath}"); 
            }
            var lines = new string[] { authResource.Uid, authResource.Token };
            await File.AppendAllLinesAsync(@$"{StringConstants.ResourceFileNamePath}", lines);
        }
    }
}
