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
    class SpecialHitCountText : BaseClasses.GameText
    {
        public SpecialHitCountText()
        {
            Reset();
            AddToGame(ZIndexType.Game);
        }

        public void ChangeText(string text)
        {
            //Scale = 1.0;
            textBlock.Text = text;
            //animate = true;
        }

        public void FadeOut()
        {
            //DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(1));
            //textBlock.BeginAnimation(TextBlock.OpacityProperty, animation);
        }

        public override void Reset()
        {
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.FontSize = 45;
            textBlock.Text = "0";
            textBlock.Foreground = Brushes.YellowGreen;
            textBlock.Visibility = Visibility.Hidden;

            Scale = 1.5 * Global.ScalingRatio;
        }

        public override void Update()
        {
            //Position updates with parent gameobject.

            //if (animate)
            //{
            //    ticks++;
            //    if (ticks < 5)
            //    {
            //        Scale += 0.05;
            //    }
            //    else if (ticks < 10)
            //    {
            //        Scale -= 0.05;
            //    }
            //    else
            //    {
            //        animate = false;
            //        ticks = 0;
            //    }
            //}
        }
    }
}
