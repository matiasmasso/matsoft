Public Class Xl_DUN14
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property Value As String
        Get
            Return TextBox1.Text
        End Get
        Set(src As String)
            TextBox1.Text = src
        End Set
    End Property

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Select Case e.KeyChar
            Case Chr(8), Chr(13)
                'backspace, carriage return
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            Case Else
                e.Handled = True
        End Select
    End Sub



    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim src As String = TextBox1.Text
        Select Case src.Length
            Case 0
                PictureBox1.Image = Nothing
                TextBox1.BackColor = Color.White
            Case 13, 14
                TextBox1.BackColor = Color.AliceBlue
                'PictureBox1.Image = BLLBarCode.ITF14(src, PictureBox1.Width, PictureBox1.Height, IncludeLabel:=False)
                RaiseEvent AfterUpdate(Me, New MatEventArgs(TextBox1.Text))
            Case Else
                TextBox1.BackColor = Color.Yellow
                PictureBox1.Image = Nothing
        End Select

    End Sub

End Class
