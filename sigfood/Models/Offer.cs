using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sigfood.Models
{
    public class Offer
    {
        public Dish dish { get; set; }
        public int costStudent { get; set; }
        public int costServant { get; set; }
        public int costGuest { get; set; }
    }
}
