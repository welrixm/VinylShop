﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceLibrary.Models
{
    partial class Shipment
    {
        public ObservableCollection<ProductShipment> ProductShipments
        {
            get
            {
                return new ObservableCollection<ProductShipment>(ProductShipment);
            }
        }
        public int? Quantity
        {
            get
            {
                return this.ProductShipment.Sum(x => x.Quantity);
            }
        }
        public decimal? TotalPrice
        {
            get
            {
                return this.ProductShipment.Sum(x => x.Quantity * x.Product.Price);
            }
        }
    }
}
