Public Class Frm_SQLError

    Private Sub Frm_SQLError_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = BLLApp.SqlServerName
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If RadioButtonRemote.Checked Then
            BLLApp.SqlServerName = TextBox1.Text
        Else
            BLLApp.SqlServerName = "LocalHost"
        End If

        Dim exs As New List(Of Exception)
        If BLL.BLLApp.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.MatNet, DTOLang.Ids.CAT, DTOCur.Ids.EUR, exs) Then

            If Session.Initialize Then
                Dim oFrm As New Frm__Idx
                oFrm.Show()
                Me.Close()
            Else
                Dim oFrm As New Frm_Login
                oFrm.ShowDialog()
            End If

        Else
            UIHelper.WarnError(exs, "imposible iniciar la aplicació")
            Application.Exit()
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        System.Windows.Forms.Application.Exit()
    End Sub

    Private Sub RadioButtonLocal_CheckedChanged(sender As Object, e As EventArgs) Handles _
        RadioButtonLocal.CheckedChanged, _
         TextBox1.TextChanged

        ButtonOk.Enabled = True
    End Sub
End Class