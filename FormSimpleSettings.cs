using System.Windows.Forms;

namespace WFASystemInformation
{
    public partial class FormSimpleSettings : Form
    {
        public FormSimpleSettings()
        {
            InitializeComponent();
        }

        private void clb_Simple_MouseUp(object sender, MouseEventArgs e)
        {
            if (!clb_Simple.GetItemChecked(4))
            {
                clb_Simple.SetItemChecked(5, false);
            }
        }

        private void btn_SelectAll_Click(object sender, System.EventArgs e)
        {
            for (int i = 0; i < clb_Simple.Items.Count; i++)
            {
                clb_Simple.SetItemChecked(i, true);
            }
        }
    }
}
