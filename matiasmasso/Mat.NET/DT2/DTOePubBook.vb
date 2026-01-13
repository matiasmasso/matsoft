Public Class DTOePubBook
    Property Lang As String = "es"
    Property Identifier As String = "urn:uuid:" & Guid.NewGuid.ToString
    Property Title As String = "sample e-Book"
    Property Author As String = "MATIAS MASSO, S.A."
    Property Creator As String = "MATIAS MASSO, S.A."
    Property Fch As Date = DateTime.Now
    Property Publisher As String = _Author
    Property Subject As String = "sin clasificar"
    <JsonIgnore> Property CoverImage As Image
    Property Closed As Boolean = False

    Property Mimetype As DTOePubMimeType = Nothing
    Property ContainerXml As DTOePubContainerXML = Nothing
    Property ContentOpf As DTOePubContentOpf = Nothing
    Property TocNcx As DTOePubTocNcx = Nothing
    Property StyleSheet As DTOePubStyleSheet = Nothing
    Property CoverPage As DTOePubCoverPage = Nothing
    Property TitlePage As DTOePubTitlePage = Nothing
    Property Chapters As List(Of DTOePubChapter)
    Property Icons As List(Of DTOePubIcon)

    Public Sub New(ByVal sTitle As String)
        MyBase.New()
        _Title = sTitle

        _Mimetype = New DTOePubMimeType
        _StyleSheet = New DTOePubStyleSheet
        If _CoverImage IsNot Nothing Then
            _CoverPage = New DTOePubCoverPage(Me)
        End If
        _TitlePage = New DTOePubTitlePage(Me)
        _Chapters = New List(Of DTOePubChapter)
        _Icons = New List(Of DTOePubIcon)
    End Sub
End Class

Public Class DTOePubChapter
    Inherits DTOePubInnerFile

    Property Title As String = ""
    Property InnerHtml As String = ""
    Property DisplayOnIndex As Boolean = True
    Property Images As ArrayList = Nothing
    Property StyleSheet As DTOePubStyleSheet

    Public Sub New(ByVal sId As String, ByVal sTitle As String, ByVal oStyleSheet As DTOePubStyleSheet, Optional ByVal BlDisplayOnIndex As Boolean = True)
        MyBase.New()
        MyBase.Id = sId
        MyBase.Extension = FileExtensions.xhtml
        MyBase.Folder = Folders.Oebps
        MyBase.MediaType = MediaTypes.xhtml
        MyBase.Rol = Rols.text
        _Title = sTitle
        _StyleSheet = oStyleSheet
        _Images = New ArrayList
        _DisplayOnIndex = BlDisplayOnIndex
    End Sub
End Class

Public Class DTOePubChapterImage
    Inherits DTOePubInnerFile

    Property Chapter As DTOePubChapter
    <JsonIgnore> Property Image As Image
    Property AlternativeText As String

    Public Sub New(oChapter As DTOePubChapter, iIdx As Integer, oImage As Image, sAlternativeText As String)
        MyBase.New()
        _Chapter = oChapter
        MyBase.Id = String.Format("{0}_img_{1}", _Chapter.Id, TextHelper.VbFormat(iIdx, "0000"))
        MyBase.Extension = FileExtensions.jpeg
        MyBase.Folder = Folders.images
        MyBase.MediaType = MediaTypes.jpeg
        _Image = oImage
        _AlternativeText = sAlternativeText
    End Sub
End Class

Public Class DTOePubContainerXML
    Inherits DTOePubInnerFile

    Property Book As DTOePubBook

    Public Sub New(ByVal oBook As DTOePubBook)
        MyBase.New()
        MyBase.Id = "container"
        MyBase.Extension = FileExtensions.xml
        MyBase.Folder = Folders.MetaInf
        _Book = oBook
    End Sub
End Class

Public Class DTOePubContentOpf
    Inherits DTOePubInnerFile

    Property Identifier As String
    Property Fch As Date
    Property Lang As String
    Property Title As String
    Property Creator As String
    Property Subject As String
    Property Chapters As List(Of DTOePubChapter)
    Property CoverImageExists As Boolean

    Property CoverPage As DTOePubCoverPage
    Property Titlepage As DTOePubTitlePage
    Property StyleSheet As DTOePubStyleSheet
    Property TocNcx As DTOePubTocNcx

    Public Sub New(ByVal oBook As DTOePubBook)
        MyBase.New()
        MyBase.Id = "content"
        MyBase.Extension = FileExtensions.opf
        MyBase.Folder = Folders.Oebps

        With oBook
            _Identifier = .Identifier
            _Fch = .Fch
            _Lang = .Lang
            _Title = .Title
            _Creator = .Creator
            _Subject = .Subject
            _Chapters = .Chapters
            _CoverImageExists = .CoverImage IsNot Nothing

            _CoverPage = .CoverPage
            _Titlepage = .TitlePage
            _StyleSheet = .StyleSheet
            _TocNcx = .TocNcx
        End With
    End Sub
End Class

