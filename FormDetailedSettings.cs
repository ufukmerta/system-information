using System;
using System.Windows.Forms;

namespace WFASystemInformation
{
    public partial class FormDetailedSettings : Form
    {
        public FormDetailedSettings()
        {
            InitializeComponent();
        }

        private void clb_Detailed_MouseUp(object sender, MouseEventArgs e)
        {
            if (!clb_Detailed.GetItemChecked(4))
            {
                clb_Detailed.SetItemChecked(5, false);
            }
        }

        private void btn_SelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < clb_Detailed.Items.Count; i++)
            {
                clb_Detailed.SetItemChecked(i, true);
            }
        }
    }
}
