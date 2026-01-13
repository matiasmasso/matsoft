Public Class Ediversa_INVOIC_D_93A_UN_EAN007
    'carrefour, Eci

    Shared Async Function EdiversaMessage(oEmp As DTOEmp, oInvoice As DTOInvoice, exs As List(Of DTOEdiversaException)) As Task(Of String)
        Dim retval As String = ""
        Dim ex2 As New List(Of Exception)
        If Not FEB2.Invoice.Load(oInvoice, ex2) Then
            exs.AddRange(DTOEdiversaException.FromSystemExceptions(ex2))
        End If

        Dim oDelivery As DTODelivery = oInvoice.Deliveries.First
        FEB2.Delivery.Load(oDelivery, ex2)

        Dim oOrder As DTOPurchaseOrder = Nothing ' oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder


        Dim oProveidor As DTOContact = oEmp.Org
        Dim oClient As DTOContact = oInvoice.Customer
        Dim oComprador As DTOCustomer = oDelivery.Customer
        Dim sRegistroMercantil As String = "RM Barcelona T6403 L5689 S2 F167 H76326"

        FEB2.Customer.Load(oClient, ex2)
        FEB2.Contact.Load(oClient, ex2)
        FEB2.Contact.Load(oComprador, ex2)

        Dim oNadBy As DTOContact = oComprador
        Dim oNadDp As DTOContact = oComprador
        Dim oNadIv As DTOContact = oClient

        If oDelivery.Platform IsNot Nothing Then oNadDp = oDelivery.Platform

        Dim oFirstDelivery As DTODelivery = oInvoice.Deliveries.First
        Dim oFirstOrder As DTOPurchaseOrder = oFirstDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
        Dim sPedido As String = oFirstOrder.Concept
        Dim sCentro As String = ""
        Dim sDept As String = ""
        Dim sNumPrv As String = ""

        If oClient.isElCorteIngles Then
            Dim oEdiversaOrderFile = Await FEB2.EdiversaFile.FromResultGuid(ex2, oFirstOrder.Guid)
            If oEdiversaOrderFile Is Nothing Then
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSkuEAN, oFirstOrder, "No s'ha trobat el fitxer Edi de la comanda '" & oFirstOrder.Concept & "' a la factura " & oInvoice.Num))
            Else
                oNadBy = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADBY, exs)
                oNadDp = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADDP, exs)
                oNadIv = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADIV, exs)
            End If
            DTOEci.GetDetailsFromPdc(oFirstOrder, sPedido, sCentro, sDept, sNumPrv)
        End If

        Dim sb As New Text.StringBuilder
        If oNadBy IsNot Nothing And oNadDp IsNot Nothing And oNadIv IsNot Nothing Then
            sb.AppendLine("INVOIC_D_93A_UN_EAN007")
            sb.AppendLine("INV|" & oInvoice.Num & "|380")
            sb.AppendLine("DTM|" & DTOEdiversaFile.EdiFormat(oInvoice.Fch))

            If oInvoice.IsSingleOrder Then
                sb.AppendLine("RFF|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oFirstOrder.Fch))
            End If

            If oInvoice.IsSingleDelivery Then
                sb.AppendLine("RFF|DQ|" & oFirstDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oFirstDelivery.Fch))
            End If

            For Each oDelivery In oInvoice.Deliveries
                Try

                    FEB2.Delivery.Load(oDelivery, ex2)
                    FEB2.Contact.Load(oDelivery.Customer, ex2)

                    If oInvoice.Customer.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)) Then

                        With oComprador
                            sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}", oNadBy.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            'sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}|{6}", "8480015009007", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11), Left(sDept, 3)))
                        End With
                        With oClient
                            sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", oNadIv.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            'sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", "8480015222222", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11)))
                            sb.AppendLine(String.Format("NADBCO|{0}|{1}|{2}|{3}|{4}|{5}", oNadIv.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            'sb.AppendLine(String.Format("NADPR|{0}|{1}|{2}|{3}|{4}|{5}", "8480015222222", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11)))
                        End With
                        With oProveidor
                            sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                            sb.AppendLine(String.Format("NADSCO|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                            sb.AppendLine(String.Format("NADII|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                        End With
                        sb.AppendLine("NADDP|" & oNadDp.GLN.Value) 'Datos receptor mercancia (plataforma)
                    Else
                        With oComprador
                            sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11), TextHelper.VbLeft(sDept, 3)))
                        End With
                        With oClient
                            sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            sb.AppendLine(String.Format("NADBCO|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                        End With
                        With oProveidor
                            sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                            sb.AppendLine(String.Format("NADSCO|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                            sb.AppendLine(String.Format("NADII|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                        End With

                        'Recupera plataforma de entrega com el client a nom del qual va el primer albarà de la factura
                        Dim sGln As String = ""
                        Dim oGln As DTOEan = DTODelivery.deliveryGLN(oFirstDelivery)
                        If oGln IsNot Nothing Then
                            sGln = oGln.Value
                        End If

                        sb.AppendLine("NADDP|" & sGln) 'Datos receptor mercancia (plataforma)
                    End If



                    sb.AppendLine("CUX|EUR|4")
                    'sb.AppendLine("ALC|N|1|TD|0|0") 'Descompte comercial global

                    'sb.AppendLine(EdiSegmentPaymentTerms(oInvoice)) 'Pago|Basico (habitual)|despues de fecha factura|dias (anulat perque ECI diu que volen PAT|35, ningu sap que vol dir, pero que es opcional)

                    oOrder = New DTOPurchaseOrder
                    For Each item As DTODeliveryItem In oDelivery.Items
                        If item.PurchaseOrderItem.PurchaseOrder.UnEquals(oOrder) Then
                            oOrder = item.PurchaseOrderItem.PurchaseOrder

                            If oClient.isElCorteIngles Then
                                'Ojo customitzat per ECI:
                                DTOEci.GetDetailsFromPdc(oOrder, sPedido, sCentro, sDept, sNumPrv)
                            Else
                                sPedido = oOrder.Concept
                            End If

                        End If

                        Dim oSku As DTOProductSku = item.PurchaseOrderItem.Sku
                        If oSku.Ean13 Is Nothing Then
                            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSkuEAN, item.PurchaseOrderItem, String.Format("fra.{0}: Falta codi EAN a l'article {1}: {2}", oInvoice.Num, oSku.Id, oSku.NomLlarg.Esp)))
                        Else
                            sb.AppendLine("LIN|" & oSku.Ean13.Value & "|EN") 'Datos proveedor
                        End If

                        sb.AppendLine("PIALIN|" & oSku.Id)
                        sb.AppendLine("IMDLIN|" & oSku.NomLlarg.Esp.Left(70) & "|M")
                        sb.AppendLine("QTYLIN|47|" & item.Qty & "|PCE")
                        sb.AppendLine("MOALIN|" & DTOEdiversaFile.EdiFormat(item.Import))
                        sb.AppendLine("PRILIN|AAB|" & DTOEdiversaFile.EdiFormat(item.Price))
                        sb.AppendLine("PRILIN|AAA|" & DTOEdiversaFile.EdiFormat(item.netPrice))

                        If Not oInvoice.IsSingleOrder Then
                            sb.AppendLine("RFFLIN|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oOrder.Fch))
                        End If

                        If Not oInvoice.IsSingleDelivery Then
                            sb.AppendLine("RFFLIN|DQ|" & oDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oDelivery.Fch))
                        End If

                        If item.Dto <> 0 Then
                            sb.AppendLine("ALCLIN|A|1|TD||" & DTOEdiversaFile.EdiFormat(item.Dto))
                        End If
                    Next
                Catch ex As Exception
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oDelivery, String.Format("Error al generar el Edi per l'albarà {0}", oDelivery.Id)))
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, Nothing, ex.Message))
                End Try
            Next
            sb.AppendLine("CNTRES|2")

            Dim sNeto As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImportes(oInvoice)) 'Importe neto de la factura = sumatorio de los importes netos por línea (incluye descuentos y cargos lineales, pero no globales)
            Dim sBase As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)) 'Base imponible = importe neto total de la factura menos total descuentos globales mas total cargos globales
            Dim sTotal As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getTotal(oInvoice))  'Importe total de la factura = base imponible + importe total de impuestos. Obligatorio para facturas comerciales.
            Dim sImpuestos As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImpuestos(oInvoice))
            Dim sDebido As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getTotal(oInvoice))
            sb.AppendLine("MOARES|" & sNeto & "||" & sBase & "|" & sTotal & "|" & sImpuestos)

            If oInvoice.Iva = 0 Then
                sb.AppendLine("TAXRES|EXT")
            Else
                sb.AppendLine("TAXRES|VAT|" & DTOEdiversaFile.EdiFormat(oInvoice.Iva) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.iva_Standard)) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
                If oInvoice.Req <> 0 Then
                    sb.AppendLine("TAXRES|RE|" & DTOEdiversaFile.EdiFormat(oInvoice.Req) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.recarrec_Equivalencia_Standard)) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
                End If
            End If
        End If
        retval = sb.ToString

        Return retval
    End Function
    Shared Async Function EdiMessage(oEmp As DTOEmp, oInvoice As DTOInvoice, exs As List(Of DTOEdiversaException)) As Task(Of String)
        Dim retval As String = ""
        Dim ex2 As New List(Of Exception)
        If Not FEB2.Invoice.Load(oInvoice, ex2) Then
            exs.AddRange(DTOEdiversaException.FromSystemExceptions(ex2))
        End If

        Dim oDelivery As DTODelivery = oInvoice.Deliveries.First
        FEB2.Delivery.Load(oDelivery, ex2)

        Dim oOrder As DTOPurchaseOrder = Nothing ' oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder


        Dim oProveidor As DTOContact = oEmp.Org
        Dim oClient As DTOContact = oInvoice.Customer
        Dim oComprador As DTOCustomer = oDelivery.Customer
        Dim sRegistroMercantil As String = "RM Barcelona T6403 L5689 S2 F167 H76326"

        FEB2.Customer.Load(oClient, ex2)
        FEB2.Contact.Load(oClient, ex2)
        FEB2.Contact.Load(oComprador, ex2)

        Dim oNadBy As DTOContact = oComprador
        Dim oNadDp As DTOContact = oComprador
        Dim oNadIv As DTOContact = oClient

        If oDelivery.Platform IsNot Nothing Then oNadDp = oDelivery.Platform

        Dim oFirstDelivery As DTODelivery = oInvoice.Deliveries.First
        Dim oFirstOrder As DTOPurchaseOrder = oFirstDelivery.Items.First.PurchaseOrderItem.PurchaseOrder
        Dim sPedido As String = oFirstOrder.Concept
        Dim sCentro As String = ""
        Dim sDept As String = ""
        Dim sNumPrv As String = ""

        If oClient.isElCorteIngles Then
            Dim oEdiversaOrderFile = Await FEB2.EdiversaFile.FromResultGuid(ex2, oFirstOrder.Guid)
            If oEdiversaOrderFile Is Nothing Then
                exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSkuEAN, oFirstOrder, "No s'ha trobat el fitxer Edi de la comanda '" & oFirstOrder.Concept & "' a la factura " & oInvoice.Num))
            Else
                oNadBy = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADBY, exs)
                oNadDp = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADDP, exs)
                oNadIv = Await FEB2.EdiversaFile.GetInterlocutor(oEdiversaOrderFile, DTOEdiversaContact.Cods.NADIV, exs)
            End If
            DTOEci.GetDetailsFromPdc(oFirstOrder, sPedido, sCentro, sDept, sNumPrv)
        End If

        Dim sb As New Text.StringBuilder
        If oNadBy IsNot Nothing And oNadDp IsNot Nothing And oNadIv IsNot Nothing Then
            sb.AppendLine("INVOIC_D_93A_UN_EAN007")
            sb.AppendLine("INV|" & oInvoice.NumeroYSerie & "|380")
            sb.AppendLine("DTM|" & DTOEdiversaFile.EdiFormat(oInvoice.Fch))

            If oInvoice.IsSingleOrder Then
                sb.AppendLine("RFF|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oFirstOrder.Fch))
            End If

            If oInvoice.IsSingleDelivery Then
                sb.AppendLine("RFF|DQ|" & oFirstDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oFirstDelivery.Fch))
            End If

            Dim isFirstDelivery As Boolean = True
            For Each oDelivery In oInvoice.Deliveries
                Try

                    FEB2.Delivery.Load(oDelivery, ex2)

                    If isFirstDelivery Then
                        FEB2.Contact.Load(oDelivery.Customer, ex2)

                        If oInvoice.Customer.Equals(DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)) Then

                            With oComprador
                                sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}", oNadBy.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                                'sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}|{6}", "8480015009007", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11), Left(sDept, 3)))
                            End With
                            With oClient
                                sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", oNadIv.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                                'sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", "8480015222222", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11)))
                                sb.AppendLine(String.Format("NADBCO|{0}|{1}|{2}|{3}|{4}|{5}", oNadIv.GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                                'sb.AppendLine(String.Format("NADPR|{0}|{1}|{2}|{3}|{4}|{5}", "8480015222222", Left(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(oClient.Nif, 11)))
                            End With
                            With oProveidor
                                sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                                sb.AppendLine(String.Format("NADSCO|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                                sb.AppendLine(String.Format("NADII|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            End With
                            sb.AppendLine("NADDP|" & oNadDp.GLN.Value) 'Datos receptor mercancia (plataforma)
                        Else
                            With oComprador
                                sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11), TextHelper.VbLeft(sDept, 3)))
                            End With
                            With oClient
                                sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                                sb.AppendLine(String.Format("NADBCO|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            End With
                            With oProveidor
                                sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                                sb.AppendLine(String.Format("NADSCO|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.VbLeft(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(.PrimaryNifValue(), 11)))
                                sb.AppendLine(String.Format("NADII|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(oClient.PrimaryNifValue(), 11)))
                            End With

                            'Recupera plataforma de entrega com el client a nom del qual va el primer albarà de la factura
                            Dim sGln As String = ""
                            Dim oGln As DTOEan = DTODelivery.deliveryGLN(oFirstDelivery)
                            If oGln IsNot Nothing Then
                                sGln = oGln.Value
                            End If

                            sb.AppendLine("NADDP|" & sGln) 'Datos receptor mercancia (plataforma)
                        End If



                        sb.AppendLine("CUX|EUR|4")
                        'sb.AppendLine("ALC|N|1|TD|0|0") 'Descompte comercial global

                        'sb.AppendLine(EdiSegmentPaymentTerms(oInvoice)) 'Pago|Basico (habitual)|despues de fecha factura|dias (anulat perque ECI diu que volen PAT|35, ningu sap que vol dir, pero que es opcional)
                        isFirstDelivery = False
                    End If

                    oOrder = New DTOPurchaseOrder
                    For Each item As DTODeliveryItem In oDelivery.Items
                        If item.PurchaseOrderItem.PurchaseOrder.UnEquals(oOrder) Then
                            oOrder = item.PurchaseOrderItem.PurchaseOrder

                            If oClient.isElCorteIngles Then
                                'Ojo customitzat per ECI:
                                DTOEci.GetDetailsFromPdc(oOrder, sPedido, sCentro, sDept, sNumPrv)
                            Else
                                sPedido = oOrder.Concept
                            End If

                        End If

                        Dim oSku As DTOProductSku = item.PurchaseOrderItem.Sku
                        If oSku.Ean13 Is Nothing Then
                            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSkuEAN, item.PurchaseOrderItem, String.Format("fra.{0}: Falta codi EAN a l'article {1}: {2}", oInvoice.Num, oSku.Id, oSku.NomLlarg.Esp)))
                        Else
                            sb.AppendLine("LIN|" & oSku.Ean13.Value & "|EN") 'Datos proveedor
                        End If

                        sb.AppendLine("PIALIN|" & oSku.Id)
                        sb.AppendLine("IMDLIN|" & oSku.NomLlarg.Esp.Left(70) & "|M")
                        sb.AppendLine("QTYLIN|47|" & item.Qty & "|PCE")
                        sb.AppendLine("MOALIN|" & DTOEdiversaFile.EdiFormat(item.Import))
                        sb.AppendLine("PRILIN|AAB|" & DTOEdiversaFile.EdiFormat(item.Price))
                        sb.AppendLine("PRILIN|AAA|" & DTOEdiversaFile.EdiFormat(item.netPrice))

                        If Not oInvoice.IsSingleOrder Then
                            sb.AppendLine("RFFLIN|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oOrder.Fch))
                        End If

                        If Not oInvoice.IsSingleDelivery Then
                            sb.AppendLine("RFFLIN|DQ|" & oDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oDelivery.Fch))
                        End If

                        If item.Dto <> 0 Then
                            sb.AppendLine("ALCLIN|A|1|TD||" & DTOEdiversaFile.EdiFormat(item.Dto))
                        End If
                    Next
                Catch ex As Exception
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oDelivery, String.Format("Error al generar el Edi per l'albarà {0}", oDelivery.Id)))
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, Nothing, ex.Message))
                End Try
            Next
            sb.AppendLine("CNTRES|2")

            Dim sNeto As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImportes(oInvoice)) 'Importe neto de la factura = sumatorio de los importes netos por línea (incluye descuentos y cargos lineales, pero no globales)
            Dim sBase As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)) 'Base imponible = importe neto total de la factura menos total descuentos globales mas total cargos globales
            Dim sTotal As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getTotal(oInvoice))  'Importe total de la factura = base imponible + importe total de impuestos. Obligatorio para facturas comerciales.
            Dim sImpuestos As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImpuestos(oInvoice))
            Dim sDebido As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getTotal(oInvoice))
            sb.AppendLine("MOARES|" & sNeto & "||" & sBase & "|" & sTotal & "|" & sImpuestos)

            If oInvoice.Iva = 0 Then
                sb.AppendLine("TAXRES|EXT")
            Else
                sb.AppendLine("TAXRES|VAT|" & DTOEdiversaFile.EdiFormat(oInvoice.Iva) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.iva_Standard)) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
                If oInvoice.Req <> 0 Then
                    sb.AppendLine("TAXRES|RE|" & DTOEdiversaFile.EdiFormat(oInvoice.Req) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.recarrec_Equivalencia_Standard)) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
                End If
            End If
        End If
        retval = sb.ToString

        Return retval
    End Function

End Class
