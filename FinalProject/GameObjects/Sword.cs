
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
        private double startingScale = 0.5;
        private MediaPlayer splatSound = new MediaPlayer();
        private MediaPlayer levelDownSound = new MediaPlayer();

        public int Misses { get; set; }
        public int Level { get; set; }

        private HitCountText velocityText = new HitCountText();

        public Sword()
        {
            Hits = 0;
            Misses = 0;
            Level = 1;

            UseImage(Global.SwordToLeft, bitmap);

            string fileName = Global.ResourcePath + Global.SplatSound;
            Uri uriFile = new Uri(fileName);
            splatSound.Open(uriFile);

            //fileName = Global.LevelDownSound;
            //uriFile = new Uri(fileName, UriKind.Relative);
            //levelDownSound.Open(uriFile);

            mousePosition = Mouse.GetPosition(MainWindow.canvas);
            X = mousePosition.X;
            Y = mousePosition.Y;
            Scale = startingScale;
            AddToGame(ZIndexType.Game);
            velocityText.textBlock.Visibility = Visibility.Visible;
        }

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
        private bool sameStrike = false;

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

            //velocityText.textBlock.Text = currentAngle.ToString();

            //Make sure mouse is fast enough to make a hit.
            if (CalculateVelocity() > Global.MinVelocityToHit)
            {
                foreach (GameObject obj in TestObject.List)
                {
                    if (Global.CheckCollision(this, obj))
                    {
                        if (obj.currentState == State.Active)
                        {
                            splatSound.Stop();
                            splatSound.Play();
                            Hits++;
                            obj.Hits++;
                            obj.currentState = State.Hit;
                            GameEngine.Instance.IncreaseScore();
                        }
                        //For Special Objects
                        else if (obj.currentState == State.Slow && HitID != obj.HitID)
                        {
                            splatSound.Stop();
                            splatSound.Play();
                            Hits++;
                            obj.Hits++;
                            GameEngine.Instance.IncreaseScore();
                        }
                    }
                }
            }

        }
    }
}
