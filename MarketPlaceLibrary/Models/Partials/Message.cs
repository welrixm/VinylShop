using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlaceLibrary.Models
{
    partial class Message
    {
        public string Color
        {
            get
            {
                if (StateMessageId == 1) return "#FF2400";
                else if (StateMessageId == 2) return "#50C878";
                else return "#000000";
            }
        }
    }
}
