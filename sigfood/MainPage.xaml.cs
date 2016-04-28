using sigfood.Models;
using sigfood.Services;
using sigfood.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Core;
// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace sigfood
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random r;
        public MainPage()
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Colors.LightGray;
                    titleBar.BackgroundColor = Colors.LightGray;
                }
            }
            
            r = new Random();
            DataContext = Utility.viewModel;
            this.InitializeComponent();
            DayPivot.SelectedIndex = (DataContext as MainViewModel).PivotItems.IndexOf((DataContext as MainViewModel).selectedDay);
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            (DayPivot.SelectedItem as Day).menu.SelectedOffer = (Offer)e.ClickedItem;

            if (ActualWidth < 920)
                gotoDetailView();
        }

        private async void DayPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Utility.viewModel.selectedDay = (Day)DayPivot.SelectedItem;

            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                Utility.viewModel.loadNext();
            });
        }

        private void AdaptiveStates_CurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            if (e.OldState != null && e.OldState.Name == "DefaultState" && e.NewState.Name == "NarrowState")
            {
                gotoDetailView();
            }
        }

        private void gotoDetailView()
        {
            Frame.Navigate(typeof(DetailsView), new SuppressNavigationTransitionInfo());
        }

        private void MasterListView_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).SelectedIndex = 0;
        }
    }
}
