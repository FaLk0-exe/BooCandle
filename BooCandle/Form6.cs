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
    public partial class Form6 : Form
    {
        string name="";
        public Form6()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        public void LoadInfo(List<dynamic> materials)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(BooRepository.GetMaterialTypes().ToArray());
            dataGridView1.Rows.Clear();
            foreach(var r in materials)
            {
                dataGridView1.Rows.Add(r.Name, r.MaterialType);
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            LoadInfo(BooRepository.GetMaterials());
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadInfo(BooRepository.GetMaterials());
        }

        private void Form6_Activated(object sender, EventArgs e)
        {
            LoadInfo(BooRepository.GetMaterials());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadInfo(BooRepository.GetMaterials().Where
                (m=>m.Name.Contains(textBox1.Text))
                .ToList<dynamic>());
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInfo(BooRepository.GetMaterials().Where
                (m => m.MaterialType.Contains(comboBox1.SelectedItem.ToString()))
                .ToList<dynamic>());
            textBox1.Text = "";
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex==0)
            LoadInfo(BooRepository.GetMaterials().OrderBy(m=>m.Name).ToList<dynamic>());
            if(e.ColumnIndex==1)
                LoadInfo(BooRepository.GetMaterials().OrderBy(m => m.MaterialType).ToList<dynamic>());
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox1.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(name!="")
            {
                Form9 f = new Form9(BooRepository.GetMaterialByName(name));
                f.ShowDialog();
            }
            else
            {
                MessageBox.Show("Спочатку оберіть матеріал!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            ToolTip t = new ToolTip();
            t.SetToolTip(label3, "Для редагування складової натисніть на комірку з нею");
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex!=-1&&e.RowIndex!=dataGridView1.RowCount-1)
            {
                name = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form7 f = new Form7();
            f.ShowDialog();

        }
    }
}
