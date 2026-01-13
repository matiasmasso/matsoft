Public Class Frm_ForecastFollowUp
    Private _Brand As DTOProductBrand

    Public Sub New(oBrand As DTOProductBrand)
        MyBase.New
        InitializeComponent()
        _Brand = oBrand
    End Sub

    Private Async Sub Frm_ForecastFollowUp_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        Await refresca()
    End Sub

    Private Async Sub refresca(sender As Object, e As System.EventArgs)
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        ProgressBar1.Visible = True
        Dim oForecasts = Await FEB.Forecasts.All(Current.Session.Emp.Mgz, exs, Nothing, _Brand)
        If exs.Count = 0 Then
            Xl_ForecastFollowUp1.Load(oForecasts, DateTimePicker1.Value)
            ProgressBar1.Visible = False
        Else
            ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
    End Function
End Class