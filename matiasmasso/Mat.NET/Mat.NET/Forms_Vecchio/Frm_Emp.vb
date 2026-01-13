Public Class Frm_Emp
    Private _Emp As DTOEmp
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEmp As DTOEmp)
        MyBase.New()
        Me.InitializeComponent()
        _Emp = oEmp
        BLL.BLLEmp.Load(_Emp)
        Refresca()
        mAllowEvents = True
    End Sub

    Private Sub Refresca()
        With _Emp
            TextBoxNom.Text = .Nom
            TextBoxAlias.Text = .Abr
            Xl_Contact2_Mgz.Contact = .Mgz
            TextBoxMsgFrom.Text = .MsgFrom
            TextBoxSmtp.Text = .MailBoxSmtp
            TextBoxUser.Text = .MailboxUsr
            TextBoxPwd.Text = .MailboxPwd
            TextBoxPort.Text = .MailBoxPort
        End With
    End Sub

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged, _
         TextBoxAlias.TextChanged, _
          Xl_Contact2_Mgz.AfterUpdate, _
           TextBoxMsgFrom.TextChanged, _
            TextBoxSmtp.TextChanged, _
             TextBoxUser.TextChanged, _
              TextBoxPwd.TextChanged, _
               TextBoxPort.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _Emp
            .Nom = TextBoxNom.Text
            .Abr = TextBoxAlias.Text
            .Mgz = Xl_Contact2_Mgz.Contact

            .MsgFrom = TextBoxMsgFrom.Text
            .MailboxUsr = TextBoxUser.Text
            .MailboxPwd = TextBoxPwd.Text
            .MailBoxSmtp = TextBoxSmtp.Text
            .MailBoxPort = TextBoxPort.Text

        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLEmp.Update(_Emp, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Emp))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar la empresa")
        End If
    End Sub

End Class