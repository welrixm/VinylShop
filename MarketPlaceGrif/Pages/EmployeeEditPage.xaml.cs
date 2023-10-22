using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
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
using MarketPlaceLibrary.Models;
using Microsoft.Win32;

namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditPage.xaml
    /// </summary>
    public partial class EmployeeEditPage : Page
    {
        User contextUser;
        public EmployeeEditPage(User user)
        {
            InitializeComponent();
            contextUser = user;
            DataContext = contextUser;
            RoleCbx.ItemsSource = App.db.Role.Where(x => x.Id != 2).ToList();
            RoleCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneTb.Text.Length > 0 && EmailTb.Text.Length > 0 && SurnameTb.Text.Length > 0 && NameTb.Text.Length > 0 &&
                MiddleTb.Text.Length > 0 && BirthTb.Text.Length > 0 && LoginTb.Text.Length > 0 && PasswordTb.Text.Length > 0 )
            {
                string email = EmailTb.Text;
                string phone = PhoneTb.Text;
                if (Regex.IsMatch(phone, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?){10}$") && Regex.IsMatch(phone, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$") && Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                {
                    if(RoleCbx.SelectedItem == null)
                    {
                        MessageBox.Show("Выберите должность сотрудника", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        if (contextUser.Id == 0)
                        {
                            App.db.User.Add(contextUser);


                        }
                        if (MessageBox.Show("Вы точно хотите сохранить?", "Уведомления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            App.db.SaveChanges();
                            MessageBox.Show("Пользователь успешно добавлен", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
                            NavigationService.GoBack();
                        }
                    }
                   



                }
                else
                {
                    MessageBox.Show($" Проверьте почту {email} или телефон {phone} на корректность", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    EmailTb.Clear();
                    PhoneTb.Clear();
                }
            }
            else MessageBox.Show("Заполните все поля.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void BDChange_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpg|*.jpg|*.jpeg|*.jpeg"
            };
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                contextUser.ImagePath = File.ReadAllBytes(openFileDialog.FileName);
                ProdImage.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }

        private void SurnameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void MiddleTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsLetter(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        

        private void PhoneTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
