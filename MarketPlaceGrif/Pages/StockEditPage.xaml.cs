using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
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
    /// Логика взаимодействия для StockEditPage.xaml
    /// </summary>
    public partial class StockEditPage : Page
    {
        Stock stockContext;
        public StockEditPage(Stock stock)
        {
            InitializeComponent();
            stockContext = stock;
            DataContext = stockContext;
            NameProdCbx.ItemsSource = App.db.Product.ToList();
            NameProdCbx.DisplayMemberPath = "Name";
        }

        private void PriceTotalTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (QuanTbx.Text.Length > 0)
            {
                PriceTotalTbx.Text = (Convert.ToInt32(PriceOneTbx.Text) * Convert.ToInt32(QuanTbx.Text)).ToString();
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
           
            string count = stockContext.CountOfProducts.ToString();
            string price = PriceOneTbx.Text.Trim();
            string date = stockContext.DateOfReceipt.ToString();
            string price2 = stockContext.PriceOfPurchase.ToString();
           
            try
            {
                if (stockContext.Product == null)
                {
                    MessageBox.Show("Выберите наименование товара", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (price.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните стоимость товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (count.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните количество товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (price2.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните общую стоимость всех товаров!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (date.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните дату!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                
                if (stockContext.Id == 0)
                {

                    App.db.Stock.Add(stockContext);

                }

                App.db.SaveChanges();
                MessageBox.Show("Успешно сохранено!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
                StockPage.Instance.RefreshCounters();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при редактировании склада: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Otmena_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            StockPage.Instance.RefreshCounters();
        }

        private void QuanTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PriceOneTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void PriceTotalTbx_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
