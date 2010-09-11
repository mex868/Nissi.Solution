using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Nissi.Util
{
    /// <summary>
    /// Classe para uso da dll SharpZipLib
    /// </summary>
    public class Zip
    {
        #region Construtor
        public Zip()
        {
        }
        #endregion

        #region Método de Compressão
        public static byte[] Compress(byte[] bytes)
        {
            MemoryStream memory = new MemoryStream();
            DeflaterOutputStream stream = new DeflaterOutputStream(memory, new Deflater(Deflater.BEST_COMPRESSION), 131072);
            stream.Write(bytes, 0, bytes.Length);
            stream.Close();
            return memory.ToArray();
        }
        #endregion

        #region Método de Descompressão
        public static byte[] Decompress(byte[] bytes)
        {
            InflaterInputStream stream = new InflaterInputStream(new MemoryStream(bytes));
            MemoryStream memory = new MemoryStream();
            Byte[] writeData = new byte[4096];
            int size;
            while (true)
            {
                size = stream.Read(writeData, 0, writeData.Length);
                if (size > 0)
                {
                    memory.Write(writeData, 0, size);
                }
                else
                {
                    break;
                }
            }
            stream.Close();
            return memory.ToArray();
        }
        #endregion
    }
}
