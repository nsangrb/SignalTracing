using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace SignalTracing
{
    class RunCommandLine
    {

        private static RunCommandLine instance;
        private static object key = new object();

        public static RunCommandLine Instance
        {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new RunCommandLine();
                    }
                }
                return instance;
            }
        }

        #region Methods
        public void Run_cmd(string args)
        {
            using (Process p = new Process())
            {
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.Arguments = args;
                p.StartInfo.FileName = @"C:\Windows\System32\cmd.exe";
                p.Start();
                p.WaitForExit();
            }
        }

        public void Run_cmd_batch(string args,bool CreateNoWindow)
        {
            if (!File.Exists(Path.Combine(Application.UserAppDataPath, "tmp.cmd")))
            {
                File.Delete(Path.Combine(Application.UserAppDataPath, "tmp.cmd"));
            }

            FileStream fs = new FileStream(Path.Combine(Application.UserAppDataPath, "tmp.cmd"), FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write("@ECHO OFF\r\n" + args);
            sw.Flush();
            sw.Close();
            fs.Close();
            Process proc = null;

            proc = new Process();
            proc.StartInfo.WorkingDirectory = Application.UserAppDataPath;
            proc.StartInfo.FileName = "tmp.cmd";
            proc.StartInfo.CreateNoWindow = CreateNoWindow;
            proc.Start();
            proc.WaitForExit();
            proc.Close();

        }
        #endregion
    }
}
