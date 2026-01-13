Public Class Frm_Consumer
    Private _User As DTOUser
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOUser)
        MyBase.New()
        Me.InitializeComponent()
        _User = value
    End Sub

    Private Sub Frm_User_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.User.Load(exs, _User) Then
            With _User
                TextBoxEmailAddress.Text = .emailAddress
                TextBoxNom.Text = .nom
                TextBoxCognoms.Text = .cognoms
                TextBoxTel.Text = .tel
                LoadAddress()
                TextBoxObs.Text = .obs
                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub LoadAddress()
        Dim retval As New DTOAddress
        retval.text = _User.adr

    End Sub


    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNom.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _User
            .nom = TextBoxNom.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        If Await FEB2.User.Update(exs, _User) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(PanelButtons, False)
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            UIHelper.ToggleProggressBar(PanelButtons, True)
            If Await FEB2.User.Delete(exs, _User) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_User))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class