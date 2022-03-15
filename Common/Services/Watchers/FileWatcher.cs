using Common.Helpers;
using Common.Lookups;
using Common.Models;
using Common.Services;
using Common.Utils;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using Refit;

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
            FileHelper.AddLogEntry(@$"Type: {ex.GetType()}", WorkingPath);
            FileHelper.AddLogEntry(@$"Message: {ex.Message}", WorkingPath);
            if (ex is ApiException)
            {
                FileHelper.AddLogEntry(@$"Content: {(ex as ApiException).Content}", WorkingPath); 
            }
            FileHelper.AddLogEntry(@$"InnerException: {ex.InnerException}", WorkingPath);
            FileHelper.AddLogEntry(@$"StackTrace: {ex.StackTrace}", WorkingPath);
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
                    AddLogEntry($"MoveFilesToStage: {fileDestName} @{DateTime.Now}");
                }
            }
            else
            {
                var sourceFile = sourceFiles.FirstOrDefault();
                if (sourceFile != null && File.Exists(sourceFile.FullName))
                {
                    var sourceFileFullName = FileHelper.MoveFileToProcess(sourceFile.FullName);

                    var lines = FileHelper.GetLinesFromFile(sourceFileFullName);
                    if (lines.Any())
                    {
                        var segmentList = FileProcessor.BuildFileSegments(lines);
                        var MIRSegments = SegmentProcessor.GenerateAllSegments(segmentList);

                        var passengerSegments = MIRSegments.All(SegmentType.Passenger)
                            .Select(_ => _ as PassengerSegment);
                        var hotelSegments = MIRSegments.All(SegmentType.A16Hotel)
                            .Select(_ => _ as A16HotelSegment);
                        var carSegments = MIRSegments.All(SegmentType.A16Car)
                            .Select(_ => _ as A16CarSegment);
                        var a14FTSegment = MIRSegments.First(SegmentType.A14FT) as A14FTSegment;
                        var headerSegment = MIRSegments.First(SegmentType.Header) as HeaderSegment;
                        var taxSegment = MIRSegments.First(SegmentType.FareValue) as FareValueSegment;

                        var a14FT = new A14FT(a14FTSegment);
                        var PNR = headerSegment.T50RCL.Trim();
                        var provider = headerSegment.T50ISS.Trim();
                        var cost = MapperService.MapFromSegment(taxSegment);
                        var cars = carSegments.Select(MapperService.MapFromSegment);
                        var hotels = hotelSegments.Select(MapperService.MapFromSegment);
                        var passengers = MapperService.MapFromSegment(passengerSegments);

                        try
                        {
                            var apiRequest = MapperService
                                    .MapToApi(passengers, cost, provider, PNR, a14FT);
                                apiRequest.Cars = cars.ToArray();
                                apiRequest.Hotels = hotels.ToArray();

                            await RestClientService.SendRequest(apiRequest);
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
