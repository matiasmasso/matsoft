Public Class Frm_SurveyTree
    Private _Survey As DTOSurvey
    'Private _Participants As List(Of DTOSurveyParticipant)

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Private Enum Tabs
        Diseny
        Participants
    End Enum

    Public Sub New(oSurvey As DTOSurvey)
        MyBase.New
        InitializeComponent()
        _Survey = oSurvey

        refresca()

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        Select Case TabControl1.SelectedIndex
            Case Tabs.Participants
                Static ParticipantsDone As Boolean
                If Not ParticipantsDone Then
                    refresca()

                    ParticipantsDone = True
                End If
        End Select
    End Sub

    Private Sub Xl_TextboxSearchParticipants_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearchParticipants.AfterUpdate
        Xl_SurveyParticipants1.Filter = e.Argument
    End Sub

    Private Sub Xl_SurveyParticipants1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_SurveyParticipants1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oParticipants As List(Of DTOSurveyParticipant) = BLLSurveyParticipants.Headers(_Survey)
        Dim Actius As Integer = oParticipants.Where(Function(x) x.Fch > Date.MinValue).Count
        ToolStripStatusLabelParticipacio.Text = String.Format("participació {0:N0}% ({1:N0}/{2:N0})", 100 * Actius / oParticipants.Count, Actius, oParticipants.Count)

        Dim oScores As List(Of DTOSurveyAnswer) = BLLSurveyAnswers.Scores(_Survey)
        Dim iPreguntesContestades As Integer = oScores.Sum(Function(x) x.ParticipantsCount)
        Dim iScores As Integer = oScores.Sum(Function(x) x.Value * x.ParticipantsCount)
        ToolStripStatusLabelValoracio.Text = String.Format("valoració {0:N0}% ({1:N0}/{2:N0})", 100 * iScores / (iPreguntesContestades * 5), iScores, iPreguntesContestades * 5)

        BLLSurvey.Load(_Survey)
        Xl_QuizTree1.Load(_Survey, oScores)

        Xl_SurveyParticipants1.Load(oParticipants)
    End Sub

    Private Sub ToolStripSplitButton1_ButtonClick(sender As Object, e As EventArgs) Handles ToolStripSplitButton1.ButtonClick
        refresca()
    End Sub
End Class