Public Class Mayborn
    Public Enum Modes
        Sales
        Distribution
    End Enum

    Shared Function Proveidor() As DTOProveidor
        Dim retval As DTOProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)
        Return retval
    End Function

    Shared Function Sales(Optional year As Integer = 0) As List(Of DTOProductMonthQtySalepoint)
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Return MaybornLoader.Sales(Proveidor, year)
    End Function

    Shared Function Grafic(items As List(Of DTOProductMonthQtySalepoint)) As Byte()
        Dim retval = LegacyHelper.DTOGraph.Image(items)
        Return retval
    End Function


    Shared Function SalesCsv(oProveidor As DTOProveidor, Optional year As Integer = 0) As DTOCsv
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Dim oLang As DTOLang = DTOLang.ENG

        Dim retval As New DTOCsv("M+O monthly sales data summary.csv")

        Dim oFirstRow As DTOCsvRow = CsvHelper.AddRow(retval)

        Dim oRow As DTOCsvRow = CsvHelper.AddRow(retval)
        oRow.Cells.Add("Category")
        oRow.Cells.Add("Mayborn Code")
        oRow.Cells.Add("M+O Code")
        oRow.Cells.Add("Barcode")
        oRow.Cells.Add("Description")

        For i As Integer = 1 To 12
            oRow.Cells.Add(oLang.MesAbr(i))
        Next
        Dim iLastQtyCell As Integer = oRow.Cells.Count - 1

        For i As Integer = 1 To 12
            oRow.Cells.Add(oLang.MesAbr(i))
        Next
        Dim iLastVolumeCell As Integer = oRow.Cells.Count - 1

        For i As Integer = 1 To 12
            oRow.Cells.Add(oLang.MesAbr(i))
        Next
        Dim iLastSalePointsCell As Integer = oRow.Cells.Count - 1

        For i As Integer = 1 To iLastSalePointsCell
            oFirstRow.Cells.Add("")
        Next

        oFirstRow.Cells(iLastQtyCell - 12 + 1) = "Volume / Units"
        oFirstRow.Cells(iLastVolumeCell - 12 + 1) = "M+O Value / Amount"
        oFirstRow.Cells(iLastSalePointsCell - 12 + 1) = "Distribution/Destination (No Of Outlets)"

        Dim items As List(Of DTOProductMonthQtySalepoint) = MaybornLoader.Sales(oProveidor, year)
        Dim oSku As New DTOProductSku
        For Each Item As DTOProductMonthQtySalepoint In items
            If Item.Product.UnEquals(oSku) Then
                oSku = Item.Product
                oRow = CsvHelper.AddRow(retval)
                oRow.cells.Add(oSku.category.nom.Tradueix(oLang))
                oRow.Cells.Add(oSku.RefProveidor)
                oRow.Cells.Add(oSku.Id)
                oRow.Cells.Add("'" & oSku.Ean13.Value)
                oRow.Cells.Add(oSku.NomProveidor)
                For i As Integer = 1 To 3 * 12
                    oRow.Cells.Add(0)
                Next
            End If

            oRow.Cells(iLastQtyCell - 12 + Item.Month) = Item.Qty
            oRow.Cells(iLastVolumeCell - 12 + Item.Month) = Format(Item.Eur, "0.00").Replace(",", ".")
            oRow.Cells(iLastSalePointsCell - 12 + Item.Month) = Item.SalePoints
        Next
        Return retval
    End Function

    Shared Function Graph(oMode As Modes) As Byte()
        Dim oLang As DTOLang = DTOLang.ENG
        Dim oProveidor As DTOProveidor = Proveidor()
        Dim lastmonth As Integer = DTO.GlobalVariables.Today().AddMonths(-1).Month
        Dim year As Integer = DTO.GlobalVariables.Today().AddMonths(-1).Year

        Dim DcValues(11) As Decimal

        Dim oGraph As New LegacyHelper.DTOGraph
        Select Case oMode
            Case Modes.Sales
                Dim items As List(Of DTOProductMonthQtySalepoint) = MaybornLoader.Sales(oProveidor, year)
                For i = 0 To 11
                    Dim iMonth As Integer = i + 1
                    DcValues(i) = items.Where(Function(x) x.Month = iMonth).Sum(Function(x) x.Eur)
                Next
                With oGraph
                    .Title = "Volumes " & year
                    .Values = DcValues.ToList
                    .ColumnsCount = 12
                    .EndColumn = lastmonth - 1
                    .xAxisLabels = LegacyHelper.DTOGraph.xAxisMonths(oLang)
                End With
        End Select
        Dim retval As Byte() = oGraph.Bitmap()
        Return retval
    End Function


    Shared Function GraphUrl() As String
        Dim retval As String = UrlHelper.Image(DTO.Defaults.ImgTypes.Mayborn, DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn).Guid)
        Return retval
    End Function

    Shared Function CsvUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval As String = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.MaybornSalesExcel)
        'Dim retval As String = UrlHelper.Factory(False, "doc", DTODocFile.Cods.MaybornSalesExcel, BLLProveidor.Wellknown(DTOProveidor.Wellknown.Mayborn).Guid.ToString())
        Return retval
    End Function
End Class

