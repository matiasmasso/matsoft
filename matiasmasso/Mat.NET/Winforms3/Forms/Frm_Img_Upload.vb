Public Class Frm_Img_Upload

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ButtonBrowse.Click
        Dim oDlg As New OpenFileDialog
        With oDlg
            If .ShowDialog Then
                TextBox1.Text = .FileName
            End If
        End With
    End Sub

    Private Sub ButtonUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonUpload.Click

    End Sub
End Class