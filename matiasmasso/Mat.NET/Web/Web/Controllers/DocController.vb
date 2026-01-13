Public Class DocController
    Inherits _MatController

    <HttpGet>
    Public Overridable Function Excel(ByVal fileGuid As String, ByVal fileName As String) As ActionResult
        If TempData(fileGuid) IsNot Nothing Then
            Dim data As Byte() = TryCast(TempData(fileGuid), Byte())
            Return File(data, "application/vnd.ms-excel", fileName)
        Else
            Return New EmptyResult()
        End If
    End Function

    Async Function Index(cod As String, Optional id As String = "", Optional filename As String = "") As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oCod As DTODocFile.Cods
        Dim oLang = ContextHelper.Lang

        If id = "" Then
            If cod.Length = 24 Then
                oCod = DTODocFile.Cods.download
                id = cod
            End If
        ElseIf GuidHelper.IsGuid(cod) Then
            'per compatibilitat amb la web antiga doncs hi han enllaços por ahi
            'Dim oGuid As New Guid(cod)
            'Dim oDocfileSrc As DTODocFileSrc = DTODocFile.GetHashFromGuid(oGuid)
            'If oDocfileSrc IsNot Nothing Then
            'cod = oDocfileSrc.SrcCod
            'id = oDocfileSrc.DocFile.Hash
            'If cod = DTODocFile.Cods.Download Then
            'id = CryptoHelper.StringToHexadecimal(id)
            'End If
            'End If
        End If

        oCod = cod
        Select Case oCod
            Case DTODocFile.Cods.pdc
                retval = Await PurchaseOrder(id)
            Case DTODocFile.Cods.alb
                retval = Await Delivery(id)
            Case DTODocFile.Cods.transmisioalbarans
                retval = Await TransmisioAlbarans(New Guid(id), exs)
            Case DTODocFile.Cods.tpaepubbook
                retval = EPub(id)
            Case DTODocFile.Cods.statsellout
                retval = Await SellOut()
            Case DTODocFile.Cods.selloutexcel
                retval = Await SellOutExcel(exs, id)
            Case DTODocFile.Cods.download
                Dim sHash As String = CryptoHelper.HexToString(id)
                Dim oDocFile = Await FEB2.DocFile.FindWithStream(sHash, exs)
                If exs.Count = 0 Then
                    retval = New FileContentResult(oDocFile.stream, FEB2.MediaHelper.ContentType(oDocFile.mime))
                    Await FEB2.DocFile.Log(oDocFile, ContextHelper.GetUser(), exs)
                Else
                    retval = View("NotFound")
                End If
            Case DTODocFile.Cods.incidenciesexcel
                Dim oUser = Await FEB2.User.Find(New Guid(id), exs)
                Dim year As Integer = filename
                retval = Await Incidencies(oUser, year)
            Case DTODocFile.Cods.ibanmandato
                retval = IbanMandato(id)
            Case DTODocFile.Cods.tarifaexcel
                If filename = "" Then
                    If GuidHelper.IsGuid(id) Then
                        retval = TarifaExcel(New Guid(id))
                    Else
                        retval = TarifaExcel(id, filename)
                    End If
                Else
                    retval = TarifaExcel(id, filename, filename)
                End If
            Case DTODocFile.Cods.tarifacsv
                'obsolet
            Case DTODocFile.Cods.openorders
                filename = String.Format("M+O {0} {1}.xlsx", ContextHelper.Lang.Tradueix("pedidos pendientes de entrega", "comandes pendents de entrega", "open orders"), Format(Today, "dd.MM.yyyy"))
                retval = Await OpenOrders(exs, id, filename)
            Case DTODocFile.Cods.purchaseorderexcel
                retval = PurchaseOrderExcel(New Guid(id), filename)
            Case DTODocFile.Cods.repcertretencio
                Dim oCert = Await FEB2.RepCertRetencio.FromBase64(id)
                FEB2.Contact.Load(oCert.Rep, exs)
                Dim oPdf As New LegacyHelper.PdfRepCertRetencio(oCert)
                Dim oDocFile = oPdf.DocFile
                Dim sFilename As String = String.Format("M+O Certificado retenciones {0}.T{1}.pdf", DTORepCertRetencio.Year(oCert), DTORepCertRetencio.Quarter(oCert))
                retval = LoginOrFile(oDocFile, sFilename)
            Case DTODocFile.Cods.skurefs
                filename = String.Format("M+O {0} {1}.xlsx", ContextHelper.Lang.Tradueix("referencias productos", "referencies productes", "product codes"), Format(Today, "yyyy.MM.dd"))
                retval = Await SkuRefs()
            Case DTODocFile.Cods.excelcustomerdeliveries3yearsdetail
                Dim oCustomer As New DTOCustomer(New Guid(id))
                Dim oSheet As ExcelHelper.Sheet = Await FEB2.Deliveries.ExcelHistoricDetallat(oCustomer, exs)
                retval = MyBase.ExcelResult(oSheet)
            Case DTODocFile.Cods.mediaresource
                'Dim oMediaResource = Await FEB2.MediaResource.Find(exs, New Guid(id))
                'If exs.Count = 0 Then
                'If oMediaResource Is Nothing Then
                'retval = Await ErrorNotFoundResult()
                'Else
                'Dim url = String.Format("~/Recursos/{0}", DTOMediaResource.TargetFilename(oMediaResource))
                'Dim path As String = Server.MapPath(url)
                'Dim oBuffer As Byte() = Nothing
                'If MatHelperStd.FileSystemHelper.GetStreamFromFile(path, oBuffer, exs) Then
                'filename = DTOMediaResource.FriendlyName(oMediaResource)
                'Dim sContentType As String = MediaHelper.ContentType(oMediaResource.Mime)
                'retval = New FileContentResult(oBuffer, sContentType) ' "application/pdf")
                'HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" & filename & "")
                'Else
                'retval = Await ErrorResult(exs)
                'End If
                'End If
                'Else
                'Return Await ErrorResult(exs)
                'End If
                Return retval
        End Select

        If filename = "" Then
            HttpContext.Response.AddHeader("content-disposition", "inline")
        Else
            Dim sContentDisposition As String = HttpContext.Response.Headers("content-disposition")
            If sContentDisposition = "" Then
                HttpContext.Response.AddHeader("content-disposition", "attachment; filename='" & filename & "'")
            End If
        End If

        Return retval
    End Function



    Function EPub(sGuid As String) As FileResult
        Dim retval As FileResult = Nothing
        'Dim oTpa As New Tpa(New Guid(sGuid))
        'Dim oEPubBook As DTOePubBook = oTpa.ePubBook
        'retval = New FileContentResult(oEPubBook.ToByteArray, "application/epub+zip")
        'retval.FileDownloadName = Server.UrlEncode(oTpa.Nom & ".ePub")
        Return retval
    End Function

    Async Function PurchaseOrder(sGuid As String) As Threading.Tasks.Task(Of FileResult)
        Dim exs As New List(Of Exception)
        Dim retval As FileResult = Nothing
        Dim oPurchaseOrder = Await FEB2.PurchaseOrder.Find(New Guid(sGuid), exs)
        If oPurchaseOrder IsNot Nothing Then
            'Dim oStream As Byte() = oPdc.PdfStream
            'retval = New FileContentResult(oStream, "application/pdf")
            'retval.FileDownloadName = Server.UrlEncode(oPdc.FileName)
        End If

        Return retval
    End Function



    Async Function Delivery(sGuid As String) As Threading.Tasks.Task(Of FileResult)
        Dim retval As FileResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oDelivery = Await FEB2.Delivery.Find(New Guid(sGuid), exs)
        Dim oStream As Byte() = Await FEB2.Delivery.Pdf(oDelivery, False, exs)
        If exs.Count = 0 Then
            retval = New FileContentResult(oStream, "application/pdf")
            Dim sFilename As String = DTODelivery.FileName(oDelivery)
            retval.FileDownloadName = Server.UrlEncode(sFilename)
        End If
        Return retval
    End Function

    Async Function TransmisioAlbarans(oGuid As Guid, exs As List(Of Exception)) As Threading.Tasks.Task(Of FileResult)
        Dim retval As FileResult = Nothing

        Dim oTransmisio = Await FEB2.Transmisio.Find(oGuid, exs)
        If exs.Count = 0 And oTransmisio IsNot Nothing Then
            Dim oStream As Byte() = Await FEB2.Transmisio.PdfDeliveries(oTransmisio, exs)
            If exs.Count = 0 And oStream IsNot Nothing Then
                retval = New FileContentResult(oStream, "application/pdf")
                retval.FileDownloadName = Server.UrlEncode("albarans transmisio " & oTransmisio.fch.Year & "." & oTransmisio.id & ".pdf")
            End If
        End If

        Return retval
    End Function

    Function IbanMandato(sid As String) As FileResult
        Dim retval As FileResult = Nothing
        Dim oIban As New DTOIban
        oIban.Digits = sid
        If oIban IsNot Nothing Then
            'Dim oDocFile As DTODocFile = BLL_IbanMandatos.FromDigits(sid)
            'Dim oStream As Byte() = oDocFile.Stream
            'retval = New FileContentResult(oStream, "application/pdf")
            'retval.FileDownloadName = Server.UrlEncode("M+O - Mandato Bancario " & BLL_Iban.Formated(oIban))
        End If

        Return retval
    End Function

    Async Function OpenOrders(exs As List(Of Exception), customerguid As String, filename As String) As Threading.Tasks.Task(Of FileResult)
        Dim oGuid As New Guid(customerguid)
        Dim oCustomer = FEB2.Customer.FindSync(exs, oGuid)
        Dim items As List(Of DTOPurchaseOrderItem) = Nothing
        Select Case oCustomer.rol.id
            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                items = Await FEB2.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.client, GlobalVariables.Emp.Mgz)
            Case DTORol.Ids.manufacturer
                items = Await FEB2.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.proveidor, GlobalVariables.Emp.Mgz)
        End Select

        Dim oExcelSheet = FEB2.PurchaseOrder.Excel(items)

        Dim retval As FileResult = LoginOrFile(oExcelSheet)
        Return retval
    End Function

    Async Function SkuRefs() As Threading.Tasks.Task(Of FileResult)
        Dim exs As New List(Of Exception)
        Dim oSkus = Await FEB2.ProductCatalog.Refs(exs)
        Dim oExcelSheet As ExcelHelper.Sheet = DTOProductCatalog.RefsExcel(oSkus)

        Dim retval As FileResult = LoginOrFile(oExcelSheet)
        Return retval
    End Function

    Function TarifaExcel(customerguid As Guid) As ActionResult
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = Today
        Dim oCustomer = FEB2.Customer.FindSync(exs, customerguid)
        Dim oTarifa = FEB2.CustomerTarifa.LoadSync(exs, oCustomer, DtFch)
        Dim oExcelSheet As ExcelHelper.Sheet = oTarifa.Excel(oCustomer.lang)
        oExcelSheet.filename = String.Format("M+O {0} {1}.xlsx", oCustomer.lang.Tradueix("tarifa de precios a", "tarifa de preus a", "price list"), Format(DtFch, "dd.MM.yyyy"))

        Dim retval = LoginOrFile(oExcelSheet)
        Return retval
    End Function

    Function TarifaExcel(customerguid As String, fch As Long, ByRef filename As String) As FileResult
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = DateTime.FromFileTime(fch)
        Dim oGuid As New Guid(customerguid)
        Dim oCustomer = FEB2.Customer.FindSync(exs, oGuid)
        Dim oTarifa = FEB2.CustomerTarifa.LoadSync(exs, oCustomer, DtFch)
        Dim oExcelSheet As ExcelHelper.Sheet = oTarifa.Excel(oCustomer.lang)
        oExcelSheet.filename = String.Format("M+O {0} {1}.xlsx", oCustomer.lang.Tradueix("tarifa de precios a", "tarifa de preus a", "price list"), Format(DtFch, "dd.MM.yyyy"))

        Dim retval As FileResult = LoginOrFile(oExcelSheet)
        Return retval
    End Function

    Function TarifaExcel(fch As Long, ByRef filename As String) As FileResult
        Dim exs As New List(Of Exception)
        Dim DtFch As Date = DateTime.FromFileTime(fch)
        Dim oUser As DTOUser = ContextHelper.GetUser
        Dim oTarifa = FEB2.CustomerTarifa.LoadSync(exs, oUser, DtFch)
        Dim oExcelSheet As ExcelHelper.Sheet = oTarifa.Excel(ContextHelper.Lang)
        filename = String.Format("M+O {0} {1}.xlsx", ContextHelper.Lang.Tradueix("tarifa de precios a", "tarifa de preus a", "price list"), Format(DtFch, "dd.MM.yyyy"))
        Dim retval As FileResult = LoginOrFile(oExcelSheet)
        Return retval
    End Function


    Async Function Incidencies(oUser As DTOUser, year As Integer) As Threading.Tasks.Task(Of FileResult)
        Dim exs As New List(Of Exception)
        Dim model = Await FEB2.Incidencias.Model(exs, oUser, False,,, year)
        Dim oCulture = ContextHelper.GetCultureInfo
        Dim oLang = ContextHelper.Lang
        Dim oSheet As ExcelHelper.Sheet = FEB2.IncidenciaQuery.ExcelSheet(model, oCulture, ContextHelper.GetLang)
        Return LoginOrFile(oSheet)
    End Function

    Function PurchaseOrderExcel(OrderGuid As Guid, ByRef filename As String) As FileResult
        Dim exs As New List(Of Exception)
        Dim oPurchaseOrder = FEB2.PurchaseOrder.FindSync(exs, OrderGuid)
        filename = DTOPurchaseOrder.filename(oPurchaseOrder, MimeCods.Xlsx)
        Dim oSheet = FEB2.PurchaseOrder.Excel(oPurchaseOrder.items)
        Dim bytes As Byte() = Nothing
        LegacyHelper.ExcelHelper.Stream(oSheet, bytes)
        Dim retval As FileResult = New FileContentResult(bytes, "application/vnd.openxmlformats-officedocument." + "spreadsheetml.sheet")

        Return retval
    End Function

    Async Function SellOut() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As FileResult = Nothing
        Dim iYear As Integer = Today.Year
        Dim oContact = Await ContextHelper.Contact(exs)
        If exs.Count = 0 Then
            If oContact IsNot Nothing Then
                ' Dim oProveidor As New DTOProveidor(oContact.Guid)
                ' Dim oStat As DTOStat = BLLStat.SellOut(oProveidor, iYear)
                ' Dim src As String = BLLStat.Csv(oStat)
                ' Dim bytes As Byte() = New Byte(src.Length * 2 - 1) {}
                ' System.Buffer.BlockCopy(src.ToCharArray(), 0, bytes, 0, bytes.Length)
                ' retval = New FileContentResult(bytes, "text/csv")
                ' Dim sFilename As String = String.Format("M+O sell-out data {0}.{1}.{2} {3}:{4}.csv", Today.Year, Today.Month, Today.Day, Now.Hour, Now.Minute)
                ' HttpContext.Response.AddHeader("content-disposition", "attachment; filename='" & sFilename & "'")
            End If
            Return LoginOrFile(retval)
        Else
            Return View("Error")
        End If
    End Function


    Async Function SellOutExcel(exs As List(Of Exception), sUrlFriendlyBase64Json As String) As Threading.Tasks.Task(Of ActionResult)
        Dim oParameters As Dictionary(Of String, String) = CryptoHelper.FromUrlFriendlyBase64Json(sUrlFriendlyBase64Json)
        Dim oUser = ContextHelper.GetUser
        oParameters.Add("User", oUser.Guid.ToString())
        Dim oSellout = Await FEB2.SellOut.FromParameters(exs, oParameters)
        oSellout.user = oUser
        Dim pSellout = Await FEB2.SellOut.Load(exs, oSellout)
        If exs.Count = 0 Then
            Dim oSheet As ExcelHelper.Sheet = DTOSellOut.Excel(exs, pSellout)
            If exs.Count = 0 Then
                Return LoginOrFile(oSheet)
            Else
                Return View("Error")
            End If
        Else
            Return View("Error")
        End If

    End Function

    Function Diari(fch As Long, ByRef filename As String) As FileResult
        Dim DtFch As Date = DateTime.FromFileTime(fch)
        Dim oExercici As DTOExercici = DTOExercici.FromYear(Mvc.GlobalVariables.Emp, DtFch.Year)
        Dim oLlibre As List(Of DTOCcb) = Nothing
        'SaveFile(oLlibre, FileFormats.Csv)

        filename = String.Format("M+O {0} {1}.xlsx", ContextHelper.Lang.Tradueix("libro diario", "llibre diari", "Journal of accounting"), Format(DtFch, "dd.MM.yyyy"))
        'Dim retval As FileResult = LoginOrFile(oExcelSheet)
        'Return retval
        Return Nothing
    End Function

    Function MaybornSalesExcel(ByRef filename As String) As FileResult
        Return Nothing
    End Function


End Class

