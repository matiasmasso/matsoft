Public Class Ediversa_INVOIC_D_01B_UN_EAN010
    'Sonae 

    Shared Async Function EdiversaMessage(oEmp As DTOEmp, oInvoice As DTOInvoice, exs As List(Of DTOEdiversaException)) As Task(Of String)
        Dim ex2 As New List(Of Exception)
        If Not Invoice.Load(oInvoice, ex2) Then
            exs.AddRange(DTOEdiversaException.FromSystemExceptions(ex2))
        End If

        Dim oDelivery As DTODelivery = oInvoice.Deliveries.First
        Delivery.Load(oDelivery, ex2)

        Dim oOrder As DTOPurchaseOrder = Nothing ' oDelivery.Items.First.PurchaseOrderItem.PurchaseOrder


        Dim oProveidor As DTOContact = oEmp.Org
        Dim oClient As DTOContact = oInvoice.Customer
        Dim oComprador As DTOCustomer = oDelivery.Customer
        Dim sRegistroMercantil As String = "RM Barcelona T6403 L5689 S2 F167 H76326"

        Customer.Load(oClient, ex2)
        Contact.Load(oClient, ex2)
        Contact.Load(oComprador, ex2)

        Dim sb As New System.Text.StringBuilder
        sb.AppendLine("INVOIC_D_01B_UN_EAN010")
        If (oInvoice.Serie = DTOInvoice.Series.rectificativa) Then
            sb.AppendLine("INV|" & oInvoice.NumeroYSerie & "|381")
        Else
            sb.AppendLine("INV|" & oInvoice.NumeroYSerie & "|380")
        End If
        sb.AppendLine("DTM|" & DTOEdiversaFile.EdiFormat(oInvoice.Fch))

        Dim oFirstDelivery As DTODelivery = oInvoice.Deliveries.First
        Dim oFirstOrder As DTOPurchaseOrder = oFirstDelivery.Items.First.PurchaseOrderItem.PurchaseOrder

        Dim sPedido As String = ""
        Dim sCentro As String = ""
        Dim sDept As String = ""
        Dim sNumPrv As String = ""
        If oComprador.isElCorteIngles Then
            'Ojo customitzat per ECI:
            DTOEci.GetDetailsFromPdc(oFirstOrder, sPedido, sCentro, sDept, sNumPrv)
        ElseIf oComprador.isSonae Then
            sPedido = Left(oFirstOrder.Concept, 17)
        Else
            sPedido = Left(oFirstOrder.Concept, 35)
        End If

        sb.AppendLine("RFF|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oFirstOrder.Fch))
        sb.AppendLine("RFF|DQ|" & oFirstDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oFirstDelivery.Fch))

        'Get custom Ean from original Edi order file
        Dim oPncGuids = oInvoice.
            Deliveries.SelectMany(Function(x) x.Items).
            Where(Function(x) x.PurchaseOrderItem IsNot Nothing).
            Select(Function(x) x.PurchaseOrderItem.Guid).ToList()
        Dim oEdiSkus As List(Of DTOEdiSku) = Await FEB.Delivery.LoadEdiSkus(ex2, oPncGuids)

        For Each oDelivery In oInvoice.Deliveries
            Delivery.Load(oDelivery, ex2)
            Contact.Load(oDelivery.Customer, ex2)
            Customer.Load(oDelivery.Customer, ex2)

            Dim sNifClient As String = DTONifOld.Intl(oClient)
            Dim sNifProveidor As String = DTONifOld.Intl(oProveidor)

            Dim NADBY = "", NADIV = "", NADDP = ""
            Dim oEdiOrderFiles As List(Of DTOEdiversaFile) = Await Invoice.EdiversaOrderFiles(ex2, oInvoice)
            If oEdiOrderFiles.Count > 0 Then
                Dim oEdiOrderFile = oEdiOrderFiles.First
                oEdiOrderFile.LoadSegments()
                NADBY = oEdiOrderFile.FieldValue("NADBY", 1)
                NADIV = oEdiOrderFile.FieldValue("NADIV", 1)
                NADDP = oEdiOrderFile.FieldValue("NADDP", 1)
            End If

            With oComprador
                If NADBY.isNotEmpty() Then
                    sb.AppendLine(String.Format("NADBY|{0}", NADBY))
                Else
                    sb.AppendLine(String.Format("NADBY|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, .Nom.Truncate(70).RemoveDiacritics(), .Address.Text.Truncate(70).RemoveDiacritics(), .Address.Zip.Location.Nom.Truncate(35), .Address.Zip.ZipCod.Truncate(5), TextHelper.VbLeft(sNifClient, 11), TextHelper.VbLeft(sDept, 3)))
                End If
            End With
            With oClient
                Dim sNif As String = DTONifOld.Intl(oClient)
                If NADIV.isNotEmpty() Then
                    sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", NADIV, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(sNifClient, 11)))
                Else
                    sb.AppendLine(String.Format("NADIV|{0}|{1}|{2}|{3}|{4}|{5}", .GLN.Value, TextHelper.VbLeft(.Nom, 70), TextHelper.ToSingleLine(.Address.Text, 70), TextHelper.VbLeft(.Address.Zip.Location.Nom, 35), TextHelper.VbLeft(.Address.Zip.ZipCod, 5), TextHelper.VbLeft(sNifClient, 11)))
                End If
            End With
            With oProveidor
                Dim sNif As String = DTONifOld.Intl(oProveidor)
                'sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}", .GLN.Value, Left(.Nom, 70), Left(sRegistroMercantil, 70), TextHelper.ToSingleLine(.Address.Text, 70), Left(.Address.Zip.Location.Nom, 35), Left(.Address.Zip.ZipCod, 5), Left(sNifProveidor, 11)))
                sb.AppendLine(String.Format("NADSU|{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}|{12}|{13}|{14}",
                                            .GLN.Value,
                                            TextHelper.VbLeft(.Nom, 70),
                                            "",
                                            TextHelper.ToSingleLine(.Address.Text, 70),
                                            TextHelper.VbLeft(.Address.Zip.Location.Nom, 35),
                                            TextHelper.VbLeft(.Address.Zip.ZipCod, 5),
                                            TextHelper.VbLeft(sNifProveidor, 11),
                                            "",
                                            "",
                                            "",
                                            TextHelper.VbLeft(sRegistroMercantil, 70),
                                            "",
                                            "",
                                            "ES",
                                            "60120 EUR"))
            End With

            Dim sGln As String = ""
            Dim oGln As DTOEan = DTODelivery.deliveryGLN(oFirstDelivery)
            If oGln IsNot Nothing Then
                sGln = oGln.Value
            End If

            If NADDP.isNotEmpty Then
                sb.AppendLine("NADDP|" & NADDP) 'Datos receptor mercancia (plataforma)
            Else
                sb.AppendLine("NADDP|" & sGln) 'Datos receptor mercancia (plataforma)
            End If

            sb.AppendLine("CUX|EUR|4")

            sb.AppendLine("TDT|30") 'Transporte por carretera. Ojo, generalitzar -----------------------------------------------------------------------
            sb.AppendLine(String.Format("LOCTDT|7|{0}|{1}||{2}", oClient.GLN.Value, TextHelper.VbLeft(oClient.Nom, 70), DTOEdiversaFile.EdiFormat(oDelivery.Fch)))
            sb.AppendLine(String.Format("LOCTDT|9|{0}|{1}|||{2}", oProveidor.GLN.Value, TextHelper.VbLeft(oProveidor.Nom, 70), DTOEdiversaFile.EdiFormat(oDelivery.Fch)))

            'sb.AppendLine(EdiSegmentPaymentTerms(oInvoice)) 'Pago|Basico (habitual)|despues de fecha factura|dias (anulat perque ECI diu que volen PAT|35, ningu sap que vol dir, pero que es opcional)

            oOrder = New DTOPurchaseOrder
            For Each item As DTODeliveryItem In oDelivery.Items
                If item.PurchaseOrderItem.PurchaseOrder.UnEquals(oOrder) Then
                    oOrder = item.PurchaseOrderItem.PurchaseOrder

                    If oComprador.isElCorteIngles Then
                        'Ojo customitzat per ECI:
                        DTOEci.GetDetailsFromPdc(oOrder, sPedido, sCentro, sDept, sNumPrv)
                    Else
                        sPedido = oFirstOrder.Concept
                    End If
                End If

                Dim oSku As DTOProductSku = item.PurchaseOrderItem.Sku

                'replace our Ean for customer Sku Ean from original edi order file if any
                Dim oEdiSku = oEdiSkus.FirstOrDefault(Function(x) x.PncGuid = item.PurchaseOrderItem.Guid)
                If oEdiSku IsNot Nothing Then oSku.Ean13 = New DTOEan(oEdiSku.Ean)

                If oSku.Ean13 Is Nothing Then
                    exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.MissingSkuEAN, item.PurchaseOrderItem, String.Format("fra.{0}: Falta codi EAN a l'article {1}: {2}", oInvoice.Num, oSku.Id, oSku.RefYNomLlarg.Esp)))
                Else
                    sb.AppendLine("LIN|" & oSku.Ean13.Value & "|EN") 'Datos proveedor
                End If

                sb.AppendLine("PIALIN|" & oSku.Id)
                sb.AppendLine("IMDLIN|" & oSku.NomLlarg.Esp.Truncate(70) & "|M")
                sb.AppendLine("QTYLIN|47|" & Math.Abs(item.Qty) & "|PCE")
                sb.AppendLine("MOALIN|" & DTOEdiversaFile.EdiFormat(item.Import.absolute()))
                sb.AppendLine("PRILIN|AAA|" & DTOEdiversaFile.EdiFormat(item.netPrice.absolute()))
                sb.AppendLine("PRILIN|AAB|" & DTOEdiversaFile.EdiFormat(item.Price.absolute()))

                'Ojo customitzat per ECI:
                sb.AppendLine("RFFLIN|ON|" & sPedido & "|" & DTOEdiversaFile.EdiFormat(oOrder.Fch))
                sb.AppendLine("RFFLIN|DQ|" & oDelivery.Id & "|" & DTOEdiversaFile.EdiFormat(oDelivery.Fch))

                If oInvoice.Iva = 0 Then
                    'sb.AppendLine("TAXLIN|EXT") //expressament refusat per Sonae
                    sb.AppendLine("TAXLIN|VAT|0|0")
                Else
                    sb.AppendLine("TAXLIN|VAT|" & DTOEdiversaFile.EdiFormat(oInvoice.Iva) & "|" & DTOEdiversaFile.EdiFormat(item.Import.Percent(oInvoice.Iva)) & "")
                End If

                If item.Dto <> 0 Then
                    sb.AppendLine("ALCLIN|A|1|TD||" & DTOEdiversaFile.EdiFormat(item.Dto))
                End If
            Next
        Next
        sb.AppendLine("CNTRES|2")


        Dim sNeto As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImportes(oInvoice).absolute())
        Dim sBase As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice).absolute())
        Dim sImpuestos As String = DTOEdiversaFile.EdiFormat(DTOInvoice.sumaDeImpuestos(oInvoice).absolute())
        Dim sDebido As String = DTOEdiversaFile.EdiFormat(DTOInvoice.getTotal(oInvoice).absolute())

        sb.AppendLine("MOARES|" & sNeto & "||" & sBase & "||" & sImpuestos & "||||" & sDebido & "|")

        '12/12/22 Sonae: "El porcentaje de IVA de la línea debe existir también en el resumen de IVA”. -> evitem TAXRES|EXT i posem TAXRES|VAT|0
        If oInvoice.Iva = 0 Then
            'sb.AppendLine("TAXRES|EXT") //expressament refusat per Sonae
            sb.AppendLine("TAXRES|VAT|0|0" & "|||||" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
        Else
            sb.AppendLine("TAXRES|VAT|" & DTOEdiversaFile.EdiFormat(oInvoice.Iva) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.iva_Standard)) & "|||||" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
            If oInvoice.Req <> 0 Then
                sb.AppendLine("TAXRES|RE|" & DTOEdiversaFile.EdiFormat(oInvoice.Req) & "|" & DTOEdiversaFile.EdiFormat(DTOInvoice.taxAmt(oInvoice, DTOTax.Codis.recarrec_Equivalencia_Standard)) & "|||||" & DTOEdiversaFile.EdiFormat(DTOInvoice.getBaseImponible(oInvoice)))
            End If
        End If

        Dim retval As String = sb.ToString
        Return retval
    End Function

End Class
