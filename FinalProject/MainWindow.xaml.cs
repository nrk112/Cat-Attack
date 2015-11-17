using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        public static Canvas canvas { get; set; }

        public GameEngine gameEngine
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Called when the main window is loaded. Add startup functionality here.
        /// </summary>
        void MainWindow_Loaded(object sender, EventArgs e)
        {
            canvas = new Canvas();
            canvas.Background = Brushes.Azure;

            //Use a background image
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"../../Resources/Images/background2.jpg", UriKind.Relative));
            ib.Stretch = Stretch.Fill;
            canvas.Background = ib;

            mainGrid.Height = Height - 40;
            mainGrid.Width = Width;
            canvas.Height = mainGrid.Height;
            canvas.Width = mainGrid.Width;

            mainGrid.Children.Add(canvas);

            //Load game engine after canvas has been setup.
            gameEngine = GameEngine.Instance;
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                GameEngine.Instance.PauseGame();
            }
        }
    }
}
