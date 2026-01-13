Public Class DTOProductCatalog
    Inherits DTOProduct

    Property Brands As New List(Of DTOProductBrand)
    Property Categories As New List(Of DTOProductCategory)
    Property Skus As New List(Of DTOProductSku)
    Property Bases As New List(Of DTOProduct)
    Property Cnaps As New List(Of DTOCnap)

    Shared Function RefsExcel(oSkus As List(Of DTOProductSku)) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Referencias M+O")
        With retval
            .AddColumn("Ref.M+O")
            .AddColumn("EAN 13")
            .AddColumn("Ref.Fabricante")
            .AddColumn("Marca Comercial")
            .AddColumn("Categoría")
            .AddColumn("Producto")
        End With
        For Each oSku In oSkus
            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow()
            oRow.AddCell(oSku.id)
            If oSku.ean13 Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(oSku.ean13.value)
            End If
            oRow.AddCell(oSku.refProveidor)
            oRow.AddCell(oSku.category.Brand.nom)
            oRow.AddCell(oSku.category.nom)
            oRow.AddCell(oSku.nomCurt)
        Next
        Return retval
    End Function

    Shared Function Excel(exs As List(Of Exception), oCatalog As DTOProductCatalog) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Stocks", "M+O stocks " & Format(Now, "yyyyMMddThhmmssfffZ"))
        With retval
            .AddColumn("Code", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Product", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Stock", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Customers", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Supplier", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Available", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
        End With
        For Each item As DTOProductSku In oCatalog.Skus
            Dim iAvailable = Math.Max(0, item.stock - (item.clients - item.clientsAlPot - item.clientsEnProgramacio))

            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            oRow.AddCell(item.refProveidor)
            oRow.AddCell(item.nomProveidor)
            oRow.AddCell(item.stock)
            oRow.AddCell(item.clients)
            oRow.AddCell(item.proveidors)
            oRow.AddCell(iAvailable)
        Next
        Return retval
    End Function

    Shared Function ExcelStocks(oCatalog As DTOProductCatalog, oRol As DTORol) As MatHelperStd.ExcelHelper.Sheet
        Dim retval As New MatHelperStd.ExcelHelper.Sheet("Stocks", "M+O stocks " & Format(Now, "yyyyMMddThhmmssfffZ"))
        With retval
            .AddColumn("Ref.M+O", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Ean", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Ref.Fabricante", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Producto", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
            .AddColumn("Stock", MatHelperStd.ExcelHelper.Sheet.NumberFormats.Integer)
            .AddColumn("Más de 10 uds. disponibles", MatHelperStd.ExcelHelper.Sheet.NumberFormats.W50)
        End With

        For Each item As DTOProductSku In oCatalog.Skus
            Dim iAvailable = Math.Max(0, item.stock - (item.clients - item.clientsAlPot - item.clientsEnProgramacio))

            Dim oRow As MatHelperStd.ExcelHelper.Row = retval.AddRow
            oRow.AddCell(item.id)
            If item.ean13 Is Nothing Then
                oRow.AddCell()
            Else
                oRow.AddCell(item.ean13.value)
            End If
            oRow.AddCell(item.refProveidor)
            oRow.AddCell(item.NomLlargNoRef)
            oRow.AddCell(DTOProductSku.TruncatedStock(item, oRol))
            If DTOProductSku.IsTruncatedStock(item, oRol) Then
                oRow.AddCell("+")
            End If

        Next
        Return retval
    End Function

    Shared Function CsvStocks(oCatalog As DTOProductCatalog, oRol As DTORol) As DTOCsv
        Dim retval As New DTOCsv("M+O Stocks.csv")

        For Each oSku As DTOProductSku In oCatalog.Skus.Where(Function(x) x.stock > 0).OrderBy(Function(y) y.id)
            Dim oRow As DTOCsvRow = retval.AddRow()
            oRow.AddCell(oSku.id)
            oRow.AddCell(DTOProductSku.TruncatedStockValue(oSku, oRol))
        Next

        Return retval
    End Function

End Class

