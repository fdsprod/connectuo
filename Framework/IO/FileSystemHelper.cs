using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConnectUO.Framework.IO
{
    public static class FileSystemHelper
    {
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string GetValidDirectoryName(string directoryName)
        {
            char[] invalidFileChars = Path.GetInvalidFileNameChars();

            for (int i = 0; i < invalidFileChars.Length; i++)
            {
                directoryName = directoryName.Replace(invalidFileChars[i], '_');
            }

            return directoryName;
        }
    }
}
