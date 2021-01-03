namespace SignalTracing
{
    partial class f_AskTagFile
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
            this.label1 = new System.Windows.Forms.Label();
            this.txb_Tagfile = new System.Windows.Forms.TextBox();
            this.ckb_ListAll = new System.Windows.Forms.CheckBox();
            this.btn_Ok = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(165, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choose tag file you need";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txb_Tagfile
            // 
            this.txb_Tagfile.Location = new System.Drawing.Point(42, 43);
            this.txb_Tagfile.Multiline = true;
            this.txb_Tagfile.Name = "txb_Tagfile";
            this.txb_Tagfile.Size = new System.Drawing.Size(184, 36);
            this.txb_Tagfile.TabIndex = 1;
            this.txb_Tagfile.Text = "*.c;*.h;*.py;*.yaml;*.kgt;*.kon;*.mak;*.osd";
            // 
            // ckb_ListAll
            // 
            this.ckb_ListAll.AutoSize = true;
            this.ckb_ListAll.Location = new System.Drawing.Point(42, 89);
            this.ckb_ListAll.Name = "ckb_ListAll";
            this.ckb_ListAll.Size = new System.Drawing.Size(56, 17);
            this.ckb_ListAll.TabIndex = 2;
            this.ckb_ListAll.Text = "List All";
            this.ckb_ListAll.UseVisualStyleBackColor = true;
            this.ckb_ListAll.CheckedChanged += new System.EventHandler(this.ckb_ListAll_CheckedChanged);
            // 
            // btn_Ok
            // 
            this.btn_Ok.Location = new System.Drawing.Point(46, 112);
            this.btn_Ok.Name = "btn_Ok";
            this.btn_Ok.Size = new System.Drawing.Size(75, 23);
            this.btn_Ok.TabIndex = 3;
            this.btn_Ok.Text = "OK";
            this.btn_Ok.UseVisualStyleBackColor = true;
            this.btn_Ok.Click += new System.EventHandler(this.btn_Ok_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(150, 112);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 3;
            this.btn_Cancel.Text = "CANCEL";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // f_AskTagFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(264, 143);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.btn_Ok);
            this.Controls.Add(this.ckb_ListAll);
            this.Controls.Add(this.txb_Tagfile);
            this.Controls.Add(this.label1);
            this.Name = "f_AskTagFile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Modified Tag File";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.f_AskTagFile_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txb_Tagfile;
        private System.Windows.Forms.CheckBox ckb_ListAll;
        private System.Windows.Forms.Button btn_Ok;
        private System.Windows.Forms.Button btn_Cancel;
    }
}