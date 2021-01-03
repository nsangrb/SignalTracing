using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Data;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;
using Cmd = SignalTracing.RunCommandLine;
using static SignalTracing.TriStateTreeView;

namespace SignalTracing
{
    public partial class MainForm : Form
    {
        #region ----- Variables -----
        private Boolean m_closing = false;
        private Boolean _Is_Indexing = false;

        AutoCompleteStringCollection autoComplete_serchkey = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_folder = new AutoCompleteStringCollection();
        AutoCompleteStringCollection autoComplete_view = new AutoCompleteStringCollection();
        private string search_Text;
        private int hits_cnt = 0, files_cnt = 0;
        string[] need = new string[100];
        private int cnt_thread = 8;
        private List<Thread> m_thread = new List<Thread>();
        private List<Watcher> watcher = new List<Watcher>();
        private List<Watcher> watcher_server = new List<Watcher>();
        List<Searcher> searcher = new List<Searcher>();
        List<Lucene_Indexer> lucene_Indexers = new List<Lucene_Indexer>();

        private ContextMenuStrip cms = new ContextMenuStrip();
        private ContextMenuStrip cms_search = new ContextMenuStrip();
        private ContextMenuStrip cms_interfaces = new ContextMenuStrip();
        private ContextMenuStrip cms_tv_server = new ContextMenuStrip();
        private static string indexDir;
        private static string last_indexDir = "";
        private static string tagFile;
        private static string all_errormsg = "";
        private static int cnt_threadmsg = 0;
        private static List<FileInfo> files = null;
        private int split_form_Height = 230;
        private static string indexBase;
        private static List<ExistIndexDir> lstIndexedDir = new List<ExistIndexDir>();
        private List<String> real_paths_Index = new List<string>();
        private List<String> tagFiles_Index = new List<string>();
        #endregion

        #region ----- Synchronizing Delegates -----

        private delegate void IndexInfoSyncHandler(FoundInfoEventArgs e);
        private IndexInfoSyncHandler IndexInfo;

        private delegate void FoundInfoSyncHandler(FoundInfoEventArgs e);
        private FoundInfoSyncHandler FoundInfo;

        private delegate void ThreadEndedSyncHandler(ThreadEndedEventArgs e);
        private ThreadEndedSyncHandler ThreadEnded_Searcher;

        private ThreadEndedSyncHandler ThreadEnded_Indexer;

        private delegate void FileChangeHandler(object sender, object e);
        private FileChangeHandler FileChange;

        delegate void SetWatcherCallback(object sender, object e);
        private delegate void DelegUpdateTreeView(TreeNode[] arrNodes);
        #endregion

        public MainForm()
        {
            InitializeComponent();

            #region Initialize toolStripMenus
            ToolStripMenuItem openinExplorer = new ToolStripMenuItem();
            ToolStripMenuItem collapseAll = new ToolStripMenuItem();
            ToolStripMenuItem expandAll = new ToolStripMenuItem();
            ToolStripMenuItem pathtoClipboard = new ToolStripMenuItem();
            ToolStripMenuItem nametoClipboard = new ToolStripMenuItem();
            ToolStripMenuItem delete = new ToolStripMenuItem();
            cms.ItemClicked += new ToolStripItemClickedEventHandler(Cms_Click);
            openinExplorer.Text = "Open in Explorer";
            collapseAll.Text = "Collapse all";
            expandAll.Text = "Expand all";
            pathtoClipboard.Text = "Directory to Clipboard";
            nametoClipboard.Text = "File path to Clipboard";
            delete.Text = "Delete";
            cms.Items.Add(openinExplorer);
            cms.Items.Add(collapseAll);
            cms.Items.Add(expandAll);
            cms.Items.Add(pathtoClipboard);
            cms.Items.Add(nametoClipboard);
            cms.Items.Add(delete);

            ToolStripMenuItem collapseAll_searchNode = new ToolStripMenuItem();
            ToolStripMenuItem expandAll_searchNode = new ToolStripMenuItem();
            ToolStripMenuItem delete_searchNode = new ToolStripMenuItem();
            collapseAll_searchNode.Text = "Collapse all";
            expandAll_searchNode.Text = "Expand all";
            delete_searchNode.Text = "Delete";
            cms_search.ItemClicked += new ToolStripItemClickedEventHandler(Cms_searchNode_Click);
            cms_search.Items.Add(collapseAll_searchNode);
            cms_search.Items.Add(expandAll_searchNode);
            cms_search.Items.Add(delete_searchNode);

            ToolStripMenuItem openinExplorer_interf = new ToolStripMenuItem();
            ToolStripMenuItem collapseAll_interf = new ToolStripMenuItem();
            ToolStripMenuItem expandAll_interf = new ToolStripMenuItem();
            ToolStripMenuItem pathtoClipboard_interf = new ToolStripMenuItem();
            ToolStripMenuItem nametoClipboard_interf = new ToolStripMenuItem();
            ToolStripMenuItem delete_interf = new ToolStripMenuItem();
            cms_interfaces.ItemClicked += new ToolStripItemClickedEventHandler(Cms_interfaces_Click);
            openinExplorer_interf.Text = "Open in Explorer";
            collapseAll_interf.Text = "Collapse all";
            expandAll_interf.Text = "Expand all";
            pathtoClipboard_interf.Text = "Directory to Clipboard";
            nametoClipboard_interf.Text = "File path to Clipboard";
            delete_interf.Text = "Delete";
            cms_interfaces.Items.Add(openinExplorer_interf);
            cms_interfaces.Items.Add(collapseAll_interf);
            cms_interfaces.Items.Add(expandAll_interf);
            cms_interfaces.Items.Add(pathtoClipboard_interf);
            cms_interfaces.Items.Add(nametoClipboard_interf);
            cms_interfaces.Items.Add(delete_interf);


            ToolStripMenuItem addVobs_interf = new ToolStripMenuItem();
            cms_tv_server.ItemClicked += new ToolStripItemClickedEventHandler(Cms_tV_sevrer_Click);
            addVobs_interf.Text = "Add Vobs";
            cms_tv_server.Items.Add(addVobs_interf);
            tv_FromServer.ContextMenuStrip = cms_tv_server;
            #endregion
        }

