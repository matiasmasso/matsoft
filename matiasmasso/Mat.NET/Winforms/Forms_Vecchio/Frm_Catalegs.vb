

Public Class Frm_Catalegs
    Private mAllowEvents As Boolean = False

    Private Sub Frm_Catalegs_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        TextBoxRootUrl.Text = BLL.Defaults.GetRootUrl(True)
        TextBoxUrl.Text = BLL.BLLDefault.EmpValue(DTODefault.Codis.HiResUrl)
        TextBoxPath.Text = maxisrvr.ServerWeb.PathToDownloads
        mAllowEvents = True
    End Sub

    Private Sub TextBoxUrl_TextChanged(sender As Object, e As System.EventArgs) Handles _
        TextBoxUrl.TextChanged, _
         TextBoxPath.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As System.EventArgs) Handles ButtonOk.Click
        BLL.BLLDefault.SetEmpValue(DTODefault.Codis.HiResUrl, TextBoxUrl.Text)
        BLL.BLLDefault.SetEmpValue(DTODefault.Codis.HiResPath, TextBoxPath.Text)
        Me.Close()
    End Sub

    Private Sub ButtonLookupPath_Click(sender As Object, e As System.EventArgs) Handles ButtonLookupPath.Click
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .Description = "seleccionar la ruta arrel de les imatges en alta resolució"
            '.RootFolder = Environment.SpecialFolder.MyDocuments
            If TextBoxPath.Text > "" Then
                .SelectedPath = TextBoxPath.Text
            End If
            If .ShowDialog = vbOK Then
                TextBoxPath.Text = .SelectedPath
                ButtonOk.Enabled = True
            End If
        End With
    End Sub

    Private Sub TextBoxUrl_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TextBoxUrl.Validating
        Dim s As String = TextBoxUrl.Text
        If s.Length > 0 Then
            Dim sLastChar As String = s.Substring(s.Length - 1)
            If sLastChar <> "/" Then
                TextBoxUrl.Text += "/"
            End If
        End If
    End Sub
End Class