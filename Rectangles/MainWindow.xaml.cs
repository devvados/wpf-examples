using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Forms;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Rectangles
{

    public partial class MainWindow : Window
    {
        
        System.Windows.Point start = new System.Windows.Point();
        PointCollection col = new PointCollection();
        bool en = false;

        public MainWindow()
        {
            
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            en = true;
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (en == true)
            {
                start = e.GetPosition(canvas);
                col.Add(start);
            }
        }

        private void canvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (en == true)
            {
                for (int i = 0; i < col.Count; i++)
                {
                    Line line = new Line();
                    if (i == col.Count - 1)
                    {
                        line.X1 = col[col.Count - 1].X;
                        line.X2 = col[0].X;
                        line.Y1 = col[col.Count - 1].Y;
                        line.Y2 = col[0].Y;
                    }
                    else
                    {
                        line.X1 = col[i].X;
                        line.X2 = col[i + 1].X;
                        line.Y1 = col[i].Y;
                        line.Y2 = col[i + 1].Y;
                    }
                    line.Stroke = System.Windows.Media.Brushes.Black;
                    line.StrokeThickness = 2;
                    canvas.Children.Add(line);
                }
                col.Clear();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            System.Drawing.Pen blpen = new System.Drawing.Pen(System.Drawing.Color.Black, 3);
            Random rand = new Random();
            System.Windows.Shapes.Rectangle[] rects = new System.Windows.Shapes.Rectangle[rand.Next(2,8)];
            
            for(int i = 0; i < rects.Length; i++)
            {
                rects[i] = new System.Windows.Shapes.Rectangle();
                Canvas.SetLeft(rects[i], rand.Next(80, 500));
                Canvas.SetTop(rects[i], rand.Next(10, 200));
                rects[i].Stroke = System.Windows.Media.Brushes.Black;
                rects[i].Height = 100;
                rects[i].Width = 150;
                canvas.Children.Add(rects[i]);
            }
            
        }
    }
}
