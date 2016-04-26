using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sigfood.Models
{
    public class Day
    {
        public DateTime date { get; set; }
        public DateTime? nextDate { get; set; }
        public DateTime? prevDate { get; set; }
        public Menu menu { get; set; }
    }
}
