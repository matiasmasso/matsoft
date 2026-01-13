Public Class Frm_InvoicesToPrint

    Private Async Sub Frm_InvoicesToPrint_Load(sender As Object, e As EventArgs) Handles Me.Load
        ' LoadPrintFchs()
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim AllItems = Await FEB2.Invoices.PrintPending(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Dim items As New List(Of DTOInvoice)
            If ToolStripMenuItemPaper.Checked Then items.AddRange(AllItems.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Printer).ToList)
            If ToolStripMenuItemEmail.Checked Then items.AddRange(AllItems.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Email).ToList)
            If ToolStripMenuItemEdi.Checked Then items.AddRange(AllItems.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Edi).ToList)

            Dim oSortedItems As List(Of DTOInvoice) = items.OrderBy(Function(x) x.Num).OrderBy(Function(x) x.Fch.Year).ToList
            Xl_InvoicesToPrint1.Load(oSortedItems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function


    Private Async Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        ButtonOk.Visible = False
        Xl_ProgressBar1.Load("Carregant dades")

        Dim oInvoices As List(Of DTOInvoice) = Xl_InvoicesToPrint1.CheckedValues()

        Dim exs As New List(Of Exception)
        Await PrintInvoices(oInvoices, Current.Session.User, Now, AddressOf Xl_ProgressBar1.ShowProgress, exs)

        Await refresca()
        Xl_ProgressBar1.Visible = False
        ButtonOk.Visible = True

        If exs.Count = 0 Then
            MsgBox(String.Format("{0} factures enviades correctament", oInvoices.Count))
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub


    Private Async Function PrintInvoices(oInvoices As List(Of DTOInvoice), oUser As DTOUser, DtFch As Date, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        'Provisionalment fora de febl.Invoices mentres DocRpt sigui dins de MaxiSrvr

        Dim oInvoicesForPrinter As List(Of DTOInvoice) = oInvoices.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Printer).ToList
        Dim oInvoicesForEmail As List(Of DTOInvoice) = oInvoices.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Email).ToList
        Dim oInvoicesForEdi As List(Of DTOInvoice) = oInvoices.Where(Function(x) x.PrintMode = DTOInvoice.PrintModes.Edi).ToList

        If oInvoicesForPrinter.Count > 0 Then
            MsgBox("L'enviament de factures a la impresora està obsolet, cal imprimir el pdf", MsgBoxStyle.Exclamation)
        End If

        Await FEB2.Invoices.SendToEmail(oInvoicesForEmail, oUser, DtFch, AddressOf Xl_ProgressBar1.ShowProgress, exs)
        Await FEB2.Invoices.SendToEdi(Current.Session.Emp, oInvoicesForEdi, oUser, DtFch, AddressOf Xl_ProgressBar1.ShowProgress, exs)
        Return True
    End Function


    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_InvoicesToPrint1.Filter = e.Argument
    End Sub

    Private Sub ToolStripMenuItemSelectAll_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSelectAll.Click
        Xl_InvoicesToPrint1.SelectAll()
    End Sub

    Private Sub ToolStripMenuItemSelectNone_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSelectNone.Click
        Xl_InvoicesToPrint1.SelectNone()
    End Sub

    Private Sub ToolStripMenuItemSelectRest_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemSelectRest.Click
        Xl_InvoicesToPrint1.SelectRest()
    End Sub

    Private Sub ToolStripMenuItemRecuperar_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItemRecuperar.Click
        Dim oFrm As New Frm_PrintedInvoices
        oFrm.Show()
    End Sub

    Private Async Sub Xl_Lookup_PrintedInvoices1_AfterUpdate(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim items = Await FEB2.Invoices.PrintPending(exs, Current.Session.Emp, e.Argument)
        If exs.Count = 0 Then
            Dim oSortedItems As List(Of DTOInvoice) = items.OrderBy(Function(x) x.Num).OrderBy(Function(x) x.Fch.Year).ToList
            Xl_InvoicesToPrint1.Load(oSortedItems)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub ToolStripMenuItemPaper_Click(sender As Object, e As EventArgs) Handles _
        ToolStripMenuItemPaper.Click, ToolStripMenuItemEmail.Click, ToolStripMenuItemEdi.Click
        Await refresca()
    End Sub

    Private Async Sub Xl_InvoicesToPrint1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_InvoicesToPrint1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Sub Xl_InvoicesToPrint1_RequestToCheckOut(sender As Object, e As MatEventArgs) Handles Xl_InvoicesToPrint1.RequestToCheckOut
        Dim oInvoices As List(Of DTOInvoice) = e.Argument
        Dim exs As New List(Of Exception)
        If Await FEB2.Invoices.CheckOutFromPrinting(exs, oInvoices, Current.Session.User, Now, AddressOf Xl_ProgressBar1.ShowProgress) Then
            Await refresca()
            Xl_ProgressBar1.ShowStart("")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub
End Class