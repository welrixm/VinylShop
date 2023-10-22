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
using Provider.Models;

namespace Provider.Pages
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
            var user = Navigation.AuthUser.Id;
            if (user == 3)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 1).ToList();
            }
            else if (user == 7)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 2).ToList();
            }
            else if (user == 8)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 3).ToList();
            }
            else if (user == 9)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 4).ToList();
            }
            else if (user == 10)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 5).ToList();
            }
            else if (user == 11)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 6).ToList();
            }
            else if (user == 12)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 7).ToList();
            }
            else if (user == 13)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 8).ToList();
            }
            else if (user == 14)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 9).ToList();
            }
            else if (user == 15)
            {
                ListShipment.ItemsSource = App.db.Shipment.Where(x => x.ProviderId == 10).ToList();
            }
            else
            {
                MessageBox.Show("Такого поставщика не имеется в системе", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void BtnEditShip_Click(object sender, RoutedEventArgs e)
        {
            var selship = (sender as Button).DataContext as Shipment;
            ShipmentEditWindow shipmentEditWindow = new ShipmentEditWindow(selship);
            shipmentEditWindow.ShowDialog();
        }
    }
}
