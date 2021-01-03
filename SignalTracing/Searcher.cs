using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SignalTracing
{
    class Searcher
    {
        // ----- Asynchronous Events -----

        public delegate void FoundInfoEventHandler(FoundInfoEventArgs e);
        public event FoundInfoEventHandler FoundInfo;

        public delegate void ThreadEndedEventHandler(ThreadEndedEventArgs e);
        public event ThreadEndedEventHandler ThreadEnded;

        // ----- Variables -----

        private Boolean m_stop = false;
        private SearcherParams m_pars = null;
        private int _offset, _cnt_thread;
        public static int Hits_cnt = 0;
        public static int Cnt = 0;
        public Boolean IsDone { get; set; } = false;
        public TreeNode search_Node { get; private set; } = new TreeNode();

        // ----- Public Methods -----
        public Boolean Start(SearcherParams pars, int _offset, int _cnt_thread)
        {
            bool success = false;

            // Perform a reset of all variables,
            // to ensure that the state of the searcher is the same on every new start:
            ResetVariables();

            // Remember the parameters:
            m_pars = pars;
            this._offset = _offset;
            this._cnt_thread = _cnt_thread;
            search_Node = new TreeNode("Search for: \"" + m_pars.ContainingText + "\"");
            // Start searching for FileSystemInfos that match the parameters:
            success = true;

            return success;
        }
        public void Stop()
        {
            // Stop the thread by setting a flag:
            m_stop = true;
        }
        private void ResetVariables()
        {
            m_stop = false;
            m_pars = null;
            search_Node = null;
        }
        public void SearchThread()
        {
            Boolean success = true;
            String errorMsg = "";
            if (m_pars.List_Paths.Length > 0)
            {
                // Convert the string to search for into bytes if necessary:
                if (m_pars.ContainingText == "")
                {
                    success = false;
                    errorMsg = "The string to search for must not be empty.";
                }
            }

            if (success)
            {
                // Get the directory info for the search directory:

                // Search the directory (maybe recursively),
                // and raise events if something was found:
                for (int i = _offset; i < m_pars.List_Paths.Length; i += _cnt_thread)
                {
                    CreateResultsListItem(m_pars.List_Paths[i]);
                    lock (this)
                    {
                        Interlocked.Increment(ref Cnt);
                        FoundInfo(new FoundInfoEventArgs(Cnt));
                    }
                    if (m_stop)
                    {
                        break;
                    }
                }
            }
            else
            {
                success = false;
                errorMsg = "Please enter one or more filenames to search for.";
            }

            IsDone = true;
            // Raise an event:
            if (ThreadEnded != null && IsDone)
            {
                ThreadEnded(new ThreadEndedEventArgs(success, Thread.CurrentThread.Name + ": " + errorMsg));
            }
        }
        public void SearchThread_RegEx()
        {
            Boolean success = true;
            String errorMsg = "";

            if (m_pars.List_Paths.Length > 0)
            {
                // Convert the string to search for into bytes if necessary:
                if (m_pars.ContainingText == "")
                {
                    success = false;
                    errorMsg = "The string to search for must not be empty.";
                }
            }

            if (success)
            {

                if (success)
                {
                    // Search the directory (maybe recursively),
                    // and raise events if something was found:
                    for (int i = _offset; i < m_pars.List_Paths.Length; i += _cnt_thread)
                    {
                        CreateResultsListItem_WithRegEx(m_pars.List_Paths[i]);
                        lock (this)
                        {
                            Interlocked.Increment(ref Cnt);
                            FoundInfo(new FoundInfoEventArgs(Cnt));
                        }
                        if (m_stop)
                        {
                            break;
                        }
                    }

                }
            }
            else
            {
                success = false;
                errorMsg = "Please enter one or more filenames to search for.";
            }

            IsDone = true;
            // Raise an event:
            if (ThreadEnded != null && IsDone)
            {
                ThreadEnded(new ThreadEndedEventArgs(success, Thread.CurrentThread.Name + ": " + errorMsg));
            }
        }
        private void CreateResultsListItem(FileSystemInfo info)
        {
            int l_hits_cnt = 0;
            TreeNode filePath_Node = new TreeNode(info.FullName);
            string[] Line = File.ReadAllLines(info.FullName.ToString());

            for (int i = 0; i < Line.Length; i++)
            {
                string _line = Line[i], _contain = m_pars.ContainingText.Trim();
                if (!m_pars.MatchCase)
                {
                    _line = _line.ToLower();
                }
                if (_line.Contains(_contain))
                {
                    string line = (i + 1).ToString();
                    string contain = Line[i];
                    contain = contain.Replace('\t', ' ');
                    while (contain.IndexOf(" ") == 0) contain = contain.Remove(0, 1);
                    string contain_tmp = contain;
                    try
                    {
                        contain_tmp = contain.Substring(0, 200);
                    }
                    catch { }
                    filePath_Node.Nodes.Add("Line: " + line + " : " + contain_tmp);
                    while (_line.Contains(_contain))
                    {
                         _line = _line.Substring(_line.IndexOf(_contain) + _contain.Length);
                        Interlocked.Increment(ref Hits_cnt);
                        l_hits_cnt++;
                    }
                }
            }
            if (l_hits_cnt >= 2)
            {
                filePath_Node.Text = filePath_Node.Text + " (" + l_hits_cnt.ToString() + " hits)";
                search_Node.Nodes.Add(filePath_Node);
            }
            else if (l_hits_cnt == 1)
            {
                filePath_Node.Text = filePath_Node.Text + " (" + l_hits_cnt.ToString() + " hit)";
                search_Node.Nodes.Add(filePath_Node);
            }
        }


        private void CreateResultsListItem_WithRegEx(FileSystemInfo info)
        {
            int l_hits_cnt = 0;
            TreeNode filePath_Node = new TreeNode(info.FullName);
            string[] Line = File.ReadAllLines(info.FullName.ToString());

            for (int i = 0; i < Line.Length; i++)
            {
                string _line = Line[i], _contain = m_pars.ContainingText.Trim();
                if (!m_pars.MatchCase)
                {
                    _line = _line.ToLower();
                }
                if (Regex.IsMatch(_line,_contain))                
                {
                    string line = (i + 1).ToString();
                    string contain = Line[i];
                    contain = contain.Replace('\t', ' ');
                    while (contain.IndexOf(" ") == 0) contain = contain.Remove(0, 1);
                    string contain_tmp = contain;
                    try
                    {
                        contain_tmp = contain.Substring(0, 200);
                    }
                    catch { }
                    filePath_Node.Nodes.Add("Line: " + line + " : " + contain_tmp);
                    MatchCollection matchCollection = Regex.Matches(_line, _contain);
                    foreach (Match match in matchCollection)
                    {
                        Interlocked.Increment(ref Hits_cnt);
                        l_hits_cnt++;
                    }
                }
            }
            if (l_hits_cnt >= 2)
            {
                filePath_Node.Text = filePath_Node.Text + " (" + l_hits_cnt.ToString() + " hits)";
                search_Node.Nodes.Add(filePath_Node);
            }
            else if (l_hits_cnt == 1)
            {
                filePath_Node.Text = filePath_Node.Text + " (" + l_hits_cnt.ToString() + " hit)";
                search_Node.Nodes.Add(filePath_Node);
            }
        }
    }
}
