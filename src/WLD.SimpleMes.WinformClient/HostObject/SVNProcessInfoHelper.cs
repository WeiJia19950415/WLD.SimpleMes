using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace WLD.SimpleMes.WinformClient.HostObject
{
    public class SVNProcessInfoHelper
    {
        public ProcessStartInfo ProcessStartInfo { get; set; }

        public SVNProcessInfoHelper(string svnExePath = @"D:\Program Files\TortoiseSVN\bin\Svn.exe", string workPath = @"E:\系统部\MES")
        {
            ProcessStartInfo = new ProcessStartInfo()
            {
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                WorkingDirectory = workPath,
                FileName = svnExePath,
                CreateNoWindow = true,
            };
        }

        public void Add(string path)
        {
            var arguments = new StringBuilder();
            arguments.AppendFormat($"add \"{path}\" ");
            ProcessStartInfo.Arguments = arguments.ToString();
            ExcuteCommand();
        }

        public void Update(string path = "")
        {
            var arguments = new StringBuilder();
            arguments.AppendFormat($"update");
            ProcessStartInfo.Arguments = arguments.ToString();
            ExcuteCommand();
        }

        public void Commit(string path, string messageInfo)
        {
            var arguments = new StringBuilder();
            arguments.AppendFormat($"commit  -m \"{messageInfo}\"");
            ProcessStartInfo.Arguments = arguments.ToString();
            ExcuteCommand();
        }


        private void ExcuteCommand()
        {
            var process = Process.Start(ProcessStartInfo);
            string standardOutput = process?.StandardOutput.ReadToEnd();
            process?.WaitForExit();
            if (process == null || string.IsNullOrEmpty(standardOutput))
            {
                MessageBox.Show(process?.StandardError.ReadToEnd());
            }
        }
    }
}
