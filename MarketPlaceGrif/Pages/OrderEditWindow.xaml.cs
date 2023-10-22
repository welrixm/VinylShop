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
using MarketPlaceLibrary.Models;

namespace MarketPlaceGrif.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderEditWindow.xaml
    /// </summary>
    public partial class OrderEditWindow : Window
    {
        Order contextOrder;
        public OrderEditWindow(Order order)
        {
            InitializeComponent();
            contextOrder = order;
            DataContext = contextOrder;
            StageCbx.ItemsSource = App.db.OrderStatus.ToList();
            StageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextOrder.Id == 0)
            {
                if(TimeTbx.Text.Length > 0)
                {
                    App.db.Order.Add(contextOrder);
                    App.db.SaveChanges();
                    MessageBox.Show("Успешно сохранено!", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Не все поля заполнены", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
