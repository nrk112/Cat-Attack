using System;
using System.Windows;
using System.Windows.Media;

namespace FinalProject.GameObjects
{
    class ComboText : BaseClasses.GameText
    {
        private static MediaPlayer powerUpSound = new MediaPlayer();

        public ComboText()
        {
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 500.0;
            X = MainWindow.canvas.Width / 2;
            Y = MainWindow.canvas.Height / 4;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.FontSize = 45;
            textBlock.Text = "";

            Scale = 2.0 * Global.ScalingRatio;

            string fileName = Global.ResourcePath + Global.SlashSound;
            Uri uriFile = new Uri(fileName);
            powerUpSound.Open(uriFile);

            textBlock.Foreground = Brushes.Red;
            AddToGame(ZIndexType.UI);
        }

        public override void Reset()
        {

        }

        private int timeoutCounter = 0;
        public override void Update()
        {
            if (textBlock.Visibility == Visibility.Visible)
            {
                timeoutCounter++;
                if (timeoutCounter > 60)
                {
                    timeoutCounter = 0;
                    textBlock.Visibility = Visibility.Hidden;
                }
            }
        }

        public void SetVisible()
        {
            powerUpSound.Stop();
            powerUpSound.Play();
            textBlock.Visibility = Visibility.Visible;
        }
    }
}

