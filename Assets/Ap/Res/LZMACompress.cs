using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SevenZip.Compression;

namespace Ap.Res
{
    /// <summary>
    /// LZMA 压缩
    /// </summary>
    public class LZMACompress : CompressBase
    {
        /// <summary>
        /// 压缩信息
        /// </summary>
        public struct CompressInfo
        {
            public StringBuilder FileNames;
            public List<string> Files;

        }

        public virtual void Compress(string from, string to)
        {
            if (OnCompress != null)
            {
                OnCompress(this, 0, "");
            }
            using (FileStream fromStream = File.Open(from, FileMode.Open))
            {
                using (FileStream toStream = File.Open(to, FileMode.OpenOrCreate))
                {
                    SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();
                    coder.WriteCoderProperties(toStream);
                    toStream.Write(BitConverter.GetBytes(fromStream.Length), 0, 8);
                    coder.Code(fromStream, toStream, fromStream.Length, -1, null);
                    toStream.Flush();
                }
            }

            if (OnCompress != null)
            {
                OnCompress(this, 100, "");
            }

        }

        public virtual void Decompress(string from, string to)
        {
            if (OnDecompress != null)
            {
                OnDecompress(this, 0, "");
            }
            using (FileStream fromStream = File.Open(from, FileMode.Open))
            {
                byte[] properties = new byte[5];
                fromStream.Read(properties, 0, 5);
                using (FileStream toStream = File.Open(to, FileMode.OpenOrCreate))
                {
                    SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
                    byte[] fileLengthBytes = new byte[8];
                    fromStream.Read(fileLengthBytes, 0, 8);
                    long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);
                    coder.SetDecoderProperties(properties);
                    coder.Code(fromStream, toStream, fromStream.Length, fileLength, null);
                    toStream.Flush();
                }
            }

            if (OnDecompress != null)
            {
                OnDecompress(this, 100, "");
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void CompressFolder(string from, string to)
        {
            if (OnCompress != null)
            {
                OnCompress(this, 0, "");
            }
            CompressInfo info = new CompressInfo();
            info.FileNames = new StringBuilder();
            info.Files = new List<string>();
            GetCompressInfo(from, "", ref info);

            using (FileStream fs = File.Open(to, FileMode.CreateNew))
            {
                // 先写入文件信息
                byte[] bytes = Encoding.UTF8.GetBytes(info.FileNames.ToString());
                fs.Write(BitConverter.GetBytes(bytes.Length), 0, 4);
                fs.Write(bytes, 0, bytes.Length);
                // 写入文件大小

                for (int i = 0; i < info.Files.Count; i++)
                {
                    CompressWithStream(info.Files[i], fs);
                }
            }
            if (OnCompress != null)
            {
                OnCompress(this, 100, "");
            }
        }
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void DecompressFolder(string from, string to)
        {
            if (OnDecompress != null)
            {
                OnDecompress(this, 0, "");
            }
            using (FileStream fs = File.Open(from, FileMode.Open))
            {
                CompressInfo info = new CompressInfo();
                info.FileNames = new StringBuilder();
                info.Files = new List<string>();
                byte[] bytes = new byte[4];

                fs.Read(bytes, 0, 4);
                int size = BitConverter.ToInt32(bytes, 0);
                bytes = new byte[size];

                fs.Read(bytes, 0, size);

                string fileNames = Encoding.UTF8.GetString(bytes);
                string[] splits = fileNames.Split(';');

                for (int i = 0; i < splits.Length - 1; i++)
                {
                    string path = to + "/" + splits[i];
                    DecompressWithStream(fs, path);
                }

            }
            if (OnDecompress != null)
            {
                OnDecompress(this, 100, "");
            }
        }
        /// <summary>
        /// 获取压缩信息
        /// </summary>
        /// <param name="path"></param>
        /// <param name="relative"></param>
        /// <param name="info"></param>
        protected void GetCompressInfo(string path, string relative, ref CompressInfo info)
        {
            foreach (string file in System.IO.Directory.GetFiles(path))
            {
                string fileName = file.Replace("\\", "/");
                fileName = fileName.Substring(fileName.LastIndexOf("/"));
                info.FileNames.Append(relative + fileName + ";");
                info.Files.Add(file.Replace("\\", "/"));
            }

            foreach (string dir in System.IO.Directory.GetDirectories(path))
            {
                string dirName = dir.Replace("\\", "/");
                dirName = dirName.Substring(dirName.LastIndexOf("/"));
                GetCompressInfo(dir, relative + "/" + dirName + "/", ref info);
            }
        }
        /// <summary>
        /// 根据输出流压缩
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        protected void CompressWithStream(string from, Stream to)
        {
            using (FileStream input = File.Open(from, FileMode.Open))
            {

                SevenZip.Compression.LZMA.Encoder coder = new SevenZip.Compression.LZMA.Encoder();

                // Write the encoder properties
                coder.WriteCoderProperties(to);

                // Write the decompressed file size.
                to.Write(BitConverter.GetBytes(input.Length), 0, 8);

                // Encode the file.
                coder.Code(input, to, input.Length, -1, null);
            }
        }
        /// <summary>
        /// 通过输入流解压
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="to"></param>
        protected void DecompressWithStream(Stream stream, string to)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            string path = to.Substring(0, to.LastIndexOf("/"));
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            using (FileStream output = new FileStream(to, FileMode.Create))
            {
                // Read the decoder properties
                byte[] properties = new byte[5];
                stream.Read(properties, 0, 5);

                // Read in the decompress file size.
                byte[] fileLengthBytes = new byte[8];
                stream.Read(fileLengthBytes, 0, 8);
                long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

                // Decompress the file.
                coder.SetDecoderProperties(properties);
                coder.Code(stream, output, stream.Length, fileLength, null);
            }

        }
    }
}
