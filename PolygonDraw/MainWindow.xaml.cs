using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {
        Polygon poly = new Polygon();
        System.Windows.Point[] points = new System.Windows.Point[5];

        int dots, angles = 10;

        public MainWindow()
        {
            InitializeComponent();
            points[0] = new System.Windows.Point(200, 125);
            points[1] = new System.Windows.Point(300, 200);
            points[2] = new System.Windows.Point(250, 300);
            points[3] = new System.Windows.Point(150, 300);
            points[4] = new System.Windows.Point(100, 200);


            //Random rand = new Random();
            //int rad = rand.Next(100, 150);
            poly.Fill = Brushes.Magenta;
            this.Canvas.Children.Add(poly);

            Rectangle[] tmp = new Rectangle[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                poly.Points.Add(points[i]);
                tmp[i] = new Rectangle();
                tmp[i].Width = 10;
                tmp[i].Height = 10;
                Canvas.SetLeft(tmp[i], points[i].X - 5);
                Canvas.SetTop(tmp[i], points[i].Y - 5);
                tmp[i].Fill = System.Windows.Media.Brushes.Black;
                Canvas.Children.Add(tmp[i]);
            }
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
