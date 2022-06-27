using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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


namespace WPF_Study
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public class Person
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public Person(string i,string f,string l, string e, string ip) 
        { 
            ID = i; Name = f; Surname = l; Email = e; IP = ip; 
        }
    }

    public class People 
    {
        ObservableCollection<Person> people = null;
        public People()
        {
            people = new ObservableCollection<Person>();
            string path = "MOCK_DATA.csv";
            int c = 0;
            using (StreamReader reader = new StreamReader(path))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                   if(c >= 1)
                   {
                        string[] subs = line.Split(',');
                        people.Add(new Person(subs[0], subs[1], subs[2], subs[3], subs[4]));
                   }
                    c++;
                }
            }
        }

        public ObservableCollection<Person> GetData(int n)
        {
            if( n >= 0 && n < people.Count)
            {
                ObservableCollection<Person> p = new ObservableCollection<Person>();
                for(int i= n; i < n + 10; i++)
                {
                    p.Add(people[i]);
                }
                return p;
            }
            else 
                return people;
            
            
        }
    }
    public partial class MainWindow : Window
    {
        ViewModel viewModel;
        int start = 0;
        People people = new People();
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel();
            DataContext = viewModel;
            lvPerson.ItemsSource = people.GetData(start);
        }

        private void btn_F(object sender, RoutedEventArgs e)
        {
            if (start <= 1000)
                start += 10;
            else
                start = 0;
           lvPerson.ItemsSource = people.GetData(start);
        }

        private void btn_P(object sender, RoutedEventArgs e)
        {
            if (start > 0)
                start -= 10;
            else
                start = 990;
           lvPerson.ItemsSource = people.GetData(start);
        }
    }
}
