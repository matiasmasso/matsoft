Public Class DTOPriceListSupplier
    Inherits DTOBaseGuid

    Property Proveidor As DTOProveidor
    Property Fch As Date
    Property Concepte As String
    Property Cur As DTOCur
    Property Discount_OnInvoice As Decimal
    Property Discount_OffInvoice As Decimal
    Property DocFile As DTODocFile
    Property Items As List(Of DTOPriceListItem_Supplier)

    Public Sub New()
        MyBase.New()
        _Items = New List(Of DTOPriceListItem_Supplier)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Shadows Function Factory(oProveidor As DTOProveidor) As DTOPriceListSupplier
        Dim retval As New DTOPriceListSupplier
        With retval
            .Proveidor = oProveidor
            .Fch = DateTime.Today
            .Cur = .Proveidor.Cur
        End With
        Return retval
    End Function

    Shared Function GenerateCustomerPriceList(oPriceList As DTOPriceListSupplier) As DTOPricelistCustomer
        Dim oCommercialMargin As DTOCommercialMargin = DTOProveidor.GetCommercialMargin(oPriceList.Proveidor)

        'Load(oPriceList)
        Dim retval As New DTOPricelistCustomer()
        With retval
            .Fch = DateTime.Today.AddDays(1)
            .Concepte = "(nova tarifa de " & oPriceList.Proveidor.FullNom & ")"
            .Cur = DTOCur.Eur
            For Each oSupplierItem As DTOPriceListItem_Supplier In oPriceList.Items
                Dim oCustomerItem As DTOPricelistItemCustomer = DTOPriceListSupplier.GetSalePrice(oSupplierItem, retval, oCommercialMargin)
                If oCustomerItem IsNot Nothing Then
                    If .Items.Find(Function(x) x.Sku.RefProveidor = oSupplierItem.Sku.RefProveidor) Is Nothing Then
                        .Items.Add(oCustomerItem)
                    End If
                End If
            Next
        End With

        Return retval
    End Function

    Shared Function GetSalePrice(oItem As DTOPriceListItem_Supplier, oParent As DTOPricelistCustomer, oCommercialMargin As DTOCommercialMargin, Optional DcFixTarifaAFromSupplier As Decimal = 0) As DTOPricelistItemCustomer
        Dim retval As DTOPricelistItemCustomer = Nothing
        oCommercialMargin.CostNet = oItem.CostNet()
        Dim oSku As DTOProductSku = oItem.Sku ' Art.FromProveidor(New Proveidor(oItem.Parent.Proveidor.Guid), oItem.Ref)
        If oSku IsNot Nothing Then
            retval = New DTOPricelistItemCustomer(oParent)
            With retval
                .Sku = oSku
                '.Nom = (New Product(oArt)).Nom(DTOLang.Factory("ESP"))


                If oItem.Retail = 0 Then
                    .Retail = DTOAmt.Factory(oCommercialMargin.GetRetail(DcFixTarifaAFromSupplier))
                Else
                    .Retail = DTOAmt.Factory(oItem.Retail)
                End If
            End With
        End If
        Return retval
    End Function

    Shared Function Excel(oPriceList As DTOPriceListSupplier) As MatHelperStd.ExcelHelper.Sheet
        Dim sCaption As String = String.Format("tarifa {0} {1:yyyy.MM.dd}", oPriceList.Proveidor.NomComercialOrDefault(), oPriceList.Fch)
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sCaption, sCaption)
        With retval
            .AddColumn("ref.M+O", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("ref.proveidor", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("codi EAN", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("descripció", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("cost brut", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("cost net", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("RRPP", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
        End With

        For Each Itm As DTOPriceListItem_Supplier In oPriceList.Items
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            If Itm.Sku Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(Itm.Sku.id)
            End If
            oRow.AddCell(Itm.Ref)
            oRow.AddCell(Itm.EAN)
            oRow.AddCell(Itm.Description)
            oRow.AddCell(Itm.Price)
            oRow.AddCell(Itm.CostNet())
            oRow.AddCell(Itm.Retail)
        Next
        Return retval
    End Function

    Shared Function ExcelTarifaVigent(items As List(Of DTOPriceListItem_Supplier)) As MatHelperStd.ExcelHelper.Sheet
        Dim sCaption As String = String.Format("tarifa vigent de cost {0:yyyy.MM.dd}", DateTime.Now)
        Dim retval As New MatHelperStd.ExcelHelper.Sheet(sCaption, sCaption)
        With retval
            .AddColumn("ref.M+O", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("ref.proveidor", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("codi EAN", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("descripció", MatHelperStd.ExcelHelper.Sheet.NumberFormats.PlainText)
            .AddColumn("cost brut", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("cost net", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("RRPP", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn("tarifa", MatHelperStd.ExcelHelper.Sheet.NumberFormats.DDMMYY)
        End With

        For Each Itm As DTOPriceListItem_Supplier In items
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            If Itm.Sku Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(Itm.Sku.id)
            End If
            oRow.AddCell(Itm.Ref)
            oRow.AddCell(Itm.EAN)
            oRow.AddCell(Itm.Description)
            oRow.AddCell(Itm.Price)
            oRow.AddCell(Itm.CostNet())
            oRow.AddCell(Itm.Retail)
            If Itm.Parent.DocFile Is Nothing Then
                oRow.AddCell(Itm.Parent.Fch)
            Else
                oRow.AddCell(Itm.Parent.Fch, Itm.Parent.DocFile.DownloadUrl(True))
            End If
        Next
        Return retval
    End Function

End Class


Public Class DTOPriceListItem_Supplier
    Property Parent As DTOPriceListSupplier
    Property Ref As String
    Property EAN As String
    Property Description As String
    Property Price As Decimal
    'Property TarifaA As Decimal
    Property Retail As Decimal
    Property SkuGuid As Guid
    Property Sku As DTOProductSku
    Property InnerPack As Integer
    Property Lin As Integer

    Property IsNew As Boolean
    Property IsLoaded As Boolean

    Shared Function Clon(oSrcItem As DTOPriceListItem_Supplier) As DTOPriceListItem_Supplier
        Dim retval As New DTOPriceListItem_Supplier
        With retval
            .Parent = oSrcItem.Parent
            .Ref = oSrcItem.Ref
            .EAN = oSrcItem.EAN
            .Description = oSrcItem.Description
            .Price = oSrcItem.Price
            .Sku = oSrcItem.Sku
            .InnerPack = oSrcItem.InnerPack
        End With
        Return retval
    End Function

    Public Function CostNet() As Decimal
        Dim retval As Decimal
        Dim DcCostBrut As Decimal = _Price
        retval = Math.Round(DcCostBrut * (100 - _Parent.Discount_OnInvoice - _Parent.Discount_OffInvoice) / 100, DTOCur.Eur.Decimals, MidpointRounding.AwayFromZero)
        Return retval
    End Function

    Shared Function CostNet(oPriceItem As DTOPriceListItem_Supplier) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oPriceItem IsNot Nothing Then
            retval = DTOAmt.Factory(oPriceItem.Price)
            If oPriceItem.Parent.Discount_OnInvoice <> 0 Then
                retval.DeductPercent(oPriceItem.Parent.Discount_OnInvoice)
            End If
        End If
        Return retval
    End Function

    Shared Function Ref_And_Description(oItem As DTOPriceListItem_Supplier) As String
        Dim sb As New System.Text.StringBuilder
        If oItem.Ref > "" Then
            sb.Append(oItem.Ref)
            If oItem.Description > "" Then
                sb.Append(" ")
            End If
        End If
        sb.Append(oItem.Description)
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class