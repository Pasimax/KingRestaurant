using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Resturant
{
    public partial class invoice_view : Form
    {
        public invoice_view()
        {
            InitializeComponent();
        }

        private void invoice_view_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'Restaurant_slip.Slip' table. You can move, or remove it, as needed.
            this.SlipTableAdapter.Fill(this.Restaurant_slip.Slip);

            this.reportViewer1.RefreshReport();
        }
    }
}
