using Client.Models;
using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCardWindow.xaml
    /// </summary>
    public partial class AddCardWindow : Window
    {
        Card cardContext;
        public AddCardWindow(Card card)
        {
            InitializeComponent();
            cardContext = card;
            DataContext = cardContext;
            BalanceTb.Text = "0";
        }

        private void NumberTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void DateTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0) && (e.Text != "/"))
            {
                e.Handled = true;
            }
        }

        private void CVCTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BalanceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
          
            if (string.IsNullOrEmpty(NumberTb.Text))
            {
                MessageBox.Show("Заполните номер карты", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(DateTb.Text))
            {
                MessageBox.Show("Заполните дату", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(CVCTb.Text))
            {
                MessageBox.Show("Заполните CVC", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (string.IsNullOrEmpty(BalanceTb.Text))
            {
                MessageBox.Show("Заполните свой баланс", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Card card = new Card();
                {
                    card.Number = NumberTb.Text;
                    card.Date = DateTb.Text;
                    card.UserId = Navigation.AuthUser.Id;
                    card.CVC = CVCTb.Text;
                    card.Balance = Convert.ToDecimal(BalanceTb.Text);
                }
                App.db.Card.Add(card);
                App.db.SaveChanges();
                MessageBox.Show("Вы успешно добавили новую карту.", "Уведомлние", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
