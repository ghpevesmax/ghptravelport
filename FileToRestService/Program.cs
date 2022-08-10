using Common.Utils;
using Common.Watchers;
using System;
using Topshelf;

namespace FileToRestService
{
    static class Program
    {
        static void Main(string[] args)
        {
            var exitCode = HostFactory.Run(hf =>
            {
                hf.Service<FileWatcher>(s =>
                {
                    s.ConstructUsing(_ => new FileWatcher(StringConstants.MIRStagePathExtension, filesToStageFolder: false));
                    s.WhenStarted(_ => _.Start());
                    s.WhenStopped(_ => _.Stop());
                });
                hf.RunAsLocalService();
                hf.StartAutomaticallyDelayed();
                hf.EnableShutdown();
                hf.EnableServiceRecovery(_ => {
                    _.RestartService(1);
                });
                hf.SetServiceName("GHPTravelPortFileToRestWatcher");
                hf.SetDisplayName("GHPTravelPort FileToRestWatcher");
                hf.SetDescription("Watches in the folder specified for new source files to begin process");
            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;
        }
    }
}
