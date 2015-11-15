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
        private static List<TestObject> list = new List<TestObject>();
        private int Level;
        private State currentState;

        private enum State
        {
            Active,
            Inactive,
            Hit
        }

        public TestObject()
        {
            UseImage(Global.TeddyBear, bitMap);
            ResetObject();
            list.Add(this);
            AddToGame();
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        private void ResetObject()
        {
            Scale = 0.3;
            currentState = State.Active;
            Level = 1;
            dX = MainWindow.canvas.Width /4;
            dY = -45.0;
            Angle = 0.0;
            Y = MainWindow.canvas.Height + ScaledHeight;
            X = MainWindow.canvas.Width;
        }

        //Physics properties
        private double gravity = 0.7;
        private double friction = 1.0;

        public override void Update()
        {
            if (currentState == State.Hit)
            {
                //Play hit animation here;
                currentState = State.Inactive;
            }
            if (currentState == State.Active)
            {
                dY += gravity;
                dX += 3;
                
                dX *= friction;
                dY *= friction;

                X = dX;
                Y += dY;
            }
            if (Y >= MainWindow.canvas.Height + 2 * ScaledHeight)
            {
                ResetObject();
            }
        }
    }


}
