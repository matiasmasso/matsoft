Public Class DTOCustomerTarifa
    Property fch As Date
    Property customer As DTOCustomer
    Property costEnabled As Boolean
    Property brands As List(Of DTOProductBrand)

    Public Sub New()
        MyBase.New
        _brands = New List(Of DTOProductBrand)
    End Sub

    Public Function Skus() As List(Of DTOProductSku)
        Dim retval = _brands.SelectMany(Function(x) x.categories).SelectMany(Function(y) y.skus).ToList
        Return retval
    End Function

    Public Function FindSku(oSku As DTOProductSku) As DTOProductSku
        Dim retval = Skus.FirstOrDefault(Function(z) z.Guid.Equals(oSku.Guid))
        Return retval
    End Function

    Public Function FindByEan(sEan As String) As DTOProductSku
        Dim retval = Skus.FirstOrDefault(Function(z) z.ean13 IsNot Nothing AndAlso z.ean13.value = sEan)
        Return retval
    End Function

    Public Function Missing(oSku As DTOProductSku) As Boolean
        Return Me.FindSku(oSku) Is Nothing
    End Function

    Public Function GetSkuFromEan(sEan As String) As DTOProductSku
        Dim retval = Skus.FirstOrDefault(Function(z) z.ean13 IsNot Nothing AndAlso z.ean13.value = sEan)
        Return retval
    End Function

    Public Function GetSkuFromRefProveidor(refProveidor As String) As DTOProductSku
        Dim retval = Skus.FirstOrDefault(Function(z) z.refProveidor = refProveidor)
        Return retval
    End Function

    Public Function Trimmed() As DTOCustomerTarifa
        Dim retval = Me
        For Each oBrand In retval.brands
            For Each oCategory In oBrand.categories
                oCategory.Brand = Nothing
                For Each oSku In oCategory.skus
                    oSku.category = Nothing
                Next
            Next
        Next
        Return retval
    End Function

    Public Function Excel(oLang As DTOLang) As ExcelHelper.Sheet
        Dim retval As New ExcelHelper.Sheet
        Dim DtoVisible As Boolean = Me.Skus.Exists(Function(x) x.customerDto <> 0)

        With retval
            .AddColumn("Ref.M+O")
            .AddColumn("Ref.Custom")
            .AddColumn(oLang.tradueix("Ref.fabricante", "Ref.fabricant", "Ref.manufacturer"))
            .AddColumn("EAN producto")
            .AddColumn("EAN packaging")
            .AddColumn(oLang.tradueix("Marca", "Marca", "Brand"), ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn(oLang.tradueix("Categoría", "Categoria", "Category"), ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn(oLang.tradueix("Producto", "Producte", "Product"), ExcelHelper.Sheet.NumberFormats.W50)
            If Me.costEnabled Then
                .AddColumn(oLang.tradueix("Coste", "Cost", "Cost"), ExcelHelper.Sheet.NumberFormats.Euro)
                If DtoVisible Then
                    .AddColumn(oLang.tradueix("Dto", "Dte", "Discount"), ExcelHelper.Sheet.NumberFormats.Percent)
                End If
            End If
            .AddColumn(oLang.tradueix("Venta", "Venda", "Retail"), ExcelHelper.Sheet.NumberFormats.Euro)
            .AddColumn(oLang.tradueix("Pedido mín.", "Comanda min.", "Moq"), ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn(oLang.tradueix("Largo", "Llarg", "Length"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.tradueix("Ancho", "Ample", "Width"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.tradueix("Alto", "Alt", "Height"), ExcelHelper.Sheet.NumberFormats.mm)
            .AddColumn(oLang.tradueix("Peso", "Pes", "Weight"), ExcelHelper.Sheet.NumberFormats.Kg)
            .AddColumn("Made In", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Codigo arancelario", ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn(oLang.tradueix("Imagen", "Imatge", "Image"), ExcelHelper.Sheet.NumberFormats.PlainText)
        End With

        For Each oBrand As DTOProductBrand In Me.brands
            For Each oCategory As DTOProductCategory In oBrand.categories
                For Each oSku As DTOProductSku In oCategory.skus
                    Dim oRow As ExcelHelper.Row = retval.AddRow
                    oRow.AddCell(oSku.id)
                    oRow.AddCell(oSku.refCustomer)
                    oRow.addCell(oSku.refProveidor)
                    oRow.AddCellEan(oSku.ean13)
                    If oSku.packageEan Is Nothing Then
                        oRow.AddCell()
                    Else
                        oRow.AddCell(oSku.packageEan.value)
                    End If
                    oRow.AddCell(oBrand.nom)
                    oRow.AddCell(oCategory.nom)
                    oRow.addCell(oSku.nomCurt)

                    If Me.costEnabled Then
                        oRow.AddCellAmt(oSku.price)
                        If DtoVisible Then
                            oRow.AddCell(oSku.customerDto)
                        End If
                    End If
                    oRow.AddCellAmt(oSku.rrpp)
                    oRow.AddCell(DTOProductSku.Moq(oSku))

                    oRow.AddCell(oSku.DimensionLOrInherited)
                    oRow.AddCell(oSku.DimensionWOrInherited)
                    oRow.AddCell(oSku.DimensionHOrInherited)
                    oRow.AddCell(oSku.WeightKgOrInherited)
                    oRow.AddCell(oSku.MadeInOrInheritedISO())
                    oRow.AddCell(oSku.CodiMercanciaIdOrInherited)

                    If oSku.imageExists Then
                        Dim url As String = oSku.ImageUrl(True)
                        oRow.AddCell(url, url)
                    Else
                        oRow.AddCell()
                    End If
                Next
            Next
        Next
        Return retval
    End Function

End Class

