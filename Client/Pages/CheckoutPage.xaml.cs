using Client.Models;
using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages
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
            DependencyProperty.Register("Products", typeof(ObservableCollection<Product>), typeof(OrderPage));

        public ObservableCollection<Product> ProductsAdd
        {
            get { return (ObservableCollection<Product>)GetValue(ProductsAddOrderProperty); }
            set { SetValue(ProductsAddOrderProperty, value); }
        }


        public static readonly DependencyProperty ProductsAddOrderProperty =
            DependencyProperty.Register("ProductsAdd", typeof(ObservableCollection<Product>), typeof(OrderPage));

        public CheckoutPage()
        {
            App.db.Product.Load();
            Products = new ObservableCollection<Product>(App.db.Product.Local);
            ProductsAdd = new ObservableCollection<Product>();
            InitializeComponent();
           
            var points = App.db.DeliveryPoint.ToList();
            DeliveryPointCmb.ItemsSource = points;
            var types = App.db.DeliveryType.ToList();
            var cards = App.db.Card.Where(x => x.UserId == Navigation.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;
           
        }

      

        private void ButtonAddProdOrder_Click(object sender, RoutedEventArgs e)
        {
            Product product = ProdList.SelectedItem as Product;
            if (product == null)
            {
                MessageBox.Show("Для добавления товара в корзину, сначала выберите его.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                var stock = App.db.Stock.FirstOrDefault(x => x.ProductId == product.Id);

                if (stock != null)
                {
                    Products.Remove(product);
                    ProductsAdd.Add(product);
                }
                else
                {
                    MessageBox.Show("К сожалению, данный товар отсутствует на складе!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
            int price = 0;
           
            foreach (var item in ProductsAdd)
                price += Convert.ToInt32(item.Price);
           
            {

                }
            if (ProductsAdd.Count == 0)
            {
                MessageBox.Show("Ваша корзина пуста. Пожалуйста, выберите товар.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }

            else
            {
             
               string quan = CountTb.Text.Trim();
                if(quan.Length > 0)
                {
                    try
                    {
                        if (CardCb.SelectedItem == null)
                        {
                            MessageBox.Show("Выберите карту", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                        else
                        {
                            var selCard = (CardCb.SelectedItem as Card);

                            if (selCard.Balance < price)
                            {
                                MessageBox.Show("На карте недостаточно средств! Выберите другую карту", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }
                            Order order = new Order();
                            {


                                order.UserId = Navigation.AuthUser.Id;
                                order.OrderStatusId = 1;
                                order.Date = DateTime.Now;
                                
                                order.AddressDelivery = AddressTb.Text;
                                

                                if (Courier.IsChecked == true)
                                {
                                    order.DeliveryTypeId = 2;
                                    order.DeliveryPointId = null;

                                }
                                else if (Pickup.IsChecked == true)
                                {
                                    order.DeliveryTypeId = 1;
                                    order.DeliveryPointId = (DeliveryPointCmb.SelectedItem as DeliveryPoint).Id;
                                }
                                else
                                {
                                    MessageBox.Show("Выберите тип доставки", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                    return;
                                }
                            }

                            App.db.Order.Add(order);
                            App.db.SaveChanges();

                            int count = 0;

                            foreach (var item in ProductsAdd)

                            {

                                count = Convert.ToInt32(CountTb.Text);


                                ProductOrder book = new ProductOrder()
                                {

                                    OrderId = order.Id,
                                    Product = item,
                                    Count = (int?)count,


                                };

                                App.db.ProductOrder.Add(book);

                                MessageBox.Show("Ваш заказа успешно создан", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
                                App.db.SaveChanges();

                                var selStock = App.db.Stock.FirstOrDefault(x => x.ProductId == book.Product.Id);
                                selStock.CountOfProducts -= count;


                            }
                            selCard.Balance -= price;



                            App.db.SaveChanges();

                            NavigationService.Navigate(new OrderPage());
                        }
                       
                       
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка при оформлении заказа: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }




                }
                else
                {
                    MessageBox.Show("Введите количество одного товара", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                    
                
                
                
            }

            
            
            
        }

      

       

        private void OtmenaBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Pickup_Checked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Visible;
        }

        private void Pickup_Unchecked(object sender, RoutedEventArgs e)
        {
            IfPickup.Visibility = Visibility.Hidden;
        }

        private void Courier_Checked(object sender, RoutedEventArgs e)
        {
            IfCourier.Visibility = Visibility.Visible;
        }

        private void Courier_Unchecked(object sender, RoutedEventArgs e)
        {
            IfCourier.Visibility = Visibility.Hidden;
        }

        private void DeliveryPointCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddressTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void AddPickUptn_Click(object sender, RoutedEventArgs e)
        {
            AddPickUpWindow delivery = new AddPickUpWindow((sender as Button).DataContext as DeliveryPoint);
            delivery.ShowDialog();
            var dels = App.db.DeliveryPoint.ToList();
            DeliveryPointCmb.ItemsSource = dels;
        }

        private void CardCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddCardBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCardWindow card = new AddCardWindow((sender as Button).DataContext as Card);
            card.ShowDialog();
            var cards = App.db.Card.Where(x => x.UserId == Navigation.AuthUser.Id).ToList();
            CardCb.ItemsSource = cards;
        }
    }
}
