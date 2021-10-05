using Common.Utils;
using Common.Watchers;
using System;
using System.IO;
using Topshelf;

namespace FileWatcherService
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var workingDir = @"C:\ghptravelport";
                if (!Directory.Exists(workingDir))
                {
                    Directory.CreateDirectory(workingDir);
                }
                Directory.SetCurrentDirectory(workingDir);
            }
            catch (Exception ex)
            {
                FileWatcher.AddLogEntry(@$"Message: {ex.Message}");
                FileWatcher.AddLogEntry(@$"Message: {ex.InnerException}");
                FileWatcher.AddLogEntry(@$"Message: {ex.StackTrace}");
                FileWatcher.AddLogEntry(@$"Message: {ex.GetType()}");
                throw;
            }

            var exitCode = HostFactory.Run(hf =>
            {
                hf.Service<FileWatcher>(s =>
                {
                    s.ConstructUsing(_ => new FileWatcher(StringConstants.MIRPathExtension, interval: 1000));
                    s.WhenStarted(_ => _.Start());
                    s.WhenStopped(_ => _.Stop());
                });
                hf.RunAsLocalService();
                hf.StartAutomaticallyDelayed();
                hf.EnableShutdown();
                hf.EnableServiceRecovery(_ => {
                    _.RestartService(1);
                });
                hf.SetServiceName("GHPTravelPortFileToStageWatcher");
                hf.SetDisplayName("GHPTravelPort FileToStageWatcher");
                hf.SetDescription("Watches in the folder specified for new source files to begin process");

            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
