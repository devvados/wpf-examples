using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;

namespace TextReadingWinForms
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> My_Dictionary = new Dictionary<string, int>();
        TypeDiagram DialogOfTypeDiagram = new TypeDiagram();
        
        public Form1()
        {
            
            InitializeComponent();
            chart1.Series.Clear();
            chart1.Series.Add("Word");
            chart1.Series["Word"].Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold);
            chart1.Series["Word"].Color = Color.Yellow;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string my_string;
            string[] work_string;
            openFileDialog1.Title = "Выберите текстовый документ";
            openFileDialog1.InitialDirectory = @"C:\Users\SONY\Documents";
            openFileDialog1.Filter = "текстовый документ (*.txt)|*.txt";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                My_Dictionary.Clear();
                my_string = File.ReadAllText(openFileDialog1.FileName);
                work_string = my_string.Split(new char[] { ' ', '\t', '\r', '\n' });
                string[] items = { "" };
                (from x in work_string where !items.Contains(x) select x).ToArray();
                foreach (string element in work_string)
                {
                    try
                    {
                        My_Dictionary.Add(element, 1);
                    }
                    catch (ArgumentException)
                    {
                        My_Dictionary[element]++;
                    }
                }
                chart1.Visible = true;

                foreach (string key in My_Dictionary.Keys.ToList())
                    chart1.Series["Word"].Points.AddXY(key, My_Dictionary[key]);
                
                Invalidate();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (DialogOfTypeDiagram.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (SeriesChartType element in Enum.GetValues(typeof(SeriesChartType)))
                    if (Convert.ToString(element) == DialogOfTypeDiagram.GetType())
                        chart1.Series["Word"].ChartType = element;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("«Частотная обработка текста». Для выбранного текстового файла подсчитать " +
                "сколько раз каждое слово встречается в данном файле. Представьте\n" +
                "результаты в виде диаграммы, используя элемент управления Chart.\n" +
                "Примечание. Реализуйте возможность выбора через меню типа диаграммы.\n\n" +
                "Дмитриев Вадим, 5-ИНТ-105", "Задание");
        }
    }
}
