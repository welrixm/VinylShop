using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
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
namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProviderEditPage.xaml
    /// </summary>
    public partial class ProviderEditPage : Page
    {
        Provider contextProv;
        
        public ProviderEditPage(Provider provider)
        {
            InitializeComponent();
            contextProv = provider;
            DataContext = contextProv;
            RoleCbx.ItemsSource = App.db.Country.ToList();
            RoleCbx.DisplayMemberPath = "Name";
        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PhoneTb.Text.Length > 0 && EmailTb.Text.Length > 0 &&  NameTb.Text.Length > 0 
                )
            {
                string email = EmailTb.Text;
                string phone = PhoneTb.Text;
                if (Regex.IsMatch(phone, @"^((\+?7|8)[ -]?)?([(]?\d[- ]?[()]?){10}$") && Regex.IsMatch(phone, @"^(\+?7|8)?[\s(-]?[(-]?\d{3,4}[)-]?[ )-]?\d{2,7}[ -]?\d{2,4}[ -]?\d{0,2}$") && Regex.IsMatch(email, @"^[\w.-]+@\w+\.\w+$"))
                {
                    if (RoleCbx.SelectedItem == null)
                    {
                        MessageBox.Show("Выберите страну поставщика", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        if (contextProv.Id == 0)
                        {
                            App.db.Provider.Add(contextProv);


                        }
                        if (MessageBox.Show("Вы точно хотите сохранить?", "Уведомления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            App.db.SaveChanges();
                            MessageBox.Show("Поставщик успешно добавлен", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
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

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
