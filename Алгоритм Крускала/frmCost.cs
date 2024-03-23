using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Алгоритм_Крускала
{
    public partial class frmCost : Form
    {
        public frmCost()
        {
            InitializeComponent();
        }
        public int cost;

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (txtCost.Text == string.Empty)
            {
                MessageBox.Show("Введите целочисленное значение","Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                cost = int.Parse(txtCost.Text);
                this.Close();
            }
        }

        private void txtCost_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !Char.IsDigit(e.KeyChar);
        }
    }
}
