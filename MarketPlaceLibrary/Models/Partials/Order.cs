using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketPlaceLibrary.Models
{
    partial class Order
    {
        public ObservableCollection<ProductOrder> ProductOrders
        {
            get
            {
                return new ObservableCollection<ProductOrder>(ProductOrder);
            }
        }
        public int? Count
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Count);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.ProductOrder.Sum(x => x.Count * x.Product.Price);
            }
        }

        public Visibility AdressVisible
        {
            get
            {
                if(DeliveryTypeId == 1)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }
        public Visibility PickUpVisible
        {
            get
            {
                if (DeliveryTypeId == 2)
                {
                    return Visibility.Collapsed;
                }
                else
                {
                    return Visibility.Visible;
                }
            }
        }


    }
}
