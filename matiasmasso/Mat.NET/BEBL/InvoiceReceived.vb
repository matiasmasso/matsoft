Public Class InvoiceReceived

    Shared Function Find(oGuid As Guid) As DTOInvoiceReceived
        Return InvoiceReceivedLoader.Find(oGuid)
    End Function

    Shared Function Load(ByRef value As DTOInvoiceReceived) As Boolean
        Return InvoiceReceivedLoader.Load(value)
    End Function

    Shared Function Update(exs As List(Of Exception), value As DTOInvoiceReceived) As Boolean
        Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
        Validate(oEmp, value)
        Return InvoiceReceivedLoader.Update(value, exs)
    End Function

    Shared Function Delete(oInvoiceReceived As DTOInvoiceReceived, exs As List(Of Exception)) As Boolean
        Return InvoiceReceivedLoader.Delete(oInvoiceReceived, exs)
    End Function

    Shared Function Factory(oEdiInvoic As EdiHelperStd.Invoic) As DTOInvoiceReceived
        Dim retval As New DTOInvoiceReceived

        With retval
            If oEdiInvoic.ProveedorEAN IsNot Nothing Then
                .ProveidorEan = DTOEan.Factory(oEdiInvoic.ProveedorEAN.Value)
            End If
            .Fch = oEdiInvoic.Fch
            .DocNum = oEdiInvoic.Docnum
            .Cur = DTOCur.Factory(oEdiInvoic.Cur)
            .TaxBase = DTOAmt.Factory(.Cur, oEdiInvoic.TaxBase)
            .NetTotal = DTOAmt.Factory(.Cur, oEdiInvoic.NetTotal)

            For Each line In oEdiInvoic.Items
                Dim item As New DTOInvoiceReceived.Item()
                With item
                    .Lin = line.Lin
                    .Qty = line.Qty
                    .Price = line.GrossPrice
                    .Dto = line.Discount
                    .Amount = DTOAmt.Factory(line.Amount)
                    If line.Ean IsNot Nothing Then
                        .SkuEan = DTOEan.Factory(line.Ean.Value)
                    End If
                    .SkuRef = line.SupplierRef
                    .SkuNom = line.Description
                    .PurchaseOrderId = oEdiInvoic.PurchaseOrderOrDefault(line)
                    .OrderConfirmation = oEdiInvoic.OrderCondirmationOrDefault(line)
                    .DeliveryNote = oEdiInvoic.DeliveryNoteOrDefault(line)
                End With
                retval.Items.Add(item)
            Next
        End With

        Return retval
    End Function

    Public Shared Function Validate(oEmp As DTOEmp, value As DTOInvoiceReceived) As Boolean
        value.ClearExceptions()
        ValidateProveidor(value)
        ValidateItems(oEmp, value)
        Return Not value.HasExceptions()
    End Function

    Private Shared Sub ValidateProveidor(ByRef value As DTOInvoiceReceived)
        With value
            If .Proveidor Is Nothing Then
                If .ProveidorEan Is Nothing OrElse .ProveidorEan.Value Is Nothing Then
                    .AddException("proveidor desconegut", DTOInvoiceReceived.ExCods.ProveidorNotFound)
                Else
                    Dim oGLN = DTOEan.Factory(.ProveidorEan.Value)
                    Dim oContact = ContactLoader.FromGln(oGLN)
                    If oContact Is Nothing Then
                        .AddException(String.Format("proveidor amb Ean {0} no registrat", .ProveidorEan.Value), DTOInvoiceReceived.ExCods.ProveidorNotFound)
                    Else
                        .Proveidor = DTOGuidNom.Factory(oContact.Guid, oContact.Nom)
                    End If
                End If
            End If
        End With
    End Sub

    Private Shared Sub ValidateItems(oEmp As DTOEmp, ByRef value As DTOInvoiceReceived)
        Dim oSkus = BEBL.ProductSkus.FromEanValues(value.SkuEans)
        Dim oPurchaseOrders = BEBL.PurchaseOrders.FromIds(oEmp, value.PurchaseOrderIds)

        For Each item In value.Items
            item.Exceptions.Clear()
            ValidateSku(item, oSkus)
            ValidatePurchaseOrder(value, item, oPurchaseOrders)
            ValidatePrice(item, oSkus)
        Next
    End Sub

    Private Shared Sub ValidatePrice(ByRef value As DTOInvoiceReceived.Item, oSkus As List(Of DTOProductSku))
        Dim pnc = value.PurchaseOrderItem
        If pnc IsNot Nothing AndAlso value.Qty <> 0 Then
            Dim invoicedPrice = Math.Round(value.Amount.Eur / value.Qty, 2)
            Dim oDto = pnc.Price.Percent(pnc.Dto)
            Dim orderedPrice = pnc.Price.Clone().Substract(oDto).Eur ' pnc.Amount.Eur / pnc.Qty
            If invoicedPrice <> orderedPrice Then
                Dim sCompare = If(invoicedPrice > orderedPrice, "Preu excessiu", "Preu mes baix")

                Dim sMsg As String
                If pnc.Dto = 0 Then
                    sMsg = String.Format("{0}. Demanat per {1}", sCompare, pnc.Price.Formatted)
                Else
                    sMsg = String.Format("{0}. Demanat per {1} -{2}% dto = {3} x {4} uds. = {5}", sCompare, pnc.Price.CurFormatted, pnc.Dto, DTOAmt.Factory(orderedPrice).CurFormatted, value.Qty, DTOAmt.Factory(orderedPrice * value.Qty).CurFormatted)
                End If
                value.AddException(sMsg, DTOInvoiceReceived.Item.ExCods.PriceGap)
            End If
        End If
    End Sub

    Private Shared Sub ValidateSku(ByRef value As DTOInvoiceReceived.Item, oSkus As List(Of DTOProductSku))
        With value
            If .Sku Is Nothing Then
                If .SkuEan Is Nothing OrElse .SkuEan.Value Is Nothing Then
                    .AddException(String.Format("producte desconegut {0} {1} ", .SkuRef, .SkuNom), DTOInvoiceReceived.Item.ExCods.SkuNotFound)
                Else
                    Dim oSku = oSkus.FirstOrDefault(Function(x) CType(x, DTOProductSku).Ean13.Value = .SkuEan.Value)
                    If oSku Is Nothing Then
                        .AddException(String.Format("producte amb Ean {0} desconegut {1} {2}", .SkuEan.Value, .SkuRef, .SkuNom), DTOInvoiceReceived.Item.ExCods.SkuNotFound)
                    Else
                        .Sku = DTOGuidNom.Factory(oSku.Guid, String.Format("{0} {1}", oSku.Id, oSku.nomPrvOrMyd))
                    End If
                End If
            End If
        End With
    End Sub

    Private Shared Sub ValidatePurchaseOrder(value As DTOInvoiceReceived, ByRef item As DTOInvoiceReceived.Item, oPurchaseOrders As List(Of DTOPurchaseOrder))
        With item

            If String.IsNullOrEmpty(.PurchaseOrderId) Then
                .AddException("producte sense comanda", DTOInvoiceReceived.Item.ExCods.MissingPurchaseOrder)
            Else
                Dim oPurchaseOrder = oPurchaseOrders.FirstOrDefault(Function(x) x.formattedId = .PurchaseOrderId)
                If oPurchaseOrder Is Nothing Then
                    .AddException(String.Format("Comanda {0} desconeguda", .PurchaseOrderId), DTOInvoiceReceived.Item.ExCods.PurchaseOrderNotFound)
                Else
                    .PurchaseOrder = DTOGuidNom.Factory(oPurchaseOrder.Guid, String.Format("Comanda {0} del {1:dd/MM/yy} {2}", oPurchaseOrder.Num, oPurchaseOrder.Fch, oPurchaseOrder.Concept))
                    Dim oSku = item.Sku
                    If oSku IsNot Nothing Then
                        Dim pncs = oPurchaseOrder.Items.Where(Function(x) x.Sku.Guid.Equals(oSku.Guid)).ToList()
                        If pncs.Count = 0 Then
                            .AddException(String.Format("Producte no demanat a la comanda {0}", .PurchaseOrderId), DTOInvoiceReceived.Item.ExCods.MissingItemInPurchaseOrder)
                        ElseIf pncs.Count = 1 And value.Importacio Is Nothing Then
                            Dim pnc = pncs.First
                            pnc.PurchaseOrder = New DTOPurchaseOrder(.PurchaseOrder.Guid)
                            item.PurchaseOrderItem = pnc
                            If pnc.Pending < item.Qty Then
                                .AddException(String.Format("Facturat ({0}) unitats pendents nomes ({1})", item.Qty, pnc.Pending), DTOInvoiceReceived.Item.ExCods.QtyGap)
                            End If
                            pnc.Pending -= item.Qty
                        ElseIf item.PurchaseOrderItem Is Nothing Then
                            item.PurchaseOrderItem = pncs.First
                        End If
                    End If
                End If
            End If
        End With
    End Sub


    Shared Function Delivery(oInvoice As DTOInvoiceReceived, oUser As DTOUser) As DTODelivery
        Dim oProveidor = BEBL.Proveidor.Find(oInvoice.Proveidor.Guid)
        Dim retval = DTODelivery.Factory(oUser, DTOPurchaseOrder.Codis.proveidor, oProveidor, DTO.GlobalVariables.Today())
        BEBL.Emp.Load(oProveidor.Emp)
        retval.Emp = oProveidor.Emp
        retval.Mgz = oProveidor.Emp.Mgz
        retval.IsNew = False
        retval.IsLoaded = True
        Dim oPurchaseOrders = BEBL.PurchaseOrders.FromIds(oProveidor.Emp, oInvoice.PurchaseOrderIds)
        For Each item In oInvoice.Items
            Dim oArc = DTODeliveryItem.Factory(item.PurchaseOrderItem, item.Qty)
            retval.Items.Add(oArc)
        Next
        retval.Import = retval.totalCash

        retval.IsNew = True
        Return retval
    End Function
