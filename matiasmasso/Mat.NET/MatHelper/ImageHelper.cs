using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using MatHelperStd;

namespace MatHelper
{
    public class ImageHelper
    {
        private static System.Drawing.Image.GetThumbnailImageAbort CallBack = new System.Drawing.Image.GetThumbnailImageAbort(MycallBack);

        public static bool MycallBack()
        {
            return false;
        }

        public static byte[] Converter(Image oImage)
        {
            return oImage.Bytes();
        }
        public static Image Converter(byte[] oBytes)
        {
            return FromBytes(oBytes);
        }

        public static Image EmptyImage(int width, int height)
        {
            return new Bitmap(width, height);
        }

        public static Bitmap GetThumbnailToFit(byte[] src, int finalWidth = 0, int finalHeight = 0)
        {
            MemoryStream ms = new MemoryStream(src);
            var oBmp = System.Drawing.Image.FromStream(ms);
            return GetThumbnailToFit((Bitmap)oBmp, finalWidth, finalHeight);
        }

        public static Bitmap GetThumbnailToFit(Bitmap oBmp, int finalWidth = 0, int finalHeight = 0)
        {
            Bitmap retval = null/* TODO Change to default(_) if this is not a reference type */;

            if (oBmp != null)
            {
                int iWidth = oBmp.Width;
                int iHeight = oBmp.Height;

                if (finalWidth > 0)
                {
                    if (iWidth > finalWidth)
                    {
                        iHeight = iHeight * finalWidth / (int)iWidth;
                        iWidth = finalWidth;
                    }
                }

                if (finalHeight > 0)
                {
                    if (iHeight > finalHeight)
                    {
                        iWidth = iWidth * finalHeight / (int)iHeight;
                        iHeight = finalHeight;
                    }
                }

                if (iWidth == oBmp.Width & iHeight == oBmp.Height)
                    retval = oBmp;
                else
                    retval = (Bitmap)oBmp.GetThumbnailImage(iWidth, iHeight, CallBack, System.IntPtr.Zero);// New System.IntPtr(0))
            }

            return retval;
        }



        public static Bitmap ResizeImage(Image srcImage, int maxWidth, int maxHeight)
        {
            int srcX = 0;
            int srcY = 0;
            int srcWidth = srcImage.Width;
            int srcHeight = srcImage.Height;

            int destX = 0;
            int destY = 0;
            int destWidth;
            int destHeight;

            // factors de reducció %
            decimal DcWidthFactor = srcWidth / (int)maxWidth;
            decimal DcHeightFactor = srcHeight / (int)maxHeight;
            bool landscape = DcWidthFactor > DcHeightFactor;

            if (landscape)
            {
                if ((srcWidth > maxWidth))
                {
                    destWidth = maxWidth;
                    destHeight = srcHeight / (int)DcWidthFactor;
                }
                else
                {
                    destWidth = srcWidth;
                    destHeight = srcHeight;
                    destX = (maxWidth - destWidth) / (int)2; // centra-ho horitzontalment
                }
            }
            else if ((srcHeight > maxHeight))
            {
                destHeight = maxHeight;
                destWidth = srcWidth / (int)DcHeightFactor;
                destX = (maxWidth - destWidth) / 2; // centra-ho horitzontalment
            }
            else
            {
                destHeight = srcHeight;
                destWidth = srcWidth;
                destX = (maxWidth - destWidth) / 2; // centra-ho horitzontalment
            }

            var destRect = new Rectangle(destX, destY, destWidth, destHeight);
            var destImage = new Bitmap(maxWidth, maxHeight);


            destImage.SetResolution(srcImage.HorizontalResolution, srcImage.VerticalResolution);
            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                // Set a white background in case srcImage does not fill destImage
                graphics.Clear(System.Drawing.Color.White);

                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (System.Drawing.Imaging.ImageAttributes oWrapMode = new System.Drawing.Imaging.ImageAttributes())
                {
                    oWrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(srcImage, destRect, srcX, srcY, srcWidth, srcHeight, GraphicsUnit.Pixel, oWrapMode);
                }
            }
            return destImage;
        }



