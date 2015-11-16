using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FinalProject.GameObjects
{
    class SpecialItem : TestObject
    {
        DispatcherTimer multiHitTimer = new DispatcherTimer();
        private static BitmapImage bitMap = null;


        private double Slowdown = 0.1;
        private SpecialHitCountText hitCountText;

        public SpecialItem()
        {
            UseImage(Global.Catnip, bitMap);
            ResetObject();
            AddToGame(ZIndexType.Game);
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;

            int hours = 0;
            int minutes = 0;
            int seconds = 3;
            multiHitTimer.Interval = new TimeSpan(hours, minutes, seconds);
            multiHitTimer.Tick += new EventHandler(MultihitTimeout);
            hitCountText = new SpecialHitCountText();

            Scale = 1.0;
        }

        private void MultihitTimeout(object sender, EventArgs e)
        {
            currentState = State.Inactive;
            hitCountText.textBlock.Visibility = System.Windows.Visibility.Hidden;
            multiHitTimer.Stop();
        }

        public override void Update()
        {
            if (currentState == State.Hit)
            {
                currentState = State.Slow;
                hitCountText.textBlock.Visibility = System.Windows.Visibility.Visible;
                hitCountText.ChangeText(Hits.ToString());
            }
            else if (currentState == State.Slow)
            {
                if (!multiHitTimer.IsEnabled)
                {
                    multiHitTimer.Start();
                }

                dY += gravity;
                dX += 0;

                dX *= friction;
                dY *= friction;

                X += xTranslation * Slowdown;
                Y += dY * Slowdown;

                hitCountText.ChangeText(Hits.ToString());
                hitCountText.X = X + ScaledWidth / 2;
                hitCountText.Y = Y - ScaledHeight / 2;
            }
            else if (currentState == State.Active)
            {
                dY += gravity;
                dX += 0;

                dX *= friction;
                dY *= friction;

                X += xTranslation;
                Y += dY;
            }

            //Reset object when its no longer in play.
            if (Y >= MainWindow.canvas.Height + 3 * ScaledHeight)
            {
                ResetObject();
            }
        }
    }
}
