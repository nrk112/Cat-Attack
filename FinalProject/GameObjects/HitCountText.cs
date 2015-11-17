using System.Windows;
using System.Windows.Media;

namespace FinalProject.GameObjects
{
    class HitCountText : BaseClasses.GameText
    {
        public HitCountText()
        {
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            X = 175;
            Y = 50;
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.FontSize = 45;
            textBlock.Text = "Hits: 0";

            Scale = 2 * Global.ScalingRatio;

            textBlock.Foreground = Brushes.White;
            AddToGame(ZIndexType.UI);
        }

        public override void Reset()
        {

        }

        public override void Update()
        {

        }
    }
}
