using System.IO;
//using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

//using Microsoft.Win32;

namespace DialogLab
{
    public partial class MainWindow : Window
    {
        string _file = "", _text = "", _copytext = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var sfd = new SaveFileDialog {Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"};
            sfd.ShowDialog();

            
            _file = sfd.FileName;
            File.WriteAllText(_file, _text);
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var fd = new FontDialog();
            fd.ShowDialog();
            //Font font = fd.Font;
            TextBox.FontFamily = new FontFamily(fd.Font.Name);
            TextBox.FontSize = fd.Font.Size;
            TextBox.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
            TextBox.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var ofd = new OpenFileDialog {Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*"};
            ofd.ShowDialog();
            _file = ofd.FileName;
            _text = File.ReadAllText(_file);
            TextBox.Text = _text;
        }
        private Color ColorPicker()
        {
            var cd = new ColorDialog();
            cd.ShowDialog();
            var col = new Color
            {
                A = cd.Color.A,
                B = cd.Color.B,
                R = cd.Color.R,
                G = cd.Color.G
            };
            return col;
        }
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var br = new SolidColorBrush(ColorPicker());
            TextBox.Foreground = br;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (ChbOnlyNumbers.IsChecked == true)
            {
                TextBox1.Text = _copytext;
                TextBox1.Foreground = TextBox.Foreground;
                TextBox1.FontFamily = TextBox.FontFamily;
                TextBox1.FontSize = TextBox.FontSize;
                TextBox1.FontWeight = TextBox.FontWeight;
            }
            else
            {
                TextBox1.Text = TextBox.Text;
                TextBox1.Foreground = TextBox.Foreground;
                TextBox1.FontFamily = TextBox.FontFamily;
                TextBox1.FontSize = TextBox.FontSize;
                TextBox1.FontWeight = TextBox.FontWeight;
            }
        }
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            var mdg = new MyDialog();
            mdg.ShowDialog();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            var fld = new FolderBrowserDialog();
            fld.ShowDialog();
        }

        //private void Grid_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        //{
        //    System.Windows.MessageBox.Show(sender.ToString());
        //}

        private void textBox_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var pt = e.GetPosition(this);
            TextBox1.Text = "X:" + pt.X + " Y:" + pt.Y;
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {
            foreach (var ch in TextBox.Text.Where(char.IsDigit))
                _copytext += ch;
        }

        private void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                _text = TextBox.Text;
        }
    }
}
