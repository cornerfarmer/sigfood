using sigfood.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sigfood.Models
{
    public class Settings
    {

        public string headerBgr { get; set; }

        public string headerBorder { get; set; }
        public Settings()
        {
            headerBgr = "#BFC67D";
            headerBorder = "#999E64";
        }

    }
}
