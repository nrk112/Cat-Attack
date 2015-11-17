using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace FinalProject.Windows
{
    /// <summary>
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits : Window
    {
        public Credits()
        {
            InitializeComponent();

            creditsTextBox.Text = "GAME\nNicholas Kehagias 2015\nSome code contributed by\nProfessor Thomas Fernandez FAU 2015 \n\r MUSIC\n\"Fretless\", \"Funcky Chunk\", and \"Ossuary 7 - Resolve\" \nKevin MacLeod (incompetech.com) \nLicensed under Creative Commons: By Attribution 3.0 License \n http://creativecommons.org/licenses/by/3.0/ \n\rIMAGES\nPublic Domain \n\rSounds\nPublic Domain";

            //Use a background image
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"../../Resources/Images/StartMenuBackground.png", UriKind.Relative));
            mainGrid.Background = ib;
        }

        /// <summary>
        /// Event handler for clicking on a button in the menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Label tempLabel = (Label)sender;
            if (tempLabel.Equals(GoBackLabel))
            {
                this.Close();
            }
        }
    }
}
