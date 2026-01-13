Public Class Importacio

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOImportacio)
        Dim retval = Await Api.Fetch(Of DTOImportacio)(exs, "Importacio", oGuid.ToString())
        retval.restoreObjects()
        Return retval
    End Function

    Shared Async Function FromDelivery(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of DTOImportacio)
        Dim retval = Await Api.Fetch(Of DTOImportacio)(exs, "Importacio/FromDelivery", oDelivery.Guid.ToString())
        If retval IsNot Nothing Then
            retval.restoreObjects()
        End If
        Return retval
    End Function

    Shared Async Function ValidateCamion(exs As List(Of Exception), oConfirmation As DTOImportacio.Confirmation) As Task(Of Boolean)
        Dim retval = Await Api.Execute(Of DTOImportacio.Confirmation)(oConfirmation, exs, "Importacio/validateCamion")
        Return retval
    End Function

    Shared Async Function FromAvisCamio(exs As List(Of Exception), oDoc As Xml.XmlDocument) As Task(Of DTOImportacio) 'to DEPRECATE Aganst Confirm
        Dim retval As DTOImportacio = Nothing
        If oDoc IsNot Nothing Then
            Dim sNum As String = oDoc.DocumentElement.GetAttribute("NUMERO")
            If TextHelper.VbIsNumeric(sNum) Then
                Dim sFch As String = oDoc.DocumentElement.GetAttribute("DATE")
                Dim sGuid As String = oDoc.DocumentElement.GetAttribute("Guid")
                Dim DtFch As Date
                If DateTime.TryParse(sFch, DtFch) Then
                    retval = Await Importacio.Find(exs, New Guid(sGuid))
                    retval.restoreObjects()
                Else
                    exs.Add(New Exception("Document incorrecte. Data de la remesa no válida (" & sFch & ")"))
                End If
            Else
                exs.Add(New Exception("Document incorrecte. Falta el número de la remesa"))
            End If
        End If
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oImportacio As DTOImportacio) As Boolean
        If Not oImportacio.IsLoaded And Not oImportacio.IsNew Then
            Dim pImportacio = Api.FetchSync(Of DTOImportacio)(exs, "Importacio", oImportacio.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOImportacio)(pImportacio, oImportacio, exs)
                oImportacio.restoreObjects()
            End If
        End If
        If oImportacio.previsions IsNot Nothing Then
            For Each oprevisio In oImportacio.previsions
                oprevisio.importacio = oImportacio
            Next
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oImportacio As DTOImportacio, exs As List(Of Exception)) As Task(Of Integer)
        Return Await Api.Update(Of DTOImportacio, Integer)(oImportacio, exs, "Importacio")
        oImportacio.IsNew = False
    End Function

    Shared Async Function Confirm(exs As List(Of Exception), oConfirmation As DTOImportacio.Confirmation, ShowProgress As ProgressBarHandler) As Task(Of Boolean)
        'quan Vivace passa el fitxer xml confirmant la mercancia que ha arribat
        Dim cancelRequest As Boolean = False

        ShowProgress(0, 100, 0, "llegint importació", CancelRequest)
        Dim oImportacio As DTOImportacio = Await Importacio.Find(exs, oConfirmation.Importacio.Guid)
        If exs.Count = 0 Then
            ShowProgress(0, 100, 0, "descarregant pla comptable", cancelRequest)
            oConfirmation.Importacio = oImportacio
            Dim oPlan = DTOApp.Current.PgcPlan
            Dim oCtas = Await PgcCtas.All(exs, oPlan)

            If exs.Count = 0 Then
                Dim oItem As DTOImportacioItem = Nothing
                Dim oProveidor = oImportacio.proveidor
                ShowProgress(0, 100, 0, "llegint les factures de la importacio", cancelRequest)
                Dim oInvoicesReceived = Await FEB.InvoicesReceived.FromConfirmation(exs, oConfirmation)
                If exs.Count = 0 Then
                    Dim iLast = oInvoicesReceived.Count
                    For Each invoice In oInvoicesReceived
                        Dim idx = oInvoicesReceived.IndexOf(invoice)

                        ShowProgress(0, iLast, idx, String.Format("llegint la factura {0}", invoice.DocNum), cancelRequest)
                        invoice = Await InvoiceReceived.Find(exs, invoice.Guid)
                        'If invoice.DocNum = "INV20521428" Then Stop

                        ShowProgress(0, iLast, idx, String.Format("verificant que la factura {0} no estigui ja entrada amb antelació", invoice.DocNum), cancelRequest)
                        If Await CheckFacturaDoesNotExists(exs, invoice, oConfirmation.User.Emp) Then

                            ShowProgress(0, iLast, idx, String.Format("redactant l'albarà d'entrada per la factura {0}", invoice.DocNum), cancelRequest)
                            Dim oDelivery = Await InvoiceReceived.Delivery(exs, invoice, oConfirmation.User)
                            oDelivery.Importacio = oImportacio

                            ShowProgress(0, iLast, idx, String.Format("desant l'albarà d'entrada per la factura {0}", invoice.DocNum), cancelRequest)
                            If Await Delivery.Update(exs, oDelivery) Then

                                'afegeix l'albarà a la llista d'albarans de la importació
                                oItem = DTOImportacioItem.Factory(oImportacio, DTOImportacioItem.SourceCodes.alb, oDelivery.Guid)
                                oItem.amt = oDelivery.baseImponible
                                oImportacio.items.Add(oItem)

                                ShowProgress(0, iLast, idx, String.Format("desant les quantitats confirmades a la factura {0}", invoice.DocNum), cancelRequest)

                                'apunta a la factura les quantitats confirmades 
                                For Each item In invoice.Items
                                    Dim oGuid = item.Guid
                                    Dim xItem = oConfirmation.Items.FirstOrDefault(Function(x) x.Guid.Equals(oGuid))
                                    If xItem Is Nothing Then
                                        exs.Add(New Exception(String.Format("la linia {0} de la factura {1} no s'ha trobat al fitxer de confirmació del magatzem", invoice.Items.IndexOf(item) + 1, invoice.DocNum)))
                                    Else
                                        item.QtyConfirmed = xItem.QtyConfirmed
                                        xItem.SkuNom = item.SkuNom
                                    End If
                                Next

                                'desa la factura
                                If Await InvoiceReceived.Update(exs, invoice) Then
                                    ShowProgress(0, iLast, idx, String.Format("generant l'assentament de la factura {0}", invoice.DocNum), cancelRequest)
                                    Await GenerateCca(exs, invoice, oImportacio, oCtas, oConfirmation.User)

                                    'treu de les previsions les linies corresponents a aquesta factura
                                    oImportacio.previsions.RemoveAll(Function(x) x.InvoiceReceivedItem IsNot Nothing AndAlso invoice.Items.Any(Function(y) y.Guid.Equals(x.InvoiceReceivedItem.Guid)))
                                Else
                                    exs.Add(New Exception("error al desar el numero de remesa de importació a la factura " & invoice.DocNum))
                                End If

                            End If

                        End If
                    Next

                    'desa la importacio amb els items afegits i les previsions retirades
                    ShowProgress(0, iLast, iLast, String.Format("desant els canvis a la importació {0}", oImportacio.id), cancelRequest)
                    Await Importacio.Update(oImportacio, exs)
                End If
            End If
        End If


        Return exs.Count = 0
    End Function

    Public Shared Async Function GenerateCca(exs As List(Of Exception), invoice As DTOInvoiceReceived, oImportacio As DTOImportacio, oCtas As List(Of DTOPgcCta), oUser As DTOUser) As Task(Of DTOCca)
        Dim retval As DTOCca = Nothing

        Dim oProveidor = Await Proveidor.Find(oImportacio.proveidor.Guid, exs)

        'genera el Pdf de la factura
        Dim oStream As Byte() = LegacyHelper.PdfEdiInvoiceReceived.Factory(invoice, oUser.Emp)
        Dim oDocFile = LegacyHelper.DocfileHelper.Factory(exs, oStream)

        'genera l'assentament de la factura
        Dim oCca = DTOCca.Factory(invoice.Fch, oUser, DTOCca.CcdEnum.FacturaProveidor)
        With oCca
            .Concept = String.Format("fra.{0} (R.{1}) de {2}", invoice.DocNum, oImportacio.id, oProveidor.Nom)
            .DocFile = oDocFile
            .BookFra = DTOBookFra.Factory(oCca, oProveidor)
            With .BookFra
                .tipoFra = "F1" 'rectificar a ma si no es aixi
                .cta = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                .contact = oProveidor
                .fraNum = invoice.DocNum
                .dsc = .cta.Nom.Esp

                If DTOContact.isIVASujeto(oProveidor) Then
                    Dim DcTipusIva = DTOTax.closest(DTOTax.Codis.iva_Standard, invoice.Fch).tipus
                    .claveRegimenEspecialOTrascendencia = "01"
                    Dim oSujeto As New DTOBaseQuota(invoice.TaxBase, DcTipusIva)
                    oSujeto.source = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                    .ivaBaseQuotas.Add(oSujeto)
                Else
                    .claveRegimenEspecialOTrascendencia = DTOContact.claveRegimenEspecialOTrascendencia(oProveidor)
                    Dim oExento As New DTOBaseQuota(invoice.TaxBase)
                    oExento.source = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Compras)
                    .ivaBaseQuotas.Add(oExento)
                    .claveExenta = DTOContact.claveCausaExempcio(oProveidor)
                End If

                AddIvas(oCca, .ivaBaseQuotas, oProveidor, oCtas)

                If .import = DTOInvoice.ExportCods.intracomunitari Then
                    Dim oBaseExempta As DTOBaseQuota = .ivaBaseQuotas.FirstOrDefault(Function(x) x.tipus = 0)
                    If oBaseExempta IsNot Nothing Then
                        AddIvaIntracomunitari(oCca, oBaseExempta.baseImponible, oCtas)
                    End If
                End If
            End With
        End With

        'Dim oCtaTotalCod = DTOPgcCta.getCtaProveedors(invoice.Cur)
        'Dim oCtaTotal = Await PgcCta.FromCod(oCtaTotalCod, oUser.Emp, exs)
        Dim oCtaTotal = oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcCta.getCtaProveedors(invoice.Cur))
        If oCtaTotal Is Nothing Then
            exs.Add(New Exception("error al llegir el compte del total"))
        Else
            AddTotal(oCca, oCtaTotal, oProveidor, invoice.TaxBase)

            Dim oPnds As List(Of DTOPnd) = Nothing
            Dim oPnd As DTOPnd = Nothing
            oPnd = DTOPnd.Factory(oUser.Emp)
            With oPnd
                .Contact = oProveidor
                .Fch = invoice.Fch
                .Cta = oCtaTotal
                .Cca = oCca
                .Vto = DTOPaymentTerms.vto(oProveidor.paymentTerms, invoice.Fch)
                .Amt = DTOAmt.Factory(invoice.TaxBase)
                '.Cfp = ComboBoxCfp.SelectedValue
                .Cfp = oProveidor.paymentTerms.Cod
                .Yef = oCca.Fch.Year
                .FraNum = invoice.DocNum
                .Fpg = DTOPaymentTerms.Text(oProveidor.paymentTerms, oProveidor.Lang, .Vto)
                .Cod = DTOPnd.Codis.Creditor
                .Status = DTOPnd.StatusCod.pendent
            End With
            oPnds = New List(Of DTOPnd)
            oPnds.Add(oPnd)

            'afegeix l'assentament a la llista de factures de la importació
            Dim oItem = DTOImportacioItem.Factory(oImportacio, DTOImportacioItem.SourceCodes.fra, oCca.Guid)
            oItem.amt = oCca.BookFra.baseDevengada
            oImportacio.items.Add(oItem)

            'desa l'assentament i deixa'l pendent de pagament
            retval = Await Proveidor.SaveFactura(exs, oCca, oPnds, oImportacio)
            If retval Is Nothing Then
                exs.Add(New Exception("Error al desar la factura"))
            End If
        End If
        Return retval
    End Function

    Private Shared Sub AddIvaIntracomunitari(ByRef oCca As DTOCca, oBaseImponible As DTOAmt, oCtas As List(Of DTOPgcCta))
        Dim oTaxIva = DTOApp.Current.Taxes.FirstOrDefault(Function(x) x.codi = DTOTax.Codis.iva_Standard)
        If oTaxIva IsNot Nothing Then
            If oBaseImponible IsNot Nothing AndAlso oBaseImponible.IsNotZero Then
                Dim oQuota As DTOAmt = oBaseImponible.Percent(oTaxIva.tipus)
                oCca.AddDebit(oQuota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatIntracomunitari))
                oCca.AddCredit(oQuota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaRepercutitIntracomunitari))
            End If
        End If
    End Sub

    Private Shared Sub AddIvas(ByRef oCca As DTOCca, oBaseIvas As List(Of DTOBaseQuota), oProveidor As DTOProveidor, oCtas As List(Of DTOPgcCta))
        For Each item As DTOBaseQuota In oBaseIvas
            Dim oCta As DTOPgcCta = item.source
            oCca.AddDebit(item.baseImponible, oCta, oProveidor)
            If item.quota IsNot Nothing Then
                If item.quota.IsNotZero Then
                    oCca.AddDebit(item.quota, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.IvaSoportatNacional))
                End If
            End If
        Next
    End Sub

    Private Shared Sub AddTotal(ByRef oCca As DTOCca, ByVal oCta As DTOPgcCta, ByVal oContact As DTOContact, oAmt As DTOAmt)
        If oAmt.IsNotZero Then
            oCca.AddSaldo(oCta, oContact)
        End If
    End Sub

    Private Shared Async Function CheckFacturaDoesNotExists(exs As List(Of Exception), oInvoice As DTOInvoiceReceived, oEmp As DTOEmp) As Task(Of Boolean)
        Dim retval As Boolean
        Dim oProveidor = New DTOProveidor(oInvoice.Proveidor.Guid)
        Dim oDuplicada As DTOCca = Await Proveidor.CheckFacturaAlreadyExists(exs, oProveidor, DTOExercici.Current(oEmp), oInvoice.DocNum)
        If oDuplicada Is Nothing Then
            retval = True
        Else
            Dim sWarn As String = "aquesta factura ja está entrada" & vbCrLf
            sWarn = sWarn & String.Format("per {0} el {1:dd/MM/yy} a las {2:HH:mm}", DTOUser.NicknameOrElse(oDuplicada.UsrLog.UsrCreated), oDuplicada.UsrLog.FchCreated, oDuplicada.UsrLog.FchCreated)
            sWarn = sWarn & vbCrLf & " (assentament " & oDuplicada.Id.ToString & ")"
            sWarn = sWarn & vbCrLf & oDuplicada.Concept
            exs.Add(New Exception(sWarn))
        End If
        Return retval
    End Function

    Shared Async Function Confirm_Old(exs As List(Of Exception), oConfirmation As DTOImportacio.Confirmation) As Task(Of Boolean)
        'quan Vivace passa el fitxer xml confirmant la mercancia que ha arribat
        Return Await Api.Execute(Of DTOImportacio.Confirmation, Boolean)(oConfirmation, exs, "Importacio/Confirm")
    End Function

    Shared Async Function UnConfirm(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Importacio/UnConfirm", oImportacio.Guid.ToString)
    End Function

    Shared Async Function RevertPrevisions(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Importacio/Previsions/revert", oImportacio.Guid.ToString)
    End Function

    Shared Async Function Entrada(exs As List(Of Exception), oDelivery As DTODelivery) As Task(Of Boolean)
        Return Await Api.Execute(Of DTODelivery, Boolean)(oDelivery, exs, "Importacio/Entrada")
    End Function

    Shared Async Function LogAvisTrp(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Importacio/LogAvisTrp", oImportacio.Guid.ToString())
    End Function

    Shared Async Function Delete(oImportacio As DTOImportacio, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOImportacio)(oImportacio, exs, "Importacio")
    End Function

    Shared Async Function MailMessageOrdenDeCarga(exs As List(Of Exception), oEmp As DTOEmp, oImportacio As DTOImportacio) As Task(Of DTOMailMessage)
        Dim retval As DTOMailMessage = Nothing
        If Importacio.Load(exs, oImportacio) Then
            Dim oLang As DTOLang = DTOApp.current.lang
            Dim sRecipients = Await Subscriptors.Recipients(exs, oEmp, DTOSubscription.Wellknowns.TransportOrdenDeCarga, oImportacio.transportista)
            If exs.Count = 0 Then
                If sRecipients.Count = 0 Then
                    Dim oEmails = Await Emails.All(exs, oImportacio.Transportista)
                    sRecipients = oEmails.Select(Function(x) x.EmailAddress).ToList
                End If

                retval = DTOMailMessage.Factory(sRecipients)
                Dim oTxt = Await Txt.Find(DTOTxt.Ids.MailTrpLoad, exs)

                Dim oPlataformaDeCarga = DTOImportacio.PlataformaDeCargaOProveidor(oImportacio)
                If Contact.Load(oPlataformaDeCarga, exs) Then
                    retval.Body = oTxt.ToHtml(DTOLang.ESP,
                                          DTOImportacio.FormattedId(oImportacio),
                                          oEmp.Org.Nom,
                                          oImportacio.Transportista.Nom,
                                          String.Format("{0}<br/>{1}", oPlataformaDeCarga.Nom, DTOAddress.FullHtml(oPlataformaDeCarga.Address)),
                                          String.Format("{0}<br/>{1}", oEmp.Mgz.Nom, DTOAddress.FullHtml(oEmp.Mgz.Address)),
                                          TextHelper.VbFormat(DTO.GlobalVariables.Now(), "dd/MM/yyyy"),
                                          TextHelper.VbFormat(oImportacio.FchETD, "dd/MM/yyyy"),
                                          TextHelper.VbFormat(oImportacio.FchETA, "dd/MM/yyyy"),
                                          TextHelper.VbFormat(oImportacio.FchETA, "HH:mm"),
                                          TextHelper.VbFormat(oImportacio.M3, "0.00 \m\3")
                                            )

                    retval.Subject = String.Format("Orden {0} de Carga y Transporte", DTOImportacio.FormattedId(oImportacio))
                End If

            End If
        End If
        Return retval
    End Function

    Shared Async Function InvoicesReceived(exs As List(Of Exception), oImportacio As DTOImportacio) As Task(Of List(Of DTOInvoiceReceived))
        Return Await Api.Fetch(Of List(Of DTOInvoiceReceived))(exs, "Importacio/InvoicesReceived", oImportacio.Guid.ToString)
    End Function


    Shared Async Function DiscrepancyReport(exs As List(Of Exception), oInvoicesReceived As List(Of DTOInvoiceReceived), oImportacio As DTOImportacio) As Task(Of MatHelper.Excel.Sheet)
        Dim retval As New MatHelper.Excel.Sheet("DiscrepancyReport " & oImportacio.id)
        With retval
            .AddColumn("Item code")
            .AddColumn("Item name")
            .AddColumn("Shipment")
            .AddColumn("Packing slip")
            .AddColumn("Invoice number")
            .AddColumn("Quantity invoiced")
            .AddColumn("Quantity received")
            .AddColumn("Difference")
        End With
        For Each invoice In oInvoicesReceived
            invoice = Await InvoiceReceived.Find(exs, invoice.Guid)
            Dim discrepancyItems = invoice.Items.Where(Function(x) x.Qty <> x.QtyConfirmed).ToList()
            For Each item In discrepancyItems
                Dim oRow = retval.AddRow()
                oRow.AddCell(item.SkuRef)
                oRow.AddCell(item.SkuNom)
                oRow.AddCell()
                oRow.AddCell(item.DeliveryNote)
                oRow.AddCell(invoice.DocNum)
                oRow.AddCell(item.Qty)
                oRow.AddCell(item.QtyConfirmed)
                oRow.AddFormula("RC[-2]-RC[-1]")
            Next
        Next
        Return retval
    End Function

    Shared Async Function ExcelGoods(exs As List(Of Exception), oImportacio As DTOImportacio, oLang As DTOLang) As Task(Of MatHelper.Excel.Sheet)
        Dim retval As New MatHelper.Excel.Sheet("Remesa Importacio " & oImportacio.id)
        If Importacio.Load(exs, oImportacio) Then
            Dim BlShowCur As Boolean
            If oImportacio.amt IsNot Nothing Then
                If Not oImportacio.amt.Cur.isEur Then
                    BlShowCur = True
                End If
            End If

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(oLang.Tradueix("albarán", "albará", "delivery"))
            oRow.AddCell(oLang.Tradueix("referencia", "referència", "sku code"))
            oRow.AddCell(oLang.Tradueix("producto", "producte", "product"))
            oRow.AddCell(oLang.Tradueix("cantidad", "quantitat", "quantity"))
            oRow.AddCell("Eur")
            If BlShowCur And (oImportacio.amt IsNot Nothing) Then oRow.AddCell(oImportacio.amt.Cur.Tag)
            oRow.AddCell(oLang.Tradueix("Dto", "Dte", "Dct"))

            For Each document In oImportacio.items
                If document.srcCod = DTOImportacioItem.SourceCodes.alb Then
                    Dim oDelivery = Await Delivery.Find(document.Guid, exs)
                    If exs.Count = 0 Then
                        For Each item As DTODeliveryItem In oDelivery.Items
                            oRow = retval.AddRow()
                            oRow.AddCell(oDelivery.Id)
                            oRow.AddCell(item.Sku.RefProveidor)
                            oRow.AddCell(item.Sku.NomProveidor)
                            oRow.AddCell(item.Qty)
                            oRow.AddCell(item.Price.Eur)
                            If BlShowCur Then oRow.AddCell(item.Price.Val)
                            oRow.AddCell(item.Dto)
                        Next
                    End If
                End If
            Next

        End If

        Return retval
    End Function


End Class

Public Class Importacions
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, Optional oProveidor As DTOProveidor = Nothing) As Task(Of List(Of DTOImportacio))
        Dim retval = Await Api.Fetch(Of List(Of DTOImportacio))(exs, "Importacions", oEmp.Id, year, OpcionalGuid(oProveidor))
        For Each oImportacio In retval
            oImportacio.restoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Weeks(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOImportacio))
        Return Await Api.Fetch(Of List(Of DTOImportacio))(exs, "Importacions/weeks", oEmp.Id)
    End Function

    Shared Async Function Transits(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOImportTransit))
        Return Await Api.Fetch(Of List(Of DTOImportTransit))(exs, "Importacions/transits", oEmp.Id)
    End Function

    Shared Async Function Excel(oEmp As DTOEmp, exs As List(Of Exception)) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "Importacions/Excel", oEmp.Id)
    End Function

    Shared Function ExcelUrl(oEmp As DTOEmp) As String
        Return ApiUrl("importacions/Excel", oEmp.Id)
    End Function

    Shared Function Excel(oImportacions As List(Of DTOImportacio), oLang As DTOLang) As MatHelper.Excel.Sheet
        Dim retval As New MatHelper.Excel.Sheet("Remeses de Importacio")
        With retval
            .AddColumn(oLang.Tradueix("remesa", "remesa", "Id"))
            .AddColumn("ETD", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("ETA", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn(oLang.Tradueix("valor", "valor", "value"), MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn(oLang.Tradueix("bultos", "bultos", "packages"))
            .AddColumn(oLang.Tradueix("peso", "pes", "weight"), MatHelper.Excel.Cell.NumberFormats.Kg)
            .AddColumn(oLang.Tradueix("volumen", "volum", "volume"), MatHelper.Excel.Cell.NumberFormats.m3D2)
            .AddColumn(oLang.Tradueix("transporte", "transport", "transport"), MatHelper.Excel.Cell.NumberFormats.Euro)

        End With
        For Each item In oImportacions
            Dim oCostMercancia As DTOAmt = DTOImportacio.CostMercancia(item)
            Dim oCostTransport As DTOAmt = DTOImportacio.CostTransport(item)

            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            With oRow
                .AddCell(item.Id)
                .AddCell(item.FchETD)
                .AddCell(item.FchETA)
                .AddCell(oCostMercancia.Eur)
                .AddCell(item.Bultos)
                .AddCell(item.Kg)
                .AddCell(item.M3)
                .AddCell(oCostTransport.Eur)
            End With
        Next

        Return retval
    End Function

End Class
