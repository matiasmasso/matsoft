Public Class Frm_BriqExams

    Private Sub Frm_BriqExams_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim oExams As BriqExams = BriqExamsLoader.All
        Xl_BriqExams1.Load(oExams)
    End Sub

    Private Sub Xl_BriqExams1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_BriqExams1.RequestToRefresh
        refresca()
    End Sub
End Class