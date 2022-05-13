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
    public partial class Form9 : Form
    {
        Material m;
        String startName;
        public Form9(Material m)
        {
            StartPosition = FormStartPosition.CenterScreen;
            this.m = m;
            startName = m.Name;
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            textBox1.Text = m.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == startName)
            {
                MessageBox.Show("OK");
                Close();
            }
            else
            {
                if (textBox1.Text != "")
                {
                    if (!BooRepository.GetMaterials().Any(m => m.Name == textBox1.Text))
                    {
                        m.Name = textBox1.Text;
                        if (BooRepository.EditMaterial(m))
                        {
                            MessageBox.Show("OK");
                            Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Cкладова з такою назвою вже існує!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Заповніть поле!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
