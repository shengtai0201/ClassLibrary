using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public class Zip
    {
        public static ICollection<string> Compress(byte[] array, int count)
        {
            ICollection<string> result = new List<string>();
            ICollection<byte[]> byteContents = new List<byte[]>();

            // 拆解
            int length = array.Length;
            int offset = 0;
            while (offset < length)
            {
                var segment = new ArraySegment<byte>(array, offset, (length - offset) < count ? length - offset : count);
                byteContents.Add(segment.ToArray());
                offset += count;
            }

            // 壓縮
            foreach(var byteContent in byteContents)
            {
                byte[] outputStreamBytes = null;
                using (ZipFile zip = new ZipFile { CompressionLevel = Ionic.Zlib.CompressionLevel.BestCompression })
                {
                    zip.AddEntry("entryName", byteContent);

                    MemoryStream outputStream = new MemoryStream();
                    zip.Save(outputStream);

                    outputStreamBytes = outputStream.ToArray();
                }

                if (outputStreamBytes == null)
                    throw new Exception();

                result.Add(Convert.ToBase64String(outputStreamBytes));
            }

            return result;
        }

        public static byte[] Decompress(ICollection<string> base64Strings)
        {
            // 解壓縮
            IList<byte[]> buffers = new List<byte[]>();
            foreach (var s in base64Strings)
            {
                var buffer = Convert.FromBase64String(s);
                MemoryStream zipStream = new MemoryStream(buffer);

                byte[] streamBytes = null;
                using (ZipFile zip = ZipFile.Read(zipStream))
                {
                    var zipEntry = zip.FirstOrDefault();

                    MemoryStream stream = new MemoryStream();
                    zipEntry.Extract(stream);

                    streamBytes = stream.ToArray();
                }

                if (streamBytes == null)
                    throw new Exception();

                buffers.Add(streamBytes);
            }

            // 合併
            byte[] bytes = buffers[0];
            for (int i = 1; i < buffers.Count; i++)
            {
                byte[] destinationArray = new byte[bytes.Length + buffers[i].Length];

                Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
                Array.Copy(buffers[i], 0, destinationArray, bytes.Length, buffers[i].Length);

                bytes = destinationArray;
            }

            return bytes;
        }

        public static byte[] Decompress(ICollection<byte[]> buffers)
        {
            // 解壓縮
            IList<byte[]> innerBuffers = new List<byte[]>();
            foreach (var buffer in buffers)
            {
                MemoryStream zipStream = new MemoryStream(buffer);

                byte[] streamBytes = null;
                using (ZipFile zip = ZipFile.Read(zipStream))
                {
                    var zipEntry = zip.FirstOrDefault();

                    MemoryStream stream = new MemoryStream();
                    zipEntry.Extract(stream);

                    streamBytes = stream.ToArray();
                }

                if (streamBytes == null)
                    throw new Exception();

                innerBuffers.Add(streamBytes);
            }

            // 合併
            byte[] bytes = innerBuffers[0];
            for (int i = 1; i < innerBuffers.Count; i++)
            {
                byte[] destinationArray = new byte[bytes.Length + innerBuffers[i].Length];

                Array.Copy(bytes, 0, destinationArray, 0, bytes.Length);
                Array.Copy(innerBuffers[i], 0, destinationArray, bytes.Length, innerBuffers[i].Length);

                bytes = destinationArray;
            }

            return bytes;
        }
    }
}
