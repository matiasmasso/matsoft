Public Class Frm_CliCreditLimit
    Private _CliCreditLog As DTOCliCreditLog = Nothing
    Private _AllowEvents As Boolean = False

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oCliCreditLog As DTOCliCreditLog)
        MyBase.New()
        Me.InitializeComponent()

        _CliCreditLog = oCliCreditLog
        BLL.BLLCliCreditLog.Load(_CliCreditLog)

        With _CliCreditLog
            If .IsNew Then
                Me.Text = "Nou limit de credit per " & oCliCreditLog.Customer.FullNom
            Else
                Me.Text = _CliCreditLog.Customer.FullNom
                Dim s As String = "Creat per " & BLL.BLLUser.NicknameOrElse(.UsrCreated) & " el " & .FchCreated.ToString
                If .FchCreated <> .FchLastEdited Then
                    s = s & " Modificat per " & BLL.BLLUser.NicknameOrElse(.UsrLastEdited) & " el " & .FchLastEdited.ToString
                End If
                TextBoxUsrLog.Text = s
                ButtonDel.Enabled = True
            End If

            'Xl_Contact_Logo1.Contact = .Client
            Xl_Amt1.Value = .Amt
            TextBoxObs.Text = .Obs
            _AllowEvents = True
        End With
    End Sub


    Private Sub Xl_Amt1_AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Xl_Amt1.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub TextBoxObs_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBoxObs.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonOk.Click
        With _CliCreditLog
            .Amt = Xl_Amt1.Value
            .Obs = TextBoxObs.Text
            .UsrLastEdited = BLL.BLLSession.Current.User
            If .UsrCreated Is Nothing Then .UsrCreated = .UsrLastEdited
            .FchLastEdited = Now
            If .FchCreated = Nothing Then .FchCreated = .FchLastEdited
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLCliCreditLog.Update(_CliCreditLog, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliCreditLog))
            Me.Close()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub ButtonDel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("retrocedim al limit anterior?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = vbOK Then
            Dim exs As New List(Of Exception)
            If BLL.BLLCliCreditLog.Delete(_CliCreditLog, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_CliCreditLog))
                Me.Close()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub ButtonCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub
End Class