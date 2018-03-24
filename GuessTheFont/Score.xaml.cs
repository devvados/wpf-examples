using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using System.Linq;

namespace GuessTheFont
{
    /// <summary>
    /// Логика взаимодействия для Score.xaml
    /// </summary>
    public partial class Score : Window
    {
        readonly string _filescore = "score.txt";
        readonly string _filesteps = "steps.txt";
        class MyTable
        {
            public MyTable(int id, int score, int steps)
            {
                Num = id;
                Score = score;
                Steps = steps;
            }

            private int Num { get; set; }
            private int Score { get; set; }
            private int Steps { get; set; }
        }


        public Score()
        {
            InitializeComponent();
        }

        private void dataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var res = new List<MyTable>(10);
            List<int> scoretb = new List<int>(), steptb = new List<int>();
            var s1 = File.ReadAllLines(_filescore);
            var s2 = File.ReadAllLines(_filesteps);

            for (var i = 0; i < s1.Length; i++)
            {
                if(i == 10) break;
                scoretb.Add(Convert.ToInt32(s1[i]));
                steptb.Add(Convert.ToInt32(s2[i]));
            }
            scoretb.Sort();
            steptb.Sort();
            steptb.Reverse();
            res.AddRange(scoretb.Select((t, i) => new MyTable(i + 1, Convert.ToInt32(steptb[i]), Convert.ToInt32(t))));
            DataGrid.ItemsSource = res;
        }
    }
}
