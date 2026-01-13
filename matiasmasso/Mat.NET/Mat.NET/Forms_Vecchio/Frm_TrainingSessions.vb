Public Class Frm_TrainingSessions

    Private Sub Frm_TrainingSessions_Load(sender As Object, e As EventArgs) Handles Me.Load
        Refresca()
    End Sub

    Private Sub Refresca()
        Dim oTrainingSessions As List(Of TrainingSession) = TrainingSessionsLoader.All
        Xl_TrainingSessions1.Load(oTrainingSessions)
    End Sub

    Private Sub Xl_TrainingSessions1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_TrainingSessions1.RequestToAddNew
        Dim oTrainingSession As New TrainingSession
        Dim oFrm As New Frm_TrainingSession(oTrainingSession)
        AddHandler oFrm.AfterUpdate, AddressOf Refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_TrainingSessions1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_TrainingSessions1.RequestToRefresh
        Refresca()
    End Sub
End Class