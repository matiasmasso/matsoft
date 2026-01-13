using Ionic.Zip;
using MatHelperStd;
using Org.BouncyCastle.Utilities.Zlib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatHelperCFwk
{
    public class ZipHelper
    {
        public static List<string> Filenames(byte[] oByteArray)
        {
            List<string> retval = new List<string>();
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
            ZipInputStream oZipStream = new ZipInputStream(oStream);

            ZipEntry e = oZipStream.GetNextEntry();
            while (e != null)
            {
                string sFilename = e.FileName;
                retval.Add(sFilename);
                e = oZipStream.GetNextEntry();
            }
            return retval;
        }

        public static List<File> Extract(byte[] oByteArray)
        {
            List<File> retval = new List<File>();
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
            Ionic.Zip.ZipInputStream oZipStream = new Ionic.Zip.ZipInputStream(oStream);

            using (ZipFile zip1 = ZipFile.Read(oStream))
            {
                foreach (ZipEntry e in zip1)
                {
                    using (System.IO.MemoryStream snar = new System.IO.MemoryStream())
                    {
                        e.Extract(snar);
                        snar.Seek(0, System.IO.SeekOrigin.Begin);
                        byte[] oBuffer = FileSystemHelper.GetByteArrayFromStream(snar);
                        var oItem = File.Factory(e.FileName, oBuffer);
                        // Dim exs As New List(Of Exception)
                        // Dim oItem = FEBL.DocFile.Factory(exs, oBuffer)
                        // oItem.Filename = e.FileName
                        retval.Add(oItem);
                    }
                }
            }
            return retval;
        }

        public static System.IO.Stream Zip(List<ZipHelper.File> items, List<Exception> exs)
        {
            System.IO.MemoryStream retval = new System.IO.MemoryStream();
            try
            {
                using (ZipFile zip1 = new ZipFile())
                {
                    foreach (var item in items)
                        zip1.AddEntry(item.Filename, item.ByteArray);
                    zip1.Save(retval);
                }
            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }
            return retval;
        }


        public class File
        {
            public string Filename { get; set; }
            public byte[] ByteArray { get; set; }

            public static File Factory(string sFilename, byte[] oByteArray)
            {
                File retval = new File();
                retval.Filename = sFilename;
                retval.ByteArray = oByteArray;
                return retval;
            }
        }
    }
}
