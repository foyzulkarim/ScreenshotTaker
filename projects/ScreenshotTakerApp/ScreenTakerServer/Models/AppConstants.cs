using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScreenTakerServer.Models
{
    using System.IO;

    public static class AppConstants
    {
        public static string Folder = @"C:\temp\team";

        static AppConstants()
        {
            CheckFolder();
        }

        private static void CheckFolder()
        {
            bool exists = Directory.Exists(Folder);
            if (!exists)
            {
                var directoryInfo = Directory.CreateDirectory(Folder);
            }
        }
    }
}