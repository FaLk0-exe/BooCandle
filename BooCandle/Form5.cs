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
    public partial class Form5 : Form
    {
        Receipt receipt;
        public Form5(Receipt r)
        {
            StartPosition = FormStartPosition.CenterScreen;
            receipt = r;
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            textBox1.Text = receipt.Name;
            richTextBox1.Text = receipt.Description;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && richTextBox1.Text != "")
            {
                if (!BooRepository.IsReceiptExist(textBox1.Text))
                {
                    receipt.Description = richTextBox1.Text;
                    receipt.Name = textBox1.Text;
                    if (BooRepository.EditReceipt(receipt))
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
    }
}
