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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace Csharp_Lab_D_1_V_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //полный список
        List<Human> HList = new List<Human>();
        String CurC;
        //текущий список

        List<Human> CList = new List<Human>();
        Dictionary<int, Human> CDictionary = new Dictionary<int, Human>();
        Queue<Human> CQueue = new Queue<Human>();
        HashSet<Human> CHashSet = new HashSet<Human>();
        IEnumerable<Human> CurEnumerable = null;
        bool SortByAscending;
        String SortAttribute;
        public MainWindow()
        {
            InitializeComponent();
        }
//************************************************************************************************
        //Обработка RadioButton-ов
        private void onRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            { 
                case "rbFind":
                    this.ActionButton.Content = "Найти";
                    this.gpSIAttr.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpSITypeSort.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIName.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIGender.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIHeight.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIWeight.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIDate.Visibility = System.Windows.Visibility.Visible;
                    this.ActionButton.Click += onButtonFindClick;
                    this.ActionButton.Click -= onButtonSortClick;
                    this.ActionButton.Click -= onButtonDeleteClick;
                    break;
                case "rbSort":
                    this.ActionButton.Content = "Сортировать";
                    this.gpSIAttr.Visibility = System.Windows.Visibility.Visible;
                    this.gpSITypeSort.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIName.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIGender.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIHeight.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIWeight.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIDate.Visibility = System.Windows.Visibility.Collapsed;
                    this.ActionButton.Click += onButtonSortClick;
                    this.ActionButton.Click -= onButtonDeleteClick;
                    this.ActionButton.Click -= onButtonFindClick;
                    break;
                case "rbDel":
                    this.ActionButton.Content = "Удалить";
                    this.gpSIAttr.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpSITypeSort.Visibility = System.Windows.Visibility.Collapsed;
                    this.gpFDIName.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIGender.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIHeight.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIWeight.Visibility = System.Windows.Visibility.Visible;
                    this.gpFDIDate.Visibility = System.Windows.Visibility.Visible;
                    this.ActionButton.Click += onButtonDeleteClick;
                    this.ActionButton.Click -= onButtonSortClick;
                    this.ActionButton.Click -= onButtonFindClick;
                    break;
            }
        }
        private void onSortTypeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name)
            {
                case "rbAscending":
                    SortByAscending = true;
                    break;
                case "rbDescending":
                    SortByAscending = false;
                    break;
            }

        }
        private void onSortAttrRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            SortAttribute = rb.Name;

        }
        private void onCollectionTypeRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            List<Human> tList;
            CurEnumerable = getCurEnumerable();
            if (CurEnumerable != null)
            {
                switch (rb.Name)
                {
                    case "rbTCList":
                        CList = CurEnumerable.ToList();       
                        CQueue.Clear();
                        CDictionary.Clear();
                        CHashSet.Clear();
                        break;
                    case "rbTCQueue":
                        CQueue = new Queue<Human>(CurEnumerable);
                        CList.Clear();
                        CDictionary.Clear();
                        CHashSet.Clear();
                        break;
                    case "rbTCDictionary":
                        int i = CurEnumerable.Count()+1;
                        CDictionary = CurEnumerable.ToDictionary(x => i++, x => x);
                        CList.Clear();
                        CQueue.Clear();
                        CHashSet.Clear();
                        break;
                    case "rbTCHashSet":
                        CHashSet = new HashSet<Human>(CurEnumerable);
                        CList.Clear();
                        CQueue.Clear();
                        CDictionary.Clear();
                        break;
                }
            }
            CurC = rb.Name;

        }
