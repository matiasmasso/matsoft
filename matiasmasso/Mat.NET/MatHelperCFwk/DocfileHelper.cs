using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using MatHelperStd;
using Microsoft.VisualBasic;
using DTO;
using LegacyHelper;
using System.Resources;
using System.Security.Policy;
using System.Security.Cryptography;

namespace MatHelperCFwk
{


    public class DocfileHelper
    {
        public static byte[] ZipThumbnail(byte[] oByteArray)
        {
            byte[] retval = null;
            List<string> sFilenames = ZipHelper.Filenames(oByteArray);
            if (sFilenames.Count > 0)
            {
                var oFiles = ZipHelper.Extract(oByteArray);
                if (oFiles.Count > 0)
                {
                    List<Exception> exs = new List<Exception>();
                    var oFirstDocfile = DocfileHelper.Factory(exs, oFiles.First().ByteArray);
                    retval = oFirstDocfile.Thumbnail;
                }
            }
            return retval;
        }

        public static List<DTODocFile> Factory(List<byte[]> oFiles, List<Exception> exs)
        {
            List<DTODocFile> retval = new List<DTODocFile>();
            foreach (byte[] oFile in oFiles)
            {
                DTODocFile oDocFile = DocfileHelper.Factory(exs, oFile, MimeCods.NotSet);
                retval.Add(oDocFile);
            }
            return retval;
        }

        public static DTODocFile Factory(List<Exception> exs, byte[] oByteArray, MimeCods oMime = MimeCods.NotSet)
        {
            DTODocFile retval = new DTODocFile();
            LoadFromStream(exs, ref retval, oByteArray, oMime);
            return retval;
        }

        public static DTODocFile LoadFromString(string src)
        {
            var oByteArray = System.Text.Encoding.UTF8.GetBytes(src);
            var retval = new DTODocFile()
            {
                Hash = MatHelperCFwk.ASINHelper.GenerateRandomASIN(),
                Sha256 = CryptoHelper.Sha256(oByteArray),
                Thumbnail = TextThumbnail(src),
                FchCreated = DTO.GlobalVariables.Now(),
                Stream = oByteArray,
                Mime = MimeCods.Txt,
                Length = oByteArray.Length
            };
            return retval;
        }



        public static byte[] TextThumbnail(string src)
        {
            System.Drawing.Bitmap oThumbnail = new System.Drawing.Bitmap(DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT);
            Graphics oGraphics = Graphics.FromImage(oThumbnail);
            oGraphics.FillRectangle(Brushes.White, 0, 0, oThumbnail.Width, oThumbnail.Height);
            var oFont = new Font("Helvetica", 10, FontStyle.Regular);
            int padding = 10;
            System.Drawing.RectangleF oRectangleF = new System.Drawing.RectangleF(padding, padding, DTODocFile.THUMB_WIDTH - 2 * padding, DTODocFile.THUMB_HEIGHT - 2 * padding);
            StringFormat sf = new StringFormat();
            oGraphics.DrawString(src, oFont, Brushes.Black, oRectangleF, sf);
            byte[] retval = null;
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                oThumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                retval = ms.ToArray();
            }
            return retval;
        }

        public static DTODocFile Factory(string sFilename, List<Exception> exs)
        {
            DTODocFile retval = null/* TODO Change to default(_) if this is not a reference type */;
            byte[] oByteArray = null;
            if (FileSystemHelper.GetStreamFromFile(sFilename, ref oByteArray, ref exs))
            {
                var oMime = MimeHelper.GetMimeFromExtension(sFilename);
                retval = Factory(exs, oByteArray, oMime);
                retval.Filename = sFilename;
            }
            return retval;
        }

