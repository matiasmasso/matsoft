Public Class PurchaseOrdersPdfHelper
    Public Sub New(ByVal oPurchaseOrders As List(Of DTOPurchaseOrder), Optional ByVal BlSigned As Boolean = False, Optional ByVal BlProforma As Boolean = False, Optional ByVal SaveAsFileName As String = "")
        'mPdf = New C1.C1Pdf.C1PdfDocument(Printing.PaperKind.A4)
        Dim exs As New List(Of Exception)
        If oPurchaseOrders.Count > 0 Then
            'ensure first order is loaded so fch can decide template before or after 2007
            Dim oFirstOrder As DTOPurchaseOrder = oPurchaseOrders.First
            FEBL.PurchaseOrder.Load(oFirstOrder, exs)

            Dim oPdfAlb As New PdfAlb2(oPurchaseOrders, BlSigned, BlProforma, SaveAsFileName)
            For Each oPurchaseOrder As DTOPurchaseOrder In oPurchaseOrders
                Dim oDoc = FEBL.PurchaseOrder.Doc(oPurchaseOrder, BlProforma)
                oPdfAlb.PrintDoc(oDoc)
            Next

        End If
        'mFileName = Save(SaveAsFileName, BlSigned)
    End Sub

End Class
