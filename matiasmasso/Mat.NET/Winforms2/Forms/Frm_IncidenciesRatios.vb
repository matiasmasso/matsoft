Public Class Frm_IncidenciesRatios
    Private _AllowEvents As Boolean

    Private Enum Intervals
        lastQ
        lastY
        YTD
        custom
    End Enum

    Private Async Sub Frm_IncidenciesRatios_Load(sender As Object, e As EventArgs) Handles Me.Load
        ComboBox1.SelectedIndex = Intervals.lastQ
        syncFchs()
        Await refresca()
        _AllowEvents = True
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim values = Await FEB.Incidencias.Ratios(exs, DateTimePicker1.Value, DateTimePicker2.Value)
        If exs.Count = 0 Then
            Xl_IncidenciesRatios1.Load(values)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub DateTimePicker1_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker1.ValueChanged
        If _AllowEvents Then
            ComboBox1.SelectedIndex = Intervals.custom
            Await refresca()
        End If
    End Sub

    Private Async Sub DateTimePicker2_ValueChanged(sender As Object, e As EventArgs) Handles DateTimePicker2.ValueChanged
        If _AllowEvents Then
            ComboBox1.SelectedIndex = Intervals.custom
            Await refresca()
        End If
    End Sub

    Private Sub syncFchs()
        Select Case ComboBox1.SelectedIndex
            Case Intervals.lastQ
                DateTimePicker1.Value = DTO.GlobalVariables.Today().AddMonths(-3)
                DateTimePicker2.Value = DTO.GlobalVariables.Today()
            Case Intervals.lastY
                DateTimePicker1.Value = DTO.GlobalVariables.Today().AddMonths(-12)
                DateTimePicker2.Value = DTO.GlobalVariables.Today()
            Case Intervals.YTD
                DateTimePicker1.Value = New Date(DTO.GlobalVariables.Today().Year, 1, 1)
                DateTimePicker2.Value = DTO.GlobalVariables.Today()

        End Select
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then syncFchs()
    End Sub
End Class