        public static bool LoadFromStream(byte[] oByteArray, ref DTODocFile oDocFile, List<Exception> exs, string sFilename = "")
        {
            bool retval = false;
            if (oByteArray == null)
                exs.Add(new Exception("ByteArray buit al generar el DocFile"));
            else
            {
                oDocFile = new DTODocFile();
                {
                    var withBlock = oDocFile;
                    withBlock.Hash = MatHelperCFwk.ASINHelper.GenerateRandomASIN();
                    withBlock.Sha256 = CryptoHelper.Sha256(oByteArray);
                    withBlock.Stream = oByteArray;
                    withBlock.Length = oByteArray.Length;
                    if (!string.IsNullOrEmpty(sFilename))
                    {
                        withBlock.Filename = sFilename;
                        withBlock.Mime = MimeHelper.GetMimeFromExtension(sFilename);
                    }
                    if (withBlock.Mime == MimeCods.NotSet)
                        withBlock.Mime = MimeHelper.GuessMime(oByteArray);
                    if (LoadMimeDetails(oDocFile, exs))
                        retval = true;
                }
            }
            return retval;
        }

        public static bool LoadMimeDetails(DTODocFile oDocFile, List<Exception> exs)
        {
            bool retval = true;
            {
                var withBlock = oDocFile;
                switch (withBlock.Mime)
                {
                    case MimeCods.Pdf:
                        {
                            retval = LoadPdf(ref oDocFile, exs);
                            break;
                        }

                    case MimeCods.Rtf:
                        {
                            break;
                        }

                    case MimeCods.Zip:
                        {
                            var oFiles = ZipHelper.Extract(withBlock.Stream);
                            if (oFiles.Count > 0)
                            {
                                var oFirstDocfile = DocfileHelper.Factory(exs, oFiles.First().ByteArray);
                                withBlock.Thumbnail = oFirstDocfile.Thumbnail;
                            }

                            break;
                        }

                    case MimeCods.Jpg:
                    case MimeCods.Gif:
                    case MimeCods.Png:
                    case MimeCods.Bmp:
                    case MimeCods.Tif:
                    case MimeCods.Tiff:
                        {
                            System.IO.MemoryStream ms = new System.IO.MemoryStream(withBlock.Stream);
                            var oImg = System.Drawing.Image.FromStream(ms);
                            withBlock.Size = new Size(oImg.Width, oImg.Height);
                            withBlock.VRes = (int)oImg.VerticalResolution;
                            withBlock.HRes = (int)oImg.HorizontalResolution;
                            System.Drawing.Image oThumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT);
                            using (System.IO.MemoryStream ms2 = new System.IO.MemoryStream())
                            {
                                oThumbnail.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                withBlock.Thumbnail = ms2.ToArray();
                            }

                            break;
                        }

                    case MimeCods.Ppt:
                    case MimeCods.Pptx:
                        {
                            break;
                        }

                    case MimeCods.Docx:
                        {
                            break;
                        }

                    case MimeCods.Txt:
                        {
                            string src = System.Text.ASCIIEncoding.ASCII.GetString(oDocFile.Stream);
                            var oThumbnail = ImageHelper.GetThumbnailFromText(src, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT);
                            withBlock.Thumbnail = oThumbnail.Bytes();
                            break;
                        }

                    case MimeCods.Mov:
                    case MimeCods.Mp4:
                    case MimeCods.Mpg:
                        {
                            Image oImg = new Bitmap(
    System.Reflection.Assembly.GetEntryAssembly().
      GetManifestResourceStream("MatHelperCFwk.Resources.video.jpg"));
                            withBlock.Thumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes();
                            break;
                        }

                    default:
                        {
                            Image oImg = new Bitmap(
    System.Reflection.Assembly.GetEntryAssembly().
      GetManifestResourceStream("MatHelperCFwk.Resources.cartadeajuste.png"));
                            withBlock.Thumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes();
                            break;
                        }
                }
            }
            return retval;
        }


        public static bool LoadFromStream(List<Exception> exs, ref DTODocFile oDocfile, byte[] oByteArray, MimeCods oMime = MimeCods.NotSet)
        {
            try
            {
                {
                    var withBlock = oDocfile;
                    withBlock.Hash = MatHelperCFwk.ASINHelper.GenerateRandomASIN();
                    withBlock.Sha256 = CryptoHelper.Sha256(oByteArray);
                    withBlock.FchCreated = DTO.GlobalVariables.Now();
                    withBlock.Stream = oByteArray;
                    if (oMime == MimeCods.NotSet)
                        withBlock.Mime = MimeHelper.GuessMime(oByteArray);
                    else
                        withBlock.Mime = oMime;
                    withBlock.Length = oByteArray.Length;

                    switch (withBlock.Mime)
                    {
                        case MimeCods.Pdf:
                            {
                                System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
                                var oPdf = GhostScriptHelper.Rasterize(exs, oStream);
                                withBlock.Thumbnail = oPdf.Thumbnail;
                                withBlock.Pags = oPdf.PageCount;
                                withBlock.Size = new Size(oPdf.Width, oPdf.Height);
                                break;
                            }

                        case MimeCods.Rtf:
                            {
                                break;
                            }

                        case MimeCods.Zip:
                            {
                                withBlock.Thumbnail = DocfileHelper.ZipThumbnail(withBlock.Stream);
                                break;
                            }

                        case MimeCods.Jpg:
                        case MimeCods.Gif:
                        case MimeCods.Png:
                        case MimeCods.Bmp:
                        case MimeCods.Tif:
                        case MimeCods.Tiff:
                            {
                                System.IO.MemoryStream ms = new System.IO.MemoryStream(withBlock.Stream);
                                var oImg = Image.FromStream(ms);
                                withBlock.Size = new Size(oImg.Width, oImg.Height);
                                withBlock.VRes = (int)oImg.VerticalResolution;
                                withBlock.HRes = (int)oImg.HorizontalResolution;
                                withBlock.Thumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes();
                                break;
                            }

                        case MimeCods.Ppt:
                        case MimeCods.Pptx:
                            {
                                break;
                            }

                        case MimeCods.Docx:
                            {
                                withBlock.Thumbnail = WordHelper.GetImgFromWordFirstPage(withBlock.Stream, exs);
                                break;
                            }

                        case MimeCods.Xlsx:
                            {
                                int iExcelRows = 0;
                                int iExcelCols = 0;
                                // .Thumbnail = MatHelper.Excel.GetImgFromExcelFirstPage(_DocFile.Stream, iExcelCols, iExcelRows)
                                // .Thumbnail = My.Resources.Excel_Big
                                withBlock.Thumbnail = MatHelper.Excel.Rasterizer.GetExcelThumbnail(withBlock.Stream, exs);
                                withBlock.Size = new Size(iExcelCols, iExcelRows);
                                break;
                            }

                        case MimeCods.Txt:
                            {
                                string src = System.Text.ASCIIEncoding.ASCII.GetString(oByteArray);
                                break;
                            }

                        case MimeCods.Mov:
                        case MimeCods.Mp4:
                            {
                                Image oImg = new Bitmap(
                                    System.Reflection.Assembly.GetEntryAssembly().
                                      GetManifestResourceStream("MatHelperCFwk.Resources.video.jpg"));
                                withBlock.Thumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImg, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT).Bytes();
                                break;
                            }
                    }
                }
            }
            catch (SystemException e)
            {
                exs.Add(e);
            }
            return exs.Count == 0;
        }


        public static bool LoadPdf(ref DTODocFile oDocFile, List<Exception> exs)
        {
            bool retval = false;
            GhostScriptHelper.Pdf oPdf = null/* TODO Change to default(_) if this is not a reference type */;
            if (Load(ref oPdf, oDocFile.Stream, DTODocFile.THUMB_WIDTH, DTODocFile.THUMB_HEIGHT, exs))
            {
                {
                    var withBlock = oDocFile;
                    withBlock.Size = new Size(oPdf.Width, oPdf.Height);
                    withBlock.Pags = oPdf.PageCount;
                    withBlock.Thumbnail = oPdf.Thumbnail;
                }
                retval = true;
            }
            return retval;
        }


        public static byte[] GetPdfThumbnail(byte[] oFileBytes, int iMaxWidth, int iMaxHeight)
        {
            GhostScriptHelper.Pdf oPdf = null/* TODO Change to default(_) if this is not a reference type */;
            List<Exception> exs = new List<Exception>();
            byte[] retval = null;
            if (Load(ref oPdf, oFileBytes, iMaxWidth, iMaxHeight, exs))
                retval = oPdf.Thumbnail;
            return retval;
        }

        public static byte[] GetThumbnail(System.IO.MemoryStream oStream, int iMaxWidth, int iMaxHeight)
        {
            GhostScriptHelper.Pdf oPdf = null/* TODO Change to default(_) if this is not a reference type */;
            List<Exception> exs = new List<Exception>();
            byte[] retval = null;
            if (Load(ref oPdf, oStream, iMaxWidth, iMaxHeight, exs))
                retval = oPdf.Thumbnail;
            return retval;
        }

        public static bool Load(string sFilename, ref byte[] oByteArray, int maxWidth, int MaxHeight, List<Exception> exs)
        {
            bool retval = false;
            if (FileSystemHelper.GetStreamFromFile(sFilename, ref oByteArray, ref exs))
            {
                GhostScriptHelper.Pdf oPdf = new GhostScriptHelper.Pdf();
                retval = Load(ref oPdf, oByteArray, maxWidth, MaxHeight, exs);
            }
            return retval;
        }

        public static bool Load(ref GhostScriptHelper.Pdf oPdf, byte[] oByteArray, int maxWidth, int maxHeight, List<Exception> exs)
        {
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
            bool retval = Load(ref oPdf, oStream, maxWidth, maxHeight, exs);
            return retval;
        }
        public static bool LoadToFit(ref GhostScriptHelper.Pdf oPdf, byte[] oByteArray, int maxWidth, int maxHeight, List<Exception> exs)
        {
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
            bool retval = LoadToFit(ref oPdf, oStream, maxWidth, maxHeight, exs);
            return retval;
        }

        public static bool Load(ref GhostScriptHelper.Pdf oPdf, string sFilename, int maxWidth, int maxHeight, List<Exception> exs)
        {
            System.IO.Stream oStream = new System.IO.FileStream(sFilename, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            bool retval = Load(ref oPdf, oStream, maxWidth, maxHeight, exs);
            return retval;
        }

        public static bool Load(ref GhostScriptHelper.Pdf oPdf, System.IO.Stream oStream, int maxWidth, int maxHeight, List<Exception> exs)
        {
            bool retval = GhostScriptHelper.Rasterize(oStream, ref oPdf, exs);
            if (retval)
            {
                if (maxWidth == 0 & maxHeight == 0)
                    oPdf.Thumbnail = oPdf.Portrait;
                else
                    oPdf.Thumbnail = ImageHelper.GetThumbnailToFill((Bitmap)ImageHelper.FromBytes(oPdf.Portrait), maxWidth, maxHeight).Bytes();
            }
            return retval;
        }

        public static bool LoadToFit(ref GhostScriptHelper.Pdf oPdf, System.IO.Stream oStream, int maxWidth, int maxHeight, List<Exception> exs)
        {
            bool retval = GhostScriptHelper.Rasterize(oStream, ref oPdf, exs);
            if (retval)
            {
                if (maxWidth == 0 & maxHeight == 0)
                    oPdf.Thumbnail = oPdf.Portrait;
                else
                {
                    var oPortrait = ImageHelper.FromBytes(oPdf.Portrait);
                    oPdf.Thumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oPortrait, maxWidth, maxHeight).Bytes();
                }
            }
            return retval;
        }

        public static byte[] ThumbnailPreview(DTODocFile oDocFile)
        {
            byte[] retval = null;
            if (oDocFile != null)
            {
                switch (oDocFile.Mime)
                {
                    case MimeCods.Bmp:
                    case MimeCods.Gif:
                    case MimeCods.Jpg:
                    case MimeCods.Png:
                    case MimeCods.Tif:
                    case MimeCods.Tiff:
                        {
                            var oImg = ImageHelper.FromBytes(oDocFile.Thumbnail);
                            retval = ImageHelper.GetThumbnailToFill((Bitmap)oImg, 100, 100).Bytes();
                            break;
                        }

                    case MimeCods.Pdf:
                        {
                            break;
                        }
                }
            }
            return retval;
        }
    }
}
