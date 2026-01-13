Public Class Frm_SurveyParticipant

    Private _SurveyParticipant As DTOSurveyParticipant
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSurveyParticipant)
        MyBase.New()
        Me.InitializeComponent()
        _SurveyParticipant = value
        BLL.BLLSurveyParticipant.Load(_SurveyParticipant)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _SurveyParticipant
            TextBoxId.Text = IdText()
            Xl_SurveyTree1.Load(_SurveyParticipant)
            TextBoxObs.Text = .Obs

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Function IdText() As String
        Dim sb As New Text.StringBuilder
        sb.AppendLine(_SurveyParticipant.Survey.Title)
        sb.AppendLine(Format(_SurveyParticipant.Fch, "dd/MM/yy HH:mm:ss"))
        sb.AppendLine(DTOUser.AddressAndNickname(_SurveyParticipant.User))
        sb.AppendLine(String.Format("Volum anual de comandes: {0:c}", _SurveyParticipant.Eur.Eur))
        Dim oContacts As List(Of DTOContact) = BLLUser.Contacts(_SurveyParticipant.User)
        For Each oContact As DTOContact In oContacts
            sb.AppendLine(oContact.FullNom)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function


    Private Sub Control_Changed(sender As Object, e As EventArgs)
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _SurveyParticipant
            '.Nom = TextBox1.Text
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLSurveyParticipant.Update(_SurveyParticipant, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_SurveyParticipant))
            Me.Close()
        Else
            UIHelper.WarnError(exs, "error al desar")
        End If
    End Sub

    Private Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        Me.Close()
    End Sub

    Private Sub ButtonDel_Click(sender As Object, e As EventArgs) Handles ButtonDel.Click
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("ho eliminem?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If BLL.BLLSurveyParticipant.Delete(_SurveyParticipant, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_SurveyParticipant))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


