
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

        public int Hits { get; set; }
        public int Misses { get; set; }
        public int Level { get; set; }

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
        }

        double friction = 0.1;
        public override void Update()
        {
            mousePosition = Mouse.GetPosition(MainWindow.canvas);
            dX = (X - mousePosition.X) * friction;
            dY = (Y - mousePosition.Y) * friction;

            X = mousePosition.X;
            Y = mousePosition.Y;

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
                    else if (obj.currentState == State.Slow)
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