        #region Event
        private void btn_ChooseFolder_Click(object sender, EventArgs e)
        {
            // Load path for creating index            
            OpenFolder();
        }
        private void btn_ChooseFolderNP_pp_Click(object sender, EventArgs e)
        {
            OpenFile_NP();
        }
        private void btn_Search_Click(object sender, EventArgs e)
        {
            if (rbtn_Wildcard.Checked && Regex.IsMatch(cb_SearchKey.Text, "\\s"))
            {
                MessageBox.Show("Key word can NOT contain \"Space\" while using WildCard mode!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tabCtrl.SelectedIndex == 0)
            {
                Update_index_local();
                Searching_Local();
            }
            else if (tabCtrl.SelectedIndex == 1)
            {
                Update_index_server();
                Searching_Server();
            }

        }

        private void Update_index_server()
        {

            string real_path = "N:\\" + cb_ListView.Text + "\\";
            real_path = Real_path(real_path);
            List<Watcher> lst_currentwatcher = watcher.FindAll(x => x.Directory.ToLower().Contains(real_path.ToLower()) && x.Ischanged);
            foreach (Watcher watcher in lst_currentwatcher)
            {
                List<string> filesToUpdate = new List<string>();
                List<string> filesToDelete = new List<string>();
                filesToUpdate.AddRange(watcher.Changed_Paths);
                filesToDelete.AddRange(watcher.Removed_Paths);
                foreach (Rename_Path file in watcher.Rename_Paths)
                {
                    filesToUpdate.Add(file.New_path);
                    filesToDelete.Add(file.Old_path);
                }
                foreach (string file in filesToUpdate)
                {
                    files.Remove(files.Find(f => f.FullName.ToLower() == file.ToLower()));
                    files.Add(new FileInfo(file));
                }
                foreach (string file in filesToDelete)
                {
                    files.Remove(files.Find(f => f.FullName.ToLower() == file.ToLower()));
                }
                watcher.Changed_Paths.Clear();
                watcher.Removed_Paths.Clear();
                watcher.New_Paths.Clear();
                watcher.Rename_Paths.Clear();
                watcher.Ischanged = false;
                DirectoryInfo[] dirs = new DirectoryInfo(indexBase + "\\" + cb_ListView.Text + "\\").GetDirectories();
                dirs = dirs.Where(f => f.FullName.ToLower().Contains(GetValidFolderName(watcher.Directory).ToLower())).ToArray();
                foreach (DirectoryInfo dir in dirs)
                {
                    Lucene_Indexer.Instance.UpdateDocuments(dir.FullName, filesToDelete, filesToUpdate);
                }
                Config_ToolStripProgressbar(tStripPBar_Index, files.Count, files.Count, false);
            }

        }

        private void Update_index_local()
        {
            string real_path = GetAbsolutePath(Real_path(cb_Folder.Text));
            Watcher current_watcher = watcher.Find(x => real_path.ToLower().Contains(x.Directory.ToLower()) && x.Ischanged);
            if (current_watcher != null)
            {
                List<string> filesToUpdate = new List<string>();
                List<string> filesToDelete = new List<string>();
                filesToUpdate.AddRange(current_watcher.Changed_Paths);
                filesToDelete.AddRange(current_watcher.Removed_Paths);
                foreach (Rename_Path file in current_watcher.Rename_Paths)
                {
                    filesToUpdate.Add(file.New_path);
                    filesToDelete.Add(file.Old_path);
                }
                foreach (string file in filesToUpdate)
                {
                    files.Remove(files.Find(f => f.FullName.ToLower() == file.ToLower()));
                    files.Add(new FileInfo(file));
                }
                foreach (string file in filesToDelete)
                {
                    files.Remove(files.Find(f => f.FullName.ToLower() == file.ToLower()));
                }
                current_watcher.Changed_Paths.Clear();
                current_watcher.Removed_Paths.Clear();
                current_watcher.New_Paths.Clear();
                current_watcher.Rename_Paths.Clear();
                current_watcher.Ischanged = false;
                Lucene_Indexer.Instance.UpdateDocuments(indexDir, filesToDelete, filesToUpdate);
                Config_ToolStripProgressbar(tStripPBar_Index, files.Count, files.Count,false);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //cb_SearchKey.AutoCompleteMode = AutoCompleteMode.Suggest;
            //cb_SearchKey.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //cb_SearchKey.AutoCompleteCustomSource = autoComplete_serchkey;
            cb_Folder.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_Folder.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cb_Folder.AutoCompleteCustomSource = autoComplete_folder;
            cb_ListView.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_ListView.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cb_ListView.AutoCompleteCustomSource = autoComplete_view;


            //Init for Index base
            indexBase = Application.UserAppDataPath + "\\index";
            if (Directory.Exists(indexBase))
            {
                Directory.Delete(indexBase, true);
            }

            //Load userID for listing view on server
            txb_IdView.Text = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\')[1].ToLower();


            // Load config values:
            Load_UserConfig();

            //Init for Index dir
            indexDir = indexBase + "\\" + GetValidFolderName(cb_Folder.Text);


            // Subscribe for my own delegates:
            this.IndexInfo += new IndexInfoSyncHandler(this_IndexInfo);
            this.FoundInfo += new FoundInfoSyncHandler(this_FoundInfo);
            this.ThreadEnded_Searcher += new ThreadEndedSyncHandler(this_ThreadEnded_Searcher);
            this.ThreadEnded_Indexer += new ThreadEndedSyncHandler(this_ThreadEnded_Indexer);

            //Number of threads
            numUpDown.Value = cnt_thread;

            // Subscribe for the Searcher's events:           

            tStripPBar_Index.Size = new Size((int)(sttStrip.Width * 0.1), tStripPBar_Index.Height);
        }

        private void btn_StopSearch_Click(object sender, EventArgs e)
        {
            StopIndex();
            StopSearch();
            EnableButtons();
        }



        private void btn_LoadFile_Click(object sender, EventArgs e)
        {            
            indexDir = indexBase + "\\" + GetValidFolderName(cb_Folder.Text);
            if (cb_Folder.Text == "") return;
            real_paths_Index.Clear();
            tagFiles_Index.Clear();
            if (!LoadSingleFolder()) return;
            _Is_Indexing = true;
            DisableButtons_Indexing();
        }
        private void GetlistPathsforLoading_server(TreeNodeCollection treeNodeCollection, List<string> paths)
        {
            foreach (TreeNode tn in treeNodeCollection)
            {
                string[] tags = tn.Tag.ToString().Split('|');
                try
                {
                    if (tags[1] == "Checked")
                    {
                        paths.Add(tags[0]);
                    }
                    else
                    {
                        GetlistPathsforLoading_server(tn.Nodes, paths);
                    }
                }
                catch
                {

                }
            }
        }
        private void GetlistPathsforSearching_server(TreeNodeCollection treeNodeCollection, List<string> paths)
        {
            foreach (TreeNode tn in treeNodeCollection)
            {
                try
                {
                    if (tn.StateImageIndex == (int)CheckedState.Checked)
                    {
                        paths.Add(tn.Tag.ToString().Split('|')[0]);
                    }
                    else if (tn.StateImageIndex == (int)CheckedState.Mixed)
                    {
                        GetlistPathsforSearching_server(tn.Nodes, paths);
                    }
                }
                catch
                {

                }
            }
        }
        private void btn_LoadFile_server_Click(object sender, EventArgs e)
        {            
            int count = 0;
            List<string> lst_paths = new List<string>();
            GetlistPathsforLoading_server(tv_CheckedFolder.Nodes, lst_paths);
            if (lst_paths.Count == 0) return;
            real_paths_Index.Clear();
            tagFiles_Index.Clear();
            foreach (var item in lst_paths)
            {
                if (!LoadMultiFolder(item, count == 0)) return;
                count++;
            }
            _Is_Indexing = true;
            DisableButtons_Indexing();
            files = Lucene_Indexer.Instance.ListFile(real_paths_Index[0], tagFiles_Index[0]).ToList();
            while (files.Count == 0 && real_paths_Index.Count > 0 && tagFiles_Index.Count > 0)
            {
                real_paths_Index.RemoveAt(0);
                tagFiles_Index.RemoveAt(0);
                files = Lucene_Indexer.Instance.ListFile(real_paths_Index[0], tagFiles_Index[0]).ToList();
            }     
            if (real_paths_Index.Count == 0 && tagFiles_Index.Count == 0)
            {
                MessageBox.Show("Directories have no files!!", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _Is_Indexing = false;
                EnableButtons();
                return;
            }

            indexDir = indexBase + "\\" + cb_ListView.Text + "\\" + GetValidFolderName(real_paths_Index[0]);
            Start_CreateIndex(real_paths_Index[0], files.ToArray());
            real_paths_Index.RemoveAt(0);
            tagFiles_Index.RemoveAt(0);
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            RemoveFolder();
        }
        private void btn_Add_Click(object sender, EventArgs e)
        {
            tv_CheckedFolder.Nodes.Clear();
            AddFolder(tv_FromServer.Nodes, tv_CheckedFolder.Nodes);
        }
        private void btn_ListView_Click(object sender, EventArgs e)
        {
            autoComplete_view.Clear();
            ListView();            
            cb_ListView.Focus();
        }

        #region ToolStripItem Events
        private void Cms_searchNode_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Collapse all")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                if (tv_Results.SelectedNode.Level == 2) tv_Results.SelectedNode.Parent.Collapse();
                else tv_Results.SelectedNode.Collapse();
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Expand all")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                tv_Results.SelectedNode.ExpandAll();
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Delete")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                tv_Results.Nodes.Remove(tv_Results.SelectedNode);
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
        }

        private void Cms_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            String path = "", name = "";
            if (tv_Results.SelectedNode.Level == 1)
            {
                path = tv_Results.SelectedNode.Text.Substring(0, tv_Results.SelectedNode.Text.LastIndexOf('\\'));
                name = tv_Results.SelectedNode.Text.Substring(tv_Results.SelectedNode.Text.LastIndexOf('\\'), tv_Results.SelectedNode.Text.LastIndexOf("(") - tv_Results.SelectedNode.Text.LastIndexOf('\\') - 1);
            }
            else if (tv_Results.SelectedNode.Level == 2)
            {
                path = tv_Results.SelectedNode.Parent.Text.Substring(0, tv_Results.SelectedNode.Parent.Text.LastIndexOf('\\'));
                name = tv_Results.SelectedNode.Parent.Text.Substring(tv_Results.SelectedNode.Parent.Text.LastIndexOf('\\'), tv_Results.SelectedNode.Parent.Text.LastIndexOf(" (") - tv_Results.SelectedNode.Parent.Text.LastIndexOf('\\'));
            }
            if (e.ClickedItem.Text == "Open in Explorer")
            {

                // Get the path from the selected item:
                if (tv_Results.SelectedNode.IsSelected)
                {


                    // Open its containing folder in Windows Explorer:
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "explorer.exe";
                        startInfo.Arguments = Path.GetFullPath(path);
                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else if (e.ClickedItem.Text == "Collapse all")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                if (tv_Results.SelectedNode.Level == 2) tv_Results.SelectedNode.Parent.Collapse();
                else tv_Results.SelectedNode.Collapse();
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Expand all")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                tv_Results.SelectedNode.ExpandAll();
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Directory to Clipboard")
            {
                Clipboard.SetText(path);
            }
            else if (e.ClickedItem.Text == "File path to Clipboard")
            {
                Clipboard.SetText(path + name);
            }
            else if (e.ClickedItem.Text == "Delete")
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                tv_Results.Nodes.Remove(tv_Results.SelectedNode);
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
        }

        private void Cms_interfaces_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            String path = "", name = "";
            if (tv_InterfaceList.SelectedNode.Level == 1)
            {
                path = tv_InterfaceList.SelectedNode.Text.Substring(0, tv_InterfaceList.SelectedNode.Text.LastIndexOf('\\'));
                name = tv_InterfaceList.SelectedNode.Text;
            }
            else if (tv_InterfaceList.SelectedNode.Level == 2)
            {
                path = tv_InterfaceList.SelectedNode.Parent.Text.Substring(0, tv_InterfaceList.SelectedNode.Parent.Text.LastIndexOf('\\'));
                name = tv_InterfaceList.SelectedNode.Parent.Text;
            }
            if (e.ClickedItem.Text == "Open in Explorer")
            {

                // Get the path from the selected item:
                if (tv_InterfaceList.SelectedNode.IsSelected)
                {


                    // Open its containing folder in Windows Explorer:
                    try
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = "explorer.exe";
                        startInfo.Arguments = Path.GetFullPath(path);
                        Process process = new Process();
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else if (e.ClickedItem.Text == "Collapse all")
            {
                tv_InterfaceList.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                if (tv_InterfaceList.SelectedNode.Level == 3) tv_InterfaceList.SelectedNode.Parent.Collapse();
                else tv_InterfaceList.SelectedNode.Collapse();
                tv_Results.EndUpdate();
                //tv_InterfaceList.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Expand all")
            {
                tv_InterfaceList.DrawMode = TreeViewDrawMode.Normal;
                tv_InterfaceList.BeginUpdate();
                tv_InterfaceList.SelectedNode.ExpandAll();
                tv_InterfaceList.EndUpdate();
                //tv_InterfaceList.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
            else if (e.ClickedItem.Text == "Directory to Clipboard")
            {
                if (tv_InterfaceList.SelectedNode.Level > 1) Clipboard.SetText(path);
            }
            else if (e.ClickedItem.Text == "File path to Clipboard")
            {
                if (tv_InterfaceList.SelectedNode.Level > 1) Clipboard.SetText(name);
            }
            else if (e.ClickedItem.Text == "Delete")
            {
                tv_InterfaceList.DrawMode = TreeViewDrawMode.Normal;
                tv_InterfaceList.BeginUpdate();
                tv_InterfaceList.Nodes.Remove(tv_InterfaceList.SelectedNode);
                tv_InterfaceList.EndUpdate();
                //tv_InterfaceList.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
        }
        private void Cms_tV_sevrer_Click(object sender, ToolStripItemClickedEventArgs e)
        {
            List<TreeNode> tree_nodes = new List<TreeNode>();
            if (e.ClickedItem.Text == "Add Vobs")
            {
                List<String> mounted_vobs = new List<string>();
                foreach (TreeNode node in tv_FromServer.Nodes)
                {
                    mounted_vobs.Add(node.Text);
                }
                f_Add_Vobs f_Add_Vobs = new f_Add_Vobs();
                f_Add_Vobs.Load_AllVobs(mounted_vobs);
                f_Add_Vobs.ShowDialog();
                foreach (String vob in f_Add_Vobs.Mounted_vobs)
                {
                    if (tv_FromServer.Nodes.Find(vob, false).Length == 0)
                    {
                        try
                        {
                            TreeNode tn = new TreeNode();
                            tree_nodes.Add(LoadDirectory("N:\\" + cb_ListView.Text + "\\" + vob));                            
                        }
                        catch { }
                    }
                }
            }
            tv_FromServer.BeginUpdate();
            tv_FromServer.Nodes.AddRange(tree_nodes.ToArray());
            tv_FromServer.EndUpdate();
        }
        #endregion

        private void tv_FromServer_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode td in e.Node.Nodes)
            {
                try
                {
                    if (td.GetNodeCount(false) == 0)
                    {
                        string Dir = td.Tag.ToString();
                        LoadSubDirectories(Dir, td);
                    }
                }
                catch { }
            }
        }

        private void tv_FromServer_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode theNode = this.tv_FromServer.GetNodeAt(e.X, e.Y);

            // Set a ToolTip only if the mouse pointer is actually paused on a node.  
            if (theNode != null && theNode.Tag != null)
            {
                // Change the ToolTip only if the pointer moved to a new node.  
                if (theNode.Tag.ToString() != this.tTip_1.GetToolTip(this.tv_FromServer))
                    this.tTip_1.SetToolTip(this.tv_FromServer, theNode.Tag.ToString());

            }
            else     // Pointer is not over a node so clear the ToolTip.  
            {
                this.tTip_1.SetToolTip(this.tv_FromServer, "");
            }
        }
        private void cb_ListView_SelectedIndexChanged(object sender, EventArgs e)
        {            
            tv_CheckedFolder.Nodes.Clear();
            tv_FromServer.Nodes.Clear();            
            //Disable_all_elements();
            Cleartool.Instance.MountVobs_by_ConfigSpecs(cb_ListView.Text);
            Cleartool.Instance.Start_view(cb_ListView.Text);
            List<TreeNode> tree_nodes = new List<TreeNode>();
            try
            {
                List<string> view_path = DirectoryAlternative.EnumerateDirectories(@"N:\" + cb_ListView.Text).ToList();

                foreach (string item in view_path)
                {
                    try
                    {
                        if (DirectoryAlternative.EnumerateDirectories(item).Count() > 0)
                        {

                            this.Invoke((MethodInvoker)delegate ()
                            {
                                tree_nodes.Add(LoadDirectory(item));
                            });
                           

                        }
                    }
                    catch { }
                }
            }
            catch { }
            tv_FromServer.BeginUpdate();
            tv_FromServer.Nodes.AddRange(tree_nodes.ToArray());
            tv_FromServer.EndUpdate();


            //MessageBox.Show("Done");

            if (cb_ListView.InvokeRequired)
            {
               // tv_FromServer.Invoke( new DelegUpdateTreeView(UpdateTreeView), tree_nodes.ToArray());
            }
            else
            {
               // tv_FromServer.Nodes.AddRange(tree_nodes.ToArray());
            }

            ExistIndexDir existIndexDir = lstIndexedDir.Find(item => item.IndexDir.ToLower() == cb_ListView.Text.ToLower());
            if (existIndexDir != null)
            {
                foreach (TreeNode treeNode in existIndexDir.TreeView.Nodes)
                {
                    this.tv_CheckedFolder.Nodes.Add((TreeNode)treeNode.Clone());
                }
                indexDir = existIndexDir.IndexDir;
                files = existIndexDir.FileInfos;
                tStrip_stt_Tagfile.Text = "with: " + existIndexDir.TagFile;
                tStrip_lbstt_Indexed.Text = files.Count.ToString() + " files";
                btn_StopSearch.Enabled = true;
                btn_Search.Enabled = true;
                btn_Power.Enabled = true;
                Config_Button(btn_LoadFile_server, "Reload", Color.White, Color.Blue);
                tStrip_lbstt_IndexName.Text = "Indexed:";
            }
            else
            {
                btn_StopSearch.Enabled = false;
                btn_Search.Enabled = false;
                btn_Power.Enabled = false;
                Config_Button(btn_LoadFile_server, "Load", Color.White, Color.Red);
                tStrip_lbstt_IndexName.Text = "Not Indexed";
                tStrip_lbstt_Indexed.Text = "";
                tStrip_stt_Tagfile.Text = "";
            }
        }

        private void UpdateTreeView(TreeNode[] arrNodes)
        {
            tv_FromServer.Nodes.AddRange(arrNodes);
        }

        private void tabCtrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtrl.SelectedIndex == 0)
            {
                sContainerForm.SplitterDistance = 100;
                sContainerForm.IsSplitterFixed = true;               
                string strTmp = cb_Folder.Text;
                if (strTmp == "") return;
                if (cb_Folder.FindStringExact(strTmp) != -1)
                {
                    int index = cb_Folder.FindStringExact(strTmp);
                    cb_Folder.Items.Add("Dummy");
                    cb_Folder.SelectedIndex = cb_Folder.Items.Count - 1;
                    cb_Folder.Items.RemoveAt(cb_Folder.SelectedIndex);
                    cb_Folder.SelectedIndex = index;
                }
            }
            else
            {
                sContainerForm.SplitterDistance = split_form_Height;
                sContainerForm.IsSplitterFixed = false;
                string strTmp = cb_ListView.Text;
                if (strTmp == "") return;
                if (cb_ListView.FindStringExact(strTmp) != -1)
                {
                    int index = cb_ListView.FindStringExact(strTmp);
                    cb_ListView.Items.Add("Dummy");
                    cb_ListView.SelectedIndex = cb_ListView.Items.Count - 1;
                    cb_ListView.Items.RemoveAt(cb_ListView.SelectedIndex);
                    cb_ListView.SelectedIndex = index;
                }
            }
        }
        #endregion

        #region Method

        private void Load_UserConfig()
        {

            // Load old data
            UserConfig.Load();

            //Load Form's last status
            this.Location = new Point(UserConfig.Data.LocationX, UserConfig.Data.LocationY);
            this.Size = new Size(UserConfig.Data.Width, UserConfig.Data.Height);
            this.WindowState = (FormWindowState)UserConfig.Data.WindowState;

            //Load Search Dir ComboBox
            try
            {
                foreach (string str in UserConfig.Data.SearchDir)
                {
                    if (str == "") continue;
                    cb_Folder.Items.Add(str);
                    autoComplete_folder.Add(str);
                }
                cb_Folder.SelectedIndex = 0;
                cb_Folder.SelectionLength = 0;
            }
            catch { }

            //Load Extensions for searching
            cb_FileExts.Text = UserConfig.Data.FileName;


            try
            {
                foreach (string str in UserConfig.Data.ContainingText)
                {
                    if (str == "") continue;
                    cb_SearchKey.Items.Add(str);
                    autoComplete_serchkey.Add(str);
                }
                cb_SearchKey.SelectedIndex = 0;
                cb_SearchKey.SelectionLength = 0;
            }
            catch { }

            cb_FileExts.SelectionLength = 0;

            //Load NotePad++ path
            Notepad.Text = UserConfig.Data.NotePad;
            if (!File.Exists(Notepad.Text))
            {
                if (Notepad.Text.ToLower().Contains("program files (x86)")) Notepad.Text = Notepad.Text.Replace("Program Files (x86)", "Program Files");
                else Notepad.Text = Notepad.Text.Replace("Program Files", "Program Files (x86)");
            }
            if (!File.Exists(Notepad.Text))
            {
                MessageBox.Show("Can't find Notepad++'s directory!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //Load Tab selected
            if (UserConfig.Data.IsServer)
            {
                tabCtrl.SelectedIndex = 1;
                sContainerForm.SplitterDistance = 230;
                sContainerForm.IsSplitterFixed = false;
                sContainerForm.Panel1.Size = new Size(sContainerForm.Panel1.Width, 100);
            }
            else
            {
                tabCtrl.SelectedIndex = 0;
                sContainerForm.SplitterDistance = 100;
                sContainerForm.IsSplitterFixed = true;
                sContainerForm.Panel1.Size = new Size(sContainerForm.Width, 300);
            }
            //Load last Search Results
            int cnt_node = 0;
            tv_Results.DrawMode = TreeViewDrawMode.Normal;
            tv_Results.BeginUpdate();
            if (UserConfig.Data.Search.GetNodeCount(false) != 0)
            {
                foreach (TreeNode node in UserConfig.Data.Search.Nodes)
                {
                    tv_Results.Nodes.Add((TreeNode)node.Clone());
                }
                foreach (TreeNode node in tv_Results.Nodes)
                {
                    if (UserConfig.IsExpland[cnt_node]) node.Expand();
                    cnt_node++;
                    foreach (TreeNode node_1 in node.Nodes)
                    {
                        if (UserConfig.IsExpland[cnt_node]) node_1.Expand();
                        cnt_node++;
                        foreach (TreeNode node_2 in node_1.Nodes)
                        {
                            if (UserConfig.IsExpland[cnt_node]) node_2.Expand();
                            cnt_node++;
                        }
                    }
                }
                tv_Results.Nodes[0].EnsureVisible();
            }
            tv_Results.EndUpdate();
            tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;

            //Load last interface results
            tv_InterfaceList.DrawMode = TreeViewDrawMode.Normal;
            tv_InterfaceList.BeginUpdate();
            if (UserConfig.Data.Interfaces.GetNodeCount(false) != 0)
            {
                foreach (TreeNode node in UserConfig.Data.Interfaces.Nodes)
                {
                    tv_InterfaceList.Nodes.Add((TreeNode)node.Clone());
                }
                foreach (TreeNode node in tv_InterfaceList.Nodes)
                {
                    if (UserConfig.IsExpland[cnt_node]) node.Expand();
                    cnt_node++;
                    foreach (TreeNode node_1 in node.Nodes)
                    {
                        if (UserConfig.IsExpland[cnt_node]) node_1.Expand();
                        cnt_node++;
                        foreach (TreeNode node_2 in node_1.Nodes)
                        {
                            if (UserConfig.IsExpland[cnt_node]) node_2.Expand();
                            cnt_node++;
                        }
                    }
                }

                tv_InterfaceList.Nodes[0].EnsureVisible();
            }
            tv_InterfaceList.EndUpdate();

        }
        private void Save_UserConfig()
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                UserConfig.Data.LocationX = this.Location.X;
                UserConfig.Data.LocationY = this.Location.Y;
                UserConfig.Data.Width = this.Size.Width;
                UserConfig.Data.Height = this.Size.Height;
            }
            if (this.WindowState != FormWindowState.Minimized)
            {
                UserConfig.Data.WindowState = (Int32)this.WindowState;
            }

            UserConfig.Data.SearchDir = cb_Folder.Items.Cast<Object>().Select(item => item.ToString()).ToArray();
            UserConfig.Data.FileName = cb_FileExts.Text;
            UserConfig.Data.ContainingText = cb_SearchKey.Items.Cast<Object>().Select(item => item.ToString()).ToArray();
            UserConfig.Data.NotePad = Notepad.Text;
            UserConfig.Data.Search = tv_Results;
            UserConfig.Data.Interfaces = tv_InterfaceList;
            if (tabCtrl.SelectedIndex == 1) UserConfig.Data.IsServer = true; else UserConfig.Data.IsServer = false;
            UserConfig.Save();

            DirectoryInfo dirInfo = new DirectoryInfo(Application.UserAppDataPath);
            DirectoryInfo[] subDirInfos = dirInfo.GetDirectories();
            foreach (DirectoryInfo item in subDirInfos)
            {
                if (item.Name.Contains("→")) Directory.Delete(item.FullName, true);
            }

        }
        private void OpenFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = cb_Folder.Text;
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath) && IsExistFolder(fbd.SelectedPath))
                {
                    cb_Folder.Text = fbd.SelectedPath;
                    string strTmp = cb_Folder.Text;
                    try
                    {
                        if (strTmp == cb_Folder.Items[0].ToString()) return;
                    }
                    catch { }
                    if (cb_Folder.FindStringExact(strTmp) != -1) cb_Folder.Items.Remove(strTmp);
                    cb_Folder.Items.Insert(0, strTmp);
                    cb_Folder.SelectedIndex = 0;
                }
                else
                {
                    return;
                }

            }
        }
        private void OpenFile_NP()
        {
            using (var fbd = new OpenFileDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                {
                    Notepad.Text = fbd.FileName;
                }
                else
                {
                    return;
                }

            }
        }
        private bool IsExistFolder(string path)
        {
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Folder is not exist. Select Folder a gain.");
                cb_Folder.Text = "";
                return false;
            }
        }

