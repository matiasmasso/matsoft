Public Class Frm_MonthCalendar
    Public Event AfterUpdate(sender As Object, e As MatEventArgs)
    Public Sub New(currentFch As Date, availableFchs As List(Of Date))
        InitializeComponent()
        With MonthCalendar1
            .SetDate(IIf(currentFch = Nothing, DTO.GlobalVariables.Today(), currentFch))
            For Each item In availableFchs
                .AddBoldedDate(item)
            Next
            .SetSelectionRange(availableFchs.Last, availableFchs.First)
        End With
        Me.Text = ""
        Me.ButtonOk.Enabled = True
    End Sub

    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        RaiseEvent AfterUpdate(Me, New MatEventArgs(MonthCalendar1.SelectionRange))
        Me.Close()
    End Sub
End Class