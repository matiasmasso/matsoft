Public Class DTODept
    Inherits DTOProduct

    Private _LangNom As DTOLangText

    Shadows Property Brand As DTOProductBrand
    Property Ord As Integer
    Property CNaps As List(Of DTOCnap)
    <JsonIgnore> Property Banner As Image
    Shared Property width As Integer = 318
    Shared Property height As Integer = 212

    Public Sub New()
        MyBase.New
        MyBase.SourceCod = SourceCods.Dept
        _CNaps = New List(Of DTOCnap)
        _LangNom = New DTOLangText
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        MyBase.SourceCod = SourceCods.Dept
        _CNaps = New List(Of DTOCnap)
        _LangNom = New DTOLangText
    End Sub

    Shadows Property LangNom As DTOLangText
        Get
            Return _LangNom
        End Get
        Set(value As DTOLangText)
            _LangNom = value
            MyBase.Nom = _LangNom.Esp
        End Set
    End Property

    Shared Shadows Function Factory(oBrand As DTOProductBrand) As DTODept
        Dim retval As New DTODept
        retval.Brand = oBrand
        Return retval
    End Function

    Public Function getUrl(Optional oTab As DTOProduct.Tabs = DTOProduct.Tabs.general, Optional AbsoluteUrl As Boolean = False) As String
        Dim sSegment = MatHelperStd.UrlHelper.EncodedUrlSegment(MyBase.nom)

        Dim retval = MmoUrl.Factory(AbsoluteUrl, DTOProductBrand.UrlSegment(Me.Brand), sSegment)

        If oTab <> DTOProduct.Tabs.general Then
            retval = retval & "/" & oTab.ToString
        End If

        Return retval
    End Function
End Class
