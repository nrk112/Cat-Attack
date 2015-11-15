using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace FinalProject.BaseClasses
{
    abstract class GameObject : Interfaces.IGameObject
    {
        /// <summary>
        /// Constructor sets initial values.
        /// </summary>
        public GameObject()
        {

        }

        private FrameworkElement _element;
        /// <summary>
        /// The is the base element that makes up what this game object is.
        /// </summary>
        protected FrameworkElement Element
        {
            get
            {
                if (_element == null)
                {
                    _element = new FrameworkElement();
                }
                return _element;
            }
            set
            {
                _element = value;
                InitializeTransformGroup();
                _element.RenderTransform = transformGroup;
            }
        }

        /// <summary>
        /// Set up the transofmations used for the Element.
        /// </summary>
        private void InitializeTransformGroup()
        {
            rotateTransform = new RotateTransform();
            rotateTransform.CenterX = _element.Width / 2.0;
            rotateTransform.CenterY = _element.Height / 2.0;

            scaleTransform = new ScaleTransform();
            scaleTransform.CenterX = _element.Width / 2.0;
            scaleTransform.CenterY = _element.Height / 2.0;

            translateTransform = new TranslateTransform();

            transformGroup = new TransformGroup();
            transformGroup.Children.Add(rotateTransform);
            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
        }

        protected RotateTransform rotateTransform { get; set; }
        protected ScaleTransform scaleTransform { get; set; }
        protected TranslateTransform translateTransform { get; set; }
        protected TransformGroup transformGroup { get; set; }

        /// <summary>
        /// Updates the object.
        /// Should be called every tick of the game engine.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Adds this object to the list of game objects
        /// </summary>
        protected void AddToGame()
        {
            MainWindow.canvas.Children.Add(_element);
            GameEngine.Instance.AddToDisplayList(this);
        }

        /// <summary>
        /// Replaces the Element with an image.
        /// </summary>
        /// <param name="imageFileName"></param>
        /// <param name="b"></param>
        protected void UseImage(string imageFileName, BitmapImage b)
        {
            if (b == null)
            {
                b = new BitmapImage();
                b.BeginInit();
                b.UriSource = new Uri(Global.ResourcePath + imageFileName);
                b.EndInit();
            }

            //TODO: make images scale based on screen size.
            Image baseElement = new Image();
            baseElement.Source = b;
            baseElement.Stretch = Stretch.Fill;
            baseElement.Height = b.Height;
            baseElement.Width = b.Width;
            Element = baseElement;
        }

        /// <summary>
        /// Change in X value.
        /// To be used in the update method.
        /// </summary>
        protected double dX { get; set; }

        /// <summary>
        /// Change in Y value.
        /// To be used in the update method.
        /// </summary>
        protected double dY { get; set; }

        /// <summary>
        /// Sets the X coordinate of the object by center point.
        /// </summary>
        protected double X
        {
            get { return translateTransform.X + Width / 2.0; }
            set { translateTransform.X = value - Width / 2.0; }
        }

        /// <summary>
        /// Sets the Y coordinate of the object by center point.
        /// Y is inverse (Top to Bottom increases)
        /// </summary>
        protected double Y
        {
            get { return translateTransform.Y + Height / 2.0; }
            set { translateTransform.Y = value - Height / 2.0; }
        }

        /// <summary>
        /// Sets the scale of the object where 1 is normal size.
        /// </summary>
        protected double Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                scaleTransform.ScaleX = value;
                scaleTransform.ScaleY = value;
            }
        }
        private double scale;

        /// <summary>
        /// Sets the angle in degrees that the object should be set to.
        /// </summary>
        protected double Angle
        {
            get { return rotateTransform.Angle; }
            set { rotateTransform.Angle = value; }
        }

        /// <summary>
        /// Sets the height of the Element.
        /// </summary>
        protected double Height
        {
            get { return Element.Height; }
            set
            {
                Element.Height = value;
                SetCenterPoint();
            }
        }

        /// <summary>
        /// The actual width of the Element
        /// Also sets the centerpoint
        /// </summary>
        protected double Width
        {
            get { return Element.Width; }
            set
            {
                Element.Width = value;
                SetCenterPoint();
            }
        }

        /// <summary>
        /// Sets the centerpoint of the object.
        /// Used for rotation and scaling.
        /// </summary>
        private void SetCenterPoint()
        {
            rotateTransform.CenterX = Element.Width / 2.0;
            rotateTransform.CenterY = Element.Height / 2.0;
            scaleTransform.CenterX = Element.Width / 2.0;
            scaleTransform.CenterY = Element.Height / 2.0;
        }

        /// <summary>
        /// Returns the height based on the set scaling.
        /// </summary>
        protected double ScaledHeight
        {
            get { return Element.Height * scaleTransform.ScaleY; }
        }

        /// <summary>
        /// Returns the width based on the set scaling.
        /// </summary>
        public double ScaledWidth
        {
            get { return Element.Width * scaleTransform.ScaleX; }
        }
    }
}

