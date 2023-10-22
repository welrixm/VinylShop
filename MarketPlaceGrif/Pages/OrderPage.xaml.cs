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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {

        public ObservableCollection<Order> Orders
        {
            get { return (ObservableCollection<Order>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<Order>), typeof(OrderPage));

        public OrderPage()
        {
            App.db.Order.Load();
            Orders = App.db.Order.Local;
            InitializeComponent();
        }

        private void BtnDeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Order;
            var stage = selProd.OrderStatus;
            if (MessageBox.Show("Вы точно хотите отменить заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (stage.Id == 1)
                {
                    var prodorderToRemove = App.db.ProductOrder.Where(ios => ios.OrderId == selProd.Id);
                    App.db.ProductOrder.RemoveRange(prodorderToRemove);

                    App.db.Order.Remove(selProd);
                    App.db.SaveChanges();
                    MessageBox.Show("Заказ отменен", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("К сожалению, заказ не может быть отменен!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ListOrder.ItemsSource = App.db.Order.ToList();
            }
        }

        private void BtnFixOrder_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Order;
            OrderEditWindow orderEditWindow = new OrderEditWindow(selProd);
            orderEditWindow.ShowDialog();
        }
    }
}
