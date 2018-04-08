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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace QuizGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayGame : Page
    {
        private Random rand = new Random();

        private int score = 0;
        int i = 0;
        private int highScore = 0;
        private int state = 1;
        private string colourResult;

        private DispatcherTimer dispatcherTimer;

        void setUpTimeBar()
        {
            timeBar.Value = 9999;
            //Timer set up for the timer bar in app
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();

        }

        private void DispatcherTimer_Tick(object sender, object e)
        {
            timeBar.Value = Settings.Settings.speed;
            if(timeBar.Value <= 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());

            }
        }

        private int randNum()
        {
            return rand.Next(1, 5);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested -= PlayGame_BackRequested;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.UI.Core.SystemNavigationManager.GetForCurrentView().BackRequested += PlayGame_BackRequested;

            highScore = int.Parse(Settings.Settings.loadSettings("highScore"));
            txtHighScore.Text = String.Format("High Score: {0}", highScore);
            dispatcherTimer = null;
            Playing();
        }

        private void Playing()
        {
            int colour = randNum();
            if(colour == 1)
            {
                colourResult = "Blue";
            }
            else if (colour == 2)
            {
                colourResult = "Red";
            }
            else if (colour == 3)
            {
                colourResult = "Green";
            }
            else if (colour == 4)
            {
                colourResult = "Orange";
            }

            txtQuest.Text = String.Format("{0}", colourResult);

            i += 1;

            if (i >= 20)
            {
                int foreColour = randNum();
                if(foreColour == 1)
                {
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
                }
                else if (foreColour == 2)
                {
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else if (foreColour == 3)
                {
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                }
                else if (foreColour == 4)
                {
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
                }
            }

            setUpTimeBar();

        }

        private async void PlayGame_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
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
                //Navigates to the game over page and passes through score
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        public PlayGame()
        {
            this.InitializeComponent();
        }

        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            if("Blue" == colourResult)
            {
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            if ("Red" == colourResult)
            {
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            if ("Green" == colourResult)
            {
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            if ("Orange" == colourResult)
            {
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Playing();
            }
            else
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }
    }
}
