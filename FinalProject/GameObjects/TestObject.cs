using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FinalProject.GameObjects
{
    class TestObject : BaseClasses.GameObject
    {
        private static BitmapImage bitMap = null;
        protected int xTranslation;
        private int startPointSectionSize;

        public TestObject()
        {
            UseImage(Global.TeddyBear, bitMap);
            ResetObject();
            list.Add(this);
            AddToGame(ZIndexType.Game);
            startPointSectionSize = (int)MainWindow.canvas.Width / 20;
        }

        private static List<TestObject> list = new List<TestObject>();
        public static List<TestObject> List
        {
            get
            {
                return list;
            }
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        protected void ResetObject()
        {
            Scale = 0.15;
            currentState = State.Active;

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
                ResetObject();
            }
        }
    }


}
