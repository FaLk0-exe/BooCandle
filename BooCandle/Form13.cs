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
    public partial class Form13 : Form
    {
        Order o;
        public Form13(Order o)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.o = o;
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex!=-1)
            {
                BooRepository.ChangeOrderStatusById(o.Id, comboBox1.SelectedItem.ToString());
                MessageBox.Show("OK!", "",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                MessageBox.Show("Оберіть новий статус!", "",
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form13_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
