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
using MarketPlaceLibrary.Models;
namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProviderPage.xaml
    /// </summary>
    public partial class ProviderPage : Page
    {
        Country selCountry;
        public ProviderPage()
        {
            InitializeComponent();
            selCountry = null;
            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<Provider> employeslist = App.db.Provider.ToList();
           
            if (selCountry != null)
            {
                employeslist = employeslist.Where(x => x.CountryId == selCountry.Id).ToList();
            }
            if (SearchTb == null)
                return;
            if (SearchTb.Text.Length > 0)
            {
                employeslist = employeslist.Where(x => x.Name.StartsWith(SearchTb.Text));
            }
            LVUser.ItemsSource = employeslist.ToList();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Italy_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

        private void France_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 2);
            Refresh();
        }

        private void Germany_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 3);
            Refresh();
        }

        private void Spane_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 4);
            Refresh();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selEmployee = (sender as Button).DataContext as Provider;
            NavigationService.Navigate(new ProviderEditPage(selEmployee));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selProv = (sender as Button).DataContext as Provider;
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var userToRemove = App.db.Shipment.Where(ios => ios.ProviderId == selProv.Id);
                App.db.Shipment.RemoveRange(userToRemove);

                App.db.Provider.Remove(selProv);
                App.db.SaveChanges();
                MessageBox.Show("Данные удалены", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
                LVUser.ItemsSource = App.db.Provider.ToList();
            }
        }

        private void BorderPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new ProviderEditPage(new Provider()));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Germany_Click_1(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

        private void UK_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 2);
            Refresh();
        }

        private void Austria_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 3);
            Refresh();
        }

        private void USA_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 4);
            Refresh();
        }

        private void China_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 5);
            Refresh();
        }

        private void Japan_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 6);
            Refresh();
        }

        private void Shweiz_Click(object sender, RoutedEventArgs e)
        {
            selCountry = App.db.Country.FirstOrDefault(x => x.Id == 7);
            Refresh();
        }
    }
}
