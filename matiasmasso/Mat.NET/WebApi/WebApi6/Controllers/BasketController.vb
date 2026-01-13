Public Class BasketController
    Inherits _BaseController

    <HttpPost>
    <Route("api/basket/submit")>
    Public Function submit(basket As DUI.Basket) As DUI.Basket
        Dim retval As New DUI.Basket
        Dim sb As New Text.StringBuilder

        Try

            Dim oRepProducts As List(Of DTORepProduct) = BLL.BLLRepProducts.All(BLL.BLLApp.Emp, , False)
            Dim oRepCliComs As List(Of DTORepCliCom) = BLL.BLLRepCliComs.All(BLL.BLLApp.Emp)

            Dim oCustomer As DTOCustomer = Nothing
            Dim oCcx As DTOCustomer = Nothing
            If basket.Customer Is Nothing Then
                sb.AppendLine("Falta cliente")
            Else
                oCustomer = BLLCustomer.Find(basket.Customer.Guid)
                BLLContact.Load(oCustomer)
                If oCustomer Is Nothing Then
                    sb.AppendLine("Cliente desconocido")
                Else
                    oCcx = BLL.BLLCustomer.CcxOrMe(oCustomer)
                    oCcx.ProductDtos = BLL.BLLCliProductDtos.All(oCcx)
                End If
            End If

            Dim oPurchaseOrder As New DTOPurchaseOrder()
            With oPurchaseOrder
                .Customer = oCustomer
                If basket.User Is Nothing Then
                    sb.AppendLine("Falta usuario")
                Else
                    .UsrCreated = BLLUser.Find(basket.User.Guid)
                    If .UsrCreated Is Nothing Then
                        sb.AppendLine("Usuario desconocido")
                    End If
                End If
                .Fch = Today
                If basket.FchMin <> Nothing Then
                    Dim DtFchMin As Date
                    If Date.TryParseExact(basket.FchMin, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, Globalization.DateTimeStyles.None, DtFchMin) Then
                        If DtFchMin > Today Then
                            .FchDelivery = DtFchMin
                        End If
                    Else
                        sb.AppendLine("Fecha no valida '" & basket.FchMin & "'")
                    End If
                End If
                .Cod = DTOPurchaseOrder.Codis.Client
                .Source = DTOPurchaseOrder.Sources.iPhone
                If basket.Promo IsNot Nothing Then
                    .Incentiu = New DTOIncentiu(basket.Promo.Guid)
                End If
                .Concept = basket.Nom
                .Obs = basket.Obs
                .TotJunt = basket.TotJunt
                .Items = New List(Of DTOPurchaseOrderItem)
                If basket.items Is Nothing Then
                    sb.AppendLine("pedido sin lineas")
                Else
                    For Each dui As DUI.PurchaseOrderItem In basket.items
                        Dim item As New DTOPurchaseOrderItem()
                        With item
                            .PurchaseOrder = oPurchaseOrder
                            .Qty = dui.Qty
                            .Pending = .Qty
                            If dui.Sku Is Nothing Then
                                sb.AppendLine("falta producto")
                            Else
                                .Sku = New DTOProductSku(dui.Sku.Guid)
                            End If
                            .Price = BLLApp.GetAmt(dui.Eur)

                            .Sku.Category = New DTOProductCategory(dui.Sku.Category.Guid) 'important per assignar el representant
                            .Sku.Category.Brand = New DTOProductBrand(dui.Sku.Category.Brand.Guid)
                            .Price = BLL.BLLCustomer.SkuPrice(oCcx, .Sku)
                            .Dto = BLL.BLLPurchaseOrderItem.GetDiscount(.Sku, oCcx)

                            Dim exs As New List(Of Exception)
                            .RepCom = BLL.BLLPurchaseOrderItem.SuggestedRepCom(item, oRepProducts, oRepCliComs, exs)
                            If exs.Count > 0 Then
                                sb.AppendLine("error al asignar comercial")
                            End If

                        End With
                        .Items.Add(item)

                        Dim oSkuWithsToAppend As List(Of DTOSkuWith) = BLL.BLLSkuWiths.Find(item.Sku)
                        If oSkuWithsToAppend.Count > 0 Then

                            BLL.BLLProductSku.Load(item.Sku)
                            For Each oSkuWith As DTOSkuWith In oSkuWithsToAppend
                                Dim oItemWith As New DTOPurchaseOrderItem
                                With oItemWith
                                    .Sku = oSkuWith.Child
                                    .Qty = item.Qty * oSkuWith.Qty
                                    .Pending = .Qty
                                    .Price = BLL.BLLCustomer.SkuPrice(oCcx, oSkuWith.Child)
                                    .Dto = item.Dto
                                    Dim exs As New List(Of Exception)
                                    .RepCom = BLL.BLLPurchaseOrderItem.SuggestedRepCom(item, oRepProducts, oRepCliComs, exs)
                                    If exs.Count > 0 Then
                                        sb.AppendLine("error al asignar comercial")
                                    End If
                                End With
                                oPurchaseOrder.Items.Add(oItemWith)
                            Next
                        End If

                    Next
                End If
            End With

            BLL.BLLPurchaseOrder.SetIncentius(oPurchaseOrder.UsrCreated, oPurchaseOrder)

            If sb.Length = 0 Then
                Dim exs As New List(Of Exception)
                If BLLPurchaseOrder.Update(oPurchaseOrder, exs) Then
                    retval = New DUI.Basket
                    With retval
                        .Guid = oPurchaseOrder.Guid
                        .Id = oPurchaseOrder.Num
                        .Success = True
                    End With
                Else
                    sb.AppendLine("error al grabar pedido")
                    sb.AppendLine(exs.First.Message)
                End If
            End If

        Catch ex As Exception
            sb.AppendLine(ex.Message)
        End Try
        retval.Message = sb.ToString
        Return retval
    End Function

    <HttpPost>
    <Route("api/basket/mailConfirmation")>
    Public Function mailConfirmation(basket As DUI.Basket) As DUI.TaskResult
        Dim oPurchaseOrder As DTOPurchaseOrder = BLLPurchaseOrder.Find(basket.Guid)
        Dim oRecipients As New List(Of DTOUser)
        Select Case basket.ConfirmationEmailCode
            Case DUI.Basket.ConfirmationEmailCodes.nobody
            Case DUI.Basket.ConfirmationEmailCodes.user
                oRecipients.Add(oPurchaseOrder.UsrCreated)
            Case DUI.Basket.ConfirmationEmailCodes.both
                oRecipients.Add(oPurchaseOrder.UsrCreated)
                Dim oCustomerUsers As List(Of DTOUser) = BLLUsers.All(oPurchaseOrder.Customer)
                If oCustomerUsers.Count > 0 Then
                    oRecipients.Add(oCustomerUsers.First)
                End If
        End Select

        Dim exs As New List(Of Exception)
        Dim retval As New DUI.TaskResult
        retval.Success = BLLPurchaseOrder.MailConfirmation(oPurchaseOrder, oRecipients, exs)
        If Not retval.Success Then
            Dim sb As New System.Text.StringBuilder
            sb.AppendLine("no ha sido posible enviar la confirmación por email")
            sb.AppendLine(BLLExceptions.ToFlatString(exs))
            retval.Message = sb.ToString
        End If
        Return retval
    End Function

End Class
