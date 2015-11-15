using System.Windows.Controls;

namespace FinalProject.BaseClasses
{
    public abstract class GameText : GameObject
    {
        public GameText() : base()
        {
            TextBlock textBlock = new TextBlock();
            textBlock.Width = 1;
            textBlock.Height = 1;
            Element = textBlock;
        }

        public TextBlock textBlock
        {
            get
            {
                return (TextBlock)Element;
            }
        }
    }
}