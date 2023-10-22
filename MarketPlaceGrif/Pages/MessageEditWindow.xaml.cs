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
    /// Логика взаимодействия для MessageEditWindow.xaml
    /// </summary>
    public partial class MessageEditWindow : Window
    {
        Message contextMessage;
        public MessageEditWindow(Message message)
        {
            InitializeComponent();
            contextMessage = message;
            DataContext = contextMessage;
            StateMessageCbx.ItemsSource = App.db.StateMessage.ToList();
            StateMessageCbx.DisplayMemberPath = "Name";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (contextMessage.Id == 0)
            {
                
                
                    App.db.Message.Add(contextMessage);

               
            }


            App.db.SaveChanges();
            MessageBox.Show("Успешно сохранено!", "Уведомления", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
