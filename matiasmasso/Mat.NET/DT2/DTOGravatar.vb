Public Class DTOGravatar
    Private _DefaultImageCod As String = "mm"
    Property EmailAddress As String


    Shared Function Factory(emailAddress As String) As DTOGravatar
        Dim retval As New DTOGravatar
        With retval
            .EmailAddress = emailAddress
        End With
        Return retval
    End Function





End Class
