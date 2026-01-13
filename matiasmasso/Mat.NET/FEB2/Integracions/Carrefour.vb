Imports DTO.Integracions

Public Class Carrefour

    Shared Function LogisticItems(oDeliveries As List(Of DTODelivery), oCountries As List(Of DTOCountry)) As List(Of DTOCarrefourItem)
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOCarrefourItem)

        For Each oDelivery As DTODelivery In oDeliveries
            FEB2.Delivery.Load(oDelivery, exs)
            FEB2.Delivery.LoadCustSkuRefs(oDelivery, exs)
            DTODelivery.loadMadeIns(oDelivery, oCountries, exs)

            Dim idx As Integer = 0
            For Each line As DTODeliveryItem In oDelivery.Items
                idx += 1
                Dim QtyPerBulto As Integer = DTOProductSku.outerPackOrInherited(line.Sku)
                Dim Bultos As Integer = line.Qty / QtyPerBulto
                For i As Integer = 1 To Bultos
                    Dim item As New DTOCarrefourItem
                    With item
                        .Albaran = oDelivery.Formatted()
                        .Linea = idx
                        .SkuCustomRef = FEB2.Carrefour.Ref(line.Sku)
                        '.Implantation = "610087"
                        .MasterBarCode = FEB2.Carrefour.DUN14(line.Sku)
                        .Height = line.Sku.dimensionHOrInherited
                        .Width = line.Sku.dimensionWOrInherited
                        .Length = line.Sku.dimensionLOrInherited
                        .SkuColor = FEB2.Carrefour.Color(line.Sku)
                        .SkuDsc = FEB2.Carrefour.Dsc(line.Sku)
                        .UnitsPerInnerBox = DTOProductSku.innerPackOrInherited(line.Sku)
                        .UnitsPerMasterBox = QtyPerBulto
                        .Weight = DTOProductSku.weightKgOrInherited(line.Sku)
                        .MadeIn = FEB2.Carrefour.MadeIn(line.Sku, oCountries)
                    End With
                    retval.Add(item)
                Next
            Next
        Next

        Return retval
    End Function

    Shared Async Function ExcelStocks(oMgz As DTOMgz, exs As List(Of Exception)) As Task(Of ExcelHelper.Sheet)

        Dim sTitle As String = "M+O Stocks " & TextHelper.VbFormat(DateTime.Now, "yyyy.MM.dd.hh.mm.ss")
        Dim retval As New ExcelHelper.Sheet(sTitle, sTitle)
        With retval
            .AddColumn("EAN", ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Ref", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Descripción", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Color", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Stock", ExcelHelper.Sheet.NumberFormats.Integer)
        End With

        Dim oCarrefour As DTOCustomer = DTOCustomer.Wellknown(DTOCustomer.Wellknowns.carrefour)
        Dim items = Await Mgzs.CustomStocks(oCarrefour, oMgz, exs)

        For Each item In items
            Dim oRow = retval.AddRow
            oRow.AddCell(item.sku.Ean13)
            oRow.AddCell(item.ref)
            oRow.AddCell(item.dsc)
            oRow.AddCell(item.color)
            oRow.AddCell(item.sku.stockAvailable())
        Next

        Return retval
    End Function

    Shared Function DeliveryDoc(oDelivery As DTODelivery) As DTODoc
        Dim oOrder As New DTOPurchaseOrder
        Dim oSpv As New DTOSpv
        Dim itm As DTODeliveryItem = Nothing


        Dim oEstilo As DTODoc.Estilos = DTODoc.Estilos.albara

        'If mAmt Is Nothing Then mAmt = DTOAmt.Empty
        Dim sAdr As String = Nothing
        Dim oZip As DTOZip = Nothing

        Dim oDoc As New DTODoc(oEstilo, oDelivery.Customer.Lang, DTOApp.Current.Cur)
        With oDoc
            .valorat = oDelivery.Valorado
            .incoterm = oDelivery.Incoterm

            Dim exs As New List(Of Exception)
            FEB2.Delivery.LoadCustSkuRefs(oDelivery, exs)
            .displayTotalLogistic = True

            Dim oCcx As DTOCustomer = oDelivery.Customer.CcxOrMe
            .recarrecEquivalencia = oCcx.Req
            .dest.Add(oCcx.Nom)
            .dest.Add(String.Format("NIF: {0}", oCcx.PrimaryNifValue()))
            sAdr = oCcx.Address.Text
            oZip = oCcx.Address.Zip


            If Not String.IsNullOrEmpty(sAdr) Then
                For Each sl As String In sAdr.Split(vbCrLf)
                    If sl > "" Then .dest.Add(sl.Trim)
                Next
            End If

            If oZip IsNot Nothing Then
                .dest.Add(oZip.FullNom(oDelivery.Customer.Lang))
            End If

            .fch = oDelivery.Fch
            .num = oDelivery.Id
            '.Dto = oDelivery.DtoPct
            '.Dpp = oDelivery.DppPct

            If oDelivery.IvaExempt Then
                .ivaBaseQuotas = New List(Of DTOTaxBaseQuota)
            Else
                .ivaBaseQuotas = oDelivery.taxBaseQuotas(DTOApp.Current.Taxes)
            End If

            Dim s As String = .lang.Tradueix("forma de envío: ", "enviament: ", "shipment: ")
            Select Case oDelivery.PortsCod
                Case DTOCustomer.PortsCodes.reculliran
                    .obs.Add(s & .lang.Tradueix("recogerán en ", "recullirán en ", "to be collected on "))
                    Dim oMgz As DTOMgz = oDelivery.Mgz
                    .obs.Add(oMgz.nomComercialOrDefault())
                    .obs.Add(oMgz.Address.Text)
                    .obs.Add(oMgz.Address.Zip.FullNom(oDelivery.Customer.Lang))
                Case DTOCustomer.PortsCodes.deguts
                    .obs.Add(s & .lang.Tradueix("portes debidos", "ports deguts", "portes debidos"))
                Case DTOCustomer.PortsCodes.pagats
                    .obs.Add(s & .lang.Tradueix("portes pagados", "ports pagats", "paid ports"))
                    If oDelivery.Platform IsNot Nothing Then
                        .obs.Add("entrega en " & oDelivery.Platform.Nom)
                        .obs.Add(oDelivery.Platform.Address.Text)
                        .obs.Add(oDelivery.Platform.Address.Zip.FullNom(oDelivery.Customer.Lang))
                    End If
            End Select

            If oDelivery.Items IsNot Nothing Then
                For Each itm In oDelivery.Items

                    Select Case oDelivery.Cod
                        Case DTOPurchaseOrder.Codis.reparacio
                            If Not oSpv.Equals(itm.Spv) Then
                                Dim i As Integer
                                oSpv = itm.Spv
                                Dim oSpvTextArray As List(Of String) = oSpv.lines(oDelivery.Customer.Lang) ' oSpv.TextArray
                                Dim iSpvTextLines As Integer = oSpvTextArray.Count
                                .itms.Add(New DTODocItm(, , , , , , , iSpvTextLines))
                                For i = 0 To iSpvTextLines - 1
                                    .itms.Add(New DTODocItm(oSpvTextArray(i), DTODoc.FontStyles.italic, , , , , , iSpvTextLines - i + 2))
                                Next
                            End If
                        Case DTOPurchaseOrder.Codis.client
                            If itm.PurchaseOrderItem.PurchaseOrder.UnEquals(oOrder) Then
                                oOrder = itm.PurchaseOrderItem.PurchaseOrder
                                .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=6))
                                .itms.Add(New DTODocItm(FEB2.Delivery.DocPdcText(oDelivery, oOrder), DTODoc.FontStyles.bold, IntMinLinesBeforeEndPage:=5))
                                .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=4))
                            End If
                    End Select

                    Dim oSku As DTOProductSku = itm.Sku
                    Dim sRef As String = oSku.Id.ToString
                    Dim sSkuNom As String = oSku.NomLlarg.Esp
                    If oDelivery.Cod = DTOPurchaseOrder.Codis.client AndAlso oDelivery.Customer.Lang.Equals(DTOLang.ENG) Then
                        sSkuNom = oSku.refYNomPrv()
                    End If

                    If (oDelivery.Valorado) Then
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.Qty, itm.Price, itm.Dto, 0, 2, , sRef))
                    Else
                        .itms.Add(New DTODocItm(sSkuNom, DTODoc.FontStyles.regular, itm.Qty, , , 0, 2, , sRef))
                    End If

                    Dim BoxUnits As Integer = oSku.innerPackOrInherited()
                    Dim Boxes As Integer = itm.Qty / BoxUnits
                    Dim oDocitm As DTODocItm = .itms.Last
                    With oDocitm
                        .Boxes = Boxes
                        .m3 = itm.Qty * oSku.volumeM3OrInherited()
                        .Kg = itm.Qty * oSku.weightKgOrInherited
                    End With
                    .itms.Add(New DTODocItm(String.Format("Dun 14: {0}", DTOCustomerProduct.Dun14OrDefault(oSku.CustomerProduct)), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("unidades/caja: {0}", BoxUnits), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("total cajas: {0}", Boxes), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("volumen caja: {0} m3", oSku.volumeM3OrInherited), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(String.Format("peso bruto caja: {0} Kg", oSku.weightKgOrInherited), IntLeftPadChars:=8))
                    .itms.Add(New DTODocItm(IntMinLinesBeforeEndPage:=2))
                Next

            End If
        End With
        Return oDoc
    End Function

    Shared Function LogisticItems(oDeliveries As List(Of DTODelivery)) As List(Of DTOCarrefourItem)
        Dim exs As New List(Of Exception)
        Dim retval As New List(Of DTOCarrefourItem)
        Dim oCountries As List(Of DTOCountry) = FEB2.Countries.AllSync(DTOLang.ESP, exs)

        For Each oDelivery As DTODelivery In oDeliveries
            FEB2.Delivery.LoadCustSkuRefs(oDelivery, exs)
            FEB2.Delivery.LoadMadeIns(oDelivery, oCountries, exs)

            Dim idx As Integer = 0
            For Each line As DTODeliveryItem In oDelivery.Items
                idx += 1
                Dim QtyPerBulto As Integer = line.Sku.outerPackOrInherited
                Dim Bultos As Integer = line.Qty / QtyPerBulto
                For i As Integer = 1 To Bultos
                    Dim item As New DTOCarrefourItem
                    With item
                        .Albaran = oDelivery.Formatted()
                        .Linea = idx
                        .SkuCustomRef = Ref(line.Sku)
                        '.Implantation = "610087"
                        .MasterBarCode = DUN14(line.Sku)
                        .Height = line.Sku.DimensionH()
                        .Width = line.Sku.DimensionW()
                        .Length = line.Sku.DimensionL()
                        .SkuColor = FEB2.Carrefour.Color(line.Sku)
                        .SkuDsc = FEB2.Carrefour.Dsc(line.Sku)
                        .UnitsPerInnerBox = line.Sku.innerPackOrInherited
                        .UnitsPerMasterBox = QtyPerBulto
                        .Weight = line.Sku.weightKgOrInherited
                        .MadeIn = FEB2.Carrefour.MadeIn(line.Sku, oCountries)
                    End With
                    retval.Add(item)
                Next
            Next
        Next


        Return retval
    End Function

    Shared Function Ref(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.ref
        End If
        Return retval
    End Function

    Shared Function DUN14(oSku As DTOProductSku) As String
        Dim retval As String = ""
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.DUN14
        End If
        Return retval
    End Function

    Shared Function Dsc(oSku As DTOProductSku) As String
        Dim retval As String = oSku.NomLlarg.Esp
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.dsc
        End If
        Return retval
    End Function
    Shared Function Color(oSku As DTOProductSku) As String
        Dim retval As String = oSku.Nom.Esp
        If oSku.CustomerProduct IsNot Nothing Then
            retval = oSku.CustomerProduct.color
        End If
        Return retval
    End Function

    Shared Function MadeIn(oSku As DTOProductSku, oCountries As List(Of DTOCountry)) As String
        Dim retval As String = ""
        Dim oCountry As DTOCountry = oSku.MadeIn
        If oCountry IsNot Nothing Then
            oCountry = oCountries.FirstOrDefault(Function(x) x.Equals(oCountry))
            If oCountry IsNot Nothing Then
                retval = oCountry.LangNom.Tradueix(DTOLang.ENG)
            End If
        End If
        Return retval
    End Function

End Class
