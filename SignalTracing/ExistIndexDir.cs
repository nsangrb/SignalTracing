using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SignalTracing
{
    public class ExistIndexDir
    {
        public ExistIndexDir(String indexDir, String tagFile, int cntFile, List<FileInfo> files)
        {
            this.IndexDir = indexDir;
            this.TagFile = tagFile;
            this.CntFile = cntFile;
            this.FileInfos = files;
        }
        public ExistIndexDir(String indexDir, String tagFile, int cntFile, List<FileInfo> files, TriStateTreeView treeview)
        {
            this.IndexDir = indexDir;
            this.TagFile = tagFile;
            this.CntFile = cntFile;
            this.FileInfos = files;

            this.TreeView = new TriStateTreeView();
            foreach (TreeNode treeNode in treeview.Nodes)
            {
                this.TreeView.Nodes.Add((TreeNode)treeNode.Clone());
            }
        }
        public string IndexDir { get; set; }

        public string TagFile { get; set; }

        public int CntFile { get; set; }

        public List<FileInfo> FileInfos { get; set; }

        public TriStateTreeView TreeView { get; set; }
    }
}
