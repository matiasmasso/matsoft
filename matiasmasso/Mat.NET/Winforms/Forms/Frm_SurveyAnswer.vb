Public Class Frm_SurveyAnswer
    Private _QuizAnswer As DTOSurveyAnswer
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Sub New(value As DTOSurveyAnswer)
        MyBase.New()
        Me.InitializeComponent()
        _QuizAnswer = value
        BLL.BLLSurveyAnswer.Load(_QuizAnswer)
    End Sub

    Private Sub Frm_Properties_Load(sender As Object, e As EventArgs) Handles Me.Load
        With _QuizAnswer
            TextBoxText.Text = .Text
            NumericUpDown1.Value = .Value

            ButtonOk.Enabled = .IsNew
            ButtonDel.Enabled = Not .IsNew
        End With
        _AllowEvents = True
    End Sub

    Private Sub Control_Changed(sender As Object, e As EventArgs) Handles _
            TextBoxText.TextChanged,
             NumericUpDown1.ValueChanged

        If _AllowEvents Then
            ButtonOk.Enabled = True
        End If
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        With _QuizAnswer
            .Text = TextBoxText.Text
            .Value = NumericUpDown1.Value
        End With

        Dim exs As New List(Of Exception)
        If BLL.BLLSurveyAnswer.Update(_QuizAnswer, exs) Then
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_QuizAnswer))
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
            If BLL.BLLSurveyAnswer.Delete(_QuizAnswer, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_QuizAnswer))
                Me.Close()
            Else
                UIHelper.WarnError(exs, "error al eliminar")
            End If
        End If
    End Sub
End Class