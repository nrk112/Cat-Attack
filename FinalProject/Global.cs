using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    static class Global
    {

        /// <summary>
        /// Returns the resource folder path from the root directory.
        /// </summary>
        private static string resourcePath;
        public static string ResourcePath
        {
            get
            {
                resourcePath = System.IO.Directory.GetCurrentDirectory();
                resourcePath += "\\..\\..\\Resources\\";
                return resourcePath;
            }
        }

        public static readonly string MusicFilePath =  @"Music\Funky Chunk.mp3";
        public static readonly string TeddyBear = @"Images\teddy.png";
    }
}
