Public Class DTOMediaResource
    Inherits DTOBaseGuid
    Implements iMedia

    Public Const THUMBWIDTH = 140
    Public Const THUMBHEIGHT = 140
    Public Const MAXNOMLENGTH = 80
    Public Const PATH As String = "C:\Public\Matsoft\Recursos\"
    Public Const VIRTUALPATH As String = "~/Recursos/"

    Property Hash As String Implements iMedia.Hash
    Property Mime As MimeCods Implements iMedia.Mime
    Property Length As Double Implements iMedia.Length
    Property Size As Size Implements iMedia.Size
    Property HRes As Integer Implements iMedia.HRes
    Property VRes As Integer Implements iMedia.VRes
    Property Pags As Integer Implements iMedia.Pags
    <JsonIgnore> Property Thumbnail As Image Implements iMedia.Thumbnail
    <JsonIgnore> Property Stream As Byte()
    Property Filename As String
    Property Nom As String
    Property Cod As Cods
    Property Lang As DTOLang
    Property Product As DTOProduct
    Property FchCreated As Date Implements iMedia.Fch
        Get
            Return _UsrLog.fchCreated
        End Get
        Set(value As Date)
            _UsrLog.fchCreated = value
        End Set
    End Property

    Property UsrLog As DTOUsrLog
    Property Obsolet As Boolean



    Public Enum Cods
        NotSet
        Product
        Features
        LifeStyle
        Banner
        Logo
        Video
    End Enum

    Public Sub New()
        MyBase.New()
        _UsrLog = New DTOUsrLog
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _UsrLog = New DTOUsrLog
    End Sub

    Shared Function Factory(oUser As DTOUser, Optional oProduct As DTOProduct = Nothing) As DTOMediaResource
        Dim retval As New DTOMediaResource
        With retval
            .Product = oProduct
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Shared Function Factory(oUser As DTOUser, oByteArray As Byte()) As DTOMediaResource
        Dim retval = DTOMediaResource.Factory(oUser)
        With retval
            .Stream = oByteArray
            .Length = oByteArray.Length
            .Hash = CryptoHelper.HashMD5(oByteArray)
        End With
        Return retval
    End Function

    Public Function ViewModel() As Model
        Dim retval As New Model
        With retval
            .Guid = MyBase.Guid
            .Product = _Product
            .Filename = _Filename
            .Cod = _Cod
            .Lang = _Lang
        End With
        Return retval
    End Function

    Public Class Model
        Property Guid As Guid
        Property Filename As String
        Property Nom As String
        Property Cod As Cods
        Property Lang As DTOLang
        Property Product As DTOProduct

    End Class


    Shared Function CodTitle(value As DTOMediaResource, oLang As DTOLang) As String
        Dim retval As String = ""
        Select Case value.Cod
            Case DTOMediaResource.Cods.Product
                retval = oLang.Tradueix("Imágenes de producto", "Imatges de producte", "Product images")
            Case DTOMediaResource.Cods.Features
                retval = oLang.Tradueix("Imágenes de detalle", "Imatges de detall", "Features images")
            Case DTOMediaResource.Cods.LifeStyle
                retval = oLang.Tradueix("Imágenes lifestyle", "Imatges lifestyle", "Lifestyle images")
            Case DTOMediaResource.Cods.Banner
                retval = "Banners"
            Case DTOMediaResource.Cods.Logo
                retval = "Logos"
            Case DTOMediaResource.Cods.Video
                retval = "Vídeos"
        End Select
        Return retval
    End Function

    Shared Function TargetFilename(oMediaResource As DTOMediaResource) As String
        Dim sName As String = CryptoHelper.UrFriendlyBase64(oMediaResource.Hash)
        Dim sExtension As String = MimeHelper.GetExtensionFromMime(oMediaResource.Mime)
        Dim retval As String = String.Format("{0}{1}", sName, sExtension)
        Return retval
    End Function

    Shared Function HashFromFilename(filename As String) As String
        Dim iPos As Integer = filename.LastIndexOfAny(New Char() {"/", "\"})
        If iPos > 0 Then filename = filename.Substring(iPos + 1)
        iPos = filename.LastIndexOf(".")
        If iPos > 0 Then filename = filename.Substring(0, iPos)
        Dim retval As String = CryptoHelper.FromUrFriendlyBase64(filename)
        Return retval
    End Function

    Shared Function FriendlyName(oMediaResource As DTOMediaResource) As String
        Dim retval As String = ""
        Dim sNom As String = oMediaResource.Nom
        If sNom = "" Then
            retval = String.Format("M+O recurso.{0}", oMediaResource.Mime.ToString())
        Else
            Dim iPos As Integer = sNom.LastIndexOf(".")
            If iPos >= 0 Then
                Dim sExtension As String = sNom.Substring(iPos + 1)
                If sExtension.ToLower = oMediaResource.Mime.ToString.ToLower Then
                    retval = sNom
                Else
                    retval = String.Format("{0}.{1}", sNom, oMediaResource.Mime.ToString())
                End If
            Else
                retval = String.Format("{0}.{1}", sNom, oMediaResource.Mime.ToString())
            End If
        End If
        Return retval
    End Function

    Shared Function validateThumbnail(sFilename As String, ByRef oImage As Image, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            oImage = Image.Load(sFilename)
            If oImage.Width = DTOMediaResource.THUMBWIDTH And oImage.Height = DTOMediaResource.THUMBHEIGHT Then
                retval = True
            Else
                exs.Add(New Exception("la miniatura ha de ser de " & DTOMediaResource.THUMBWIDTH & "x" & DTOMediaResource.THUMBHEIGHT & " px."))
            End If
        Catch ex As OutOfMemoryException
            exs.Add(New Exception("error memoria exhaurida"))
        Catch ex2 As System.IO.FileNotFoundException
            exs.Add(New Exception("error no s'ha trobat el fitxer " & sFilename))
        Catch ex3 As Exception
            exs.Add(New Exception("error al obrir la imatge"))
        End Try
        Return retval
    End Function


    Public Shadows Function UnEquals(oCandidate As DTOMediaResource) As Boolean
        Dim retval As Boolean = Not Equals(oCandidate)
        Return retval
    End Function

    Public Shadows Function Equals(oCandidate As DTOMediaResource) As Boolean
        Dim retval As Boolean
        If oCandidate IsNot Nothing Then
            retval = _Hash = oCandidate.Hash
        End If
        Return retval
    End Function

    Public Function Features() As String
        Dim retval As String = ""
        Dim sb As New Text.StringBuilder
        If _Mime <> MimeCods.NotSet Then sb.Append(_Mime.ToString & " ")
        If _Size <> Nothing AndAlso _Size.Width > 0 Then sb.Append(_Size.Width & "x" & _Size.Height & " ")
        If _Length > 0 Then sb.Append(FileSize())
        retval = sb.ToString
        Return retval
    End Function

    Public Function FileSize() As String
        Dim sb As New System.Text.StringBuilder()
        If _Length > 1000000000 Then
            sb.Append(String.Format("{0:#,##0.#} Gb", _Length / 1000000000))
        ElseIf _Length > 1000000 Then
            sb.Append(String.Format("{0:#,##0.#} Mb", _Length / 1000000))
        ElseIf _Length > 0 Then
            sb.Append(String.Format("{0:#,##0.#} Kb", _Length))
        End If
        Dim retval = sb.ToString()
        Return retval
    End Function

    Public Class Collection
        Inherits List(Of DTOMediaResource)
    End Class
End Class
