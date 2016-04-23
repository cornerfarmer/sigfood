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

        public MainViewModel()
        {
            PivotItems = new ObservableCollection<Day>();

            PivotItems.Add(SigfoodApiService.getDataOfDate());
            
            for (int i = 0; i < 3 && PivotItems.Last().nextDate != null; i++)
            {
                PivotItems.Add(SigfoodApiService.getDataOfDate(PivotItems.Last().nextDate));
            }
            for (int i = 0; i < 3 && PivotItems.First().prevDate != null; i++)
            {
                PivotItems.Insert(0, SigfoodApiService.getDataOfDate(PivotItems.First().prevDate));
            }
        }
        
    }
}
