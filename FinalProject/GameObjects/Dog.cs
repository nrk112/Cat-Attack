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
            //list.Add(this);
            AttackableObject.List.Add(this as GameObject);
            AddToGame(ZIndexType.Game);
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        public override void Reset()
        {
            Scale = startingScale;
            Hits = 0;
            fadingOut = false;

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
            growlSound.Stop();
            growlSound.Play();

            currentState = State.Active;
        }

        public void FadeOut()
        {
            if (!fadingOut)
            {
                DoubleAnimation animation = new DoubleAnimation(0, TimeSpan.FromSeconds(2));
                Element.BeginAnimation(Image.OpacityProperty, animation);
                fadingOut = true;
            }
        }
        private bool fadingOut = false;

        //Physics properties
        protected double gravity = 0.75;
        protected double friction = 1.0;

        public override void Update()
        {
            if (currentState == State.Hit)
            {

                currentState = State.Animating;
                growlSound.Stop();
                barkSound.Stop();
                barkSound.Play();
            }
            else if (currentState == State.Animating)
            {
                Scale += 0.3;
                FadeOut();
                if (Scale >= 3.0)
                {
                    GameEngine.Instance.SetGameOver();
                    if (Element.Opacity <= 0)
                    {
                        Scale = startingScale;
                        X = MainWindow.canvas.Width + this.Width;
                    }
                        
                    currentState = State.Inactive;
                }
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
    }
}

