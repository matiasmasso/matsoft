
Imports Newtonsoft.Json
Imports SixLabors.ImageSharp

Public Class DTOBanner
    Inherits DTOBaseGuid

    Property Nom As String
    Property SrcGuid As Guid
    Property SrcType As Srcs
    Property FchFrom As Date
    Property FchTo As Date
    Property NavigateTo As String '<AllowHtml>
    Property ImageUrl As String
    Property Product As DTOProduct
    Property Lang As DTOLang
    <JsonIgnore> Public Property Image As Image
    <JsonIgnore> Public Property Thumbnail As Image

    Public Const BANNERWIDTH As Integer = 640
    Public Const BANNERHEIGHT As Integer = 292
    Public Const THUMBWIDTH As Integer = 105
    Public Const THUMBHEIGHT As Integer = 48

    Public Enum Srcs
        notSet
        product
        news
    End Enum

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


End Class

