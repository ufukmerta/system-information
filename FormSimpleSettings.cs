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
    }
}
