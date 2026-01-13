

Public Class Frm_Usr
    Private mUsr As Usr
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Usr, ByVal e As System.EventArgs)

    Public Sub New(ByVal oUsr As Usr)
        MyBase.new()
        Me.InitializeComponent()
        mUsr = oUsr
        Dim sLogin As String = oUsr.login
        TextBoxLogin.Text = sLogin
        TextBoxPwd.Text = oUsr.Password
        CheckBoxTel.Checked = oUsr.CommunicationsEnabled
        CheckBoxObsolet.Checked = oUsr.Disabled
        If oUsr.Exists Then
            ButtonDel.Enabled = oUsr.AllowDelete
        End If

        mAllowEvents = True
    End Sub



    Private Sub ControlChanged(ByVal sender As Usr, ByVal e As System.EventArgs) Handles _
        TextBoxLogin.TextChanged, _
         TextBoxPwd.TextChanged, _
          CheckBoxTel.CheckedChanged, _
           CheckBoxObsolet.CheckedChanged
        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Usr, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Usr, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With mUsr
            .login = TextBoxLogin.Text
            .Password = TextBoxPwd.Text
            .CommunicationsEnabled = CheckBoxTel.Checked
            .Disabled = CheckBoxObsolet.Checked
            .Update()
            RaiseEvent AfterUpdate(mUsr, System.EventArgs.Empty)
            Me.Close()
        End With
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Usr, ByVal e As System.EventArgs) Handles ButtonDel.Click
        If mUsr.allowDelete Then
            mUsr.delete()
        End If
    End Sub

    Private Sub ControlChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class