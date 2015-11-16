using FinalProject.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace FinalProject.GameObjects
{
    class AttackText : BaseClasses.GameText
    {
        public AttackText()
        {
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.FontSize = 45;
            textBlock.Text = "Attack!!";

            Scale = 1;

            textBlock.Foreground = Brushes.Red;
            AddToGame(ZIndexType.Game);
            textBlock.Visibility = Visibility.Hidden;
        }

        public void ChangeText(string text)
        {
            textBlock.Text = text;
        }

        /// <summary>
        /// Fades out the text
        /// </summary>
        public void FadeOut()
        {
            DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            textBlock.BeginAnimation(TextBlock.OpacityProperty, animation);
        }

        /// <summary>
        /// Animates the text in a pulsing manner.
        /// Must be called repeatedly with the game engine tick.
        /// </summary>
        public void PulseAnimate()
        {               
            if (fadingIn)
            {
                textBlock.Opacity += 0.1;

                if (textBlock.Opacity >= 1)
                    fadingIn = false;
            }
            else
            {
                textBlock.Opacity -= 0.1;

                if (textBlock.Opacity <= 0)
                    fadingIn = true;
            }
        }
        private bool fadingIn = false;

        public override void Reset()
        {

        }

        public override void Update()
        {

        }
    }
}