        public static System.Drawing.Image FromBytes(byte[] oBytes)
        {
            Image retval = null/* TODO Change to default(_) if this is not a reference type */;
            if (oBytes != null && oBytes.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(oBytes))
                {
                    // retval = Image.FromStream(ms)
                    retval = new Bitmap(Image.FromStream(ms));
                }
            }
            return retval;
        }

        public static Bitmap GetThumbnailToFill(Bitmap oBmp, int finalWidth = 0, int finalHeight = 0)
        {
            Bitmap retval = null/* TODO Change to default(_) if this is not a reference type */;

            if (oBmp != null)
            {
                int iWidth = oBmp.Width;
                int iHeight = oBmp.Height;
                int iReducedWidth;
                int iReducedHeight;
                int X;
                int Y;

                // factors de reducció %
                decimal DcWidthFactor = finalWidth / iWidth;
                decimal DcHeightFactor = finalHeight / iHeight;

                if (DcWidthFactor > DcHeightFactor)
                {
                    iReducedWidth = finalWidth;
                    iReducedHeight = iHeight * finalWidth / iWidth;
                    X = 0;
                    Y = (iReducedHeight - finalHeight) / 2;
                }
                else
                {
                    iReducedWidth = iWidth * finalHeight / iHeight;
                    iReducedHeight = finalHeight;
                    X = (iReducedWidth - finalWidth) / 2;
                    Y = 0;
                }
                Bitmap oReducedBmp = (Bitmap)oBmp.GetThumbnailImage(iReducedWidth, iReducedHeight, CallBack, System.IntPtr.Zero);
                System.Drawing.Rectangle oRectangle = new System.Drawing.Rectangle(X, Y, finalWidth, finalHeight);
                retval = oReducedBmp.Clone(oRectangle, oReducedBmp.PixelFormat);
            }

            return retval;
        }

        public static Bitmap GetThumbnailToFitAndFill(Bitmap oBmp, int finalWidth = 0, int finalHeight = 0)
        {
            Bitmap Picture = GetThumbnailToFit(oBmp, finalWidth, finalHeight);
            Bitmap retval = new Bitmap(finalWidth, finalHeight);

            int X = (retval.Width - Picture.Width) / 2;
            int Y = (retval.Height - Picture.Height) / 2;
            using (Graphics GraphicsObject = Graphics.FromImage(retval))
            {
                GraphicsUnit unit = GraphicsUnit.Pixel;
                System.Drawing.RectangleF oRectangle = retval.GetBounds(ref unit);
                GraphicsObject.FillRectangle(Brushes.White, oRectangle);
                GraphicsObject.DrawImage(Picture, X, Y);
            }

            return retval;
        }

        public static System.Drawing.Image GetImgFromByteArray(byte[] oByteArray)
        {
            System.Drawing.Image retval = null/* TODO Change to default(_) if this is not a reference type */;
            System.IO.MemoryStream ImageStream;
            try
            {
                if (oByteArray != null)
                {
                    if (oByteArray.GetUpperBound(0) > 0)
                    {
                        ImageStream = new System.IO.MemoryStream(oByteArray);
                        retval = System.Drawing.Image.FromStream(ImageStream);
                    }
                    else
                        retval = null/* TODO Change to default(_) if this is not a reference type */;
                }
            }
            catch (Exception ex)
            {
                retval = null/* TODO Change to default(_) if this is not a reference type */;
            }

            return retval;
        }


        public static byte[] GetByteArrayFromImg(System.Drawing.Image oImg)
        {
            byte[] retVal;
            if (oImg == null)
                retVal = null;
            else
            {
                System.IO.MemoryStream oMemStream = new System.IO.MemoryStream();
                // oImg.Save(oMemStream, System.Drawing.Imaging.ImageFormat.Jpeg)

                string sMime = GetLegacyImageMediaType(oImg);
                System.Drawing.Imaging.ImageCodecInfo encoder = GetEncoderInfoFromMime(sMime);

                // Set image quality to reduce image and file size ...
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters(1);
                encoderParams.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);

                oImg.Save(oMemStream, encoder, encoderParams);
                retVal = oMemStream.ToArray();
                oMemStream.Close();
            }
            return retVal;
        }


        public static MimeCods GetLegacyImageMimeCod(System.Drawing.Image oImage)
        {
            System.Drawing.Imaging.ImageFormat oImageFormat = oImage.RawFormat;
            var retval = MimeCods.Jpg;
            if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)))
                retval = MimeCods.Jpg;
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)))
                retval = MimeCods.Gif;
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)))
                retval = MimeCods.Png;
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)))
                retval = MimeCods.Tif;
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)))
                retval = MimeCods.Bmp;

            var sExtension = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == oImageFormat.Guid).FilenameExtension;
            return retval;
        }



        public static string GetImageMediaType(System.Drawing.Image oImage)
        {
            System.Drawing.Imaging.ImageFormat oImageFormat = oImage.RawFormat;
            string retval = "image/jpeg"; // default
            if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)))
                retval = "image/jpeg";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)))
                retval = "image/gif";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)))
                retval = "image/png";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)))
                retval = "image/tiff";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)))
                retval = "image/bmp";
            return retval;
        }


        public static System.Drawing.Imaging.ImageCodecInfo GetEncoderInfoFromMime(string sMimeType)
        {
            int j;
            System.Drawing.Imaging.ImageCodecInfo retval = null/* TODO Change to default(_) if this is not a reference type */;
            System.Drawing.Imaging.ImageCodecInfo[] encoders = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            for (j = 0; j <= encoders.Length - 1; j++)
            {
                if (encoders[j].MimeType == sMimeType)
                    retval = encoders[j];
            }
            return retval;
        }

        public static System.Drawing.Image CropImage(System.Drawing.Image OriginalImage, int CropWidth, int CropHeight, int CropX = 0, int CropY = 0)
        {
            System.Drawing.Rectangle CropRect = new System.Drawing.Rectangle(CropX, CropY, CropWidth, CropHeight);
            Bitmap retval = new Bitmap(CropRect.Width, CropRect.Height);
            if (OriginalImage != null)
            {
                using (var grp = Graphics.FromImage(retval))
                {
                    grp.DrawImage(OriginalImage, new System.Drawing.Rectangle(0, 0, CropRect.Width, CropRect.Height), CropRect, GraphicsUnit.Pixel);
                    OriginalImage.Dispose();
                }
            }
            return retval;
        }


        public static MimeCods GuessMime(System.Drawing.Image oImage)
        {
            byte[] oByteArray = GetByteArrayFromImg(oImage);
            MimeCods retval = GuessMime(oByteArray);
            return retval;
        }

        public static MimeCods GuessMime(byte[] oByteArray)
        {
            // see https://www.mikekunz.com/image_file_header.html  

            MimeCods retval = MimeCods.NotSet;
            if (oByteArray != null)
            {
                byte[] bmp = System.Text.Encoding.ASCII.GetBytes("BM");
                byte[] gif = System.Text.Encoding.ASCII.GetBytes("GIF");
                byte[] mp4 = System.Text.Encoding.ASCII.GetBytes("MP41");
                byte[] png = new byte[] { 137, 80, 78, 71 };
                byte[] tiff = new byte[] { 73, 73, 42 };
                byte[] tiff2 = new byte[] { 77, 77, 42 };
                byte[] jpeg = new byte[] { 255, 216, 255, 224 }; // hex = "FF-D8"
                byte[] jpeg2 = new byte[] { 255, 216, 255, 225 };

                if (bmp.SequenceEqual(oByteArray.Take(bmp.Length)))
                    retval = MimeCods.Bmp;
                else if (gif.SequenceEqual(oByteArray.Take(gif.Length)))
                    retval = MimeCods.Gif;
                else if (png.SequenceEqual(oByteArray.Take(png.Length)))
                    retval = MimeCods.Png;
                else if (tiff.SequenceEqual(oByteArray.Take(tiff.Length)))
                    retval = MimeCods.Tiff;
                else if (tiff2.SequenceEqual(oByteArray.Take(tiff2.Length)))
                    retval = MimeCods.Tiff;
                else if (jpeg.SequenceEqual(oByteArray.Take(jpeg.Length)))
                    retval = MimeCods.Jpg;
                else if (jpeg2.SequenceEqual(oByteArray.Take(jpeg2.Length)))
                    retval = MimeCods.Jpg;
                else if (mp4.SequenceEqual(oByteArray.Take(mp4.Length)))
                    retval = MimeCods.Mp4;
            }
            return retval;
        }


        public static System.Drawing.Imaging.ImageFormat GetImageFormat(MimeCods oMimeCod)
        {
            System.Drawing.Imaging.ImageFormat retval = null/* TODO Change to default(_) if this is not a reference type */;
            switch (oMimeCod)
            {
                case MimeCods.Gif:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Gif;
                        break;
                    }

                case MimeCods.Jpg:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Jpeg;
                        break;
                    }

                case MimeCods.Bmp:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Bmp;
                        break;
                    }

                case MimeCods.Tif:
                case MimeCods.Tiff:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Tiff;
                        break;
                    }

                case MimeCods.Png:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Png;
                        break;
                    }

                default:
                    {
                        retval = System.Drawing.Imaging.ImageFormat.Wmf;
                        break;
                    }
            }
            return retval;
        }

        public static System.Drawing.Image GetThumbnailFromText(string src, int iWidth, int iHeight)
        {
            var FontColor = System.Drawing.Color.Navy;
            var BackColor = System.Drawing.Color.White;
            string FontName = "Arial";
            int FontSize = 14;
            Bitmap oBitmap = new Bitmap(794, 1123); // 96dpi, 595x842 px a 72dpi
            Graphics oGraphics = Graphics.FromImage(oBitmap);
            System.Drawing.Font oFont = new System.Drawing.Font(FontName, FontSize);
            System.Drawing.PointF oPoint = new System.Drawing.PointF(5.0F, 5.0F);
            System.Drawing.SolidBrush oBrushForeColor = new System.Drawing.SolidBrush(FontColor);
            System.Drawing.SolidBrush oBrushBackColor = new System.Drawing.SolidBrush(BackColor);
            oGraphics.FillRectangle(oBrushBackColor, 0, 0, oBitmap.Width, oBitmap.Height);
            oGraphics.DrawString(src, oFont, oBrushForeColor, oPoint);

            System.Drawing.SizeF oSizeF = oGraphics.MeasureString(src, oFont);
            System.Drawing.Rectangle oCropRect = new System.Drawing.Rectangle(0, 0, System.Convert.ToInt32(oSizeF.Width), System.Convert.ToInt32(oSizeF.Height));
            Bitmap oCropImage = new Bitmap(oCropRect.Width, oCropRect.Height);
            using (var grp = Graphics.FromImage(oCropImage))
            {
                grp.DrawImage(oBitmap, new System.Drawing.Rectangle(0, 0, oCropRect.Width, oCropRect.Height), oCropRect, GraphicsUnit.Pixel);
                oBitmap.Dispose();
            }

            System.Drawing.Image retval = GetThumbnailToFit(oCropImage, iWidth, iHeight);
            return retval;
        }


        public static bool IsImage(string sFilename)
        {
            bool retval=false;
            int iPos = sFilename.LastIndexOf(".");
            if (iPos > 0)
            {
                switch (sFilename.Substring(iPos))
                {
                    case ".jpg":
                    case ".jpeg":
                    case ".gif":
                    case ".tiff":
                    case ".tif":
                    case ".bmp":
                    case ".png":
                    case ".eps":
                        {
                            retval = true;
                            break;
                        }
                }
            }
            return retval;
        }

        public static bool IsImage(MimeCods oMime)
        {
            bool retval=false;
            switch (oMime)
            {
                case MimeCods.Jpg:
                case MimeCods.Gif:
                case MimeCods.Tif:
                case MimeCods.Tiff:
                case MimeCods.Bmp:
                case MimeCods.Png:
                case MimeCods.Eps:
                    {
                        retval = true;
                        break;
                    }
            }
            return retval;
        }


        public static string GetLegacyImageMediaType(System.Drawing.Image oImage)
        {
            System.Drawing.Imaging.ImageFormat oImageFormat = oImage.RawFormat;
            string retval = "image/jpeg"; // default
            if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg)))
                retval = "image/jpeg";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif)))
                retval = "image/gif";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Png)))
                retval = "image/png";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff)))
                retval = "image/tiff";
            else if ((oImageFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp)))
                retval = "image/bmp";
            return retval;
        }
    }
}
