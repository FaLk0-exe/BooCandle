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
    public partial class Form10 : Form
    {
        public Form10()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(label3, "Для детального просмотру замовлення двічі натисніть на комірку з ним");
        }

        private void LoadOrders()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(BooRepository.GetOrderCodes());
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadOrders();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form11 f = new Form11(BooRepository.GetOrderByCode(
                Convert.ToInt32(listBox1.SelectedItem.ToString())));
            f.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form12 f = new Form12();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                var res = MessageBox.Show("Ви впевнені, що хочете відмінити замовлення?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(res==DialogResult.Yes)
                {
                    BooRepository.ChangeOrderStatusById(Convert.ToInt32(listBox1.SelectedItem.ToString()), "Завершено");
                    MessageBox.Show("Готово", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOrders();
                }
                else
                {
                    MessageBox.Show("Для відхилення замовлення спочатку оберіть код замовлення", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form13 f = new Form13(BooRepository.GetOrderByCode(Convert.ToInt32(listBox1.SelectedItem.ToString())));
            f.ShowDialog();
        }
    }
}
