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
            textBlock.FontFamily = new FontFamily("Impact Regular");
            textBlock.Height = 100.0;
            textBlock.Width = 300.0;
            X = MainWindow.canvas.Width - 200;
            Y = 50;
            textBlock.TextAlignment = TextAlignment.Left;
            textBlock.FontSize = 45;
            textBlock.Text = "Time: 60";

            Scale = 2;
            Seconds = 60;

            textBlock.Foreground = Brushes.Black;
            AddToGame(ZIndexType.UI);

            countdownTimer.Tick += new EventHandler(CountdownTimerTick);
            int hours = 0;
            int minutes = 0;
            int timerSeconds = 1;

            countdownTimer.Interval = new TimeSpan(hours, minutes, timerSeconds);
        }

        private int Seconds;

        private void CountdownTimerTick(object sender, EventArgs e)
        {
            textBlock.Text = "Time: " + --Seconds;
            if (Seconds == 0)
            {
                countdownTimer.Stop();
            }
        }

        public void StartTimer()
        {
            Seconds = 60;
            countdownTimer.Start();
        }

        public override void Update()
        {
        }
    }
}
