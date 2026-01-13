Public Class EdiversaOrder


#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOEdiversaOrder
        Dim retval As DTOEdiversaOrder = EdiversaOrderLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Factory(src As String) As DTOEdiversaOrder
        Dim oEdiversaFile As New DTOEdiversaFile()
        oEdiversaFile.Stream = src
        oEdiversaFile.FchCreated = DTO.GlobalVariables.Now()
        Dim retval = BEBL.EdiversaOrder.Factory(oEdiversaFile)
        Return retval
    End Function

    Shared Function Factory(oEdiversaFile As DTOEdiversaFile) As DTOEdiversaOrder
        Dim retval = DTOEdiversaOrder.Factory(oEdiversaFile)
        MergeEdiversaOrder(retval)
        Return retval
    End Function

    Shared Function searchByDocNum(docnum As String) As List(Of DTOEdiversaOrder)
        Return EdiversaOrderLoader.searchByDocNum(docnum)
    End Function

    Shared Function Load(ByRef oEDiversaOrder As DTOEdiversaOrder) As Boolean
        Dim exs As New List(Of Exception)
        Dim retval As Boolean = EdiversaOrderLoader.Load(oEDiversaOrder)
        BEBL.EdiversaOrder.Validate(exs, oEDiversaOrder)
        Return retval
    End Function

    Shared Function Update(oEDiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Boolean
        'Dim sEan = DTOEan.eanValue(oEDiversaOrder.FacturarAEAN)
        'Dim oGuid As Guid = Nothing
        'If oEDiversaOrder.FacturarA IsNot Nothing Then oGuid = oEDiversaOrder.FacturarA.Guid
        'MailMessageHelper.MailAdmin("EDI NADIV Check NADIV leaving factory (2)", String.Format("FacturarA Ean: {0} Facturar a Guid: {1}", sEan, oGuid.ToString), exs)

        Return EdiversaOrderLoader.Update(oEDiversaOrder, exs)
    End Function

    Shared Function Update(oEDiversaOrder As DTOEdiversaOrder, oEdiversaFile As DTOEdiversaFile, oPurchaseOrder As DTOPurchaseOrder, ByRef exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean = EdiversaOrderLoader.Update(oEDiversaOrder, oEdiversaFile, oPurchaseOrder, exs)
        Return retval
    End Function

    Shared Function Delete(oEDiversaOrder As DTOEdiversaOrder, exs As List(Of Exception)) As Boolean
        Return EdiversaOrderLoader.Delete(oEDiversaOrder, exs)
    End Function

    Shared Function SetResult(exs As List(Of Exception), oEdiOrder As DTOEdiversaOrder, oPdc As DTOPurchaseOrder) As Boolean
        Return EdiversaOrderLoader.SetResult(exs, oEdiOrder, oPdc)
    End Function

#End Region

    Shared Function EdiFile(oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As DTOEdiversaFile
        Dim retval As DTOEdiversaFile = Nothing
        If oOrder Is Nothing Then
            exs.Add(New Exception("comanda buida"))
        Else
            PurchaseOrderLoader.Load(oOrder)
            EmpLoader.Load(oOrder.Emp)
            ContactLoader.Load(oOrder.Emp.Org)
            Dim oSender As New DTOEdiversaContact
            With oSender
                .Contact = oOrder.Emp.Org
                .Ean = .Contact.GLN
            End With

            Dim oReceiver As New DTOEdiversaContact
            With oReceiver
                .Contact = oOrder.Proveidor
                .Ean = .Contact.GLN
            End With

            retval = New DTOEdiversaFile
            With retval
                .Tag = DTOEdiversaFile.Tags.ORDERS_D_96A_UN_EAN008.ToString
                .Fch = oOrder.Fch
                .Sender = oSender
                .Receiver = oReceiver
                .Docnum = IIf(oOrder.Cod = DTOPurchaseOrder.Codis.proveidor, oOrder.Num, oOrder.Concept)
                .Amount = oOrder.SumaDeImportes()
                .Result = DTOEdiversaFile.Results.pending
                .ResultBaseGuid = oOrder
                .Stream = EdiMessage(oOrder, exs)
                .IOCod = DTOEdiversaFile.IOcods.outbox
            End With
        End If
        Return retval

    End Function

    Shared Function EdiMessage(oOrder As DTOPurchaseOrder, exs As List(Of Exception)) As String
        Dim retval As String = Ediversa_ORDERS_D_96A_UN_EAN008.Factory(oOrder, exs)
        Return retval
    End Function

    Shared Function Procesa(oEDiversaOrder As DTOEdiversaOrder, oUser As DTOUser, exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean
        Dim oPurchaseOrder As DTOPurchaseOrder = PurchaseOrder.FromEdiversa(oEDiversaOrder, oUser)
        Dim oDuplicate As DTOPurchaseOrder = PurchaseOrder.FindDuplicate(oPurchaseOrder.Contact, oPurchaseOrder.Fch, oPurchaseOrder.Concept)
        If oDuplicate Is Nothing Then
            Dim oEdiversaFile As DTOEdiversaFile = EdiversaFileLoader.Find(oEDiversaOrder.Guid)
            oEdiversaFile.ResultBaseGuid = oPurchaseOrder
            oEdiversaFile.Result = DTOEdiversaFile.Results.processed
            retval = EdiversaOrder.Update(oEDiversaOrder, oEdiversaFile, oPurchaseOrder, exs)
        Else
            Dim sMsg As String = String.Format("Comanda duplicada {0}. Ja es va entrar amb el numero {1} el {2:dd/MM/yy} a les {2:HH:mm}", oPurchaseOrder.Concept, oDuplicate.Num, oDuplicate.UsrLog.FchCreated)
            exs.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.NotSet, oEDiversaOrder, sMsg))
        End If
        Return retval
    End Function

    Shared Function ExceptionsPending(oEDiversaOrder As DTOEdiversaOrder) As Boolean
        Dim retval As Boolean
        If oEDiversaOrder.Exceptions.Count = 0 Then
            retval = oEDiversaOrder.Items.Exists(Function(x) x.Exceptions.Count > 0)
        Else
            retval = True
        End If
        Return retval
    End Function

    Shared Function FromEdiversaFile(src As DTOEdiversaFile) As DTOEdiversaOrder
        Dim retval As New DTOEdiversaOrder(src.Guid)
        EdiversaFileLoader.Load(src)
        LoadFromEdiversaFile(retval, src)
        Return retval
    End Function

    Shared Sub MergeEdiversaOrder(ByRef oEdiversaOrder As DTOEdiversaOrder)
        Try
            Dim eansInterlocutors = oEdiversaOrder.EdiversaFile.EansInterlocutors()
            Dim oInterlocutors = BEBL.Contacts.FromGLNs(eansInterlocutors)
            Dim eansSkus = oEdiversaOrder.EdiversaFile.EansSkus()
            Dim oCustomerTarifaDtos = New List(Of DTOCustomerTarifaDto)
            Dim oCliProductDtos = New List(Of DTOCliProductDto)
            Dim oCustomCosts = New List(Of DTOPricelistItemCustomer)
            With oEdiversaOrder
                .Proveedor = oInterlocutors.FirstOrDefault(Function(x) x.GLN.Equals(.ProveedorEAN))
                .Comprador = DTOCustomer.FromContact(oInterlocutors.FirstOrDefault(Function(x) x.GLN.Equals(.CompradorEAN)))
                .ReceptorMercancia = oInterlocutors.FirstOrDefault(Function(x) x.GLN.Equals(.ReceptorMercanciaEAN))
                .FacturarA = DTOCustomer.FromContact(oInterlocutors.FirstOrDefault(Function(x) x.GLN.Equals(.FacturarAEAN)))
                If .Proveedor Is Nothing Then .AddException(DTOEdiversaException.Cods.InterlocutorNotFound, String.Format("proveidor {0} desconegut", DTOEan.eanValue(.ProveedorEAN)))
                If .Comprador Is Nothing Then
                    .AddException(DTOEdiversaException.Cods.InterlocutorNotFound, String.Format("client {0} desconegut", DTOEan.eanValue(.CompradorEAN)), DTOEdiversaException.TagCods.NADBY, oEdiversaOrder)
                Else
                    .Customer = .Comprador.Clone()
                    oCustomerTarifaDtos = BEBL.CustomerTarifaDtos.Active(.Customer)
                    oCustomCosts = BEBL.PriceListItemsCustomer.Active(.Customer, oEdiversaOrder.FchDoc)
                    oCliProductDtos = BEBL.CliProductDtos.All(.Customer)
                End If
                If .ReceptorMercancia Is Nothing Then .AddException(DTOEdiversaException.Cods.InterlocutorNotFound, String.Format("receptor mercancia {0} desconegut", DTOEan.eanValue(.ReceptorMercanciaEAN)), DTOEdiversaException.TagCods.NADDP, oEdiversaOrder)
                If .FacturarA Is Nothing Then .AddException(DTOEdiversaException.Cods.InterlocutorNotFound, String.Format("destinatari factura {0} desconegut", DTOEan.eanValue(.FacturarAEAN)), DTOEdiversaException.TagCods.NADIV, oEdiversaOrder)

                Dim oSkus = BEBL.ProductSkus.FromEanValues(eansSkus)
                Dim oUnknownEans = eansSkus.Where(Function(x) Not oSkus.Any(Function(y) y.Ean13.Value = x)).ToList()
                If oUnknownEans.Count > 0 Then
                    Dim oArtCustRefs = BEBL.CustomerProducts.All(.Customer)
                    For Each sEan In oUnknownEans
                        Dim oArtCustRef = oArtCustRefs.FirstOrDefault(Function(x) x.DUN14 = sEan)
                        If oArtCustRef IsNot Nothing Then
                            Dim oSku = BEBL.ProductSku.Find(oArtCustRef.Sku.Guid)
                            'replace temporarily sku Ean with custom Ean from customer
                            oSku.Ean13 = New DTOEan(sEan)
                            oSkus.Add(oSku)
                        End If
                    Next
                End If
                For Each item In .Items
                    item.Validate(oSkus, oCustomCosts, oCustomerTarifaDtos, oCliProductDtos)
                Next
            End With

        Catch ex As Exception
            oEdiversaOrder.AddException(DTOEdiversaException.Cods.NotSet, ex.Message)
        End Try
    End Sub

    Shared Function FromNumComanda(oEmp As DTOEmp, year As Integer, NumComanda As String) As DTOEdiversaFile
        Return EdiversaFileLoader.FromNumComanda(oEmp, year, NumComanda)
    End Function

    Shared Sub LoadFromEdiversaFile(ByRef oOrder As DTOEdiversaOrder, src As DTOEdiversaFile)
        Dim exs As New List(Of Exception)

        Dim oCurs = BEBL.Curs.All
        src.LoadSegments()
        Dim oInterlocutor As DTOEdiversaFile.Interlocutors = EdiversaFile.Interlocutor(src)
        If oInterlocutor = DTOEdiversaFile.Interlocutors.unknown Then
            oOrder.AddException(DTOEdiversaException.Cods.InterlocutorNotFound, String.Format("El fitxer {0} no conté cap interlocutor registrat", src.FileName), DTOEdiversaException.TagCods.EdiversaFile, src)
        End If


        Dim oCustomerProducts As New List(Of DTOCustomerProduct)

        oOrder.Items = New List(Of DTOEdiversaOrderItem)
        Dim oItem As DTOEdiversaOrderItem = Nothing
        Dim oCur As DTOCur = Nothing

        Dim sb As New System.Text.StringBuilder
        For Each oSegment As DTOEdiversaSegment In src.Segments
            Try

                sb.AppendLine(String.Join("|", oSegment.Fields))
                With oOrder
                    Select Case oSegment.Fields.First
                        Case "ORD"
                            .DocNum = oSegment.Fields(1)
                            .Tipo = oSegment.Fields(2)
                            .Funcion = oSegment.Fields(3)
                        Case "DTM"
                            .FchDoc = DTOEdiversaFile.ParseFch(oSegment.Fields(1), oOrder.Exceptions)

                            If oSegment.Fields.Count > 2 Then
                                .FchDeliveryMin = DTOEdiversaFile.ParseFch(oSegment.Fields(2), oOrder.Exceptions) ' delivery date requested
                                If oSegment.Fields.Count > 3 Then
                                    .FchDeliveryMax = DTOEdiversaFile.ParseFch(oSegment.Fields(3), oOrder.Exceptions)
                                    'If oSegment.Fields.Count > 5 Then
                                    '    .FchDeliveryMin = DTOEdiversaFile.ParseFch(oSegment.Fields(5), oOrder.Exceptions)
                                    'End If
                                End If
                            End If
                        Case "FTX"
                            If .Obs > "" Then .Obs = .Obs & vbCrLf
                            .Obs = .Obs & oSegment.Fields(3)
                        Case "NADMS" 'message sender
                            .NADMS = DTOEan.Factory(oSegment.Fields(1))
                        Case "NADSU" 'proveidor
                            .Proveedor = CustomerLoader.FromGln(DTOEan.Factory(oSegment.Fields(1)))
                        Case "NADIV" 'facturar a
                            .FacturarA = CustomerLoader.FromGln(DTOEan.Factory(oSegment.Fields(1)))
                            .FacturarAEAN = DTOEan.Factory(oSegment.Fields(1))
                        Case "NADBY" 'comprador
                            .CompradorEAN = DTOEan.Factory(oSegment.Fields(1))

                            .Comprador = CustomerLoader.FromGln(.CompradorEAN)
                            If .Comprador Is Nothing Then
                                oOrder.AddException(DTOEdiversaException.Cods.ContactCompradorNotFound, String.Format("Comprador {0} no registrat", oSegment.Fields(1)))
                            Else
                                .Customer = .Comprador
                                oCustomerProducts = CustomerProducts.All(DirectCast(.Comprador, DTOCustomer).Ccx)
                                Select Case oInterlocutor
                                    Case DTOEdiversaFile.Interlocutors.ElCorteIngles
                                        If oSegment.Fields.Count > 2 Then 'El Corte Ingles
                                            .Departamento = oSegment.Fields(2)
                                        End If
                                        If oSegment.Fields.Count > 4 Then 'El Corte Ingles
                                            .Centro = oSegment.Fields(4)
                                        End If
                                End Select
                            End If
                        Case "NADDP" 'receptor de la mercancia
                            Select Case oInterlocutor
                                Case DTOEdiversaFile.Interlocutors.amazon
                                    .CompradorEAN = DTOEan.Factory(oSegment.Fields(1))
                                    .Comprador = CustomerLoader.FromGln(.CompradorEAN)
                                Case DTOEdiversaFile.Interlocutors.carrefour
                                    .CompradorEAN = DTOEan.Factory(oSegment.Fields(1))
                                    .Comprador = CustomerLoader.FromGln(.CompradorEAN)
                                    If .Comprador Is Nothing Then
                                        oOrder.AddException(DTOEdiversaException.Cods.ContactCompradorNotFound, String.Format("Comprador {0} no registrat", oSegment.Fields(1)))
                                    End If
                            End Select
                            .ReceptorMercanciaEAN = DTOEan.Factory(oSegment.Fields(1))
                            .ReceptorMercancia = CustomerLoader.FromGln(.ReceptorMercanciaEAN)
                        Case "CUX"
                            .Cur = oCurs.FirstOrDefault(Function(x) x.Tag = oSegment.Fields(1))

                        Case "LIN"
                            oItem = New DTOEdiversaOrderItem()
                            oItem.Parent = oOrder
                            oItem.Ean = DTOEan.Factory(oSegment.Fields(1))
                            oItem.Lin = oSegment.Fields(3)
                            oItem.Sku = ProductSku.FromEan(oItem.Ean)
                            .Items.Add(oItem)
                        Case "PIALIN"
                            Select Case oSegment.Fields(1)
                                Case "IN", "BP"
                                    oItem.RefClient = oSegment.Fields(2)
                                    If Not oCustomerProducts.Any(Function(x) x.Ref = oItem.RefClient) Then
                                        CustomerProduct.SaveIfMissing(DirectCast(.Comprador, DTOCustomer).Ccx, oItem.Sku, oItem.RefClient, exs)
                                    End If
                                Case "SA"
                                    oItem.RefProveidor = oSegment.Fields(2)
                                    If oItem.Sku Is Nothing Then
                                        If oItem.Ean.Value = "0000000000000" Then
                                            If oItem.RefProveidor = "" Then
                                                oItem.AddException(DTOEdiversaException.Cods.SkuNotFound, "producte sense EAN ni referencia de proveidor")
                                            Else
                                                Dim sRef As String = oItem.RefProveidor.Trim
                                                If sRef.IndexOf(" ") > 0 Then sRef = sRef.Substring(0, sRef.IndexOf(" "))
                                                Dim oSkus As List(Of DTOProductSku) = ProductSkuLoader.FromProveidor(sRef)
                                                Select Case oSkus.Count
                                                    Case 0
                                                        oItem.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("producte sense EAN i referencia de proveidor {0} desconeguda", oItem.RefProveidor))
                                                    Case 1
                                                        oItem.Sku = oSkus.First
                                                    Case Else
                                                        oItem.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("producte sense EAN i amb referencia de proveidor {0} repetida entre diferents productes", oItem.RefProveidor))
                                                        'salta exception 2 productes amb la mateixa referencia de proveidor
                                                End Select
                                            End If
                                        End If
                                    End If

                            End Select
                        Case "IMDLIN"
                            Select Case oSegment.Fields(1)
                                Case "F"
                                    If oSegment.Fields.Count < 5 Then
                                        oItem.AddException(DTOEdiversaException.Cods.MissingSegmentFields, "segment IMDLIN.F (Descripció) amb menys de 4 camps")
                                    Else
                                        oItem.Dsc = oSegment.Fields(4)
                                    End If
                            End Select
                        Case "QTYLIN"
                            Select Case oSegment.Fields(1)
                                Case "21" 'Unidades pedidas del artículo
                                    oItem.Qty = oSegment.Fields(2)
                            End Select
                        Case "PRILIN"
                            Select Case oSegment.Fields(1)
                                Case "AAB" 'Preu brut abans de descomptes
                                    oItem.Preu = DTOEdiversaFile.ParseAmt(oSegment.Fields(2), oItem.Exceptions)
                                Case "AAA" 'Preu brut despres de descomptes pero abans de impostos (Amazon)
                                    oItem.PreuNet = DTOEdiversaFile.ParseAmt(oSegment.Fields(2), oItem.Exceptions)
                            End Select
                        Case "ALCLIN"
                            If (oSegment.Fields.Count > 5) Then
                                Select Case oSegment.Fields(1)
                                    Case "A" 'Descomptes
                                        oItem.Dto = oSegment.Fields(5)
                                End Select
                            End If
                    End Select
                End With
            Catch ex As Exception
                oOrder.AddException(DTOEdiversaException.Cods.NotSet, String.Format("BEBL.EdiversaOrder.LoadFromEdiversaFile. Error {0}", ex.Message))
            End Try
        Next


    End Sub



    Shared Function Validate(exs As List(Of Exception), ByRef oOrder As DTOEdiversaOrder) As Boolean
        'Dim oCustomer As New DTOCustomer(oOrder.Comprador.Guid)
        Dim oCustomer As DTOCustomer = oOrder.Customer
        BEBL.Customer.Load(oCustomer)
        Dim oCliProductDtos As List(Of DTOCliProductDto) = CliProductDtos.All(oCustomer)
        Dim oTarifa = CustomerTarifa.Load(oCustomer, oOrder.FchDoc)
        Dim retval As Boolean = EdiversaOrder.Validate(oOrder, oTarifa, oCliProductDtos)
        BEBL.EdiversaOrder.Update(oOrder, exs)
        Return retval
    End Function

    Shared Function Validate(ByRef oOrder As DTOEdiversaOrder, oTarifa As DTOCustomerTarifa.Compact, oCliProductDtos As List(Of DTOCliProductDto)) As Boolean
        Dim retval As Boolean
        oOrder.Exceptions = New List(Of DTOEdiversaException)
        If oOrder.Customer Is Nothing Then
            oOrder.AddException(DTOEdiversaException.Cods.ContactCompradorNotFound, "no s'ha pogut trobar el client")
        Else
            CustomerLoader.Load(oOrder.Customer)
            retval = ValidateHeader(oOrder)
            PreventDuplicates(oOrder)
            If oTarifa Is Nothing Then
                oOrder.AddException(DTOEdiversaException.Cods.MissingPrice, "no s'ha pogut treure tarifa per aquest client")
            Else
                For Each item As DTOEdiversaOrderItem In oOrder.Items
                    item.Parent = oOrder
                    If Not EdiversaOrderItem.Validate(item, oTarifa, oCliProductDtos) Then
                        retval = False
                    End If
                Next
            End If
        End If
        Return retval
    End Function

    Shared Function Duplicates(oEDiversaOrder As DTOEdiversaOrder) As List(Of DTOEdiversaOrder)
        Dim retval As List(Of DTOEdiversaOrder) = EdiversaOrderLoader.Duplicates(oEDiversaOrder)
        Return retval
    End Function

    Shared Function PreventDuplicates(ByRef oOrder As DTOEdiversaOrder) As Boolean
        Dim retval As Boolean
        Dim oDuplicates As List(Of DTOEdiversaOrder) = EdiversaOrder.Duplicates(oOrder)
        If oDuplicates.Count > 0 Then
            For Each item In oDuplicates
                If item.Result Is Nothing Then
                    If oOrder.GuionLessGuid > item.GuionLessGuid Then
                        oOrder.AddException(DTOEdiversaException.Cods.DuplicatedOrder, String.Format("Comanda duplicada amb la num.{0} del {1:dd/MM/yy} per processar", oOrder.DocNum, oOrder.FchDoc), DTOEdiversaException.TagCods.EdiversaOrder, oOrder)
                    End If
                Else
                    Dim sMsg = String.Format("Comanda ja entrada amb el num. {0} el {1:dd/MM/yy} a les {1:HH:mm}", item.Result.Num, item.Result.UsrLog.FchCreated)
                    oOrder.AddException(DTOEdiversaException.Cods.DuplicatedOrder, sMsg, DTOEdiversaException.TagCods.PurchaseOrder, item.Result)
                End If
            Next
        End If
        Return retval
    End Function

    Shared Function ValidateHeader(ByRef oOrder As DTOEdiversaOrder) As Boolean
        If oOrder.Comprador Is Nothing Then
            If oOrder.CompradorEAN Is Nothing Then
                oOrder.AddException(DTOEdiversaException.Cods.ContactCompradorNotFound, "Comprador no especificat")
            Else
                oOrder.AddException(DTOEdiversaException.Cods.ContactCompradorNotFound, String.Format("Comprador {0} no identificat", oOrder.CompradorEAN.Value))
            End If
        End If
        If oOrder.ReceptorMercancia Is Nothing Then
            Dim sMsg As String = ""
            If oOrder.ReceptorMercanciaEAN Is Nothing Then
                oOrder.AddException(DTOEdiversaException.Cods.ReceptorMercanciaNotFound, "Plataforma de entrega no especificada", DTOEdiversaException.TagCods.EdiversaOrder, oOrder)
            Else
                oOrder.AddException(DTOEdiversaException.Cods.ReceptorMercanciaNotFound, String.Format("Plataforma de entrega {0} no identificada", oOrder.ReceptorMercanciaEAN.Value), DTOEdiversaException.TagCods.EdiversaOrder, oOrder)
            End If
        ElseIf oOrder.Customer.isElCorteIngles Then
            Dim oDestinatari As DTOContact = oOrder.ReceptorMercancia
            Dim oAvailablePlatforms = ElCorteIngles.Platforms(oOrder.Customer)
            If Not oAvailablePlatforms.Any(Function(x) x.Equals(oDestinatari)) Then
                oOrder.AddException(DTOEdiversaException.Cods.PlatformNoValid, String.Format("Plataforma de entrega {0} no válida", oOrder.ReceptorMercanciaEAN.Value), DTOEdiversaException.TagCods.EdiversaOrder, oOrder)
            End If
        End If
        Return oOrder.Exceptions.Count = 0
    End Function


