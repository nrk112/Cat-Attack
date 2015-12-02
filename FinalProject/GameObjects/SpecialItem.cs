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
        private double startingScale = 0.5;

        private SpecialHitCountText hitCountText;
        private AttackText attackText;

        public SpecialItem()
        {
            UseImage(Global.Catnip, bitMap);

            hitCountText = new SpecialHitCountText();
            attackText = new AttackText();
            Reset();

            multiHitTimer.Interval = TimeSpan.FromSeconds(Global.MultiHitTimeout);
            multiHitTimer.Tick += new EventHandler(MultihitTimeout);

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
            X = MainWindow.canvas.Width + this.Width;
            currentState = State.Ready;
            hitCountText.FadeOut();
            attackText.FadeOut();
            multiHitTimer.Stop();
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        public override void Reset()
        {
            Scale = startingScale * Global.ScalingRatio;
            Hits = 0;
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;
            bonusMultiplier = 1;


            //Set gravity based on resolution
            if (MainWindow.canvas.Width < 1500)
            {
                gravity = 0.60 + Global.rand.NextDouble() * 0.2 * Global.ScalingRatio;
                dY = -60.0 * Global.ScalingRatio;
            }
            else if (MainWindow.canvas.Width > 2000)
            {
                dY = -50.0 * Global.ScalingRatio;
                gravity = 0.85 + Global.rand.NextDouble() * 0.3;
            }
            else
            {
                dY = -50.0 * Global.ScalingRatio;
                gravity = 0.70 + Global.rand.NextDouble() * 0.2 * Global.ScalingRatio;
            }

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

            //dY = -50.0 * Global.ScalingRatio;
            Angle = 0.0;
            Y = MainWindow.canvas.Height + ScaledHeight;

            currentState = State.Ready;
        }

        /// <summary>
        /// Sets up a random time before starting movement of object, then sets its state to active.
        /// Default wait is immediate.
        /// </summary>
        /// <param name="maxWaitTime">The maximum wait time in milliseconds allowed before starting. Leave empty for immediate.</param>
        public void Activate(int maxWaitTime = 1)
        {
            tickCount = 0;
            randomStartValue = Global.rand.Next(1, maxWaitTime);
            randomStartValue += 120;
            currentState = State.Sleeping;
            hitCountText.Element.Visibility = System.Windows.Visibility.Collapsed;
            attackText.Element.Visibility = System.Windows.Visibility.Collapsed;
        }
        private int tickCount = 0;
        private int randomStartValue = 0;

        //Physics properties
        protected double gravity = 0.85 * Global.ScalingRatio;
        protected double friction = 1.0;

        /// <summary>
        /// Updates this object every tick of the game engine.
        /// </summary>
        public override void Update()
        {
            switch (currentState)
            {
                case State.Active:
                    dY += gravity;
                    dX += 0;

                    dX *= friction;
                    dY *= friction;

                    X += xTranslation;
                    Y += dY;
                    break;
                case State.Slow:
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
                    break;
                case State.Hit:
                    currentState = State.Slow;
                    hitCountText.textBlock.Visibility = System.Windows.Visibility.Visible;
                    hitCountText.ChangeText("Hits: " + Hits);

                    attackText.textBlock.Visibility = System.Windows.Visibility.Visible;
                    break;
                case State.Ready:
                    int msToWait = 240;
                    Activate(msToWait);
                    break;
                case State.Inactive:
                    //Do Nothing
                    break;
                case State.Animating:
                    break;
                case State.Sleeping:
                    tickCount++;
                    if (tickCount > randomStartValue)
                    {
                        currentState = State.Active;
                    }
                    break;
                default:
                    break;
            }

            //Reset object when its no longer in play.
            if (Y >= MainWindow.canvas.Height + 3 * ScaledHeight)
            {
                Reset();
            }
        }

        /// <summary>
        /// Calculates the amount that each hit will provide.
        /// </summary>
        private void CalcBonus()
        {
            if (Hits <= 6)
            {
                bonusMultiplier = 1;
                attackText.ChangeText("Attack!!");
            }
            else if (Hits <= 12)
            {
                bonusMultiplier = 2;
                attackText.ChangeText("2X");
            }
            else if (Hits <= 18)
            {
                bonusMultiplier = 3;
                attackText.ChangeText("3X");
            }
            else if (Hits <= 24)
            {
                bonusMultiplier = 4;
                attackText.ChangeText("4X");
            }
            else if (Hits <= 30)
            {
                bonusMultiplier = 5;
                attackText.ChangeText("5X");
            }
            else if (Hits > 30)
            {
                bonusMultiplier = 10;
                attackText.ChangeText("AWESOME!");
            }
        }
    }
}
