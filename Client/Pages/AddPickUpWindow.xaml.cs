using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Windows.Shapes;

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddPickUpWindow.xaml
    /// </summary>
    public partial class AddPickUpWindow : Window
    {
        DeliveryPoint deliveryPointCont;
        public AddPickUpWindow(DeliveryPoint deliveryPoint)
        {
            InitializeComponent();
            deliveryPointCont = deliveryPoint;
            DataContext = deliveryPointCont;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AddressTbx.Text))
            {
                MessageBox.Show("Заполните поле адреса", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                DeliveryPoint delivery = new DeliveryPoint();
                {
                    delivery.Address = AddressTbx.Text;

                }

                App.db.DeliveryPoint.Add(delivery);
                App.db.SaveChanges();
                MessageBox.Show("Адрес пункта выдачи добавлен", "Уведомлние", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
