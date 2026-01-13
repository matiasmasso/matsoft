Public Class Xl_SearchBox
    Inherits TextBox

    Private Sub Xl_SearchBox_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Enter, Keys.Tab
                Procesa()
        End Select
    End Sub

    Private Function Procesa()

    End Function
End Class
