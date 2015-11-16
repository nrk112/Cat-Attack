using FinalProject.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace FinalProject.GameObjects
{
    class SpecialItem : BaseClasses.GameObject
    {
        DispatcherTimer multiHitTimer = new DispatcherTimer();
        private static BitmapImage bitMap = null;

        private static List<SpecialItem> list = new List<SpecialItem>();
        public static List<SpecialItem> List
        {
            get
            {
                return list;
            }
        }

        protected int xTranslation;
        protected int startPointSectionSize;

        private double Slowdown = 0.1;

        private SpecialHitCountText hitCountText;
        private AttackText attackText;

        public SpecialItem()
        {
            UseImage(Global.Catnip, bitMap);
            Reset();

            multiHitTimer.Interval = TimeSpan.FromSeconds(Global.MultiHitTimeout);
            multiHitTimer.Tick += new EventHandler(MultihitTimeout);
            hitCountText = new SpecialHitCountText();
            attackText = new AttackText();

            list.Add(this);
            AddToGame(ZIndexType.Game);
        }

        /// <summary>
        /// Event handler called when the alloted time to have multiple attacks runs out.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultihitTimeout(object sender, EventArgs e)
        {
            currentState = State.Inactive;
            hitCountText.FadeOut();
            attackText.FadeOut();
            multiHitTimer.Stop();
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        public override void Reset()
        {
            Scale = 0.5;
            Hits = 0;
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;

            int rand = Global.rand.Next(1, 19);
            X = startPointSectionSize * rand;

            if (rand < 5)
            {
                xTranslation = Global.rand.Next(5, 15);
            }
            else if (rand < 10)
            {
                xTranslation = Global.rand.Next(3, 10);
            }
            else if (rand < 15)
            {
                xTranslation = Global.rand.Next(3, 10) * -1;
            }
            else
            {
                xTranslation = Global.rand.Next(5, 15) * -1;
            }

            dY = -50.0;
            Angle = 0.0;
            Y = MainWindow.canvas.Height + ScaledHeight + Global.rand.Next((int)ScaledHeight, (int)ScaledHeight * 2);

            currentState = State.Active;
        }

        //Physics properties
        protected double gravity = 0.75;
        protected double friction = 1.0;

        /// <summary>
        /// Updates this object every tick of the game engine.
        /// </summary>
        public override void Update()
        {
            if (currentState == State.Hit)
            {
                currentState = State.Slow;
                hitCountText.textBlock.Visibility = System.Windows.Visibility.Visible;
                hitCountText.ChangeText("Hits: " + Hits);

                attackText.textBlock.Visibility = System.Windows.Visibility.Visible;
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

                CalcBonus();

                hitCountText.ChangeText("Hits: " + Hits);
                hitCountText.X = X + ScaledWidth / 2;
                hitCountText.Y = Y - ScaledHeight / 2;

                attackText.PulseAnimate();
                attackText.X = X;
                attackText.Y = Y + ScaledHeight;
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
                Reset();
            }
        }

        private void CalcBonus()
        {
            if (Hits > 10 && Hits <= 20)
                attackText.ChangeText("2X");
            else if (Hits > 20)
                attackText.ChangeText("3X");
        }
    }
}
