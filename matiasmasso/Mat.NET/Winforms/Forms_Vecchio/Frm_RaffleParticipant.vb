Public Class Frm_RaffleParticipant

    Private _RaffleParticipant As DTORaffleParticipant
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTORaffleParticipant)
        MyBase.New()
        Me.InitializeComponent()
        _RaffleParticipant = value
    End Sub

    Private Sub Frm_RaffleParticipant_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.RaffleParticipant.Load(exs, _RaffleParticipant) Then
            With _RaffleParticipant
                TextBoxFch.Text = Format(.Fch, "dd/MM/yy HH:mm")
                TextBoxNum.Text = Format(.Num, "0000")
                TextBoxRaffle.Text = .Raffle.Title & " (" & .Raffle.FchFrom.ToShortDateString & ")"
                TextBoxUsuari.Text = .User.EmailAddress
                If .Raffle.Answers IsNot Nothing Then
                    ComboBoxAnswer.Items.Clear()
                    ComboBoxAnswer.Items.Add("(sense resposta)")
                    For Each sAnswer As String In .Raffle.Answers
                        ComboBoxAnswer.Items.Add(sAnswer)
                    Next
                    ComboBoxAnswer.SelectedIndex = .Answer + 1
                    SetAnswerIcon()
                End If
                Xl_Contact2Distributor.Contact = .Distribuidor
                CheckBoxWinner.Checked = _RaffleParticipant.Equals(.Raffle.Winner)
                If .Suplente > 0 Then
                    TextBoxSuplent.Text = .Suplente
                End If
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadAnswers()
        Dim oAnswers As List(Of String) = _RaffleParticipant.Raffle.Answers
        oAnswers.Insert(0, "(sense resposta)")
        With ComboBoxAnswer
            .DataSource = oAnswers
        End With
    End Sub

    Private Sub ButtonClose_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

    Private Sub ButtonRaffle_Click(sender As Object, e As EventArgs) Handles ButtonRaffle.Click
        Dim oFrm As New Frm_Raffle(_RaffleParticipant.Raffle)
        oFrm.Show()
    End Sub

    Private Sub ButtonUsuari_Click(sender As Object, e As EventArgs) Handles ButtonUsuari.Click
        Dim oFrm As New Frm_Usuari(_RaffleParticipant.User)
        oFrm.Show()
    End Sub


    Private Sub SetAnswerIcon()
        Dim iRightAnswer As Integer = _RaffleParticipant.Raffle.RightAnswer
        If ComboBoxAnswer.SelectedIndex = iRightAnswer Then
            PictureBoxRightAnswer.Image = My.Resources.Ok
        Else
            PictureBoxRightAnswer.Image = My.Resources.aspa
        End If

    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Async Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest participant?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.RaffleParticipant.Delete(exs, _RaffleParticipant) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el participant")
            End If
        Else
            MsgBox("Operació cancel.lada per l'usuari", MsgBoxStyle.Information)
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _RaffleParticipant
            .Answer = ComboBoxAnswer.SelectedIndex - 1
            .Distribuidor = Xl_Contact2Distributor.Contact
        End With
        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(Panel1, True)
        If Await FEB2.RaffleParticipant.Update(exs, _RaffleParticipant) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RaffleParticipant))
            Me.Close()
        Else
            UIHelper.ToggleProggressBar(Panel1, False)
            UIHelper.WarnError(exs, "error al desar el participant")
        End If
    End Sub


    Private Sub ComboBoxAnswer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxAnswer.SelectedIndexChanged
        If _AllowEvents Then
            SetAnswerIcon()
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub Xl_Contact2Distributor_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_Contact2Distributor.AfterUpdate
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub
End Class


