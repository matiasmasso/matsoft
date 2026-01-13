Public Class DTOBlogger
    Inherits DTOBaseGuid

    Property Title As String
    Property Author As DTOUser
    Property Url As String
    <JsonIgnore> Property Logo As Image '150x115
    Property Posts As List(Of DTOBloggerPost)
    Property Obsoleto As Boolean

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Public Function LogoSegmentUrl() As String
        Return String.Format("Bloggers/logo", MyBase.Guid.ToString())
    End Function
End Class
