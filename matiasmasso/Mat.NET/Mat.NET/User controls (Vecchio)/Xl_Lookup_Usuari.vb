Public Class Xl_Lookup_Usuari
    Private _User As DTOUser

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property User As DTOUser
        Get
            Return _User
        End Get
        Set(value As DTOUser)
            _User = value
            If _User Is Nothing Then
                TextBox1.Text = ""
            Else
                TextBox1.Text = value.EmailAddress
            End If
        End Set
    End Property

    Public Sub Clear()
        _User = Nothing
        TextBox1.Clear()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim src As String = TextBox1.Text
        _User = BLL.BLLUser.GuessFromFirstLetters(src)
        If _User Is Nothing Then
            TextBox1.BackColor = COLOR_NOTOK
        Else
            Dim sEmailAddress As String = _User.EmailAddress
            If sEmailAddress = src Then
                TextBox1.BackColor = Color.LightBlue
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
            Else
                With TextBox1
                    .BackColor = Color.White
                    .Text = sEmailAddress
                    .SelectionStart = src.Length
                    .SelectionLength = sEmailAddress.Length - src.Length
                End With
            End If

        End If
    End Sub
End Class
