using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace MathGame
{
    using System.Windows.Threading;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        readonly DispatcherTimer _timer = new DispatcherTimer();
        int _tenthsOfSecondElapse;
        int _matchFound;
        private readonly string _textBlockName = "TimeTextBLock";

        public MainWindow()
        {
            InitializeComponent();
            _timer.Interval = TimeSpan.FromSeconds(.1);
            _timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            _tenthsOfSecondElapse++;
            TimeTextBLock.Text = (_tenthsOfSecondElapse / 10f).ToString("0.0s");
            if (_matchFound == 8)
            {
                _timer.Stop();
                TimeTextBLock.Text += " - Play again ? ";
            }
        }
        private void SetUpGame()
        {
            Random random = new Random();
            List<string> animalEmoji = new List<string>()
            {
                "🐶", "🐶",
                "🐱", "🐱",
                "🦁", "🦁",
                "🦊", "🦊",
                "🦝", "🦝",
                "🐻", "🐻",
                "🐮", "🐮",
                "🐷", "🐷",
            };
            foreach (TextBlock textBlock in MainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != _textBlockName)
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }

            }
            _timer.Start();
            _tenthsOfSecondElapse = 0;
            _matchFound = 0;
        }

        TextBlock _lastTextBlockClicked;
        bool _findingMatch;

        private void TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;

            if (_findingMatch == false)
            {
                if (textBlock != null)
                {
                    textBlock.Visibility = Visibility.Hidden;
                    _lastTextBlockClicked = textBlock;
                }

                _findingMatch = true;

            }
            else if (textBlock != null && textBlock.Text == _lastTextBlockClicked.Text)
            {
                _matchFound++;
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

        private void Time_TextBlockMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_matchFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
