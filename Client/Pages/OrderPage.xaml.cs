using Client.Models;
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

namespace Client.Pages
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
            ListOrder.ItemsSource = App.db.Order.Local.Where(x => x.UserId == Navigation.AuthUser.Id).ToList();
          
           
        }

        private void BtnInvoiceOrder_Click(object sender, RoutedEventArgs e)
        {
            var selBook = (sender as Button).DataContext as Order;
            InvoiceWindow shipmentEditWindow = new InvoiceWindow(selBook);
            shipmentEditWindow.ShowDialog();
        }

        private void BtnSaveQR_Click(object sender, RoutedEventArgs e)
        {
            var selCode = (sender as Button).DataContext as Order;
            BarcodeWindow shipmentEditWindow = new BarcodeWindow(selCode);
            shipmentEditWindow.ShowDialog();
        }
    }
}
