using FinalProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Threading;
using FinalProject.GameObjects;

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

        HitCountText hitCountText;
        TimerText timerText;

        //Updates per second, not actual framerate.
        private readonly int PsuedoFPS = 60;

        public int BonusMultiplier { get; private set; }

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
            InitializeGame();
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
            BonusMultiplier = 1;
            HighScore = 0;
            Score = 0;
            SetUpAllGameObjects();
            PlayMusic();
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
            new TestObject();
            new TestObject();
            new TestObject();
            new TestObject();
            new SpecialItem();
            new Sword();

            hitCountText = new HitCountText();
            timerText = new TimerText();
            timerText.StartTimer();
        }

        /// <summary>
        /// Increases the score based on the current bonus.
        /// </summary>
        /// <param name="amount"></param>
        public void IncreaseScore(int amount = 1)
        {
            Score += amount * BonusMultiplier;
            hitCountText.textBlock.Text = "Hits: " + Score.ToString();
        }

        /// <summary>
        /// Increases the bonus multiplier by algorithm
        /// </summary>
        public void IncreaseBonus()
        {
            if (BonusMultiplier == 1)
                BonusMultiplier++;
            else if (BonusMultiplier <= 512)
                BonusMultiplier = BonusMultiplier * 2;
        }

        /// <summary>
        /// Resets the bonus multiplier
        /// </summary>
        public void ClearBonus()
        {
            BonusMultiplier = 1;
        }

        /// <summary>
        /// Update all the objects in the game list every tick.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OnGameTick(object sender, EventArgs e)
        {
            foreach (IGameObject obj in gameObjects)
            {
                obj.Update();
            }
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