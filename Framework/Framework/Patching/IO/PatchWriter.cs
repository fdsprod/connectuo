using System;
using System.IO;

namespace ConnectUO.Framework.Patching
{
    public class PatchWriter : BinaryWriter
    {
        public static void CreateMUO(string path, Patch[] patches)
        {
            PatchWriter writer = new PatchWriter(File.Create(path), PatchFileType.MUO);

            writer.WriteMUOHeader();
            writer.WriteMUOMetaData(new string[] { "MUO", "Created with ConnectUO", "Jeff Boulanger" });
            writer.Write((int)patches.Length);

            for (int i = 0; i < patches.Length; i++)
                writer.WriteMUOPatch(patches[i]);

            writer.Close();
        }
        public static void CreateUOP(string path, Patch[] patches)
        {
            PatchWriter writer = new PatchWriter(File.Create(path), PatchFileType.UOP);

            writer.WriteUOPHeader();
            writer.Write(patches.Length);
            writer.Write((int)0);//Unknown

            for (int i = 0; i < patches.Length; i++)
                writer.WriteUOPPatch(patches[i]);

            writer.Close();
        }

        private PatchFileType patchFileType;

        public PatchWriter(Stream stream, PatchFileType patchFileType)
            : base(stream)
        {
            if (patchFileType == PatchFileType.Verdata)
                throw new Exception("This file format is not supported");

            this.patchFileType = patchFileType;
        }

        public void WriteMUOMetaData(string[] metaData)
        {
            for (int i = 0; i < metaData.Length; i++)
            {
                char[] chars = metaData[i].ToCharArray();

                for (int c = 0; c < chars.Length; c++)
                    Write(chars[c]);

                Write((byte)0);
            }
        }

        public void WriteMUOHeader()
        {
            Write(PatchReader.MUOHeader);
        }

        public void WriteUOPHeader()
        {
            Write(PatchReader.UOPHeader);
        }

        public void WritePatch(Patch patch)
        {
            switch (patchFileType)
            {
                case PatchFileType.MUO:
                    {
                        WriteMUOPatch(patch);
                        break;
                    }
                case PatchFileType.UOP:
                    {
                        WriteUOPPatch(patch);
                        break;
                    }
            }
        }

        public void WriteMUOPatch(Patch patch)
        {
            Write((int)patch.FileId);
            Write((int)patch.BlockID);
            Write((int)patch.Extra);
            Write((int)patch.Length);
            Write(patch.Data);
        }

        public void WriteUOPPatch(Patch patch)
        {
            Write((byte)patch.FileId);
            Write((int)patch.BlockID);
            Write((int)patch.Length);
            Write((int)patch.Extra);
            Write(patch.Data);
        }
    }
}
