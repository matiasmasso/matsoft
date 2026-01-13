Public Class DTOGalleryItem
    Inherits DTOBaseGuid

    Property Nom As String
    <JsonIgnore> Property Image As Image
    Property Mime As MimeCods
    Property Width As Integer
    Property Height As Integer
    Property Size As Integer
    Property FchCreated As Date
    Property Hash As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory() As DTOGalleryItem
        Dim retval As New DTOGalleryItem
        With retval
            .FchCreated = DateTime.Now
            .Nom = "(nom descriptiu de la nova imatge)"
        End With
        Return retval
    End Function

    Shared Function Factory(sFilename As String) As DTOGalleryItem
        Dim oImage As Image = Image.Load(sFilename)
        Dim oMime As MimeCods = MimeHelper.GetMimeFromExtension(sFilename)
        Dim retval As DTOGalleryItem = DTOGalleryItem.Factory(oImage, oMime)
        Return retval
    End Function

    Shared Function Factory(oImage As Image, oMime As MimeCods) As DTOGalleryItem
        Dim retval As DTOGalleryItem = DTOGalleryItem.Factory()
        With retval
            Dim oImageBytes As Byte() = MatHelperStd.ImageHelper.GetByteArrayFromImg(oImage)
            .Hash = CryptoHelper.HashMD5(oImage)
            .Image = oImage
            .Height = .Image.Height
            .Width = .Image.Width
            .Size = oImageBytes.Length / 1000
            .Mime = oMime
        End With
        Return retval
    End Function

    Shared Function MultilineText(value As DTOGalleryItem) As String
        Dim sb As New System.Text.StringBuilder
        sb.AppendLine(value.Nom)
        sb.AppendLine(value.Width & "x" & value.Height & " " & value.Mime.ToString & " " & DTODocFile.FeatureFileSize(value.Size))
        sb.AppendLine(TextHelper.VbFormat(value.FchCreated, "dd/MM/yy HH:mm"))
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function Url(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = MmoUrl.apiUrl("GalleryItem/Image", MyBase.Guid.ToString())
        Return retval
    End Function



End Class
