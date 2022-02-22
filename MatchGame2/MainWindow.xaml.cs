using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame2
{
    using System.Windows.Threading;
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();      // creates a  new timer to track elapsed time
        int tenthsOfSecondsElapsed;
        int matchesFound;
        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again??";
            }
        }

        private void SetUpGame()  // game setup method
        {
            List<string> animalEmoji = new List<string>()  // tells the strings to be placed into a list
         {
             "🦑","🦑",
             "🐿","🐿",
             "🦒","🦒",
             "🐴","🐴",
             "🐭","🐭",
             "🦉","🦉",       // 8 matching emoji pairs
             "🐼","🐼",
             "🐺","🐺",
         };
            Random random = new Random();           // random assignment

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())      // assigning random emojisto be placed into text blocks
            {
                if (textBlock.Name != "timeTextBlock")      // if the text block is not named timeTextBlock, execute the following code
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }
            }
            timer.Start();
            tenthsOfSecondsElapsed = 0;
            matchesFound = 0;
        }
        TextBlock lastTextBlockClicked;  // these are fields - insdie the class but outside of the method
        bool findingMatch = false;      // determines if this is the first animal within a pair
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            if (findingMatch == false)      // makes the first animal clicked by a player disappear
            {
                textBlock.Visibility = Visibility.Hidden;
                lastTextBlockClicked = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastTextBlockClicked.Text)       // if the player finds a match, this code will execute making the matched pair invisible and unclickable 
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastTextBlockClicked.Visibility = Visibility.Visible;       //  there is no match, and the first animal clicked becomes visible again
                findingMatch= false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