        private void CreateIndex(String indexDir, FileInfo[] files)
        {

            cnt_thread = files.Length < Decimal.ToInt32(numUpDown.Value) ? files.Length : Decimal.ToInt32(numUpDown.Value);
            lucene_Indexers = new List<Lucene_Indexer>();
            m_thread = new List<Thread>();
            for (int i = 0; i < cnt_thread; i++)
            {
                Lucene_Indexer lucene_Indexer = new Lucene_Indexer(i, cnt_thread, indexDir + "\\Index_" + i.ToString(), files);
                Thread _thread = null;
                lucene_Indexers.Add(lucene_Indexer);
                m_thread.Add(_thread);
            }

            foreach (Lucene_Indexer luceneindexer in lucene_Indexers)
            {
                luceneindexer.IndexInfo += new Lucene_Indexer.IndexInforEventHandler(Indexer_FoundInfo);
                luceneindexer.ThreadEnded += new Lucene_Indexer.ThreadEndedEventHandler(Indexer_ThreadEnded);
            }
            //Lucene_Indexer.Instance.CreateIndex(indexDir, files);
            for (int i = 0; i < cnt_thread; i++)
            {
                m_thread[i] = new Thread(new ThreadStart(lucene_Indexers[i].CreateIndex_Thread));
                m_thread[i].Start();
                m_thread[i].Name = "m_th" + i.ToString();
            }
        }

