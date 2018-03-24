using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ControlsCreate
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RadioButton[] mas;
        List<Control> cont = new List<Control>();
 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void rbButton_Checked(object sender, RoutedEventArgs e)
        {
            Button but = new Button();
            cont.Add(but);
            but.Height = 30;
            but.Width = 80;
            but.Content = "Im a button";
        }

        private void rbTextBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBox tb = new TextBox();
            cont.Add(tb);
            tb.Height = 50;
            tb.Width = 50;
        }

        private void rbLabel_Checked(object sender, RoutedEventArgs e)
        {
            Label label = new Label();
            cont.Add(label);
            label.Height = 30;
            label.Width = 80;
            label.Content = "Im a label";
        }

        private void rbCalend_Checked(object sender, RoutedEventArgs e)
        {
            Calendar cal = new Calendar();
            cont.Add(cal);
        }

        //private void rbNumUpDown_Checked(object sender, RoutedEventArgs e)
        //{
        //    System.Windows.Forms.NumericUpDown nud = new System.Windows.Forms.NumericUpDown();
        //    cont.Add(nud);
        //    nud.Height = 30;
        //    nud.Width = 30;
        //}

        private void button_Click(object sender, RoutedEventArgs e)
        {
            mas = new RadioButton[] { rbButton, rbTextBox, rbLabel, rbCalend };
            MessageBox.Show("Кликните на поле ниже, где хотите, чтобы появилась фигура");
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(canvas);
            //canvas.Children.Clear();
            for (int i = 0; i < mas.Length; i++)
            {
                if (mas[i].IsChecked == true)
                {
                    Canvas.SetLeft(cont.Last(), pos.X);
                    Canvas.SetTop(cont.Last(), pos.Y);

                    canvas.Children.Add(cont.Last());
                    MessageBox.Show("You selected " + cont.Last().GetType().ToString());
                }
            }
           
        }
    }
}
