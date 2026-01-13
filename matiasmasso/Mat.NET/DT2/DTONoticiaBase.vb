Imports System.Runtime.Serialization


Public Class DTONoticiaBase
    Inherits DTOBaseGuid

    Property fch As Date
    <DataMember> Property urlFriendlySegment As String
    Property visible As Boolean
    Property title As DTOLangText
    Property excerpt As DTOLangText '<AllowHtml>
    Property text As DTOLangText '<AllowHtml>

    Property destacarFrom As Date
    Property destacarTo As Date

    Property professional As Boolean
    <JsonIgnore> Property image265x150 As Image
    Property fchLastEdited As Date

    Property visitCount As Integer
    Property commentCount As Integer
    Property keywords As List(Of String)
    Property categorias As List(Of DTOCategoriaDeNoticia)
    Property distributionChannels As List(Of DTODistributionChannel)

    Property src As Srcs

    Public Enum Srcs
        News
        Eventos
        SabiasQue
        Promos
        TablonDeAnuncios
        Blog
        Content
    End Enum

    Public Sub New() 'per serialitzador Json
        MyBase.New()
    End Sub

    Public Sub New(oSrc As Srcs)
        MyBase.New()
        _title = New DTOLangText()
        _excerpt = New DTOLangText()
        _text = New DTOLangText()
        _src = oSrc
        _distributionChannels = New List(Of DTODistributionChannel)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _title = New DTOLangText()
        _excerpt = New DTOLangText()
        _text = New DTOLangText()
        _distributionChannels = New List(Of DTODistributionChannel)
    End Sub

    Public Shadows Function UrlSegment() As String
        Dim retval As String = _urlFriendlySegment
        Select Case _src
            Case Srcs.Blog
                retval = String.Format("/blog/{0}", _urlFriendlySegment)
            Case Else
                If retval > "" Then
                    retval = String.Format("/{0}/{1}", _src.ToString, _urlFriendlySegment)
                Else
                    retval = MyBase.UrlSegment("noticia")
                End If
        End Select
        Return retval.ToLower
    End Function

    Public Function friendlyUrl(AbsoluteUrl As Boolean) As String
        Return MmoUrl.Factory(AbsoluteUrl, Me.UrlSegment)
    End Function

    Public Function ThumbnailUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = MmoUrl.Image(Defaults.ImgTypes.News265x150, MyBase.Guid, AbsoluteUrl)
        Return retval
    End Function

    Public Function toSpecificObject() As DTONoticiaBase
        Dim retval As DTONoticiaBase = Me
        If TypeOf Me Is DTONoticia Then
            retval = Me
        ElseIf TypeOf Me Is DTOEvento Then
            retval = Me
        Else
            Select Case Me.src
                Case Srcs.News
                    Return New DTONoticia(MyBase.Guid)
                Case Srcs.Eventos
                    Return New DTOEvento(MyBase.Guid)
            End Select
        End If
        Return retval
    End Function
End Class
