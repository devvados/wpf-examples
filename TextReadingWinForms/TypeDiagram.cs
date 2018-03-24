using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace TextReadingWinForms
{
    public partial class TypeDiagram : Form
    {
        public TypeDiagram()
        {
            InitializeComponent();
            foreach (SeriesChartType element in Enum.GetValues(typeof(SeriesChartType)))
                listBox1.Items.Add(Convert.ToString(element));
        }
        
        public string GetType()
        {
            return Convert.ToString(listBox1.SelectedItem);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
