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
    public partial class Form3 : Form
    {
        List<Receipt> receipts=new List<Receipt>();
        public Form3()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        public void LoadReceipts()
        {
            listBox1.Items.Clear();
            receipts = BooRepository.GetReceipts();
            listBox1.Items.AddRange(receipts.Select(r => r.Name).ToArray());

        }
        private void Form3_Load(object sender, EventArgs e)
        {
            LoadReceipts();
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                richTextBox1.Text = receipts.First(r => r.Name == listBox1.SelectedItem.ToString()).Description;
            }
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Додати рецепт");
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Видалити рецепт");
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(button1, "Редагувати рецепт");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 f = new Form4();
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (BooRepository.GetReceiptByName(listBox1.SelectedItem.ToString()) != null)
                {
                    Form5 f = new Form5(BooRepository.GetReceiptByName(listBox1.SelectedItem.ToString()));
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть рецепт для редагування!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                var dr= MessageBox.Show("Ви впевнені, що хочете видалити рецепт?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr==DialogResult.Yes)
                {
                    if (!BooRepository.
                        DeleteReceipt(listBox1.SelectedItem.ToString()))
                        MessageBox.Show("Відбулася помилка," +
                            " зверніться до розробника", "", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("OK", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadReceipts();
                        listBox1.ClearSelected();
                    }
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть рецепт для видалення", "", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void Form3_Activated(object sender, EventArgs e)
        {
            LoadReceipts();
            richTextBox1.Text = "";
        }
    }
}
