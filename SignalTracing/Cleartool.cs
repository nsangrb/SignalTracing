using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Cmd = SignalTracing.RunCommandLine;

namespace SignalTracing
{
    class Cleartool
    {
        private static Cleartool instance;
        private static object key = new object();

        public static Cleartool Instance
        {
            get
            {
                lock (key)
                {
                    if (instance == null)
                    {
                        instance = new Cleartool();
                    }
                }
                return instance;
            }
        }

        #region Methods

        private String[] _Get_ConfigSpecs(String view_name)
        {
            Cmd.Instance.Run_cmd("/C ct catcs -tag " + view_name + " > " + "\"" + Path.Combine(Application.UserAppDataPath, "ctcs_view.txt") + "\"");
            return File.ReadAllLines(Path.Combine(Application.UserAppDataPath, "ctcs_view.txt"));
        }

        public List<String> ListView_by_ID(String id)
        {
            String filt = "";
            Char[] arr_c = id.ToLower().ToCharArray();
            foreach (Char c in arr_c)
            {
                if (Char.IsLetter(c)) filt = filt + "[" + c.ToString() + c.ToString().ToUpper() + "]";
                else filt += c.ToString();
            }
            Cmd.Instance.Run_cmd("/C ct lsview -short *" + filt + "* > " + "\"" + Path.Combine(Application.UserAppDataPath, "lsview.txt") + "\"");
            return File.ReadAllLines(Path.Combine(Application.UserAppDataPath, "lsview.txt")).ToList();
        }
        
        public void Start_view(String view_name)
        {
            Cmd.Instance.Run_cmd("/C ct startview " + view_name);
        }
        public void Mount_vob(string vob)
        {
            Cmd.Instance.Run_cmd("/C ct mount \\" + vob);
        }
        public void MountVobs_by_ConfigSpecs(String view_name)
        {
            String[] config_specs = _Get_ConfigSpecs(view_name);
            List<string> vobs = new List<string>();
            foreach (string line in config_specs)
            {
                try
                {
                    string _line = line.Replace('/', '\\');
                    while (_line.Substring(0, 1) == " ") _line = _line.Remove(0, 1);
                    if (_line.Contains("element") && _line.Substring(0, 1) != "#")
                    {
                        string[] str_arr = _line.Split(' ');
                        if (str_arr[1].Contains("*")) continue;
                        else str_arr = str_arr[1].Split('\\');
                        if (vobs.Find(x => x.ToString() == str_arr[1]) == null)
                        {
                            vobs.Insert(0, str_arr[1]);
                            Cmd.Instance.Run_cmd("/C ct mount \\" + str_arr[1]);
                        }
                    }
                }
                catch
                {

                }
            }
        }
        public List<String> List_All_vobs()
        {
            Cmd.Instance.Run_cmd("/C ct lsvob -short > " + Path.Combine(Application.UserAppDataPath, "all_vobs.txt"));
            return File.ReadAllLines(Path.Combine(Application.UserAppDataPath, "all_vobs.txt")).ToList();
        }
        #endregion
    }
}
