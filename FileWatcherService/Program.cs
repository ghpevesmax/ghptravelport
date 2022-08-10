using Common.Helpers;
using Common.Utils;
using Common.Watchers;
using System;
using System.IO;
using Topshelf;

namespace FileWatcherService
{
    static class Program
    {
        static void Main(string[] args)
        {
            var workingDir = @"C:\ghptravelport";
            try
            {
                if (!Directory.Exists(workingDir))
                {
                    Directory.CreateDirectory(workingDir);
                }
                Directory.SetCurrentDirectory(workingDir);
            }
            catch (Exception ex)
            {
                FileHelper.AddLogEntry(@$"Message: {ex.Message}", workingDir);
                FileHelper.AddLogEntry(@$"Message: {ex.InnerException}", workingDir);
                FileHelper.AddLogEntry(@$"Message: {ex.StackTrace}", workingDir);
                FileHelper.AddLogEntry(@$"Message: {ex.GetType()}", workingDir);
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
