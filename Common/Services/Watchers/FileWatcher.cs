using Common.Helpers;
using Common.Lookups;
using Common.Models;
using Common.Models.Entities;
using Common.Services;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Common.Watchers
{
    public class FileWatcher
    {
        //private static readonly 
        private readonly Timer Timer;
        public readonly string BasePath = @$"C:\{StringConstants.BrandName}";
        public readonly string StagePath = @$"C:\{StringConstants.BrandName}\{StringConstants.Stage}";


        private string WorkingPath { get; set; }
        public string FileExtensionToWatch { get; set; }
        public bool MoveFilesToStage { get; set; }

        public static void AddLogEntry(string line)
        {
            var lines = new List<string> { line };
            File.AppendAllLines(@$"C:\ghptravelport\Log.txt", lines);
        }

        public static void AddLogRestEntry(string line)
        {
            var lines = new List<string> { line };
            File.AppendAllLines(@$"C:\ghptravelport\stage\LogRest.txt", lines);
        }

        public FileWatcher(string fileExtension, bool filesToStageFolder = true, double interval = 60000)
        {
            Timer = new Timer(interval) { AutoReset = true };
            Timer.Elapsed += TimerElapsed;
            WorkingPath = filesToStageFolder ? BasePath : StagePath;
            FileExtensionToWatch = fileExtension;
            MoveFilesToStage = filesToStageFolder;
            EnsureWorkingPathsExist();
            AddLogEntry($"BasePath:{BasePath} SourcePath:{WorkingPath} FileExtensionToWatch:{FileExtensionToWatch} @{DateTime.Now} ");
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            AddLogEntry("Timer Elapsed - " + DateTime.Now.ToString());
            await WatchSourcePath();
        }

        private async Task WatchSourcePath()
        {
            var directoryInfo = new DirectoryInfo(WorkingPath);
            var sourceFiles = directoryInfo.GetFiles(FileExtensionToWatch);

            if (MoveFilesToStage)
            {
                AddLogEntry("MoveFilesToStage - " + DateTime.Now.ToString());
                foreach (var file in sourceFiles.Where(sf => !sf.FullName.Contains(StringConstants.DupFileExtension)))
                {
                    AddLogEntry($"MoveFilesToStage:{file.FullName} @{DateTime.Now} ");
                    var fileDestName = $@"{WorkingPath}\{StringConstants.Stage}\{file.Name.AddStageExtension()}";
                    if (File.Exists(fileDestName))
                    {
                        Directory.Move(file.FullName, file.FullName.AddDupExtension());
                    }
                    else
                    {
                        Directory.Move(file.FullName, fileDestName);
                    }
                } 
            }
            else
            {
                var sourceFile = sourceFiles.FirstOrDefault();
                if (sourceFile != null && File.Exists(sourceFile.FullName))
                {
                    var sourceFileFullName = FileHelper.MoveFileToProcess(sourceFile.FullName);

                    var lines = FileProcessor.GetLinesFromFile(sourceFileFullName);
                    if (lines.Any())
                    {
                        var segmentList = FileProcessor.BuildFileSegments(lines);
                        var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);

                        var headerSegment = MIRSegments.FirstOrDefault(mir => mir.Type == SegmentType.Header) as HeaderSegment;
                        var passengerSegment = MIRSegments.FirstOrDefault(mir => mir.Type == SegmentType.Passenger) as PassengerSegment;
                        var taxSegment = MIRSegments.FirstOrDefault(mir => mir.Type == SegmentType.FareValue) as FareValueSegment;

                        var PNR = headerSegment.T50RCL.Trim();
                        var passenger = new Passenger
                        {
                            PassengerName = passengerSegment.A02NME.Trim()
                        };
                        var cost = new Cost
                        {
                            Total = Convert.ToDouble(taxSegment.A07TTA.Trim()),
                            PrimaryTaxAmount = Convert.ToDouble(taxSegment.A07TT1.Trim())
                        };
                        var provider = new Provider
                        {
                            ProviderName = headerSegment.T50ISS.Trim()
                        };

                        var success = await RestClientService.SendRequest(passenger, cost, provider, PNR);
                        if(success)
                        {
                            FileHelper.MoveFileToProcessed(sourceFileFullName);
                        }
                        else
                        {
                            FileHelper.MoveFileToApiError(sourceFileFullName);
                        }
                    }

                }
            }
        }

        private void EnsureWorkingPathsExist()
        {
            try
            {
                AddLogEntry($"EnsureWorkingPathsExist SourcePath:{BasePath} @{DateTime.Now} ");
                if (!Directory.Exists(BasePath))
                {
                    AddLogEntry($"CreateDirectory: @{DateTime.Now} ");
                    Directory.CreateDirectory(BasePath);
                }

                AddLogEntry($"EnsureWorkingPathsExist SourcePath:{StagePath} @{DateTime.Now} ");
                if (!Directory.Exists(StagePath))
                {
                    AddLogEntry($"CreateDirectory: @{DateTime.Now} ");
                    Directory.CreateDirectory(StagePath);
                }
            }
            catch (Exception ex)
            {
                AddLogEntry($"MoveFilesToStage:Exception @{DateTime.Now} ");
                AddLogEntry(@$"Message: {ex.Message}");
                AddLogEntry(@$"InnerException: {ex.InnerException}");
                AddLogEntry(@$"StackTrace: {ex.StackTrace}");
                AddLogEntry(@$"Type: {ex.GetType()}");
                throw;
            }
        }

        public void Start()
        {
            Timer.Enabled = true;
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }
    }
}
