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

namespace Client.Pages
{
    /// <summary>
    /// Логика взаимодействия для InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow : Window
    {
        
        public IEnumerable<Product> Products
        {
            get { return (IEnumerable<Product>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        public static readonly DependencyProperty ProductsProperty =
            DependencyProperty.Register("IngredientOfStage", typeof(IEnumerable<Product>), typeof(InformationWindow));

        public static InformationWindow Instance { get; private set; }

        public Product DishObject
        {
            get { return (Product)GetValue(DishesProperty); }
            set { SetValue(DishesProperty, value); }
        }

        public Product Product { get; }

        public static readonly DependencyProperty DishesProperty =
            DependencyProperty.Register("DishObject", typeof(Product), typeof(InformationWindow));
        public InformationWindow(Product product)
        {
            App.db.Product.Load();
            Products = App.db.Product.Local;
            DishObject = product;
            InitializeComponent();
            Instance = this;
            Product = product;
            
        }
    }
}
