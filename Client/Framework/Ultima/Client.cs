using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;

namespace ConnectUO.Framework.Ultima
{
    public class Client
    {    
        private static List<string> validClientPaths = new List<string>();

        public static List<string> ValidClientPaths
        {
            get { return Client.validClientPaths; }
            set { Client.validClientPaths = value; }
        }

        static Client()
        {
            validClientPaths = LoadDirectories();
        }

        private static string GetExePath(string subName)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));
                if (key == null)
                {
                    key = Registry.CurrentUser.OpenSubKey(string.Format(@"SOFTWARE\{0}", subName));
                    if (key == null)
                    {
                        return null;
                    }
                }
                string path = key.GetValue("ExePath") as string;
                if (((path == null) || (path.Length <= 0)) || (!Directory.Exists(path) && !File.Exists(path)))
                {
                    path = key.GetValue("Install Dir") as string;
                    if (((path == null) || (path.Length <= 0)) || (!Directory.Exists(path) && !File.Exists(path)))
                    {
                        return null;
                    }
                }
                path = Path.GetDirectoryName(path);
                if ((path == null) || !Directory.Exists(path))
                {
                    return null;
                }
                return path;
            }
            catch
            {
                return null;
            }
        }
        private static List<string> LoadDirectories()
        {
            List<string> dirs = new List<string>();

            for (int i = 0; i < knownRegkeys.Length; i++)
            {
                string exePath;
                if (IntPtr.Size == 8)
                {
                    exePath = GetExePath(@"Wow6432Node\" + knownRegkeys[i]);
                }
                else
                {
                    exePath = GetExePath(knownRegkeys[i]);
                }
                if ((exePath != null) && !dirs.Contains(exePath))
                {
                    dirs.Add(exePath);
                }
            }

            for (int j = 0; j < knownDirectories.Length; j++)
            {
                if (Directory.Exists(knownDirectories[j]))
                {
                    dirs.Add(Path.GetDirectoryName(knownDirectories[j]));
                }
            }

            return dirs;
        }

        static readonly string[] knownRegkeys = new string[] { 
                @"Origin Worlds Online\Ultima Online\KR Legacy Beta", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", 
                @"Origin Worlds Online\Ultima Online\1.0", 
                @"Origin Worlds Online\Ultima Online Third Dawn\1.0",
                @"EA GAMES\Ultima Online Samurai Empire", 
                @"EA Games\Ultima Online: Mondain's Legacy", 
                @"EA GAMES\Ultima Online Samurai Empire\1.0", 
                @"EA GAMES\Ultima Online Samurai Empire\1.00.0000", 
                @"EA GAMES\Ultima Online: Samurai Empire\1.0", 
                @"EA GAMES\Ultima Online: Samurai Empire\1.00.0000", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.0", 
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", 
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\2d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\3d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire\2d\1.0", 
                @"Origin Worlds Online\Ultima Online Samurai Empire\3d\1.0"
            };

        static readonly string[] knownDirectories = new string[] { 
                @"C:\Program Files\Ultima Online\", 
                @"C:\Program Files\Ultima Online Third Dawn\", 
                @"C:\Program Files\Ultima Online Samurai Empire\", 
                @"C:\Program Files\Ultima Online Mondains Legacy\", 
                @"C:\Program Files\UO\",
                @"C:\UO\"
            };
    }
}
