
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FinalProject.BaseClasses;

namespace FinalProject.GameObjects
{
    public class Sword : BaseClasses.GameObject
    {
        private static BitmapImage bitmap = null;
        private Point mousePosition = new Point();
        private double startingScale = 0.05;
        private static MediaPlayer splatSound = new MediaPlayer();
        private static MediaPlayer levelDownSound = new MediaPlayer();

        public Sword()
        {
            UseImage(Global.Paw1, bitmap);
            Reset();

            string fileName = Global.ResourcePath + Global.SplatSound;
            Uri uriFile = new Uri(fileName);
            splatSound.Open(uriFile);

            AddToGame(ZIndexType.UI);
        }

        /// <summary>
        /// Calculates the velocity of the mouse movement.
        /// </summary>
        /// <returns></returns>
        private double CalculateVelocity()
        {
            double velocity = Math.Sqrt((Math.Pow(dX, 2) + Math.Pow(dY, 2)));
            return velocity;
        }

        /// <summary>
        /// Calculates the angle in degrees of the movement of the mouse.
        /// </summary>
        /// <returns></returns>
        private double CalculateAngle()
        {
            double angleRadians = Math.Atan2(dY, dX);
            double angleDegrees = (angleRadians * 180) / Math.PI;
            return angleDegrees;
        }

        private double friction = 1.0;
        private double lastAngle = 0.0;
        private double dAngle = 0.0;
        private double currentAngle = 0.0;

        public override void Reset()
        {
            Hits = 0;

            mousePosition = Mouse.GetPosition(MainWindow.canvas);
            X = mousePosition.X;
            Y = mousePosition.Y;
            Scale = startingScale;
            currentState = State.Active;
        }

        public override void Update()
        {
            mousePosition = Mouse.GetPosition(MainWindow.canvas);
            dX = (mousePosition.X - X) * friction;
            dY = (mousePosition.Y - Y) * friction;

            X = mousePosition.X;
            Y = mousePosition.Y;

            //Calculate the angles to see if this is a new strike pattern.
            currentAngle = CalculateAngle();
            dAngle = currentAngle - lastAngle;
            lastAngle = currentAngle;

            //If its a new strike pattern, give it a new ID.
            if (dAngle > 20)
            {
                HitID = Global.rand.Next(1, int.MaxValue);
            }

            //Make sure mouse is fast enough to make a hit.
            if (CalculateVelocity() > Global.MinVelocityToHit)
            {
                //Check normal objects
                foreach (GameObject obj in AttackableObject.List)
                {
                    if (Global.CheckCollision(this, obj))
                    {
                        if (obj.currentState == State.Active)
                        {
                            OnActiveHit(obj);
                        }
                    }
                }

                //Check Special Objects
                foreach (GameObject obj in SpecialItem.List)
                {
                    if (Global.CheckCollision(this, obj))
                    {
                        if (obj.currentState == State.Active)
                        {
                            OnActiveHit(obj);
                        }
                        else if (obj.currentState == State.Slow && HitID != obj.HitID)
                        {
                            splatSound.Stop();
                            splatSound.Play();
                            obj.Hits++;
                            GameEngine.Instance.IncreaseScore(obj.GetScorePerHit());
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Instructions to run when a game object in the active state is hit. 
        /// </summary>
        /// <param name="obj">The game object to modify</param>
        private void OnActiveHit(GameObject obj)
        {
            splatSound.Stop();
            splatSound.Play();
            obj.Hits++;
            obj.currentState = State.Hit;
            GameEngine.Instance.IncreaseScore(obj.GetScorePerHit());
        }
    }
}