        private void ListView()
        {
            cb_ListView.Items.Clear();
            List<String> Views = Cleartool.Instance.ListView_by_ID(txb_IdView.Text);
            foreach (string item in Views)
            {
                cb_ListView.Items.Add(item);
                autoComplete_view.Add(item);
            }
            cb_ListView.DroppedDown = true;
        }
        private void RemoveFolder()
        {
            if (tv_CheckedFolder.SelectedNode != null)
            {
                tv_CheckedFolder.Nodes.Remove(tv_CheckedFolder.SelectedNode);
            }
        }
        private void AddFolder(TreeNodeCollection treeNodeCollection_source, TreeNodeCollection treeNodeCollection_target)
        {
            try
            {
                foreach (TreeNode source_tn in treeNodeCollection_source)
                {                    
                    if (source_tn.StateImageIndex == (int)CheckedState.Checked || source_tn.StateImageIndex == (int)CheckedState.Mixed)
                    {
                        TreeNode target_tn = treeNodeCollection_target.Add(source_tn.Text);
                        target_tn.Tag = source_tn.Tag + "|" + (CheckedState)(source_tn.StateImageIndex);
                        target_tn.ImageIndex = source_tn.ImageIndex;
                        target_tn.StateImageIndex = (int)CheckedState.Checked;
                        AddFolder(source_tn.Nodes, target_tn.Nodes);
                    }
                }
            }
            catch { }
        }
        private void Searching_Local()
        {

            string dataDir = cb_Folder.Text;
            string strTmp = cb_SearchKey.Text;

            if (cb_SearchKey.FindStringExact(strTmp) != -1) cb_SearchKey.Items.Remove(cb_SearchKey.Text);
            else autoComplete_serchkey.Add(cb_SearchKey.Text);
            cb_SearchKey.Items.Insert(0, strTmp);
            cb_SearchKey.SelectedItem = strTmp;

            strTmp = dataDir;
            autoComplete_folder.Add(dataDir);
            if (cb_Folder.FindStringExact(strTmp) != -1) cb_Folder.Items.Remove(dataDir);
            else autoComplete_folder.Add(dataDir);
            cb_Folder.Items.Insert(0, strTmp);
            cb_Folder.SelectedItem = strTmp;

            TreeNode search_Node_tmp;
            search_Text = "Search for: \"" + cb_SearchKey.Text + "\"";
            search_Node_tmp = new TreeNode(search_Text);

            // tv_Results.BeginUpdate();
            tv_Results.DrawMode = TreeViewDrawMode.Normal;
            tv_Results.Nodes.Insert(0, search_Node_tmp);
            tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;

            Lucene_SearchParams.QueryMode mode;
            if (rbtn_Normal.Checked) mode = Lucene_SearchParams.QueryMode.Text;
            else if (rbtn_Regex.Checked) mode = Lucene_SearchParams.QueryMode.RegEx;
            else mode = Lucene_SearchParams.QueryMode.WildCard;

            List<String> exts_list = cb_FileExts.Text.Split(new Char[] { ';' }).ToList();

            Lucene_SearchParams lucene_searchParams = new Lucene_SearchParams(new DirectoryInfo(indexDir), Real_path(dataDir), cb_SearchKey.Text, mode, exts_list, ckb_MatchWholeWord.Checked);
            Lucene_Searcher lucene_searcher = new Lucene_Searcher(lucene_searchParams);
            lucene_searcher.Start();
            Config_TextProgressbar(pTextBar_Search, 0, lucene_searcher.Results.Count, "", ProgressBarDisplayMode.CurrProgress, Color.LimeGreen);

            if (lucene_searcher.Results.Count == 0)
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.Nodes[0].Text = tv_Results.Nodes[0].Text + " - No match with " + cb_FileExts.Text;
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
                return;
            }

            
            SearcherParams pars = new SearcherParams(files.Where(f => lucene_searcher.Results.Contains(f.FullName.ToLower())).ToList(), cb_SearchKey.Text, mode, ckb_MatchCase.Checked, ckb_MatchWholeWord.Checked);


            cnt_thread = lucene_searcher.Results.Count < Decimal.ToInt32(numUpDown.Value) ? lucene_searcher.Results.Count : Decimal.ToInt32(numUpDown.Value);
            searcher = new List<Searcher>();
            m_thread = new List<Thread>();
            for (int i = 0; i < cnt_thread; i++)
            {
                Searcher _searcher = new Searcher();
                Thread _thread = null;
                searcher.Add(_searcher);
                m_thread.Add(_thread);
            }

            foreach (Searcher search in searcher)
            {
                search.FoundInfo += new Searcher.FoundInfoEventHandler(Searcher_FoundInfo);
                search.ThreadEnded += new Searcher.ThreadEndedEventHandler(Searcher_ThreadEnded);
            }