Public Class DTOePubCoverPage
    Inherits DTOePubInnerFile

    Property Title As String
    <JsonIgnore> Property Image As Image
    Property StyleSheet As DTOePubStyleSheet

    Public Const IMAGE_ID As String = "cover-image"
    Public Const IMAGE_FILENAME As String = "cover.jpeg"
    Public Const IMAGE_RELATIVEPATH As String = DTOePubInnerFile.FOLDER_IMAGES & "/" & IMAGE_FILENAME
    Public Const IMAGE_ABSOLUTEPATH As String = DTOePubInnerFile.FOLDER_OEBPS & "/" & IMAGE_RELATIVEPATH

    Public Sub New(ByVal oBook As DTOePubBook)
        MyBase.New()
        MyBase.Id = "coverpage"
        MyBase.Extension = FileExtensions.xhtml
        MyBase.Folder = Folders.Oebps
        MyBase.MediaType = MediaTypes.xhtml
        MyBase.Rol = Rols.cover

        With oBook
            _Title = .Title
            _Image = .CoverImage
            _StyleSheet = .StyleSheet
        End With
    End Sub

End Class

Public Class DTOePubIcon
    Inherits DTOePubInnerFile
    Property Image As Image = Nothing


    Public Sub New(ByVal sId As String, ByVal oImage As Image)
        MyBase.New()
        MyBase.Id = sId
        MyBase.Extension = FileExtensions.gif
        MyBase.Folder = Folders.images
        _Image = oImage
    End Sub
End Class

Public MustInherit Class DTOePubInnerFile
    Property Id As String = ""
    Property Extension As FileExtensions
    Property Folder As Folders
    Property MediaType As MediaTypes
    Property Rol As Rols
    Property Filename As String
    Property Encoding As System.Text.Encoding = System.Text.Encoding.UTF8

    Public Const FOLDER_METAINF As String = "META-INF"
    Public Const FOLDER_OEBPS As String = "OEBPS"
    Public Const FOLDER_IMAGES As String = "images"

    Public Const XMLDECLARATION As String = "<?xml version='1.0' encoding='utf-8'?>"
    Public Const XHTMLDOCTYPE As String = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.1//EN' 'http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd'>"
    Public Const XMLNSATTRIBUTE As String = "<html xmlns='http://www.w3.org/1999/xhtml'>"


    Public Enum FileExtensions
        none
        xhtml
        jpeg
        gif
        css
        ncx
        opf
        xml
    End Enum

    Public Enum Folders
        Root
        MetaInf
        Oebps
        images
    End Enum

    Public Enum MediaTypes
        none
        xhtml
        jpeg
        css
        ncx
    End Enum

    Public Enum Rols
        none
        cover
        title_page
        copyright_page
        preface
        text
    End Enum

    Public Enum PathTypes
        absolute 'from Zip root
        relative 'relative to OBPS folder
    End Enum

    Public Sub New(Optional sId As String = "")
        MyBase.New()
        _Id = sId
    End Sub
End Class

Public Class DTOePubMimeType
    Inherits DTOePubInnerFile

    Public Sub New()
        MyBase.New()
        MyBase.Id = "mimetype"
        MyBase.Extension = FileExtensions.none
        MyBase.Folder = Folders.Root
    End Sub

End Class

Public Class DTOePubStyleSheet

    Inherits DTOePubInnerFile

    Public Sub New()
        MyBase.New()
        MyBase.Id = "css"
        MyBase.Extension = FileExtensions.css
        MyBase.Filename = "stylesheet.css"
        MyBase.Folder = Folders.Oebps
        MyBase.MediaType = MediaTypes.css
    End Sub

End Class

Public Class DTOePubTitlePage
    Inherits DTOePubInnerFile

    Property Title As String
    Property Creator As String
    Property Fch As Date
    Property StyleSheet As DTOePubStyleSheet
    Property CoverImageExists As Boolean

    Public Sub New(ByVal oBook As DTOePubBook)
        MyBase.New()
        MyBase.Id = "titlepage"
        MyBase.Extension = FileExtensions.xhtml
        MyBase.Folder = Folders.Oebps
        MyBase.Rol = Rols.title_page
        MyBase.MediaType = MediaTypes.xhtml

        With oBook
            _Title = oBook.Title
            _Creator = oBook.Creator
            _Fch = oBook.Fch
            _StyleSheet = oBook.StyleSheet
            _CoverImageExists = oBook.CoverImage IsNot Nothing
        End With
    End Sub

End Class

Public Class DTOePubTocNcx
    Inherits DTOePubInnerFile

    Property Identifier As String
    Property Title As String
    Property TitlePage As DTOePubTitlePage
    Property Chapters As List(Of DTOePubChapter)

    Public Sub New(ByVal oBook As DTOePubBook)
        MyBase.New()
        MyBase.Id = "ncx"
        MyBase.Extension = FileExtensions.ncx
        MyBase.FileName = "toc.ncx"
        MyBase.Folder = Folders.Oebps
        MyBase.MediaType = MediaTypes.ncx

        With oBook
            _Identifier = .Identifier
            _Title = .Title
            _TitlePage = .TitlePage
            _Chapters = .Chapters
        End With
    End Sub
End Class