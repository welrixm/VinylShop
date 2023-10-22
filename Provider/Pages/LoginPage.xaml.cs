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
using Provider.Models;

namespace Provider.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            if (Properties.Settings.Default.Login != null)
                TbLogin.Text = Properties.Settings.Default.Login;
            if (Properties.Settings.Default.Password != null)
                TbPassword.Password = Properties.Settings.Default.Password;
        }

        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistPage());
        }
        
        private void BorderAuth_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string login = TbLogin.Text.Trim();
            string password = TbPassword.Password.Trim();
            if (login.Length == 0 && password.Length == 0)
            {
                MessageBox.Show("Пусто. Пожалуйста, заполните поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                var employee = App.db.User.ToList().Find(emp => emp.Login == login && emp.Password == password);
                if (employee == null)
                {

                    MessageBox.Show("Данного пользователя не существует.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    if (SaveCb.IsChecked == true)
                    {
                        Properties.Settings.Default.Login = TbLogin.Text;
                        Properties.Settings.Default.Password = TbPassword.Password;
                        Properties.Settings.Default.Save();


                    }
                    Navigation.isAuth = true;
                    Navigation.AuthUser = employee;
                    MessageBox.Show("Вы успешно вошли в систему.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.Navigate(new MarketPage());
                }
            }
        }
    }
}
