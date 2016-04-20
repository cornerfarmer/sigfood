using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sigfood.Models
{
    public class Menu : INotifyPropertyChanged
    {
        public List<Offer> offers { get; set; }

        private Offer _selectedOffer;
        public Offer SelectedOffer
        {
            get
            {
                return _selectedOffer;
            }
            set
            {
                _selectedOffer = value;
                NotifyPropertyChanged("SelectedOffer");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                var test = PropertyChanged.GetInvocationList();

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Menu()
        {
            offers = new List<Offer>();
        }
    }
}
