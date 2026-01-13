Public Class LoginViewModel
    Property EmailAddress As String
    Property Password As String
    Property ReturnUrl As String
    Property Persist As Boolean

    Property Src As DTOUser.Sources

    Property Errs As List(Of String)

    Public Sub New()
        MyBase.New
        _Errs = New List(Of String)
    End Sub

End Class
