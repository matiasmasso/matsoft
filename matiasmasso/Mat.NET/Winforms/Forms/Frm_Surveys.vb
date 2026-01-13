Public Class Frm_Surveys

    Private _DefaultValue As DTOSurvey
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Private Sub Frm_Surveys_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub


    Private Sub Xl_Surveys1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Quizs1.RequestToAddNew
        Dim oSurvey As New DTOSurvey
        Dim oFrm As New Frm_Survey(oSurvey)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Surveys1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Quizs1.RequestToRefresh
        refresca()
    End Sub

    Private Sub refresca()
        Dim oSurveys As List(Of DTOSurvey) = BLL.BLLSurveys.Headers
        Xl_Quizs1.Load(oSurveys)
    End Sub
End Class