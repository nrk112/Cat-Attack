using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FinalProject.GameObjects
{
    class HitCountText : BaseClasses.GameText
    {
        public HitCountText()
        {
            textBlock.FontFamily = new FontFamily("Arial Black");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            textBlock.TextAlignment = TextAlignment.Center;
            textBlock.FontSize = 45;
            textBlock.Text = "0";

            Scale = 1;

            textBlock.Foreground = Brushes.Red;
            AddToGame(ZIndexType.Game);
            textBlock.Visibility = Visibility.Hidden;
        }

        public override void Update()
        {
            //Location updates with parent gameobject.
        }
    }
}
