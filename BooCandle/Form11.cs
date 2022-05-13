using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BooCandle
{
    public partial class Form11 : Form
    {
        Order order;
        public Form11(Order order)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.order = order;
            InitializeComponent();
        }

        public void LoadOrderList()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(BooRepository.GetOrderListById(order.Id).ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form11_Load(object sender, EventArgs e)
        {
            LoadOrderList();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
