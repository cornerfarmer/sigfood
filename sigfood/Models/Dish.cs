using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace sigfood.Models
{
    public class Dish
    {
        public String name { get; set; }
        public int ratingCounter { get; set; }
        public double ratingMedian { get; set; }
        public double ratingStandardDeviation { get; set; }
        public List<Comment> comments { get; set; }

        public List<BitmapImage> images { get; set; }

        public Dish()
        {
            comments = new List<Comment>();
            images = new List<BitmapImage>();
        }

        public void addImageFromUrl(string url)
        {
            images.Add(new BitmapImage(new Uri(url)));
         }
    }
}
