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
        BLL.BLLRaffleParticipant.Load(_RaffleParticipant)
        With _RaffleParticipant
            TextBoxFch.Text = Format(.Fch, "dd/MM/yy HH:mm")
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
        End With
        _AllowEvents = True
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

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest participant?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BLL.BLLRaffleParticipant.Delete(_RaffleParticipant, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar el participant")
            End If
        Else
            MsgBox("Operació cancel.lada per l'usuari", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _RaffleParticipant
            .Answer = ComboBoxAnswer.SelectedIndex - 1
            .Distribuidor = Xl_Contact2Distributor.Contact
        End With
        Dim exs As New List(Of exception)
        If BLL.BLLRaffleParticipant.Update(_RaffleParticipant, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_RaffleParticipant))
            Me.Close()
        Else
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