            // Start the search thread if it is not already running:
            all_errormsg = "";
            bool check = true;
            for (int i = 0; i < cnt_thread; i++)
            {
                if (searcher[i].Start(pars, i, cnt_thread))
                {
                    if (rbtn_Normal.Checked)
                    {
                        m_thread[i] = new Thread(new ThreadStart(searcher[i].SearchThread));
                    }
                    else
                    {
                        m_thread[i] = new Thread(new ThreadStart(searcher[i].SearchThread_RegEx));
                    }
                    m_thread[i].Start();
                    m_thread[i].Name = "m_th" + i.ToString();
                }
                else
                {
                    check = false;
                    MessageBox.Show("The searcher is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
            }
            if (check)
            {
                DisableButtons_Searching();
            }
        }

        private void Searching_Server()
        {
            Lucene_SearchParams.QueryMode mode;
            if (rbtn_Normal.Checked) mode = Lucene_SearchParams.QueryMode.Text;
            else if (rbtn_Regex.Checked) mode = Lucene_SearchParams.QueryMode.RegEx;
            else mode = Lucene_SearchParams.QueryMode.WildCard;

            List<string> listResults = new List<string>();


            string strTmp = cb_SearchKey.Text;
            if (cb_SearchKey.FindStringExact(strTmp) != -1) cb_SearchKey.Items.Remove(cb_SearchKey.Text);
            else autoComplete_serchkey.Add(cb_SearchKey.Text);
            cb_SearchKey.Items.Insert(0, strTmp);
            cb_SearchKey.SelectedItem = strTmp;
            List<String> exts_list = cb_FileExts.Text.Split(new Char[] { ';' }).ToList();

            int count = 0;
            DirectoryInfo[] IndexedInfo = new DirectoryInfo(indexBase + "\\" + cb_ListView.Text).GetDirectories();
            List<string> listpaths = new List<string>();
            GetlistPathsforSearching_server(tv_CheckedFolder.Nodes, listpaths);
            foreach (var item in listpaths)
            {
                List<DirectoryInfo> filter_dir = IndexedInfo.Where(f => f.FullName.ToLower().Contains(GetValidFolderName(item.ToLower())) || (indexBase + "\\" + cb_ListView.Text + "\\" + GetValidFolderName(item)).ToLower().Contains(f.FullName.ToLower())).ToList();
                foreach (DirectoryInfo dir in filter_dir)
                {
                    Lucene_SearchParams lucene_searchParams = new Lucene_SearchParams(dir, Real_path(item), cb_SearchKey.Text, mode, exts_list, ckb_MatchWholeWord.Checked);
                    Lucene_Searcher lucene_searcher = new Lucene_Searcher(lucene_searchParams);
                    lucene_searcher.Start();
                    listResults.AddRange(lucene_searcher.Results);
                    count++;
                }
            }
            TreeNode search_Node_tmp;
            search_Text = "Search for: \"" + cb_SearchKey.Text + "\"";
            search_Node_tmp = new TreeNode(search_Text);

            // tv_Results.BeginUpdate();
            tv_Results.DrawMode = TreeViewDrawMode.Normal;
            tv_Results.Nodes.Insert(0, search_Node_tmp);
            tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            Config_TextProgressbar(pTextBar_Search, 0, listResults.Count, "", ProgressBarDisplayMode.CurrProgress, Color.LimeGreen);

            if (listResults.Count == 0)
            {
                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.Nodes[0].Text = tv_Results.Nodes[0].Text + " - No match with " + cb_FileExts.Text;
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
                return;
            }
            SearcherParams pars = new SearcherParams(files.Where(f => listResults.Contains(f.FullName.ToLower())).ToList(), cb_SearchKey.Text, mode, ckb_MatchCase.Checked, ckb_MatchWholeWord.Checked);

            cnt_thread = listResults.Count < Decimal.ToInt32(numUpDown.Value) ? listResults.Count : Decimal.ToInt32(numUpDown.Value);
            searcher.Clear();
            m_thread.Clear();
            for (int i = 0; i < cnt_thread; i++)
            {
                Searcher _searcher = new Searcher();
                Thread _thread = null;
                searcher.Add(_searcher);
                m_thread.Add(_thread);
            }

            foreach (Searcher search in searcher)
            {
                search.FoundInfo += new Searcher.FoundInfoEventHandler(Searcher_FoundInfo);
                search.ThreadEnded += new Searcher.ThreadEndedEventHandler(Searcher_ThreadEnded);
            }

            // Start the search thread if it is not already running:
            all_errormsg = "";
            bool check = true;
            for (int i = 0; i < cnt_thread; i++)
            {
                if (searcher[i].Start(pars, i, cnt_thread))
                {
                    if (rbtn_Normal.Checked)
                    {
                        m_thread[i] = new Thread(new ThreadStart(searcher[i].SearchThread));
                    }
                    else
                    {
                        m_thread[i] = new Thread(new ThreadStart(searcher[i].SearchThread_RegEx));
                    }
                    m_thread[i].Start();
                    m_thread[i].Name = "m_th" + i.ToString();
                }
                else
                {
                    check = false;
                    MessageBox.Show("The searcher is already running.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                }
            }
            if (check)
            {
                DisableButtons_Searching();
            }
        }

        private void StopIndex()
        {
            try
            {
                real_paths_Index.Clear();
                tagFiles_Index.Clear();
                for (int i = 0; i < cnt_thread; i++)
                {
                    lucene_Indexers[i].StopIndexing();
                    m_thread[i] = null;
                }                
            }
            catch { }
        }
        private void StopSearch()
        {
            // Stop the search threads if they're running:
            try
            {
                for (int i = 0; i < cnt_thread; i++)
                {
                    searcher[i].Stop();
                    m_thread[i] = null;
                }
            }
            catch { }
        }



        private void btn_ClrResult_Click(object sender, EventArgs e)
        {
            tv_Results.BeginUpdate();
            tv_InterfaceList.BeginUpdate();
            tv_Results.Nodes.Clear();
            tv_InterfaceList.Nodes.Clear();
            tv_Results.EndUpdate();
            tv_InterfaceList.EndUpdate();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            btn_StopSearch.PerformClick();
            if (_Is_Indexing == true)
            {
                MessageBox.Show("The Index is stopped!!", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            // Remember that the window is closing,
            m_closing = true;
            // Save config values:
            Save_UserConfig();
        }


        private void cb_Folder_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isExistIndexDir = false;
            string newIndexdir = indexBase + "\\" + GetValidFolderName(cb_Folder.Text);
            if (newIndexdir == last_indexDir) return;
            if (lstIndexedDir.Count == 0)
            {
                indexDir = newIndexdir;
                return;
            }
            foreach (ExistIndexDir existDir_Indexed in lstIndexedDir)
            {
                if (newIndexdir.ToLower().Contains(existDir_Indexed.IndexDir.ToLower()))
                {
                    isExistIndexDir = true;
                    indexDir = existDir_Indexed.IndexDir;
                    files = existDir_Indexed.FileInfos;
                    string real_path = Real_path(cb_Folder.Text);
                    real_path = GetAbsolutePath(real_path);
                    FileInfo[] current_files = files.Where(f => f.FullName.ToLower().Contains(real_path.ToLower())).ToArray();

                    tStrip_stt_Tagfile.Text = "with: " + existDir_Indexed.TagFile;
                    tStrip_lbstt_Indexed.Text = current_files.Length.ToString() + " files";
                    break;
                }
                else if (existDir_Indexed.IndexDir.ToLower().Contains(newIndexdir.ToLower()))
                {
                    indexDir = newIndexdir;
                    try
                    {
                        if (!Directory.Exists(newIndexdir))
                        {
                            Directory.CreateDirectory(newIndexdir);
                        }
                        Directory.Move(existDir_Indexed.IndexDir, newIndexdir);

                    }
                    catch { }

                    if (Directory.Exists(existDir_Indexed.IndexDir))
                    {
                        Directory.Delete(existDir_Indexed.IndexDir, true);
                    }
                    existDir_Indexed.IndexDir = newIndexdir;
                }
                else
                    indexDir = newIndexdir;
            }

            if (isExistIndexDir)
            {
                btn_StopSearch.Enabled = true;
                btn_Search.Enabled = true;
                btn_Power.Enabled = true;
                Config_Button(btn_LoadFile_local, "Reload", Color.White, Color.Blue);
                tStrip_lbstt_IndexName.Text = "Indexed:";
            }
            else
            {
                btn_StopSearch.Enabled = false;
                btn_Search.Enabled = false;
                btn_Power.Enabled = false;
                Config_Button(btn_LoadFile_local, "Load", Color.White, Color.Red);
                tStrip_lbstt_IndexName.Text = "Not Indexed";
                tStrip_lbstt_Indexed.Text = "";
                tStrip_stt_Tagfile.Text = "";
            }
            last_indexDir = newIndexdir;

        }

        private void tv_Results_DoubleClick(object sender, EventArgs e)
        {
            if (tv_Results.SelectedNode != null)
            {
                try
                {
                    String path = "\"" + tv_Results.SelectedNode.Parent.Text.Substring(0, tv_Results.SelectedNode.Parent.Text.LastIndexOf(" (")) + "\"";
                    string Temp = tv_Results.SelectedNode.Text.Split(':')[1];
                    Temp = Temp.Trim();
                    int LineNum = Convert.ToInt32(Temp);
                    var line = LineNum;
                    Process.Start(Notepad.Text, path + " -n" + LineNum);
                }
                catch (Exception)
                {
                }
            }
        }
        private void tv_Results_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Font font;
            if (e.Node.Level == 0)
            {
                string[] texts = e.Node.Text.Split('\"');
                font = new Font("Courier New", 10, FontStyle.Bold);
                if (e.Node.IsSelected)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.LightBlue, Color.Blue, TextFormatFlags.NoPrefix);
                }
                else if (e.State == TreeNodeStates.Hot)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.Blue, Color.Aquamarine, TextFormatFlags.NoPrefix);
                }
                else TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.Blue, Color.LightBlue, TextFormatFlags.NoPrefix);
                if (texts[1] == cb_SearchKey.Text)
                {
                    Size s_search = TextRenderer.MeasureText(texts[1], font);
                    s_search.Width -= 8;
                    Size s = TextRenderer.MeasureText(texts[0], font);
                    if (e.Node.IsSelected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(119, 255, 187)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, texts[1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.Blue);
                    }
                    else if (e.State == TreeNodeStates.Hot)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(128, 128, 255)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, texts[1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.LightBlue);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(79, 79, 255)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, texts[1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.FromArgb(119, 255, 187));
                    }
                }

            }
            else if (e.Node.Level == 1)
            {
                font = new Font("Courier New", 9.75f, FontStyle.Bold);
                if (e.Node.IsSelected)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.FromArgb(189, 253, 192), Color.Green, TextFormatFlags.NoPrefix);
                }
                else if (e.State == TreeNodeStates.Hot)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.Green, Color.LightGreen, TextFormatFlags.NoPrefix);
                }
                else TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, Color.Green, Color.FromArgb(189, 253, 192), TextFormatFlags.NoPrefix);
            }
            else
            {
                font = new Font("Courier New", 9.5f, FontStyle.Regular, GraphicsUnit.Point);
                if (e.Node.IsSelected)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 10, e.Bounds.Height), Color.Black, Color.FromArgb(206, 226, 251), TextFormatFlags.NoPrefix);
                }
                else if (e.State == TreeNodeStates.Hot)
                {
                    TextRenderer.DrawText(e.Graphics, e.Node.Text, font, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 10, e.Bounds.Height), Color.Black, Color.FromArgb(235, 233, 254), TextFormatFlags.NoPrefix);
                }
                else TextRenderer.DrawText(e.Graphics, e.Node.Text, font, new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width + 10, e.Bounds.Height), Color.Black, Color.White, TextFormatFlags.NoPrefix);
                string[] texts = e.Node.Parent.Parent.Text.Split('\"');
                string[] texts_2;
                List<String> texts_3 = new List<string>();
                List<String> contain_strs = new List<string>();
                string node_result;
                MatchCollection matches = null;

                if (!e.Node.Parent.Parent.Tag.ToString().Contains("MatchCase"))
                {
                    if (texts[1].LastIndexOf(' ') == texts[1].Length - 1)
                    {
                        if (e.Node.Parent.Parent.Tag.ToString().Contains("Normal"))
                        {
                            texts_2 = (e.Node.Text + " ").ToLower().Replace(texts[1].ToLower(), "\r").Split('\r');
                        }
                        else if (e.Node.Parent.Parent.Tag.ToString().Contains("WildCard"))
                        {
                            string str_regex = WildCardToRegular(texts[1].ToLower());
                            string node_text = e.Node.Text.ToLower() + " ";
                            matches = Regex.Matches(e.Node.Text, str_regex);
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        else
                        {

                            string keyword_lowcase = texts[1];
                            MatchCollection matches_for_lowcase = Regex.Matches(keyword_lowcase, "[A-Z]+");
                            foreach (Match match in matches_for_lowcase)
                            {
                                keyword_lowcase = keyword_lowcase.Replace(match.Value, match.Value.ToLower());
                            }
                            matches = Regex.Matches(e.Node.Text.ToLower(), "(" + keyword_lowcase + ")");
                            string node_text = e.Node.Text.ToLower() + " ";
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        node_result = e.Node.Text + " ";
                    }
                    else
                    {
                        if (e.Node.Parent.Parent.Tag.ToString().Contains("Normal"))
                        {
                            texts_2 = e.Node.Text.ToLower().Replace(texts[1].ToLower(), "\r").Split('\r');
                        }
                        else if (e.Node.Parent.Parent.Tag.ToString().Contains("WildCard"))
                        {
                            string str_regex = WildCardToRegular(texts[1].ToLower());
                            string node_text = e.Node.Text.ToLower();
                            matches = Regex.Matches(node_text, str_regex);
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        else
                        {
                            string keyword_lowcase = texts[1];
                            MatchCollection matches_for_lowcase = Regex.Matches(keyword_lowcase, "[A-Z]+");
                            foreach (Match match in matches_for_lowcase)
                            {
                                keyword_lowcase = keyword_lowcase.Replace(match.Value, match.Value.ToLower());
                            }
                            matches = Regex.Matches(e.Node.Text.ToLower(), "(" + keyword_lowcase + ")");
                            string node_text = e.Node.Text.ToLower();
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        node_result = e.Node.Text;
                    }
                }
                else
                {
                    if (texts[1].LastIndexOf(' ') == texts[1].Length - 1)
                    {
                        if (e.Node.Parent.Parent.Tag.ToString().Contains("Normal"))
                        {
                            texts_2 = (e.Node.Text + " ").Replace(texts[1], "\r").Split('\r');
                        }
                        else if (e.Node.Parent.Parent.Tag.ToString().Contains("WildCard"))
                        {
                            string str_regex = WildCardToRegular(texts[1]);
                            string node_text = e.Node.Text + " ";
                            matches = Regex.Matches(e.Node.Text, str_regex);
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        else
                        {
                            matches = Regex.Matches(e.Node.Text, "(" + texts[1] + ")");
                            string node_text = e.Node.Text + " ";
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }

                        node_result = e.Node.Text + " ";
                    }
                    else
                    {
                        if (e.Node.Parent.Parent.Tag.ToString().Contains("Normal"))
                        {
                            texts_2 = (e.Node.Text).Replace(texts[1], "\r").Split('\r');
                        }
                        else if (e.Node.Parent.Parent.Tag.ToString().Contains("WildCard"))
                        {
                            string str_regex = WildCardToRegular(texts[1]);
                            string node_text = e.Node.Text;
                            matches = Regex.Matches(e.Node.Text, str_regex);
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        else
                        {
                            matches = Regex.Matches(e.Node.Text, "(" + texts[1] + ")");
                            string node_text = e.Node.Text;
                            foreach (Match match in matches)
                            {
                                node_text = node_text.Substring(0, node_text.IndexOf(match.Groups[1].Value)) + "\r" + node_text.Substring(node_text.IndexOf(match.Groups[1].Value) + match.Groups[1].Value.Length);
                            }
                            texts_2 = node_text.Split('\r');
                        }
                        node_result = e.Node.Text;
                    }
                }
                for (int i = 0; i < texts_2.Length; i++)
                {
                    texts_3.Add(node_result.Substring(0, texts_2[i].Length).Replace("&", "&a"));
                    if (matches == null)
                    {
                        if (i != texts_2.Length - 1)
                        {
                            node_result = node_result.Remove(0, texts_2[i].Length);
                            contain_strs.Add(node_result.Substring(0, texts[1].Length));
                            node_result = node_result.Remove(0, texts[1].Length);
                        }
                    }
                    else
                    {
                        if (i != texts_2.Length - 1)
                        {
                            node_result = node_result.Remove(0, texts_2[i].Length);
                            contain_strs.Add(node_result.Substring(0, matches[i].Groups[1].Value.Length));
                            node_result = node_result.Remove(0, matches[i].Groups[1].Value.Length);
                        }
                    }
                }
                string str_tmp = texts_3[0];

                for (int i = 1; i < texts_3.Count; i++)
                {
                    Size s_search = TextRenderer.MeasureText(contain_strs[i - 1], font);
                    s_search.Width -= 10;
                    Size s = TextRenderer.MeasureText(str_tmp.Substring(0, str_tmp.Length - 1), font);
                    if (e.Node.IsSelected)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(206, 226, 251)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, contain_strs[i - 1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.Red);
                    }
                    else if (e.State == TreeNodeStates.Hot)
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(235, 233, 254)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, contain_strs[i - 1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.Red);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 210)), new RectangleF(new Point(e.Bounds.Left + s.Width + 3, e.Bounds.Top), s_search));
                        TextRenderer.DrawText(e.Graphics, contain_strs[i - 1], font, new Point(e.Bounds.Left + s.Width, e.Bounds.Top), Color.Red);
                    }
                    str_tmp = str_tmp + contain_strs[i - 1] + texts_3[i];
                }
            }
        }
        private void tv_Results_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                tv_Results.DrawMode = TreeViewDrawMode.Normal;
                tv_Results.BeginUpdate();
                try
                {
                    tv_Results.Nodes.Remove(tv_Results.SelectedNode);
                }
                catch { }
                tv_Results.EndUpdate();
                tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
            }
        }
        private void tv_Results_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tv_Results.SelectedNode = e.Node;
                if (e.Node.Level == 1 || e.Node.Level == 2)
                {
                    tv_Results.SelectedNode.ContextMenuStrip = cms;
                }
                else if (e.Node.Level == 0)
                {
                    tv_Results.SelectedNode.ContextMenuStrip = cms_search;
                }

            }
        }

        private void this_FileChanged(object sender, object e)
        {
            //if (this.tabCtrl.InvokeRequired)
            //{
            //    SetWatcherCallback d = new SetWatcherCallback(this_FileChanged);
            //    this.Invoke(d, new object[] { sender, e });
            //}
            //else if (tabCtrl.SelectedIndex == 0)
            //{
            //    string real_path = GetAbsolutePath(Real_path(cb_Folder.Text));
            //    if (watcher.Find(x => real_path.ToLower().Contains(x.Directory.ToLower())).Ischanged)
            //    {
            //        _Is_FilesChange = true;
            //    }
            //}
            //else
            //{
            //    string real_path = "N:\\" + cb_ListView.Text + "\\";
            //    real_path = Real_path(real_path);
            //    if (watcher.Find(x => x.Directory.ToLower().Contains(real_path.ToLower()) && x.Ischanged) != null) _Is_FilesChange = true;
            //}


            //Do nothing at current version
        }

        private void Indexer_FoundInfo(FoundInfoEventArgs e)
        {
            if (!m_closing)
            {
                // Invoke the method "this_FoundInfo" through a delegate,
                // so it is executed in the same thread as MainWindow:
                this.Invoke(IndexInfo, new object[] { e });
            }
        }
        private void this_IndexInfo(FoundInfoEventArgs e)
        {
            // Update the progressBar Value:
            if (tStripPBar_Index.Value <= tStripPBar_Index.Maximum)
            {
                tStripPBar_Index.Value++;
            }
            tStrip_lbstt_Indexed.Text = tStripPBar_Index.Value.ToString() + "/" + tStripPBar_Index.Maximum.ToString();
            sttStrip.Refresh();
        }
        private void Searcher_FoundInfo(FoundInfoEventArgs e)
        {
            if (!m_closing)
            {
                // Invoke the method "this_FoundInfo" through a delegate,
                // so it is executed in the same thread as MainWindow:
                this.Invoke(FoundInfo, new object[] { e });
            }
        }
        private void this_FoundInfo(FoundInfoEventArgs e)
        {
            // Update the progressBar Value:
            pTextBar_Search.Value = e.Info;
        }
        private void Searcher_ThreadEnded(ThreadEndedEventArgs e)
        {
            if (!m_closing)
            {
                // Invoke the method "this_ThreadEnded" through a delegate,
                // so it is executed in the same thread as MainWindow:
                this.Invoke(ThreadEnded_Searcher, new object[] { e });
            }
        }
        private void Indexer_ThreadEnded(ThreadEndedEventArgs e)
        {
            if (!m_closing)
            {
                // Invoke the method "this_ThreadEnded" through a delegate,
                // so it is executed in the same thread as MainWindow:
                this.Invoke(ThreadEnded_Indexer, new object[] { e });
            }
        }
        private void this_ThreadEnded_Indexer(ThreadEndedEventArgs e)
        {
            if (e.Success)
            {
                bool check = true;
                foreach (Lucene_Indexer li in lucene_Indexers)
                {
                    if (!li.IsDone) check = false;
                }
                if (check)
                {
                    Lucene_Indexer.Instance.MergeIndexes(indexDir);                    
                    if (real_paths_Index.Count > 0 && tagFiles_Index.Count > 0)
                    {

                        FileInfo[] current_files = Lucene_Indexer.Instance.ListFile(real_paths_Index[0], tagFiles_Index[0]);
                        while (current_files.Length == 0)
                        {
                            real_paths_Index.RemoveAt(0);
                            tagFiles_Index.RemoveAt(0);
                            current_files = Lucene_Indexer.Instance.ListFile(real_paths_Index[0], tagFiles_Index[0]);
                        }
                        indexDir = indexBase + "\\" + cb_ListView.Text + "\\" + GetValidFolderName(real_paths_Index[0]);
                        Start_CreateIndex(real_paths_Index[0], current_files);
                        files.AddRange(current_files);
                        real_paths_Index.RemoveAt(0);
                        tagFiles_Index.RemoveAt(0);
                    }
                    else
                    {
                        tStrip_lbstt_IndexName.Text = "Indexed:";
                        tStrip_lbstt_Indexed.Text = files.Count + tStripPBar_Index.Maximum - tStripPBar_Index.Value + " files";
                        tStripPBar_Index.Visible = false;

                        tStrip_stt_Tagfile.Text = "with: " + tagFile;
                        tStrip_lbstt_CurrentPath.Text = "";

                        if (tabCtrl.SelectedIndex == 0)
                        {
                            lstIndexedDir.Add(new ExistIndexDir(indexDir, tagFile, files.Count, files));
                            if (btn_LoadFile_local.Text == "Load")
                            {
                                Config_Button(btn_LoadFile_local, "Reload", Color.White, Color.Blue);
                            }
                        }
                        else
                        {
                            lstIndexedDir.Add(new ExistIndexDir(cb_ListView.Text, tagFile, files.Count, files, tv_CheckedFolder));
                            if (btn_LoadFile_server.Text == "Load")
                            {
                                Config_Button(btn_LoadFile_server, "Reload", Color.White, Color.Blue);
                            }
                        }
                        foreach (Lucene_Indexer li in lucene_Indexers)
                        {
                            li.IsDone = false;
                        }
                        _Is_Indexing = false;
                        EnableButtons();
                    }
                }
            }
        }
        private void this_ThreadEnded_Searcher(ThreadEndedEventArgs e)
        {
            // Enable all buttons except stop button:

            // Show an error message if necessary:
            if (!e.Success)
            {
                cnt_threadmsg++;
                all_errormsg += e.ErrorMsg + "\r\n";
                for (int i = 0; i < cnt_thread; i++)
                {
                    m_thread[i] = null;
                    searcher[i].IsDone = false;
                }
                if (cnt_threadmsg == cnt_thread)
                {
                    MessageBox.Show(all_errormsg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cnt_threadmsg = 0;
                    Searcher.Hits_cnt = 0;
                    Searcher.Cnt = 0;
                    pTextBar_Search.VisualMode = ProgressBarDisplayMode.NoText;
                    pTextBar_Search.Value = 0;
                    EnableButtons();
                }
            }
            else
            {
                bool check = true;
                foreach (Searcher search in searcher)
                {
                    if (!search.IsDone) check = false;
                }
                if (check)
                {
                    TreeNode tn_tmp = null;
                    //progressBar.Value = 3283;
                    if (cnt_thread >= 2)
                    {
                        tn_tmp = DoMerge(searcher[1].search_Node, searcher[0].search_Node).Clone() as TreeNode;
                        for (int i = 2; i < cnt_thread; i++)
                        {
                            tn_tmp = DoMerge(searcher[i].search_Node, tn_tmp).Clone() as TreeNode;
                        }
                    }
                    else if (cnt_thread == 1)
                    {
                        tn_tmp = searcher[0].search_Node as TreeNode;
                    }
                    try
                    {
                        tv_Results.Nodes.RemoveAt(0);
                    }
                    catch { }
                    for (int i = 0; i < tv_Results.GetNodeCount(false); i++)
                    {
                        if (tv_Results.Nodes[i].IsExpanded)
                            tv_Results.Nodes[i].Collapse(true);
                    }
                    files_cnt = tn_tmp.GetNodeCount(false);
                    hits_cnt = Searcher.Hits_cnt;
                    tn_tmp.Text = search_Text + " (" + hits_cnt.ToString();
                    if (hits_cnt >= 2) tn_tmp.Text += " matches in "; else tn_tmp.Text += " match in ";
                    tn_tmp.Text += files_cnt.ToString();
                    if (files_cnt >= 2) tn_tmp.Text += " files)"; else tn_tmp.Text += " file)";
                    tn_tmp.Text += "(" + cb_FileExts.Text + ")";
                    tn_tmp.ExpandAll();
                    tv_Results.DrawMode = TreeViewDrawMode.Normal;
                    tv_Results.BeginUpdate();
                    tv_Results.Nodes.Insert(0, tn_tmp);
                    tv_Results.Nodes[0].Tag = "";
                    if (ckb_MatchCase.Checked)
                    {
                        tv_Results.Nodes[0].Tag += "MatchCase";
                    }

                    if (rbtn_Normal.Checked)
                    {
                        tv_Results.Nodes[0].Tag += "Normal";
                    }
                    else if (rbtn_Wildcard.Checked)
                    {
                        tv_Results.Nodes[0].Tag += "WildCard";
                    }
                    else if (rbtn_Regex.Checked)
                    {
                        tv_Results.Nodes[0].Tag += "RegEx";
                    }
                    tv_Results.EndUpdate();
                    tv_Results.Nodes[0].Expand();
                    //tv_Results.CollapseAll();
                    tv_Results.SelectedNode = tv_Results.Nodes[0];
                    tv_Results.DrawMode = TreeViewDrawMode.OwnerDrawText;
                    for (int i = 0; i < cnt_thread; i++)
                    {
                        m_thread[i] = null;
                        searcher[i].IsDone = false;
                    }
                    // searcher3.search_Node. = null;
                    Searcher.Hits_cnt = 0;
                    Searcher.Cnt = 0;
                    pTextBar_Search.VisualMode = ProgressBarDisplayMode.NoText;
                    pTextBar_Search.Value = 0;
                    EnableButtons();
                }
            }

        }
        private TreeNode DoMerge(TreeNode source, TreeNode target)
        {
            if (source == null || target == null) return null;
            foreach (TreeNode n in source.Nodes)
            {
                // see if there is a match in target
                var match = FindNode(n, target.Nodes); // match paths
                if (match == null)
                { // no match was found so add n to the target
                    target.Nodes.Add(n);
                }
                else
                {
                    // a match was found so add the children of match 
                    DoMerge(n, match);
                }

            }
            return target;

        }


        private TreeNode FindNode(TreeNode tn1, TreeNodeCollection tncl)
        {
            foreach (TreeNode tn in tncl)
            {
                if (tn1.Text == tn.Text) { return tn1; }
            }
            return null;
        }

        private void Config_TextProgressbar(TextProgressBar tPB, int value, int maximum, string customText, ProgressBarDisplayMode visualMode, Color color)
        {
            tPB.Value = value;
            tPB.Maximum = maximum;
            tPB.CustomText = customText;
            tPB.VisualMode = visualMode;
            tPB.ProgressColor = color;
        }

        private void rbtn_Normal_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Normal.Checked) ckb_MatchWholeWord.Enabled = true;
        }

        private void rbtn_Regex_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Regex.Checked) ckb_MatchWholeWord.Enabled = false;
        }

        private void rbtn_Wildcard_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtn_Wildcard.Checked) ckb_MatchWholeWord.Enabled = false;
        }

        private void Config_ToolStripProgressbar(ToolStripProgressBar tSPB, int value, int maximum, bool is_Visible)
        {
            tStripPBar_Index.Visible = is_Visible;
            tSPB.Maximum = maximum;
            tSPB.Value = value;
            tStrip_lbstt_Indexed.Text = value.ToString() + "/" + maximum.ToString();
        }
        private void Config_Button(Button btn, String text, Color text_color, Color back_color)
        {
            btn.Text = text;
            btn.ForeColor = text_color;
            btn.BackColor = back_color;
        }

        private void EnableButtons()
        {
            btn_ChooseFolder.Enabled = true;
            cb_Folder.Enabled = true;
            btn_LoadFile_local.Enabled = true;
            rbtn_Normal.Enabled = true;
            rbtn_Regex.Enabled = true;
            rbtn_Wildcard.Enabled = true;
            btn_StopSearch.Enabled = false;
            btn_Search.Enabled = true;
            btn_Power.Enabled = true;
            ckb_MatchCase.Enabled = true;
            if (rbtn_Normal.Checked) ckb_MatchWholeWord.Enabled = true;
            numUpDown.Enabled = true;
            btn_ListView.Enabled = true;
            btn_LoadFile_local.Enabled = true;
            btn_LoadFile_server.Enabled = true;
            txb_IdView.Enabled = true;
            btn_Add.Enabled = true;
            btn_Remove.Enabled = true;
            cb_ListView.Enabled = true;
            tabCtrl.Enabled = true;
        }

        private void DisableButtons_Indexing()
        {
            btn_ChooseFolder.Enabled = false;
            cb_Folder.Enabled = false;
            btn_StopSearch.Enabled = true;
            btn_Search.Enabled = false;
            btn_Power.Enabled = false;
            numUpDown.Enabled = false;
            btn_ListView.Enabled = false;
            btn_LoadFile_local.Enabled = false;
            btn_LoadFile_server.Enabled = false;
            txb_IdView.Enabled = false;
            btn_Add.Enabled = false;
            btn_Remove.Enabled = false;
            cb_ListView.Enabled = false;
            tabCtrl.Enabled = false;
        }
        private void DisableButtons_Searching()
        {
            btn_ChooseFolder.Enabled = false;
            cb_Folder.Enabled = false;
            btn_LoadFile_local.Enabled = false;
            rbtn_Normal.Enabled = false;
            rbtn_Regex.Enabled = false;
            rbtn_Wildcard.Enabled = false;
            btn_StopSearch.Enabled = true;
            btn_Search.Enabled = false;
            btn_Power.Enabled = false;
            ckb_MatchCase.Enabled = false;
            ckb_MatchWholeWord.Enabled = false;
            numUpDown.Enabled = false;
            tabCtrl.Enabled = false;
        }

        private static String WildCardToRegular(String value)
        {
            if (value.IndexOf('*') != 0) value = "*" + value;
            if (value.LastIndexOf('*') != value.Length) value += "*";
            return "(" + Regex.Escape(value).Replace("\\?", "[\\w]").Replace("\\*", "[\\w]*") + ")";
        }

        private string Real_path(string path)
        {
            string pathsubst = Path.Combine(Application.UserAppDataPath, "subst.txt");
            Cmd.Instance.Run_cmd("/c subst > " + pathsubst);
            String[] Line = File.ReadAllLines(pathsubst);
            if (Line.Length != 0)
            {
                foreach (string line in Line)
                {
                    if (line.ToLower().Substring(0, 3) == path.ToLower().Substring(0, 3))
                    {
                        path = path.Replace(path.Substring(0, 2), line.Substring(8));
                        break;
                    }
                }
            }
            return path;
        }

        private void sContainerForm_Panel1_SizeChanged(object sender, EventArgs e)
        {
            if (tabCtrl.SelectedIndex == 1)
            {
                split_form_Height = sContainerForm.Panel1.Height;
            }
        }

        public TreeNode LoadDirectory(string Dir)
        {
            DirectoryInfo di = new DirectoryInfo(Dir);
            TreeNode tds = new TreeNode(di.Name);
            tds.Tag = di.FullName;
            tds.ImageIndex = 0;
            tds.StateImageIndex = 0;
            LoadFiles(Dir, tds);
            LoadSubDirectories(Dir, tds);
            return tds;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (tabCtrl.SelectedIndex == 0)
            {
                sContainerForm.SplitterDistance = 100;
                sContainerForm.IsSplitterFixed = true;
            }
            else
            {
                sContainerForm.SplitterDistance = split_form_Height;
                sContainerForm.IsSplitterFixed = false;
            }
            tStripPBar_Index.Size = new Size((int)(sttStrip.Width * 0.1), tStripPBar_Index.Height);
        }

        private void cb_SearchKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Search.PerformClick();
            }
        }

        private void cb_FileExts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_Search.PerformClick();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
                if (Directory.Exists(indexBase))
                {
                    Directory.Delete(indexBase, true);
                }

        }

        private void LoadSubDirectories(string dir, TreeNode td)
        {
            // Get all subdirectories  
            List<string> subdirectoryEntries = DirectoryAlternative.EnumerateDirectories(dir).ToList();
            // Loop through them to see if they have any other subdirectories  
            foreach (string subdirectory in subdirectoryEntries)
            {

                DirectoryInfo di = new DirectoryInfo(subdirectory);
                TreeNode tds = td.Nodes.Add(di.Name);
                tds.ImageIndex = 0;
                tds.StateImageIndex = tds.Parent.StateImageIndex;
                tds.Tag = di.FullName;
                //LoadFiles(subdirectory, tds);
                //LoadSubDirectories(subdirectory, tds);

            }
        }

        private bool LoadSingleFolder()
        {
            if (!Directory.Exists(indexDir))
            {
                Directory.CreateDirectory(indexDir);
            }

            f_AskTagFile f1 = new f_AskTagFile();
            f1.ShowDialog();
            tagFile = f1.tagfile;
            if (tagFile == "")
            {
                return false;
            }
            string real_path = Real_path(cb_Folder.Text);
            real_path = GetAbsolutePath(real_path);
            if (watcher.Find(x => real_path.Contains(x.Directory)) == null)
            {
                if (watcher.Find(x => x.Directory.Contains(real_path)) != null)
                {
                    watcher.Remove(watcher.Find(x => x.Directory.Contains(real_path)));
                }
                if (this.FileChange == null) this.FileChange += new FileChangeHandler(this_FileChanged);
                watcher.Add(new Watcher(real_path, tagFile, FileChange));
                watcher[watcher.Count - 1].StartWatch();
            }
            files = Lucene_Indexer.Instance.ListFile(real_path, tagFile).ToList();
            if (files.Count == 0)
            {
                MessageBox.Show("Directory has no files!!", "Warning!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            Start_CreateIndex(real_path, files.ToArray());
            return true;
        }

        private bool LoadMultiFolder(string dir, bool firstFolder)
        {
            if (!Directory.Exists(indexDir))
            {
                Directory.CreateDirectory(indexDir);
            }
            if (firstFolder == true)
            {
                f_AskTagFile f1 = new f_AskTagFile();
                f1.ShowDialog();
                tagFile = f1.tagfile;
            }
            if (tagFile == "")
            {
                return false;
            }
            string real_path = Real_path(dir);
            if (watcher.Find(x => real_path.Contains(x.Directory)) == null)
            {
                if (watcher.Find(x => x.Directory.Contains(real_path)) != null)
                {
                    watcher.Remove(watcher.Find(x => x.Directory.Contains(real_path)));
                }
                if (this.FileChange == null) this.FileChange += new FileChangeHandler(this_FileChanged);
                watcher.Add(new Watcher(real_path, tagFile, FileChange));
                watcher[watcher.Count - 1].StartWatch();
            }
            real_paths_Index.Add(real_path);
            tagFiles_Index.Add(tagFile);
            return true;
        }
        private void Start_CreateIndex(string path, FileInfo[] files)
        {
            Config_ToolStripProgressbar(tStripPBar_Index, 0, files.Length, true);
            tStrip_lbstt_CurrentPath.Text = path;
            tStrip_lbstt_IndexName.Text = "Indexing:";
            tStrip_stt_Tagfile.Text = "";
            CreateIndex(indexDir, files);
        }
        private void LoadFiles(string dir, TreeNode td)
        {
            List<string> Files = DirectoryAlternative.EnumerateFiles(dir, "*.*").ToList();
            // Loop through them to see files  
            foreach (string file in Files)
            {
                FileInfo fi = new FileInfo(file);
                TreeNode tds = td.Nodes.Add(fi.Name);
                tds.Tag = fi.FullName;
                tds.ImageIndex = 1;
            }
        }

        private void cb_Folder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strTmp = cb_Folder.Text;
                if (strTmp == "") return;
                if (cb_Folder.FindStringExact(strTmp) != -1) cb_Folder.Items.Remove(strTmp);
                cb_Folder.Items.Insert(0, strTmp);
                cb_Folder.SelectedIndex = 0;
                autoComplete_folder.Add(strTmp);
            }
        }

        private void cb_Folder_Leave(object sender, EventArgs e)
        {
            string strTmp = cb_Folder.Text;
            if (strTmp == "") return;
            try
            {
                if (strTmp == cb_Folder.Items[0].ToString()) return;
                if (cb_Folder.FindStringExact(strTmp) != -1) cb_Folder.Items.Remove(strTmp);
                cb_Folder.Items.Insert(0, strTmp);
                cb_Folder.SelectedIndex = 0;
                autoComplete_folder.Add(strTmp);
            }
            catch { }
        }

        private void tv_CheckedFolder_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode td in e.Node.Nodes)
            {
                try
                {
                    if (td.GetNodeCount(false) == 0)
                    {
                        string Dir = td.Tag.ToString().Split('|')[0];
                        LoadSubDirectories(Dir, td);
                    }
                }
                catch { }
            }
        }

        private string GetAbsolutePath(string path)
        {
            path = path.Replace("/", "\\");
            if (path.LastIndexOf("\\") < path.Length - 1)
            {
                path += "\\";
            }
            return path;
        }

        private void cb_ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strTmp = cb_ListView.Text;
                if (strTmp == "") return;
                if (cb_ListView.FindStringExact(strTmp) != -1) cb_ListView.SelectedIndex = cb_ListView.FindStringExact(strTmp);
            }
        }

        private void cb_SearchKey_TextChanged(object sender, EventArgs e)
        {
            tv_Results.Refresh();
        }

        private void btn_Power_Click(object sender, EventArgs e)
        {
            try
            {
                if (tv_Results.SelectedNode.Level == 0)
                {
                    Tracking(tv_Results.SelectedNode);
                }
                else if (tv_Results.SelectedNode.Level == 1)
                {
                    Tracking(tv_Results.SelectedNode.Parent);
                }
                else
                {
                    Tracking(tv_Results.SelectedNode.Parent.Parent);
                }
            }
            catch {
                MessageBox.Show("Please select the node that you want to filter!", "Warning!!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool CheckLogic(string line, string Keyword)
        {
            string[] SplitString = new string[] { Keyword };
            string[] temp = line.Split(SplitString, StringSplitOptions.None);
            bool Logic;
            Logic = (temp.Last().Contains("=") && !temp.Last().Contains("==") && !temp.Last().Contains("!=") && !temp.Last().Contains(">=") && !temp.Last().Contains("<=")) || line.Contains("W_" + Keyword) || line.Contains("SET_" + Keyword);
            return Logic;
        }
        private void Tracking(TreeNode results)
        {
            try
            {
                List<string> LineTrue = new List<string>();
                List<string> Results = new List<string>();
                string tempp = "";
                bool Logic = false;
                string[] Keyword = results.Text.Replace("SET_","").Replace("W_","").Split('\"');
                //resultsList.Items.Add("Search Level: " + SearchLevel.ToString());
                TreeNode treenode_tmp = new TreeNode(Keyword[1] + ":")
                {
                    //treenode_tmp.BackColor = Color.FromArgb(255, 255, 210);
                    ForeColor = Color.Red,
                    NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold)
                };
                for (int i = 0; i < results.GetNodeCount(false); i++)
                {
                    Results.Add(@results.Nodes[i].Text.Substring(0, results.Nodes[i].Text.LastIndexOf(" (")));
                }
                int index_tree = 0;
                for (int f = 0; f < Results.Count; f++)
                {
                    string t = Results[f];
                    //MessageBox.Show(t);
                    if (File.Exists(t) && t != tempp)
                    {
                        tempp = t;
                        //MessageBox.Show(t);
                        string FilePath = t;
                        string[] Lines = File.ReadAllLines(t);

                        for (int i = 0; i < Lines.Length; i++)
                        {
                            if (Lines[i].Contains(Keyword[1]))
                            {
                                //MessageBox.Show(SearchLevel.ToString() + Lines[i]);
                                //MessageBox.Show(temp[1]);
                                Logic = CheckLogic(Lines[i], Keyword[1]);
                                if (Logic)
                                {
                                    string line = (i + 1).ToString();
                                    string content = Lines[i];
                                    content = content.Replace('\t', ' ');
                                    while (content.IndexOf(" ") == 0) content = content.Remove(0, 1);
                                    //    resultsList.Items.Add("             Line: " + line + " : " + content);
                                    try
                                    {
                                        content = content.Substring(0, 200);
                                    }
                                    catch { }
                                    //MessageBox.Show(content);
                                    LineTrue.Add("  Line: " + line + " : " + content);
                                }
                            }
                        }
                        if (LineTrue.Count > 0)
                        {
                            // resultsList.Items.Add(FilePath);
                            /*if (treenode_tmp.Nodes[0].Nodes) */
                            treenode_tmp.Nodes.Add(FilePath);
                            treenode_tmp.Nodes[treenode_tmp.Nodes.Count - 1].ForeColor = Color.DarkGreen;
                            treenode_tmp.Nodes[treenode_tmp.Nodes.Count - 1].NodeFont = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold);
                            for (int h = 0; h < LineTrue.Count; h++)
                            {
                                string te = LineTrue[h];
                                // resultsList.Items.Add(LineTrue[h]);
                                treenode_tmp.Nodes[index_tree].Nodes.Add(LineTrue[h]);
                                /*
                                ////////////////////////////////////////////////////////////////////////////////////////
                                int j = 0;
                                //MessageBox.Show(te + "#" + oldKeyword);
                                //MessageBox.Show(Logic.ToString());
                                Logic = CheckLogic(te, Keyword);
                               // MessageBox.Show(te);
                               //MessageBox.Show(Logic.ToString());
                                if (te.Contains("(") && te.Contains(")") || Logic)
                                    {
                                    if (Logic == true && !te.Contains("W_" + Keyword))
                                    {
                                        te = te.Split('=').Last();
                                        te = te.Split(';').First();
                                        //MessageBox.Show(te);
                                        if (te.Contains("UI"))
                                        {
                                            te = te.Split(new string[] { "UI" }, StringSplitOptions.None).Last();
                                            te = te.Substring("16)".Length);
                                        }
                                        need[j] = te;
                                    }
                                    else
                                    {
                                        if (te.Contains("UI"))
                                        {
                                            te = te.Split(new string[] { "UI" }, StringSplitOptions.None).Last();
                                            te = te.Substring("16)".Length);
                                        }
                                        string[] tem = te.Split('(');
                                        //MessageBox.Show("1" + tem.Last());
                                        tem = tem.Last().Split(')');
                                        //MessageBox.Show("1" + tem.First());
                                        tem = tem.First().Split(',');
                                        //MessageBox.Show(tem[tem.Length-1]);
                                        //tem = tem[tem.Length-1].Split(' ');
                                        //MessageBox.Show("1" + tem.First());
                                        need[j] = tem.First();
                                        //MessageBox.Show(tem[tem.Length-1]);
                                        for (int i = 0; i < tem.Length; i++)
                                        {
                                            if (need.Length < tem[i].Length)
                                            {
                                                need[j] = tem[i];
                                                j++;
                                            }
                                        }
                                    }

                                    if (need[j] != null)
                                    {
                                        need[j] = need[j].Trim();
                                        Keyword = need[j];
                                        if (Keyword.Length > 19)
                                        {
                                            //MessageBox.Show(need[j]);
                                            //MessageBox.Show(Keyword.Length.ToString());
                                            if (!oldKeyword.Contains(Keyword))
                                            {
                                                //MessageBox.Show(need[j]);
                                                //SearchLevel++;
                                                resultsList.Items.Add("");
                                                resultsList.Items.Add("Keyword: " + need[j]);
                                                //resultsList.Items.Add("");
                                                tempp = "";
                                                oldKeyword.Add(Keyword);
                                                // MessageBox.Show(Keyword);
                                                Tracking(Keyword);
                                                //goto SmartSearch;
                                            }
                                        }
                                    }
                                    }
                                ////////////////////////////////////////////////////////////////////////////////////////
                                */

                            }
                            index_tree++;
                            LineTrue.Clear();
                        }
                    }
                }
                treenode_tmp.ExpandAll();
                tv_InterfaceList.BeginUpdate();
                for (int i = 0; i < tv_InterfaceList.GetNodeCount(false); i++)
                {
                    if (tv_InterfaceList.Nodes[i].IsExpanded)
                        tv_InterfaceList.Nodes[i].Collapse(true);
                }
                tv_InterfaceList.Nodes.Insert(0, treenode_tmp);              
                tv_InterfaceList.Nodes[0].Expand();
                tv_InterfaceList.EndUpdate();

            }
            catch { }
        }

        private void tv_InterfaceList_DoubleClick(object sender, EventArgs e)
        {
            if (tv_InterfaceList.SelectedNode != null)
            {
                try
                {
                    if (tv_InterfaceList.SelectedNode.Level == 2)
                    {
                        String path = "\"" + tv_InterfaceList.SelectedNode.Parent.Text + "\"";
                        string Temp = tv_InterfaceList.SelectedNode.Text.Split(':')[1];
                        Temp = Temp.Trim();
                        int LineNum = Convert.ToInt32(Temp);
                        var line = LineNum;
                        Process.Start(Notepad.Text, path + " -n" + LineNum);
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private string GetValidFolderName(string path)
        {
            path = GetAbsolutePath(path);
            string result = Regex.Replace(path, "\\W{1,}", "♫");
            return result;
        }

        private void tv_InterfaceList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                tv_InterfaceList.SelectedNode = e.Node;

                tv_InterfaceList.SelectedNode.ContextMenuStrip = cms_interfaces;

            }
        }

        private void tv_InterfaceList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {

                tv_InterfaceList.BeginUpdate();
                try
                {
                    tv_InterfaceList.Nodes.Remove(tv_InterfaceList.SelectedNode);
                }
                catch { }
                tv_InterfaceList.EndUpdate();
            }
        }

        private string ProcessTagFile(string oldFile, string newFile)
        {
            if (oldFile == "*.*" | newFile == "*.*")
            {
                return "*.*";
            }
            List<string> lstOldTagFile = oldFile.Split(new char[] { ';', ' ', ',', '/', '-' }).ToList();
            List<string> lstNewTagFile = newFile.Split(new char[] { ';', ' ', ',', '/', '-' }).ToList();
            foreach (string item in lstNewTagFile)
            {
                if (lstOldTagFile.Contains(item))
                {
                    continue;
                }
                lstOldTagFile.Add(item);
            }
            return string.Join(";", lstOldTagFile);
        }
        #endregion
    }
}
