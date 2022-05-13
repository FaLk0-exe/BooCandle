using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastReport;

namespace BooCandle
{
    public partial class Form12 : Form
    {
        List<Candle> candles = new List<Candle>();
        List<CandleComposition> candleComposition
            = new List<CandleComposition>();

        List<int> counts = new List<int>();
        public Form12()
        {
            StartPosition = FormStartPosition.CenterScreen;
            InitializeComponent();
        }

        void LoadCandles()
        {
            checkedListBox3.Items.Clear();
            int i = 0;
            foreach(var c in candles)
            {
                checkedListBox3.Items.Add(c.CandleCode.ToString() + " "
                    + c.Form + ", вартість: " + c.Price.ToString() + ", кількість: " + counts[i]);
                i++;
            }
        }

        void RemoveAllSelectedCandles()
        {
            foreach (int i in checkedListBox3.CheckedIndices)
            {
                candles.RemoveAt(i);
                counts.RemoveAt(i);
            }
            LoadCandles();
        }

        void LoadBasis()
        {
            listBox1.Items.Clear();
            listBox1.Items.AddRange(BooRepository.GetMaterials()
                .Where(m => m.MaterialType == "Основа")
                .Select(m=>m.Name).ToArray());
        }

        void LoadFlavours()
        {
            checkedListBox1.Items.Clear();
            checkedListBox1.Items.AddRange(BooRepository.GetMaterials().Where(m => m.MaterialType == "Ароматизатор").Select(m => m.Name).ToArray());
        }

        void LoadDye()
        {
            checkedListBox2.Items.Clear();
            checkedListBox2.Items.AddRange(BooRepository.GetMaterials().Where(m => m.MaterialType == "Барвник").Select(m => m.Name).ToArray());
        }

        private void Form12_Load(object sender, EventArgs e)
        {
            StartPosition = FormStartPosition.CenterScreen;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            LoadBasis();
            LoadDye();
            LoadFlavours();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                if (checkedListBox1.CheckedItems.Count != 0
                 && checkedListBox2.CheckedItems.Count != 0)
                {
                    if (textBox1.Text != "")
                    {
                        int code;
                        if (BooRepository.IsCandleExist())
                            code = BooRepository.GetLastCandleId() + 1;
                        else
                            code = 1;
                        Candle candle = new Candle
                        {
                            CandleCode = code,
                            CandleComposition = new List<CandleComposition>(candleComposition),
                            Price = numericUpDown2.Value,
                            Weight = Convert.ToDouble(numericUpDown3.Value),
                            Form = textBox1.Text
                        };
                        counts.Add(Convert.ToInt32(numericUpDown1.Value));
                        foreach (var item in checkedListBox1.CheckedItems)
                        {
                            candleComposition.Add(
                                new CandleComposition
                                {
                                    MaterialId = BooRepository.GetMaterialByName
                                (item.ToString()).Id
                                });
                        }
                        foreach (var item in checkedListBox2.CheckedItems)
                        {
                            candleComposition.Add(
                                new CandleComposition
                                {
                                    MaterialId = BooRepository.GetMaterialByName
                                (item.ToString()).Id
                                });
                        }
                        candleComposition.Add(new CandleComposition
                        {
                            MaterialId = BooRepository.GetMaterialByName
                                (listBox1.SelectedItem.ToString()).Id
                        });
                        candle.CandleComposition = candleComposition;
                        candleComposition.Clear();
                        candles.Add(candle);
                        LoadCandles();
                        textBox1.Text = "";
                        checkedListBox1.ClearSelected();
                        checkedListBox2.ClearSelected();
                        listBox1.ClearSelected();
                        numericUpDown1.Value = 1;
                        numericUpDown2.Value = 1;
                        numericUpDown3.Value = 1;
                    }
                    else
                    {
                        MessageBox.Show("Спочатку введіть форму!", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Спочатку оберіть хотя б один з елементів у кожній категорії!", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Спочатку оберіть тип основи!", "",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            RemoveAllSelectedCandles();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (candles.Any())
            {
                BooRepository.AddOrder(new Order { OrderDate = DateTime.Now });
                var order = BooRepository.boo.Order.ToList().Last().Id;
                int j = 0;
                foreach (var c in candles)
                {
                    foreach (var cc in c.CandleComposition)
                    {
                        cc.CandleId = c.Id;
                    }
                    BooRepository.AddCandle(c);
                    BooRepository.AddOrderList(new ListOrder
                    {
                        OrderId = order,
                        CandleId = c.Id,
                        Count = counts[j]
                    });
                }

                int i = 0;
                decimal price = 0;
                foreach(var c in candles)
                {
                    price += c.Price * counts[i];
                }
    
                Report f = Report.FromFile("Report.frx");
                f.SetParameterValue("price", price);
                f.SetParameterValue("orderCode", order.ToString());
                f.Show();
                Close();
        }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }

}
