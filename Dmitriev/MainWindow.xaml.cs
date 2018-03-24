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

namespace Dmitriev
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Window1 win;
        public static string text = "";
        public static List<TextBox> tblist;
        public static List<List<TextBox>> list = new List<List<TextBox>>() { };
        public static List<CheckBox> chblist = new List<CheckBox>(4)
            {
                new CheckBox(),
                new CheckBox(),
                new CheckBox(),
                new CheckBox(),
                new CheckBox(),
            };
        
        public int add = 1, row = 0, id = 1;
        public MainWindow()
        {
            win = new Window1();

            InitializeComponent();

            win.Show();
        }

        private void butrefr_Click(object sender, RoutedEventArgs e)
        {
            for(int i = 0; i < 5; i++)
                for(int j = 0; j < 3; j++)
                {
                    Grid.SetRow(tblist[j], i);
                    text = text + tblist[j].Text;
                }
        }

        private void butdel_Click(object sender, RoutedEventArgs e)
        {
            //add--;
            //butadd.IsEnabled = true;
            //for (int i = 0; i < list.Count; i++)
            //    if (chblist[i].IsChecked == true)
            //    {
                   
            //    }
            //    else MessageBox.Show("Уже удалили");
        }

        private void butadd_Click(object sender, RoutedEventArgs e)
        {
            if (add == 5) butadd.IsEnabled = false;
            add++;
            
            tblist = new List<TextBox>(2) {
                new TextBox() { Height = 20, Width = 80 },
                new TextBox() { Height = 20, Width = 80 },
                new TextBox() { Height = 20, Width = 80 }
            };
            //list.Add(tblist);
            stackPan.Children.Add(chblist[row]);
            for (int i = 0; i < 3; i++)
            {
                stackPan.Children.Add(tblist[i]);
            }
            
            

            row++;
        }
    }
}
