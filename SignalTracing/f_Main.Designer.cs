using System;

namespace SignalTracing
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ChooseFolder = new System.Windows.Forms.Button();
            this.cb_Folder = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_LoadFile_local = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numUpDown = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.ckb_MatchWholeWord = new System.Windows.Forms.CheckBox();
            this.ckb_MatchCase = new System.Windows.Forms.CheckBox();
            this.gb_SearchMode = new System.Windows.Forms.GroupBox();
            this.rbtn_Wildcard = new System.Windows.Forms.RadioButton();
            this.rbtn_Regex = new System.Windows.Forms.RadioButton();
            this.rbtn_Normal = new System.Windows.Forms.RadioButton();
            this.cb_FileExts = new System.Windows.Forms.ComboBox();
            this.cb_SearchKey = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Power = new System.Windows.Forms.Button();
            this.btn_StopSearch = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.btn_ClrResult = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_Results = new System.Windows.Forms.TreeView();
            this.tv_InterfaceList = new System.Windows.Forms.TreeView();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabPage_Local = new System.Windows.Forms.TabPage();
            this.tabPage_Server = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_LoadFile_server = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.imList = new System.Windows.Forms.ImageList(this.components);
            this.btn_ListView = new System.Windows.Forms.Button();
            this.txb_IdView = new System.Windows.Forms.TextBox();
            this.cb_ListView = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.sttStrip = new System.Windows.Forms.StatusStrip();
            this.tStrip_lbstt_Space = new System.Windows.Forms.ToolStripStatusLabel();
            this.tStrip_lbstt_IndexName = new System.Windows.Forms.ToolStripStatusLabel();
            this.tStrip_lbstt_CurrentPath = new System.Windows.Forms.ToolStripStatusLabel();
            this.tStrip_lbstt_Indexed = new System.Windows.Forms.ToolStripStatusLabel();
            this.tStripPBar_Index = new System.Windows.Forms.ToolStripProgressBar();
            this.tStrip_stt_Tagfile = new System.Windows.Forms.ToolStripStatusLabel();
            this.Notepad = new System.Windows.Forms.TextBox();
            this.btn_ChooseFolderNP_pp = new System.Windows.Forms.Button();
            this.tTip_1 = new System.Windows.Forms.ToolTip(this.components);
            this.sContainerForm = new System.Windows.Forms.SplitContainer();
            this.tv_FromServer = new SignalTracing.TriStateTreeView();
            this.tv_CheckedFolder = new SignalTracing.TriStateTreeView();
            this.pTextBar_Search = new SignalTracing.TextProgressBar();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).BeginInit();
            this.gb_SearchMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabCtrl.SuspendLayout();
            this.tabPage_Local.SuspendLayout();
            this.tabPage_Server.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.sttStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sContainerForm)).BeginInit();
            this.sContainerForm.Panel1.SuspendLayout();
            this.sContainerForm.Panel2.SuspendLayout();
            this.sContainerForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_ChooseFolder);
            this.panel1.Controls.Add(this.cb_Folder);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_LoadFile_local);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.MinimumSize = new System.Drawing.Size(80, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(830, 114);
            this.panel1.TabIndex = 0;
            // 
            // btn_ChooseFolder
            // 
            this.btn_ChooseFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChooseFolder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ChooseFolder.Location = new System.Drawing.Point(799, 7);
            this.btn_ChooseFolder.Name = "btn_ChooseFolder";
            this.btn_ChooseFolder.Size = new System.Drawing.Size(26, 25);
            this.btn_ChooseFolder.TabIndex = 2;
            this.btn_ChooseFolder.Text = "...";
            this.btn_ChooseFolder.UseVisualStyleBackColor = true;
            this.btn_ChooseFolder.Click += new System.EventHandler(this.btn_ChooseFolder_Click);
            // 
            // cb_Folder
            // 
            this.cb_Folder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_Folder.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cb_Folder.DropDownHeight = 100;
            this.cb_Folder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_Folder.FormattingEnabled = true;
            this.cb_Folder.IntegralHeight = false;
            this.cb_Folder.ItemHeight = 15;
            this.cb_Folder.Location = new System.Drawing.Point(87, 8);
            this.cb_Folder.Name = "cb_Folder";
            this.cb_Folder.Size = new System.Drawing.Size(706, 23);
            this.cb_Folder.TabIndex = 1;
            this.cb_Folder.SelectedIndexChanged += new System.EventHandler(this.cb_Folder_SelectedIndexChanged);
            this.cb_Folder.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_Folder_KeyDown);
            this.cb_Folder.Leave += new System.EventHandler(this.cb_Folder_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(2, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "ChooseFolder:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_LoadFile_local
            // 
            this.btn_LoadFile_local.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LoadFile_local.BackColor = System.Drawing.Color.Red;
            this.btn_LoadFile_local.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LoadFile_local.ForeColor = System.Drawing.Color.White;
            this.btn_LoadFile_local.Location = new System.Drawing.Point(758, 36);
            this.btn_LoadFile_local.Name = "btn_LoadFile_local";
            this.btn_LoadFile_local.Size = new System.Drawing.Size(67, 25);
            this.btn_LoadFile_local.TabIndex = 2;
            this.btn_LoadFile_local.Text = "Load";
            this.btn_LoadFile_local.UseVisualStyleBackColor = false;
            this.btn_LoadFile_local.Click += new System.EventHandler(this.btn_LoadFile_Click);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.numUpDown);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.ckb_MatchWholeWord);
            this.panel2.Controls.Add(this.ckb_MatchCase);
            this.panel2.Controls.Add(this.gb_SearchMode);
            this.panel2.Controls.Add(this.cb_FileExts);
            this.panel2.Controls.Add(this.cb_SearchKey);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.MinimumSize = new System.Drawing.Size(80, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(836, 89);
            this.panel2.TabIndex = 0;
            // 
            // numUpDown
            // 
            this.numUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numUpDown.Location = new System.Drawing.Point(777, 60);
            this.numUpDown.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDown.Name = "numUpDown";
            this.numUpDown.Size = new System.Drawing.Size(48, 20);
            this.numUpDown.TabIndex = 21;
            this.numUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label3.Location = new System.Drawing.Point(698, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Speed (1-10):";
            // 
            // ckb_MatchWholeWord
            // 
            this.ckb_MatchWholeWord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_MatchWholeWord.AutoSize = true;
            this.ckb_MatchWholeWord.Location = new System.Drawing.Point(696, 30);
            this.ckb_MatchWholeWord.Name = "ckb_MatchWholeWord";
            this.ckb_MatchWholeWord.Size = new System.Drawing.Size(135, 17);
            this.ckb_MatchWholeWord.TabIndex = 6;
            this.ckb_MatchWholeWord.Text = "Match whole word only";
            this.ckb_MatchWholeWord.UseVisualStyleBackColor = true;
            // 
            // ckb_MatchCase
            // 
            this.ckb_MatchCase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckb_MatchCase.AutoSize = true;
            this.ckb_MatchCase.Checked = true;
            this.ckb_MatchCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckb_MatchCase.Location = new System.Drawing.Point(696, 12);
            this.ckb_MatchCase.Name = "ckb_MatchCase";
            this.ckb_MatchCase.Size = new System.Drawing.Size(83, 17);
            this.ckb_MatchCase.TabIndex = 6;
            this.ckb_MatchCase.Text = "Match Case";
            this.ckb_MatchCase.UseVisualStyleBackColor = true;
            // 
            // gb_SearchMode
            // 
            this.gb_SearchMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_SearchMode.Controls.Add(this.rbtn_Wildcard);
            this.gb_SearchMode.Controls.Add(this.rbtn_Regex);
            this.gb_SearchMode.Controls.Add(this.rbtn_Normal);
            this.gb_SearchMode.Location = new System.Drawing.Point(550, 3);
            this.gb_SearchMode.Name = "gb_SearchMode";
            this.gb_SearchMode.Size = new System.Drawing.Size(134, 74);
            this.gb_SearchMode.TabIndex = 1;
            this.gb_SearchMode.TabStop = false;
            this.gb_SearchMode.Text = "SearchMode";
            // 
            // rbtn_Wildcard
            // 
            this.rbtn_Wildcard.AutoSize = true;
            this.rbtn_Wildcard.Location = new System.Drawing.Point(9, 51);
            this.rbtn_Wildcard.Name = "rbtn_Wildcard";
            this.rbtn_Wildcard.Size = new System.Drawing.Size(67, 17);
            this.rbtn_Wildcard.TabIndex = 0;
            this.rbtn_Wildcard.Text = "Wildcard";
            this.rbtn_Wildcard.UseVisualStyleBackColor = true;
            this.rbtn_Wildcard.CheckedChanged += new System.EventHandler(this.rbtn_Wildcard_CheckedChanged);
            // 
            // rbtn_Regex
            // 
            this.rbtn_Regex.AutoSize = true;
            this.rbtn_Regex.Location = new System.Drawing.Point(9, 32);
            this.rbtn_Regex.Name = "rbtn_Regex";
            this.rbtn_Regex.Size = new System.Drawing.Size(116, 17);
            this.rbtn_Regex.TabIndex = 0;
            this.rbtn_Regex.Text = "Regular Expression";
            this.rbtn_Regex.UseVisualStyleBackColor = true;
            this.rbtn_Regex.CheckedChanged += new System.EventHandler(this.rbtn_Regex_CheckedChanged);
            // 
            // rbtn_Normal
            // 
            this.rbtn_Normal.AutoSize = true;
            this.rbtn_Normal.Checked = true;
            this.rbtn_Normal.Location = new System.Drawing.Point(9, 14);
            this.rbtn_Normal.Name = "rbtn_Normal";
            this.rbtn_Normal.Size = new System.Drawing.Size(58, 17);
            this.rbtn_Normal.TabIndex = 0;
            this.rbtn_Normal.TabStop = true;
            this.rbtn_Normal.Text = "Normal";
            this.rbtn_Normal.UseVisualStyleBackColor = true;
            this.rbtn_Normal.CheckedChanged += new System.EventHandler(this.rbtn_Normal_CheckedChanged);
            // 
            // cb_FileExts
            // 
            this.cb_FileExts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_FileExts.DropDownHeight = 100;
            this.cb_FileExts.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_FileExts.FormattingEnabled = true;
            this.cb_FileExts.IntegralHeight = false;
            this.cb_FileExts.ItemHeight = 15;
            this.cb_FileExts.Items.AddRange(new object[] {
            "*.c;*.h",
            "*.kgt;*.kon",
            "*.c;*.h;*.kgt;*.kon",
            "*.mak",
            "*.osd",
            "*.c",
            "*.h",
            "*.kgt",
            "*.kon",
            "*.py",
            "*.py;*.mak",
            "*.*"});
            this.cb_FileExts.Location = new System.Drawing.Point(78, 46);
            this.cb_FileExts.Name = "cb_FileExts";
            this.cb_FileExts.Size = new System.Drawing.Size(466, 23);
            this.cb_FileExts.TabIndex = 1;
            this.cb_FileExts.Text = "*.c;*.h";
            this.cb_FileExts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_FileExts_KeyDown);
            // 
            // cb_SearchKey
            // 
            this.cb_SearchKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_SearchKey.DropDownHeight = 100;
            this.cb_SearchKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_SearchKey.FormattingEnabled = true;
            this.cb_SearchKey.IntegralHeight = false;
            this.cb_SearchKey.ItemHeight = 15;
            this.cb_SearchKey.Location = new System.Drawing.Point(78, 14);
            this.cb_SearchKey.Name = "cb_SearchKey";
            this.cb_SearchKey.Size = new System.Drawing.Size(466, 23);
            this.cb_SearchKey.TabIndex = 1;
            this.cb_SearchKey.TextChanged += new System.EventHandler(this.cb_SearchKey_TextChanged);
            this.cb_SearchKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_SearchKey_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(30, 49);
            this.label5.Margin = new System.Windows.Forms.Padding(5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 15);
            this.label5.TabIndex = 0;
            this.label5.Text = "Filter :";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 17);
            this.label4.Margin = new System.Windows.Forms.Padding(5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "FindWhat :";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Power
            // 
            this.btn_Power.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Power.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.btn_Power.Enabled = false;
            this.btn_Power.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Power.Location = new System.Drawing.Point(771, 96);
            this.btn_Power.Name = "btn_Power";
            this.btn_Power.Size = new System.Drawing.Size(69, 24);
            this.btn_Power.TabIndex = 2;
            this.btn_Power.Text = "Power";
            this.btn_Power.UseVisualStyleBackColor = false;
            this.btn_Power.Click += new System.EventHandler(this.btn_Power_Click);
            // 
            // btn_StopSearch
            // 
            this.btn_StopSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_StopSearch.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_StopSearch.Enabled = false;
            this.btn_StopSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_StopSearch.Location = new System.Drawing.Point(621, 96);
            this.btn_StopSearch.Name = "btn_StopSearch";
            this.btn_StopSearch.Size = new System.Drawing.Size(69, 24);
            this.btn_StopSearch.TabIndex = 2;
            this.btn_StopSearch.Text = "Stop";
            this.btn_StopSearch.UseVisualStyleBackColor = true;
            this.btn_StopSearch.Click += new System.EventHandler(this.btn_StopSearch_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Search.Enabled = false;
            this.btn_Search.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Search.Location = new System.Drawing.Point(696, 96);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(69, 24);
            this.btn_Search.TabIndex = 2;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // btn_ClrResult
            // 
            this.btn_ClrResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ClrResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ClrResult.Location = new System.Drawing.Point(13, 606);
            this.btn_ClrResult.Margin = new System.Windows.Forms.Padding(5);
            this.btn_ClrResult.Name = "btn_ClrResult";
            this.btn_ClrResult.Size = new System.Drawing.Size(97, 25);
            this.btn_ClrResult.TabIndex = 2;
            this.btn_ClrResult.Text = "Clear Results";
            this.btn_ClrResult.UseVisualStyleBackColor = true;
            this.btn_ClrResult.Click += new System.EventHandler(this.btn_ClrResult_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(4, 124);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_Results);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tv_InterfaceList);
            this.splitContainer1.Size = new System.Drawing.Size(833, 478);
            this.splitContainer1.SplitterDistance = 602;
            this.splitContainer1.TabIndex = 3;
            // 
            // tv_Results
            // 
            this.tv_Results.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_Results.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tv_Results.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Bold);
            this.tv_Results.HotTracking = true;
            this.tv_Results.Indent = 19;
            this.tv_Results.ItemHeight = 16;
            this.tv_Results.LabelEdit = true;
            this.tv_Results.Location = new System.Drawing.Point(0, 0);
            this.tv_Results.Name = "tv_Results";
            this.tv_Results.Size = new System.Drawing.Size(602, 478);
            this.tv_Results.TabIndex = 10;
            this.tv_Results.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.tv_Results_DrawNode);
            this.tv_Results.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_Results_NodeMouseClick);
            this.tv_Results.DoubleClick += new System.EventHandler(this.tv_Results_DoubleClick);
            this.tv_Results.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv_Results_KeyDown);
            // 
            // tv_InterfaceList
            // 
            this.tv_InterfaceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_InterfaceList.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawText;
            this.tv_InterfaceList.Location = new System.Drawing.Point(0, 0);
            this.tv_InterfaceList.Name = "tv_InterfaceList";
            this.tv_InterfaceList.Size = new System.Drawing.Size(227, 478);
            this.tv_InterfaceList.TabIndex = 0;
            this.tv_InterfaceList.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_InterfaceList_NodeMouseClick);
            this.tv_InterfaceList.DoubleClick += new System.EventHandler(this.tv_InterfaceList_DoubleClick);
            this.tv_InterfaceList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tv_InterfaceList_KeyDown);
            // 
            // tabCtrl
            // 
            this.tabCtrl.Controls.Add(this.tabPage_Local);
            this.tabCtrl.Controls.Add(this.tabPage_Server);
            this.tabCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl.Location = new System.Drawing.Point(0, 0);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(844, 146);
            this.tabCtrl.TabIndex = 4;
            this.tabCtrl.SelectedIndexChanged += new System.EventHandler(this.tabCtrl_SelectedIndexChanged);
            // 
            // tabPage_Local
            // 
            this.tabPage_Local.Controls.Add(this.panel1);
            this.tabPage_Local.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Local.Name = "tabPage_Local";
            this.tabPage_Local.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Local.Size = new System.Drawing.Size(836, 120);
            this.tabPage_Local.TabIndex = 0;
            this.tabPage_Local.Text = "Local";
            this.tabPage_Local.UseVisualStyleBackColor = true;
            // 
            // tabPage_Server
            // 
            this.tabPage_Server.Controls.Add(this.panel3);
            this.tabPage_Server.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Server.Name = "tabPage_Server";
            this.tabPage_Server.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Server.Size = new System.Drawing.Size(836, 120);
            this.tabPage_Server.TabIndex = 1;
            this.tabPage_Server.Text = "Server";
            this.tabPage_Server.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_LoadFile_server);
            this.panel3.Controls.Add(this.tableLayoutPanel1);
            this.panel3.Controls.Add(this.btn_ListView);
            this.panel3.Controls.Add(this.txb_IdView);
            this.panel3.Controls.Add(this.cb_ListView);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.MinimumSize = new System.Drawing.Size(80, 60);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(830, 114);
            this.panel3.TabIndex = 1;
            // 
            // btn_LoadFile_server
            // 
            this.btn_LoadFile_server.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_LoadFile_server.BackColor = System.Drawing.Color.Red;
            this.btn_LoadFile_server.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LoadFile_server.ForeColor = System.Drawing.Color.White;
            this.btn_LoadFile_server.Location = new System.Drawing.Point(763, 79);
            this.btn_LoadFile_server.Name = "btn_LoadFile_server";
            this.btn_LoadFile_server.Size = new System.Drawing.Size(62, 29);
            this.btn_LoadFile_server.TabIndex = 5;
            this.btn_LoadFile_server.Text = "Load";
            this.btn_LoadFile_server.UseVisualStyleBackColor = false;
            this.btn_LoadFile_server.Click += new System.EventHandler(this.btn_LoadFile_server_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 39F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.tv_FromServer, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tv_CheckedFolder, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(8, 30);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(754, 78);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.btn_Add, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.btn_Remove, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(360, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(33, 72);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // btn_Add
            // 
            this.btn_Add.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Add.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_Add.Location = new System.Drawing.Point(3, 3);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(27, 30);
            this.btn_Add.TabIndex = 3;
            this.btn_Add.Text = ">>";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btn_Remove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btn_Remove.Location = new System.Drawing.Point(3, 39);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(27, 30);
            this.btn_Remove.TabIndex = 3;
            this.btn_Remove.Text = "<<";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // imList
            // 
            this.imList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imList.ImageStream")));
            this.imList.TransparentColor = System.Drawing.Color.Transparent;
            this.imList.Images.SetKeyName(0, "folder.ico");
            this.imList.Images.SetKeyName(1, "file.ico");
            // 
            // btn_ListView
            // 
            this.btn_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ListView.Location = new System.Drawing.Point(768, 3);
            this.btn_ListView.Name = "btn_ListView";
            this.btn_ListView.Size = new System.Drawing.Size(52, 21);
            this.btn_ListView.TabIndex = 3;
            this.btn_ListView.Text = "List";
            this.btn_ListView.UseVisualStyleBackColor = true;
            this.btn_ListView.Click += new System.EventHandler(this.btn_ListView_Click);
            // 
            // txb_IdView
            // 
            this.txb_IdView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_IdView.Location = new System.Drawing.Point(709, 4);
            this.txb_IdView.Name = "txb_IdView";
            this.txb_IdView.Size = new System.Drawing.Size(53, 20);
            this.txb_IdView.TabIndex = 2;
            // 
            // cb_ListView
            // 
            this.cb_ListView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_ListView.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cb_ListView.DropDownHeight = 100;
            this.cb_ListView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_ListView.FormattingEnabled = true;
            this.cb_ListView.IntegralHeight = false;
            this.cb_ListView.ItemHeight = 13;
            this.cb_ListView.Location = new System.Drawing.Point(88, 3);
            this.cb_ListView.Name = "cb_ListView";
            this.cb_ListView.Size = new System.Drawing.Size(584, 21);
            this.cb_ListView.TabIndex = 1;
            this.cb_ListView.SelectedIndexChanged += new System.EventHandler(this.cb_ListView_SelectedIndexChanged);
            this.cb_ListView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_ListView_KeyDown);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(685, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(22, 15);
            this.label6.TabIndex = 0;
            this.label6.Text = "ID:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(5, 6);
            this.label7.Margin = new System.Windows.Forms.Padding(5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "Choose View :";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.label2.Location = new System.Drawing.Point(130, 611);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 15);
            this.label2.TabIndex = 20;
            this.label2.Text = "Notepad++ path: ";
            // 
            // sttStrip
            // 
            this.sttStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tStrip_lbstt_Space,
            this.tStrip_lbstt_IndexName,
            this.tStrip_lbstt_CurrentPath,
            this.tStrip_lbstt_Indexed,
            this.tStripPBar_Index,
            this.tStrip_stt_Tagfile});
            this.sttStrip.Location = new System.Drawing.Point(0, 784);
            this.sttStrip.Name = "sttStrip";
            this.sttStrip.Size = new System.Drawing.Size(844, 22);
            this.sttStrip.TabIndex = 21;
            this.sttStrip.Text = "sttStrip";
            // 
            // tStrip_lbstt_Space
            // 
            this.tStrip_lbstt_Space.Name = "tStrip_lbstt_Space";
            this.tStrip_lbstt_Space.Size = new System.Drawing.Size(757, 17);
            this.tStrip_lbstt_Space.Spring = true;
            // 
            // tStrip_lbstt_IndexName
            // 
            this.tStrip_lbstt_IndexName.Name = "tStrip_lbstt_IndexName";
            this.tStrip_lbstt_IndexName.Size = new System.Drawing.Size(72, 17);
            this.tStrip_lbstt_IndexName.Text = "Not Indexed";
            // 
            // tStrip_lbstt_CurrentPath
            // 
            this.tStrip_lbstt_CurrentPath.Name = "tStrip_lbstt_CurrentPath";
            this.tStrip_lbstt_CurrentPath.Size = new System.Drawing.Size(0, 17);
            // 
            // tStrip_lbstt_Indexed
            // 
            this.tStrip_lbstt_Indexed.Name = "tStrip_lbstt_Indexed";
            this.tStrip_lbstt_Indexed.Size = new System.Drawing.Size(0, 17);
            // 
            // tStripPBar_Index
            // 
            this.tStripPBar_Index.Name = "tStripPBar_Index";
            this.tStripPBar_Index.Size = new System.Drawing.Size(100, 16);
            this.tStripPBar_Index.Step = 1;
            this.tStripPBar_Index.Visible = false;
            // 
            // tStrip_stt_Tagfile
            // 
            this.tStrip_stt_Tagfile.Name = "tStrip_stt_Tagfile";
            this.tStrip_stt_Tagfile.Size = new System.Drawing.Size(0, 17);
            // 
            // Notepad
            // 
            this.Notepad.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Notepad.Location = new System.Drawing.Point(228, 608);
            this.Notepad.Name = "Notepad";
            this.Notepad.Size = new System.Drawing.Size(573, 20);
            this.Notepad.TabIndex = 19;
            // 
            // btn_ChooseFolderNP_pp
            // 
            this.btn_ChooseFolderNP_pp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ChooseFolderNP_pp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ChooseFolderNP_pp.Location = new System.Drawing.Point(807, 606);
            this.btn_ChooseFolderNP_pp.Name = "btn_ChooseFolderNP_pp";
            this.btn_ChooseFolderNP_pp.Size = new System.Drawing.Size(26, 25);
            this.btn_ChooseFolderNP_pp.TabIndex = 22;
            this.btn_ChooseFolderNP_pp.Text = "...";
            this.btn_ChooseFolderNP_pp.UseVisualStyleBackColor = true;
            this.btn_ChooseFolderNP_pp.Click += new System.EventHandler(this.btn_ChooseFolderNP_pp_Click);
            // 
            // sContainerForm
            // 
            this.sContainerForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sContainerForm.Location = new System.Drawing.Point(0, 0);
            this.sContainerForm.Name = "sContainerForm";
            this.sContainerForm.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // sContainerForm.Panel1
            // 
            this.sContainerForm.Panel1.Controls.Add(this.tabCtrl);
            this.sContainerForm.Panel1.SizeChanged += new System.EventHandler(this.sContainerForm_Panel1_SizeChanged);
            // 
            // sContainerForm.Panel2
            // 
            this.sContainerForm.Panel2.Controls.Add(this.panel2);
            this.sContainerForm.Panel2.Controls.Add(this.btn_ChooseFolderNP_pp);
            this.sContainerForm.Panel2.Controls.Add(this.pTextBar_Search);
            this.sContainerForm.Panel2.Controls.Add(this.Notepad);
            this.sContainerForm.Panel2.Controls.Add(this.btn_Power);
            this.sContainerForm.Panel2.Controls.Add(this.btn_ClrResult);
            this.sContainerForm.Panel2.Controls.Add(this.btn_StopSearch);
            this.sContainerForm.Panel2.Controls.Add(this.label2);
            this.sContainerForm.Panel2.Controls.Add(this.splitContainer1);
            this.sContainerForm.Panel2.Controls.Add(this.btn_Search);
            this.sContainerForm.Size = new System.Drawing.Size(844, 784);
            this.sContainerForm.SplitterDistance = 146;
            this.sContainerForm.TabIndex = 23;
            // 
            // tv_FromServer
            // 
            this.tv_FromServer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_FromServer.ImageIndex = 0;
            this.tv_FromServer.ImageList = this.imList;
            this.tv_FromServer.Location = new System.Drawing.Point(3, 3);
            this.tv_FromServer.Name = "tv_FromServer";
            this.tv_FromServer.SelectedImageIndex = 0;
            this.tv_FromServer.Size = new System.Drawing.Size(351, 72);
            this.tv_FromServer.TabIndex = 1;
            this.tv_FromServer.TriStateStyleProperty = SignalTracing.TriStateTreeView.TriStateStyles.Installer;
            this.tv_FromServer.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_FromServer_AfterExpand);
            this.tv_FromServer.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tv_FromServer_MouseMove);
            // 
            // tv_CheckedFolder
            // 
            this.tv_CheckedFolder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_CheckedFolder.ImageIndex = 0;
            this.tv_CheckedFolder.ImageList = this.imList;
            this.tv_CheckedFolder.Location = new System.Drawing.Point(399, 3);
            this.tv_CheckedFolder.Name = "tv_CheckedFolder";
            this.tv_CheckedFolder.SelectedImageIndex = 0;
            this.tv_CheckedFolder.Size = new System.Drawing.Size(352, 72);
            this.tv_CheckedFolder.TabIndex = 2;
            this.tv_CheckedFolder.TriStateStyleProperty = SignalTracing.TriStateTreeView.TriStateStyles.Installer;
            this.tv_CheckedFolder.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tv_CheckedFolder_AfterExpand);
            // 
            // pTextBar_Search
            // 
            this.pTextBar_Search.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pTextBar_Search.CustomText = "";
            this.pTextBar_Search.Location = new System.Drawing.Point(4, 98);
            this.pTextBar_Search.Maximum = 0;
            this.pTextBar_Search.Name = "pTextBar_Search";
            this.pTextBar_Search.ProgressColor = System.Drawing.Color.LimeGreen;
            this.pTextBar_Search.Size = new System.Drawing.Size(611, 20);
            this.pTextBar_Search.Step = 1;
            this.pTextBar_Search.TabIndex = 4;
            this.pTextBar_Search.TextColor = System.Drawing.Color.Black;
            this.pTextBar_Search.TextFont = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pTextBar_Search.VisualMode = SignalTracing.ProgressBarDisplayMode.NoText;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btn_StopSearch;
            this.ClientSize = new System.Drawing.Size(844, 806);
            this.Controls.Add(this.sContainerForm);
            this.Controls.Add(this.sttStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(550, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SignalTracing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDown)).EndInit();
            this.gb_SearchMode.ResumeLayout(false);
            this.gb_SearchMode.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabCtrl.ResumeLayout(false);
            this.tabPage_Local.ResumeLayout(false);
            this.tabPage_Server.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.sttStrip.ResumeLayout(false);
            this.sttStrip.PerformLayout();
            this.sContainerForm.Panel1.ResumeLayout(false);
            this.sContainerForm.Panel2.ResumeLayout(false);
            this.sContainerForm.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.sContainerForm)).EndInit();
            this.sContainerForm.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ChooseFolder;
        private System.Windows.Forms.ComboBox cb_Folder;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.ComboBox cb_SearchKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gb_SearchMode;
        private System.Windows.Forms.CheckBox ckb_MatchWholeWord;
        private System.Windows.Forms.CheckBox ckb_MatchCase;
        private System.Windows.Forms.RadioButton rbtn_Regex;
        private System.Windows.Forms.RadioButton rbtn_Normal;
        private System.Windows.Forms.ComboBox cb_FileExts;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_StopSearch;
        private System.Windows.Forms.Button btn_ClrResult;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv_InterfaceList;
        private System.Windows.Forms.RadioButton rbtn_Wildcard;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabPage_Local;
        private System.Windows.Forms.TabPage tabPage_Server;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cb_ListView;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_ListView;
        private System.Windows.Forms.TextBox txb_IdView;
        private System.Windows.Forms.Button btn_Power;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btn_Add;
        private TextProgressBar pTextBar_Search;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btn_Remove;
        private TriStateTreeView tv_FromServer;
        private System.Windows.Forms.Button btn_LoadFile_local;
        private System.Windows.Forms.Button btn_LoadFile_server;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numUpDown;
        private System.Windows.Forms.TreeView tv_Results;
        private System.Windows.Forms.StatusStrip sttStrip;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_lbstt_IndexName;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_lbstt_Indexed;
        private System.Windows.Forms.ToolStripProgressBar tStripPBar_Index;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_stt_Tagfile;
        private System.Windows.Forms.TextBox Notepad;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_lbstt_Space;
        private System.Windows.Forms.Button btn_ChooseFolderNP_pp;
        private System.Windows.Forms.ToolTip tTip_1;
        private System.Windows.Forms.ImageList imList;
        private System.Windows.Forms.SplitContainer sContainerForm;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_lbstt_CurrentPath;
        private TriStateTreeView tv_CheckedFolder;
    }
}

