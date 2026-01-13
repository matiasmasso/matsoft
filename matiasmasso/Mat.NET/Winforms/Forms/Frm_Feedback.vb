Public Class Frm_Feedback
    Private _Feedback As DTOFeedback
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOFeedback)
        MyBase.New()
        Me.InitializeComponent()
        _Feedback = value
    End Sub

    Private Sub Frm_Feedback_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If FEB2.Feedback.Load(exs, _Feedback) Then
            With _Feedback
                TextBoxSource.Text = .Source.Nom
                TextBoxFch.Text = .Fch.ToString("dd/MM/yy HH:mm:ss")
                If .UserOrCustomer Is Nothing Then
                    TextBoxUser.Text = "(anonim)"
                Else
                    TextBoxUser.Text = .UserOrCustomer.nom
                End If
                TextBoxScore.Text = .Score.ToString
                TextBoxComment.Text = .Comments
                ButtonOk.Enabled = .isNew
                ButtonDel.Enabled = Not .isNew
            End With
            _AllowEvents = True
        Else
            UIHelper.WarnError(exs)
            Me.Close()
        End If
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
        TextBoxComment.TextChanged
        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _Feedback
            .Comments = TextBoxComment.Text
        End With

        Dim exs As New List(Of Exception)
        UIHelper.ToggleProggressBar(PanelButtons, True)
        Dim oGuid = Await FEB2.Feedback.Update(exs, _Feedback)
        If exs.Count = 0 Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Feedback))
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
            If Await FEB2.Feedback.Delete(exs, _Feedback) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Feedback))
                Me.Close()
            Else
                UIHelper.ToggleProggressBar(PanelButtons, False)
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class


