using MSUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace MvcMusicStore.Infrastructure
{
    public class ReportGenerator
    {
        const string SCRIPT_NAME = "generate_report.bat";
        const string REPORT_NAME = "REPORT.csv";
        readonly string SCRIPT_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, SCRIPT_NAME);
        readonly string REPORT_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, REPORT_NAME);
        readonly string LOGS_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");

        public void GenerateScript()
        {
            string scriptReportString = $@"logparser.exe ""SELECT COUNT(*) as TraceCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'TRACE'"" -o:csv -fileMode:1 -headers:ON
            logparser.exe ""SELECT COUNT(*) as DebugCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'DEBUG'"" -o:csv -fileMode:0 -headers:ON
            logparser.exe ""SELECT COUNT(*) as InfoCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'INFO'"" -o:csv -fileMode:0 -headers:ON
            logparser.exe ""SELECT COUNT(*) as WarnCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'WARN'"" -o:csv -fileMode:0 -headers:ON
            logparser.exe ""SELECT COUNT(*) as ErrorCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'ERROR'"" -o:csv -fileMode:0 -headers:ON
            logparser.exe ""SELECT COUNT(*) as FatalCounts INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'FATAL'"" -o:csv -fileMode:0 -headers:ON
            logparser.exe ""SELECT* INTO '{REPORT_PATH}' FROM '{LOGS_PATH}\*.csv' where LogLevel = 'ERROR'"" -o:csv -fileMode:0 -headers:ON";

            if(!File.Exists(SCRIPT_PATH))
            {
                var fs = File.Create(SCRIPT_PATH);
                fs.Close();

                using (StreamWriter sw = new StreamWriter(SCRIPT_PATH, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(scriptReportString);
                }
            }


            Process process = new Process();
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = "explorer.exe";
            psi.Arguments = SCRIPT_PATH;
            psi.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo = psi;
            process.Start();
            process.WaitForExit();
        }
    }
}