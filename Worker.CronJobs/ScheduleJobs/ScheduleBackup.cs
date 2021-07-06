using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Worker.CronJobs.GDriveApi;
using Worker.CronJobs.Helper;

namespace Worker.CronJobs.ScheduleJobs
{
    public class ScheduleBackup : CronJobService
    {
        public ScheduleBackup(IScheduleConfig<ScheduleBackup> config) : base(config.CronExpression, config.TimeZoneInfo)
        {

        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            //string filePath = @"C:\Backup\";
            string filePath = @"/var/opt/mssql/backup/";
            filePath = $"{filePath}CloudApp_{DateTime.Now.ToString("yyMMddhhmm")}.bak";
            //TakeDBBackUp(filePath);

            filePath = "/app/shell.sh";

            if (System.IO.File.Exists(filePath)){

                Console.WriteLine("Found");
                await Task.Run(() =>
                {
                    FileHandler.FileUploadInFolder(filePath);
                });

                System.IO.File.Delete(filePath);
            }
        }

        private void TakeDBBackUp(string filePath)
        {
            ProcessStartInfo pi = new ProcessStartInfo()
            {
                FileName = @"shell.sh",
                CreateNoWindow = false,
                UseShellExecute = true,
                Arguments = filePath
            };

            using (var process = new Process() { StartInfo = pi })
            {
                process.Start();

                process.WaitForExit();
            }
        }

    }
}
