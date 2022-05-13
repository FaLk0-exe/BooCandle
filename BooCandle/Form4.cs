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
    public partial class Form4 : Form
    {
        public Form4()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && richTextBox1.Text != "")
            {
                if (!BooRepository.IsReceiptExist(textBox1.Text))
                {
                    if (BooRepository.AddReceipt(textBox1.Text, richTextBox1.Text))
                    {
                        MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Відбулася помилка, зверніться до розробника", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Рецепт з такою назвою вже існує!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заповніть поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }
    }
}
