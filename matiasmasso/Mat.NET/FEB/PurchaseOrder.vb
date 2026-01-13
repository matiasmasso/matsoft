Public Class PurchaseOrder
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOPurchaseOrder)
        Dim retval = Await Api.Fetch(Of DTOPurchaseOrder)(exs, "purchaseorder", oGuid.ToString())
        For Each item In retval.Items
            item.PurchaseOrder = retval
        Next
        Return retval
    End Function

    Shared Function FindSync(exs As List(Of Exception), oGuid As Guid) As DTOPurchaseOrder
        Dim retval = Api.FetchSync(Of DTOPurchaseOrder)(exs, "purchaseorder", oGuid.ToString())
        For Each item In retval.Items
            item.PurchaseOrder = retval
        Next
        Return retval
    End Function

    Shared Async Function FromNum(oEmp As DTOEmp, year As Integer, id As Integer, exs As List(Of Exception)) As Task(Of DTOPurchaseOrder)
        Return Await Api.Fetch(Of DTOPurchaseOrder)(exs, "purchaseOrder/FromNum", oEmp.Id, year, id)
    End Function

    Shared Async Function FromEdi(exs As List(Of Exception), src As String, oEmp As DTOEmp) As Task(Of DTOPurchaseOrder)
        Return Await Api.Execute(Of String, DTOPurchaseOrder)(src, exs, "purchaseOrder/fromEdi", oEmp.Id)
    End Function

    Shared Function Load(ByRef oPurchaseOrder As DTOPurchaseOrder, exs As List(Of Exception)) As Boolean
        If Not oPurchaseOrder.IsLoaded And Not oPurchaseOrder.IsNew Then
            Dim pPurchaseOrder = Api.FetchSync(Of DTOPurchaseOrder)(exs, "PurchaseOrder", oPurchaseOrder.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPurchaseOrder)(pPurchaseOrder, oPurchaseOrder, exs)
                For Each item In oPurchaseOrder.Items
                    item.PurchaseOrder = oPurchaseOrder
                Next
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oPurchaseOrder As DTOPurchaseOrder, Optional oMgz As DTOMgz = Nothing) As Boolean
        If Not oPurchaseOrder.IsLoaded And Not oPurchaseOrder.IsNew Then
            Dim pPurchaseOrder = Api.FetchSync(Of DTOPurchaseOrder)(exs, "PurchaseOrder", oPurchaseOrder.Guid.ToString, OpcionalGuid(oMgz))
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOPurchaseOrder)(pPurchaseOrder, oPurchaseOrder, exs)
                For Each item In oPurchaseOrder.Items
                    item.PurchaseOrder = oPurchaseOrder
                Next
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function OrderWithDeliveryItems(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOPurchaseOrder)
        Return Await Api.Fetch(Of DTOPurchaseOrder)(exs, "PurchaseOrder/WithDeliveryItems", oGuid.ToString())
    End Function

    Shared Async Function Update(exs As List(Of Exception), value As DTOPurchaseOrder) As Task(Of DTOPurchaseOrder)
        Dim retval As DTOPurchaseOrder = Nothing

        'trim in order to avoid out of memory
        For Each item In value.Items
            item.Sku = New DTOProductSku(item.Sku.Guid)
            For Each bundle In item.Bundle
                bundle.Sku = New DTOProductSku(bundle.Sku.Guid)
            Next
        Next

        Dim serialized = Api.Serialize(exs, value) ', Newtonsoft.Json.ReferenceLoopHandling.Serialize) 'perque permeti la referencia circular dels bundles

        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.DocFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", value.DocFile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", value.DocFile.Stream)
            End If
            If value.EtiquetesTransport IsNot Nothing Then
                oMultipart.AddFileContent("EtiquetesTransport_thumbnail", value.EtiquetesTransport.Thumbnail)
                oMultipart.AddFileContent("EtiquetesTransport_stream", value.EtiquetesTransport.Stream)
            End If
            retval = Await Api.Upload(Of DTOPurchaseOrder)(oMultipart, exs, "PurchaseOrder/update")
        End If
        Return retval
    End Function


    Shared Async Function Delete(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of Boolean)
        Dim values As New List(Of DTOPurchaseOrder)
        values.Add(oPurchaseOrder)
        Return Await Api.Delete(Of List(Of DTOPurchaseOrder))(values, exs, "PurchaseOrders")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oGuids As List(Of Guid)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of Guid))(oGuids, exs, "PurchaseOrders")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPurchaseOrders As List(Of DTOPurchaseOrder)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTOPurchaseOrder))(oPurchaseOrders, exs, "PurchaseOrders")
    End Function


    Shared Async Function CobraPerVisa(exs As List(Of Exception), oTpvLog As DTOTpvLog) As Task(Of DTOTpvLog)
        Return Await Api.Execute(Of DTOTpvLog, DTOTpvLog)(oTpvLog, exs, "PurchaseOrder/CobraPerVisa")
    End Function

    Shared Async Function ResetPendingQty(exs As List(Of Exception), value As DTOPurchaseOrder) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "purchaseOrder/ResetPendingQty", value.Guid.ToString())
    End Function

    Shared Async Function SearchConcepte(exs As List(Of Exception), sKey As String) As Task(Of DTOPurchaseOrderConcepte)
        Return Await Api.Execute(Of String, DTOPurchaseOrderConcepte)(sKey, exs, "purchaseOrder/SearchConcepte")
    End Function

    Shared Async Function RecalculaPendents(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "purchaseOrder/RecalculaPendents", oPurchaseOrder.Guid.ToString())
    End Function

    Shared Async Function RemovePromo(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "purchaseOrder/RemovePromo", oPurchaseOrder.Guid.ToString())
    End Function

    Shared Function Url(oOrder As DTOPurchaseOrder, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oOrder IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "pedido", oOrder.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function UrlTpv(value As DTOPurchaseOrder, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            Dim oParameters As New Dictionary(Of String, String)
            oParameters.Add("Guid", value.Guid.ToString())
            oParameters.Add("Mode", DTOTpvRequest.Modes.Pdc)
            Dim sParameter As String = CryptoHelper.UrlFriendlyBase64Json(oParameters)
            retval = UrlHelper.Factory(AbsoluteUrl, "tpv", sParameter)
        End If
        Return retval
    End Function


    Shared Function ExcelUrl(oOrder As DTOPurchaseOrder, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oOrder IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.PurchaseOrderExcel, oOrder.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function PdfUrl(value As DTOPurchaseOrder, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            retval = DocFile.DownloadUrl(value.DocFile, AbsoluteUrl)
        End If
        Return retval
    End Function


    Shared Function SetIncentius(exs As List(Of Exception), oUser As DTOUser, ByRef oPurchaseOrder As DTOPurchaseOrder) As Boolean
        Dim retval As Boolean
        'No carreguis PurchaseOrder des de la base de dades perque ho fa servir al afegir linia de comanda on the fly

        Dim oCcx = Customer.CcxOrMe(exs, oPurchaseOrder.Contact)
        If Not oCcx.NoIncentius Then
            Dim oIncentius = Incentius.AllSync(exs, oUser, False).FindAll(Function(x) x.OnlyInStk = False).ToList
            Dim oOrderIncentius As New List(Of DTOIncentiu)
            Dim oOrderIncentiu As New DTOIncentiu
            Dim oItm As DTOPurchaseOrderItem
            Dim DcDto As Decimal

            'assigna les quantitats de cada oferta
            For Each oItm In oPurchaseOrder.Items
                oItm.Incentius = DTOProductSku.Incentius(oItm.Sku, oIncentius)
                If oItm.Incentius.Count > 0 Then
                    For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                        Dim BlFoundInOrderIncentius As Boolean = False
                        For Each oOrderIncentiu In oOrderIncentius
                            If oOrderIncentiu.Equals(oItmIncentiu) Then
                                BlFoundInOrderIncentius = True
                                Exit For
                            End If
                        Next

                        If Not BlFoundInOrderIncentius Then
                            oOrderIncentiu = oItmIncentiu
                            oOrderIncentius.Add(oOrderIncentiu)
                        End If

                        oOrderIncentiu.Unitats += oItm.Qty
                    Next
                End If
            Next


            For Each oItm In oPurchaseOrder.Items

                'assigna la quantitat mès alta de les ofertes en que participa cada linia
                Dim iQty As Integer = 0
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    For Each oOrderIncentiu In oOrderIncentius
                        If oOrderIncentiu.Equals(oItmIncentiu) Then
                            If oOrderIncentiu.Unitats > iQty Then
                                iQty = oOrderIncentiu.Unitats
                            End If
                            Exit For
                        End If
                    Next
                Next

                'assigna el descompte de la oferta mes favorable que li toca a cada linea
                Dim DcOrderDto As Decimal = oItm.Dto
                For Each oItmIncentiu As DTOIncentiu In oItm.Incentius
                    DcDto = DTOIncentiu.GetDto(oItmIncentiu, iQty)
                    If DcDto > DcOrderDto Then
                        DcOrderDto = DcDto
                        retval = True
                    End If
                Next
                oItm.Dto = DcOrderDto
            Next

        End If
        Return retval
    End Function

    Shared Async Function BlockPendingItems(exs As List(Of Exception), oPurchaseOrder As DTOPurchaseOrder) As Task(Of Boolean)
        Dim dirty As Boolean
        oPurchaseOrder = Await PurchaseOrder.Find(oPurchaseOrder.Guid, exs)
        For i = 0 To oPurchaseOrder.Items.Count - 1
            Dim item = oPurchaseOrder.Items(i)
            If oPurchaseOrder.Items(i).ErrCod = DTOPurchaseOrderItem.ErrCods.Success AndAlso oPurchaseOrder.Items(i).Pending > 0 Then
                If oPurchaseOrder.Items(i).Qty = oPurchaseOrder.Items(i).Pending Then
                    oPurchaseOrder.Items(i).ErrCod = DTOPurchaseOrderItem.ErrCods.OutOfStock
                Else
                    'passa la part pendent a una nova linia al final de la comanda
                    Dim oNewItem = DTOPurchaseOrderItem.Factory(oPurchaseOrder, oPurchaseOrder.Items(i).Sku, oPurchaseOrder.Items(i).Pending, oPurchaseOrder.Items(i).Price, oPurchaseOrder.Items(i).Dto, oPurchaseOrder.Items(i).RepCom)
                    oPurchaseOrder.Items.Add(oNewItem)
                    oNewItem.ErrCod = DTOPurchaseOrderItem.ErrCods.OutOfStock

                    oPurchaseOrder.Items(i).Qty = oPurchaseOrder.Items(i).Qty - oPurchaseOrder.Items(i).Pending
                    oPurchaseOrder.Items(i).Pending = 0
                End If
                dirty = True
            End If
        Next

        If dirty Then Await PurchaseOrder.Update(exs, oPurchaseOrder)
        Return exs.Count = 0
    End Function

    Shared Function Doc(oPurchaseOrder As DTOPurchaseOrder, BlProforma As Boolean) As DTODoc
        Dim exs As New List(Of Exception)
        Dim retval As DTODoc = Nothing
        If PurchaseOrder.Load(oPurchaseOrder, exs) Then
            If Contact.Load(oPurchaseOrder.Contact, exs) Then
                Dim oContact As DTOContact = oPurchaseOrder.contact

                Dim oDocStyle As DTODoc.Estilos = If(BlProforma, DTODoc.Estilos.proforma, DTODoc.Estilos.comanda)
                retval = New DTODoc(oDocStyle, oPurchaseOrder.Contact.Lang, oPurchaseOrder.Cur)

                With retval
                    .Dest.Add(oPurchaseOrder.Contact.Nom)
                    If oContact.NomComercial > "" Then .Dest.Add(oContact.NomComercial)

                    For Each s As String In oContact.Address.Text.Split(vbCrLf)
                        If s > "" Then .Dest.Add(s.Trim)
                    Next

                    .Dest.Add(DTOAddress.ZipyCit(oContact.Address))
                    If Not oContact.Address.IsEsp Then
                        .Dest.Add("(" & DTOAddress.Country(oContact.address).LangNom.Esp & ")")
                    End If

                    If oPurchaseOrder.Cod = DTOPurchaseOrder.Codis.Client Then
                        .RecarrecEquivalencia = oPurchaseOrder.Customer.CcxOrMe.Req
                    End If

                    If oPurchaseOrder.fchDeliveryMin > Date.MinValue Then
                        Dim obs = oContact.lang.Tradueix("plazo de entrega", "plaç de entrega", "delivery time") & " " & oPurchaseOrder.fchDeliveryMin.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                        If oPurchaseOrder.fchDeliveryMax > Date.MinValue Then
                            obs = obs & " - " & oPurchaseOrder.fchDeliveryMax.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"))
                        End If
                        .obs.Add(obs)
                    End If

                    If oPurchaseOrder.cod = DTOPurchaseOrder.Codis.proveidor Then
                        'desde 14/11/08 ocultem les observacions al client
                        If oPurchaseOrder.obs > "" Then .Obs.Add(oPurchaseOrder.obs)
                        'afegeix magatzem de entrega
                        If oPurchaseOrder.platform IsNot Nothing Then
                            Dim oMgzEntregarEn = oPurchaseOrder.platform
                            Dim oAdresses As List(Of DTOAddress) = Addresses.AllSync(oMgzEntregarEn, exs)
                            Dim oAddress As DTOAddress = oAdresses.FirstOrDefault(Function(x) x.codi = DTOAddress.Codis.Entregas)
                            If oAddress Is Nothing Then oAddress = oAdresses.FirstOrDefault(Function(x) x.codi = DTOAddress.Codis.Fiscal)

                            .Obs.Add(.Lang.Tradueix("entregar en", "entregar a", "deliver to") & ": " & oMgzEntregarEn.NomComercialOrDefault())
                            If oAddress IsNot Nothing Then
                                .Obs.Add(oAddress.Text)
                                .Obs.Add(DTOZip.FullNom(oAddress.Zip))
                            End If
                        End If
                    End If

                    If oPurchaseOrder.TotJunt Then
                        Dim s As String = oContact.Lang.Tradueix("servir todo junto", "servir tot junt", "ship all together")
                        .Obs.Add(s)
                    End If

                    If oPurchaseOrder.ProformaObs IsNot Nothing Then
                        For Each s In oPurchaseOrder.ProformaObs
                            .Obs.Add(s)
                        Next
                    End If
                    .Fch = oPurchaseOrder.Fch
                    .Num = oPurchaseOrder.num
                    '.Dto = mDt2
                    '.Dpp = mDpp
                    If oPurchaseOrder.cod = DTOPurchaseOrder.Codis.client Then
                        .Itms.Add(New DTODocItm(DTOPurchaseOrder.FullConcepte(oPurchaseOrder, oPurchaseOrder.Contact.lang), DTODoc.FontStyles.Underline))
                        .Itms.Add(New DTODocItm)
                    End If
                    Dim sArtNom As String = ""
                    For Each item In oPurchaseOrder.items
                        If oPurchaseOrder.cod = DTOPurchaseOrder.Codis.proveidor Or oContact.lang.Equals(DTOLang.ENG) Then
                            sArtNom = DTOProductSku.RefYNomPrv(item.sku)
                        Else
                            sArtNom = DTOProductSku.FullNom(item.Sku)
                        End If

                        .Itms.Add(New DTODocItm(sArtNom, DTODoc.FontStyles.Regular, item.qty, item.price, item.dto, , 2))

                        If item.Bundle.Count > 0 Then
                            .Itms.Add(New DTODocItm(.Lang.Tradueix("compuesto de los siguientes elementos:", "compost dels següents elements", "composed of the following elements:"),,,,,, 6))
                            For Each oChildItem In item.Bundle
                                Dim sSkuNom = oChildItem.Sku.RefYNomLlarg.Tradueix(oPurchaseOrder.Contact.Lang)
                                If oPurchaseOrder.Cod = DTOPurchaseOrder.Codis.Client AndAlso oPurchaseOrder.Contact.lang.Equals(DTOLang.ENG) Then
                                    sSkuNom = oChildItem.sku.refYNomPrv()
                                End If
                                Dim oDocItm = New DTODocItm(sSkuNom,  , , , , , , 4)
                                oDocItm.LeftPadChars = 12
                                .Itms.Add(oDocItm)
                            Next
                        End If
                    Next

                    .IvaBaseQuotas = oPurchaseOrder.IvaBaseQuotas

                End With

            End If
        End If
        Return retval
    End Function

    Shared Async Function MailConfirmation(exs As List(Of Exception), oUser As DTOUser, oPurchaseOrder As DTOPurchaseOrder, Optional sRecipients As List(Of String) = Nothing) As Task(Of Boolean)
        Dim oMailMessage = DTOPurchaseOrder.MailMessageConfirmation(oPurchaseOrder, sRecipients)
        Dim retval = Await MailMessage.Send(exs, oUser, oMailMessage)
        Return retval
    End Function

    Shared Function Excel(oOrder As DTOPurchaseOrder) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("M+O")
        Dim oLang = oOrder.contact.lang
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        With retval
            .filename = DTOPurchaseOrder.filename(oOrder, MimeCods.Xlsx)

            .addColumn(, MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.PlainText)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.Integer)
            If DTOPurchaseOrder.pendentsExist(oOrder) Then
                .addColumn(, MatHelper.Excel.Cell.NumberFormats.Integer)
            End If
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.Euro)
            If DTOPurchaseOrder.discountsExist(oOrder) Then
                .addColumn(, MatHelper.Excel.Cell.NumberFormats.Percent)
            End If
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.Euro)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.m3)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.m3)
            .addColumn(, MatHelper.Excel.Cell.NumberFormats.Kg)
            .AddColumn(, MatHelper.Excel.Cell.NumberFormats.Kg)
            .AddColumn(, MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With

        Dim oRow As MatHelper.Excel.Row = retval.addRow
        oRow.addCell()
        oRow.addCell()
        oRow.addCell(oOrder.contact.FullNom)

        oRow = retval.addRow
        oRow.addCell()
        oRow.addCell()
        oRow.addCell(oOrder.caption(oOrder.contact.lang))

        oRow = retval.addRow
        oRow = retval.addRow


        oRow.addCell("M+O")
        oRow.addCell(oLang.Tradueix("Referencia", "Referència", "Code"))
        oRow.addCell(oLang.Tradueix("Concepto", "Concepte", "Concept"))
        oRow.addCell(oLang.Tradueix("Unidades", "Unitats", "Units"))
        If DTOPurchaseOrder.pendentsExist(oOrder) Then
            oRow.addCell(oLang.Tradueix("Pendiente", "Pendent", "Pending"))
        End If
        oRow.addCell(oLang.Tradueix("Precio/unidad", "Preu/unitat", "Unit price"))
        If DTOPurchaseOrder.discountsExist(oOrder) Then
            oRow.addCell(oLang.Tradueix("Descuento", "Descompte", "Discount"))
        End If
        oRow.addCell(oLang.Tradueix("Importe", "Import", "Amount"))
        oRow.addCell(oLang.Tradueix("Vol/unidad", "Vol/unitat", "Unit volume"))
        oRow.addCell(oLang.Tradueix("Volumen total", "Volum total", "Total volume"))
        oRow.addCell(oLang.Tradueix("Peso/unidad", "Pes/unitat", "Unit weight"))
        oRow.AddCell(oLang.Tradueix("Peso total", "Pes total", "Total weight"))
        oRow.AddCell(oLang.Tradueix("Código EAN", "Codi EAN", "EAN code"))

        Dim sUrlOrder As String = PurchaseOrder.Url(oOrder, True)
        oRow = retval.AddRow
        oRow = retval.AddRow
        'oOrder = item.purchaseOrder
        oRow.AddCell(oOrder.Num, sUrlOrder)
        Dim oCell As MatHelper.Excel.Cell = oRow.AddCell(oOrder.caption(oLang))
        oCell.CellStyle = MatHelper.Excel.Cell.CellStyles.Italic

        If oOrder.items.Count > 0 Then

            For Each item As DTOPurchaseOrderItem In oOrder.Items
                oRow = retval.AddRow

                Dim sUrlSku As String = item.Sku.GetUrl(oLang,, True)
                oRow.addCell(item.sku.id, sUrlSku)
                oRow.addCell(item.sku.refProveidor)
                If oOrder.cod = DTOPurchaseOrder.Codis.proveidor Then
                    oRow.addCell(item.sku.nomProveidor)
                Else
                    oRow.AddCell(item.Sku.RefYNomLlarg.Esp)
                End If

                oRow.addCell(item.qty)
                If DTOPurchaseOrder.pendentsExist(oOrder) Then
                    oRow.addCell(item.pending)
                End If

                If item.price Is Nothing Then
                    oRow.addCell()
                    oRow.addCell()
                Else
                    oRow.addCell(item.price.Eur)
                    If DTOPurchaseOrder.discountsExist(oOrder) Then
                        oRow.addCell(item.dto)
                        oRow.addFormula("RC[-3]*RC[-2]-ROUND(RC[-3]*RC[-2]*RC[-1]/100,2)")
                    Else
                        oRow.addFormula("RC[-2]*RC[-1]")
                    End If
                End If


                Dim m3 As Decimal = DTOProductSku.volumeM3OrInherited(item.sku)
                oRow.addCell(m3)
                If DTOPurchaseOrder.discountsExist(oOrder) Then
                    oRow.addFormula("RC[-5]*RC[-1]")
                Else
                    oRow.addFormula("RC[-4]*RC[-1]")
                End If

                Dim kg As Decimal = DTOProductSku.weightKgOrInherited(item.sku)
                oRow.addCell(kg)
                If DTOPurchaseOrder.discountsExist(oOrder) Then
                    oRow.addFormula("RC[-7]*RC[-1]")
                Else
                    oRow.addFormula("RC[-6]*RC[-1]")
                End If

                If item.Sku.Ean13 IsNot Nothing Then
                    oRow.AddCell(item.Sku.Ean13.Value)
                Else
                    oRow.AddCell()
                End If
            Next

            oRow = retval.addRow
            oRow.addCell()
            oRow.addCell()
            oRow.addCell("Totals")
            oRow.addCell()

            If DTOPurchaseOrder.pendentsExist(oOrder) Then
                oRow.addCell()
            End If

            oRow.addCell()

            If DTOPurchaseOrder.discountsExist(oOrder) Then
                oRow.addCell()
            End If

            oRow.addFormula("SUM(R[-" & oOrder.items.Count & "]C:R[-1]C)")
            oRow.addCell()
            oRow.addFormula("SUM(R[-" & oOrder.items.Count & "]C:R[-1]C)")
            oRow.addCell()
            oRow.addFormula("SUM(R[-" & oOrder.items.Count & "]C:R[-1]C)")

        End If
        Return retval
    End Function

    Shared Function Excel(items As List(Of DTOPurchaseOrderItem), Optional oLang As DTOLang = Nothing) As MatHelper.Excel.Sheet
        'pendents d'entrega

        Dim retval As New MatHelper.Excel.Sheet
        Dim oDomain = DTOWebDomain.Factory(oLang, True)
        Dim oCod As DTOPurchaseOrder.Codis = items(0).purchaseOrder.cod

        Dim oRow As MatHelper.Excel.Row = retval.addRow()
        oRow.addCell("Fecha")
        oRow.addCell("Pedido")
        oRow.addCell("Sku")
        oRow.addCell("Producto")
        oRow.addCell("Cantidad")

        For Each item As DTOPurchaseOrderItem In items
            oRow = retval.addRow()
            oRow.addCell(item.purchaseOrder.fch.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES")))
            If oCod = DTOPurchaseOrder.Codis.proveidor Then
                oRow.addCell(item.purchaseOrder.num, PurchaseOrder.Url(item.purchaseOrder, True))
                oRow.addCell(item.sku.refProveidor, item.sku.GetUrl(oLang))
                oRow.addCell(item.sku.nomProveidor)
            Else
                oRow.addCell(item.purchaseOrder.concept, PurchaseOrder.Url(item.purchaseOrder, True))
                oRow.addCell(item.sku.id, item.sku.GetUrl(oLang))
                oRow.AddCell(item.Sku.Nom.Esp)
            End If
            oRow.addCell(item.pending)
        Next
        Return retval
    End Function


End Class


Public Class PurchaseOrders
    Inherits _FeblBase

    Shared Async Function Years(exs As List(Of Exception), oEmp As DTOEmp, oCod As DTOPurchaseOrder.Codis, Optional oContact As DTOContact = Nothing, Optional IncludeGroupSalePoints As Boolean = False) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "purchaseorders/years", oEmp.Id, oCod, OpcionalGuid(oContact), OpcionalBool(IncludeGroupSalePoints))
    End Function

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, oCod As DTOPurchaseOrder.Codis, year As Integer, Optional oContact As DTOContact = Nothing, Optional IncludeGroupSalePoints As Boolean = False) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders", oEmp.Id, oCod, year, OpcionalGuid(oContact), OpcionalBool(IncludeGroupSalePoints))
    End Function

    Shared Async Function Headers(exs As List(Of Exception),
                                  oEmp As DTOEmp,
                            Optional Cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet,
                            Optional Contact As DTOContact = Nothing,
                            Optional Ccx As DTOCustomer = Nothing,
                            Optional Rep As DTORep = Nothing,
                            Optional Year As Integer = 0,
                            Optional FchCreatedFrom As Date = Nothing,
                            Optional FchCreatedTo As Date = Nothing) As Task(Of List(Of DTOPurchaseOrder))

        Dim retval = Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/headers", oEmp.Id, Cod, OpcionalGuid(Contact), OpcionalGuid(Ccx), OpcionalGuid(Rep), Year, FormatFch(FchCreatedFrom), FormatFch(FchCreatedTo))
        'Dim retval = Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "test") ', oEmp.Id, Cod, OpcionalGuid(Contact), OpcionalGuid(Ccx), OpcionalGuid(Rep), Year, FormatFch(FchCreatedFrom), FormatFch(FchCreatedTo))
        Return retval
    End Function

    Shared Function ExistsSync(exs As List(Of Exception), oUser As DTOUser, FchFrom As Date, FchTo As Date) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "purchaseorders/exists", oUser.Guid.ToString, FormatFch(FchFrom), FormatFch(FchTo))
    End Function

    Shared Function LastOrdersEnteredSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOPurchaseOrder)
        Return Api.FetchSync(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/LastOrdersEntered", oUser.Guid.ToString())
    End Function

    Shared Async Function LastOrdersEntered(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOPurchaseOrder))
        Return Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/LastOrdersEntered", oUser.Guid.ToString())
    End Function

    Shared Async Function Pending(exs As List(Of Exception), oEmp As DTOEmp, Optional cod As DTOPurchaseOrder.Codis = DTOPurchaseOrder.Codis.NotSet, Optional contact As DTOContact = Nothing) As Task(Of List(Of DTOPurchaseOrder))
        Dim retval = Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/Pending", oEmp.Id, cod, OpcionalGuid(contact))
        If contact IsNot Nothing Then
            For Each oOrder In retval
                If oOrder.contact IsNot Nothing Then
                    Select Case cod
                        Case DTOPurchaseOrder.Codis.proveidor
                            oOrder.Proveidor = contact
                        Case Else
                            oOrder.Customer = contact
                    End Select
                End If
                For Each item In oOrder.Items
                    item.PurchaseOrder = oOrder
                Next
            Next
        End If
        Return retval
    End Function

    Shared Async Function PendingForPlatforms(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOPurchaseOrder))
        Dim retval = Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/PendingForPlatforms", oCustomer.Guid.ToString())
        If exs.Count = 0 Then
            For Each oPurchaseOrder In retval
                For Each item In oPurchaseOrder.Items
                    item.PurchaseOrder = oPurchaseOrder
                Next
            Next
        End If
        Return retval
    End Function

    Shared Async Function PendingForPlatforms(exs As List(Of Exception), oHolding As DTOHolding) As Task(Of List(Of DTOPurchaseOrder))
        Dim retval = Await Api.Fetch(Of List(Of DTOPurchaseOrder))(exs, "purchaseorders/PendingForHoldingPlatforms", oHolding.Guid.ToString())
        If exs.Count = 0 Then
            For Each oPurchaseOrder In retval
                For Each item In oPurchaseOrder.Items
                    item.PurchaseOrder = oPurchaseOrder
                Next
            Next
        End If
        Return retval
    End Function

    Shared Async Function StocksAvailableForPlatforms(exs As List(Of Exception), oEmp As DTOEmp, oCcx As DTOCustomer) As Task(Of List(Of DTOStockAvailable))
        Return Await Api.Fetch(Of List(Of DTOStockAvailable))(exs, "purchaseorders/StocksAvailableForPlatforms", oEmp.Id, oCcx.Guid.ToString())
    End Function

    Shared Async Function StocksAvailableForPlatforms(exs As List(Of Exception), oEmp As DTOEmp, oHolding As DTOHolding) As Task(Of List(Of DTOStockAvailable))
        Return Await Api.Fetch(Of List(Of DTOStockAvailable))(exs, "purchaseorders/StocksAvailableForHoldingPlatforms", oEmp.Id, oHolding.Guid.ToString())
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oPurchaseOrders As List(Of DTOPurchaseOrder)) As Task(Of Boolean)
        Dim oGuids = oPurchaseOrders.Select(Function(x) x.Guid).ToList()
        Return Await Api.Delete(Of List(Of Guid))(oGuids, exs, "purchaseorders")
    End Function

    Shared Async Function DeletePendingItems(oPurchaseOrders As List(Of DTOPurchaseOrder), ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim cancel As Boolean
        For Each order As DTOPurchaseOrder In oPurchaseOrders
            If PurchaseOrder.Load(order, exs) Then
                Dim dirty As Boolean = False
                For i As Integer = order.Items.Count - 1 To 0 Step -1
                    Dim item = order.Items(i)
                    If item.Pending > 0 Then
                        item.Qty = item.Qty - item.Pending
                        If item.Qty = 0 Then
                            order.Items.RemoveAt(i)
                        End If
                        dirty = True
                    End If
                Next
                If dirty Then
                    Dim ex2s As New List(Of Exception)
                    Dim pOrder = Await PurchaseOrder.Update(ex2s, order)
                    If exs.Count > 0 Then
                        Dim sMsg As String = String.Format("error al desar comanda {0} de {1}:", order.Num, order.Contact.FullNom)
                        exs.Add(New Exception(sMsg))
                        exs.AddRange(ex2s)
                    End If
                End If
            End If
            ShowProgress(0, oPurchaseOrders.Count - 1, oPurchaseOrders.IndexOf(order), "comanda " & order.Num & " de " & order.Contact.FullNom, cancel)
            If cancel Then Exit For
        Next
        Dim retval As Boolean = True
        Return retval
    End Function

    Shared Function Url(oContact As DTOContact, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "pedidos", oContact.Guid.ToString())
    End Function

    Shared Function ExcelUrl(oCustomer As DTOContact, Optional AbsoluteUrl As Boolean = False)
        Dim retval = UrlHelper.Factory(AbsoluteUrl, "doc", DTODocFile.Cods.openorders, oCustomer.Guid.ToString())
        Return retval
    End Function

    Shared Function ExcelPdcs(oPurchaseOrders As List(Of DTOPurchaseOrder)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("promos")
        With retval
            .AddColumn("Comanda", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Client", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Concepte", MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With
        For Each order In oPurchaseOrders
            Dim oRow As MatHelper.Excel.Row = retval.AddRow()
            With oRow
                .AddCell(order.Num, PurchaseOrder.Url(order, True))
                .AddCell(order.Fch)
                .AddCell(order.Contact.FullNom)
                .AddCell(order.Concept)
            End With
        Next
        Return retval
    End Function

    Shared Function ExcelLinies(exs As List(Of Exception), oPurchaseOrders As List(Of DTOPurchaseOrder)) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("promos")
        With retval
            .AddColumn("Comanda", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("Client", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Marca", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Categoria", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Producte", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Quantitat", MatHelper.Excel.Cell.NumberFormats.Integer)
            .AddColumn("Preu", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Descompte", MatHelper.Excel.Cell.NumberFormats.Percent)
            .AddColumn("Import Descompte", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Import Net", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With
        For Each order In oPurchaseOrders
            PurchaseOrder.Load(order, exs)
            For Each item In order.Items
                Dim oRow As MatHelper.Excel.Row = retval.AddRow()
                With oRow
                    .AddCell(order.Num, PurchaseOrder.Url(order, True))
                    .AddCell(order.Fch)
                    .AddCell(order.Contact.FullNom)
                    .AddCell(DTOProduct.BrandNom(item.Sku))
                    .AddCell(DTOProduct.CategoryNom(item.Sku))
                    .addCell(item.sku.nom.Esp)
                    .AddCell(item.Qty)
                    .AddCellAmt(item.Price)
                    .AddCell(100 * item.Dto)
                    .AddFormula("RC[-3]*RC[-2]*RC[-1]/100")
                    .AddFormula("RC[-3]*RC[-2]-RC[-3]*RC[-2]*RC[-1]/100")
                End With
            Next
        Next
        Return retval
    End Function


End Class
