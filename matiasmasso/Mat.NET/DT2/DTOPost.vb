Public Class DTOPost
    Inherits DTOBaseGuid
    Property Title As String
    Property Excerpt As String
    Property Url As String
    Property ThumbnailUrl As String


    Public Sub New()
        MyBase.New
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub


End Class
