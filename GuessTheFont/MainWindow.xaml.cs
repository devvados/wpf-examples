using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GuessTheFont
{
    public partial class MainWindow : Window
    {
        string _text = "Ванька, встань-ка";
        string[] _font = new string[] { "Times New Roman", "Arial", "Courier New",
            "Lucida Console", "Tahoma", "Verdana" };
 
        byte _r, _g, _b, _r1, _g1, _b1;
        string _file = "log.txt";
        string _filescore = "score.txt";
        string _filesteps = "steps.txt";

        public static List<int> Sc = new List<int>(), Step = new List<int>();
        CheckBox[] _chb;

        int _count = 1, _newcount = 1, _proverka = 1, _color = 3, _score = 0;

        public MainWindow()
        {
            File.WriteAllText(_file, "");

            var randsize = new Random();
            var randfont = new Random();
            var randstyle = new Random();

            var randr = new Random();
            var randg = new Random();
            var randb = new Random();

            var p = randstyle.Next(1,4);
            InitializeComponent();
            using (var sw = File.AppendText(_file))
                sw.WriteLine("1 шаг:"); 
            Label1.Content = _newcount.ToString() + " шаг";
            Label7.Content = _score.ToString() + " очков";
            Tbstock.Text = _text;
            Tbstock.FontSize = randsize.Next(14,16);
            Tbstock.FontFamily = new FontFamily(_font[randfont.Next(0,_font.Length)]);

            if (p == 1 || p == 2) Tbstock.FontStyle = FontStyles.Italic;
            else if (p == 3 || p == 4) Tbstock.FontWeight = FontWeights.Bold;
                  
            foreach(var ff in System.Drawing.FontFamily.Families)
            {
                ComboBox.Items.Add(ff.Name);
            }
            for(var i = 10; i < 25; i++)
            {
                ComboBox1.Items.Add(i);
            }
            _r = Convert.ToByte(randr.Next(140,200));
            _g = Convert.ToByte(randg.Next(100,140));
            _b = Convert.ToByte(randb.Next(180,220));

            var col = Color.FromRgb(_r, _g, _b);
            var br = new SolidColorBrush(col);
            Button4.Content = _r.ToString() + " " + _g.ToString() + " " + _b.ToString();
            Button4.Background = br;
            Tbstock.Foreground = br;
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (_color > 0)
            {
                var scb = new SolidColorBrush(ColorPicker());
                ButtonColor.Background = scb;

                if (_r == _r1 && _g == _g1 && _b == _b1)
                {
                    _score++;
                    Button7.IsEnabled = false;
                    TextBoxR.IsReadOnly = true;
                    TextBoxG.IsReadOnly = true;
                    TextBoxB.IsReadOnly = true;
                    Label7.Content = _score.ToString() + " очков";
                    using (var sw = File.AppendText(_file))
                        sw.WriteLine("- Пользователь задал палитру: верно. Очки +1");
                    MessageBox.Show("Вы угадали цвет шрифта!");
                }
                else
                    using (var sw = File.AppendText(_file))
                        sw.WriteLine("- Пользователь задал палитру: неверно");

                _color--;
            }
            else
            {
                Button7.IsEnabled = false;
                TextBoxR.IsReadOnly = true;
                TextBoxG.IsReadOnly = true;
                TextBoxB.IsReadOnly = true;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Игра: 'Угадай шрифт'.\nДмитриев Вадим, 5-ИНТ-105", "О программе");
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("1. На каждый ход есть одна подсказка.\n" +
                "2. Попытаться задать цвет можно 4 раза за один ход.\n" +
                "3. После выбора шрифта, его размера и цвета, нажмите 'Применить'\n" +
                "4. Чтобы проверить, нажмите 'Начертание' и введите текст в TextBox.", "Помощь");
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
            Sc.Add(_score); Step.Add(_newcount);
            using (var sw = File.AppendText(_file))
                sw.WriteLine("- Игрок сдался!");
            using (var sw = File.AppendText(_filesteps))
                sw.WriteLine("{0}", Sc.Last());
            using (var sw = File.AppendText(_filescore))
                sw.WriteLine("{0}", Step.Last());
            var dlg = new Score();
            dlg.ShowDialog();

            Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (_count == 1)
            {
                var rand = new Random();
                var stock = Tbstock.FontFamily.ToString().Split(' ');
                var newstr = stock.Aggregate("", (current, t) => current + t);
                Tbrandchar.Text = newstr;
                var num = rand.Next(0, newstr.Length);
                Tbrandchar.Text = newstr[num].ToString();
                using (var sw = File.AppendText(_file))
                    sw.WriteLine("- Применена подсказка 'Случайная буква названия шрифта'");
                _count--;
                Button.IsEnabled = false;
            }
            else MessageBox.Show("Вы уже использовали подсказку на этом шаге!");
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (_count == 1)
            {
                if (ComboBox1.SelectedIndex != -1)
                {
                    var size = Convert.ToInt32(ComboBox1.SelectedItem.ToString());
                    Tbuser.FontSize = size;


                    if ((int)Tbuser.FontSize == (int)Tbstock.FontSize)
                    {
                        MessageBox.Show("Вы угадали шрифт!");
                        Button3.IsEnabled = false;
                        ComboBox1.IsEnabled = false;
                        _score++;
                        Label7.Content = _score.ToString() + " очков";
                        using (var sw = File.AppendText(_file))
                            sw.WriteLine("- Применена подсказка 'Сравнить размер шрифта': удачно. Очки +1");
                    }
                    else if (Tbuser.FontSize < Tbstock.FontSize)
                    {
                        MessageBox.Show("Шрифт исходного текста больше!");
                        using (var sw = File.AppendText(_file))
                            sw.WriteLine("- Применена подсказка 'Сравнить размер шрифта': тщетно");
                    }
                    else if (Tbuser.FontSize > Tbstock.FontSize)
                    {
                        MessageBox.Show("Шрифт исходного текста меньше!");
                        using (var sw = File.AppendText(_file))
                            sw.WriteLine("- Применена подсказка 'Сравнить размер шрифта': тщетно");
                    }
                    _count--;
                    Button3.IsEnabled = false;
                }
            }
            else MessageBox.Show("Вы уже использовали подсказку на этом шаге!");
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            if (_count == 1)
            {
                Tbuser.IsReadOnly = false;
                using (var sw = File.AppendText(_file))
                    sw.WriteLine("- Применена подсказка 'Начертание'");
                Button5.IsEnabled = false;
                _count--;
            }
            else MessageBox.Show("Вы уже использовали подсказку на этом шаге!");
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            _chb = new[] { CheckBox, CheckBox1 };
            Tbuser.FontFamily = new FontFamily("Segoe UI");
            Tbuser.FontSize = 12;

            if (ComboBox1.SelectedIndex != -1 && ComboBox.SelectedIndex != -1 &&
                TextBoxR.Text != "" && TextBoxG.Text != "" && TextBoxB.Text != "")
            {
                Tbuser.FontSize = Convert.ToInt32(ComboBox1.SelectedItem.ToString());
                Tbuser.FontFamily = new FontFamily(ComboBox.SelectedItem.ToString());

                foreach (var t in _chb)
                {
                    Tbuser.FontStyle = _chb[0].IsChecked == true ? FontStyles.Italic : FontStyles.Normal;
                    Tbuser.FontWeight = _chb[1].IsChecked == true ? FontWeights.Bold : FontWeights.Normal;
                }
                //System.Windows.Media.Color col = System.Windows.Media.Color.FromRgb(r, g, b);
                var br = new SolidColorBrush(ColorPicker());
                Tbuser.Foreground = br;

                using (var sw = File.AppendText(_file))
                    sw.WriteLine("- Пользователь задал шрифт и размер шрифта");
            }
            else MessageBox.Show("Нужно задать шрифт, его размер и цвет!");
        }

        private Color ColorPicker()
        {
            if (int.Parse(TextBoxR.Text) < 256 && int.Parse(TextBoxG.Text) < 256 && int.Parse(TextBoxB.Text) < 256)
            {
                _r1 = Convert.ToByte(TextBoxR.Text);
                _g1 = Convert.ToByte(TextBoxG.Text);
                _b1 = Convert.ToByte(TextBoxB.Text);
                var col = Color.FromRgb(_r1, _g1, _b1);
                return col;
            }
            else
            {
                MessageBox.Show("Цифры в палитре RGB строго < 255!");
                return new Color();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void textBoxR_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
            {
                e.Handled = true;
            }
            int i;
            if (int.TryParse(((TextBox)sender).Text + e.Text, out i) && (int.Parse(((TextBox)sender).Text + e.Text) > 255))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Tbstock.FontFamily.ToString() == Tbuser.FontFamily.ToString() &&
            Tbstock.FontSize == Tbuser.FontSize &&
            Tbstock.FontStyle == Tbuser.FontStyle &&
            Tbstock.FontWeight == Tbuser.FontWeight &&
            _r == Convert.ToInt32(TextBoxR.Text) &&
            _g == Convert.ToInt32(TextBoxG.Text) &&
            _b == Convert.ToInt32(TextBoxB.Text))
            {
                MessageBox.Show("Вы угадали все параметры! Ваш результат: " + _newcount + " шагов");
                Sc.Add(_score); Step.Add(_newcount);
                using (var sw = File.AppendText(_file))
                    sw.WriteLine("- Попытка проверить все параметры: удачно");
                using (var sw = File.AppendText(_filesteps))
                    sw.WriteLine("{0} ", Sc.Last());
                using (var sw = File.AppendText(_filescore))
                    sw.WriteLine("{0} ", Step.Last());
                
                var dlg = new Score();
                dlg.ShowDialog();

                Close();
            }
            else
            {
                MessageBox.Show("Параметры не совпали! Попробуйте на следующем шаге!");
                //score--;
                Label7.Content = _score.ToString() + " очков";
                using (var sw = File.AppendText(_file))
                    sw.WriteLine("- Попытка проверить все параметры: тщетно. Очки -1");
            }
            Button2.IsEnabled = false;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            TextBoxR.IsReadOnly = false;
            TextBoxG.IsReadOnly = false;
            TextBoxB.IsReadOnly = false;

            CheckBox.IsEnabled = true;
            CheckBox1.IsEnabled = true;

            Button.IsEnabled = true;
            Button2.IsEnabled = true;

            if (Tbuser.FontSize == Tbstock.FontSize) Button3.IsEnabled = false;
            else Button3.IsEnabled = true;

            if (_r == _r1 && _g == _g1 && _b == _b1) ButtonColor.IsEnabled = false;
            else ButtonColor.IsEnabled = true;

            Button5.IsEnabled = true;
            Button7.IsEnabled = true;

            Tbuser.IsReadOnly = true;

            _color = 3;

            _newcount++;
            if(_count < 1) _count++;

            Label1.Content = (_newcount).ToString() + " шаг\n";

            using (var sw = File.AppendText(_file))
                sw.WriteLine(_newcount.ToString() + " шаг:");
        }
    }
}
