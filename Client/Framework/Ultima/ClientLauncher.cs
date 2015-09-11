using System;
using System.IO;
using System.Runtime.InteropServices;
//using ConnectUO.Config;
using Fds.Core;
using Fds.Core.Diagnostics;
using ConnectUO.Forms;

namespace ConnectUO.Framework.Ultima
{
    public class ClientLauncher
    {
        static DebugLog _log = new DebugLog("ClientLauncher");

        static ClientLauncher()
        {
            _log = new DebugLog("ClientLauncher");
            //_log.Level = Program.ApplicationInfo.LogLevel;
        }

        [DllImport("Loader.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling=true, PreserveSig=true)]
		private static unsafe extern UInt32 Load(string clientPath, string dllPath, 
            string funcName, byte* ptr, Int32 dataSize, out UInt32 pid_ptr);

        public static unsafe LaunchStatus Launch(int id, string hostAddress, int port, string clientPath,
            string razorPath, bool useRazor, bool removeEncryption, string serverDirectory, string[] patchfiles)
        {
            _log.Debug("Launching Server [ host: {0} port: {1} client: {2} razor: {3} useRazor: {4} removeEncryption: {5} serverDirectory: {6} patchCount: {7} ]",
                hostAddress, port, clientPath, razorPath, useRazor, removeEncryption, serverDirectory, patchfiles == null ? 0 : patchfiles.Length);

            UInt32 pid;
            LaunchStatus err;

            string encPath = Path.Combine(Directory.GetCurrentDirectory(), "EncPatcher.dll");
            string overrideFile = BuildOverrideList(hostAddress, port, serverDirectory, patchfiles);

            if (!File.Exists(clientPath))
            {
                throw new FileNotFoundException(clientPath);
            }

            byte[] param = new byte[257];

            MemoryStream memStream = new MemoryStream(param);
            BinaryWriter writer = new BinaryWriter(memStream);

            if (useRazor && !string.IsNullOrEmpty(razorPath) && File.Exists(razorPath))
                writer.Write((byte)0); // when razor is enabled it will remove the encryption for us
            else
                writer.Write((byte)Convert.ToByte(removeEncryption));

            writer.Write(overrideFile.ToCharArray());
            writer.Close();

            fixed (byte* para_ptr = param)
            {
                err = (LaunchStatus)Load(clientPath, encPath, "Attach", para_ptr, param.Length, out pid);
            }

            if (err == LaunchStatus.SUCCESS)
            {
                _log.Debug("Client launch successful.");

                if (pid != 0)
                {
                    Program.Database.LogProcess(id, pid);

                    if (Program.Database.MinimizeOnPlay && Program.MainForm != null)
                    {
                        Program.MainForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    }

                    if (!string.IsNullOrEmpty(razorPath) && useRazor)
                    {
                        string opts;
                        if (removeEncryption)
                            opts = "--clientenc";
                        else
                            opts = "--clientenc --serverenc";

                        _log.Debug("Attaching Razor [pid={0}] [args={1}]", pid, opts);

                        System.Diagnostics.Process.Start(razorPath, String.Format("{0} --pid {1}", opts, pid));
                    }
                }
            }

            return err;
        }

        //public static bool Launch(Shard shard)
        //{
        //    if (string.IsNullOrEmpty(Program.Database.UltimaOnlineDirectory))
        //    {
        //        MessageBoxEx.Show(Program.MainForm, "ConnectUO was unable to find the directory that Ultima Online is installed to.  This must be set in order to launch the client.");
        //        return false;
        //    }

        //    if (string.IsNullOrEmpty(Program.Database.UltimaOnlineExe))
        //    {
        //        MessageBoxEx.Show(Program.MainForm, "ConnectUO was unable to find the client executable.  This must be set in order to launch the client.");
        //        return false;
        //    }

        //    if (string.IsNullOrEmpty(Program.Database.RazorDirectory))
        //    {
        //        MessageBoxEx.Show(Program.MainForm, "ConnectUO was unable to find the razor executable. Razor must be installed in order to play servers through ConnectUO.");
        //        return false;
        //    }

        //    string folderName = shard.Name;
        //    string hostAddress = shard.HostAddress;
        //    int port = shard.Port;
        //    bool removeEnc = shard.RemoveEncryption;
        //    bool allowRazor = ((shard is PublicShard) ? ((PublicShard)shard).AllowRazor : true) & Program.Database.LaunchRazor;
        //    string[] patchFiles = null;

        //    if (shard.HasPatches)
        //    {
        //        //patchFiles = GetPatches(Path.Combine(Program.Database.PatchDirectory, folderName));
        //    }

        //    Utility.EnsureValidFolderName(ref folderName);

        //    ClientLauncher.Launch(hostAddress, port,
        //        Path.Combine(Program.Database.UltimaOnlineDirectory, Program.Database.UltimaOnlineExe),
        //        Program.Database.RazorDirectory, allowRazor, removeEnc,
        //        Path.Combine(Program.Database.PatchDirectory, folderName), patchFiles);

        //    return true;
        //}

        //private static string[] GetPatches(string serverDirectory)
        //{
            //Utility.EnsureDirectory(serverDirectory);

            //try
            //{

            //}
            //catch (Exception e)
            //{

            //}
            //finally
            //{

            //}

            //bool requiresPatchReset = false;

            //List<ShardPatch> appliedPatches = GetAppliedPatches(serverDirectory);
            
            //for (int i = 0; i < appliedPatches.Count && !requiresPatchReset; i++)
            //{
            //    bool found = false;

            //    //for (int j = 0; j < shardPatches.Count && !found; j++)
            //    //{
            //    //    found = shardPatches[j] == appliedPatches[i];
            //    //}

            //    requiresPatchReset = !found;
            //}


        //    List<string> patchedFiles = new List<string>();
        //}

        //private static List<ShardPatch> GetAppliedPatches(string serverDirectory)
        //{
        //    List<ShardPatch> patches = new List<ShardPatch>();
        //    string patchChk = Path.Combine(serverDirectory, "patch.chk");

        //    if (File.Exists(patchChk))
        //    {
        //        StreamReader reader = new StreamReader(patchChk);

        //        while (!reader.EndOfStream)
        //        {
        //            string applied = reader.ReadLine();
        //            string[] split = applied.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

        //            if (split.Length == 2)
        //            {
        //                ShardPatch patch = new ShardPatch();
        //                patch.PatchUrl = split[0];

        //                int version;

        //                if (int.TryParse(split[1], out version))
        //                {
        //                    patch.Version = version;
        //                    patches.Add(patch);
        //                }
        //            }
        //        }
        //    }

        //    return patches;
        //}

        private static string BuildOverrideList(string hostAddress, int port, 
            string serverDirectory, string[] patchfiles)
        {
            string overrideFile = Path.Combine(serverDirectory, "fileoverrides.cuo");  
            string loginFile = CreateLogin(serverDirectory, hostAddress, port);

            _log.Debug("Override File [{0}]", overrideFile);
            _log.Debug("Login.cfg [{0}]", loginFile);

            using (StreamWriter sw = new StreamWriter(overrideFile, false))
            {
                sw.WriteLine(String.Format("{0}={1}", "login.cfg", loginFile));

                _log.Debug("Override Entry {0}", String.Format("{0}={1}", "login.cfg", loginFile));

                if (patchfiles != null && patchfiles.Length > 0)
                {
                    for (int i = 0; i < patchfiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(patchfiles[i]);
                        sw.WriteLine(String.Format("{0}={1}", fileName, patchfiles[i]));

                        _log.Debug("Override Entry {0}", String.Format("{0}={1}", fileName, patchfiles[i]));
                    }
                }

                sw.Close();
            }

            return overrideFile;
        }

        private static string CreateLogin(string path, string hostAddress, int port)
        {
            Utility.EnsureDirectory(path);

            string file = Path.Combine(path, "login.cfg");

            StreamWriter writer = null;

            try
            {
                writer = new StreamWriter(file);

                writer.WriteLine(";Editing this file will cause issues with login and support");
                writer.WriteLine(";ConnectUO generated entry");
                writer.WriteLine("LoginServer={0},{1}", hostAddress, port);
            }
            catch
            {
                throw;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }

            return file;
        }
    }
}
