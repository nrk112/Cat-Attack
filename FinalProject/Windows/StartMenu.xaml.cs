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
using System.Windows.Shapes;

namespace FinalProject.Windows
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : Window
    {
        private bool isResumeable = false;

        public StartMenu(string title = "Cat Attack", int score = -1)
        {
            InitializeComponent();

            //Use a background image
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"../../Resources/Images/StartMenuBackground.png", UriKind.Relative));
            mainGrid.Background = ib;

            TitleLabel.Content = title;
            if (score == -1)
            {
                ScoreLabel.Content = "";
                ScoreAmtLabel.Content = "";
            }
            else if (score == -2)
            {
                PlayArcadeLabel.Content = "Resume";
                ScoreLabel.Content = "";
                ScoreAmtLabel.Content = "";
                isResumeable = true;
            }
            else
            {
                PlayArcadeLabel.Content = "Play Again";
                ScoreLabel.Content = "Hits";
                ScoreAmtLabel.Content = score;
            }
        }

        /// <summary>
        /// Event handler for clicking on a button in the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Label tempLabel = (Label)sender;
            if (tempLabel.Equals(QuitOptionLabel))
            {
                Application.Current.Shutdown();
            }
            else if (tempLabel.Equals(PlayArcadeLabel))
            {
                if (isResumeable)
                {
                    GameEngine.Instance.ResumeGame();
                }
                else
                {
                    GameEngine.Instance.StartArcadeGame();
                }
                this.Close();
            }
            else if (tempLabel.Equals(CreditsLabel))
            {
                Credits credits = new Credits();
                credits.ShowDialog();
            }
        }
    }
}
