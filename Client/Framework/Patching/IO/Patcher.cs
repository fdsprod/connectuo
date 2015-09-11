using System;
using System.Collections.Generic;
using System.IO;
//using ConnectUO.Config;
using CUODesktop.PatchLib;
using Fds.Core;

namespace ConnectUO.Framework.Patching
{
	public class PatcherProgressChangeEventArgs : EventArgs
	{
		private int percentComplete;

        public int PercentComplete { get { return percentComplete; } }

        public PatcherProgressChangeEventArgs(int percentComplete)
			: base()
		{
            this.percentComplete = percentComplete;
		}
	}

	public class Patcher
	{
		private Patch[] patches;
		private string serverFolder;
        //private Thread thread;

        public string PatchToFolder { get { return serverFolder; } }

        public event EventHandler<PatcherProgressChangeEventArgs> ProgressChanged;
        public event EventHandler<EventArgs> Complete;

		public Patcher(Patch[] patches, string serverFolder)
		{
            if (string.IsNullOrEmpty(Program.Database.UltimaOnlineDirectory))
                throw new Exception("ConnectUO was unable to find the directory that Ultima Online is installed to.");

            Utility.EnsureDirectory(serverFolder);

            this.patches = patches;
			this.serverFolder = serverFolder;
            //this.thread = new Thread(InternalPatchMuls);
        }

        private void OnProgressChanged(object sender, PatcherProgressChangeEventArgs e)
        {
            if (ProgressChanged != null)
                ProgressChanged(sender, e);
        }
        private void OnCompleteChanged(object sender, EventArgs e)
        {
            if (Complete != null)
                Complete(sender, e);
        }

		private void InternalPatchMuls()
		{
			OnProgressChanged(this, new PatcherProgressChangeEventArgs(0));

			Dictionary<int, List<Patch>> typedPatchTable = CreateTable();
			List<int> keys = new List<int>(typedPatchTable.Keys);

			for (int i = 0; i < keys.Count; i++)
			{
				int key = keys[i];
				List<Patch> patches = typedPatchTable[keys[i]];

				patches.Sort(new Comparison<Patch>(CompareTo));
                string id = Enum.GetName(typeof(FileId), (FileId)key);

                if (id == null && (id = Enum.GetName(typeof(ExtendedFileId), (ExtendedFileId)key)) == null)
					continue;

				switch (key)
				{
					case (int)FileId.Anim_mul:
							PatchAnim(patches);
							break;
					case (int)FileId.Art_mul:
							PatchArt(patches);
							break;
					case (int)FileId.AnimData_mul:
							PatchAnimData(patches);
							break;
					case (int)FileId.GumpArt_mul:
							PatchGump(patches);
							break;
					case (int)FileId.Hues_mul:
							PatchHues(patches);
							break;
					case (int)FileId.Map0_mul:
							PatchMap0(patches);
							break;
					case (int)FileId.Multi_mul:
							PatchMultis(patches);
							break;
					case (int)FileId.Skills_mul:
							PatchSkills(patches);
							break;
					case (int)FileId.Sound_mul:
							PatchSounds(patches);
							break;
					case (int)FileId.Statics0_mul:
							PatchStatics(patches);
							break;
					case (int)FileId.TexMaps_mul:
							PatchTextures(patches);
							break;
					case (int)FileId.TileData_mul:
							PatchTileData(patches);
							break;
					case (int)ExtendedFileId.Anim_mul:
							PatchAnim(patches);
							break;
					case (int)ExtendedFileId.Art_mul:
							PatchArt(patches);
							break;
					case (int)ExtendedFileId.GumpArt_mul:
							PatchGump(patches);
							break;
					case (int)ExtendedFileId.Map0_mul:
							PatchMap0(patches);
							break;
					case (int)ExtendedFileId.Multi_mul:
							PatchMultis(patches);
							break;
					case (int)ExtendedFileId.Sound_mul:
							PatchSounds(patches);
							break;
					case (int)ExtendedFileId.Statics0_mul:
                            PatchStatics(patches);
							break;
					case (int)ExtendedFileId.TexMaps_mul:
							PatchTextures(patches);
							break;
                    default: throw new Exception(String.Format("Unable to identify Patch Id {0:X2}", key));
				}
			}

            OnProgressChanged(this, new PatcherProgressChangeEventArgs(100));
            OnCompleteChanged(this, EventArgs.Empty);

            //thread.Join();
		}

