using sigfood.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace sigfood
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            if (rb != null)
            {
                string color = rb.Tag.ToString();
                Utility.viewModel.headerBgr = color.Substring(0, color.IndexOf("|"));
                Utility.viewModel.headerBorder = color.Substring(color.IndexOf("|") + 1);
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void MenuButton2_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void MenuButton1_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void RadioButton_Loading(FrameworkElement sender, object args)
        {
            RadioButton button = sender as RadioButton;
            string color = button.Tag.ToString();
            button.IsChecked = (color == Utility.viewModel.headerBgr + "|" + Utility.viewModel.headerBorder);
        }
    }
}
