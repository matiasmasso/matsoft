Public Class Frm_Inem_CompresProductRanking
    Private _allowEvents As Boolean
    Private Async Sub Frm_Inem_CompresProductRanking_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim oYearMonth = DTOYearMonth.FromFch(Today.AddMonths(-1))
        Xl_YearMonth1.YearMonth = oYearMonth
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oYearMonth As DTOYearMonth = Xl_YearMonth1.YearMonth
        Dim items = Await FEB2.InemCompresProductRanking.All(exs, GlobalVariables.Emp, oYearMonth)
        If exs.Count = 0 Then
            Xl_Inem_CompresProductRanking1.Load(items)
            _allowEvents = True
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_YearMonth1_AfterUpdate(sender As Object, e As EventArgs) Handles Xl_YearMonth1.AfterUpdate
        If _allowEvents Then
            Await refresca()
        End If
    End Sub
End Class