using sigfood.Models;
using sigfood.ViewModels;
using System;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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
            this.InitializeComponent();
         
            DataContext = Utility.viewModel;
            if ((DataContext as MainViewModel).selectedDay != null)
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
                try
                {
                    Utility.viewModel.loadNext();
                }
                catch(System.Net.WebException ex)
                {

                }
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

        private void gotoErrorView()
        {
            bool e = Frame.Navigate(typeof(NetworkError));
        }

        private void MasterListView_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as ListView).SelectedIndex = 0;
        }

        private void Page_Loading(FrameworkElement sender, object args)
        {
           
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if ((DataContext as MainViewModel).hasLoadingError())
                gotoErrorView();
        }
    }
}
