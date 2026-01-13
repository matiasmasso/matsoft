Public Class Bebitus
    Shared Function Customer() As DTOCustomer
        Return DTOCustomer.WellKnown(DTOCustomer.Wellknowns.Bebitus)
    End Function

    Shared Function isPurchaseOrder(sSegments As List(Of String)) As Boolean
        Dim retval As Boolean = sSegments.Any(Function(x) x.Contains("fiscal (NIF): N0040976C"))
        If Not sSegments(0).Contains("Línea Código de artículo EAN Descripción Descripción 2 Cantidad Unidad") Then retval = False
        Return retval
    End Function

    Shared Function PurchaseOrder(exs As List(Of Exception), oUser As DTOUser, sSegments As List(Of String), oTarifa As DTOCustomerTarifa) As DTOPurchaseOrder
        Dim retval As DTOPurchaseOrder = Nothing
        Dim sFchLabels = sSegments.Where(Function(x) x = "Fecha")
        Select Case sFchLabels.Count
            Case 0
                exs.Add(New Exception("no s'ha trobat cap camp amb la etiqueta 'fecha'"))
            Case 1
                Dim fchIdx = sSegments.IndexOf(sFchLabels.First)
                Dim DtFch = DateTime.ParseExact(sSegments(fchIdx + 1), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)
                retval = DTOPurchaseOrder.Factory(Customer(), oUser, DtFch)
                Dim sConceptSegment = sSegments.FirstOrDefault(Function(x) x.StartsWith("BEST-"))
                If Not String.IsNullOrEmpty(sConceptSegment) Then
                    retval.concept = sConceptSegment
                End If
                Dim idx As Integer = 6
                Do While idx < sSegments.Count
                    Dim sEan = sSegments(idx).Trim
                    If Not IsNumeric(sEan) Then Exit Do
                    Dim oSku = oTarifa.FindByEan(sEan)

                    Dim item As New DTOPurchaseOrderItem()
                    With item
                        .sku = oSku
                        .qty = Qty(idx, sSegments)
                        .pending = .qty
                        If oSku IsNot Nothing Then .price = oSku.price
                    End With
                    retval.items.Add(item)
                    idx += 5
                    If idx = 61 Then idx = 109
                    If sSegments(idx + 10).Contains("nif") Then Exit Do
                Loop
            Case Else
                exs.Add(New Exception("la data es ambigua; hi han diferents camps amb la etiqueta 'fecha'"))
        End Select
        Return retval
    End Function

    Shared Function Qty(idx As Integer, segments As List(Of String)) As Integer
        Dim idxQty = idx + 1
        Dim segment = segments(idxQty)
        Dim sWords = segment.Split(" ")
        Dim sQty = sWords.Last
        If sQty.IndexOf(",") >= 0 Then
            Dim comapos = sQty.IndexOf(",")
            sQty = sQty.Substring(0, comapos)
        End If
        Dim retval = CInt(sQty)
        Return retval
    End Function

End Class
