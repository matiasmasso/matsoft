using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Ghostscript.NET.Rasterizer;
using Ghostscript.NET;
using DTO;
using MatHelper;

namespace MatHelperCFwk
{
    public class GhostScriptHelper
    {
        private static Image.GetThumbnailImageAbort CallBack = new Image.GetThumbnailImageAbort(MycallBack);

        public static bool MycallBack()
        {
            return false;
        }



        public static Pdf Rasterize(byte[] oByteArray, int iWidth, int iHeight, List<Exception> exs)
        {
            System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
            return Rasterize(exs, oStream, iWidth, iHeight);
        }

        public static Pdf Rasterize(List<Exception> exs, System.IO.MemoryStream oStream, int maxWidth = 0, int maxHeight = 0)
        {
            Pdf retval = new Pdf();
            int dpi = 96;
            decimal DcFactor = (decimal)2.54 * 10 / 96; // cm/inch x mm/cm / dpi
            GhostscriptRasterizer oRasterizer = new GhostscriptRasterizer();
            try
            {
                GhostscriptVersionInfo oLastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
                oRasterizer.Open(oStream, oLastInstalledVersion, true);
                var oPortrait = oRasterizer.GetPage(dpi, 1);

                {
                    var withBlock = retval;
                    withBlock.Portrait = ImageHelper.GetByteArrayFromImg(oPortrait); // oPortrait.Bytes()
                    if (withBlock.Portrait == null)
                        exs.Add(new Exception(@"Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"));
                    else
                    {
                        withBlock.PageCount = oRasterizer.PageCount;
                        withBlock.Width = (int)(oPortrait.Width * DcFactor);
                        withBlock.Height = (int)(oPortrait.Height * DcFactor);
                        if (maxWidth == 0 & maxHeight == 0)
                            withBlock.Thumbnail = withBlock.Portrait;
                        else
                            withBlock.Thumbnail = ImageHelper.GetThumbnailToFill((Bitmap)oPortrait, maxWidth, maxHeight).Bytes();
                    }
                }
            }
            catch (Exception ex)
            {
                exs.Add(ex);
            }

            return retval;
        }


        public static Image Pdf2Jpg(byte[] oFileBytes)
        {
            Image retval = null/* TODO Change to default(_) if this is not a reference type */;
            GhostscriptRasterizer oRasterizer = new GhostscriptRasterizer();

            lock (oRasterizer)
            {
                int dpi = 96;

                try
                {
                    GhostscriptVersionInfo oLastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
                    System.IO.MemoryStream oStream = new System.IO.MemoryStream(oFileBytes);
                    oRasterizer.Open(oStream, oLastInstalledVersion, true);
                    var oPortrait = oRasterizer.GetPage(dpi, 1);
                    retval = ImageHelper.ResizeImage(oPortrait, 350, 400);
                }
                catch (GhostscriptLibraryNotInstalledException ex)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    throw new Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html");
                }
                catch (SystemException e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    oRasterizer.Close();
                }
            }
            return retval;
        }

        public static DTODocFile.Pdf Pdf2Docfile(byte[] oFileBytes)
        {
            DTODocFile.Pdf retval = new DTODocFile.Pdf();
            GhostscriptRasterizer oRasterizer = new GhostscriptRasterizer();
            int dpi = 96;

            try
            {
                GhostscriptVersionInfo oLastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
                System.IO.MemoryStream oStream = new System.IO.MemoryStream(oFileBytes);
                oRasterizer.Open(oStream, oLastInstalledVersion, true);
                var oPortrait = oRasterizer.GetPage(dpi, 1);
                var oThumbnailBytes = ImageHelper.ResizeImage(oPortrait, 350, 400).Bytes();
                {
                    var withBlock = retval;
                    withBlock.ThumbnailDataUrl = string.Format("data:image/jpeg;base64,{0}", Convert.ToBase64String(oThumbnailBytes));
                    withBlock.Pags = oRasterizer.PageCount;
                    withBlock.Width = oPortrait.Width;
                    withBlock.Height = oPortrait.Height;
                }
            }
            catch (GhostscriptLibraryNotInstalledException ex)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                throw new Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html");
            }
            catch (SystemException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                oRasterizer.Close();
            }
            return retval;
        }

        public static Pdf Pdf2Img(byte[] oFileBytes)
        {
            Pdf retval = null;
            GhostscriptRasterizer oRasterizer = new GhostscriptRasterizer();
            int dpi = 96;

            try
            {
                GhostscriptVersionInfo oLastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
                System.IO.MemoryStream oStream = new System.IO.MemoryStream(oFileBytes);
                oRasterizer.Open(oStream, oLastInstalledVersion, true);
                var oPortrait = oRasterizer.GetPage(dpi, 1);

                decimal DcFactor = (decimal)(2.54 * 10 / 96); // cm/inch x mm/cm / dpi
                retval = new Pdf();
                {
                    var withBlock = retval;
                    withBlock.PageCount = oRasterizer.PageCount;
                    withBlock.Portrait = oPortrait.Bytes();
                    if (withBlock.Portrait == null)
                        throw (new Exception(@"Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"));
                    else
                    {
                        withBlock.Width = (int)(oPortrait.Width * DcFactor);
                        withBlock.Height = (int)(oPortrait.Height * DcFactor);
                    }
                }
            }
            catch (GhostscriptLibraryNotInstalledException ex)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                throw new Exception("Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html");
            }
            catch (SystemException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                oRasterizer.Close();
            }
            return retval;
        }

        public static bool Rasterize(System.IO.Stream oStream, ref Pdf oPdf, List<Exception> exs)
        {
            bool retval=false;
            GhostscriptRasterizer oRasterizer = new GhostscriptRasterizer();

            try
            {
                int dpi = 96;
                // Dim desired_x_dpi As Integer = 96
                // Dim desired_y_dpi As Integer = 96
                GhostscriptVersionInfo oLastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);


                oRasterizer.Open(oStream, oLastInstalledVersion, true);
                var oPortrait = oRasterizer.GetPage(dpi, 1);

                decimal DcFactor = (decimal)(2.54 * 10 / 96); // cm/inch x mm/cm / dpi
                oPdf = new Pdf();
                {
                    var withBlock = oPdf;
                    withBlock.PageCount = oRasterizer.PageCount;
                    withBlock.Portrait = oPortrait.Bytes();
                    // .Portrait = ImageHelper.Converter(oRasterizer.GetPage(desired_x_dpi, desired_y_dpi, 1))
                    if (withBlock.Portrait == null)
                        exs.Add(new Exception(@"Ghostscript no ha estat capaç de renderitzar el Pdf\nProbar de imprimir el document com a Pdf"));
                    else
                    {
                        withBlock.Width = (int)(oPortrait.Width * DcFactor);
                        withBlock.Height = (int)(oPortrait.Height * DcFactor);
                        retval = true;
                    }
                }
            }
            catch (GhostscriptLibraryNotInstalledException ex)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                string sMsg = "Falta instal.lar el component Ghostscript. Cal baixar-lo de http://www.ghostscript.com/download/gsdnld.html";
                sb.Append(sMsg);
                System.Exception e = new System.Exception(sMsg);
                exs.Add(ex);
            }
            catch (SystemException e)
            {
                exs.Add(e);
            }
            finally
            {
            }
            return retval;
        }


        public class Pdf
        {
            public int PageCount { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            [JsonIgnore]
            public byte[] Portrait { get; set; }
            [JsonIgnore]
            public byte[] Thumbnail { get; set; }
        }
    }
}
