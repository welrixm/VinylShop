using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using Client.Models;
using MarketPlaceLibrary.Models;
namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public static List<User> UserL { get; set; }
        public ProfilePage()
        {
            InitializeComponent();
           
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selUser = (sender as Button).DataContext as User;
            ProfileEditWindow profileEditWindow = new ProfileEditWindow(selUser);
            profileEditWindow.ShowDialog();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                App.db.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
                UserL = App.db.User.Where(x => x.Id == Navigation.AuthUser.Id).ToList();
                LVUser.ItemsSource = UserL;
            }
        }

        private void AllOrder_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new OrderPage());
           
        }
    }
}
