Imports System.IO
Imports System.IO.Compression

Public Class Frm_Invoices

    Private _Summary As List(Of DTOYearMonth)
    Private _Invoices As List(Of DTOInvoice)
    Private _AllowEvents As Boolean

    Private Async Sub Frm_Invoices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Xl_ProgressBar1.ShowMarquee("Carregant dades...")
        Xl_ProgressBar1.Visible = True
        _Summary = Await FEB.Invoices.Summary(exs, Current.Session.Emp)
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
        Dim items As List(Of DTOYearMonth) = _Summary.Where(Function(x) x.Year = iYear).ToList
        Xl_InvoicesSummaryMonths1.Load(items)
        Await LoadInvoices()
    End Function

    Private Async Function LoadInvoices() As Task
        Dim exs As New List(Of Exception)
        Xl_ProgressBar1.ShowMarquee("Carregant factures...")
        Xl_ProgressBar1.Visible = True
        _Invoices = Await FEB.Invoices.Headers(exs, Current.Session.Emp, CurrentMonth)
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
        Dim oSheet = Await FEB.Invoices.LlibreDeFactures(exs, oExercici)
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
        Dim oFrm As New Frm_IntrastatFactory(CurrentMonth, DTOIntrastat.Flujos.expedicion)
        oFrm.Show()
    End Sub

    Private Sub ComboBoxSeries_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxSeries.SelectedIndexChanged
        If _AllowEvents Then
            refrescaFilteredInvoices()
        End If
    End Sub

    Private Sub Xl_Invoices1_RequestToToggleProgressBar(sender As Object, e As MatEventArgs) Handles Xl_Invoices1.RequestToToggleProgressBar
        Dim visible As Boolean = e.Argument
        Xl_ProgressBar1.Visible = visible
    End Sub

    Private Async Sub TotesLesFacturesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TotesLesFacturesToolStripMenuItem.Click
        Dim exs As New List(Of Exception)
        Dim CancelRequest As Boolean
        Dim oDlg As New FolderBrowserDialog
        With oDlg
            .Description = "Seleccionar una carpeta per desar les factures"
            .ShowNewFolderButton = True
            If .ShowDialog Then

                Dim zipFilename = String.Format("{0}\factures emeses {1} {2:00} {3}.zip", .SelectedPath, CurrentMonth.Year, CurrentMonth.MonthNum, DTOLang.CAT().MesAbr(CurrentMonth.MonthNum))
                Using zipToOpen As FileStream = New FileStream(zipFilename, FileMode.Create)
                    Using archive As ZipArchive = New ZipArchive(zipToOpen, ZipArchiveMode.Update)

                        For Each oInvoice In _Invoices
                            Dim label As String = String.Format("desant factura {0} del {1:dd/MM/yyyy}", oInvoice.NumeroYSerie, oInvoice.Fch)
                            Xl_ProgressBar1.ShowProgress(1, _Invoices.Count, _Invoices.IndexOf(oInvoice) + 1, label, CancelRequest)
                            If CancelRequest Then Exit For

                            Dim oByteArray = Await FEB.Invoice.Pdf(exs, oInvoice)
                            If exs.Count = 0 Then
                                Dim filename = String.Format("factura {0}.{1}.pdf", CurrentMonth.Year, oInvoice.NumeroYSerie)
                                Dim entry As ZipArchiveEntry = archive.CreateEntry(filename)
                                Dim ms As New System.IO.MemoryStream(oByteArray)
                                Using entryStream = entry.Open()
                                    ms.CopyTo(entryStream)
                                End Using
                            Else
                                UIHelper.WarnError(exs)
                                Exit For
                            End If

                        Next
                    End Using
                End Using


                Xl_ProgressBar1.Visible = False
                If exs.Count = 0 Then
                    MsgBox("factures desades a " & zipFilename, MsgBoxStyle.Information)
                Else
                    UIHelper.WarnError(exs)
                End If
            End If
        End With
    End Sub
End Class