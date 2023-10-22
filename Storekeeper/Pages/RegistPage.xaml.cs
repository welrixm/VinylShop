using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Storekeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistPage.xaml
    /// </summary>
    public partial class RegistPage : Page
    {
        public RegistPage()
        {
            InitializeComponent();
        }

        private void RegistBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = TbLogin.Text.Trim();
            string password = TbPassword.Password.Trim();
            string firstname = TbSurname.Text.Trim();
            string lastname = TbName.Text.Trim();
            string patronymic = TbMiddle.Text.Trim();
            string date = TbDate.Text.Trim();

            if (login.Length > 0 && password.Length > 0)
            {
                if (Regex.IsMatch(login, @"^[\w.-]+@\w+\.\w+$"))
                {
                    App.db.User.Add(new User
                    {
                        Login = login,
                        Password = password,
                        Surname = firstname,
                        Name = lastname,
                        MiddleName = patronymic,
                        DateOfBirth = (DateTime)TbDate.SelectedDate,
                        RoleId = 4
                    });
                    App.db.SaveChanges();
                    MessageBox.Show("Вы зарегистрированы!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    NavigationService.GoBack();
                }

                else
                {
                    MessageBox.Show($" Проверьте логин {login} на корректность", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    TbLogin.Clear();

                }
            }
            else
            {
                MessageBox.Show("Пусто! Заполните поля.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            TbLogin.Text = "";
            TbPassword.Password = "";
            TbSurname.Text = "";
            TbName.Text = "";
            TbMiddle.Text = "";
            TbDate.Text = "";
        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void TbSurname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void TbMiddle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
