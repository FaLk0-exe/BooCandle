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
    public partial class Form7 : Form
    {
        public Form7()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(BooRepository.GetMaterialTypes().ToArray());
        }

  

        private void Form7_Activated(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(BooRepository.GetMaterialTypes().ToArray());

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.SelectedIndex != -1)
            {
                if (!BooRepository.GetMaterials().Any(m => m.Name == textBox1.Text))
                {
                    BooRepository.AddMaterial(new Material
                    {
                        Name = textBox1.Text,
                        MaterialTypeId = BooRepository.GetMaterialTypeByName
                    (comboBox1.SelectedItem.ToString()).Id
                    });
                    MessageBox.Show("OK!");
                    Close();
                }
                else
                {
                    MessageBox.Show("Cкладова з такою назвою вже існує!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Заповніть поля!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
