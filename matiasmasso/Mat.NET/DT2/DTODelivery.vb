Public Class DTODelivery
    Inherits DTOBaseGuid

    Property emp As DTOEmp
    Property id As Integer
    Property cod As DTOPurchaseOrder.Codis
    Property fch As Date
    Property import As DTOAmt
    Property kg As Decimal
    Property m3 As Decimal
    Property bultos As Integer

    Property mgz As DTOMgz
    Property platform As DTOCustomerPlatform
    Property transportista As DTOTransportista

    Property ports As DTOAmt
    Property ivaExempt As Boolean
    Property recarrec As DTOAmt
    Property customerDocURL As String
    Property transmisio As DTOTransmisio
    Property invoice As DTOInvoice

    Property customer As DTOCustomer
    Property proveidor As DTOProveidor
    Property nom As String
    Property address As DTOAddress
    Property tel As String

    Property cashCod As DTOCustomer.CashCodes
    Property portsCod As DTOCustomer.PortsCodes
    Property retencioCod As DTODelivery.RetencioCods
    Property importAdicional As DTOAmt 'Import adicional que es carrega sobre el de l'albarà per cobrar imports pendents a l'hora
    Property fchCobroReembolso As Date
    Property valorado As Boolean
    Property facturable As Boolean
    Property dto As Decimal
    Property dpp As Decimal
    Property paymentTerms As DTOPaymentTerms
    Property importacio As DTOImportacio
    Property exportCod As DTOInvoice.ExportCods
    Property Justificante As JustificanteCodes
    Property fchJustificante As Date
    Property fpg As String
    Property obs As String

    Property usrLog As DTOUsrLog

    Property items As List(Of DTODeliveryItem)
    Property purchaseOrders As List(Of DTOPurchaseOrder)
    Property etiquetesTransport As DTODocFile


    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTODeliveryItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Items = New List(Of DTODeliveryItem)
    End Sub

    Public Enum RetencioCods
        NotSet = -1
        Free = 0
        Altres = 1
        Transferencia = 2
        VISA = 3
    End Enum

    Public Enum PortsQualification
        NotSet
        Qualified
        LowVolume
        NotApplicable
    End Enum

    Public Enum JustificanteCodes
        None
        Solicitado
        Recibido
    End Enum


    Shared Function FactoryElCorteIngles(oPurchaseOrder As DTOPurchaseOrder, oMgz As DTOMgz, DtFch As Date, oUser As DTOUser) As DTODelivery
        Dim retval As New DTODelivery
        'evita loads per no enlentir carregues de grup

        Dim oCustomer As DTOCustomer = oPurchaseOrder.Customer
        ' If oCustomer Is Nothing Then oCustomer = DTOCustomer.FromContact(oPurchaseOrder.Contact)
        Dim oCcx As DTOCustomer = oCustomer.Ccx
        With retval
            .Emp = oUser.Emp
            .Mgz = oMgz
            .Fch = DtFch

            .Cod = DTOPurchaseOrder.Codis.Client
            .Customer = oCustomer
            .Nom = oCustomer.NomComercialOrDefault()
            .Fch = Today

            .CashCod = oCcx.CashCod
            .Facturable = True
            .RetencioCod = DTODelivery.RetencioCods.Free
            .Valorado = oCcx.AlbValorat

            .PortsCod = oCcx.PortsCod
            If oPurchaseOrder.Platform IsNot Nothing Then
                .Platform = oPurchaseOrder.Platform
                .Address = .Platform.Address
                .Tel = .Platform.Telefon
            End If

            .UsrLog = DTOUsrLog.Factory(oUser)
            .Items = New List(Of DTODeliveryItem)

        End With
        Return retval
    End Function

    Public ReadOnly Property Contact As DTOContact
        Get
            Dim retval As DTOContact = Nothing
            If _Customer IsNot Nothing Then
                retval = _Customer
            ElseIf _Proveidor IsNot Nothing Then
                retval = _Proveidor
            End If
            Return retval
        End Get
    End Property

    Public ReadOnly Property Liquid As DTOAmt
        Get
            Dim retval As DTOAmt = Nothing
            If _Import IsNot Nothing Then
                retval = _Import
            End If
            If _ImportAdicional IsNot Nothing Then
                If retval Is Nothing Then
                    retval = _ImportAdicional
                Else
                    retval.Add(_ImportAdicional)
                End If
            End If
            Return retval
        End Get
    End Property

    Shared Function Factory(oUser As DTOUser, oCod As DTOPurchaseOrder.Codis, oContact As DTOContact, Optional DtFch As Date = Nothing)
        'Traspas de magatzem
        If DtFch = Nothing Then DtFch = Today
        Dim retval As New DTODelivery
        With retval
            .Cod = oCod
            If .Cod = DTOPurchaseOrder.Codis.Proveidor Then
                .Proveidor = oContact
            Else
                .Customer = oContact
            End If
            .Fch = DtFch
            .UsrLog = DTOUsrLog.Factory(oUser)
        End With
        Return retval
    End Function

    Public Function Spvs() As List(Of DTOSpv)
        Dim retval As New List(Of DTOSpv)
        For Each item In _Items
            If item.Spv IsNot Nothing Then
                If Not retval.Any(Function(x) x.Equals(item.Spv)) Then
                    retval.Add(item.Spv)
                End If
            End If
        Next
        Return retval
    End Function

    Public Function GetPurchaseOrders() As List(Of DTOPurchaseOrder)
        Dim retval As New List(Of DTOPurchaseOrder)
        If _items IsNot Nothing Then
            retval = _items.Where(Function(x) x.purchaseOrderItem IsNot Nothing).GroupBy(Function(y) y.purchaseOrderItem.purchaseOrder.Guid).Select(Function(z) z.First.purchaseOrderItem.purchaseOrder).ToList
        End If
        Return retval
    End Function


    Shared Function Caption(oDelivery As DTODelivery, Optional oLang As DTOLang = Nothing) As String
        If oLang Is Nothing Then oLang = DTOApp.current.lang

        Dim sb As New System.Text.StringBuilder
        sb.Append(oLang.tradueix("Albarán ", "Albará ", "Delivery note "))
        sb.Append(oDelivery.Id & " ")
        sb.Append(oLang.tradueix("del ", "del ", "from ") & oDelivery.Fch.ToShortDateString)
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function Formatted() As String
        Dim retVal As String = Format(_Fch.Year, "0000") & Format(_Id, "000000")
        Return retVal
    End Function

    Public Function HasSameAddressOf(oCandidate As DTODelivery) As Boolean
        Dim retval As Boolean
        If _Nom = oCandidate.Nom Then
            If _Address.Equals(oCandidate.Address) Then
                retval = True
            End If
        End If
        Return retval
    End Function

    Shared Function PickUpDeadline(DtPickupfchfrom As Date) As Date
        Dim retval As Date
        Select Case DtPickupfchfrom.DayOfWeek
            Case DayOfWeek.Friday
                retval = DtPickupfchfrom.AddDays(3)
            Case Else
                retval = DtPickupfchfrom.AddDays(1)
        End Select
        Return retval
    End Function

    Shared Function DeliveryGLN(oDelivery As DTODelivery) As DTOEan
        Dim retVal As DTOEan = Nothing
        If oDelivery.Platform Is Nothing Then
            retVal = oDelivery.Customer.GLN
        Else
            retVal = oDelivery.Platform.GLN
        End If
        Return retVal
    End Function

    Public Function Incoterm() As DTOProveidor.Incoterms
        Dim retval As DTOProveidor.Incoterms = DTOProveidor.Incoterms.NotSet
        Select Case _ExportCod
            Case DTOInvoice.ExportCods.Intracomunitari, DTOInvoice.ExportCods.Extracomunitari
                If _PortsCod = DTOCustomer.PortsCodes.Reculliran Then
                    retval = DTOProveidor.Incoterms.EXW
                Else
                    retval = DTOProveidor.Incoterms.CIF
                End If
        End Select
        Return retval
    End Function

    Shared Function BaseImponible(oDelivery As DTODelivery) As DTOAmt
        Dim retval As DTOAmt = DTODelivery.SumaDeImports(oDelivery)
        Return retval
    End Function

    Public Function TaxBaseQuotas(oTaxes As List(Of DTOTax)) As List(Of DTOTaxBaseQuota) 'TO DEPRECATE
        Dim retval As New List(Of DTOTaxBaseQuota)
        Dim BlIva As Boolean = _ExportCod = DTOInvoice.ExportCods.Nacional
        Dim BlReq As Boolean = _Cod <> DTOPurchaseOrder.Codis.Proveidor AndAlso _Customer.CcxOrMe.Req
        If BlIva Then
            If _Items IsNot Nothing Then
                For Each oItm As DTODeliveryItem In _Items
                    Dim oQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oItm.IvaCod)
                    If oQuota Is Nothing Then
                        Dim oTax As DTOTax = DTOTax.Closest(oItm.IvaCod, _Fch)
                        oQuota = New DTOTaxBaseQuota(oTax, oItm.Import())
                        retval.Add(oQuota)
                        If BlReq Then
                            Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oQuota.Tax.Codi)
                            Dim oTaxReq As DTOTax = DTOTax.Closest(oTaxReqCodi, _Fch)
                            Dim oQuotaReq As New DTOTaxBaseQuota(oTaxReq, oItm.Import())
                            retval.Add(oQuotaReq)
                        End If
                    Else
                        oQuota.AddBase(oItm.Import())
                        'oQuota.Base.Add(oItm.Import())
                        If BlReq Then
                            Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oQuota.Tax.Codi)
                            Dim oQuotaReq As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oTaxReqCodi)
                            'oQuotaReq.Base.Add(oItm.Import())
                            oQuotaReq.AddBase(oItm.Import())
                        End If
                    End If
                Next
            End If
        End If
        Return retval
    End Function
    Public Function TaxBaseQuotas() As List(Of DTOTaxBaseQuota)
        Dim retval As New List(Of DTOTaxBaseQuota)
        Dim BlIva As Boolean = _ExportCod = DTOInvoice.ExportCods.Nacional
        Dim BlReq As Boolean = _Cod <> DTOPurchaseOrder.Codis.Proveidor AndAlso _Customer.CcxOrMe.Req
        If BlIva Then
            If _Items IsNot Nothing Then
                For Each oItm As DTODeliveryItem In _Items
                    Dim oQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oItm.IvaCod)
                    If oQuota Is Nothing Then
                        Dim oTax As DTOTax = DTOTax.Closest(oItm.IvaCod, _Fch)
                        oQuota = New DTOTaxBaseQuota(oTax, oItm.Import())
                        retval.Add(oQuota)
                        If BlReq Then
                            Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oQuota.Tax.Codi)
                            Dim oTaxReq As DTOTax = DTOTax.Closest(oTaxReqCodi, _Fch)
                            Dim oQuotaReq As New DTOTaxBaseQuota(oTaxReq, oItm.Import())
                            retval.Add(oQuotaReq)
                        End If
                    Else
                        oQuota.AddBase(oItm.Import())
                        'oQuota.Base.Add(oItm.Import())
                        If BlReq Then
                            Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oQuota.Tax.Codi)
                            Dim oQuotaReq As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oTaxReqCodi)
                            'oQuotaReq.Base.Add(oItm.Import())
                            oQuotaReq.AddBase(oItm.Import())
                        End If
                    End If
                Next
            End If
        End If
        Return retval
    End Function

    Shared Function GetIvaBaseQuotas(items As List(Of DTODeliveryItem), Taxes As List(Of DTOTax), Iva As Boolean, Optional Req As Boolean = False) As List(Of DTOTaxBaseQuota)
        Dim retval As New List(Of DTOTaxBaseQuota)
        If Iva Then
            For Each oItm As DTODeliveryItem In items
                Dim oIvaBaseQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oItm.IvaCod)
                If oIvaBaseQuota Is Nothing Then
                    Dim oTax As DTOTax = Taxes.Find(Function(x) x.Codi = oItm.IvaCod)
                    oIvaBaseQuota = New DTOTaxBaseQuota(oTax, oItm.Import)
                    retval.Add(oIvaBaseQuota)
                    If Req Then
                        Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oIvaBaseQuota.Tax.Codi)
                        Dim oTaxReq As DTOTax = Taxes.Find(Function(x) x.Codi = oTaxReqCodi)
                        Dim oReqBaseQuota As New DTOTaxBaseQuota(oTaxReq, oItm.Import)
                        retval.Add(oReqBaseQuota)
                    End If
                Else
                    oIvaBaseQuota.AddBase(oItm.Import)
                    If Req Then
                        Dim oTaxReqCodi As DTOTax.Codis = DTOTax.ReqForIvaCod(oIvaBaseQuota.Tax.Codi)
                        Dim oReqBaseQuota As DTOTaxBaseQuota = retval.Find(Function(x) x.Tax.Codi = oTaxReqCodi)
                        oReqBaseQuota.AddBase(oItm.Import)
                    End If
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function IvaTipus(oDelivery As DTODelivery) As Decimal
        Dim retval As Decimal
        If Not oDelivery.IvaExempt Then
            Dim oCustomer As DTOCustomer = oDelivery.Customer
            If oCustomer.IVA Then
                retval = DTOTax.ClosestTipus(DTOTax.Codis.Iva_Standard)
            End If
        End If
        Return retval
    End Function

    Shared Function IvaAmt(oDelivery As DTODelivery) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If Not oDelivery.IvaExempt Then
            Dim oCustomer As DTOCustomer = oDelivery.Customer
            If oCustomer.IVA Then
                Dim DcTipus As Decimal = DTOTax.ClosestTipus(DTOTax.Codis.Iva_Standard)
                retval = SumaDeImports(oDelivery).Percent(DcTipus)
            End If
        End If
        Return retval
    End Function

    Shared Function ReqTipus(oDelivery As DTODelivery) As Decimal
        Dim retval As Decimal
        If Not oDelivery.IvaExempt Then
            Dim oCustomer As DTOCustomer = oDelivery.Customer
            If oCustomer.Req Then
                retval = DTOTax.ClosestTipus(DTOTax.Codis.Recarrec_Equivalencia_Standard)
            End If
        End If
        Return retval
    End Function

    Shared Function ReqAmt(oDelivery As DTODelivery) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If Not oDelivery.IvaExempt Then
            Dim oCustomer As DTOCustomer = oDelivery.Customer
            If oCustomer.IVA Then
                Dim DcTipus As Decimal = DTOTax.ClosestTipus(DTOTax.Codis.Recarrec_Equivalencia_Standard)
                retval = DTODelivery.SumaDeImports(oDelivery).Percent(DcTipus)
            End If
        End If
        Return retval
    End Function

    Shared Function SumaDeImports(oDelivery As DTODelivery) As DTOAmt
        Dim retval As DTOAmt = DTOAmt.Empty
        For Each item As DTODeliveryItem In oDelivery.Items
            retval.Add(item.Import)
        Next
        Return retval
    End Function



    Shared Function BaseImponible(items As List(Of DTODeliveryItem), Optional oCur As DTOCur = Nothing) As DTOAmt
        If oCur Is Nothing Then oCur = DTOApp.Current.Cur
        Dim retval = DTOAmt.Empty(oCur)
        For Each item In items
            retval.Add(item.Import())
        Next
        Return retval
    End Function

    Shared Function BaseImponible(oDeliveries As List(Of DTODelivery)) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oItem As DTODelivery In oDeliveries
            retval.Add(DTODelivery.BaseImponible(oItem))
        Next
        Return retval
    End Function

    Public Function BaseImponible() As DTOAmt
        Dim oCur As DTOCur = Nothing
        Select Case _Cod
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim oProveidor As DTOProveidor = _Proveidor
                oCur = _Proveidor.Cur
            Case Else
                oCur = DTOCur.Eur
        End Select
        Dim retval As DTOAmt = BaseImponible(Items, oCur)
        Return retval
    End Function

    Public Function TotalCash() As DTOAmt
        Dim retval As DTOAmt
        Dim Iva As Boolean
        Dim Req As Boolean

        Select Case _Cod
            Case DTOPurchaseOrder.Codis.Proveidor
                Dim oProveidor As DTOProveidor = _Proveidor
                retval = DTOAmt.Empty(_Proveidor.Cur)
                Dim oExportCod = oProveidor.Address.ExportCod
                Iva = (oExportCod = DTOInvoice.ExportCods.Nacional)
                Req = False
            Case Else
                Dim oCcx As DTOCustomer = _Customer.CcxOrMe
                retval = DTOAmt.Empty(DTOCur.Eur)
                Iva = oCcx.IVA
                Req = oCcx.Req
        End Select

        retval = Me.BaseImponible().Clone
        Select Case _ExportCod
            Case DTOInvoice.ExportCods.Nacional
                Dim oQuotas = TaxBaseQuotas()
                For Each oQuota In oQuotas
                    Select Case oQuota.Tax.Codi
                        Case DTOTax.Codis.Iva_Standard, DTOTax.Codis.Iva_Reduit, DTOTax.Codis.Iva_SuperReduit
                            retval.Add(oQuota.Quota)
                        Case DTOTax.Codis.Recarrec_Equivalencia_Standard, DTOTax.Codis.Recarrec_Equivalencia_Reduit, DTOTax.Codis.Recarrec_Equivalencia_SuperReduit
                            retval.Add(oQuota.Quota)
                    End Select
                Next
        End Select

        Return retval
    End Function

    Shared Function VolumeM3(oItems As List(Of DTODeliveryItem)) As Decimal
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.VolumeM3(x))
        Return retval
    End Function

    Shared Function WeightKg(oItems As List(Of DTODeliveryItem)) As Integer
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.WeightKg(x))
        Return retval
    End Function

    Shared Function DeliveryLocationNom(oDelivery As DTODelivery) As String
        Dim retVal As String = ""
        Dim oAddress As DTOAddress = Nothing
        If oDelivery.Platform Is Nothing Then
            oAddress = oDelivery.Address
        Else
            oAddress = oDelivery.Platform.Address
        End If
        If oAddress IsNot Nothing Then
            If oAddress.Zip IsNot Nothing Then
                If oAddress.Zip.Location IsNot Nothing Then
                    retVal = oAddress.Zip.Location.Nom
                End If
            End If
        End If
        Return retVal
    End Function

    Shared Function FileName(oDelivery As DTODelivery, Optional BlProforma As Boolean = False) As String
        Dim oDeliveries = {oDelivery}.ToList
        Return FileName(oDeliveries, BlProforma)
    End Function

    Shared Function FileName(oDeliveries As List(Of DTODelivery), Optional BlProforma As Boolean = False) As String
        Dim retval As String = ""
        If oDeliveries.Count > 0 Then
            retval = String.Format("M+O.{0}.pdf", LabelAndNumsText(oDeliveries, BlProforma))
        End If
        Return retval
    End Function

    Shared Function LabelAndNumsText(oDeliveries As List(Of DTODelivery), BlProforma As Boolean) As String
        Dim retval As String = ""
        If oDeliveries.Count > 0 Then
            Dim sAlbLabel As String = Label(oDeliveries, BlProforma)
            Dim sNums = NumsText(oDeliveries)
            retval = String.Format("{0} {1}", sAlbLabel, sNums)
        End If
        Return retval
    End Function

    Shared Function NumsText(oDeliveries As List(Of DTODelivery)) As String
        Dim retval As String = ""
        If oDeliveries.Count > 0 Then
            If DTODelivery.AreConsecutive(oDeliveries) Then
                retval = String.Format("{0}-{1}", oDeliveries.First.Formatted, oDeliveries.Last.Formatted)
            ElseIf oDeliveries.Count < 4 Then
                Dim sb As New Text.StringBuilder
                For Each oDelivery In oDeliveries
                    If oDelivery.UnEquals(oDeliveries.First) Then sb.Append("-")
                    sb.Append(oDelivery.Formatted)
                Next
                retval = sb.ToString
            End If
        End If
        Return retval
    End Function

    Shared Function Label(oDeliveries As List(Of DTODelivery), BlProforma As Boolean) As String
        Dim retval As String = ""
        Dim olang As DTOLang = Lang(oDeliveries.First)
        If BlProforma Then
            If oDeliveries.Count = 1 Then
                retval = "proforma"
            Else
                retval = "proformas"
            End If
        Else
            If oDeliveries.Count = 1 Then
                retval = olang.tradueix("albarán", "albarà", "delivery", "nota de entrega")
            Else
                retval = olang.tradueix("albaranes", "albarans", "deliveries", "notas de entrega")
            End If
        End If
        Return retval
    End Function

    Shared Function InvoiceText(oDelivery As DTODelivery, oLang As DTOLang) As String
        Dim retval As String = ""
        If oDelivery.Invoice IsNot Nothing Then
            retval = String.Format("{0} {1} {2} {3}", oLang.tradueix("factura", "factura", "invoice"), oDelivery.Invoice.Num, oLang.tradueix("del", "del", "from"), Format(oDelivery.Fch, "dd/MM/yy"))
        End If
        Return retval
    End Function

    Shared Function AreConsecutive(oDeliveries As List(Of DTODelivery)) As Boolean
        Dim retval As Boolean = True
        If oDeliveries.Count > 1 Then
            Dim sortedNums = DTODelivery.Sorted(oDeliveries).Select(Function(x) x.Id).ToList
            For i = 1 To sortedNums.Count - 1
                If sortedNums(i) <> sortedNums(i - 1) + 1 Then
                    retval = False
                    Exit For
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function Sorted(oDeliveries As List(Of DTODelivery)) As List(Of DTODelivery)
        Dim retval = oDeliveries.OrderBy(Function(x) x.Id).OrderBy(Function(y) y.Fch.Year).ToList
        Return retval
    End Function

    Shared Function Formatted(oDelivery As DTODelivery) As String
        Dim retVal As String = Format(oDelivery.Fch.Year, "0000") & Format(oDelivery.Id, "000000")
        Return retVal
    End Function

    Shared Function SameCustomer(oDeliveries As List(Of DTODelivery)) As Boolean
        Dim retval As Boolean = oDeliveries.All(Function(x) x.Customer.Equals(oDeliveries.First.Customer))
        Return retval
    End Function

    Shared Function DiscountExists(oDelivery As DTODelivery) As Boolean
        Return oDelivery.Items.Any(Function(x) x.Dto <> 0)
    End Function

    Shared Function LoadMadeIns(ByRef oDelivery As DTODelivery, oCountries As List(Of DTOCountry), exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = True
        For Each item As DTODeliveryItem In oDelivery.Items
            Dim oSku As DTOProductSku = item.Sku
            Dim oCountry As DTOCountry = DTOProductSku.MadeInOrInherited(oSku)
            If oCountry IsNot Nothing Then
                oSku.MadeIn = oCountries.FirstOrDefault(Function(x) x.Equals(oCountry))
            End If
        Next
        Return retval
    End Function

    Shared Function Lang(oDelivery As DTODelivery) As DTOLang
        Dim retval As DTOLang = DTOLang.ESP
        If oDelivery IsNot Nothing Then
            If oDelivery.Customer IsNot Nothing Then
                If oDelivery.Customer.Lang IsNot Nothing Then
                    retval = oDelivery.Customer.Lang
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function FullNom(oDelivery As DTODelivery) As String
        Dim olang As DTOLang = DTODelivery.Lang(oDelivery)
        Dim sAlbLabel As String = olang.tradueix("albaran", "albara", "delivery")
        Dim retval As String = String.Format("{0} {1} del {2:dd/MM/yy} a {3}", sAlbLabel, Formatted(oDelivery), oDelivery.Fch, oDelivery.Customer.FullNom)
        Return retval
    End Function

    Shared Function DeliveryTerms(oDelivery As DTODelivery) As String
        Dim retval As String = ""
        Select Case oDelivery.PortsCod
            Case DTOCustomer.PortsCodes.Altres
                retval = "(altres)"
            Case DTOCustomer.PortsCodes.EntregatEnMa
                retval = "(entregat en ma)"
            Case DTOCustomer.PortsCodes.Pagats
                Dim oTransportista As DTOTransportista = oDelivery.Transportista
                If oTransportista Is Nothing Then
                    retval = "(pagats)"
                Else
                    retval = oTransportista.Abr
                End If
            Case DTOCustomer.PortsCodes.Reculliran
                retval = "(reculliran)"
        End Select
        Return retval
    End Function

    Shared Function GetPortsQualification(oDelivery As DTODelivery) As DTODelivery.PortsQualification
        Dim retval As DTODelivery.PortsQualification = DTODelivery.PortsQualification.NotApplicable
        Dim DcSumaDeImports As Decimal = DTODelivery.SumaDeImports(oDelivery).Eur
        Dim oCustomer As DTOCustomer = oDelivery.Customer
        Select Case oCustomer.PortsCondicio
            Case DTOCustomer.PortsCondicions.PeninsulaBalears
                retval = DTODelivery.PortsQualification.LowVolume
                If DcSumaDeImports > 300 Then
                    retval = DTODelivery.PortsQualification.Qualified
                Else
                    Dim DcSumaTommeeTippee As Decimal = oDelivery.Items.Where(Function(x) x.PurchaseOrderItem.Sku.Category.Brand.Equals(DTOProductBrand.wellknown(DTOProductBrand.wellknowns.TommeeTippee))).Sum(Function(x) x.Qty * x.Price.Eur * (100 - x.Dto) / 100)
                    If DcSumaTommeeTippee > 150 Then retval = DTODelivery.PortsQualification.Qualified
                End If
            Case DTOCustomer.PortsCondicions.Canaries
                retval = IIf(DcSumaDeImports > 400, DTODelivery.PortsQualification.Qualified, DTODelivery.PortsQualification.LowVolume)
            Case DTOCustomer.PortsCondicions.Andorra
                retval = IIf(DcSumaDeImports > 400, DTODelivery.PortsQualification.Qualified, DTODelivery.PortsQualification.LowVolume)
            Case DTOCustomer.PortsCondicions.ResteDelMon
                retval = DTODelivery.PortsQualification.NotApplicable
            Case DTOCustomer.PortsCondicions.eCom
                retval = DTODelivery.PortsQualification.NotApplicable
        End Select
        Return retval
    End Function

    Shared Function DocSpvText(oDelivery As DTODelivery, ByVal oSpv As DTOSpv) As String
        Dim oLang As DTOLang
        oLang = oDelivery.Customer.Lang
        Dim StDoc As String = oLang.tradueix("Reparación ", "Reparació ", "Item repaired ")
        Dim StFrom As String = oLang.tradueix("de fecha", "de data", "from")
        Dim retval As String = StDoc & oSpv.Id & ": " & DTOProduct.GetNom(oSpv.Product)
        Return retval
    End Function

End Class


Public Class DTODeliveryItem
    Inherits DTOBaseGuid

    Property delivery As DTODelivery
    Property lin As Integer
    Property purchaseOrderItem As DTOPurchaseOrderItem
    Property mgz As DTOMgz
    Property repLiq As DTORepLiq
    Property repLiqs As New List(Of DTORepLiq)
    Property repCom As DTORepCom
    Property repComLiquidable As DTORepComLiquidable
    Property spv As DTOSpv
    Property qty As Integer
    Property sku As DTOProductSku
    Property price As DTOAmt
    Property dto As Single
    Property pmc As Decimal
    Property cod As Cods
    Property incentius As List(Of DTOIncentiu)
    Property incentiu As DTOIncentiu
    Property ivaCod As DTOTax.Codis = DTOTax.Codis.Iva_Standard

    Property bundle As List(Of DTODeliveryItem)

    Public Enum Cods
        NotSet = 0
        Entrada = 11
        TraspasEntrada = 12
        Unknown13 = 13
        Unknown21 = 21
        Unknown31 = 31
        Unknown32 = 32
        Sortida = 51
        TraspasSortida = 52
        Unknown53 = 53
        Reparacio = 61
        Unknown62 = 62
        Unknown73 = 73
        Unknown81 = 81
        Unknown82 = 82
        Unknown91 = 91
    End Enum

    Public Sub New()
        MyBase.New()
        _Bundle = New List(Of DTODeliveryItem)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Bundle = New List(Of DTODeliveryItem)
    End Sub

    Shared Function Factory(oPurchaseOrderItem As DTOPurchaseOrderItem, iQty As Integer, Optional oPrice As DTOAmt = Nothing, Optional DcDto As Nullable(Of Decimal) = Nothing) As DTODeliveryItem
        Dim retval As New DTODeliveryItem
        With retval
            .PurchaseOrderItem = oPurchaseOrderItem
            .Qty = iQty
            .Price = IIf(oPrice Is Nothing, oPurchaseOrderItem.Price, oPrice)
            .Dto = IIf(DcDto Is Nothing, oPurchaseOrderItem.Dto, DcDto)
        End With

        For Each pnc In oPurchaseOrderItem.bundle
            Dim arc As New DTODeliveryItem
            With arc
                .purchaseOrderItem = pnc
                .purchaseOrderItem.purchaseOrder = oPurchaseOrderItem.purchaseOrder
                .qty = iQty
                .price = pnc.price
                .dto = pnc.dto
            End With
            retval.bundle.Add(arc)
        Next
        Return retval
    End Function

    Shared Function Factory(oSpv As DTOSpv, oSku As DTOProductSku, Optional iQty As Integer = 1, Optional oPrice As DTOAmt = Nothing, Optional DcDto As Decimal = 0) As DTODeliveryItem
        Dim retval As New DTODeliveryItem
        With retval
            .Spv = oSpv
            .Sku = oSku
            .Qty = iQty
            .Price = oPrice
            .Dto = DcDto
        End With
        Return retval
    End Function

    Public Function NetPrice() As DTOAmt
        Dim retval As DTOAmt = _Price.Clone
        If _Dto <> 0 Then
            retval.DeductPercent(_Dto)
        End If
        Return retval
    End Function


    Public Function Import() As DTOAmt
        Dim retval As DTOAmt = DTOAmt.Import(_Qty, _Price, _Dto)
        Return retval
    End Function

    Shared Function VolumeM3(oItem As DTODeliveryItem) As Decimal
        Dim DcVolumeM3 As Decimal = oItem.Sku.VolumeM3OrInherited
        Dim retval As Decimal = oItem.Qty * DcVolumeM3
        Return retval
    End Function

    Shared Function WeightKg(oItem As DTODeliveryItem) As Integer
        Dim iWeightKg As Integer = oItem.Sku.WeightKgOrInherited
        Dim retval As Integer = oItem.Qty * iWeightKg
        Return retval
    End Function

    Shared Function Bultos(oItem As DTODeliveryItem) As Integer
        Dim retval As Integer
        Dim innerPack As Integer = oItem.Sku.InnerPackOrInherited
        If innerPack <= 0 Then
            retval = 1
        ElseIf oItem.Qty <= innerPack Then
            retval = 1
        ElseIf oItem.Qty Mod innerPack = 0 Then
            retval = oItem.Qty / innerPack
        Else
            retval = oItem.Qty / innerPack + 1
        End If
        Return retval
    End Function

    Shared Function VolumeM3(oItems As List(Of DTODeliveryItem)) As Decimal
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.VolumeM3(x))
        Return retval
    End Function

    Shared Function WeightKg(oItems As List(Of DTODeliveryItem)) As Integer
        Dim retval As Decimal = oItems.Sum(Function(x) DTODeliveryItem.WeightKg(x))
        Return retval
    End Function

    Shared Function BaseImponible(oItems As List(Of DTODeliveryItem)) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each item In oItems
            retval.Add(item.Import)
        Next
        Return retval
    End Function


    Shared Function ComValue(item As DTODeliveryItem) As DTOAmt
        Dim retval = DTOAmt.Empty
        If item.RepCom IsNot Nothing Then
            retval = item.Import.Percent(item.RepCom.Com)
        End If
        Return retval
    End Function


    Shared Function Excel(oItems As List(Of DTODeliveryItem), sFilename As String) As MatHelperStd.ExcelHelper.Sheet

        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sFilename)
        With retval
            .AddColumn("Transmisió", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Albarà", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Data", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
            .AddColumn("Procedencia/Destinació", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("Entrades", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Sortides", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Stock", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Preu", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("Dte", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Percent)
        End With

        Dim oSortedItems = oItems.OrderBy(Function(x) x.delivery.id).OrderBy(Function(y) y.delivery.fch)
        Dim iStock As Integer = 0
        For Each item In oSortedItems
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            With oRow
                If item.delivery.transmisio Is Nothing Then
                    oRow.AddCell()
                Else
                    .AddCell(item.delivery.transmisio.id)
                End If
                oRow.AddCell(item.delivery.id)
                oRow.AddCell(item.delivery.fch)
                oRow.AddCell(item.delivery.customer.FullNom)
                If item.cod < 50 Then
                    oRow.AddCell(item.qty)
                    oRow.AddCell()
                Else
                    oRow.AddCell()
                    oRow.AddCell(item.qty)
                End If
                oRow.AddFormula("IF(ISNUMBER(R[-1]C),R[-1]C,0)+RC[-2]-RC[-1]")
                oRow.AddCellAmt(item.price)
                oRow.AddCell(item.dto)
            End With
        Next
        Return retval
    End Function

End Class

