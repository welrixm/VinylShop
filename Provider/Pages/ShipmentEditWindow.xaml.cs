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
using System.Windows.Shapes;

namespace Provider.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShipmentEditWindow.xaml
    /// </summary>
    public partial class ShipmentEditWindow : Window
    {
        Shipment contextShipment;
        public ShipmentEditWindow(Shipment shipment)
        {
            InitializeComponent();
            contextShipment = shipment;
            DataContext = contextShipment;
            StageCbx.ItemsSource = App.db.State.ToList();
            StageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextShipment.Id == 0)
            {
                if (TimeTbx.Text.Length > 0)
                {
                    App.db.Shipment.Add(contextShipment);
                    App.db.SaveChanges();
                    MessageBox.Show("Успешно сохранено!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Заполните все поля!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }    
            }


            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
