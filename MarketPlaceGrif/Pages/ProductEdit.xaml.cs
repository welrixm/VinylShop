using MarketPlaceLibrary.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.IO;
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
    /// Логика взаимодействия для ProductEdit.xaml
    /// </summary>
    public partial class ProductEdit : Page
    {
        Product contextPtoduct;
        
        public ProductEdit(Product product)
        {
            InitializeComponent();
            
            CBType.ItemsSource = App.db.ProductType.ToList();
            contextPtoduct = product;
            DataContext = contextPtoduct;
            Refresh();
           
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            string price = contextPtoduct.Price.ToString();
            string year = contextPtoduct.YearRelize.ToString();
            try
            {
                if (string.IsNullOrEmpty(contextPtoduct.Name))
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните наименование товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (price.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните стоимость товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
               

                if (string.IsNullOrEmpty(contextPtoduct.Description))
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните описание товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (year.Length == 0)
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните год выпуска товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextPtoduct.Author))
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните наименование автора товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(contextPtoduct.State))
                {
                    MessageBox.Show("Пусто. Пожалуйста, заполните состояние товара!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextPtoduct.ProductType == null)
                {
                    MessageBox.Show("Выберите тип товара", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (contextPtoduct.Id == 0)
                {
                    App.db.Product.Add(contextPtoduct);

                }
                App.db.SaveChanges();
                MessageBox.Show("Успешно сохранено!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при редактировании товара: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
           
            NavigationService.Navigate(new ProductPage());
        }

        private void BDChange_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog() { Multiselect = true };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                foreach (var filename in dialog.FileNames)
                {
                    contextPtoduct.ProductPhoto.Add(new ProductPhoto()
                    {
                        Image = File.ReadAllBytes(dialog.FileName),
                        Product = contextPtoduct
                    });
                }


                Refresh();
                DataContext = null;
                DataContext = contextPtoduct;
            }
        }
        private void Refresh()
        {
            LPhoto.ItemsSource = contextPtoduct.ProductPhoto.ToList();
        }

       

        private void PriceTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void CountTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BDDelete_Click(object sender, RoutedEventArgs e)
        {
            var sel = LPhoto.SelectedItem as ProductPhoto;
            if (sel != null)
            {
                if (MessageBox.Show("Точно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.db.ProductPhoto.Remove(sel);
                    App.db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите изображение", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Refresh();
            }
        }
    }
}
