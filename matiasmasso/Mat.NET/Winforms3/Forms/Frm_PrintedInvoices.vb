Public Class Frm_PrintedInvoices

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Private Async Sub Frm_PrintedInvoices_Load(sender As Object, e As EventArgs) Handles Me.Load
        Await refresca()
    End Sub

    Private Sub Xl_PrintedInvoices1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_PrintedInvoices1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub


    Private Async Sub Xl_PrintedInvoices1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PrintedInvoices1.RequestToRefresh
        Await refresca()
    End Sub

    Private Async Function refresca() As Task
        Dim exs As New List(Of Exception)
        Dim oPrintedInvoices = Await FEB.Invoices.PrintedCollection(exs, Current.Session.Emp)
        If exs.Count = 0 Then
            Xl_PrintedInvoices1.Load(oPrintedInvoices)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub Xl_TextboxSearch1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_TextboxSearch1.AfterUpdate
        Xl_PrintedInvoices1.Filter = e.Argument
    End Sub
End Class