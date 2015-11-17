using System.Windows;
using FinalProject.BaseClasses;
using System.Windows.Media;
using System.Windows.Threading;
using System;

namespace FinalProject.GameObjects
{
    class TimerText : GameText
    {
        private DispatcherTimer countdownTimer = new DispatcherTimer();

        public TimerText()
        {
            Reset();
            AddToGame(ZIndexType.UI);

            countdownTimer.Tick += new EventHandler(CountdownTimerTick);
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
        }

        private int Seconds;

        /// <summary>
        /// Countdown Timer tick event handler. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountdownTimerTick(object sender, EventArgs e)
        {
            textBlock.Text = "Time: " + --Seconds;
            if (Seconds == 0)
            {
                countdownTimer.Stop();
                GameEngine.Instance.SetGameOver("Time Over");
            }
        }

        /// <summary>
        /// Sets the game countdown timer and starts it
        /// </summary>
        public void StartTimer()
        {
            Seconds = 60;
            countdownTimer.Start();
        }

        /// <summary>
        /// Stops the count down timer.
        /// </summary>
        public void StopTimer()
        {
            countdownTimer.Stop();
        }

        /// <summary>
        /// Resets object to its original state.
        /// </summary>
        public override void Reset()
        {
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            X = MainWindow.canvas.Width - 200;
            Y = 50;
            Seconds = 60;

            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.FontSize = 45;
            textBlock.Text = "Time: " + Seconds;

            Scale = 2 * Global.ScalingRatio;

            textBlock.Foreground = Brushes.Black;
        }

        /// <summary>
        /// Unused.
        /// Self Updating through countdown timer.
        /// </summary>
        public override void Update()
        {
        }
    }
}
