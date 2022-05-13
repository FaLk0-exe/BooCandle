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
    public partial class Form1 : Form
    {
        public Form1()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             using (var boo = new Boo())
             {
                 var user = boo.User.FirstOrDefault(u => u.Login == textBox1.Text && u.Password == textBox2.Text);
                 if (user != null)
                 {
                         Form2 f = new Form2(user.Role);
                         f.Show();
                         Hide();


                 }
                 else
                 {
                     MessageBox.Show("Користувач з такими даними не знайдений!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
             }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
