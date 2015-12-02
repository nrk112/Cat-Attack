using FinalProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using FinalProject.GameObjects;
using System.Windows;
using System.Windows.Input;

namespace FinalProject
{
    public sealed class GameEngine
    {
        #region Singleton GameEngine
        private static volatile GameEngine instance;
        private static object syncRoot = new Object();

        private GameEngine()
        {
        }

        /// <summary>
        /// Get the instance of the engine or create a new one if not yet created.
        /// Threadsafe
        /// </summary>
        public static GameEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new GameEngine();
                    }
                    instance.InitializeEngine();
                }
                return instance;
            }
        }
        #endregion

        DispatcherTimer gameTick = new DispatcherTimer();
        List<IGameObject> gameObjects = new List<IGameObject>();
        MediaPlayer music = new MediaPlayer();
        MusicPlayer musicPlayer = new MusicPlayer();

        HitCountText hitCountText;
        TimerText timerText;

        private State currentState;

        private enum State
        {
            Loading,
            GameOver,
            Menu,
            RunningArcade,
            RunningClassic,
            Stopped
        }

        //Updates per second, not actual framerate.
        private readonly int PsuedoFPS = 60;

        public int HighScore { get; private set; }

        public int Score { get; private set; }

        /// <summary>
        /// Sets the initial settings for the game engine.
        /// </summary>
        public void InitializeEngine()
        {
            gameTick.Interval = new TimeSpan(0, 0, 0, 0, GetMSFromFPS());
            gameTick.Tick += OnGameTick;
            gameTick.Start();

            currentState = State.Loading;

            InitializeGame();
        }

        public void StartArcadeGame()
        {
            HighScore = 0;
            Score = 0;
            SetUpAllGameObjects();

            //PlayMusic();
            musicPlayer.PlayMP3(Global.MusicFilePath);
            currentState = State.RunningArcade;
        }


        /// <summary>
        /// Convert the frames per second to approximate miliseconds for use in the ticker interval.
        /// </summary>
        /// <returns></returns>
        private int GetMSFromFPS()
        {
            return 1000 / PsuedoFPS;
        }

        /// <summary>
        /// Sets or resets the initial settings for the current game setup.
        /// </summary>
        private void InitializeGame()
        {
            musicPlayer.PlayMP3(Global.MenuMusic);
            Windows.StartMenu sm = new Windows.StartMenu();
            sm.ShowDialog();
        }

        /// <summary>
        /// Adds the objects to the list of objects that will be displayed.
        /// </summary>
        /// <param name="obj"></param>
        public void AddToDisplayList(IGameObject obj)
        {
            gameObjects.Add(obj);
        }

        /// <summary>
        /// Setup any game objects in here.
        /// </summary>
        /// <param name="isFirstTime"></param>
        public void SetUpAllGameObjects(bool isFirstTime = false)
        {
            gameObjects.Clear();
            MainWindow.canvas.Children.Clear();
            AttackableObject.List.Clear();
            SpecialItem.List.Clear();

            //Create Regular objects
            for (int i = 0; i < 5; i++)
            {
                new AttackableObject();
            }

            //Create Special Objects
            for (int i = 0; i < 1; i++)
            {
                new SpecialItem();
            }

            //Create Enemies
            for (int i = 0; i < 2; i++)
            {
                new Dog();
            }

            //Create Player
            new Sword();

            //Create UI
            hitCountText = new HitCountText();
            timerText = new TimerText();
            timerText.StartTimer(true);
        }

        /// <summary>
        /// Increases the score based on the current bonus.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseScore(int amount = 1)
        {
            Score += amount;
            hitCountText.textBlock.Text = "Hits: " + Score;
        }

        /// <summary>
        /// Update all the objects in the game list every tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGameTick(object sender, EventArgs e)
        {
            switch (currentState)
            {
                case State.Loading:
                    break;
                case State.GameOver:
                    break;
                case State.Menu:
                    break;
                case State.RunningArcade:
                    //Invalid operation exception here when gameover called in this loop.
                    foreach (IGameObject obj in gameObjects)
                    {
                        obj.Update();
                    }
                    break;
                case State.RunningClassic:
                    break;
                case State.Stopped:
                    break;
                default:
                    break;
            }
        }

        public void SetGameOver(string title)
        {
            currentState = State.GameOver;

            foreach (IGameObject obj in gameObjects)
            {
                obj.Reset();
            }
            musicPlayer.PlayMP3(Global.GameOverMusic);
            Windows.StartMenu sm = new Windows.StartMenu(title, Score);
            sm.ShowDialog();
        }

        public void ResumeGame()
        {
            //TODO: If adding different game modes, need to save previouse state when escape is pressed then load the proper resume here.
            currentState = State.RunningArcade;
            timerText.StartTimer(false);
        }

        public void PauseGame()
        {
            currentState = State.Stopped;
            timerText.StopTimer();
            Windows.StartMenu sm = new Windows.StartMenu("Paused", -2);
            sm.ShowDialog();

        }

        #region Audio
        /// <summary>
        /// Play background music
        /// </summary>
        private void PlayMusic(bool autoRestart = true)
        {
            Uri uriFile = new Uri(Global.ResourcePath + Global.MusicFilePath);
            music.Open(uriFile);
            music.Volume = 0.1f;
            if (autoRestart) music.MediaEnded += new EventHandler(Media_Ended);
            music.Play();
            //System.Windows.MessageBox.Show(Global.ResourcePath.ToString());
        }

        /// <summary>
        /// Restart background music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Media_Ended(object sender, EventArgs e)
        {
            music.Position = TimeSpan.Zero;
            music.Play();
        }
        #endregion
    }
}