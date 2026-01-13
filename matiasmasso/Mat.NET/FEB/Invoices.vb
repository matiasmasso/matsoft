Public Class Invoice
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOInvoice)
        Dim retval = Await Api.Fetch(Of DTOInvoice)(exs, "Invoice", oGuid.ToString())

        For Each oDelivery In retval.Deliveries
            For Each item In oDelivery.Items
                item.Delivery = oDelivery
            Next
        Next
        Return retval
    End Function

    Shared Async Function FromNum(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, num As Integer) As Task(Of DTOInvoice)
        Return Await Api.Fetch(Of DTOInvoice)(exs, "Invoice/fromNum", oEmp.Id, year, num)
    End Function

    Shared Function FromNumSync(exs As List(Of Exception), oEmp As DTOEmp, year As Integer, num As Integer) As DTOInvoice
        Return Api.FetchSync(Of DTOInvoice)(exs, "Invoice/fromNum", oEmp.Id, year, num)
    End Function


    Shared Function Load(ByRef oInvoice As DTOInvoice, exs As List(Of Exception)) As Boolean
        If Not oInvoice.IsLoaded And Not oInvoice.IsNew Then
            Dim pInvoice = Api.FetchSync(Of DTOInvoice)(exs, "Invoice", oInvoice.Guid.ToString())
            If exs.Count = 0 And pInvoice IsNot Nothing Then
                DTOBaseGuid.CopyPropertyValues(Of DTOInvoice)(pInvoice, oInvoice, exs)
            End If
            For Each oDelivery In oInvoice.Deliveries
                For Each item In oDelivery.Items
                    item.Delivery = oDelivery
                Next
            Next
        End If

        For Each oDelivery In oInvoice.Deliveries
            For Each item In oDelivery.Items
                item.Delivery = oDelivery
            Next
        Next

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oInvoice As DTOInvoice, oDocfile As DTODocFile) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(oInvoice, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If oInvoice.cca.docFile IsNot Nothing Then
                oMultipart.AddFileContent("docfile_thumbnail", oDocfile.Thumbnail)
                oMultipart.AddFileContent("docfile_stream", oDocfile.Stream)
            End If
            retval = Await Api.Upload(oMultipart, exs, "Invoice")
        End If
        Return retval
    End Function

    Shared Function CcaBuilder(exs As List(Of Exception), oInvoice As DTOInvoice, oDocfile As DTODocFile, oUser As DTOUser, oCtas As List(Of DTOPgcCta)) As DTOCca
        Dim retval As DTOCca = Nothing
        oDocfile.fch = oInvoice.fch

        If oInvoice.deliveries.Count > 0 Then
            Dim oCashCod As DTOCustomer.CashCodes = oInvoice.deliveries.First.CashCod
            Dim deutor = If(oInvoice.Deutor = Nothing, oInvoice.Customer, New DTOCustomer(oInvoice.Deutor))

            If oInvoice.cca Is Nothing Then
                retval = DTOCca.Factory(oInvoice.fch, oUser, DTOCca.CcdEnum.FacturaNostre)
            Else
                Cca.Load(oInvoice.cca, exs)
                retval = oInvoice.cca
                retval.UsrLog.usrLastEdited = oUser
            End If

            With retval
                .docFile = oDocfile
                .ref = oInvoice
                .cdn = oInvoice.num
                .items = New List(Of DTOCcb)
                If oInvoice.baseImponible.IsNegative Then
                    .concept = String.Format("nuestra factura rectificativa R{0}", oInvoice.num)
                    retval.AddDebit(oInvoice.BaseImponible.Inverse, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.DevolucionsDeVendes), deutor)
                Else
                    .concept = String.Format("nuestra factura numero {0}", oInvoice.num)
                    retval.AddCredit(oInvoice.BaseImponible, oCtas.FirstOrDefault(Function(x) x.Codi = DTOPgcPlan.Ctas.Vendes), deutor)
                End If

                For Each oIvaBaseQuota As DTOTaxBaseQuota In oInvoice.ivaBaseQuotas
                    retval.AddCredit(oIvaBaseQuota.Quota, oCtas.FirstOrDefault(Function(x) x.codi = DTOTax.CtaCod(oIvaBaseQuota.Tax.Codi)))
                Next
            End With


            Dim oPnd As DTOPnd = Nothing
            If oCashCod = DTOCustomer.CashCodes.credit Then
                oPnd = DTOPnd.Factory(oInvoice, oCtas.FirstOrDefault(Function(x) x.codi = DTOInvoice.CtaDeb(DTOInvoice.CashCod(oInvoice))))
                retval.pnds = New List(Of DTOPnd)
                retval.pnds.Add(oPnd)
            End If

            retval.AddSaldo(oCtas.FirstOrDefault(Function(x) x.Codi = DTOInvoice.CtaDeb(oCashCod)), deutor, oPnd)
        End If
        Return retval
    End Function

    Shared Async Function Delete(oInvoice As DTOInvoice, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOInvoice)(oInvoice, exs, "Invoice")
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oEmp As DTOEmp, oDeliveries As List(Of DTODelivery)) As Task(Of DTOInvoice)
        Dim retval As DTOInvoice = Nothing
        If oDeliveries Is Nothing OrElse oDeliveries.Count = 0 Then
            exs.Add(New Exception("No hi han albarans per facturar"))
        Else
            For Each oDelivery In oDeliveries
                Delivery.Load(oDelivery, exs)
                Dim idx = oDeliveries.IndexOf(oDelivery)
            Next
            If Deliveries.HaveSameCcx(exs, oDeliveries) Then

                Dim oExercici As DTOExercici = DTOExercici.FromYear(oEmp, oDeliveries.First.Fch.Year)
                Dim oLastInvoicesEachSerie = Await Invoices.LastEachSerie(exs, oExercici)
                Dim lastFch As Date = oDeliveries.Last.Fch
                Dim DtMinFch = New Date(oExercici.Year, 1, 1)
                Dim oTaxes As List(Of DTOTax) = DTOTax.closest(lastFch)

                Dim oFirstDelivery = oDeliveries.First
                Dim oCustomer = oDeliveries.First.Customer
                If oCustomer.IsConsumer And oFirstDelivery.ConsumerTicket IsNot Nothing Then
                    Dim oConsumerTicket = oFirstDelivery.ConsumerTicket
                    ConsumerTicket.Load(exs, oConsumerTicket)
                    'oCustomer.Nom = oConsumerTicket.RaoSocial()
                    'oCustomer.Nifs = DTONif.Collection.Factory(oConsumerTicket.Nif, DTONif.Cods.Nif)
                    'oCustomer.Address = oFirstDelivery.ConsumerTicket.FiscalAddress()
                    oCustomer.PaymentTerms = New DTOPaymentTerms
                    oCustomer.PaymentTerms.cod = DTOPaymentTerms.CodsFormaDePago.comptat
                    retval = DTOInvoice.Factory(oCustomer)
                    With retval
                        .Nom = oConsumerTicket.RaoSocial()
                        .Nifs = DTONif.Collection.Factory(oConsumerTicket.Nif, DTONif.Cods.Nif)
                        .Adr = oFirstDelivery.ConsumerTicket.FiscalAddress().Text
                        .Zip = oFirstDelivery.ConsumerTicket.FiscalAddress().Zip
                        .Lang = oConsumerTicket.Lang
                        .Concepte = DTOInvoice.getConcepte(oDeliveries.First)
                        .Deliveries = oDeliveries

                        DTOInvoice.SetRegimenEspecialOTrascendencia(retval)
                        DTOInvoice.setImports(retval, oTaxes)
                        DTOInvoice.setNumberAndFch(retval, oLastInvoicesEachSerie, DtMinFch)
                        If oConsumerTicket.MarketPlace IsNot Nothing Then
                            retval.Ob1 = "Operación realizada en " & oConsumerTicket.MarketPlace.Nom
                        End If
                        .SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(retval)


                        Select Case oDeliveries.First.Cod 'check diferents formes de pagament
                            Case DTOPurchaseOrder.Codis.reparacio
                                .Concepte = DTOInvoice.Conceptes.servicios
                            Case Else
                                .Concepte = DTOInvoice.Conceptes.ventas
                        End Select
                    End With
                Else
                    Dim oFirstCcx = Customer.CcxOrMe(exs, oDeliveries.First.customer)
                    If Customer.Load(oFirstCcx, exs) Then
                        retval = DTOInvoice.Factory(oFirstDelivery)
                        With retval
                            .Deliveries = oDeliveries
                            DTOInvoice.setImports(retval, oTaxes)
                            DTOInvoice.setNumberAndFch(retval, oLastInvoicesEachSerie, DtMinFch)
                            retval = Await Invoice.SetFormaDePago(exs, oEmp, retval)

                            '.Nom = oFirstCcx.Nom
                            '.Nifs = oFirstCcx.Nifs
                            '.Adr = oFirstCcx.Address.Text
                            '.Zip = oFirstCcx.Address.Zip
                            '.Lang = oFirstCcx.Lang
                            '.Concepte = DTOInvoice.getConcepte(oDeliveries.First)
                            '.ExportCod = oDeliveries.First.ExportCod
                            '.Incoterm = oDeliveries.First.Incoterm
                            'DTOInvoice.SetRegimenEspecialOTrascendencia(retval)
                            '.SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(retval)

                        End With
                    End If
                End If

            Else
                exs.Add(New Exception("Els albarans de diferents clients no es poden facturar en una mateixa factura"))
            End If
        End If
        Return retval
    End Function


    Shared Function Url(oInvoice As DTOInvoice, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If oInvoice IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "factura", oInvoice.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Async Function Pdf(exs As List(Of Exception), oInvoice As DTOInvoice) As Task(Of Byte())
        Return Await Api.FetchBinary(exs, "Invoice/pdf", oInvoice.Guid.ToString())
    End Function

    Shared Function MailUrl(oInvoice As DTOInvoice, Optional AbsoluteUrl As Boolean = True) As String
        Dim retval As String = ""
        If oInvoice IsNot Nothing Then
            retval = UrlHelper.Factory(AbsoluteUrl, "mail/factura", oInvoice.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Function PdfUrl(value As DTOInvoice, Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = ""
        If value IsNot Nothing Then
            retval = DocFile.DownloadUrl(value.DocFile, AbsoluteUrl)
        End If
        Return retval
    End Function

    Shared Async Function Factory(exs As List(Of Exception), oEmp As DTOEmp, oDeliveries As List(Of DTODelivery), DtMinFch As DateTime, lastInvoicesEachSerie As List(Of DTOInvoice), Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of List(Of DTOInvoice))
        Dim retval As New List(Of DTOInvoice)
        Dim CancelRequest As Boolean
        Dim lastFch As Date = oDeliveries.Last.Fch
        Dim oTaxes As List(Of DTOTax) = DTOTax.closest(lastFch)

        For Each oDelivery As DTODelivery In oDeliveries

            Dim oInvoice = DTOInvoice.Factory(oDelivery)
            With oInvoice
                DTOInvoice.setImports(oInvoice, oTaxes)
                DTOInvoice.setNumberAndFch(oInvoice, lastInvoicesEachSerie, DtMinFch)
                oInvoice = Await Invoice.SetFormaDePago(exs, oEmp, oInvoice)
            End With
            retval.Add(oInvoice)

            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oDeliveries.Count, oDeliveries.IndexOf(oDelivery), "redactant factura " & oInvoice.Num, CancelRequest)
                If CancelRequest Then Exit For
            End If

        Next
        Return retval
    End Function

    Shared Async Function SetFormaDePago(exs As List(Of Exception), oEmp As DTOEmp, oInvoice As DTOInvoice) As Task(Of DTOInvoice)
        Dim oCustomer As DTOCustomer = oInvoice.Customer
        'If oCustomer.Guid.ToString.ToUpper = "A01BA973-DA56-4E5A-84EA-5A277DE2F2B8" Then Stop

        Dim oLang As DTOLang = oCustomer.Lang
        Dim oDelivery As DTODelivery = oInvoice.Deliveries.First
        Select Case oDelivery.CashCod
            Case DTOCustomer.CashCodes.TransferenciaPrevia
                oInvoice.Cfp = DTOPaymentTerms.CodsFormaDePago.Comptat
                oInvoice.Fpg = oLang.Tradueix("Cobrado por transferencia bancaria", "Cobrat per transferència bancària", "Charged by bank transfer", "Cobrado por transferência bancária")
            Case DTOCustomer.CashCodes.Visa
                oInvoice.Cfp = DTOPaymentTerms.CodsFormaDePago.Comptat
                oInvoice.Fpg = oLang.Tradueix("Cobrado por tarjeta de crédito", "Cobrat per tarja de crèdit", "Charged by credit card", "Cobrado por cartão de crédito")
            Case DTOCustomer.CashCodes.Reembols
                oInvoice.Cfp = DTOPaymentTerms.CodsFormaDePago.Comptat
                oInvoice.Fpg = oLang.Tradueix("Cobrado contra reembolso", "Cobrat contra reembolsament", "Cash on delivery", "COD cobrado")
            Case DTOCustomer.CashCodes.Diposit
                oInvoice.Cfp = DTOPaymentTerms.CodsFormaDePago.Comptat
                oInvoice.Fpg = oLang.Tradueix("Descontado del depósito", "Descomptat del dipòsit", "Discounted from deposit", "Deduzido do depósito")
            Case DTOCustomer.CashCodes.credit
                oCustomer.Ccx = Customer.CcxOrMe(exs, oCustomer)
                If exs.Count = 0 Then
                    If oCustomer.Ccx.PaymentTerms.Cod = DTOPaymentTerms.CodsFormaDePago.notSet Then
                        oInvoice.AddException(DTOInvoiceException.Cods.missingPaymentTerms, "client sense forma de pago")
                    Else
                        DTOInvoice.SetVto(oInvoice)
                        Dim sVto As String = TextHelper.VbFormat(oInvoice.vto, "dd/MM/yy")
                        oInvoice.Cfp = oCustomer.PaymentTerms.Cod
                        Dim obs As New List(Of String)
                        Select Case oCustomer.PaymentTerms.Cod
                            Case DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria
                                oInvoice.Fpg = oLang.Tradueix("Efecto domiciliado Sepa Core vto.", "Efecte domiciliat Sepa Core venciment ", "Sepa Core Direct Debit due ", "Efecto domiciliado Sepa Core vto.") & sVto
                                obs = Await IbanObs(exs, oInvoice)
                            Case DTOPaymentTerms.CodsFormaDePago.EfteAndorra
                                oInvoice.Fpg = oLang.Tradueix("Efecto domiciliado vto.", "Efecte domiciliat venciment ", "Direct Debit due ", "Efecto domiciliado vto.") & sVto
                                obs = Await IbanObs(exs, oInvoice)
                            Case DTOPaymentTerms.CodsFormaDePago.ReposicioFons
                                oInvoice.Fpg = oLang.Tradueix("Reposición fondos antes del ", "Reposiciò fons abans del ", "Check before ", "Cheque antes de ") & sVto
                            Case DTOPaymentTerms.CodsFormaDePago.Transferencia
                                oInvoice.Fpg = oLang.Tradueix("Transferencia antes del ", "Transferència abans del ", "Bank transfer before ", "Transferencia antes de ") & sVto
                                Dim oBancGuid As Guid = [Default].EmpGuidSync(oEmp, DTODefault.Codis.bancToReceiveTransfers, exs)
                                Dim oBanc As DTOBanc = Banc.FindSync(oBancGuid, exs)
                                Dim oIban As DTOIban = oBanc.Iban
                                obs = Await IbanObs(exs, oInvoice, oIban, HideFirstDigits:=False)
                        End Select
                        If obs.Count > 0 Then
                            oInvoice.Ob1 = obs(0)
                            If obs.Count > 1 Then
                                oInvoice.Ob2 = obs(1)
                                If obs.Count > 2 Then
                                    oInvoice.Ob3 = obs(2)
                                End If
                            End If
                        End If

                    End If
                End If
        End Select
        If oInvoice.Fpg.Length > 50 Then
            Dim sMsg As String = String.Format("La forma de pagament té {0} caracters quan no pot passar de {1}.", oInvoice.Fpg.Length, 50)
            oInvoice.AddException(DTOInvoiceException.Cods.ObsTooLong, sMsg)
        End If
        If oInvoice.Ob1.Length > 50 Then
            Dim sMsg As String = String.Format("La 1ª linia de observacions té {0} caracters quan no pot passar de {1}.", oInvoice.Ob1.Length, 50)
            oInvoice.AddException(DTOInvoiceException.Cods.ObsTooLong, sMsg)
        End If
        If oInvoice.Ob2.Length > 50 Then
            Dim sMsg As String = String.Format("La 2ª linia de observacions té {0} caracters quan no pot passar de {1}.", oInvoice.Ob2.Length, 50)
            oInvoice.AddException(DTOInvoiceException.Cods.ObsTooLong, sMsg)
        End If
        If oInvoice.Ob3.Length > 50 Then
            Dim sMsg As String = String.Format("La 3ª linia de observacions té {0} caracters quan no pot passar de {1}.", oInvoice.Ob3.Length, 50)
            oInvoice.AddException(DTOInvoiceException.Cods.ObsTooLong, sMsg)
        End If
        Return oInvoice
    End Function


    Protected Shared Async Function IbanObs(exs As List(Of Exception), oInvoice As DTOInvoice, Optional oIban As DTOIban = Nothing, Optional HideFirstDigits As Boolean = True) As Task(Of List(Of String))
        Dim retval As New List(Of String)
        If oIban Is Nothing Then
            'oIban = oInvoice.Customer.PaymentTerms.Iban
            oIban = Await Iban.FromContact(exs, oInvoice.Customer, DTOIban.Cods.Client, oInvoice.Vto)
        End If

        If oIban Is Nothing Then
            oInvoice.AddException(DTOInvoiceException.Cods.MissingIban, "falta l'Iban")
        Else
            retval = Iban.ToMultilineString(oIban, True, HideFirstDigits, exs).Result.Split(vbCrLf).ToList
            If exs.Count > 0 Then
                oInvoice.AddException(DTOInvoiceException.Cods.UncompleteBank, "banc incomplet")
            End If
        End If
        Return retval
    End Function
    Protected Shared Sub SetObs(exs As List(Of Exception), ByRef oInvoice As DTOInvoice, Optional oIban As DTOIban = Nothing)
        If oIban Is Nothing Then
            'oIban = oInvoice.Customer.PaymentTerms.Iban
            oIban = Iban.FromContactSync(exs, oInvoice.Customer, DTOIban.Cods.Client, oInvoice.Vto)
        End If

        If oIban Is Nothing Then
            oInvoice.AddException(DTOInvoiceException.Cods.MissingIban, "falta l'Iban")
        Else
            Dim Obs As String() = Iban.ToMultilineString(oIban, True, True, exs).Result.Split(vbCrLf)
            If exs.Count > 0 Then
                oInvoice.AddException(DTOInvoiceException.Cods.UncompleteBank, "banc incomplet")
            End If
            If Obs.Length > 0 Then
                oInvoice.Ob1 = Obs(0)
                If Obs.Length > 1 Then
                    oInvoice.Ob2 = Obs(1)
                    If Obs.Length > 2 Then
                        oInvoice.Ob3 = Obs(2)
                    End If
                End If
            End If
        End If
    End Sub


    Shared Async Function LogSii(exs As List(Of Exception), oInvoice As DTOInvoice) As Task(Of Boolean)
        Return Await Api.Execute(Of DTOInvoice)(oInvoice, exs, "Invoice/LogSii")
    End Function

    Shared Async Function LogPrint(exs As List(Of Exception), oInvoice As DTOInvoice, oPrintMode As DTOInvoice.PrintModes, oWinUser As DTOUser, oDestUser As DTOUser, DtFch As Date) As Task(Of Boolean)
        Return Await Api.Fetch(Of Boolean)(exs, "Invoice/LogPrint", oInvoice.Guid.ToString, oPrintMode, oWinUser.Guid.ToString, OpcionalGuid(oDestUser), FormatFch(DtFch))
    End Function



    Shared Async Function SendToEmail(exs As List(Of Exception), oInvoice As DTOInvoice, oUser As DTOUser, DtFch As Date) As Task(Of Boolean)
        oInvoice.fchLastPrinted = DtFch
        Return Await Api.Execute(Of DTOInvoice, Boolean)(oInvoice, exs, "Invoice/SendToEmail", oUser.Guid.ToString())
    End Function

    Shared Async Function EdiversaOrderFiles(exs As List(Of Exception), oInvoice As DTOInvoice) As Task(Of List(Of DTOEdiversaFile))
        Return Await Api.Fetch(Of List(Of DTOEdiversaFile))(exs, "invoice/ediOrderFiles", oInvoice.Guid.ToString)
    End Function




End Class


Public Class Invoices
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oExercici As DTOExercici, mes As Integer) As Task(Of List(Of DTOInvoice)) 'per declaració Iva
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices", oExercici.Emp.Id, oExercici.Year, mes)
    End Function

    Shared Async Function All(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/fromCustomer", oCustomer.Guid.ToString())
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oEmp As DTOEmp, oYearMonth As DTOYearMonth) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/Headers", oEmp.Id, oYearMonth.year, oYearMonth.month)
    End Function


    Shared Async Function LlibreDeFactures(exs As List(Of Exception), oExercici As DTOExercici, Optional ToFch As Date = Nothing, Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of MatHelper.Excel.Sheet) ' Task(Of MatHelper.Excel.Sheet)
        Dim sFilename As String = ""
        If ToFch = Nothing Then
            sFilename = String.Format("{0}.{1} Llibre de Factures emeses.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        ElseIf ToFch.Day = 31 And ToFch.Month = 3 Then
            sFilename = String.Format("{0}.{1}.Q1 Llibre de Factures emeses.xlsx", oExercici.Emp.Org.PrimaryNifValue(), oExercici.Year)
        Else
            sFilename = String.Format("{0}.{1:yyyyMMdd} Llibre de Factures emeses.xlsx", oExercici.Emp.Org.PrimaryNifValue(), ToFch)
        End If
        Dim oInvoices = Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/LlibreDeFactures", oExercici.Emp.Id, oExercici.Year, FormatFch(ToFch))

        'Dim retval2 As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)
        'Invoices.LoadExcel(retval2, oInvoices, ShowProgress)

        Dim retval As New MatHelper.Excel.Sheet(oExercici.Year, sFilename)
        Invoices.LoadExcel(retval, oInvoices, ShowProgress)
        Return retval
    End Function


    Shared Async Function Printed(exs As List(Of Exception), oCustomer As DTOCustomer) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/Printed", oCustomer.Guid.ToString())
    End Function

    Shared Function PrintedSync(exs As List(Of Exception), oCustomer As DTOCustomer) As List(Of DTOInvoice)
        Return Api.FetchSync(Of List(Of DTOInvoice))(exs, "invoices/Printed", oCustomer.Guid.ToString())
    End Function

    Shared Async Function PrintPending(exs As List(Of Exception), oEmp As DTOEmp, Optional DtFch As Date = Nothing) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/PrintPending", oEmp.Id, FormatFch(DtFch))
    End Function

    Shared Async Function PrintedCollection(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/PrintedCollection", oEmp.Id)
    End Function

    Shared Async Function SetNoPrint(exs As List(Of Exception), oInvoices As List(Of DTOInvoice), oUser As DTOUser) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOInvoice), Boolean)(oInvoices, exs, "invoices/SetNoPrint", oUser.Guid.ToString())
    End Function

    Shared Async Function Last(exs As List(Of Exception), oExercici As DTOExercici, oSerie As DTOInvoice.Series) As Task(Of DTOInvoice)
        Return Await Api.Fetch(Of DTOInvoice)(exs, "invoices/last", oExercici.Emp.Id, oExercici.Year, oSerie)
    End Function

    Shared Async Function LastEachSerie(exs As List(Of Exception), oExercici As DTOExercici) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/LastEachSerie", oExercici.Emp.Id, oExercici.Year)
    End Function

    Shared Async Function LastNum(oEmp As DTOEmp, iYear As Integer, oSerie As DTOInvoice.Series, exs As List(Of Exception)) As Task(Of Integer)
        Return Await Api.Fetch(Of Integer)(exs, "invoices/lastnum", CInt(oEmp.Id), iYear, CInt(oSerie))
    End Function

    Shared Async Function AvailableNums(oEmp As DTOEmp, iYear As Integer, oSerie As DTOInvoice.Series, exs As List(Of Exception)) As Task(Of List(Of Integer))
        Return Await Api.Fetch(Of List(Of Integer))(exs, "invoices/availableNums", CInt(oEmp.Id), iYear, CInt(oSerie))
    End Function

    Shared Async Function IntrastatPending(oEmp As DTOEmp, oYearMonth As DTOYearMonth, exs As List(Of Exception)) As Task(Of List(Of DTOInvoice))
        Return Await Api.Fetch(Of List(Of DTOInvoice))(exs, "invoices/IntrastatPending", oEmp.Id, oYearMonth.Year, oYearMonth.Month)
    End Function

    Shared Async Function Summary(exs As List(Of Exception), oEmp As DTOEmp) As Task(Of List(Of DTOYearMonth))
        Return Await Api.Fetch(Of List(Of DTOYearMonth))(exs, "invoices/Summary", oEmp.Id)
    End Function

    Shared Async Function CheckOutFromPrinting(exs As List(Of Exception), oInvoices As List(Of DTOInvoice), oUser As DTOUser, DtFch As Date, ShowProgress As ProgressBarHandler) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        Dim sMessage As String = "descartant " & oInvoices.Count & " factures"
        ShowProgress(0, oInvoices.Count, 0, sMessage, CancelRequest)
        For Each oInvoice As DTOInvoice In oInvoices
            Dim ex2 As New List(Of Exception)
            If Not Await Invoice.LogPrint(ex2, oInvoice, DTOInvoice.PrintModes.NoPrint, oUser, Nothing, DtFch) Then
                exs.Add(New Exception("error al descartar la factura " & oInvoice.Num))
                exs.AddRange(ex2)
            End If
            If CancelRequest Then Exit For
        Next
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function SendToEdi(oEmp As DTOEmp, oInvoices As List(Of DTOInvoice), oUser As DTOUser, DtFch As Date, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        Dim retval As Boolean = True
        For Each oInvoice As DTOInvoice In oInvoices
            Dim exs2 As New List(Of DTOEdiversaException)
            Dim oEdiFile As DTOEdiversaFile = Await EdiversaInvoice.EdiFile(oEmp, oInvoice, exs2)
            If oEdiFile Is Nothing Or exs2.Count > 0 Then
                Dim sNum As String = ""
                If oInvoice IsNot Nothing Then sNum = oInvoice.Num
                exs.Add(New Exception("error al enrutar factura " & sNum))
                exs.AddRange(DTOEdiversaException.ToSystemExceptions(exs2))
            Else
                oEdiFile.resultBaseGuid = New DTOBaseGuid(oEdiFile.resultBaseGuid.Guid)
                Dim oLog As New DTOInvoicePrintLog
                With oLog
                    .EdiversaFile = oEdiFile
                    .Invoice = New DTOInvoice(oEdiFile.source.Guid)
                    .WinUser = New DTOUser(oUser.Guid)
                    .Fch = DtFch
                End With

                If Not Await EdiversaFile.QueueInvoice(exs, oLog) Then
                    exs.Add(New Exception("error al enrutar factura " & oInvoice.Num))
                End If
            End If
            ShowProgress(0, oInvoices.Count, oInvoices.IndexOf(oInvoice), "enviant " & oInvoices.Count & " factures per Edi", CancelRequest)
            If CancelRequest Then Exit For
        Next
        Return retval
    End Function

    Shared Async Function SendToSii(exs As List(Of Exception), oEmp As DTOEmp, entorno As DTO.Defaults.Entornos, oInvoices As List(Of DTOInvoice), oX509Cert As Security.Cryptography.X509Certificates.X509Certificate2, Optional ShowProgress As ProgressBarHandler = Nothing) As Task(Of Boolean)
        Dim BlCancelRequest As Boolean
        Dim sLabel As String = ""
        Dim oInvoice As DTOInvoice = Nothing
        Dim iStep As Integer = 1
        Try
            For Each oInvoice In oInvoices
                If Invoice.Load(oInvoice, exs) Then
                    sLabel = String.Format("Pas " & iStep & " de 3 passos: carregant fra.{0} del {1:dd/MM/yyy} a {2}", oInvoice.num, oInvoice.fch, oInvoice.customer.nom)
                    If ShowProgress IsNot Nothing Then
                        ShowProgress(0, oInvoices.Count - 1, oInvoices.IndexOf(oInvoice), sLabel, BlCancelRequest)
                        If BlCancelRequest Then
                            Dim idx As Integer = oInvoices.IndexOf(oInvoice)
                            oInvoices = oInvoices.Take(idx + 1)
                            Exit For
                        End If
                    End If
                End If
            Next

            iStep = 2
            If ShowProgress IsNot Nothing Then
                sLabel = String.Format("Pas " & iStep & " de 3 passos: enviant {0} factures a Hisenda", oInvoices.Count)
                ShowProgress(0, oInvoices.Count - 1, 0, sLabel, BlCancelRequest)
            End If

            AeatSii.FacturasEmitidas.Send(entorno, oInvoices, oX509Cert, exs)

            iStep = 3
            For Each oInvoice In oInvoices
                Await Invoice.LogSii(exs, oInvoice)
                If ShowProgress IsNot Nothing Then
                    sLabel = String.Format("Pas " & iStep & " de 3 passos: desant fra.{0} del {1:dd/MM/yyy} a {2}", oInvoice.num, oInvoice.fch, oInvoice.customer.nom)
                    ShowProgress(0, oInvoices.Count - 1, oInvoices.IndexOf(oInvoice), sLabel, BlCancelRequest)
                End If
            Next

        Catch ex As Exception
            Dim sInvoiceNum As String = ""
            If oInvoice IsNot Nothing Then
                sInvoiceNum = oInvoice.num
            Else
                sInvoiceNum = "(s/n)"
            End If
            Dim sMsg As String = String.Format("Step {0} factura {1} error: {2}", iStep, sInvoiceNum, ex.Message)
            Try
                'If EventLog.SourceExists("MatSched") Then
                'EventLog.WriteEntry("MatSched", sMsg, EventLogEntryType.Warning)
                'End If
            Catch ex2 As Security.SecurityException

            End Try
            exs.Add(New Exception(sMsg))
        End Try
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function ClearPrintLog(exs As List(Of Exception), oInvoices As List(Of DTOInvoice)) As Task(Of Boolean)
        Return Await Api.Execute(Of List(Of DTOInvoice), Boolean)(oInvoices, exs, "Invoice/ClearPrintLog")
    End Function


    Shared Function Excel(oInvoices As List(Of DTOInvoice))
        Dim retval As New MatHelper.Excel.Sheet("M+O invoices")
        With retval
            .AddColumn("factura")
            .AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("import", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText)
        End With

        For Each oInvoice In oInvoices
            Dim oRow As MatHelper.Excel.Row = retval.AddRow
            oRow.AddCell(oInvoice.Num, Invoice.PdfUrl(oInvoice, True))
            oRow.AddCell(oInvoice.Fch)
            oRow.AddCellAmt(oInvoice.Total)
            oRow.AddCell(oInvoice.Customer.FullNom)
        Next
        Return retval
    End Function

    Shared Sub LoadExcel(ByRef oExcelSheet As MatHelper.Excel.Sheet, items As List(Of DTOInvoice), Optional ShowProgress As ProgressBarHandler = Nothing)
        Dim CancelRequest As Boolean

        If ShowProgress IsNot Nothing Then
            ShowProgress(0, 1000, 0, "Llibre de factures emeses", CancelRequest)
        End If

        With oExcelSheet
            .AddColumn("registre", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("factura", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("data", MatHelper.Excel.Cell.NumberFormats.DDMMYY)
            .AddColumn("NIF", MatHelper.Excel.Cell.NumberFormats.W50)
            .AddColumn("client", MatHelper.Excel.Cell.NumberFormats.PlainText)
            .AddColumn("Base imponible", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Iva", MatHelper.Excel.Cell.NumberFormats.Euro)
            .AddColumn("Rec.Equivalencia", MatHelper.Excel.Cell.NumberFormats.Euro)
        End With

        Dim oRow As MatHelper.Excel.Row = oExcelSheet.AddRow
        Dim iRows As Integer = items.Count
        Dim sFormula As String = "SUM(R[+1]C[0]:R[" & iRows.ToString() & "]C[0])"
        With oRow
            .AddCell() 'registre
            .AddCell() 'factura
            .AddCell() 'data
            .AddCell() 'Nif
            .AddCell("Total")
            .AddFormula(sFormula) 'base sujeta
            .AddFormula(sFormula) 'quota Iva
            .AddFormula(sFormula) 'quota recarrec equivalencia
        End With


        For Each item As DTOInvoice In items
            oRow = oExcelSheet.AddRow
            With item
                oRow.AddCell(.Cca.Id)
                If .Cca.DocFile Is Nothing Then
                    oRow.AddCell(item.NumeroYSerie())
                Else
                    oRow.AddCell(item.NumeroYSerie(), DocFile.DownloadUrl(.Cca.DocFile, True))
                End If
                oRow.AddCell(.Fch)
                If .Customer IsNot Nothing Then
                    oRow.AddCell(.Customer.PrimaryNifValue())
                    oRow.AddCell(.Customer.Nom)
                End If

                'If .Customer.Nif.StartsWith("PT") Then Stop

                oRow.AddCellAmt(DTOInvoice.ivaBase(item))
                oRow.AddCellAmt(DTOInvoice.getIvaAmt(item))
                oRow.AddCellAmt(DTOInvoice.getReqAmt(item))
            End With
            If ShowProgress IsNot Nothing Then
                ShowProgress(0, items.Count, oExcelSheet.Rows.Count - 1, "Redactant Excel", CancelRequest)
            End If
            If CancelRequest Then Exit For
        Next
    End Sub


    Shared Async Function SendToEmail(oInvoices As List(Of DTOInvoice), oUser As DTOUser, DtFch As Date, ShowProgress As ProgressBarHandler, exs As List(Of Exception)) As Task(Of Boolean)
        Dim CancelRequest As Boolean
        Dim retval As Boolean = True
        For Each oInvoice As DTOInvoice In oInvoices
            Await Invoice.SendToEmail(exs, oInvoice, oUser, DtFch)

            If ShowProgress IsNot Nothing Then
                ShowProgress(0, oInvoices.Count, oInvoices.IndexOf(oInvoice), "enviant " & oInvoices.Count & " factures per email", CancelRequest)
                If CancelRequest Then Exit For
            End If
        Next

        Return retval
    End Function


End Class
