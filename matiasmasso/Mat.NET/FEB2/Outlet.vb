Public Class Outlet
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "Outlet", oUser.Guid.ToString())
    End Function

    Shared Function MOQ(oSku As DTOProductSku) As Integer
        Dim retval As Integer = Math.Max(1, oSku.OutletQty)
        Return retval
    End Function

    Shared Async Function Csv(oEmp As DTOEmp, oUser As DTOUser, Optional oLang As DTOLang = Nothing) As Task(Of DTOCsv)
        Dim exs As New List(Of Exception)
        Dim oDomain = DTOWebDomain.Factory(oLang)
        Dim oMgz = oEmp.Mgz
        Dim oBrands = Await FEB2.ProductBrands.All(exs, oUser)
        Dim items As List(Of DTOProductSku) = Await FEB2.Outlet.All(exs, oUser)
        items = items.Where(Function(x) oBrands.Any(Function(y) y.Equals(x.category.brand))).ToList

        Dim retval As New DTOCsv
        Dim oRow As DTOCsvRow = retval.addRow()
        oRow.addCell("Brand")
        oRow.addCell("Category")
        oRow.addCell("Sku")
        oRow.addCell("Nom")
        oRow.addCell("Product")
        oRow.addCell("Stock")
        oRow.addCell("RRPP")
        oRow.addCell("Url")

        For Each item As DTOProductSku In items
            oRow = retval.addRow()
            oRow.addCell(item.category.brand.nom.Tradueix(oLang))
            oRow.addCell(item.category.nom.Tradueix(oLang))
            oRow.addCell(item.refProveidor)
            oRow.addCell(item.nomProveidor)
            oRow.addCell(item.nom.Tradueix(oLang))
            oRow.addCell(item.stock)
            If item.rrpp Is Nothing Then
                oRow.addCell("")
            Else
                oRow.addCell(item.rrpp.Eur)
            End If
            oRow.addCell(item.GetUrl(oDomain.DefaultLang))
        Next
        Return retval
    End Function

End Class
