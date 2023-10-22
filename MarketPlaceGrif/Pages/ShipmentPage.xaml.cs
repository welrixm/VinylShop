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
    /// Логика взаимодействия для ShipmentPage.xaml
    /// </summary>
    public partial class ShipmentPage : Page
    {
        public ObservableCollection<Shipment> Shipments
        {
            get { return (ObservableCollection<Shipment>)GetValue(ShipmentsProperty); }
            set { SetValue(ShipmentsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShipmentsProperty =
            DependencyProperty.Register("Shipments", typeof(ObservableCollection<Shipment>), typeof(ShipmentPage));


        public ShipmentPage()
        {
            App.db.Shipment.Load();
            Shipments = App.db.Shipment.Local;
            InitializeComponent();
        }

        private void BtnDeleteShip_Click(object sender, RoutedEventArgs e)
        {
            var selProd = (sender as Button).DataContext as Shipment;
            var state = selProd.State;
            if (MessageBox.Show("Вы точно хотите отменить поставку?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                if (state.Id == 1)
                {
                    var bookshipmentToRemove = App.db.ProductShipment.Where(ios => ios.ShipmentId == selProd.Id);
                    App.db.ProductShipment.RemoveRange(bookshipmentToRemove);

                    App.db.Shipment.Remove(selProd);
                    App.db.SaveChanges();
                    MessageBox.Show("Поставка отменена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("К сожалению, поставка не может быть отменена!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                ListShipment.ItemsSource = App.db.Shipment.ToList();
            }
        }

        private void BtnShip_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckoutPage());
        }
    }
}
