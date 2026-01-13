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
using System.IO.Packaging;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using System.Windows.Xps.Packaging;
using System.Drawing;

namespace MatHelper
{


public class XPSHelper
{
    private XpsDocument mXpsDocument;
    private DocumentPaginator mDocumentPaginator;
    private int mDefaultThumbnailWidth = 350;
    private int mDefaultThumbnailHeight = 400;
    private const double FACTOR_XPSTOMM = 3.88571;

    public XPSHelper(XpsDocument oXpsDocument) : base()
    {
        mXpsDocument = oXpsDocument;
        FixedDocumentSequence docSeq = mXpsDocument.GetFixedDocumentSequence();
        mDocumentPaginator = docSeq.DocumentPaginator;
    }

    public XPSHelper(byte[] oByteArray) : base()
    {
        System.IO.MemoryStream oStream = new System.IO.MemoryStream(oByteArray);
        mXpsDocument = GetXPSDocumentFromMemoryStream(oStream);
        FixedDocumentSequence docSeq = mXpsDocument.GetFixedDocumentSequence();
        mDocumentPaginator = docSeq.DocumentPaginator;
    }

    public static XPSHelper FromFilename(string sFilename)
    {
        XpsDocument oXpsDoc = new XpsDocument(sFilename, System.IO.FileAccess.Read);
        XPSHelper retval = new XPSHelper(oXpsDoc);
        return (retval);
    }

    public int PageCount()
    {
        return mDocumentPaginator.PageCount;
    }

    public int PageWidth()
    {
        System.Windows.Size oSize = mDocumentPaginator.PageSize;
        int retval = (int)Math.Round(oSize.Width / FACTOR_XPSTOMM, 0, MidpointRounding.AwayFromZero);
        return retval;
    }

    public int PageHeight()
    {
        System.Windows.Size oSize = mDocumentPaginator.PageSize;
        int retval = (int)Math.Round(oSize.Height / 3.55, 0, MidpointRounding.AwayFromZero);
        return retval;
    }

    public byte[] GenerateThumbnail(int iWidth = 0, int iHeight = 0)
    {
        if (iWidth == 0)
            iWidth = mDefaultThumbnailWidth;
        if (iHeight == 0)
            iHeight = mDefaultThumbnailHeight;

        FixedDocumentSequence docSeq = mXpsDocument.GetFixedDocumentSequence();

        JpegBitmapEncoder encoder = new JpegBitmapEncoder(); // Choose type here ie: JpegBitmapEncoder, BmpBitmapEncoder, etc
        double imageQualityRatio = 1.0;

        int FirstPageNum = 0;
        DocumentPage docPage = docSeq.DocumentPaginator.GetPage(FirstPageNum);

        RenderTargetBitmap targetBitmap = new RenderTargetBitmap(System.Convert.ToInt32(docPage.Size.Width * imageQualityRatio), System.Convert.ToInt32(docPage.Size.Height * imageQualityRatio), 96 * imageQualityRatio, 96 * imageQualityRatio, System.Windows.Media.PixelFormats.Pbgra32); // WPF(Avalon) units are 96 dpi based  
        targetBitmap.Render(docPage.Visual);

        encoder.Frames.Add(BitmapFrame.Create(targetBitmap));

        System.IO.MemoryStream oStream = new System.IO.MemoryStream();
        encoder.Save(oStream);

        var oImage = Image.FromStream(oStream);
        var oThumbnail = ImageHelper.GetThumbnailToFit((Bitmap)oImage, iWidth, iHeight);
        byte[] retval = null;
        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
        {
            oThumbnail.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            retval = ms.ToArray();
        }
        return retval;
    }

    private XpsDocument GetXPSDocumentFromMemoryStream(System.IO.MemoryStream oStream)
    {
        XpsDocument retval = null/* TODO Change to default(_) if this is not a reference type */;
        // Using package1 As Package = Package.Open(oStream)
        Package package1 = Package.Open(oStream);
        // Remember to create URI for the package
        string packageUriString = "memorystream://data.xps";
        Uri packageUri = new Uri(packageUriString);

        // Need to add the Package to the PackageStore
        PackageStore.RemovePackage(packageUri);
        PackageStore.AddPackage(packageUri, package1);

        // Create instance of XpsDocument 
        retval = new XpsDocument(package1, System.IO.Packaging.CompressionOption.Maximum, packageUriString);

        // End Using
        return retval;
    }
    }
}

