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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizGame.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Options : Page
    {
        public Options()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= Options_BackRequested;
        }

        //When the user navigates to the options page it sets the sliders value to the current speed
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += Options_BackRequested;

            int sliderValue = Settings.Settings.speed;
            adjustSpeed.Value = sliderValue;
        }

        //If the back button is pushed go back to the main page
        private void Options_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }

        //If the slider value is changed set it to the current speed
        private void Slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Settings.Settings.speed = int.Parse(adjustSpeed.Value.ToString());
            Settings.Settings.saveSetting("speed", Settings.Settings.speed.ToString());
        }

        //If the back button is pressed then Save all the values and go back to the main page
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int sliderValue = Settings.Settings.speed;
            adjustSpeed.Value = sliderValue;
            Settings.Settings.speed = int.Parse(adjustSpeed.Value.ToString());
            Settings.Settings.saveSetting("speed", Settings.Settings.speed.ToString());
            Frame.Navigate(typeof(MainPage));
        }
    }
}