End Class

Public Class EDiversaOrders

    Shared Function All(Optional OnlyOpenFiles As Boolean = True) As List(Of DTOEdiversaOrder)
        Dim retval As List(Of DTOEdiversaOrder) = EDiversaOrdersLoader.All(OnlyOpenFiles)
        Return retval
    End Function

    Shared Function Headers(oEmp As DTOEmp, year As Integer) As List(Of DTOEdiversaOrder)
        Dim retval As List(Of DTOEdiversaOrder) = EDiversaOrdersLoader.Headers(oEmp, year)
        Return retval
    End Function


    Shared Function Validate(ByRef oOrders As List(Of DTOEdiversaOrder)) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim sb As New Text.StringBuilder
        Dim oCustomer As New DTOCustomer
        Dim oTarifa As DTOCustomerTarifa.Compact = Nothing
        Dim oCliProductDtos As List(Of DTOCliProductDto) = Nothing

        Try

            For Each oOrder As DTOEdiversaOrder In oOrders
                If oOrder.Customer Is Nothing And oCustomer IsNot Nothing Then
                    oCustomer = oOrder.Customer
                    If oCustomer IsNot Nothing Then
                        oTarifa = CustomerTarifa.Load(oCustomer)
                    End If
                    oCliProductDtos = New List(Of DTOCliProductDto)
                Else
                    If oOrder.Customer Is Nothing Then
                        If oCustomer IsNot Nothing Then
                            oCustomer = Nothing
                            oTarifa = CustomerTarifa.Load(Nothing)
                            oCliProductDtos = New List(Of DTOCliProductDto)
                        End If
                    ElseIf oOrder.Customer.UnEquals(oCustomer) Then
                        oCustomer = oOrder.Customer
                        BEBL.Customer.Load(oCustomer)
                        oTarifa = CustomerTarifa.Load(oCustomer)
                        oCliProductDtos = CliProductDtos.All(oCustomer)
                    End If
                End If
                EdiversaOrder.Validate(oOrder, oTarifa, oCliProductDtos)
                Dim oTaskResult = EdiversaOrderLoader.Update(oOrder)
                sb.AppendLine(oTaskResult.Msg)
            Next
            retval.Succeed("{0} comandes Edi processades" & vbCrLf & sb.ToString, oOrders.Count)
        Catch ex As Exception
            retval.Fail(ex, "BEBL.EdiversaOrders.Validate: Error al validar els missatges Edi de comanda")
        End Try

        Return retval
    End Function


    Shared Function Procesa(items As List(Of DTOEdiversaOrder), oUser As DTOUser, exs As List(Of DTOEdiversaException)) As Boolean
        Dim retval As Boolean = True
        For Each item As DTOEdiversaOrder In items
            If Not EdiversaOrder.Procesa(item, oUser, exs) Then retval = False
        Next
        Return retval
    End Function

    Shared Function Descarta(exs As List(Of Exception), oEDiversaOrders As List(Of DTOEdiversaOrder)) As Boolean
        Dim retval As Boolean
        If EDiversaOrdersLoader.Descarta(oEDiversaOrders, exs) Then
            For Each oOrder As DTOEdiversaOrder In oEDiversaOrders
                oOrder.EdiversaFile.Result = DTOEdiversaFile.Results.deleted
            Next
            retval = True
        End If
        Return retval
    End Function


    Shared Function ProcessAllValidated(oUser As DTOUser, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try

            Dim iCount As Integer
            'Dim oEanSkus As List(Of DTOProductSku) = BEBL.ProductSkus.AllWithEan()
            Dim AllOrders As List(Of DTOEdiversaOrder) = EDiversaOrdersLoader.All(True)
            EDiversaOrders.Validate(AllOrders)
            Dim oValidatedOrders = AllOrders.Where(Function(x) x.Exceptions.Count = 0 And x.Items.All(Function(y) y.Exceptions.Count = 0)).ToList
            Dim eExs As New List(Of DTOEdiversaException)
            For Each oOrder As DTOEdiversaOrder In oValidatedOrders
                If oOrder.Exceptions.Count = 0 Then
                    If Not oOrder.Items.Exists(Function(x) x.Exceptions.Count > 0) Then
                        EdiversaOrder.Procesa(oOrder, oUser, eExs)
                        iCount += 1
                    End If
                End If
            Next

            If eExs.Count = 0 Then
                retval = True
            Else
                exs.AddRange(DTOEdiversaException.ToSystemExceptions(eExs))
            End If
            'retval.Msg += "entrades " & iCount & " comandes validades de " & AllOrders.Count & " missatges EDI" & vbCrLf
        Catch ex As Exception
            exs.Add(New Exception("BEBL.EdiversaOrders.ProcessAllValidated: Error al intentar validar els missatges Edi"))
        End Try

        Return retval
    End Function

    Shared Function ConfirmationPending() As List(Of DTOEdiversaOrder)
        Return EDiversaOrdersLoader.ConfirmationPending
    End Function
End Class

Public Class EdiversaOrderItem

    Shared Function Find(oGuid As Guid) As DTOEdiversaOrderItem
        Dim retval As DTOEdiversaOrderItem = EdiversaOrderItemLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oEdiversaOrderItem As DTOEdiversaOrderItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaOrderItemLoader.Update(oEdiversaOrderItem, exs)
        Return retval
    End Function


    Shared Function Delete(oEdiversaOrderItem As DTOEdiversaOrderItem, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaOrderItemLoader.Delete(oEdiversaOrderItem, exs)
        Return retval
    End Function



    Shared Function Validate(ByRef item As DTOEdiversaOrderItem, oTarifa As DTOCustomerTarifa.Compact, oCliProductDtos As List(Of DTOCliProductDto)) As Boolean

        item.Exceptions = New List(Of DTOEdiversaException)
        If ValidateSku(item, oTarifa) Then
            ValidateQty(item, oTarifa)
            If item.SkipPreuValidationUser Is Nothing Then
                ValidatePrice(item, oTarifa)
            End If
            If item.SkipDtoValidationUser Is Nothing Then
                If item.Sku IsNot Nothing Then
                    ValidateDto(item, oCliProductDtos)
                End If
            End If
        End If
        Return item.Exceptions.Count = 0
    End Function

    Shared Function ValidateSku(ByRef item As DTOEdiversaOrderItem, oTarifa As DTOCustomerTarifa.Compact) As Boolean
        Dim retval As Boolean
        If item.Sku Is Nothing And item.RefProveidor > "" Then
            item.Sku = GetSkuFromRefProveidor(item.RefProveidor, oTarifa)
        End If

        If item.Sku Is Nothing Then
            Dim sEan = DTOEan.eanValue(item.Ean)
            If sEan = "0000000000000" Then
                item.AddException(DTOEdiversaException.Cods.SkuNotFound, "Codi EAN de producte buit", DTOEdiversaException.TagCods.EdiversaOrderItem, item)
            Else
                Dim oSku = oTarifa.Brands.SelectMany(Function(x) x.Categories).SelectMany(Function(y) y.Skus).FirstOrDefault(Function(z) z.Ean13 IsNot Nothing AndAlso z.Ean13.Value = sEan)
                If item.Sku Is Nothing Then
                    'no està al seu cataleg. Verifiquem si hi es al nostre
                    item.Sku = BEBL.ProductSku.FromEan(item.Ean)
                    If item.Sku Is Nothing Then
                        item.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("Producte {0} no identificat", item.Ean.Value), DTOEdiversaException.TagCods.EdiversaOrderItem, item)
                    Else
                        BEBL.ProductSku.Load(item.Sku)
                        If item.Sku.obsoleto Then
                            If item.Sku.FchObsoleto = DateTime.MinValue Then
                                item.AddException(DTOEdiversaException.Cods.SkuObsolet, String.Format("Producte {0} obsolet", item.Sku.RefYNomLlarg.Esp), DTOEdiversaException.TagCods.EdiversaOrderItem, item)
                            Else
                                item.AddException(DTOEdiversaException.Cods.SkuObsolet, String.Format("Producte {0} obsolet des del {1:dd/MM/yy}", item.Sku.RefYNomLlarg.Esp, item.Sku.FchObsoleto), DTOEdiversaException.TagCods.EdiversaOrderItem, item)
                            End If
                        Else
                            item.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("Article {0} fora del cataleg d'aquest client", item.Sku.RefYNomLlarg.Esp), DTOEdiversaException.TagCods.Sku, item.Sku)
                        End If
                    End If
                Else
                    item.Sku = oSku.ToProductSku()
                End If
            End If
        ElseIf item.Sku.obsoleto Then
            item.AddException(DTOEdiversaException.Cods.SkuObsolet, "Article obsolet", DTOEdiversaException.TagCods.Sku, item.Sku)
        ElseIf oTarifa.Missing(DTOProductSku.Treenode.Factory(item.Sku.Guid)) Then
            item.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("Article {0} fora del cataleg d'aquest client", item.Sku.RefYNomLlarg.Esp), DTOEdiversaException.TagCods.Sku, item.Sku)
        Else
            retval = True
        End If

        Return retval
    End Function

    Shared Function GetSkuFromRefProveidor(sRefProveidor As String, oTarifa As DTOCustomerTarifa.Compact) As DTOProductSku
        Dim oSku = oTarifa.Brands.
                        SelectMany(Function(x) x.Categories).
                        SelectMany(Function(y) y.Skus).
                        FirstOrDefault(Function(z) z.RefProveidor = sRefProveidor)
        Dim retval As DTOProductSku = Nothing
        If oSku IsNot Nothing Then retval = oSku.ToProductSku()
        Return retval
    End Function
    Shared Function GetSku(oSku As DTOProductSku, oTarifa As DTOCustomerTarifa) As DTOProductSku
        Dim retval = oTarifa.Brands.
                        SelectMany(Function(x) x.Categories).
                        SelectMany(Function(y) y.Skus).
                        FirstOrDefault(Function(z) z.Equals(oSku))
        Return retval
    End Function

    Shared Sub ValidateQty(ByRef item As DTOEdiversaOrderItem, oTarifa As DTOCustomerTarifa.Compact)

    End Sub

    Shared Sub ValidatePrice(ByRef item As DTOEdiversaOrderItem, oTarifa As DTOCustomerTarifa.Compact)
        If item.Sku IsNot Nothing Then
            Dim oSku = item.Sku
            Dim oTarifaSku = oTarifa.Brands.SelectMany(Function(x) x.Categories).SelectMany(Function(y) y.Skus).FirstOrDefault(Function(z) z.Guid.Equals(oSku.Guid))
            If oTarifaSku Is Nothing Then
                If item.Preu Is Nothing Then
                    item.AddException(DTOEdiversaException.Cods.MissingPrice, "Article no disponible per aquest client. Demanat sense carrec.", DTOEdiversaException.TagCods.Sku, item.Sku)
                Else
                    If item.Preu.Eur = 0 Then
                        item.AddException(DTOEdiversaException.Cods.MissingPrice, "Article no disponible per aquest client. Demanat sense carrec.", DTOEdiversaException.TagCods.Sku, item.Sku)
                    Else
                        item.AddException(DTOEdiversaException.Cods.MissingPrice, String.Format("Article no disponible per aquest client. Demanat per {0}.", DTOAmt.CurFormatted(item.Preu)), DTOEdiversaException.TagCods.Sku, item.Sku)
                    End If
                End If
            Else
                If item.Preu Is Nothing Then
                    If oTarifaSku.Price IsNot Nothing Then
                        item.Preu = oTarifaSku.Price.ToAmt()
                    End If
                Else
                    If oTarifaSku.Price.Eur <> item.Preu.Eur Then
                        item.AddException(DTOEdiversaException.Cods.WrongPrice, String.Format("Preu no valid. Demanat per {0} quan surt en tarifa per {1}", DTOAmt.CurFormatted(item.Preu), DTOAmt.CurFormatted(oTarifaSku.Price)), DTOEdiversaException.TagCods.EdiversaOrderItem, item)
                    End If
                End If
            End If
        End If
    End Sub

    Shared Sub ValidateDto(ByRef item As DTOEdiversaOrderItem, oCliProductDtos As List(Of DTOCliProductDto))
        Dim DcDto As Decimal = PurchaseOrderItem.GetDiscount(item.Sku, item.Parent.Customer, oCliProductDtos)
        If item.Dto <> DcDto Then
            item.AddException(DTOEdiversaException.Cods.WrongDiscount, String.Format("Demanat amb descompte del {0}% en lloc del {1}%", item.Dto, DcDto), DTOEdiversaException.TagCods.EdiversaOrderItem, item)
        End If
    End Sub



End Class
