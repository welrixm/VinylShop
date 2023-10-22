using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceLibrary.Models
{
    public partial class Product
    {
        public byte[] mainPhoto
        {
            get
            {
                var firstPhoto = this.ProductPhoto.FirstOrDefault();
                if (firstPhoto != null)
                {
                    return firstPhoto.Image;

                }
                return null;
            }
        }
        public string ActiveText
        {
            get
            {
                if (Stock.Count == 0)
                    return "Нет в наличии";
                else
                    return "В наличии";

            }
        }
        public string Color
        {
            get
            {
                if (Stock.Count == 0)
                    return "#d1f5f4";
                else
                    return "#FDFDFD";

            }
        }
        
    }
}
