using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sigfood.Models
{
    public class Comment
    {
        public String author { get; set; }
        public DateTime time { get; set; }
        public String text { get; set; }
    }
}
