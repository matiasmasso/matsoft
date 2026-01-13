Public Class Mayborn
    Inherits _FeblBase

    Shared Async Function ValidateCustomerOrder(exs As List(Of Exception), oSheet As MatHelper.Excel.Sheet, oEmp As DTOEmp) As Task(Of MatHelper.Excel.Sheet)
        Dim oCandidate = DTOExcelMaybornPOFarma.Factory(oSheet)
        Dim oProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)
        Dim oPrvCliNums = Await PrvCliNums.All(oProveidor, exs)
        Dim oInventari = Await Mgz.Inventari(exs, oEmp.Mgz, oProveidor)

        Dim retval = oCandidate.Validate(oInventari, oPrvCliNums)
        Return retval
    End Function


    Shared Async Function CustomerOrdersFactory(exs As List(Of Exception), oSheet As MatHelper.Excel.Sheet, oUser As DTOUser, oMgz As DTOMgz) As Task(Of List(Of DTOPurchaseOrder))
        Dim oCandidate = DTOExcelMaybornPOFarma.Factory(oSheet)
        Dim oProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)
        Dim oPrvCliNums = Await PrvCliNums.All(oProveidor, exs)
        Dim oInventari = Await Mgz.Inventari(exs, oMgz, oProveidor)

        Dim retval = oCandidate.PurchaseOrders(oInventari, oPrvCliNums, oUser)
        Return retval
    End Function

    Shared Async Function Sales(exs As List(Of Exception), Optional year As Integer = 0) As Task(Of List(Of DTOProductMonthQtySalepoint))
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Return Await Api.Fetch(Of List(Of DTOProductMonthQtySalepoint))(exs, "mayborn/sales", year)
    End Function

    Shared Function GraphUrl() As String
        Dim retval = UrlHelper.Image(DTO.Defaults.ImgTypes.Mayborn, DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn).Guid)
        Return retval
    End Function

    Shared Function CsvUrl(Optional AbsoluteUrl As Boolean = False) As String
        Dim retval = UrlHelper.Dox(AbsoluteUrl, DTODocFile.Cods.MaybornSalesExcel)
        Return retval
    End Function

    Shared Async Function SalesCsv(exs As List(Of Exception), Optional year As Integer = 0) As Task(Of DTOCsv)
        Dim oProveidor = DTOProveidor.Wellknown(DTOProveidor.Wellknowns.Mayborn)
        If year = 0 Then year = DTO.GlobalVariables.Today().Year
        Dim oLang = DTOLang.ENG

        Dim retval As New DTOCsv("M+O monthly sales data summary.csv")

        Dim oFirstRow As DTOCsvRow = retval.AddRow()

        Dim oRow As DTOCsvRow = retval.AddRow()
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

        Dim items = Await Mayborn.Sales(exs, year)
        If exs.Count = 0 Then
            Dim oSku As New DTOProductSku
            For Each Item As DTOProductMonthQtySalepoint In items
                If Item.Product.UnEquals(oSku) Then
                    oSku = Item.Product
                    oRow = retval.AddRow()
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
                oRow.cells(iLastVolumeCell - 12 + Item.Month) = TextHelper.VbFormat(Item.Eur, "0.00").Replace(",", ".")
                oRow.Cells(iLastSalePointsCell - 12 + Item.Month) = Item.SalePoints
            Next
        End If
        Return retval
    End Function

End Class