//************************************************************************************************

        private void onButtonFindClick(Object Sender, RoutedEventArgs e)    
        {
            string NameFrom = "", NameTo = "zzzzzzzzzzzzzzzzzz";
            int HeightFrom = 150, HeightTo = 230, WeightFrom = 60, WeightTo = 150;
            DateTime DateFrom = new DateTime(1950, 10, 10), DateTo = new DateTime(2015, 10, 10);
            if (tbHeightFrom.Text != "") HeightFrom = int.Parse(tbHeightFrom.Text);
            if (tbHeightTo.Text != "") HeightTo = int.Parse(tbHeightTo.Text);
            if (tbWeightFrom.Text != "") WeightFrom = int.Parse(tbWeightFrom.Text);
            if (tbWeightTo.Text != "") WeightTo = int.Parse(tbWeightTo.Text);
            DateFrom = tbDateFrom.DisplayDate;
            DateTo = tbDateTo.DisplayDate;
            if (tbNameFrom.Text != "") NameFrom = tbNameFrom.Text;
            if (tbNameTo.Text != "") NameTo = tbNameTo.Text;
            IEnumerable<Human> query = getCurEnumerable();
            IEnumerator<Human> er = null;
            //IEnumerable<string> sequence = cars.Where(p => p.StartsWith("F"));
            //доделать имена
            IEnumerable<Human> finded = query.Where(x => (x.Height > HeightFrom 
                                                            & x.Height < HeightTo
                                                          & x.Weight > WeightFrom
                                                            & x.Weight < WeightTo
                                                          & x.Date > DateFrom
                                                            & x.Date < DateTo));
            
            er = finded.GetEnumerator();
            ChangeCurCollection(finded);
            
            OutputListToTree(er);
        }
        private void onButtonSortClick(Object Sender, RoutedEventArgs e)
        {
            IEnumerable<Human> query = getCurEnumerable();
            IEnumerator<Human> er = null;
            switch (SortAttribute)
            {
                case "rbAtName":
                    if (SortByAscending)
                        er = query.OrderBy((h => h.Name)).GetEnumerator();
                    else er = query.OrderByDescending(h => h.Name).GetEnumerator();
                    break;
                case "rbAtHeight":
                    if(SortByAscending)
                        er = query.OrderBy((h => h.Height)).GetEnumerator();
                    else er = query.OrderByDescending(h => h.Height).GetEnumerator();
                    break;
                case "rbAtWeight":
                    if (SortByAscending)
                        er = query.OrderBy((h => h.Weight)).GetEnumerator();
                    else er = query.OrderByDescending(h => h.Weight).GetEnumerator();
                    break;
                case "rbAtDate":
                    if (SortByAscending)
                        er = query.OrderBy((h => h.Date)).GetEnumerator();
                    else er = query.OrderByDescending(h => h.Date).GetEnumerator();
                    break;            
            }
            OutputListToTree(er);
        }
        private void onButtonDeleteClick(Object Sender, RoutedEventArgs e)
        {
            string NameFrom = "", NameTo = "zzzzzzzzzzzzzzzzzz";
            int HeightFrom = 230, HeightTo = 150, WeightFrom = 200, WeightTo = 50;
            DateTime DateFrom = new DateTime(2015, 10, 10), DateTo = new DateTime(1950, 10, 10);
            if (tbHeightFrom.Text != "") HeightFrom = int.Parse(tbHeightFrom.Text);
            if (tbHeightTo.Text != "") HeightTo = int.Parse(tbHeightTo.Text);
            if (tbWeightFrom.Text != "") WeightFrom = int.Parse(tbWeightFrom.Text);
            if (tbWeightTo.Text != "") WeightTo = int.Parse(tbWeightTo.Text);
            if (tbDateFrom.DisplayDate != new DateTime(1950,11,05)) DateFrom = tbDateFrom.DisplayDate;
            if (tbDateFrom.DisplayDate != new DateTime(2015,11,21)) DateTo = tbDateTo.DisplayDate;
            if (tbNameFrom.Text != "") NameFrom = tbNameFrom.Text;
            if (tbNameTo.Text != "") NameTo = tbNameTo.Text;
            IEnumerable<Human> query = getCurEnumerable();
            IEnumerator<Human> er = null;
            //IEnumerable<string> sequence = cars.Where(p => p.StartsWith("F"));
            //доделать имена
            IEnumerable<Human> finded = query.Where(x => ((x.Height < HeightFrom
                                                            | x.Height > HeightTo)
                                                         &(x.Weight < WeightFrom
                                                            | x.Weight > WeightTo)
                                                         &(x.Date < DateFrom
                                                            | x.Date > DateTo)));

            er = finded.GetEnumerator();
            ChangeCurCollection(finded);

            OutputListToTree(er);
        }
        //Добавление в список*********************************************************************
        //Добавление одного человека
        private void onGenButtonAddHuman(Object Sender, RoutedEventArgs e)
        {
            List<Human> AddList = new List<Human>();

                Human rh = new Human();
                rh.Name = tbGenHumanName.Text;
                rh.Height = double.Parse(tbGenHumanHeight.Text);
                rh.Weight = double.Parse(tbGenHumanWeight.Text);
                rh.Date = tbGenHumanDate.DisplayDate;
                if ((bool)rbGenHumanM.IsChecked)
                    rh.Gender = "М";
                else rh.Gender = "Ж";
                
                AddList.Add(rh);
            
            //CList = new List<Human>(HList);
            IEnumerable<Human> CurEnum = getCurEnumerable();
            IEnumerable<Human> AddEnum = AddList.AsEnumerable();
            IEnumerable<Human> CombEnum = CurEnum.Concat(AddEnum);

            ChangeCurCollection(CombEnum);
            //SerializeThisShit(CList);
            OutputListToTree(getCurEnumerable().GetEnumerator());
        }
        //Добавление из файла
        private void onGenButtonAddFromFile(Object Sender, RoutedEventArgs e)
        {
            //this.ActionButton.Content = "Удалить";
            List<Human> tList = new List<Human>();
            SerializeThisShit(tList);
            ChangeCurCollection(HList.AsEnumerable());
            OutputListToTree(getCurEnumerable().GetEnumerator());
        }
        //Добавление случайного списка человеков
        private void onGenButtonAddRandom(Object Sender, RoutedEventArgs e)
        {
            Random rand = new Random();
            int count = rand.Next(5, 10);
            List<Human> AddList = new List<Human>();
            for (int i = 0; i < count; i++)
            {
                Human rh = new Human();
                rh.Name = "name_" + i.ToString();
                if (rand.Next(0, 2 ) == 0)
                    rh.Gender = "М";
                else rh.Gender = "Ж";
                rh.Height = rand.Next(140, 230);
                rh.Weight = rand.Next(60, 150);
                rh.Date = new DateTime(rand.Next(1950, 2000), rand.Next(1, 12), rand.Next(1, 28));
                //HList.Add(rh);
                AddList.Add(rh);
            }
            //CList = new List<Human>(HList);
            IEnumerable<Human> CurEnum = getCurEnumerable();
            IEnumerable<Human> AddEnum = AddList.AsEnumerable();
            IEnumerable<Human> CombEnum = CurEnum.Concat(AddEnum);
          
            ChangeCurCollection(CombEnum);
            //SerializeThisShit(CList);
            OutputListToTree(getCurEnumerable().GetEnumerator());
        }

        //Вспомогательные функции*****************************************************************
        private void OutputListToTree(List<Human> lh)
        {
            tree.Items.Clear();
            foreach (var h in lh)
            {
                
                TreeViewItem item = new TreeViewItem();
                item.Header = h.Name;
                item.Tag = h;
                item.Items.Add("пол   " + h.Gender);
                item.Items.Add("рост   " + h.Height + " см");
                item.Items.Add("вес   " + h.Weight);
                item.Items.Add("дата рождения   " + h.Date);
                tree.Items.Add(item);
            }
        }                               
        private void OutputListToTree( IEnumerator<Human> lh)
        {
            tree.Items.Clear();
            if (lh == null) return;
            while (lh.MoveNext())
            {
                Human h = lh.Current;
                TreeViewItem item = new TreeViewItem();
                item.Header = h.Name;
                item.Tag = h;
                item.Items.Add("пол   " + h.Gender);
                item.Items.Add("рост   " + h.Height + " см");
                item.Items.Add("вес   " + h.Weight);
                item.Items.Add("дата рождения   " + h.Date);
                tree.Items.Add(item);
            }
            
        }
        private IEnumerable<Human> getCurEnumerable()
        {
            IEnumerable<Human> CurEnum = null;
            
            switch (CurC)
            {
                case "rbTCList":
                    CurEnum = CList.AsEnumerable();
                    break;
                case "rbTCQueue":
                    CurEnum = CQueue.AsEnumerable();
                    break;
                case "rbTCDictionary":
                    CurEnum = CDictionary.Values.AsEnumerable();
                    break;
                case "rbTCHashSet":
                    CurEnum = CHashSet.AsEnumerable();
                    break;
            }
            return CurEnum;
        }
        private void ChangeCurCollection(IEnumerable<Human> newCol)
        {
            switch (CurC)
            {
                case "rbTCList":
                    CList = newCol.ToList();
                    break;
                case "rbTCQueue":
                    CQueue = new Queue<Human>(newCol);
                    break;
                case "rbTCDictionary":
                    int i = newCol.Count() + 1;
                    CDictionary = newCol.ToDictionary(x => i++, x => x);
                    break;
                case "rbTCHashSet":
                    CHashSet = new HashSet<Human>(newCol);
                    break;
            }
        }
        private void tbKeyPress(object sender, TextCompositionEventArgs e)
        {
            if (Char.IsDigit(e.Text, 0) || (e.Text == ","))
            {
                e.Handled = false;
            }
            else e.Handled = true;
        }
        private void SerializeThisShit(List<Human> Shit)
        {
            IFormatter formatter = new BinaryFormatter();
            //FileStream s = File.Create("serialized.bin");
            //    //formatter.Serialize(s, Shit);
            using (FileStream s = File.OpenRead ("serialized.bin"))
            HList  = (List<Human>) formatter.Deserialize (s);
        }
    }
}
