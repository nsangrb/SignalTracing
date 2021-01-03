using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SignalTracing
{
    public partial class f_AskTagFile : Form
    {
        public string tagfile { get; private set; }
        public f_AskTagFile()
        {
            InitializeComponent();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if (ckb_ListAll.Checked)
            {
                tagfile = "*.*";
            }
            else
            {
                tagfile = txb_Tagfile.Text;
            }
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            tagfile = "";
            this.Close();
        }

        private void ckb_ListAll_CheckedChanged(object sender, EventArgs e)
        {
            if (ckb_ListAll.Checked)
            {
                txb_Tagfile.Enabled = false;
            }
            else
            {
                txb_Tagfile.Enabled = true;
            }
        }

        private void f_AskTagFile_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tagfile == null)
            {
                tagfile = "";
            }
        }
    }
}
