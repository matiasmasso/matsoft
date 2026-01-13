Public Class EdiversaSalesReport


#Region "CRUD"
    Shared Function Find(oGuid As Guid) As DTOEdiversaSalesReport
        Dim retval As DTOEdiversaSalesReport = EdiversaSalesReportLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oEdiversaSalesReport As DTOEdiversaSalesReport) As Boolean
        Dim retval As Boolean = EdiversaSalesReportLoader.Load(oEdiversaSalesReport)
        Return retval
    End Function

    Shared Function Update(oEdiversaSalesReport As DTOEdiversaSalesReport, Optional oEdiFile As DTOEdiversaFile = Nothing, Optional ByRef exs As List(Of Exception) = Nothing) As Boolean
        Dim retval As Boolean = EdiversaSalesReportLoader.Update(oEdiversaSalesReport, oEdiFile, exs)
        Return retval
    End Function

    Shared Function Delete(oEdiversaSalesReport As DTOEdiversaSalesReport, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = EdiversaSalesReportLoader.Delete(oEdiversaSalesReport, exs)
        Return retval
    End Function
#End Region

    Shared Function SalesReport(exs As List(Of Exception), ByRef value As DTOSalesReport) As Boolean
        Return EdiversaSalesReportItemsLoader.SalesReport(exs, value)
    End Function


    Shared Function FromEdiFile(oEanSkus As List(Of DTOProductSku), oEdiversaFile As DTOEdiversaFile) As DTOEdiversaSalesReport
        Dim retval As DTOEdiversaSalesReport = Nothing
        If oEdiversaFile IsNot Nothing Then
            retval = New DTOEdiversaSalesReport
            retval.Cur = DTOCur.Eur

            Dim oLocation As New DTOEdiversaSalesReport.Item
            Dim item As DTOEdiversaSalesReport.Item = Nothing
            oEdiversaFile.LoadSegments()
            For Each oSegment As DTOEdiversaSegment In oEdiversaFile.Segments
                Select Case oSegment.Fields.First
                    Case "BGM"
                        retval.Id = oSegment.ParseString(1, retval.Exceptions)
                    Case "DTM"
                        retval.Fch = oSegment.ParseFch(1, retval.Exceptions)
                    Case "NADSE"
                        Dim oEan As DTOEan = oSegment.ParseEan(1, retval.Exceptions)
                        retval.Customer = CustomerLoader.FromGln(oEan)
                    Case "CUX"
                        Dim sTag As String = oSegment.ParseString(1, retval.Exceptions)
                        retval.Cur = DTOCur.Factory(sTag)
                    Case "LOC"
                        oLocation = New DTOEdiversaSalesReport.Item
                        With oLocation
                            Dim oEan As DTOEan = oSegment.ParseEan(1, retval.Exceptions)
                            .customer = CustomerLoader.FromGln(oEan)
                            If .customer Is Nothing Then
                                Dim sb2 As New System.Text.StringBuilder
                                sb2.Append("Centre desconegut")
                                sb2.Append(" " & oSegment.ParseString(1, retval.Exceptions))
                                sb2.Append(" " & oSegment.ParseString(3, retval.Exceptions))
                                retval.Exceptions.Add(DTOEdiversaException.Factory(DTOEdiversaException.Cods.ContactCompradorNotFound, oEdiversaFile, sb2.ToString()))
                            End If
                            .dept = oSegment.ParseString(2, retval.Exceptions)
                            If oSegment.Fields.Count > 4 Then
                                .centro = oSegment.ParseString(4, retval.Exceptions)
                            End If
                        End With
                    Case "DTMLOC"
                        oLocation.fch = oSegment.ParseFch(1, retval.Exceptions)
                    Case "LIN"
                        item = New DTOEdiversaSalesReport.Item
                        retval.Items.Add(item)
                        With item
                            Dim oEan As DTOEan = oSegment.ParseEan(1, retval.Exceptions)
                            item.sku = oEanSkus.FirstOrDefault(Function(x) x.ean13.value = oEan.Value)
                            item.customer = oLocation.customer
                            item.centro = oLocation.centro
                            item.dept = oLocation.dept
                            item.fch = oLocation.fch
                        End With
                    Case "QTYLIN"
                        Dim sQualifier As String = oSegment.ParseString(1, retval.Exceptions)
                        Select Case sQualifier
                            Case "153"
                                item.Qty = oSegment.ParseInteger(2, retval.Exceptions)
                            Case "77E"
                                item.QtyBack = oSegment.ParseInteger(2, retval.Exceptions)
                        End Select
                    Case "MOALIN"
                        item.Eur = oSegment.ParseDecimal(1, retval.Exceptions)
                    Case "PIALIN"
                        Dim sQualifier As String = oSegment.ParseString(1, retval.Exceptions)
                        Select Case sQualifier
                            Case "IN"
                                If item.Sku Is Nothing Then
                                    Dim src As String = oSegment.ParseString(2, retval.Exceptions).Trim
                                    Dim oSkus As List(Of DTOProductSku) = ProductSkusLoader.FromRefProveidor(New DTOEmp(DTOEmp.Ids.MatiasMasso), src)
                                    If oSkus.Count = 1 Then
                                        item.Sku = oSkus.First
                                    End If
                                End If
                            Case "SA"
                                If item.Sku Is Nothing Then
                                    Dim src As String = oSegment.ParseString(2, retval.Exceptions).Trim
                                    retval.AddException(DTOEdiversaException.Cods.SkuNotFound, String.Format("producte no trobat: {0}", src))
                                End If
                        End Select
                End Select
            Next
        End If
        Return retval
    End Function


    Shared Function Items(oStat As DTOStat) As List(Of DTOStatItem)
        Dim oItems = EdiversaSalesReportItemsLoader.All(oStat)
        Dim SortedItems As List(Of DTOEdiversaSalesReport.Item) = Nothing

        Select Case oStat.ConceptType ' PASSAR BEBL.EdiversaSalesReport.Items(oStat as DTOStat) a FEBL per minimitzar el volum de dades transmeses
            Case DTOStat.ConceptTypes.Product
                SortedItems = oItems.
                    OrderBy(Function(x) x.Fch).
                    OrderBy(Function(x) x.Customer.Nom).
                    OrderBy(Function(x) x.Sku.Nom).
                    OrderBy(Function(x) x.Sku.Category.Nom).
                    OrderBy(Function(x) x.Sku.Category.Brand.Nom).
                    ToList
            Case DTOStat.ConceptTypes.Geo
                SortedItems = oItems.
                    OrderBy(Function(x) x.Fch).
                    OrderBy(Function(x) x.Sku.Nom).
                    OrderBy(Function(x) x.Sku.Category.Nom).
                    OrderBy(Function(x) x.Sku.Category.Brand.Nom).
                    OrderBy(Function(x) x.Customer.Nom).
                    ToList
            Case DTOStat.ConceptTypes.Yeas
        End Select


        Dim oSalePoint As New DTOCustomer
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim oSku As New DTOProductSku

        Dim oSalePointItem As DTOStatItem = Nothing
        Dim oBrandItem As DTOStatItem = Nothing
        Dim oCategoryItem As DTOStatItem = Nothing
        Dim oSkuItem As DTOStatItem = Nothing

        Dim retval As New List(Of DTOStatItem)

        Dim index As Integer = 1
        For Each item As DTOEdiversaSalesReport.Item In SortedItems

            If oStat.ConceptType = DTOStat.ConceptTypes.Geo And Not item.customer.Equals(oSalePoint) Then
                oSalePoint = item.customer
                oSalePointItem = New DTOStatItem(oStat, oSalePoint.Guid, oSalePoint.nom)
                With oSalePointItem
                    .Level = 0
                    .Index = index
                    .ParentIndex = 0
                    .HasChildren = True
                End With
                retval.Add(oSalePointItem)
                index += 1
            End If

            If Not item.sku.category.Brand.Equals(oBrand) Then
                oBrand = item.sku.category.brand
                oBrandItem = New DTOStatItem(oStat, oBrand.Guid, oBrand.nom.Esp)
                With oBrandItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 1, 0)
                    .Index = index
                    .ParentIndex = 0
                    If oStat.ConceptType = DTOStat.ConceptTypes.Geo Then
                        .ParentIndex = oSalePointItem.Index
                    End If
                    .HasChildren = True
                End With
                retval.Add(oBrandItem)
                index += 1
            End If

            If Not item.sku.category.Equals(oCategory) Then
                oCategory = item.sku.category
                oCategoryItem = New DTOStatItem(oStat, oCategory.Guid, oCategory.nom.Esp)
                With oCategoryItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 2, 1)
                    .Index = index
                    .ParentIndex = oBrandItem.Index
                    .HasChildren = True
                End With
                retval.Add(oCategoryItem)
                index += 1
            End If

            If Not item.sku.Equals(oSku) Then
                oSku = item.sku
                oSkuItem = New DTOStatItem(oStat, oSku.Guid, oSku.nom.Esp)
                With oSkuItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 3, 2)
                    .Index = index
                    .ParentIndex = oCategoryItem.Index
                    .HasChildren = False
                End With
                retval.Add(oSkuItem)
                index += 1
            End If

            If oStat.ConceptType <> DTOStat.ConceptTypes.Geo And Not item.customer.Equals(oSalePoint) Then
                oSalePoint = item.customer
                oSalePointItem = New DTOStatItem(oStat, oSalePoint.Guid, oSalePoint.nom)
                With oSalePointItem
                    .Level = 3
                    .Index = index
                    .ParentIndex = oSkuItem.Index
                    .HasChildren = True
                End With
                retval.Add(oSalePointItem)
                index += 1
            End If

            Dim DcValue As Decimal = IIf(oStat.Format = DTOStat.Formats.Units, item.qty - item.qtyBack, item.retail * (item.qty - item.qtyBack))
            Dim iMonth As Integer = item.fch.Month
            oBrandItem.Values(iMonth - 1) += DcValue
            oCategoryItem.Values(iMonth - 1) += DcValue
            oSkuItem.Values(iMonth - 1) += DcValue
            oSalePointItem.Values(iMonth - 1) += DcValue
        Next
        Return retval
    End Function

    Shared Function Procesa(oRawFile As DTOEdiversaFile, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean
        Try
            Dim oEanSkus As List(Of DTOProductSku) = BEBL.ProductSkus.AllWithEan()
            Dim oRpt As DTOEdiversaSalesReport = BEBL.EdiversaSalesReport.FromEdiFile(oEanSkus, oRawFile)
            With oRawFile
                .Result = DTOEdiversaFile.Results.Processed
                .ResultBaseGuid = oRpt
            End With

            retval = BEBL.EdiversaSalesReport.Update(oRpt, oRawFile, exs)

        Catch ex As Exception
            exs.Add(ex)
        End Try
        Return retval
    End Function

End Class

Public Class EdiversaSalesReports

    Shared Function RebuildAll(exs As List(Of Exception)) As Boolean
        Dim oFiles = EdiversaFilesLoader.All(DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004.ToString())
        Dim oEanSkus As List(Of DTOProductSku) = BEBL.ProductSkus.AllWithEan()
        Dim idx As Integer
        For Each oFile In oFiles
            Dim oReport = EdiversaSalesReport.FromEdiFile(oEanSkus, oFile)
            BEBL.EdiversaSalesReport.Update(oReport, oFile, exs)
            idx += 1
        Next
        Return exs.Count = 0
    End Function


    Shared Function Years(oEmp As DTOEmp) As List(Of Integer)
        Dim retval As List(Of Integer) = EdiversaSalesReportsLoader.Years(oEmp)
        Return retval
    End Function

    Shared Function All(oEmp As DTOEmp, year As Integer) As List(Of DTOEdiversaSalesReport)
        Return EdiversaSalesReportsLoader.all(oEmp, year)
    End Function

    Shared Function ProcessPendingMessages(oEanSkus As List(Of DTOProductSku), oUser As DTOUser) As DTOTaskResult
        Dim retval As New DTOTaskResult
        Dim successFiles As New List(Of DTOEdiversaFile)
        Dim failFiles As New List(Of DTOEdiversaFile)
        Dim oRawFiles As List(Of DTOEdiversaFile) = EdiversaFilesLoader.All(DTOEdiversaFile.Tags.SLSRPT_D_96A_UN_EAN004.ToString, True)
        Dim iCount As Integer
        For Each oRawFile In oRawFiles
            Dim ex2 As New List(Of Exception)
            Dim oRpt As DTOEdiversaSalesReport = EdiversaSalesReport.FromEdiFile(oEanSkus, oRawFile)
            If oRpt Is Nothing Then
                failFiles.Add(oRawFile)
                retval.AddException(New Exception("error al processar el missatge '" & oRawFile.Guid.ToString & "'"))
                retval.Exceptions.AddRange(ex2)
            Else
                iCount += 1

                With oRawFile
                    .Result = DTOEdiversaFile.Results.Processed
                    .ResultBaseGuid = oRpt
                End With

                If EdiversaSalesReport.Update(oRpt, oRawFile, ex2) Then
                    successFiles.Add(oRawFile)
                Else
                    failFiles.Add(oRawFile)
                End If
            End If
        Next

        If successFiles.Count = oRawFiles.Count Then
            retval.Msg = "processats " & iCount & " raports de vendes EDI" & vbCrLf
        Else
            retval.Msg = "retransmesos " & iCount & " raports de vendes de " & oRawFiles.Count & " missatges EDI" & vbCrLf
        End If
        Return retval
    End Function
End Class

Public Class EdiversaSalesReportItems
    Shared Function All(oStat As DTOStat) As List(Of DTOEdiversaSalesReport.Item)
        Dim retval As List(Of DTOEdiversaSalesReport.Item) = EdiversaSalesReportItemsLoader.All(oStat)
        Return retval
    End Function

    Shared Function Cataleg(iYear As Integer, Optional oCustomer As DTOCustomer = Nothing, Optional oProveidor As DTOProveidor = Nothing) As DTOProductCatalog
        Dim retval As DTOProductCatalog = EdiversaSalesReportItemsLoader.Cataleg(iYear, oCustomer, oProveidor)
        Return retval
    End Function

    Shared Function StatItems(iYear As Integer, oUser As DTOUser, Optional oProduct As DTOProduct = Nothing, Optional oCustomer As DTOCustomer = Nothing) As DTOStat
        Dim oProveidor As DTOProveidor = User.GetProveidor(oUser)
        Dim retval As DTOStat = EdiversaSalesReportItemsLoader.StatItems(iYear, oUser.Lang, oProduct, oCustomer, oProveidor)
        Return retval
    End Function

    Shared Function StatItems2(oUser As DTOUser, year As Integer, oHolding As DTOHolding) As DTOStat2
        Dim retval = EdiversaSalesReportItemsLoader.StatItems2(oUser, year, oHolding)
        Return retval
    End Function


    Shared Function Excel(oUser As DTOUser, year As Integer, oHolding As DTOHolding, units As DTOStat2.Units, oBrand As DTOBaseGuid, oCategory As DTOBaseGuid, oSku As DTOBaseGuid) As MatHelper.Excel.Sheet
        Dim retval = EdiversaSalesReportItemsLoader.Excel(oUser, year, oHolding, units, oBrand, oCategory, oSku)
        Return retval
    End Function


    Shared Sub LoadStat(ByRef oStat As DTOStat)
        Dim items As List(Of DTOEdiversaSalesReport.Item) = EdiversaSalesReportItems.All(oStat)
        Dim SortedItems As List(Of DTOEdiversaSalesReport.Item) = Nothing

        Select Case oStat.ConceptType
            Case DTOStat.ConceptTypes.Product
                SortedItems = items.
                    OrderBy(Function(x) x.Fch).
                    OrderBy(Function(x) x.Customer.Nom).
                    OrderBy(Function(x) x.Sku.Nom).
                    OrderBy(Function(x) x.Sku.Category.Nom).
                    OrderBy(Function(x) x.Sku.Category.Brand.Nom).
                    ToList
            Case DTOStat.ConceptTypes.Geo
                SortedItems = items.
                    OrderBy(Function(x) x.Fch).
                    OrderBy(Function(x) x.Sku.Nom).
                    OrderBy(Function(x) x.Sku.Category.Nom).
                    OrderBy(Function(x) x.Sku.Category.Brand.Nom).
                    OrderBy(Function(x) x.Customer.Nom).
                    ToList
            Case DTOStat.ConceptTypes.Yeas
        End Select


        Dim oSalePoint As New DTOCustomer
        Dim oBrand As New DTOProductBrand
        Dim oCategory As New DTOProductCategory
        Dim oSku As New DTOProductSku

        Dim oSalePointItem As DTOStatItem = Nothing
        Dim oBrandItem As DTOStatItem = Nothing
        Dim oCategoryItem As DTOStatItem = Nothing
        Dim oSkuItem As DTOStatItem = Nothing

        oStat.Items = New List(Of DTOStatItem)

        Dim index As Integer = 1
        For Each item As DTOEdiversaSalesReport.Item In SortedItems

            If oStat.ConceptType = DTOStat.ConceptTypes.Geo And Not item.customer.Equals(oSalePoint) Then
                oSalePoint = item.customer
                oSalePointItem = New DTOStatItem(oStat, oSalePoint.Guid, oSalePoint.nom)
                With oSalePointItem
                    .Level = 0
                    .Index = index
                    .ParentIndex = 0
                    .HasChildren = True
                End With
                oStat.Items.Add(oSalePointItem)
                index += 1
            End If

            If Not item.sku.category.Brand.Equals(oBrand) Then
                oBrand = item.sku.category.brand
                oBrandItem = New DTOStatItem(oStat, oBrand.Guid, oBrand.nom.Esp)
                With oBrandItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 1, 0)
                    .Index = index
                    .ParentIndex = 0
                    If oStat.ConceptType = DTOStat.ConceptTypes.Geo Then
                        .ParentIndex = oSalePointItem.Index
                    End If
                    .HasChildren = True
                End With
                oStat.Items.Add(oBrandItem)
                index += 1
            End If

            If Not item.sku.category.Equals(oCategory) Then
                oCategory = item.sku.category
                oCategoryItem = New DTOStatItem(oStat, oCategory.Guid, oCategory.nom.Esp)
                With oCategoryItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 2, 1)
                    .Index = index
                    .ParentIndex = oBrandItem.Index
                    .HasChildren = True
                End With
                oStat.Items.Add(oCategoryItem)
                index += 1
            End If

            If Not item.sku.Equals(oSku) Then
                oSku = item.sku
                oSkuItem = New DTOStatItem(oStat, oSku.Guid, oSku.nom.Esp)
                With oSkuItem
                    .Level = IIf(oStat.ConceptType = DTOStat.ConceptTypes.Geo, 3, 2)
                    .Index = index
                    .ParentIndex = oCategoryItem.Index
                    .HasChildren = False
                End With
                oStat.Items.Add(oSkuItem)
                index += 1
            End If

            If oStat.ConceptType <> DTOStat.ConceptTypes.Geo And Not item.customer.Equals(oSalePoint) Then
                oSalePoint = item.customer
                oSalePointItem = New DTOStatItem(oStat, oSalePoint.Guid, oSalePoint.nom)
                With oSalePointItem
                    .Level = 3
                    .Index = index
                    .ParentIndex = oSkuItem.Index
                    .HasChildren = True
                End With
                oStat.Items.Add(oSalePointItem)
                index += 1
            End If

            Dim DcValue As Decimal = IIf(oStat.Format = DTOStat.Formats.Units, item.qty - item.qtyBack, item.retail * (item.qty - item.qtyBack))
            Dim iMonth As Integer = item.fch.Month
            oBrandItem.Values(iMonth - 1) += DcValue
            oCategoryItem.Values(iMonth - 1) += DcValue
            oSkuItem.Values(iMonth - 1) += DcValue
            oSalePointItem.Values(iMonth - 1) += DcValue
        Next

    End Sub


End Class
