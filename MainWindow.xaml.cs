using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Database
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Avenger item;
        Avenger SelectedAvenger;
        private static Db _data;
        public Task<List<Avenger>> ListOfAvengers;

        public Db Db
        {
            get
            {
                if (_data == null)
                {
                    _data = new Db("./Avenger.db3");
                }
                return _data;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            ToListView();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            item = new Avenger()
            {
                Name = t1.Text,
                RealName = t3.Text,
                Gender = t5.Text
            };
            Db.SaveItemAsync(item);

            t1.Text = "";
            t3.Text = "";
            t5.Text = "";
            ToListView();

        }
        public void ToListView()
        {
            AvengersList.Items.Clear();
            foreach (Avenger avenger in Db.GetItemsAsync().Result)
            {
                AvengersList.Items.Add(avenger);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteItemFromDB();
            ToListView();
        }

        public void DeleteItemFromDB()
        {
            item = (Avenger)AvengersList.SelectedItem;
            if (item == null)
            {
                MessageBox.Show("Select row");
            } else
            {
                Db.DeleteItemAsync(item.ID);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if ((Avenger)AvengersList.SelectedItem == null)
            {
               MessageBox.Show("Select row");
            } else
            {
                item = (Avenger)AvengersList.SelectedItem;
                t1.Text = item.Name;
                t3.Text = item.RealName;
                t5.Text = item.Gender;
                Create.Visibility = Visibility.Collapsed;
                EditBtn.Visibility = Visibility.Visible;
            }
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            SelectedAvenger = (Avenger)AvengersList.SelectedItem;
            item = new Avenger()
            {
                Name = t1.Text,
                RealName = t3.Text,
                Gender = t5.Text
            };
            Db.EditItemAsync(item, SelectedAvenger.ID);
            ToListView();
            Create.Visibility = Visibility.Visible;
            EditBtn.Visibility = Visibility.Collapsed;
            t1.Text = "";
            t3.Text = "";
            t5.Text = "";
        }
    }
}
