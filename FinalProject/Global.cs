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
        public static readonly string SwordToLeft = @"Images\swordToLeft.png";
        public static readonly string SwordToRight = @"Images\swordToRight.png";
        public static readonly string Catnip = @"Images\catnip.png";

        //Sounds
        public static readonly string MusicFilePath = @"Music\Funky Chunk.mp3";
        public static readonly string SplatSound = @"Sounds\splat.mp3";
        #endregion

        public static readonly Random rand = new Random();
        public static readonly double MinVelocityToHit = 150;
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
