using System;
using System.IO;
using System.Runtime.InteropServices;
using ConnectUO.Framework;
using ConnectUO.Framework.Diagnostics;
using ConnectUO.Framework.Data;
using ConnectUO.Framework.Web;
using ConnectUO.Framework.Services;
using Ninject;
using ConnectUO.Framework.IO;

namespace ConnectUO.Framework.Ultima
{
    public class ClientLauncher
    {
        [DllImport("Loader.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling=true, PreserveSig=true)]
		private static unsafe extern UInt32 Load(string clientPath, string dllPath, 
            string funcName, byte* ptr, Int32 dataSize, out UInt32 pid_ptr);

        public static unsafe LaunchStatus Launch(IKernel kernel, int id, string hostAddress, int port, string clientPath,
            string razorPath, bool useRazor, bool removeEncryption, string serverDirectory, string[] patchfiles)
        {
            Tracer.Verbose("Launching Server [ host: {0} port: {1} client: {2} razor: {3} useRazor: {4} removeEncryption: {5} serverDirectory: {6} patchCount: {7} ]",
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
                Tracer.Verbose("Client launch successful.");

                if (pid != 0)
                {
                    IStorageService dataService = kernel.Get<IStorageService>();
                    
                    try
                    {
                        dataService.LogProcess(id, pid);
                    }
                    catch { } //If it doesnt work, we really dont care to much...

                    ISettingsService settingsService = kernel.Get<ISettingsService>();
                    IApplicationService applicationService = kernel.Get<IApplicationService>();

                    if (settingsService.MinimizeOnPlay && applicationService.MainForm != null)
                    {
                        applicationService.MainForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                    }

                    if (!string.IsNullOrEmpty(razorPath) && useRazor)
                    {
                        string opts;
                        if (removeEncryption)
                            opts = "--clientenc";
                        else
                            opts = "--clientenc --serverenc";

                        Tracer.Verbose("Attaching Razor [pid={0}] [args={1}]", pid, opts);

                        System.Diagnostics.Process.Start(razorPath, String.Format("{0} --pid {1}", opts, pid));
                    }
                }
            }

            return err;
        }

        private static string BuildOverrideList(string hostAddress, int port, 
            string serverDirectory, string[] patchfiles)
        {
            string overrideFile = Path.Combine(serverDirectory, "fileoverrides.cuo");  
            string loginFile = CreateLogin(serverDirectory, hostAddress, port);

            Tracer.Verbose("Override File [{0}]", overrideFile);
            Tracer.Verbose("Login.cfg [{0}]", loginFile);

            using (StreamWriter sw = new StreamWriter(overrideFile, false))
            {
                sw.WriteLine(String.Format("{0}={1}", "login.cfg", loginFile));

                Tracer.Verbose("Override Entry {0}", String.Format("{0}={1}", "login.cfg", loginFile));

                if (patchfiles != null && patchfiles.Length > 0)
                {
                    for (int i = 0; i < patchfiles.Length; i++)
                    {
                        string fileName = Path.GetFileName(patchfiles[i]);
                        sw.WriteLine(String.Format("{0}={1}", fileName, patchfiles[i]));

                        Tracer.Verbose("Override Entry {0}", String.Format("{0}={1}", fileName, patchfiles[i]));
                    }
                }

                sw.Close();
            }

            return overrideFile;
        }

        private static string CreateLogin(string path, string hostAddress, int port)
        {
            FileSystemHelper.EnsureDirectoryExists(path);

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
