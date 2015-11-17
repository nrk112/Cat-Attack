using FinalProject.BaseClasses;
using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace FinalProject.GameObjects
{
    class Dog : GameObject
    {
        private static BitmapImage bitMap = null;
        protected int xTranslation;
        protected int startPointSectionSize;

        private static MediaPlayer growlSound = new MediaPlayer();
        private static MediaPlayer barkSound = new MediaPlayer();

        private double startingScale = 0.3;

        public Dog()
        {
            string fileName = Global.ResourcePath + Global.DogGrowlSound;
            Uri uriFile = new Uri(fileName);
            growlSound.Open(uriFile);

            fileName = Global.ResourcePath + Global.DogBarkSound;
            uriFile = new Uri(fileName);
            barkSound.Open(uriFile);

            UseImage(Global.Dog, bitMap);
            Reset();

            AttackableObject.List.Add(this as GameObject);
            AddToGame(ZIndexType.Game);
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        public override void Reset()
        {
            Scale = startingScale * Global.ScalingRatio;
            Hits = 0;
            fadingOut = false;
            Element.Opacity = 1.0;
            bonusMultiplier = -10;

            //Set gravity based on resolution
            if (MainWindow.canvas.Width > 2000)
                gravity = 0.85 + Global.rand.NextDouble() * 0.3;
            else
                gravity = 0.70 + Global.rand.NextDouble() * 0.2 * Global.ScalingRatio;

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

            dY = -50.0 * Global.ScalingRatio;
            Angle = 0.0;
            Y = MainWindow.canvas.Height + ScaledHeight;

            currentState = State.Ready;
        }

        public void FadeOut()
        {
            //Currently only plays once....  

            //if (!fadingOut)
            //{
            //    DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
            //    Element.BeginAnimation(Image.OpacityProperty, animation);
            //    fadingOut = true;
            //}
        }
        private bool fadingOut = false;

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
                    currentState = State.Animating;
                    growlSound.Stop();
                    barkSound.Stop();
                    barkSound.Play();
                    break;
                case State.Ready:
                    int msToWait = 240;
                    Activate(msToWait);
                    break;
                case State.Inactive:
                    //Do Nothing
                    break;
                case State.Animating:
                    Scale += 0.3 * Global.ScalingRatio;
                    FadeOut();
                    if (Scale >= 3.0 * Global.ScalingRatio)
                    {
                        //GameEngine.Instance.SetGameOver();
                        if (Element.Opacity <= 0)
                        {
                            Scale = startingScale * Global.ScalingRatio;
                            X = MainWindow.canvas.Width + this.Width;
                        }

                        Reset();
                    }
                    break;
                case State.Sleeping:
                    tickCount++;
                    if (tickCount > randomStartValue)
                    {
                        currentState = State.Active;
                        growlSound.Stop();
                        growlSound.Play();
                    }
                    break;
                default:
                    break;
            }

            //If object is missed and falls off the screen reset it.
            if (Y >= MainWindow.canvas.Height + 3 * ScaledHeight)
            {
                Reset();
            }
        }
    }
}

