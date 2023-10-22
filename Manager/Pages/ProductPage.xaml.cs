using MarketPlaceLibrary.Models;
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

namespace Manager.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        VinylShopEntities _context = new VinylShopEntities();
        ProductType selType;
        public ProductPage()
        {
            InitializeComponent();
            selType = null;
            RefreshData();
        }

        private void RefreshData()
        {


            List<Product> listBooks = _context.Product.ToList();

            if (selType != null)
            {
                listBooks = listBooks.Where(x => x.TypeId == selType.Id).ToList();
            }
            if (CbSort.SelectedItem != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":

                        listBooks = _context.Product.ToList();
                        break;
                    case "2":

                        listBooks = listBooks.OrderBy(x => x.Name).ToList();
                        break;
                    case "3":

                        listBooks = listBooks.OrderByDescending(x => x.Name).ToList();

                        break;
                    case "4":

                        listBooks = listBooks.OrderBy(x => x.Price).ToList();
                        break;
                    case "5":

                        listBooks = listBooks.OrderByDescending(x => x.Price).ToList();
                        break;

                }

            }
            var searchString = SearchTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listBooks = listBooks.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            LVProducts.ItemsSource = listBooks;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            RefreshData();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }

        private void ElectroBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 1);
            RefreshData();
        }

        private void KeysBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 2);
            RefreshData();
        }

        private void MicrophonBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 3);
            RefreshData();
        }

        private void BeatBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 4);
            RefreshData();
        }

        private void BreathBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 5);
            RefreshData();
        }

        private void BowBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 6);
            RefreshData();
        }

        private void DJBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 7);
            RefreshData();
        }

        private void AcusticBtn_Click(object sender, RoutedEventArgs e)
        {
            selType = App.db.ProductType.FirstOrDefault(x => x.Id == 9);
            RefreshData();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}
