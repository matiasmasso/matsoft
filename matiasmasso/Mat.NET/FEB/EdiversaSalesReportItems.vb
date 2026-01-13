Public Class EdiversaSalesReportItems
    Inherits _FeblBase

    Shared Async Function SalesReport(exs As List(Of Exception), value As DTOSalesReport) As Task(Of DTOSalesReport)
        Dim retval = Await Api.Execute(Of DTOSalesReport, DTOSalesReport)(value, exs, "EdiversaSalesReport")
        If retval IsNot Nothing AndAlso retval.Catalog IsNot Nothing Then
            For Each oBrand In retval.Catalog
                For Each oCategory In oBrand.Categories
                    oCategory.Brand = oBrand
                    For Each oSku In oCategory.Skus
                        oSku.Category = oCategory
                    Next
                Next
            Next
        End If
        Return retval
    End Function

    Shared Function CatalegSync(exs As List(Of Exception), iYear As Integer, Optional oCustomer As DTOCustomer = Nothing, Optional oProveidor As DTOProveidor = Nothing) As DTOProductCatalog
        Return Api.FetchSync(Of DTOProductCatalog)(exs, "EdiversaSalesReportItems/Cataleg", iYear, OpcionalGuid(oCustomer), OpcionalGuid(oProveidor))
    End Function

    Shared Async Function StatItems(exs As List(Of Exception), iYear As Integer, oUser As DTOUser, Optional oProduct As DTOProduct = Nothing, Optional oCustomer As DTOCustomer = Nothing) As Task(Of DTOStat)
        Return Await Api.Fetch(Of DTOStat)(exs, "EdiversaSalesReportItems/StatItems", iYear, OpcionalGuid(oUser), OpcionalGuid(oProduct), OpcionalGuid(oCustomer))
    End Function

    Shared Function StatItemsSync(exs As List(Of Exception), iYear As Integer, oUser As DTOUser, Optional oProduct As DTOProduct = Nothing, Optional oCustomer As DTOCustomer = Nothing) As DTOStat
        Return Api.FetchSync(Of DTOStat)(exs, "EdiversaSalesReportItems/StatItems", iYear, OpcionalGuid(oUser), OpcionalGuid(oProduct), OpcionalGuid(oCustomer))
    End Function

    Shared Async Function SellOutData(exs As List(Of Exception), iYear As Integer, oUser As DTOUser, oHolding As DTOHolding) As Task(Of DTOStat2)
        Return Await Api.Fetch(Of DTOStat2)(exs, "EdiversaSalesReportItems/StatItems", iYear, oUser.Guid.ToString, oHolding.Guid.ToString)
    End Function

    Shared Async Function Excel(exs As List(Of Exception), iYear As Integer, oUser As DTOUser, oHolding As DTOHolding, units As DTOStat2.Units, brand As Guid, category As Guid, sku As Guid) As Task(Of MatHelper.Excel.Sheet)
        Return Await Api.Fetch(Of MatHelper.Excel.Sheet)(exs, "EdiversaSalesReportItems/Excel", iYear, oUser.Guid.ToString, oHolding.Guid.ToString, units, brand.ToString(), category.ToString(), sku.ToString())
    End Function


End Class
