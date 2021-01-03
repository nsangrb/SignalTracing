using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SignalTracing
{
    public class Rename_Path
    {
        public Rename_Path() { }
        public Rename_Path(string old_path, string new_path)
        {
            Old_path = old_path;
            New_path = new_path;
        }
        public string Old_path { get; private set; }
        public string New_path { get; private set; }
    }
    class Watcher
    {
        private string RegExFilter { get; set; }
        public string Directory { get; set; }
        public string Filter { get; set; }

        public bool Ischanged { get; set; } = false;

        public List<string> New_Paths { get; private set; } = new List<string>();
        public List<string> Removed_Paths { get; private set; } = new List<string>();
        public List<string> Changed_Paths { get; private set; } = new List<string>();
        public List<Rename_Path> Rename_Paths { get; private set; } = new List<Rename_Path>();
        public Delegate ChangeMethod { get; set; }

        FileSystemWatcher fileSystemWatcher = new FileSystemWatcher();

        public Watcher(string directory, string filter, Delegate invokeMethod)
        {
            this.ChangeMethod = invokeMethod;
            this.Directory = directory;
            this.Filter = filter;
            this.RegExFilter = filter.ToLower().Replace(";", "$|").Replace("*", ".\\") + "$";
        }

        public void StartWatch()
        {
            fileSystemWatcher.Filter = "*.*";
            fileSystemWatcher.Path = this.Directory;
            fileSystemWatcher.IncludeSubdirectories = true;
            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Changed += new FileSystemEventHandler(fileSystemWatcher_Changed);
            fileSystemWatcher.Created += new FileSystemEventHandler(fileSystemWatcher_Changed);
            fileSystemWatcher.Deleted += new FileSystemEventHandler(fileSystemWatcher_Changed);
            fileSystemWatcher.Renamed += new RenamedEventHandler(fileSystemWatcher_Renamed);
        }

        public void Clear_Newpaths()
        {
            New_Paths.Clear();
        }
        public void Clear_Changedpaths()
        {
            Changed_Paths.Clear();
        }
        void fileSystemWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (ChangeMethod != null)
            {
                if (e.ChangeType == WatcherChangeTypes.Created)
                {
                    if (New_Paths.Contains(e.FullPath)) return;
                    if (Removed_Paths.Contains(e.FullPath)) Removed_Paths.Remove(e.FullPath);
                    if (Regex.IsMatch(e.FullPath.ToLower(), RegExFilter))
                    {
                        this.Ischanged = true;
                        New_Paths.Add(e.FullPath);
                    }
                }
                else if (e.ChangeType == WatcherChangeTypes.Deleted)
                {
                    if (Removed_Paths.Contains(e.FullPath)) return;
                    if (New_Paths.Contains(e.FullPath)) New_Paths.Remove(e.FullPath);
                    if (Rename_Paths.Find(f => f.New_path == e.FullPath) != null)
                    {
                        Rename_Paths.Remove(Rename_Paths.Find(f => f.New_path == e.FullPath));
                    }
                    if (Regex.IsMatch(e.FullPath.ToLower(), RegExFilter))
                    {
                        this.Ischanged = true;
                        Removed_Paths.Add(e.FullPath);
                    }
                }
                else if (e.ChangeType == WatcherChangeTypes.Changed)
                {
                    if (Changed_Paths.Contains(e.FullPath)) return;
                    if (New_Paths.Contains(e.FullPath)) New_Paths.Remove(e.FullPath);
                    if (Removed_Paths.Contains(e.FullPath)) Removed_Paths.Remove(e.FullPath);
                    if (Rename_Paths.Find(f => f.New_path == e.FullPath) != null)
                    {
                        Rename_Paths.Remove(Rename_Paths.Find(f => f.New_path == e.FullPath));
                    }
                    if (Regex.IsMatch(e.FullPath.ToLower(), RegExFilter))
                    {
                        this.Ischanged = true;
                        Changed_Paths.Add(e.FullPath);
                    }
                }
                ChangeMethod.DynamicInvoke(sender, e);
            }
        }
        void fileSystemWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            if (ChangeMethod != null)
            {
                if (Removed_Paths.Contains(e.FullPath)) Removed_Paths.Remove(e.FullPath);
                if (New_Paths.Contains(e.FullPath)) New_Paths.Remove(e.FullPath);
                if (New_Paths.Contains(e.OldFullPath)) New_Paths.Remove(e.OldFullPath);
                if (Changed_Paths.Contains(e.FullPath)) Changed_Paths.Remove(e.FullPath);
                if (Changed_Paths.Contains(e.OldFullPath)) Changed_Paths.Remove(e.OldFullPath);
                if (Regex.IsMatch(e.FullPath.ToLower(), RegExFilter))
                {
                    this.Ischanged = true;
                    Rename_Paths.Add(new Rename_Path(e.OldFullPath, e.FullPath));
                }
                ChangeMethod.DynamicInvoke(sender, e);
            }
        }

    }
}
