Public Class DTOBloggerPost
    Inherits DTOBaseGuid

    Property Blogger As DTOBlogger
    Property Title As String
    Property Url As String
    Property Fch As Date
    Property Lang As DTOLang
    Property HighlightFrom As Date
    Property HighlightTo As Date
    Property Products As List(Of DTOProduct)

    Public Sub New()
        MyBase.New()
        _Products = New List(Of DTOProduct)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Products = New List(Of DTOProduct)
    End Sub

    Shared Function Factory(Optional oBlogger As DTOBlogger = Nothing) As DTOBloggerPost
        Dim retval As New DTOBloggerPost
        With retval
            .Blogger = oBlogger
            .Fch = DateTime.Today
        End With
        Return retval
    End Function

    Shared Function FullUrl(oPost As DTOBloggerPost) As String
        Dim retval As String = oPost.Url
        If Not retval.StartsWith("http") Then
            retval = "https://" & retval
        End If
        Return retval
    End Function
End Class
