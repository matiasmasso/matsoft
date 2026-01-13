Public Class Frm_Emp
    Private _Emp As DTOEmp
    Private mAllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEmp As DTOEmp)
        MyBase.New()
        Me.InitializeComponent()
        _Emp = oEmp
    End Sub

    Private Async Sub Frm_Emp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await Refresca()
    End Sub

    Private Async Function Refresca() As Task
        Dim exs As New List(Of Exception)
        If FEB2.Emp.Load(_Emp, exs) Then
            With _Emp
                TextBoxNom.Text = .Nom
                TextBoxAlias.Text = .Abr
                Await Xl_Contact2_Mgz.Load(.Mgz)
                Xl_ContactOrg.Emp = _Emp
                Await Xl_ContactOrg.Load(.Org)
                TextBoxMsgFrom.Text = .MsgFrom
                TextBoxSmtp.Text = .MailBoxSmtp
                TextBoxUser.Text = .MailboxUsr
                TextBoxPwd.Text = .MailboxPwd
                TextBoxPort.Text = .MailBoxPort
            End With
            mAllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Function

    Private Sub ControlChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        TextBoxNom.TextChanged,
         TextBoxAlias.TextChanged,
          Xl_Contact2_Mgz.AfterUpdate,
          Xl_ContactOrg.AfterUpdate,
           TextBoxMsgFrom.TextChanged,
            TextBoxSmtp.TextChanged,
             TextBoxUser.TextChanged,
              TextBoxPwd.TextChanged,
               TextBoxPort.TextChanged

        If mAllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        UIHelper.ToggleProggressBar(Panel1, True)
        With _Emp
            .Nom = TextBoxNom.Text
            .Abr = TextBoxAlias.Text
            .Mgz = DTOMgz.FromContact(Xl_Contact2_Mgz.Contact)
            .Org = Xl_ContactOrg.Contact

            .MsgFrom = TextBoxMsgFrom.Text
            .MailboxUsr = TextBoxUser.Text
            .MailboxPwd = TextBoxPwd.Text
            .MailBoxSmtp = TextBoxSmtp.Text
            .MailBoxPort = TextBoxPort.Text

        End With

        Dim exs As New List(Of Exception)
        If Await FEB2.Emp.Update(_Emp, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Emp))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar la empresa")
        End If
    End Sub


End Class