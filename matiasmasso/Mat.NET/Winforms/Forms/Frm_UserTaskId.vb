Public Class Frm_UserTaskId

    Private _UserTaskId As DTOUserTaskId
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOUserTaskId)
        MyBase.New()
        Me.InitializeComponent()
        _UserTaskId = value
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.UserTaskId.Load(exs, _UserTaskId) Then
            With _UserTaskId
                UIHelper.LoadComboFromEnum(ComboBoxId, GetType(DTOUserTaskId.Ids), "(sel.leccionar codi)", .Id)
                TextBoxNomEsp.Text = .Nom.Esp
                TextBoxNomCat.Text = .Nom.Cat
                TextBoxNomEng.Text = .Nom.Eng
                TextBoxNomPor.Text = .Nom.Por

                If .CompleteMode = DTOUserTaskId.CompleteModes.EachUserCompletesHisOwnTask Then
                    RadioButtonEach.Checked = True
                Else
                    RadioButtonAll.Checked = True
                End If

                Xl_Users1.Load(.Subscriptors)

                ButtonOk.Enabled = .IsNew
                ButtonDel.Enabled = Not .IsNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxNomEsp.TextChanged,
        TextBoxNomCat.TextChanged,
        TextBoxNomEng.TextChanged,
        TextBoxNomPor.TextChanged,
        RadioButtonEach.CheckedChanged,
         RadioButtonAll.CheckedChanged,
         Xl_Users1.RowsRemoved

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _UserTaskId
            .Nom.Esp = TextBoxNomEsp.Text
            .Nom.Cat = TextBoxNomCat.Text
            .Nom.Eng = TextBoxNomCat.Text
            .Nom.Por = TextBoxNomCat.Text
            .CompleteMode = IIf(RadioButtonEach.Checked, DTOUserTaskId.CompleteModes.EachUserCompletesHisOwnTask, DTOUserTaskId.CompleteModes.OneUserCompletesAllUsersTask)
            .Subscriptors = Xl_Users1.Values
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.UserTaskId.Update(exs, _UserTaskId) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_UserTaskId))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
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
            If Await FEB2.UserTaskId.Delete(exs, _UserTaskId) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_UserTaskId))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub

    Private Sub TextBoxEmail_TextChanged(sender As Object, e As EventArgs) Handles TextBoxEmail.TextChanged
        ButtonAdd.Enabled = TextBoxEmail.Text.Length > 0
    End Sub

    Private Async Sub ButtonAdd_Click(sender As Object, e As EventArgs) Handles ButtonAdd.Click
        Dim exs As New List(Of Exception)
        Dim sEmailAdr As String = TextBoxEmail.Text
        Dim oUser = Await FEB2.User.FromEmail(exs, Current.Session.Emp, sEmailAdr)
        If exs.Count = 0 Then
            If oUser Is Nothing Then
                Dim sMsg As String = String.Format("No s'ha trobat cap email per'{0}'", sEmailAdr)
                UIHelper.WarnError(sMsg)
            Else
                Dim oUsers As List(Of DTOUser) = Xl_Users1.Values
                oUsers.Add(oUser)
                Xl_Users1.Load(oUsers)
                TextBoxEmail.Clear()
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub


End Class


