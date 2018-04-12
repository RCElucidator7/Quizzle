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
using Windows.Media.Playback;
using Windows.Media.Core;

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
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

        }

        //Function to determine the speed of the timer
        private void DispatcherTimer_Tick(object sender, object e)
        {
            //Decrements by the speed every few seconds
            timeBar.Value -= Settings.Settings.speed;
            //When the time goes to zero the timer stops and the user is navigated to the game over page
            if (timeBar.Value <= 0)
            {
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());

            }
        }

        //Random number generator to determine the colours
        private int randNumGenerator()
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

            //Loads the highest score from the local settings
            highScore = int.Parse(Settings.Settings.loadSettings("highScore"));
            txtHighScore.Text = String.Format("High Score: {0}", highScore);
            dispatcherTimer = null;
            //Starts game as soon as the user is navigated to the page
            StartGame();
        }

        private void StartGame()
        {
            //Generates a random number between 1 and 4 to determine what colour displays
            int colour = randNumGenerator();
            if(colour == 1)
            {
                //Word Blue displayed
                colourResult = "Blue";
            }
            else if (colour == 2)
            {
                //Word Red displayed
                colourResult = "Red";
            }
            else if (colour == 3)
            {
                //Word Green displayed
                colourResult = "Green";
            }
            else if (colour == 4)
            {
                //Word Orange displayed
                colourResult = "Orange";
            }

            txtQuest.Text = String.Format("{0}", colourResult);

            //Counter to control the change in colour of the text
            i += 1;

            //If statement to check when its over 15
            if (i >= 15)
            {
                //Generate random number between 1 and 4 to determine the colour
                int foreColour = randNumGenerator();
                if(foreColour == 1)
                {
                    //Changes text blue 
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Blue);
                }
                else if (foreColour == 2)
                {
                    //Changes Text red
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }
                else if (foreColour == 3)
                {
                    //Changes Text green   
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.Green);
                }
                else if (foreColour == 4)
                {
                    //Changes Text Orange
                    txtQuest.Foreground = new SolidColorBrush(Windows.UI.Colors.DarkOrange);
                }
            }

            //Resets the timer bar
            setUpTimeBar();
        }

        //Function to check if the player backs out of the game
        private async void PlayGame_BackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
        {
            //Stops timer, prompts the user if they want to close the app
            e.Handled = true;
            dispatcherTimer.Stop();
            dispatcherTimer = null;
            var backMessage = new MessageDialog("Are you sure you want to close the App?");
            var okBtn = new UICommand("Yes");
            var cancelBtn = new UICommand("Cancel");
            backMessage.Commands.Add(okBtn);
            backMessage.Commands.Add(cancelBtn);

            IUICommand result = await backMessage.ShowAsync();

            //If user wants to quit the app go to the game over screen
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

        //Checks if the user presses the blue button
        private void btnBlue_Click(object sender, RoutedEventArgs e)
        {
            //if the colour selected is the same as the colour displayed increment score and reset game
            if ("Blue" == colourResult)
            {
                //Set the source of the sound file and play when the answers correct
                dingControl.Source = new Uri("ms-appx:///Assets/ding.mp3");
                dingControl.Play();

                //increment the score by one if correct
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                StartGame();
            }
            //Else game over
            else
            {
                //Stop the timer and push to the game over page
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnRed_Click(object sender, RoutedEventArgs e)
        {
            //if the colour selected is the same as the colour displayed increment score and reset game
            if ("Red" == colourResult)
            {
                //Set the source of the sound file and play when the answers correct
                dingControl.Source = new Uri("ms-appx:///Assets/ding.mp3");
                dingControl.Play();

                //increment the score by one if correct
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                StartGame();
            }
            //Else game over
            else
            {
                //Stop the timer and push to the game over page
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnGreen_Click(object sender, RoutedEventArgs e)
        {
            //if the colour selected is the same as the colour displayed increment score and reset game
            if ("Green" == colourResult)
            {
                //Set the source of the sound file and play when the answers correct
                dingControl.Source = new Uri("ms-appx:///Assets/ding.mp3");
                dingControl.Play();

                //increment the score by one if correct
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                StartGame();
            }
            //Else game over
            else
            {
                //Stop the timer and push to the game over page
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }

        private void btnOrange_Click(object sender, RoutedEventArgs e)
        {
            //if the colour selected is the same as the colour displayed increment score and reset game
            if ("Orange" == colourResult)
            {
                //Set the source of the sound file and play when the answers correct
                dingControl.Source = new Uri("ms-appx:///Assets/ding.mp3");
                dingControl.Play();

                //increment the score by one if correct
                txtScore.Text = String.Format("Score: {0}".ToUpper(), ++score);
                txtState.Text = String.Format("{0}", ++state);
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                StartGame();
            }
            //Else game over
            else
            {
                //Stop the timer and push to the game over page
                dispatcherTimer.Stop();
                dispatcherTimer = null;
                Frame.Navigate(typeof(GameOver), score.ToString());
            }
        }
    }
}
