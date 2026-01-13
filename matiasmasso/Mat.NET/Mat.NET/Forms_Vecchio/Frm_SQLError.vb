Public Class Frm_SQLError

    Private Sub Frm_SQLError_Load(sender As Object, e As EventArgs) Handles Me.Load
        TextBox1.Text = BLL_App.ServerName
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If RadioButtonRemote.Checked Then
            BLL_App.ServerName = TextBox1.Text
        Else
            BLL_App.ServerName = "LocalHost"
        End If


        Dim exs As New List(Of Exception)
        If BLL_App.Initialize(DTOEmp.Ids.MatiasMasso, DTOSession.AppTypes.MatNet, BLL_App.Current.Lang.Id, DTOCur.Ids.ESP, exs) Then
            Dim oFrm As New Frm__Idx
            oFrm.Show()
            Me.Close()
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