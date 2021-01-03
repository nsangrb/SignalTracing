using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SignalTracing
{
    public partial class f_Add_Vobs : Form
    {
        private List<String> Added_vobs = new List<string>();
        AutoCompleteStringCollection autoComplete_vobs = new AutoCompleteStringCollection();
        public List<String> Mounted_vobs { get; } = new List<string>();
        public f_Add_Vobs()
        {
            InitializeComponent();
        }

        private void cb_Vobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!lB_VobstoMount.Items.Contains(cb_Vobs.Text))
            {
                lB_VobstoMount.Items.Add(cb_Vobs.Text);
                Added_vobs.Add(cb_Vobs.Text);
                autoComplete_vobs.Remove(cb_Vobs.Text);
                cb_Vobs.Items.Remove(cb_Vobs.Text);
            }
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                Added_vobs.Remove(lB_VobstoMount.SelectedItems.ToString());
                cb_Vobs.Items.Add(lB_VobstoMount.SelectedItems.ToString());
                autoComplete_vobs.Add(lB_VobstoMount.SelectedItems.ToString());
                lB_VobstoMount.Items.RemoveAt(lB_VobstoMount.SelectedIndex);
                
            }
            catch { }
        }

        private void btn_RmAll_Click(object sender, EventArgs e)
        {
            foreach (String vob in Added_vobs)
            { 
                cb_Vobs.Items.Add(vob);
                autoComplete_vobs.Add(vob);
            }
            Added_vobs.Clear();
            lB_VobstoMount.Items.Clear();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            foreach(String vob in lB_VobstoMount.Items)
            {
                tStrip_stt_Mount.Text = "Mounting " + vob + "...";
                Cleartool.Instance.Mount_vob(vob);
                if (!Mounted_vobs.Contains(vob))
                {
                    Mounted_vobs.Add(vob);
                }
            }
            tStrip_stt_Mount.Text = "Mounted all ";
        }

        public void Load_AllVobs(List<String> except)
        {
           List<String> all_vobs = Cleartool.Instance.List_All_vobs();
           foreach (String vob in all_vobs)
            {
                if (except.Find(x => x.ToString() == vob.Substring(1)) == null)
                {
                    cb_Vobs.Items.Add(vob.Substring(1));
                    autoComplete_vobs.Add(vob.Substring(1));
                }
            }
        }

        private void f_Add_Vobs_Load(object sender, EventArgs e)
        {
            cb_Vobs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cb_Vobs.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cb_Vobs.AutoCompleteCustomSource = autoComplete_vobs;
        }

        private void cb_Vobs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string strTmp = cb_Vobs.Text;
                if (strTmp == "") return;
                if (!lB_VobstoMount.Items.Contains(cb_Vobs.Text))
                {
                    lB_VobstoMount.Items.Add(cb_Vobs.Text);
                    Added_vobs.Add(cb_Vobs.Text);
                    autoComplete_vobs.Remove(cb_Vobs.Text);
                    cb_Vobs.Items.Remove(cb_Vobs.Text);
                }
            }
        }
    }
}
