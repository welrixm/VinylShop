using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceLibrary.Models
{
    partial class User
    {
        public string FUO
        {
            get
            {
                return Surname + " " + Name + " " + MiddleName;
            }
        }
    }
}
