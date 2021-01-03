namespace SignalTracing
{
    partial class f_Add_Vobs
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
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_RmAll = new System.Windows.Forms.Button();
            this.cb_Vobs = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lB_VobstoMount = new System.Windows.Forms.ListBox();
            this.sttStrip = new System.Windows.Forms.StatusStrip();
            this.tStrip_stt_Mount = new System.Windows.Forms.ToolStripStatusLabel();
            this.sttStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(192, 41);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 0;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Location = new System.Drawing.Point(192, 70);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(75, 23);
            this.btn_Remove.TabIndex = 2;
            this.btn_Remove.Text = "Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(192, 12);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "Mount";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_RmAll
            // 
            this.btn_RmAll.Location = new System.Drawing.Point(192, 99);
            this.btn_RmAll.Name = "btn_RmAll";
            this.btn_RmAll.Size = new System.Drawing.Size(75, 23);
            this.btn_RmAll.TabIndex = 4;
            this.btn_RmAll.Text = "Remove All";
            this.btn_RmAll.UseVisualStyleBackColor = true;
            this.btn_RmAll.Click += new System.EventHandler(this.btn_RmAll_Click);
            // 
            // cb_Vobs
            // 
            this.cb_Vobs.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cb_Vobs.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cb_Vobs.FormattingEnabled = true;
            this.cb_Vobs.Location = new System.Drawing.Point(11, 26);
            this.cb_Vobs.Name = "cb_Vobs";
            this.cb_Vobs.Size = new System.Drawing.Size(175, 21);
            this.cb_Vobs.TabIndex = 5;
            this.cb_Vobs.SelectedIndexChanged += new System.EventHandler(this.cb_Vobs_SelectedIndexChanged);
            this.cb_Vobs.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cb_Vobs_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Select:";
            // 
            // lB_VobstoMount
            // 
            this.lB_VobstoMount.FormattingEnabled = true;
            this.lB_VobstoMount.Location = new System.Drawing.Point(11, 53);
            this.lB_VobstoMount.Name = "lB_VobstoMount";
            this.lB_VobstoMount.Size = new System.Drawing.Size(175, 69);
            this.lB_VobstoMount.TabIndex = 7;
            // 
            // sttStrip
            // 
            this.sttStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tStrip_stt_Mount});
            this.sttStrip.Location = new System.Drawing.Point(0, 126);
            this.sttStrip.Name = "sttStrip";
            this.sttStrip.Size = new System.Drawing.Size(278, 22);
            this.sttStrip.TabIndex = 8;
            this.sttStrip.Text = "statusStrip1";
            // 
            // tStrip_stt_Mount
            // 
            this.tStrip_stt_Mount.Name = "tStrip_stt_Mount";
            this.tStrip_stt_Mount.Size = new System.Drawing.Size(0, 17);
            // 
            // f_Add_Vobs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(278, 148);
            this.Controls.Add(this.sttStrip);
            this.Controls.Add(this.lB_VobstoMount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cb_Vobs);
            this.Controls.Add(this.btn_RmAll);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.btn_Remove);
            this.Controls.Add(this.btn_Cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "f_Add_Vobs";
            this.Text = "Add VOBs";
            this.Load += new System.EventHandler(this.f_Add_Vobs_Load);
            this.sttStrip.ResumeLayout(false);
            this.sttStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_RmAll;
        private System.Windows.Forms.ComboBox cb_Vobs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lB_VobstoMount;
        private System.Windows.Forms.StatusStrip sttStrip;
        private System.Windows.Forms.ToolStripStatusLabel tStrip_stt_Mount;
    }
}