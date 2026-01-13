using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using C1.C1Pdf;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Versioning;

namespace Web.Helpers
{
    [SupportedOSPlatform("windows")]
    public class PdfBase
    {
        public C1PdfDocument Pdf { get; set; }
        public bool Landscape { get; set; }
        public Font Font { get; set; }
        public Brush Brush { get; set; }
        public Pen Pen { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int marginTop { get; set; }
        public int marginLeft { get; set; }
        public int marginRight { get; set; }
        public int marginBottom { get; set; }

        public List<Exception> exs { get; set; } = new List<Exception>();

        protected double PxToMm = 25.4 / 100;
        protected double MmToPx ;



        public PdfBase(System.Drawing.Printing.PaperKind paperKind = System.Drawing.Printing.PaperKind.A4, bool landscape = false)
        {
            MmToPx = 1 / PxToMm;
            Pdf = new C1PdfDocument(paperKind, landscape);
            Font = new Font("Helvetica", 8, FontStyle.Regular);
            Brush = new SolidBrush(System.Drawing.Color.Black);
            Pen = new Pen(Brush, (float)0.5);
        }

        public void SetMargins(int top = 0, int left = 0, int right = 0, int bottom = 0)
        {
            this.marginTop = top;
            this.marginLeft = left;
            this.marginRight = right;
            this.marginBottom = bottom;
        }

        public int Top()
        {
            return (int)(this.Pdf.PageRectangle.Top + this.marginTop);
        }

        public int Right()
        {
            var retval = this.Pdf.PageRectangle.Right - this.marginRight;
            return (int)retval;
        }

        public int Bottom()
        {
            return (int)(this.Pdf.PageRectangle.Bottom - this.Bottom());
        }

        public int Left()
        {
            var retval = this.Pdf.PageRectangle.Left + this.marginLeft;
            return (int)retval;
        }

        public void NewPage()
        {
            this.Pdf.NewPage();
        }

        public byte[] Stream()
        {
            System.IO.MemoryStream oMemoryStream = new System.IO.MemoryStream();
            Pdf.Save(oMemoryStream);

            byte[] retval = oMemoryStream.ToArray();
            return retval;
        }

        public void DrawStringLine(string sText)
        {
            DrawString(sText);
            Y += Font.Height;
        }

        public void DrawString(string sText, Font? font = default, Brush? brush = default, System.Drawing.RectangleF rc = default, StringFormat? sF = default)
        {
            if (font == null)
                font = Font;
            if (brush == null)
                brush = Brush;
            if (sF == null)
                sF = new StringFormat();
            if (!string.IsNullOrEmpty(sText ))
            {
                System.Drawing.PointF pt;
                if (rc == default )
                    pt = new System.Drawing.PointF(marginLeft + X, marginTop + Y);
                else
                    pt = new System.Drawing.PointF(rc.Left, rc.Top);
                Pdf.DrawString(sText, font, brush, pt, sF);
            }
        }

        public void DrawCenteredString(string sText)
        {
            if (!string.IsNullOrEmpty(sText))
            {
                int iWidth = (int)this.Pdf.MeasureString(sText, Font).Width;
                System.Drawing.PointF pt = new System.Drawing.PointF((float)(marginLeft + X - iWidth / (double)2), marginTop + Y);
                Pdf.DrawString(sText, Font, Brush, pt);
            }
        }

        public void DrawImage(System.Drawing.Image oImage, System.Drawing.RectangleF rc)
        {
            this.Pdf.DrawImage(oImage, rc);
        }

        public void DrawRectangle(int iX, int iY, float iWidth = -1, float iHeight = -1)
        {
            if (iWidth == -1)
                iWidth = this.Pdf.PageRectangle.Width - iX - this.marginLeft - this.marginRight;
            if (iHeight == -1)
                iHeight = this.Pdf.PageRectangle.Height - iY - this.marginTop - this.marginBottom;
            this.Pdf.DrawRectangle(Pen, marginLeft + iX, marginTop + iY, iWidth, iHeight);
        }

        public void DrawPageRectangle()
        {
            System.Drawing.RectangleF rc = Pdf.PageRectangle;
            // rc.Inflate(-_marginLeft - _marginRight, -_marginTop - _marginBottom)
            rc.Inflate(-marginLeft, -marginTop);
            this.Pdf.DrawRectangle(Pen, rc);
        }


        public System.Drawing.Rectangle MarginsRectangle()
        {
            var retval = new System.Drawing.Rectangle(this.marginLeft, this.marginTop, (int)(this.Pdf.PageRectangle.Width - this.marginLeft - this.marginRight), (int)(this.Pdf.PageRectangle.Height - this.marginTop - this.marginBottom));
            return retval;
        }

        public int MeasureStringWidth(string s)
        {
            int retval = (int)Pdf.MeasureString(s, Font).Width;
            return retval;
        }

        public Font GetAdjustedFont(string GraphicString, int ContainerWidth, int MaxFontSize, int MinFontSize, bool SmallestOnFail = false)
        {
            Font? testFont = default;
            for (int AdjustedSize = MaxFontSize; AdjustedSize >= MinFontSize; AdjustedSize += -1)
            {
                testFont = new Font(Font.Name, AdjustedSize, Font.Style);
                System.Drawing.SizeF AdjustedSizeNew = Pdf.MeasureString(GraphicString, testFont);
                if (ContainerWidth > Convert.ToInt32(AdjustedSizeNew.Width))
                    return testFont;
            }

            if (SmallestOnFail)
                return testFont;
            else
                return Font;
        }
    }
}