End Class



Public Class InvoicesReceived
    Shared Function All(year As Integer) As List(Of DTOInvoiceReceived)
        Return InvoicesReceivedLoader.All(year:=year)
    End Function

    Shared Function All(oImportacio As DTOImportacio) As List(Of DTOInvoiceReceived)
        Return InvoicesReceivedLoader.All(importacio:=oImportacio)
    End Function

    Shared Function All(oConfirmation As DTOImportacio.Confirmation) As List(Of DTOInvoiceReceived)
        Return InvoicesReceivedLoader.All(oConfirmation)
    End Function

    Shared Function ClearConfirmation(exs As List(Of Exception), oImportacio As DTOImportacio) As Boolean
        Return InvoicesReceivedLoader.ClearConfirmation(exs, oImportacio)
    End Function

    Shared Function Delete(values As List(Of DTOInvoiceReceived), ByRef exs As List(Of Exception)) As Boolean
        Return InvoicesReceivedLoader.Delete(values, exs)
    End Function

    Shared Function SetPrevisions(exs As List(Of Exception), oInvoicesReceived As List(Of DTOInvoiceReceived), oImportacio As DTOImportacio) As Boolean
        Dim oPrevisions As New List(Of DTOImportPrevisio)
        BEBL.Importacio.Load(oImportacio)
        Dim oEmp = oImportacio.emp

        Dim isSameProveidor As Boolean = oInvoicesReceived.All(Function(x) x.Proveidor.Equals(oInvoicesReceived.First().Proveidor))
        If isSameProveidor Then
            If oInvoicesReceived.First().Proveidor.Guid.Equals(oImportacio.proveidor.Guid) Then
                Dim oProveidor = oImportacio.proveidor
                For Each invoice In oInvoicesReceived
                    BEBL.InvoiceReceived.Load(invoice)

                    'check if duplicated
                    Dim oDuplicada As DTOCca = BEBL.Proveidor.CheckFacturaAlreadyExists(oProveidor, DTOExercici.Current(oEmp), invoice.DocNum)
                    If oDuplicada IsNot Nothing Then
                        Dim sWarn As String = "aquesta factura ja está entrada" & vbCrLf
                        sWarn = sWarn & "per " & DTOUser.NicknameOrElse(oDuplicada.UsrLog.UsrCreated) & " el " & oDuplicada.UsrLog.FchCreated.ToShortDateString & " a las " & String.Format("{0:HH:mm}", oDuplicada.UsrLog.FchCreated)
                        sWarn = sWarn & vbCrLf & " (assentament " & oDuplicada.Id.ToString & ")"
                        sWarn = sWarn & vbCrLf & oDuplicada.Concept
                        exs.Add(New Exception(sWarn))
                    Else
                        Dim oSkuGuids = invoice.Items.Where(Function(b) b.Sku IsNot Nothing).Select(Function(c) c.Sku.Guid).ToList()
                        Dim oSkus = BEBL.ProductSkus.FromGuids(oSkuGuids, oEmp.Mgz)
                        For Each item In invoice.Items


                            Dim oPrevisio As New DTOImportPrevisio()
                            Dim oSku As DTOProductSku = Nothing
                            If item.Sku IsNot Nothing Then oSku = oSkus.FirstOrDefault(Function(x) x.Guid.Equals(item.Sku.Guid))
                            If oSku Is Nothing Then
                                oPrevisio.Sku = New DTOProductSku(item.Sku.Guid)
                                oPrevisio.Sku.Nom.Esp = item.Sku.Nom
                                oPrevisio.SkuNom = item.Sku.Nom
                            Else
                                oPrevisio.Sku = oSku
                                oPrevisio.SkuNom = oSku.nomPrvAndRefOrMyd
                                oPrevisio.SkuRef = oSku.Id
                            End If
                            oPrevisio.NumComandaProveidor = item.PurchaseOrderId
                            oPrevisio.PurchaseOrderItem = item.PurchaseOrderItem
                            oPrevisio.InvoiceReceivedItem = item
                            oPrevisio.Qty = item.Qty
                            oPrevisio.Importacio = oImportacio
                            oImportacio.previsions.RemoveAll(Function(x) x.InvoiceReceivedItem IsNot Nothing AndAlso x.InvoiceReceivedItem.Guid.Equals(item.Guid))
                            oImportacio.previsions.Add(oPrevisio)
                        Next
                    End If
                Next


                If BEBL.Importacio.Update(oImportacio, exs) Then
                    BEBL.InvoicesReceived.SetImportacio(exs, oInvoicesReceived, oImportacio)
                End If

            Else
                exs.Add(New Exception("El proveidor de la importacio no coincideix amb el de les factures"))
            End If
        Else
            exs.Add(New Exception("Les factures no son del mateix proveidor"))
        End If
        Return exs.Count = 0
    End Function

    Shared Function ClearImportacio(exs As List(Of Exception), values As List(Of DTOInvoiceReceived)) As Boolean
        Return InvoicesReceivedLoader.SetImportacio(exs, values)
    End Function

    Shared Function SetImportacio(exs As List(Of Exception), values As List(Of DTOInvoiceReceived), oImportacio As DTOImportacio) As Boolean
        Return InvoicesReceivedLoader.SetImportacio(exs, values, oImportacio)
    End Function
End Class