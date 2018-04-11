using QuizGame.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace QuizGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= MainPage_BackRequested;
        }

        //When the user is navigated to this page it pulls the highest score from the local storage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += MainPage_BackRequested;
            txtBestScore.Text = "High Score : " + Settings.Settings.loadSettings("highScore").ToString();
        }

        //Checks if the user navigates back a page and saves all settings, then closes the app
        private async void MainPage_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            var backMessage = new MessageDialog("Are you sure you want to close the App?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("Cancel");
            backMessage.Commands.Add(okBtn);
            backMessage.Commands.Add(cancelBtn);

            IUICommand result = await backMessage.ShowAsync();

            if(result != null && result.Label.Equals("Yes"))
            {
                Application.Current.Exit();
            }
        }

        //Play button navigates the user to the play game page
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayGame));
        }

        //Options button navigates the user to the options menu
        private void btnOptions_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Options));
        }
    }
}
