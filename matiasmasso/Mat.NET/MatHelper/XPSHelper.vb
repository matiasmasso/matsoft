Imports System.IO.Packaging
Imports System.Windows.Documents
Imports System.Windows.Xps.Packaging
Imports System.Windows.Media.Imaging

Public Class XPSHelper
    Private mXpsDocument As XpsDocument
    Private mDocumentPaginator As DocumentPaginator
    Private mDefaultThumbnailWidth As Integer = 350
    Private mDefaultThumbnailHeight As Integer = 400
    Private Const FACTOR_XPSTOMM As Decimal = 3.88571

    Public Sub New(oXpsDocument As XpsDocument)
        MyBase.New()
        mXpsDocument = oXpsDocument
        Dim docSeq As FixedDocumentSequence = mXpsDocument.GetFixedDocumentSequence()
        mDocumentPaginator = docSeq.DocumentPaginator
    End Sub

    Public Sub New(oByteArray() As Byte)
        MyBase.New()
        Dim oStream As New System.IO.MemoryStream(oByteArray)
        mXpsDocument = GetXPSDocumentFromMemoryStream(oStream)
        Dim docSeq As FixedDocumentSequence = mXpsDocument.GetFixedDocumentSequence()
        mDocumentPaginator = docSeq.DocumentPaginator
    End Sub

    Public Shared Function FromFilename(sFilename As String) As XPSHelper
        Dim oXpsDoc As New XpsDocument(sFilename, System.IO.FileAccess.Read)
        Dim retval As New XPSHelper(oXpsDoc)
        Return (retval)
    End Function

    Public Function PageCount() As Integer
        Return mDocumentPaginator.PageCount
    End Function

    Public Function PageWidth() As Integer
        Dim oSize As System.Windows.Size = mDocumentPaginator.PageSize
        Dim retval As Integer = Math.Round(oSize.Width / FACTOR_XPSTOMM, 0, MidpointRounding.AwayFromZero)
        Return retval
    End Function

    Public Function PageHeight() As Integer
        Dim oSize As System.Windows.Size = mDocumentPaginator.PageSize
        Dim retval As Integer = Math.Round(oSize.Height / 3.55, 0, MidpointRounding.AwayFromZero)
        Return retval
    End Function

    Public Function GenerateThumbnail(Optional iWidth As Integer = 0, Optional iHeight As Integer = 0) As System.Drawing.Image
        If iWidth = 0 Then iWidth = mDefaultThumbnailWidth
        If iHeight = 0 Then iHeight = mDefaultThumbnailHeight

        Dim docSeq As FixedDocumentSequence = mXpsDocument.GetFixedDocumentSequence()

        Dim encoder As New JpegBitmapEncoder() 'Choose type here ie: JpegBitmapEncoder, BmpBitmapEncoder, etc
        Dim imageQualityRatio As Double = 1.0

        Dim FirstPageNum As Integer = 0
        Dim docPage As DocumentPage = docSeq.DocumentPaginator.GetPage(FirstPageNum)

        Dim targetBitmap As New RenderTargetBitmap( _
            CInt(docPage.Size.Width * imageQualityRatio), _
            CInt(docPage.Size.Height * imageQualityRatio), _
            96 * imageQualityRatio, 96 * imageQualityRatio, _
            System.Windows.Media.PixelFormats.Pbgra32) 'WPF(Avalon) units are 96 dpi based  
        targetBitmap.Render(docPage.Visual)

        encoder.Frames.Add(BitmapFrame.Create(targetBitmap))

        Dim oStream As New System.IO.MemoryStream()
        encoder.Save(oStream)

        Dim oImage As System.Drawing.Image = System.Drawing.Image.FromStream(oStream)
        Dim retval As System.Drawing.Image = ImageHelper.GetThumbnailToFit(oImage, iWidth, iHeight)
        Return retval
    End Function

    Private Function GetXPSDocumentFromMemoryStream(oStream As System.IO.MemoryStream) As XpsDocument
        Dim retval As XpsDocument = Nothing
        'Using package1 As Package = Package.Open(oStream)
        Dim package1 As Package = Package.Open(oStream)
        'Remember to create URI for the package
        Dim packageUriString As String = "memorystream://data.xps"
        Dim packageUri As New Uri(packageUriString)

        'Need to add the Package to the PackageStore
        PackageStore.RemovePackage(packageUri)
        PackageStore.AddPackage(packageUri, package1)

        'Create instance of XpsDocument 
        retval = New XpsDocument(package1, System.IO.Packaging.CompressionOption.Maximum, packageUriString)

        'End Using
        Return retval
    End Function

End Class
