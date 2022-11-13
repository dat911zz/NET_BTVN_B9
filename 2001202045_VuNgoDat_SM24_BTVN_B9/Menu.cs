using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2001202045_VuNgoDat_SM24_BTVN_B9
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void menuBai1_Click(object sender, EventArgs e)
        {
            new Views.frmDanhMucHangHoa().ShowDialog();
        }

        private void menuBai2_Click(object sender, EventArgs e)
        {

        }
    }
}
