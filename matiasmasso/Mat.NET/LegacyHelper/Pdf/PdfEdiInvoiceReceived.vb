Public Class PdfEdiInvoiceReceived
    Protected Enum Tabs
        HeaderEpg = 100
        Qty = 350
        Price = 390
        Dto = 440
        Amt = 480
    End Enum
    Shared Function Factory(value As DTOInvoiceReceived, oEmp As DTOEmp) As Byte()
        Dim oPdf As New _PdfBase()
        oPdf.SetMargins(30, 30, 20, 30)
        oPdf.Y = oPdf.Top
        With value
            DrawString("Emisor:", oPdf, oPdf.Left, oPdf.Left + Tabs.HeaderEpg)
            DrawString(.Proveidor.Nom, oPdf, oPdf.Left + Tabs.HeaderEpg, oPdf.Right)
            oPdf.Y += oPdf.Font.Height

            DrawString("Receptor:", oPdf, oPdf.Left, oPdf.Left + Tabs.HeaderEpg)
            DrawString(String.Format("{0} {1}", oEmp.Org.Nom, oEmp.Org.PrimaryNifQualifiedValue), oPdf, oPdf.Left + Tabs.HeaderEpg, oPdf.Right)
            oPdf.Y += oPdf.Font.Height

            DrawString("Factura num:", oPdf, oPdf.Left, oPdf.Left + Tabs.HeaderEpg)
            DrawString(.DocNum, oPdf, oPdf.Left + Tabs.HeaderEpg, oPdf.Right)
            oPdf.Y += oPdf.Font.Height

            DrawString("Data:", oPdf, oPdf.Left, oPdf.Left + Tabs.HeaderEpg)
            DrawString(String.Format("{0:dd/MM/yyyy}", .Fch), oPdf, oPdf.Left + Tabs.HeaderEpg, oPdf.Right)
            oPdf.Y += oPdf.Font.Height

            Dim oPurchaseOrder As New DTOGuidNom()
            Dim sPurchaseOrderId = "@@"
            Dim sOrderConfirmation = "@@"
            Dim sDeliveryNote = "@@"

            oPdf.Y += oPdf.Font.Height
            DrawString("Concepte", oPdf, oPdf.Left, oPdf.Left + Tabs.Qty)
            DrawString("Quantitat", oPdf, oPdf.Left + Tabs.Qty, oPdf.Left + Tabs.Price, StringAlignment.Far)
            DrawString("Preu", oPdf, oPdf.Left + Tabs.Price, oPdf.Left + Tabs.Dto, StringAlignment.Far)
            DrawString("Dte", oPdf, oPdf.Left + Tabs.Dto, oPdf.Left + Tabs.Amt, StringAlignment.Far)
            DrawString("Import", oPdf, oPdf.Left + Tabs.Amt, oPdf.Right, StringAlignment.Far)
            oPdf.Y += oPdf.Font.Height
            oPdf.Y += oPdf.Font.Height

            For Each item As DTOInvoiceReceived.Item In .Items
                Dim lines As Integer = 1
                With item
                    If sDeliveryNote <> (.DeliveryNote) Then
                        sDeliveryNote = .DeliveryNote
                        DrawString(String.Format("Albarà {0}", .DeliveryNote), oPdf, oPdf.Left, oPdf.Left + Tabs.Qty)
                        oPdf.Y += oPdf.Font.Height
                    End If
                    If (.PurchaseOrderId <> sPurchaseOrderId) Or (.PurchaseOrder IsNot Nothing AndAlso oPurchaseOrder.UnEquals(.PurchaseOrder)) Then
                        oPurchaseOrder = .PurchaseOrder
                        sPurchaseOrderId = .PurchaseOrderId
                        Dim sComanda = String.Format("Comanda {0}", .PurchaseOrderId)
                        If sOrderConfirmation <> (.OrderConfirmation) Then
                            sOrderConfirmation = .OrderConfirmation
                            sComanda = String.Format("{0} - Confirmació {1}", sComanda, .OrderConfirmation)
                        End If
                        DrawString(sComanda, oPdf, oPdf.Left, oPdf.Left + Tabs.Qty)
                        oPdf.Y += oPdf.Font.Height
                    End If
                    oPdf.X = 30
                    Dim sEan As String = DTOEan.eanValue(.SkuEan)
                    Dim s = String.Format("{0} {1} {2}", DTOEan.eanValue(.SkuEan), .SkuRef, .SkuNom)
                    lines = DrawString(s, oPdf, oPdf.Left, oPdf.Left + Tabs.Qty)
                    DrawInteger(.Qty, oPdf, oPdf.Left + Tabs.Qty, oPdf.Left + Tabs.Price)
                    DrawAmount(DTOAmt.Factory(.Price), oPdf, oPdf.Left + Tabs.Price, oPdf.Left + Tabs.Dto)
                    DrawPercent(.DtoOrDefault, oPdf, oPdf.Left + Tabs.Dto, oPdf.Left + Tabs.Amt)
                    DrawAmount(.Amount, oPdf, oPdf.Left + Tabs.Amt, oPdf.Right)
                End With
                oPdf.Y += oPdf.Font.Height
            Next
            oPdf.Y += oPdf.Font.Height
            Dim oSumaDeImports = .SumaDeImports()
            If oSumaDeImports.Eur <> .TaxBase.Eur Then
                DrawString("Suma de imports " & value.Cur.Tag, oPdf, oPdf.Left, oPdf.Left + Tabs.Qty, , Brushes.Red)
                DrawAmount(.SumaDeImports(), oPdf, oPdf.Left + Tabs.Amt, oPdf.Right, Brushes.Red)
                DrawString("Total en factura", oPdf, oPdf.Left, oPdf.Left + Tabs.Qty)
                DrawAmount(.TaxBase, oPdf, oPdf.Left + Tabs.Amt, oPdf.Right, Brushes.Red)
            Else
                DrawString("Total " & value.Cur.Tag, oPdf, oPdf.Left, oPdf.Left + Tabs.Qty,, Brushes.Green)
                DrawAmount(.TaxBase, oPdf, oPdf.Left + Tabs.Amt, oPdf.Right, Brushes.Green)
            End If

        End With

        Dim retval As Byte() = oPdf.Stream()
        Return retval
    End Function

    Private Shared Function DrawString(ByVal value As String, ByRef oPdf As _PdfBase, xLeft As Integer, xRight As Integer, Optional alignment As StringAlignment = StringAlignment.Near, Optional oBrush As Brush = Nothing) As Integer
        Dim sF As New StringFormat()
        sF.Alignment = alignment
        Dim s As String = value
        Dim iHeight = oPdf.Pdf.MeasureString(s, oPdf.Font).Height
        Dim oRc As New RectangleF(xLeft, oPdf.Y, xRight - xLeft, iHeight)
        If sF.Alignment = StringAlignment.Far Then
            oRc = New RectangleF(xRight, oPdf.Y, xRight - xLeft, iHeight)
        End If
        If oBrush Is Nothing Then oBrush = Brushes.Black
        oPdf.DrawString(s, oPdf.Font, oBrush, oRc, sF)
        Return iHeight
    End Function

    Private Shared Sub DrawInteger(ByVal value As Integer, ByRef oPdf As _PdfBase, xLeft As Integer, xRight As Integer, Optional oBrush As Brush = Nothing)
        Dim s As String = String.Format("{0:#,###}", value)
        DrawString(s, oPdf, xLeft, xRight, StringAlignment.Far, oBrush)
    End Sub

    Private Shared Sub DrawAmount(ByVal value As DTOAmt, ByRef oPdf As _PdfBase, xLeft As Integer, xRight As Integer, Optional oBrush As Brush = Nothing)
        Dim s As String = value.CurFormatted
        DrawString(s, oPdf, xLeft, xRight, StringAlignment.Far, oBrush)
    End Sub

    Private Shared Sub DrawPercent(ByVal value As Decimal, ByRef oPdf As _PdfBase, xLeft As Integer, xRight As Integer, Optional oBrush As Brush = Nothing)
        Dim s As String = String.Format("{0:###.0 \%;-###.0 \%;#}", value)
        DrawString(s, oPdf, xLeft, xRight, StringAlignment.Far, oBrush)
    End Sub
End Class
