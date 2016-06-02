using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using sigfood.Services;
using sigfood.Models;
using System.ComponentModel;

namespace sigfood.ViewModels
{

    public static class Utility
    {
        private static MainViewModel viewModel_;
        public static MainViewModel viewModel {
            get
            {
                if (viewModel_ == null)
                    viewModel_ = new MainViewModel();
                return viewModel_;
            }
            set
            {

            }
        }
    }

    public class MainViewModel
    {
        public ObservableCollection<Day> PivotItems { get; set; }   
        public Day selectedDay { get; set; }

        public string headerBgr { get; set; }

        public string headerBorder { get; set; }
        public MainViewModel()
        {
            PivotItems = new ObservableCollection<Day>();
            headerBgr = "#BFC67D";
            headerBorder = "#999E64";
            try
            {          
                loadNext();
            }
            catch (System.Net.WebException e)
            {

            }

        }

        public void loadNext()
        {
            if (PivotItems.Count == 0)
            {
                PivotItems.Add(SigfoodApiService.getDataOfDate());

                selectedDay = PivotItems.First();
            }

            while(PivotItems.Count - PivotItems.IndexOf(selectedDay) - 1 < 3 && PivotItems.Last().nextDate != null)
            {
                PivotItems.Add(SigfoodApiService.getDataOfDate(PivotItems.Last().nextDate));
            }
            while (PivotItems.IndexOf(selectedDay) < 3 && PivotItems.First().prevDate != null)
            {
                PivotItems.Insert(0, SigfoodApiService.getDataOfDate(PivotItems.First().prevDate));
            }
        }

        public bool hasLoadingError()
        {
            return PivotItems.Count == 0;
        }
    }
}
