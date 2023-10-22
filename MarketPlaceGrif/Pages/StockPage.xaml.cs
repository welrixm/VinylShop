using MarketPlaceLibrary.Models;
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

namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для StockPage.xaml
    /// </summary>
    public partial class StockPage : Page
    {
        private int CountEntriestOnPage = 4;

        public int CountProducts
        {
            get
            {
                return (int)GetValue(CountProductsProperty);
            }
            set
            {
                SetValue(CountProductsProperty, value);
            }
        }
        public static readonly DependencyProperty CountProductsProperty = DependencyProperty.Register("CountProducts", typeof(int), typeof(StockPage));




        public static StockPage Instance { get; private set; }
        private IEnumerable<Stock> TestingIEnumerableStocks;

        public StockPage()
        {
            App.db.Stock.Load();
            Stock = App.db.Stock.Local;
            TestingIEnumerableStocks = Stock;
            NumberPage = 1;
            TotalNumberPage = 0;
            NumberEntriestOnOnePage = new List<int>();
            CallingMethodBeforeInitialization();
            InitializeComponent();
            Instance = this;
        }

        public void UpdateCountIngredient()
           => CountProducts = TestingIEnumerableStocks.Count();

        public void RefreshCounters()
        {
           
            UpdateCountIngredient();
        }


        private void CallingMethodBeforeInitialization()
        {
            ValidateCountProducts();
            ValidateTotalCountPage();
            PageProcessing();
        }
        private void PageProcessing()
        {
            Stock = TestingIEnumerableStocks;
            Stock = Stock.Cast<Stock>()
                                  .Skip((NumberPage - 1) * CountEntriestOnPage)
                                  .Take(CountEntriestOnPage);

        }

        private void ValidateCountProducts()
        {
            CountProducts = TestingIEnumerableStocks.Count();

        }

        private void ValidateTotalCountPage()
        {
            TotalNumberPage = (int)Math.Ceiling(Convert.ToDouble(TestingIEnumerableStocks.Cast<Stock>().Count()) / Convert.ToDouble(CountEntriestOnPage));
            ValidateNumberEntriestOnOnePage();
        }

        private void ValidateNumberEntriestOnOnePage()
        {
            int n = 4, sum = 0;

            if (NumberEntriestOnOnePage.Count() == 0)
                for (int i = 2; i <= n; i++)
                {
                    sum += i;
                    NumberEntriestOnOnePage.Add(sum);
                }
        }

        private void ValidateCountEntriestOnPage()
        {
            if (CbCount.SelectedItem == null)
                return;

            CountEntriestOnPage = (int)CbCount.SelectedItem;
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selStock = (sender as Button).DataContext as Stock;
            NavigationService.Navigate(new StockEditPage(selStock));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selStock = (sender as Button).DataContext as Stock;
            if (MessageBox.Show("Вы точно хотите удалить эту запись ", "Уведомление ", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                App.db.Stock.Remove(selStock);
                MessageBox.Show("Запись удалена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                App.db.SaveChanges();

            }
            NumberPage = 1;
            ValidateTotalCountPage();
            PageProcessing();
        }

        private void BorderPlus_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new StockEditPage(new Stock()));
        }

        private void CbCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ValidateCountEntriestOnPage();
            ValidateTotalCountPage();

            if (NumberPage >= TotalNumberPage)
                NumberPage = TotalNumberPage;

            PageProcessing();
        }

        private void BtnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = 1;

            PageProcessing();
        }

        private void BtnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if ((NumberPage - 1) <= 0)
                return;

            NumberPage--;

            PageProcessing();
        }

        private void BtnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (TotalNumberPage <= NumberPage)
                return;

            NumberPage++;

            PageProcessing();
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            NumberPage = TotalNumberPage;

            PageProcessing();
        }
    }
}
