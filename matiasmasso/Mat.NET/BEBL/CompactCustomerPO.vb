Public Class CompactCustomerPO

    Shared Function Upload(oPO As DTOCompactCustomerPO) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Try
            Dim oUser = BEBL.User.Find(oPO.User)
            If oUser Is Nothing Then
                retval.Fail("User unknown")
            Else
                Dim oCustomer = BEBL.Customer.Find(oPO.Customer)
                If oCustomer Is Nothing Then
                    retval.Fail("Customer unknown")
                Else
                    If oPO.Items.Count = 0 Then
                        retval.Fail("Order has no items")
                    Else
                        Dim oTarifa = BEBL.CustomerTarifa.Load(oCustomer)
                        Dim oOrder As DTOPurchaseOrder = DTOPurchaseOrder.Factory(oCustomer, oUser, DTO.GlobalVariables.Today(), DTOPurchaseOrder.Sources.cliente_por_WebApi, oPO.Concept)
                        Dim iLine As Integer
                        For Each POitem In oPO.Items
                            iLine += 1
                            If POitem.Ean = "" Then
                                retval.AddException("Line {0:000}: Missing product Ean", iLine)
                            Else
                                Dim oSku = oTarifa.GetSkuFromEan(POitem.Ean)
                                If oSku Is Nothing Then
                                    retval.AddException("Line {0:000}: Product with Ean {1} is out of catalogue", iLine, POitem.Ean)
                                Else
                                    Dim item As New DTOPurchaseOrderItem
                                    With item
                                        .Sku = oSku.ToProductSku()
                                        .Qty = POitem.Qty
                                        .Pending = .Qty
                                        .Price = oSku.Price.ToAmt
                                        .PurchaseOrder = oOrder
                                    End With
                                    oOrder.Items.Add(item)
                                    If oSku.Price.Eur <> POitem.Price Then
                                        retval.AddException("Line {0:000}: Product with Ean {1} ordered with wrong price {2}. Correct price is {3}", iLine, POitem.Ean, POitem.Price, oSku.Price.Eur)
                                    End If
                                End If
                            End If
                        Next

                        If oOrder.Items.Count = 0 Then
                            retval.Fail("Order {0} not registered since no lines passed validation", oPO.Concept)
                        Else
                            Dim exs As New List(Of Exception)
                            If BEBL.PurchaseOrder.Update(oOrder, exs) Then
                                If retval.Exceptions.Count = 0 Then
                                    retval.Succeed("Order {0} successfully registered under our number {1}.", oPO.Concept, oOrder.Num)
                                Else
                                    retval.Succeed("Order {0} registered under our number {1}. See validation {2} issues.", oPO.Concept, oOrder.Num, retval.Exceptions.Count)
                                End If
                            Else
                                retval.Fail("SYSERR_55. Order {0} not registered. Please contact our offices", oPO.Concept)
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            retval.Fail("SYSERR_54. Order {0} not registered. Please contact our offices", oPO.Concept)
        End Try
        Return retval
    End Function
End Class
