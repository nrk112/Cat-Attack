using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FinalProject.GameObjects
{
    class AttackableObject : BaseClasses.GameObject
    {
        private static BitmapImage bitMap1 = null;
        private static BitmapImage bitMap2 = null;
        private static BitmapImage bitMap3 = null;
        protected int xTranslation;
        protected int startPointSectionSize;

        public AttackableObject()
        {
            //TODO: can this be moved to the reset function?  Will the images be properly replaced?
            UseRandomImage();
            Reset();
            list.Add(this);
            AddToGame(ZIndexType.Game);
        }

        private static List<BaseClasses.GameObject> list = new List<BaseClasses.GameObject>();
        public static List<BaseClasses.GameObject> List
        {
            get
            {
                return list;
            }
        }

        private void UseRandomImage()
        {
            int rand = Global.rand.Next(1, 100);
            if (rand < 33)
            {
                UseImage(Global.Mouse, bitMap1);
            }
            else if (rand < 66)
            {
                UseImage(Global.Bird, bitMap2);
            }
            else
            {
                UseImage(Global.Duck, bitMap3);
            }
            
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        public override void Reset()
        {
            Scale = 0.15 * Global.ScalingRatio;
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
            //Y = MainWindow.canvas.Height + ScaledHeight + Global.rand.Next((int)ScaledHeight, (int)ScaledHeight *2);
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
            currentState = State.Sleeping;
        }
        private int tickCount = 0;
        private int randomStartValue = 0;

        //Physics properties
        protected double gravity = 0.85 * Global.ScalingRatio;
        protected double friction = 1.0;

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
                    break;
                case State.Hit:
                    X = MainWindow.canvas.Width + this.Width;
                    //currentState = State.Inactive;
                    Reset();
                    break;
                case State.Ready:
                    int msToWait = 120;
                    Activate(msToWait);
                    break;
                case State.Inactive:
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
    }


}
