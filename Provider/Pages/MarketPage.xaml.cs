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

namespace Provider.Pages
{
    /// <summary>
    /// Логика взаимодействия для MarketPage.xaml
    /// </summary>
    public partial class MarketPage : Page
    {
        public MarketPage()
        {
            InitializeComponent();
        }

        private void ProductBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ProductPage());
        }

        private void Provider_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new ShipmentPage());
        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginPage());
        }
    }
}
