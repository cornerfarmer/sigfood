using sigfood.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace sigfood.Services
{
    class SigfoodApiService
    {
        public static Day getDataOfDate(DateTime date)
        {
            String url = "https://www.sigfood.de/?do=api.gettagesplan&datum=" + date.ToString("yyyy-MM-dd");
            XmlDocument doc = new XmlDocument();
            HttpClient client = new HttpClient();

            var request = WebRequest.CreateHttp(url);
            WebResponse response = request.GetResponseAsync().Result;
            StreamReader reader = new StreamReader(response.GetResponseStream());
            string test = reader.ReadToEnd();
            //String stream = client.GetStringAsync("https://www.sigfood.de/?do=api.gettagesplan&datum=2016-04-12").Result;
           
            Day day = new Day();
            XDocument xdoc = XDocument.Parse(test);

            XElement tagesMenue = xdoc.Descendants("Tagesmenue").FirstOrDefault();
            day.date = DateTime.Parse(tagesMenue.Descendants("tag").FirstOrDefault().Value);
            if (tagesMenue.Descendants("naechstertag").Count() > 0)
                day.nextDate = DateTime.Parse(tagesMenue.Descendants("naechstertag").FirstOrDefault().Value);
            day.prevDate = DateTime.Parse(tagesMenue.Descendants("vorherigertag").FirstOrDefault().Value);
            Menu menu = new Menu();
            foreach (XElement mensaEssen in tagesMenue.Descendants("Mensaessen"))
            {
                Offer offer = new Offer();
                Dish dish = new Dish();
                XElement hauptgericht = mensaEssen.Descendants("hauptgericht").FirstOrDefault();
                dish.name = WebUtility.HtmlDecode(hauptgericht.Descendants("bezeichnung").FirstOrDefault().Value);
                dish.ratingCounter = Convert.ToInt32(hauptgericht.Descendants("anzahl").FirstOrDefault().Value);
                if (hauptgericht.Descendants("stddev").Count() > 0)
                    dish.ratingStandardDeviation = Convert.ToDouble(hauptgericht.Descendants("stddev").FirstOrDefault().Value);
                if (hauptgericht.Descendants("schnitt").Count() > 0)
                    dish.ratingMedian = double.Parse(hauptgericht.Descendants("schnitt").FirstOrDefault().Value, CultureInfo.InvariantCulture);
                dish.ratingMedianInverted = 5 - dish.ratingMedian;
                foreach (XElement bild in hauptgericht.Descendants("bild"))
                {
                    dish.addImageFromUrl("https://www.sigfood.de/?do=getimage&width=300&bildid=" + bild.Attribute("id").Value);
                }
                if (dish.images.Count == 0)
                    dish.addImageFromUrl("http://www.sigfood.de/mensa//nophotoavailable000.png");

                foreach (XElement kommentar in hauptgericht.Descendants("kommentar"))
                {
                    Comment comment = new Comment();
                    if (kommentar.Descendants("nick").Count() > 0)
                        comment.author = WebUtility.HtmlDecode(kommentar.Descendants("nick").FirstOrDefault().Value);
                    comment.text = WebUtility.HtmlDecode(kommentar.Descendants("text").FirstOrDefault().Value);
                    comment.time = DateTime.Parse(kommentar.Descendants("formattedtime").FirstOrDefault().Value);
                    dish.comments.Add(comment);
                }

               

                offer.dish = dish;
                if (mensaEssen.Descendants("preisstud").Count() > 0)
                    offer.costStudent = Convert.ToInt32(mensaEssen.Descendants("preisstud").FirstOrDefault().Value);
                if (mensaEssen.Descendants("preisbed").Count() > 0)
                    offer.costServant = Convert.ToInt32(mensaEssen.Descendants("preisbed").FirstOrDefault().Value);
                if (mensaEssen.Descendants("preisgast").Count() > 0)
                    offer.costGuest = Convert.ToInt32(mensaEssen.Descendants("preisgast").FirstOrDefault().Value);
                menu.offers.Add(offer);
            }
            menu.SelectedOffer = menu.offers[0];

            day.menu = menu;
            return day;
        }
    }
}
