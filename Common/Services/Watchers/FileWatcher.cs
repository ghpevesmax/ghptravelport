using Common.Helpers;
using Common.Lookups;
using Common.Models;
using Common.Models.Entities;
using Common.Services;
using Common.Utils;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace Common.Watchers
{
    public class FileWatcher
    {
        private readonly Timer Timer;
        public readonly string BasePath = @$"C:\{StringConstants.BrandName}";
        public readonly string StagePath = @$"C:\{StringConstants.BrandName}\{StringConstants.Stage}";


        private string WorkingPath { get; set; }
        public string FileExtensionToWatch { get; set; }
        public bool MoveFilesToStage { get; set; }

        public void AddLogEntry(string line)
        {
            FileHelper.AddLogEntry($"BasePath:{BasePath} SourcePath:{WorkingPath} FileExtensionToWatch:{FileExtensionToWatch} @{DateTime.Now}", WorkingPath);
            FileHelper.AddLogEntry(line, WorkingPath);
        }
        public void AddLogEntry(string line, Exception ex)
        {
            FileHelper.AddLogEntry($"BasePath:{BasePath} SourcePath:{WorkingPath} FileExtensionToWatch:{FileExtensionToWatch} @{DateTime.Now}", WorkingPath);
            FileHelper.AddLogEntry(line, WorkingPath);
            FileHelper.AddLogEntry(@$"Message: {ex.Message}", WorkingPath);
            FileHelper.AddLogEntry(@$"InnerException: {ex.InnerException}", WorkingPath);
            FileHelper.AddLogEntry(@$"StackTrace: {ex.StackTrace}", WorkingPath);
            FileHelper.AddLogEntry(@$"Type: {ex.GetType()}", WorkingPath);
        }

        public FileWatcher(string fileExtension, bool filesToStageFolder = true, double interval = 60000)
        {
            Timer = new Timer(interval) { AutoReset = true };
            Timer.Elapsed += TimerElapsed;
            WorkingPath = filesToStageFolder ? BasePath : StagePath;
            FileExtensionToWatch = fileExtension;
            MoveFilesToStage = filesToStageFolder;
            EnsureWorkingPathsExist();
        }

        private async void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                await WatchSourcePath();
            }
            catch (Exception ex)
            {
                AddLogEntry($"Exception @{DateTime.Now} ", ex);
            }
        }

        private async Task WatchSourcePath()
        {
            var directoryInfo = new DirectoryInfo(WorkingPath);
            var sourceFiles = directoryInfo.GetFiles(FileExtensionToWatch);

            if (MoveFilesToStage)
            {
                foreach (var file in sourceFiles.Where(sf => !sf.FullName.Contains(StringConstants.DupFileExtension)))
                {
                    var fileDestName = $@"{WorkingPath}\{StringConstants.Stage}\{file.Name.AddStageExtension()}";
                    if (File.Exists(fileDestName))
                    {
                        fileDestName = file.FullName.AddDupExtension();
                        Directory.Move(file.FullName, fileDestName);
                    }
                    else
                    {
                        Directory.Move(file.FullName, fileDestName);
                    }
                    AddLogEntry($"MoveFilesToStage:{fileDestName} @{DateTime.Now}");
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

                        try
                        {
                            await RestClientService.SendRequest(passenger, cost, provider, PNR);
                            FileHelper.MoveFileToProcessed(sourceFileFullName);
                        }
                        catch (Exception)
                        {
                            FileHelper.MoveFileToApiError(sourceFileFullName);
                            throw;
                        }
                    }

                }
            }
        }

        private void EnsureWorkingPathsExist()
        {
            try
            {
                if (!Directory.Exists(BasePath))
                {
                    AddLogEntry($"CreatedDirectory SourcePath:{BasePath} @{DateTime.Now} ");
                    Directory.CreateDirectory(BasePath);
                }

                if (!Directory.Exists(StagePath))
                {
                    AddLogEntry($"CreatedDirectory SourcePath:{StagePath} @{DateTime.Now} ");
                    Directory.CreateDirectory(StagePath);
                }
            }
            catch (Exception ex)
            {
                AddLogEntry($"MoveFilesToStage:Exception @{DateTime.Now} ", ex);
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
