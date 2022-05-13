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
    public partial class Form2 : Form
    {
        public Form2(string role)
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
            if(role!="p")
            {
                pictureBox1.Visible = true;
                label1.Visible = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
         
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Form6 f = new Form6();
            f.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form10 f = new Form10();
            f.ShowDialog();
        }
    }
}
