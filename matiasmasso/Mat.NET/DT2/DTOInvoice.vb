Public Class DTOInvoice
    Inherits DTOBaseGuid

    Property emp As DTOEmp
    Property serie As Series
    Property num As Integer
    Property fch As Date
    Property customer As DTOCustomer
    Property total As DTOAmt

    Property baseImponible As DTOAmt
    Property ivaAmt As DTOAmt
    Property reqAmt As DTOAmt

    Property ivaBaseQuotas As List(Of DTOTaxBaseQuota)

    Property iva As Decimal
    Property req As Decimal

    Property deliveries As List(Of DTODelivery)
    Property fpg As String = ""
    Property ob1 As String = ""
    Property ob2 As String = ""
    Property ob3 As String = ""
    Property cfp As DTOPaymentTerms.CodsFormaDePago

    Property vto As Date
    Property printMode As PrintModes
    Property fchLastPrinted As Date
    Property userLastPrinted As DTOUser
    Property existPnds As Boolean

    Property cca As DTOCca
    Property repComLiquidables As List(Of DTORepComLiquidable)
    Property exceptions As List(Of DTOInvoiceException)

    Property tipoFactura As String
    Property tipoSujeccionIva As TiposSujeccionIva
    Property docFile As DTODocFile
    Property siiLog As DTOSiiLog
    Property siiL9 As String
    Property concepte As Conceptes

    Property export As ExportCods

    Property regimenEspecialOTrascendencia As String


    Public Enum Series
        Standard
        Rectificativa
    End Enum

    Public Enum Conceptes
        NotSet
        Ventas
        Servicios
        Suplidos
    End Enum

    Public Enum ExportCods
        NotSet
        Nacional
        Intracomunitari
        Extracomunitari
    End Enum

    Public Enum SiiResults
        NotSet
        Correcto
        AceptadoConErrores
        Incorrecto
    End Enum


    Public Enum TiposSujeccionIva
        NotSet
        SujetoNoExento
        SujetoExento
        NoSujeto
    End Enum
    Public Enum CausasExencionIva
        NotSet
        Articulo21_ExportOutOfCEE
        Articulo25_ExportInsideCEE
    End Enum

    Public Enum TiposFactura
        F1_Factura_Estandar
        F2_Factura_Simplificada
        F3_Sustitutiva
        F4_Resumen
        F5_Importaciones_DUA
        F6_Justificantes_Contables
        R1_Factura_Rectificativa1 '(Art 80.1 y 80.2 y Error fundado en derecho)
        R2_Factura_Rectificativa2 '(Art 80.3)
        R3_Factura_Rectificativa3 '(Art 80.4)
        R4_Factura_Resto
        R5_Factura_Rectificativa_simplificadas
    End Enum

    Public Enum PrintModes
        Pending
        NoPrint
        Printer
        Email
        Edi
    End Enum

    Public Sub New()
        MyBase.New()
        _IvaBaseQuotas = New List(Of DTOTaxBaseQuota)
        _Exceptions = New List(Of DTOInvoiceException)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _IvaBaseQuotas = New List(Of DTOTaxBaseQuota)
        _Exceptions = New List(Of DTOInvoiceException)
    End Sub

    Shared Function Factory(oCustomer As DTOCustomer) As DTOInvoice
        Dim retval As New DTOInvoice
        With retval
            .Emp = oCustomer.Emp
            .TipoFactura = "F1"
            .Concepte = DTOInvoice.Conceptes.Ventas
            .Customer = oCustomer
            .Deliveries = New List(Of DTODelivery)
            DTOInvoice.SetRegimenEspecialOTrascendencia(retval)
            .SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(retval)
        End With
        Return retval
    End Function

    Shared Function EdiFileName(oInvoice As DTOInvoice) As String
        Return String.Format("{0}.{1}.{2:yyyy.MM.dd.HH.mm}.txt", DTOInvoice.EdiversaTag(oInvoice).ToString, DTOInvoice.FormattedId(oInvoice), Now)
    End Function

    Shared Function EdiversaTag(oInvoice As DTOInvoice) As DTOEdiversaFile.Tags
        Dim exs As New List(Of Exception)
        Dim retval As DTOEdiversaFile.Tags
        Dim oCustomer As DTOCustomer = oInvoice.Customer
        Dim oGln As DTOEan = oCustomer.GLN
        Dim oInterlocutor = DTOEdiversaFile.ReadInterlocutor(oGln)
        Select Case oInterlocutor
            Case DTOEdiversaFile.Interlocutors.Sonae
                retval = DTOEdiversaFile.Tags.INVOIC_D_01B_UN_EAN010
            Case Else
                retval = DTOEdiversaFile.Tags.INVOIC_D_93A_UN_EAN007
        End Select
        Return retval
    End Function
    Public Sub AddException(oCod As DTOInvoiceException.Cods, Optional sMessage As String = "")
        Dim item As New DTOInvoiceException(oCod, sMessage)
        _Exceptions.Add(item)
    End Sub

    Shared Function Caption(oInvoices As List(Of DTOInvoice)) As String
        Dim retval As String = ""
        If oInvoices.Count = 1 Then
            retval = String.Format("factura {0}", oInvoices.First.Num)
        Else
            Dim sb As New System.Text.StringBuilder
            sb.Append("factures ")
            If DTOInvoice.Consecutivas(oInvoices) Then
                sb.Append(String.Format("{0}-{1}", oInvoices.First.Num, oInvoices.Last.Num))
            Else
                For Each oinvoice As DTOInvoice In oInvoices
                    If oInvoices.IndexOf(oinvoice) > 0 Then sb.Append(", ")
                    sb.Append(oinvoice.Num)
                    If sb.Length > 30 Then
                        sb.Append("...")
                        Exit For
                    End If
                Next
            End If
            retval = sb.ToString
        End If
        Return retval
    End Function

    Shared Function FullConcept(oInvoice As DTOInvoice, Optional ByVal oLang As DTOLang = Nothing, Optional ByVal BlShowCliNom As Boolean = True) As String
        If oLang Is Nothing Then oLang = oInvoice.Customer.Lang
        Dim sb As New Text.StringBuilder
        Select Case oInvoice.Serie
            Case DTOInvoice.Series.Rectificativa
                sb.Append(oLang.tradueix("factura rectificativa", "factura rectificativa", "corrective invoice ", "fatura rectificativa"))
            Case Else
                sb.Append(oLang.tradueix("factura ", "factura ", "invoice ", "fatura "))
        End Select
        sb.Append(oInvoice.Num & " ")
        sb.Append(oLang.tradueix("del", "del", "from", "del") & " ")
        sb.Append(oInvoice.Fch.ToShortDateString & " ")
        If oInvoice.Total Is Nothing Then
            sb.Append(oLang.tradueix("sin cargo", "sense carrec", "free of charge", "sin cargo") & " ")
        Else
            sb.Append(oLang.tradueix("por", "per", "for", "por") & " ")
            sb.Append(DTOAmt.CurFormatted(oInvoice.Total))
        End If
        If BlShowCliNom Then
            sb.Append(oLang.tradueix(" de ", " de ", " for ", "de "))
            sb.Append(oInvoice.Customer.Nom)
        End If
        Return sb.ToString
    End Function

    Shared Function CompactConcept(oInvoice As DTOInvoice, Optional ByVal oLang As DTOLang = Nothing, Optional ByVal BlShowCliNom As Boolean = True) As String
        If oLang Is Nothing Then oLang = oInvoice.Customer.Lang
        Dim sb As New Text.StringBuilder
        Select Case oInvoice.Serie
            Case DTOInvoice.Series.Rectificativa
                sb.Append(oLang.tradueix("fra.R", "fra.R", "inv.R", "fra.R"))
            Case Else
                sb.Append(oLang.tradueix("fra ", "fra ", "inv ", "fra "))
        End Select
        sb.Append(oInvoice.Num & " ")
        sb.Append(oLang.tradueix("del", "del", "from", "del") & " ")
        sb.Append(oInvoice.Fch.ToShortDateString & " ")
        Return sb.ToString
    End Function

    Shared Function DocAlbText(ByVal oDelivery As DTODelivery, oLang As DTOLang) As String
        Dim StDoc As String = oLang.tradueix("Albarán nº ", "Albará numº ", "Our ref# ")
        Dim StFrom As String = oLang.tradueix("de fecha", "de data", "from")
        Dim retval As String = StDoc & oDelivery.Id & " " & StFrom & " " & Format(oDelivery.Fch, "dd/MM/yy")
        If oDelivery.Customer.Ref > "" Then
            retval = retval & " a " & oDelivery.Customer.Ref
        End If
        Return retval
    End Function

    Shared Function DocPdcText(ByVal oPurchaseOrder As DTOPurchaseOrder, oLang As DTOLang) As String
        Dim StDoc As String = oLang.tradueix("Pedido", "Comanda", "Order", "Encomenda")
        Dim StFrom As String = oLang.tradueix("del", "del", "from", "del")
        Dim retval As String = StDoc & " " & oPurchaseOrder.Concept.Trim & " " & StFrom & " " & Format(oPurchaseOrder.Fch, "dd/MM/yy")
        Return retval
    End Function


    Shared Function Filename(oInvoices As List(Of DTOInvoice), Optional sExtension As String = "pdf") As String
        Dim sNums As String() = oInvoices.Select(Function(x) String.Format("{0:yyyy}.{1:00000}", x.Fch, x.Num))
        Dim retval As String = String.Format("M+O Facturas {0}.{1}", String.Join("-", sNums), sExtension)
        Return retval
    End Function

    Public Function NumeroYSerie() As String
        Dim retval As String = ""
        Select Case _Serie
            Case DTOInvoice.Series.Rectificativa
                retval = String.Format("R{0}", _Num)
            Case Else
                retval = _Num
        End Select
        Return retval
    End Function

    Shared Sub SetImports(ByRef oInvoice As DTOInvoice, oTaxs As List(Of DTOTax))
        With oInvoice
            .BaseImponible = DTOInvoice.CalcBaseImponible(oInvoice)
            .IvaBaseQuotas = DTOInvoice.GetIvaBaseQuotas(oInvoice, oTaxs)

            .Total = .BaseImponible.Clone

            For Each oIvaBaseQuota As DTOTaxBaseQuota In .IvaBaseQuotas
                Dim oAmt As DTOAmt = DTOTax.Quota(oIvaBaseQuota.Base, oIvaBaseQuota.Tax)
                .Total.Add(oAmt)
            Next
        End With
    End Sub

    Shared Function isIVAExento(oInvoice As DTOInvoice) As Boolean
        Dim retval As Boolean
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            retval = oInvoice.IvaBaseQuotas.Count = 0
        End If
        Return retval
    End Function

    Shared Sub SetRepComLiquidables(ByRef oInvoice As DTOInvoice)
        Dim retval As New List(Of DTORepComLiquidable)
        For Each oDelivery As DTODelivery In oInvoice.Deliveries
            For Each item As DTODeliveryItem In oDelivery.Items
                If item.RepCom IsNot Nothing Then

                    Dim oRepComLiquidable As DTORepComLiquidable = retval.Find(Function(x) x.Rep.Equals(item.RepCom.Rep))
                    If oRepComLiquidable Is Nothing Then
                        oRepComLiquidable = New DTORepComLiquidable()
                        With oRepComLiquidable
                            .Rep = item.RepCom.Rep
                            .Fra = oInvoice
                            .Base = DTOAmt.Empty
                            .Comisio = DTOAmt.Empty
                            .Items = New List(Of DTODeliveryItem)
                        End With
                        retval.Add(oRepComLiquidable)
                    End If

                    With oRepComLiquidable
                        .Base.Add(item.Import)
                        Dim oComisio As DTOAmt = item.Import.Percent(item.RepCom.Com)
                        .Comisio.Add(oComisio)
                        .Items.Add(item)
                    End With
                End If
            Next
        Next
        oInvoice.RepComLiquidables = retval
    End Sub


    Shared Function CtaDeb(oCashCod As DTOCustomer.CashCodes) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas = DTOPgcPlan.Ctas.Clients
        Select Case oCashCod
            Case DTOCustomer.CashCodes.TransferenciaPrevia, DTOCustomer.CashCodes.Visa
                retval = DTOPgcPlan.Ctas.Clients_Anticips
            Case DTOCustomer.CashCodes.Diposit
                retval = DTOPgcPlan.Ctas.DipositIrrevocableDeCompra
        End Select
        Return retval
    End Function

    Shared Function CtaHab(oInvoice As DTOInvoice) As DTOPgcPlan.Ctas
        Dim retval As DTOPgcPlan.Ctas = DTOPgcPlan.Ctas.Vendes
        If oInvoice.BaseImponible.IsNegative Then
            retval = DTOPgcPlan.Ctas.DevolucionsDeVendes
        End If
        Return retval
    End Function

    Shared Function IsCredit(oInvoice As DTOInvoice) As Boolean
        Dim retval As Boolean = True
        Select Case oInvoice.Cfp
            Case DTOPaymentTerms.CodsFormaDePago.Comptat,
                  DTOPaymentTerms.CodsFormaDePago.Diposit,
                  DTOPaymentTerms.CodsFormaDePago.TransfPrevia
                retval = False
        End Select
        Return retval
    End Function

    Shared Sub SetNumberAndFch(ByRef oInvoice As DTOInvoice, ByRef oPreviousInvoice As DTOInvoice, ByRef oPreviousRectificativa As DTOInvoice, DtMinFch As DateTimeOffset)
        Dim DtLastDeliveryFch As DateTimeOffset = oInvoice.Deliveries.Max(Function(x) x.Fch)
        Dim DtPreviousInvoiceFch As DateTimeOffset

        If oInvoice.BaseImponible.IsNegative Then
            oInvoice.Serie = DTOInvoice.Series.Rectificativa
            Dim invoiceNum As Integer = 1
            If oPreviousRectificativa IsNot Nothing Then
                invoiceNum = oPreviousRectificativa.Num + 1
            End If
            oInvoice.Num = invoiceNum

            If oPreviousRectificativa Is Nothing Then
                DtPreviousInvoiceFch = New Date(DtLastDeliveryFch.Year, 1, 1)
            Else
                DtPreviousInvoiceFch = oPreviousRectificativa.Fch
            End If
            oPreviousRectificativa = oInvoice
        Else
            oInvoice.Serie = DTOInvoice.Series.Standard

            If oPreviousInvoice Is Nothing Then
                DtPreviousInvoiceFch = New Date(DtLastDeliveryFch.Year, 1, 1)
                oInvoice.Num = 1
            Else
                DtPreviousInvoiceFch = oPreviousInvoice.Fch
                oInvoice.Num = oPreviousInvoice.Num + 1
            End If
            oPreviousInvoice = oInvoice
        End If

        Dim oMaxFch As DateTimeOffset = {DtLastDeliveryFch, DtPreviousInvoiceFch, DtMinFch}.Max()
        oInvoice.Fch = oMaxFch.LocalDateTime
    End Sub

    Shared Function CashCod(oInvoice As DTOInvoice) As DTOCustomer.CashCodes
        Dim retval As DTOCustomer.CashCodes = DTOCustomer.CashCodes.NotSet
        If oInvoice.Deliveries IsNot Nothing Then
            If oInvoice.Deliveries.Count > 0 Then
                Dim oFirstDelivery As DTODelivery = oInvoice.Deliveries.First
                retval = oFirstDelivery.CashCod
            End If
        End If
        Return retval
    End Function
    Shared Function CalcBaseImponible(oInvoice As DTOInvoice) As DTOAmt
        Dim DcEur As Decimal = oInvoice.Deliveries.Sum(Function(x) x.Items.Sum(Function(y) DTOAmt.Import(y.Qty, y.Price, y.Dto).Eur))
        Dim retval As DTOAmt = DTOAmt.factory(DcEur)
        Return retval
    End Function

    Shared Function GuessClauL9ExempcioIva(oInvoice As DTOInvoice) As String
        Dim retval As String = ""
        Dim ExportCods As DTOInvoice.ExportCods = DTOContact.ExportCod(oInvoice.Customer)
        Select Case ExportCods
            Case DTOInvoice.ExportCods.Intracomunitari
                retval = "E5"
            Case DTOInvoice.ExportCods.Extracomunitari
                retval = "E2"
        End Select
        Return retval
    End Function

    Shared Sub SetRegimenEspecialOTrascendencia(oInvoice As DTOInvoice)
        If oInvoice.RegimenEspecialOTrascendencia = "" Then
            Dim DtFch As Date = oInvoice.Fch
            If DtFch.Year = 2017 And DtFch.Month <= 6 Then
                oInvoice.RegimenEspecialOTrascendencia = "16"
            Else
                Dim oCustomer As DTOCustomer = oInvoice.Customer
                Select Case DTOContact.ExportCod(oCustomer)
                    Case DTOInvoice.ExportCods.Nacional
                        oInvoice.RegimenEspecialOTrascendencia = "01"
                    Case DTOInvoice.ExportCods.Intracomunitari, DTOInvoice.ExportCods.Extracomunitari
                        oInvoice.RegimenEspecialOTrascendencia = "02"
                End Select
            End If
        End If
    End Sub

    Shared Function GetIvaBaseQuotas(oInvoice As DTOInvoice, oTaxs As List(Of DTOTax)) As List(Of DTOTaxBaseQuota)
        Dim retval As New List(Of DTOTaxBaseQuota)

        With oInvoice
            If .Customer.IVA Then
                .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.SujetoNoExento
                For Each oDelivery In oInvoice.Deliveries
                    For Each oItm As DTODeliveryItem In oDelivery.Items
                        Dim oIvaBaseQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oItm.IvaCod)
                        If oIvaBaseQuota Is Nothing Then
                            Dim oTax As DTOTax = oTaxs.Find(Function(x) x.Codi = oItm.IvaCod)
                            oIvaBaseQuota = New DTOTaxBaseQuota(oTax, oItm.Import)
                            retval.Add(oIvaBaseQuota)
                            If .Customer.Req Then
                                Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oIvaBaseQuota.Tax.Codi)
                                Dim oTaxReq As DTOTax = oTaxs.Find(Function(x) x.Codi = oTaxReqCodi)
                                Dim oReqBaseQuota As New DTOTaxBaseQuota(oTaxReq, oItm.Import)
                                retval.Add(oReqBaseQuota)
                            End If
                        Else
                            oIvaBaseQuota.AddBase(oItm.Import)
                            If .Customer.Req Then
                                Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oIvaBaseQuota.Tax.Codi)
                                Dim oReqBaseQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oTaxReqCodi)
                                oReqBaseQuota.AddBase(oItm.Import)
                            End If
                        End If
                    Next
                Next
            Else
                .TipoSujeccionIva = DTOInvoice.TiposSujeccionIva.SujetoExento
                If .SiiL9 = "" Then .SiiL9 = DTOInvoice.GuessClauL9ExempcioIva(oInvoice)
            End If
        End With
        Return retval
    End Function

    Shared Sub SetVto(ByRef oInvoice As DTOInvoice)
        Dim oCcx As DTOCustomer = oInvoice.Customer.CcxOrMe
        If oCcx.PaymentTerms Is Nothing Then
            oInvoice.AddException(DTOInvoiceException.Cods.MissingPaymentTerms, "falta forma de pagament")
        Else
            oInvoice.Vto = DTOPaymentTerms.Vto(oCcx.PaymentTerms, oInvoice.Fch)
        End If
    End Sub

    Shared Function Consecutivas(oInvoices As List(Of DTOInvoice)) As Boolean
        Dim retval As Boolean = True
        If oInvoices.Count <= 1 Then
            retval = True
        Else
            Dim oSortedInvoices As List(Of DTOInvoice) = oInvoices.OrderBy(Function(x) x.Num).ToList
            Dim iNum As Integer = oSortedInvoices.First.Num
            For i As Integer = 1 To oSortedInvoices.Count - 1
                iNum += 1
                If oSortedInvoices(i).Num <> iNum Then
                    retval = False
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function SameCustomer(oInvoices As List(Of DTOInvoice)) As Boolean
        Dim retval As Boolean = oInvoices.All(Function(x) x.Customer.Equals(oInvoices.First.Customer))
        Return retval
    End Function

    Shared Function GetConcepte(oDelivery As DTODelivery) As DTOInvoice.Conceptes
        Dim retval As DTOInvoice.Conceptes = DTOInvoice.Conceptes.NotSet
        Select Case oDelivery.Cod
            Case DTOPurchaseOrder.Codis.Reparacio
                retval = DTOInvoice.Conceptes.Servicios
            Case Else
                retval = DTOInvoice.Conceptes.Ventas
        End Select
        Return retval
    End Function

    Shared Function Lang(oInvoice As DTOInvoice) As DTOLang
        Dim retval As DTOLang = oInvoice.Customer.Lang
        Return retval
    End Function

    Shared Function LastPrintedText(oInvoice As DTOInvoice) As String
        Dim retval As String = ""
        Dim exs As New List(Of Exception)
        Select Case oInvoice.PrintMode
            Case DTOInvoice.PrintModes.Pending
                retval = "pendent de imprimir"
            Case DTOInvoice.PrintModes.NoPrint
                retval = "no imprimible"
            Case DTOInvoice.PrintModes.Printer
                retval = String.Format("impresa en paper el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted))
            Case DTOInvoice.PrintModes.Email
                retval = String.Format("enviada per email el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted))
            Case DTOInvoice.PrintModes.Edi
                retval = String.Format("enviada per EDI el {0:dd/MM/yy} a les {0:hh:mm} per {1}", oInvoice.FchLastPrinted, DTOUser.NicknameOrElse(oInvoice.UserLastPrinted))
        End Select
        Return retval
    End Function

    Shared Function FormattedId(oInvoice As DTOInvoice) As String
        Dim retval As String = String.Format("{0:yyyy}{1:00000}", oInvoice.Fch, oInvoice.Num)
        Return retval
    End Function

    Shared Function Caption(oInvoice As DTOInvoice, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang

        Dim sb As New System.Text.StringBuilder
        sb.Append(oLang.tradueix("Factura ", "Factura ", "Invoice "))
        sb.Append(oInvoice.Num)
        sb.Append(oLang.tradueix(" del ", " del ", " from ") & oInvoice.Fch.ToShortDateString)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function DiscountExists(oInvoice As DTOInvoice) As Boolean
        Dim retval As Boolean
        For Each oDelivery As DTODelivery In oInvoice.Deliveries
            For Each item As DTODeliveryItem In oDelivery.Items
                If item.Dto <> 0 Then
                    retval = True
                    Exit For
                End If
            Next
        Next
        Return retval
    End Function

    Shared Function SumaDeImportes(oInvoice As DTOInvoice) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oDelivery As DTODelivery In oInvoice.Deliveries
            For Each oItem As DTODeliveryItem In oDelivery.Items
                retval.Add(oItem.Import)
            Next
        Next
        Return retval
    End Function


    Shared Function GetBaseImponible(oInvoice As DTOInvoice) As DTOAmt
        Dim retval As DTOAmt = DTOInvoice.SumaDeImportes(oInvoice)
        'afegir descomptes globals
        'afegir recarrecs globals
        Return retval
    End Function

    Shared Function IvaBase(oInvoice As DTOInvoice) As DTOAmt
        'If oInvoice.Customer.Nif.StartsWith("PT") Then Stop
        Dim retval = DTOAmt.Empty
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            Dim DcEur As Decimal = oInvoice.IvaBaseQuotas.
                Where(Function(x) x.Tax.Codi = DTOTax.Codis.Exempt Or x.Tax.Codi = DTOTax.Codis.Iva_Standard Or x.Tax.Codi = DTOTax.Codis.Iva_Reduit Or x.Tax.Codi = DTOTax.Codis.Iva_SuperReduit).
                Sum(Function(y) y.Base.Eur)
            retval = DTOAmt.factory(DcEur)
        End If
        Return retval
    End Function

    Shared Function GetIvaAmt(oInvoice As DTOInvoice) As DTOAmt
        Dim retval = DTOAmt.Empty
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            Dim DcEur As Decimal = oInvoice.IvaBaseQuotas.
                Where(Function(x) x.Tax.Codi = DTOTax.Codis.Iva_Standard Or x.Tax.Codi = DTOTax.Codis.Iva_Reduit Or x.Tax.Codi = DTOTax.Codis.Iva_SuperReduit).
                Sum(Function(y) y.Quota.Eur)
            retval = DTOAmt.factory(DcEur)
        End If
        Return retval
    End Function

    Shared Function IvaTipus(oInvoice As DTOInvoice) As Decimal
        Dim retval As Decimal
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            Dim oQuotas As List(Of DTOTaxBaseQuota) = oInvoice.IvaBaseQuotas.
                Where(Function(x) x.Tax.Codi = DTOTax.Codis.Iva_Standard Or x.Tax.Codi = DTOTax.Codis.Iva_Reduit Or x.Tax.Codi = DTOTax.Codis.Iva_SuperReduit).ToList

            If oQuotas.Count > 0 Then
                Dim MixedTaxes As Boolean = oQuotas.Any(Function(x) x.Tax.Codi <> oQuotas.First.Tax.Codi)
                If Not MixedTaxes Then
                    retval = oQuotas.First.Tax.Tipus
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function GetReqAmt(oInvoice As DTOInvoice) As DTOAmt
        Dim retval = DTOAmt.Empty
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            Dim DcEur As Decimal = oInvoice.IvaBaseQuotas.
                 Where(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Standard Or x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Reduit Or x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_SuperReduit).
                 Sum(Function(y) y.Quota.Eur)
            retval = DTOAmt.factory(DcEur)
        End If
        Return retval
    End Function

    Shared Function ReqTipus(oInvoice As DTOInvoice) As Decimal
        Dim retval As Decimal
        If oInvoice.IvaBaseQuotas IsNot Nothing Then
            Dim oQuotas As List(Of DTOTaxBaseQuota) = oInvoice.IvaBaseQuotas.
                 Where(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Standard Or x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Reduit Or x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_SuperReduit).ToList

            If oQuotas.Count > 0 Then
                Dim MixedTaxes As Boolean = oQuotas.Any(Function(x) x.Tax.Codi <> oQuotas.First.Tax.Codi)
                If Not MixedTaxes Then
                    retval = oQuotas.First.Tax.Tipus
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function TaxText(oInvoice As DTOInvoice, oCod As DTOTax.Codis) As String
        Dim retval As String = ""
        Select Case oCod
            Case DTOTax.Codis.Iva_Standard
                retval = oInvoice.Customer.Lang.tradueix("IVA", "IVA", "VAT") & " " & oInvoice.Iva & "%"
            Case DTOTax.Codis.Recarrec_Equivalencia_Standard
                retval = oInvoice.Customer.Lang.tradueix("Recargo de Equivalencia", "Recàrrec d'equivalència", "VAT Equivalence Surcharge") & " " & oInvoice.Req & "%"
        End Select
        Return retval
    End Function

    Shared Function TaxAmt(oInvoice As DTOInvoice, oCod As DTOTax.Codis) As DTOAmt
        Dim retval As DTOAmt = Nothing
        Dim oBase As DTOAmt = DTOInvoice.GetBaseImponible(oInvoice)
        Select Case oCod
            Case DTOTax.Codis.Iva_Standard
                retval = oBase.Percent(oInvoice.Iva)
            Case DTOTax.Codis.Recarrec_Equivalencia_Standard
                retval = oBase.Percent(oInvoice.Req)
        End Select
        Return retval
    End Function

    Shared Function SumaDeImpuestos(oInvoice As DTOInvoice) As DTOAmt
        Dim oBase As DTOAmt = DTOInvoice.GetBaseImponible(oInvoice)
        Dim retval = DTOAmt.Empty

        If oInvoice.Iva <> 0 Then
            Dim oIva As DTOAmt = oBase.Percent(oInvoice.Iva)
            retval.Add(oIva)
            If oInvoice.Req <> 0 Then
                Dim oReq As DTOAmt = oBase.Percent(oInvoice.Req)
                retval.Add(oReq)
            End If
        End If
        Return retval
    End Function

    Shared Function GetTotal(oInvoice As DTOInvoice) As DTOAmt
        Dim retval As DTOAmt = DTOInvoice.GetBaseImponible(oInvoice)
        retval.Add(DTOInvoice.SumaDeImpuestos(oInvoice))
        Return retval
    End Function

    Shared Function Doc(oInvoice As DTOInvoice) As DTODoc
        Dim exs As New List(Of Exception)
        'FEBL.Invoice.Load(oInvoice, exs)

        'Dim BlInsertEans As Boolean = Me.Client.Nom.Contains("AMAZON") 'to deprecate quan li factuirem via EDI

        Dim oLang = DTOInvoice.Lang(oInvoice)
        Dim oDoc As New DTODoc(DTODoc.Estilos.Factura, oLang, DTOCur.Factory(DTOLang.ESP.ToString()))
        Dim oCustomer As DTOCustomer = oInvoice.Customer
        With oDoc
            If oInvoice.Serie = DTOInvoice.Series.Rectificativa Then .SideLabel = DTODoc.SideLabels.FacturaRectificativa

            .Dest.Add(oCustomer.Nom)
            If oCustomer.NomComercial > "" Then .Dest.Add(oCustomer.NomComercial)
            If oCustomer.Nif > "" Then
                .Dest.Add(DTONif.FullText(oCustomer))
            End If
            If oCustomer.Nif2 > "" Then
                .Dest.Add(DTONif.FullText2(oCustomer))
            End If

            .CustomLines = New List(Of String)
            Dim oZona As DTOZona = DTOAddress.Zona(oCustomer.Address)
            If DTOZona.IsCanarias(oZona) AndAlso DTOInvoice.GetBaseImponible(oInvoice).Eur <= 3000 Then
                .CustomLines.Add("T2LF - MERCANCIA SIN DECLARACIÓN DE EXPEDICIÓN")
            End If

            For Each sl As String In oCustomer.Address.Text.Split(vbCrLf)
                If sl > "" Then .Dest.Add(sl.Trim)
            Next

            .Dest.Add(DTOAddress.ZipyCit(oCustomer.Address))
            If Not oCustomer.Address.IsEsp Then
                .Dest.Add("(" & DTOAddress.Country(oCustomer.address).langNom.tradueix(oLang) & ")")
            End If


            .Fch = oInvoice.Fch
            .Num = oInvoice.NumeroYSerie()
            .Obs.Add(oInvoice.Fpg)

            If oInvoice.Ob1 > "" Then .Obs.Add(oInvoice.Ob1)
            If oInvoice.Ob2 > "" Then .Obs.Add(oInvoice.Ob2)
            If oInvoice.Ob3 > "" Then .Obs.Add(oInvoice.Ob3)
            If oCustomer.SuProveedorNum > "" Then
                .Obs.Add(oLang.tradueix("Proveedor num.", "Proveidor num.", "Supplier code ") & oCustomer.SuProveedorNum)
            End If
            If DTOContact.ExportCod(oInvoice.Customer) = ExportCods.Extracomunitari Then
                .Obs.Add(oLang.tradueix("Exento de Iva (art. 21.2º Ley 37/1992)", "Exempt de Iva (art. 21.2º Llei 37/1992)", "VAT exemption (art. 21.2º Ley de IVA)"))
            End If
            If oInvoice.Cfp <> 3 And oInvoice.Total IsNot Nothing Then
                .Obs.Add(oLang.tradueix("empresa asociada a ASNEF", "empresa asociada a ASNEF", "member of ASNEF"))
            End If
            .Incoterm = oCustomer.Incoterm
            '.Dto = oInvoice.Dto
            '.PuntsQty = mPuntsQty
            '.PuntsTipus = mPuntsTipus
            '.PuntsBase = mPuntsBase
            '.Dpp = mDppPct
            .IvaBaseQuotas = oInvoice.IvaBaseQuotas
            .RecarrecEquivalencia = .IvaBaseQuotas.Exists(Function(x) x.Tax.Codi = DTOTax.Codis.Recarrec_Equivalencia_Standard)
            For Each oDelivery As DTODelivery In oInvoice.Deliveries
                .Itms.Add(New DTODocItm(, , , , , , , 4))
                .Itms.Add(New DTODocItm(DTOInvoice.DocAlbText(oDelivery, oLang), DTODoc.FontStyles.Bold))
                Dim oPurchaseOrder As New DTOPurchaseOrder
                Dim oSpv As New DTOSpv
                For Each itm In oDelivery.Items
                    If oDelivery.Cod = DTOPurchaseOrder.Codis.Client Then
                        If Not itm.PurchaseOrderItem.PurchaseOrder.Equals(oPurchaseOrder) Then
                            oPurchaseOrder = itm.PurchaseOrderItem.purchaseOrder
                            .Itms.Add(New DTODocItm(DTOInvoice.DocPdcText(oPurchaseOrder, oLang), DTODoc.FontStyles.Italic, , , , , 2, 2))
                        End If
                    End If
                    If oDelivery.Cod = DTOPurchaseOrder.Codis.Reparacio Then
                        If Not itm.Spv.Equals(oSpv) Then
                            Dim i As Integer
                            oSpv = itm.Spv
                            Dim oSpvTextArray As List(Of String) = oSpv.Lines(oInvoice.Customer.Lang)
                            Dim iSpvTextLines As Integer = oSpvTextArray.Count
                            For i = 0 To iSpvTextLines - 1
                                .Itms.Add(New DTODocItm(oSpvTextArray(i), DTODoc.FontStyles.Italic, , , , , , iSpvTextLines - i + 2))
                            Next
                        End If
                    End If

                    Dim sSku As String = IIf(oCustomer.Lang.Equals(DTOLang.ENG), DTOProductSku.RefYNomPrv(itm.Sku), itm.Sku.NomLlarg)
                    'If BlInsertEans And itm.Art.Cbar IsNot Nothing Then
                    'sSku = itm.Art.Cbar.Value & " " & sSku
                    'End If

                    .Itms.Add(New DTODocItm(sSku, DTODoc.FontStyles.Regular, itm.qty, itm.price, itm.dto, 0, 4, Hyperlink:=DTOProductSku.UrlSegment(itm.sku)))

                    If itm.Bundle.Count > 0 Then
                        .Itms.Add(New DTODocItm(.Lang.tradueix("compuesto de los siguientes elementos:", "compost dels següents elements", "composed of the following elements:"),,,,,, 6))
                        For Each oChildItem In itm.Bundle
                            Dim sSkuNom = oChildItem.Sku.nomLlarg
                            If oDelivery.cod = DTOPurchaseOrder.Codis.client AndAlso oCustomer.lang.Equals(DTOLang.ENG) Then
                                sSkuNom = oChildItem.sku.RefYNomPrv()
                            End If
                            Dim oDocItm = New DTODocItm(sSkuNom,  , , , , , , 4)
                            oDocItm.LeftPadChars = 12
                            .Itms.Add(oDocItm)
                        Next
                    End If


                Next
            Next
        End With
        Return oDoc
    End Function


End Class

Public Class DTOInvoiceException
    Inherits Exception

    Property Cod As Cods

    Public Enum Cods
        _NotSet
        WrongNif
        MultipleDeliveries
        MissingPaymentTerms
        MissingIban
        UncompleteBank
        ObsTooLong
        MissingConcept
        MissingTipoFactura
        SiiLogged
    End Enum

    Public Sub New(oCod As Cods, Optional sMessage As String = "")
        MyBase.New(sMessage)
        _Cod = oCod
    End Sub

    Shared Function MultilineString(oInvoiceExceptions As List(Of DTOInvoiceException)) As String
        Dim sb As New Text.StringBuilder
        For Each item As DTOInvoiceException In oInvoiceExceptions
            sb.AppendLine(item.Message)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

End Class

