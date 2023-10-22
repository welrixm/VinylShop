using MarketPlaceLibrary.Models;
using System;
using System.Collections.Generic;
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

namespace Storekeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для MessagePage.xaml
    /// </summary>
    public partial class MessagePage : Page
    {
        VinylShopEntities _context = new VinylShopEntities();
        StateMessage selMessage;
        public MessagePage()
        {
            InitializeComponent();
            selMessage = null;
            Refresh();
        }

        private void Refresh()
        {
            List<Message> listMessages = _context.Message.ToList();
            if (selMessage != null)
            {
                listMessages = listMessages.Where(x => x.StateMessageId == selMessage.Id).ToList();
            }
            if (CbSort.SelectedItem != null)
            {
                switch ((CbSort.SelectedItem as ComboBoxItem).Tag)
                {
                    case "1":

                        listMessages = _context.Message.ToList();
                        break;
                    case "2":

                        listMessages = listMessages.OrderBy(x => x.Name).ToList();
                        break;
                    case "3":

                        listMessages = listMessages.OrderByDescending(x => x.Name).ToList();

                        break;
                    case "4":

                        listMessages = listMessages.OrderBy(x => x.DateMessage).ToList();
                        break;
                    case "5":

                        listMessages = listMessages.OrderByDescending(x => x.DateMessage).ToList();
                        break;

                }

            }
            var searchString = SearchTb.Text;
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                listMessages = listMessages.Where(x => x.Name.ToLower().Contains(searchString.ToLower())).ToList();
            }

            HistoryList.ItemsSource = listMessages;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();

        }

        private void MessageBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageEditWindow message = new MessageEditWindow();
            message.Show();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            selMessage = App.db.StateMessage.FirstOrDefault(x => x.Id == 1);
            Refresh();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            selMessage = App.db.StateMessage.FirstOrDefault(x => x.Id == 2);
            Refresh();
        }
    }
}
