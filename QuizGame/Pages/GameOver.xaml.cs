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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizGame.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameOver : Page
    {
        private string highScore, localHighScore;

        public GameOver()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= GameOver_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += GameOver_BackRequested;
            highScore = e.Parameter as String;
            localHighScore = Settings.Settings.loadSettings("highScore");

            if (int.Parse(highScore) > int.Parse(localHighScore))
            {
                Settings.Settings.saveSetting("highScore", highScore);
            }

            txtUserScore.Text = highScore;

            sadTromboControl.Source = new Uri("ms-appx:///Assets/GameOver.mp3");
            sadTromboControl.Play();
        }

        private async void GameOver_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            e.Handled = true;
            var backMessage = new MessageDialog("Are you sure you want to close the App?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("Cancel");
            backMessage.Commands.Add(okBtn);
            backMessage.Commands.Add(cancelBtn);

            IUICommand result = await backMessage.ShowAsync();

            if (result != null && result.Label.Equals("Yes"))
            {
                Application.Current.Exit();
            }
        }

        private void btnRetry_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(PlayGame));
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }
    }
}
