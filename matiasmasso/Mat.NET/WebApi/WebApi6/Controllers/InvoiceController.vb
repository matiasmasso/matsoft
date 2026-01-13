Public Class InvoiceController
    Inherits _BaseController

    <HttpPost>
    <Route("api/contact/invoices")>
    Public Function ContactInvoices(contact As DUI.Guidnom) As List(Of DUI.Invoice)
        Dim retval As New List(Of DUI.Invoice)
        Dim oCustomer As New DTOCustomer(contact.Guid)
        Dim items As List(Of DTOInvoice) = BLLInvoices.All(oCustomer)
        For Each item As DTOInvoice In items
            Dim dui As New DUI.Invoice
            With dui
                .Guid = item.Guid
                .Id = item.Num
                .Fch = item.Fch
                .Eur = item.Total.Eur
                .FileUrl = BLLInvoice.Url(item)
                If item.DocFile IsNot Nothing Then
                    .FileUrl = BLLDocFile.DownloadUrl(item.DocFile, True)
                    .ThumbnailUrl = BLLDocFile.ThumbnailUrl(item.DocFile, True)
                End If
                retval.Add(dui)
            End With
        Next
        Return retval
    End Function

End Class
