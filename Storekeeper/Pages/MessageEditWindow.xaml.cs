using MarketPlaceLibrary.Models;
using Storekeeper.Models;
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

namespace Storekeeper.Pages
{
    /// <summary>
    /// Логика взаимодействия для MessageEditWindow.xaml
    /// </summary>
    public partial class MessageEditWindow : Window
    {
        public MessageEditWindow()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try

            {

                Message message = new Message();

                {

                    if(NameTbx.Text.Length > 0 && TextTbx.Text.Length > 0)
                    {
                        message.Name = NameTbx.Text;
                        message.Text = TextTbx.Text;

                        message.StateMessageId = 1;
                        message.DateMessage = DateTime.Now;
                        message.UserId = Navigation.AuthUser.Id;
                        App.db.Message.Add(message);

                        App.db.SaveChanges();

                        MessageBox.Show("Отправлено", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Не все поля заполнены.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

                

            }

            catch (Exception ex)

            {

                MessageBox.Show(ex.Message);

            }

        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CleanBtn_Click(object sender, RoutedEventArgs e)
        {
            NameTbx.Text = "";
            TextTbx.Text = "";
        }
    }
}
