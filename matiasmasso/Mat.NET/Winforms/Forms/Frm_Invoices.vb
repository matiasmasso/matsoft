Public Class Frm_Invoices

    Private _Summary As List(Of DTOYearMonth)
    Private _Invoices As List(Of DTOInvoice)
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Invoices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Xl_ProgressBar1.ShowMarquee("Carregant dades...")
        Xl_ProgressBar1.Visible = True
        _Summary = Await FEB2.Invoices.Summary(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Await LoadYears()
            Xl_ProgressBar1.Visible = False
            ComboBoxSeries.SelectedIndex = ComboBoxSeries.Items.Count - 1
        Else
            Xl_ProgressBar1.Visible = False
            UIHelper.WarnError(exs)
        End If
        _AllowEvents = True
    End Sub

    Private Async Function LoadYears() As Task
        Xl_InvoicesSummaryYears1.Load(_Summary)
        Await LoadMonths()
    End Function

    Private Async Function LoadMonths() As Task
        Dim iYear As Integer = CurrentYear()
        Dim items As List(Of DTOYearMonth) = _Summary.Where(Function(x) x.year = iYear).ToList
        Xl_InvoicesSummaryMonths1.Load(items)
        Await LoadInvoices()
    End Function

    Private Async Function LoadInvoices() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBar1.ShowMarquee("Carregant factures...")
        Xl_ProgressBar1.Visible = True
        _Invoices = Await FEB2.Invoices.Headers(exs, Current.Session.Emp, CurrentMonth)
        Xl_ProgressBar1.Visible = False
        If exs.Count = 0 Then
            refrescaFilteredInvoices()
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Async Sub Xl_Invoices1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Invoices1.RequestToRefresh
        Await LoadInvoices()
    End Sub

    Private Sub refrescaFilteredInvoices()
        Dim oInvoices = _Invoices
        Select Case ComboBoxSeries.SelectedIndex
            Case 0
                oInvoices = _Invoices.Where(Function(x) x.Serie = DTOInvoice.Series.standard).ToList()
            Case 1
                oInvoices = _Invoices.Where(Function(x) x.Serie = DTOInvoice.Series.rectificativa).ToList()
            Case 2
                oInvoices = _Invoices.Where(Function(x) x.Serie = DTOInvoice.Series.simplificada).ToList()
        End Select
        Xl_Invoices1.Load(oInvoices, ,,, AddressOf ShowProgress)
    End Sub
    Private Function CurrentYear() As Integer
        Dim retval As Integer = Xl_InvoicesSummaryYears1.Year
        Return retval
    End Function

    Private Function CurrentMonth() As DTOYearMonth
        Dim iYear As Integer = CurrentYear()
        Dim iMonth As Integer = Xl_InvoicesSummaryMonths1.Month
        Dim retval As New DTOYearMonth(iYear, iMonth)
        Return retval
    End Function

    Private Async Sub Xl_InvoicesSummaryYears1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InvoicesSummaryYears1.ValueChanged
        If _AllowEvents Then
            Await LoadMonths()
        End If
    End Sub

    Private Async Sub Xl_InvoicesSummaryMonths1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_InvoicesSummaryMonths1.ValueChanged
        If _AllowEvents Then
            Await LoadInvoices()
        End If
    End Sub

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_Invoices1.Filter = e.Argument
    End Sub



    Public Sub ShowProgress(ByVal min As Integer, ByVal max As Integer, ByVal value As Integer, ByVal label As String, ByRef CancelRequest As Boolean)
        Xl_ProgressBar1.ShowProgress(min, max, value, label, CancelRequest)
    End Sub

    Private Async Sub ExcelToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExcelToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Current.Session.Emp, CurrentYear)
        Dim oSheet = Await FEB2.Invoices.LlibreDeFactures(exs, oExercici)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
            ' UIHelper.SaveExcelDialog(oExcel, AddressOf ShowProgress, "Desar Llibre Factures Emeses " & oExercici.Year)
            ' Xl_ProgressBar1.ShowEnd("Llibre de Factures Emeses desat")
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub Xl_InvoicesSummaryMonths1_RequestToGenerateIntrastat(sender As Object, e As MatEventArgs) Handles Xl_InvoicesSummaryMonths1.RequestToGenerateIntrastat
        Dim oFrm As New Frm_IntrastatFactory(CurrentMonth, DTOIntrastat.Flujos.Expedicion)
        oFrm.Show()
    End Sub

    Private Sub ComboBoxSeries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSeries.SelectedIndexChanged
        If _AllowEvents Then
            refrescaFilteredInvoices()
        End If
    End Sub


End Class