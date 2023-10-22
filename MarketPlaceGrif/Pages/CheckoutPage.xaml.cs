using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для CheckoutPage.xaml
    /// </summary>
    public partial class CheckoutPage : Page
    {

        public ObservableCollection<Product> Products
        {
            get { return (ObservableCollection<Product>)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(ShipmentPage));

        public ObservableCollection<Product> ProductsAdd
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsAddProperty); }
            set { SetValue(ProductsAddProperty, value); }
        }


        public static readonly DependencyProperty ProductsAddProperty =
            DependencyProperty.Register("ProductsAdd", typeof(ObservableCollection<Product>), typeof(ShipmentPage));

        public CheckoutPage()
        {
            App.db.Product.Load();
            Products = new ObservableCollection<Product>(App.db.Product.Local);
            ProductsAdd = new ObservableCollection<Product>();
            InitializeComponent();
            List<Provider> listProvs = App.db.Provider.ToList();
            listProvs.Insert(0, new Provider { Name = "Поставщик" });
            ProvCmb.ItemsSource = listProvs;
            ProvCmb.SelectedIndex = 0;
            RefreshData();
        }


        private void RefreshData()
        {

            List<Provider> listProds = App.db.Provider.ToList();
            if (ProvCmb.SelectedIndex != 0)
            {
                Provider selected = (Provider)ProvCmb.SelectedItem;
                listProds = listProds.Where(x => x.Id == selected.Id).ToList();
            }
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            decimal purchasePrice = 0;

            foreach (var item in ProductsAdd)
                purchasePrice += (decimal)item.Price;
            if (ProductsAdd.Count == 0)
            {
                MessageBox.Show("Ваша корзина пуста. Пожалуйста, выберите товар.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

            else
            {
                string quan = CountTb.Text.Trim();
                if (quan.Length > 0)
                {
                    try
                    {
                        Shipment shipment = new Shipment();
                        {


                            if (ProvCmb.SelectedIndex == 0)
                            {
                                MessageBox.Show("Выберите поставщика", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            else
                            {
                                shipment.StateId = 1;
                                shipment.Date = DateTime.Now;
                                shipment.Provider = (Provider)ProvCmb.SelectedItem;
                                shipment.PurchasePrice = (int?)purchasePrice;
                            }
                        }

                        App.db.Shipment.Add(shipment);
                        App.db.SaveChanges();

                        int count = 0;
                        int price = 0;
                        foreach (var item in ProductsAdd)


                        {
                            count = Convert.ToInt32(CountTb.Text);
                            ProductShipment product = new ProductShipment()
                            {

                                ShipmentId = shipment.Id,
                                Product = item,
                                Quantity = (int?)count
                            };

                            App.db.ProductShipment.Add(product);
                            App.db.SaveChanges();


                            var selStock = App.db.Stock.FirstOrDefault(x => x.ProductId == product.Product.Id);

                            if (selStock == null)
                            {
                                price = Convert.ToInt32(count) * Convert.ToInt32(product.Product.Price);

                                Stock stock = new Stock()
                                {
                                    ProductId = product.Product.Id,
                                    CountOfProducts = count,
                                    DateOfReceipt = DateTime.Now,
                                    PriceOfPurchase = price,
                                    CostForOne = product.Product.Price
                                };



                                App.db.Stock.Add(stock);
                                App.db.SaveChanges();

                            }
                            else
                            {
                                selStock.CountOfProducts += count;
                            }






                        }
                        App.db.SaveChanges();

                        NavigationService.Navigate(new ShipmentPage());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при оформлении заявки: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите количество одного товара", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
                
        }

        private void ButtonAddProdShipment_Click(object sender, RoutedEventArgs e)
        {
            Product product = ProdList.SelectedItem as Product;
            if (product == null)
            {
                MessageBox.Show("Для добавления товара в корзину, сначала выберите его.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                Products.Remove(product);
                ProductsAdd.Add(product);
            }
           
        }

        private void ProvCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
    }
}
