using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
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
    /// Логика взаимодействия для EmployeePage.xaml
    /// </summary>
    public partial class EmployeePage : Page
    {
        Role selRole;
        public EmployeePage()
        {
            InitializeComponent();
            selRole = null;
            
            Refresh();
        }

        private void Refresh()
        {
            IEnumerable<User> employeslist = App.db.User.ToList();
           
            if (selRole != null)
            {
                employeslist = employeslist.Where(x => x.RoleId == selRole.Id).ToList();
            }
            if (SearchTb == null)
                return;
            if (SearchTb.Text.Length > 0)
            {
                employeslist = employeslist.Where(x => x.Surname.StartsWith(SearchTb.Text) || x.Name.StartsWith(SearchTb.Text) || x.MiddleName.StartsWith(SearchTb.Text));
            }
            LVUser.ItemsSource = employeslist.Where(x => x.RoleId != 2).ToList();
        }

        

      
        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selEmployee = (sender as Button).DataContext as User;
            NavigationService.Navigate(new EmployeeEditPage(selEmployee));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selUser = (sender as Button).DataContext as User;
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                var userToRemove = App.db.Order.Where(ios => ios.UserId == selUser.Id);
                App.db.Order.RemoveRange(userToRemove);
                


                App.db.User.Remove(selUser);
                App.db.SaveChanges();
                MessageBox.Show("Данные удалены", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
                LVUser.ItemsSource = App.db.User.ToList();
            }
        }

        private void BorderPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new EmployeeEditPage(new User()));
        }

       
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Admin_Click(object sender, RoutedEventArgs e)
        {
            selRole = App.db.Role.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

        private void Provider_Click(object sender, RoutedEventArgs e)
        {
            selRole = App.db.Role.FirstOrDefault(x => x.Id == 3);
            Refresh();
        }

        private void Storekeeper_Click(object sender, RoutedEventArgs e)
        {
            selRole = App.db.Role.FirstOrDefault(x => x.Id == 4);
            Refresh();
        }

        private void Courier_Click(object sender, RoutedEventArgs e)
        {
            selRole = App.db.Role.FirstOrDefault(x => x.Id == 5);
            Refresh();
        }

        private void Manager_Click(object sender, RoutedEventArgs e)
        {
            selRole = App.db.Role.FirstOrDefault(x => x.Id == 6);
            Refresh();
        }
    }
}
