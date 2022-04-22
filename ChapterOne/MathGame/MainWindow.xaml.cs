using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MathGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
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
            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }

        }

        TextBlock _lastTextBlockClicked;
        bool _findingMatch = false;

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
                textBlock.Visibility = Visibility.Hidden;
                _findingMatch = false;
            }
            else
            {
                _lastTextBlockClicked.Visibility = Visibility.Visible;
                _findingMatch = false;
            }
        }

    }
}
