Public Class Frm_Ean_ECI

    Private Sub TextBox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
    TextBoxDepto.TextChanged, _
    TextBoxFamilia.TextChanged, _
     TextBoxBarra.TextChanged

        If TextBoxDepto.Text.Length = 3 And TextBoxFamilia.Text.Length = 3 And TextBoxBarra.Text.Length = 5 Then
            Dim sSource As String = "2" & TextBoxDepto.Text & TextBoxFamilia.Text & TextBoxBarra.Text
            Dim oEan13 As New maxisrvr.ean13(sSource)
            With oEan13
                .BackgroundColor = Color.White
                .DrawDigits = False
                .Height = 20
                PictureBox1.Image = .Bitmap(False)
            End With
            ButtonCopy.Enabled = True
        Else
            PictureBox1.Image = Nothing
            ButtonCopy.Enabled = False
        End If
    End Sub

    Private Sub ButtonCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCopy.Click
        Clipboard.SetImage(PictureBox1.Image)
    End Sub
End Class