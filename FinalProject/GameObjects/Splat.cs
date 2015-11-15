using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FinalProject.GameObjects
{
    class Splat : BaseClasses.GameObject
    {
        private static BitmapImage bitMap = null;
        private static List<BaseClasses.GameObject> list = new List<BaseClasses.GameObject>();
        private int xTranslation;
        private State currentState;

        public Splat()
        {
            UseImage(Global.Splat, bitMap);
            ResetObject();
            list.Add(this);
            AddToGame(ZIndexType.Background);
        }

        /// <summary>
        /// Sets the objects properties back to the default.
        /// </summary>
        private void ResetObject()
        {
            Scale = 1.0;
            //TODO make state active by chance
            currentState = State.Active;
            dX = MainWindow.canvas.Width / 4;
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
            else if (currentState == State.Active)
            {
                dY += gravity;
                dX += 3;

                dX *= friction;
                dY *= friction;

                X = dX;
                Y += dY;
            }
            else if (currentState == State.Slow)
            {
                dY += gravity;
                dX += 3;

                dX *= friction;
                dY *= friction;

                X = dX;
                Y += dY;
            }

            //Reset object when its no longer in play.
            if (Y >= MainWindow.canvas.Height + 2 * ScaledHeight)
            {
                ResetObject();

            }
        }
    }


}
