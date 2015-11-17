using System;
using FinalProject.BaseClasses;
using System.IO;

namespace FinalProject
{
    static class Global
    {

        #region Paths
        /// <summary>
        /// Returns the resource folder path from the root directory.
        /// </summary>
        public static string ResourcePath
        {
            get
            {
                resourcePath = System.IO.Directory.GetCurrentDirectory();
                resourcePath += "\\..\\..\\Resources\\";
                return resourcePath;
            }
        }
        private static string resourcePath;

        //Images
        public static readonly string TeddyBear = @"Images\teddy.png";
        public static readonly string Splat = @"Images\splat.png";
        public static readonly string Catnip = @"Images\catnip.png";
        public static readonly string Dog = @"Images\dog.png";
        public static readonly string Bird = @"Images\bird.png";
        public static readonly string Duck = @"Images\duck.png";
        public static readonly string Paw1 = @"Images\paw1.png";
        public static readonly string Mouse = @"Images\mouse.png";

        //Sounds
        public static readonly string MusicFilePath = @"Music\Funky Chunk.mp3";
        public static readonly string GameOverMusic = @"Music\Ossuary 7 - Resolve.mp3";
        public static readonly string MenuMusic = @"Music\Fretless.mp3";
        public static readonly string SplatSound = @"Sounds\splat.mp3";
        public static readonly string DogGrowlSound = @"Sounds\DogGrowl.wav";
        public static readonly string DogBarkSound = @"Sounds\DogBark.wav";
        #endregion

        public static readonly Random rand = new Random();

        //Game Settings
        public static readonly double MinVelocityToHit = 100;
        public static readonly int MultiHitTimeout = 3;

        /// <summary>
        /// Checks the collision between two game objects.
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        static public bool CheckCollision(GameObject obj1, GameObject obj2)
        {
            if (Math.Abs(obj1.Y - obj2.Y) < ((obj1.ScaledHeight + obj2.ScaledHeight) / 2.0))
            {
                if (Math.Abs(obj1.X - obj2.X) < ((obj1.ScaledWidth + obj2.ScaledWidth) / 2.0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