		public void WritePatches()
		{
            //if (thread.ThreadState != ThreadState.Running || thread.ThreadState != ThreadState.Background)
            //    thread.Start();

            InternalPatchMuls();
		}
		private string GetPath(string file)
		{
			if (File.Exists(Path.Combine(serverFolder, file)))
				return serverFolder;
			else
                return Program.Database.UltimaOnlineDirectory;
        }
        private int CompareTo(Patch one, Patch two)
        {
            return one.BlockID.CompareTo(two.BlockID);
        }
        private Dictionary<int, List<Patch>> CreateTable()
        {
            Dictionary<int, List<Patch>> typedPatchTable = new Dictionary<int, List<Patch>>();

            for (int i = 0; i < patches.Length; i++)
            {
                if (!typedPatchTable.ContainsKey(patches[i].FileId))
                    typedPatchTable.Add(patches[i].FileId, new List<Patch>());

                typedPatchTable[patches[i].FileId].Add(patches[i]);
            }

            return typedPatchTable;
        }

        private void PatchFile(string idxPath, string mulPath, string newIdxPath, string newMulPath, List<Patch> patches)
        {
            File.Copy(idxPath, newIdxPath, true);
            File.Copy(mulPath, newMulPath, true);

            FileInfo idxFileInfo = new FileInfo(newIdxPath);
            FileIndex index = new FileIndex(Path.GetFileName(idxPath), Path.GetFileName(mulPath), (int)(idxFileInfo.Length / 12));

            BinaryWriter idx = new BinaryWriter(new FileStream(newIdxPath, FileMode.Open));
            BinaryWriter mul = new BinaryWriter(new FileStream(newMulPath, FileMode.Open));

            int oldPercent = 0;
            for (int p = 0; p < patches.Count; p++)
            {
                Patch patch = patches[p];
                //int a = 0;
                //if (patch.BlockID == 0xEEEE)
                //    a = 4;
                /*
                int pos;

                if (index[patch.BlockID].length > patch.Length)
                    pos = index[patch.BlockID].lookup;		
                else
                */
                int pos = Convert.ToInt32(mul.BaseStream.Length);

                idx.Seek(patch.BlockID * 12, SeekOrigin.Begin);

                idx.Write(pos);
                idx.Write(patch.Length);
                idx.Write(patch.Extra);

                if (patch.Length >= 0)
                {
                    mul.Seek(pos, SeekOrigin.Begin);
                    mul.Write(patch.Data, 0, patch.Length);
                }

                int percent = (p * 100) / patches.Count;

                if (percent != oldPercent)
                {
                    OnProgressChanged(this, new PatcherProgressChangeEventArgs(percent));
                    oldPercent = percent;
                }
            }

            index.Close();

            if (idx != null)
                idx.Close();
            if (mul != null)
                mul.Close();

            if (File.Exists(newIdxPath.Substring(0, newIdxPath.Length - 4)))
                File.Delete(newIdxPath.Substring(0, newIdxPath.Length - 4));

            File.Move(newIdxPath, newIdxPath.Substring(0, newIdxPath.Length - 4));

            if (File.Exists(newMulPath.Substring(0, newMulPath.Length - 4)))
                File.Delete(newMulPath.Substring(0, newMulPath.Length - 4));

            File.Move(newMulPath, newMulPath.Substring(0, newMulPath.Length - 4));

            File.Delete(newIdxPath);
            File.Delete(newMulPath);
        }
		private void PatchTileData(List<Patch> patches)
		{
			string tiledatamul = Path.Combine(GetPath("tiledata.mul"), "tiledata.mul");
			string output = Path.Combine(serverFolder, "tiledata.mul.tmp");

			File.Copy(tiledatamul, output, true);
			BinaryWriter mul = new BinaryWriter(new FileStream(output, FileMode.Open));

			for (int p = 0; p < patches.Count; p++)
			{
				Patch patch = patches[p];
				if (patch.BlockID < 512)//1st 512 entries are Land Blocks
				{
					//each land block is 836 bytes
					mul.Seek(patch.BlockID * 836, SeekOrigin.Begin);
					mul.Write(patch.Data, 0, patch.Length);
				}
				else//static block
				{
					int offset = 428032;//offset in bytes of land blocks
					int index = patch.BlockID - 512;//index past land block offset
					int seekTo = offset + (1184 * index);//static block is 1184 bytes

					mul.Seek(seekTo, SeekOrigin.Begin);
					mul.Write(patch.Data, 0, patch.Length);
				}
			}

			mul.Close();

			File.Copy(output, output.Substring(0, output.Length - 4), true);
		}
		private void PatchAnimData(List<Patch> patches)
		{
			string animdatamul = Path.Combine(GetPath("animdata.mul"), "animdata.mul");
			string output = Path.Combine(serverFolder, "animdata.mul.tmp");

			File.Copy(animdatamul, output, true);
			BinaryWriter mul = new BinaryWriter(new FileStream(output, FileMode.Open));

			for (int p = 0; p < patches.Count; p++)
			{
				Patch patch = patches[p];
				mul.Seek(patch.BlockID * 548, SeekOrigin.Begin);
				mul.Write(patch.Data, 0, patch.Length);
			}

			mul.Close();

			File.Copy(output, output.Substring(0, output.Length - 4), true);
		}
		private void PatchMap0(List<Patch> patches)
		{
			string mapmul = Path.Combine(GetPath("map0.mul"), "map0.mul");
			string output = Path.Combine(serverFolder, "map0.mul.tmp");

			File.Copy(mapmul, output, true);
			BinaryWriter mul = new BinaryWriter(new FileStream(output, FileMode.Open));

			for (int p = 0; p < patches.Count; p++)
			{
				Patch patch = patches[p];
				mul.Seek(patch.BlockID * 196, SeekOrigin.Begin);
				mul.Write(patch.Data, 0, patch.Length);
			}

			mul.Close();

			File.Copy(output, output.Substring(0, output.Length - 4), true);
		}
		private void PatchAnim(List<Patch> patches)
		{
			Dictionary<int, List<Patch>> animTable = new Dictionary<int, List<Patch>>();

			for (int i = 0; i < patches.Count; i++)
			{
				int id = patches[i].BlockID;
				int key = 1;

				if (!animTable.ContainsKey(key))
					animTable.Add(key, new List<Patch>());

				animTable[key].Add(patches[i]);
			}

			List<int> keys = new List<int>(animTable.Keys);

			for (int i = 0; i < keys.Count; i++)
			{
				string fileNumber = keys[i] == 1 ? "" : keys[i].ToString();

				PatchFile(Path.Combine(GetPath("Anim" + fileNumber + ".idx"), "Anim" + fileNumber + ".idx"), Path.Combine(GetPath("Anim" + fileNumber + ".mul"), "Anim" + fileNumber + ".mul"),
				Path.Combine(serverFolder, "Anim" + fileNumber + ".idx.tmp"), Path.Combine(serverFolder, "Anim" + fileNumber + ".mul.tmp"), animTable[keys[i]]);
			}
		}
		private void PatchHues(List<Patch> patches)
		{
			string huesmul = Path.Combine(GetPath("hues.mul"), "hues.mul");
			string output = Path.Combine(serverFolder, "hues.mul.tmp");

			File.Copy(huesmul, output, true);
			BinaryWriter mul = new BinaryWriter(new FileStream(output, FileMode.Open));

			for (int p = 0; p < patches.Count; p++)
			{
				Patch patch = patches[p];
				mul.Seek(patch.BlockID * 708, SeekOrigin.Begin);
				mul.Write(patch.Data, 0, patch.Length);
			}

			mul.Close();

			File.Copy(output, output.Substring(0, output.Length - 4), true);
		}
		private void PatchArt(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("Artidx.mul"), "Artidx.mul"), Path.Combine(GetPath("Art.mul"), "Art.mul"),
				Path.Combine(serverFolder, "Artidx.mul.tmp"), Path.Combine(serverFolder, "Art.mul.tmp"), patches);
		}
		private void PatchGump(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("Gumpidx.mul"), "Gumpidx.mul"), Path.Combine(GetPath("Gumpart.mul"), "Gumpart.mul"),
				Path.Combine(serverFolder, "Gumpidx.mul.tmp"), Path.Combine(serverFolder, "Gumpart.mul.tmp"), patches);
		}
		private void PatchMultis(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("Multi.idx"), "Multi.idx"), Path.Combine(GetPath("Multi.mul"), "Multi.mul"),
				Path.Combine(serverFolder, "Multi.idx.tmp"), Path.Combine(serverFolder, "Multi.mul.tmp"), patches);
		}
		private void PatchSkills(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("Skills.idx"), "Skills.idx"), Path.Combine(GetPath("skills.mul"), "skills.mul"),
				Path.Combine(serverFolder, "Skills.idx.tmp"), Path.Combine(serverFolder, "skills.mul.tmp"), patches);
		}
		private void PatchSounds(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("soundidx.mul"), "soundidx.mul"), Path.Combine(GetPath("sound.mul"), "sound.mul"),
				Path.Combine(serverFolder, "soundidx.mul.tmp"), Path.Combine(serverFolder, "sound.mul.tmp"), patches);
		}
		private void PatchStatics(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("StaIdx0.mul"), "StaIdx0.mul"), Path.Combine(GetPath("Statics0.mul"), "Statics0.mul"),
				Path.Combine(serverFolder, "StaIdx0.mul.tmp"), Path.Combine(serverFolder, "Statics0.mul.tmp"), patches);
		}
		private void PatchTextures(List<Patch> patches)
		{
			PatchFile(Path.Combine(GetPath("TexIdx.mul"), "TexIdx.mul"), Path.Combine(GetPath("TexMaps.mul"), "TexMaps.mul"),
				Path.Combine(serverFolder, "TexIdx.mul.tmp"), Path.Combine(serverFolder, "TexMaps.mul.tmp"), patches);
		}
	}
}