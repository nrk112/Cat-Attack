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
                UseImage(Global.TeddyBear, bitMap1);
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
            Scale = 0.15;
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
            Y = MainWindow.canvas.Height + ScaledHeight + Global.rand.Next((int)ScaledHeight, (int)ScaledHeight *2);

            currentState = State.Active;
        }

        //Physics properties
        protected double gravity = 0.75;
        protected double friction = 1.0;

        public override void Update()
        {
            if (currentState == State.Hit)
            {
                //X = MainWindow.canvas.Width + this.Width;
                currentState = State.Inactive;
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
