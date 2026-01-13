Imports System.Web

Public Class ProductUrl

    Shared Function Search(url As String) As DTOProduct.ProductAndTab
        Return ProductUrlLoader.Search(url)
    End Function

    Shared Function Search2(url As String) As DTOProduct.ProductAndTab
        Dim retval As New DTOProduct.ProductAndTab
        Dim segments = url.Trim("/").Split("/").ToList
        Dim lastSegment = HttpUtility.UrlDecode(segments.Last)
        retval.Tab = DTOProduct.Tab(lastSegment)
        If retval.Tab <> DTOProduct.Tabs.general Then segments.Remove(segments.Last)
        If segments.Count > 4 Then segments = segments.Take(4).ToList

        Dim oCache = ServerCache.ForEmp(DTOEmp.Ids.MatiasMasso)
        ServerCache.CheckForServerUpdates(oCache)
        Dim oUrls = oCache.ProductUrls
        retval.Product = Product(segments, oUrls)
        Return retval
    End Function

    Private Shared Function Product(segments As List(Of String), oUrls As List(Of Dictionary(Of String, Object))) As DTOProduct
        Dim retval As DTOProduct = Nothing
        Dim oBrandGuid, oDeptGuid, oCategoryGuid, oSkuGuid As Guid
        Dim oBrand = oUrls.FirstOrDefault(Function(x) CType(x, Models.Min.ProductUrl).Matches(DTOProduct.SourceCods.Brand, segments))
        If oBrand IsNot Nothing Then
            oBrandGuid = Models.Min.Minifiable.Guid(oBrand, Models.Min.ProductUrl.Cods.Brand)
            retval = New DTOProductBrand(oBrandGuid)
            Dim oDept = oUrls.FirstOrDefault(Function(x) CType(x, Models.Min.ProductUrl).Matches(DTOProduct.SourceCods.Dept, segments) And Models.Min.Minifiable.Guid(x, Models.Min.ProductUrl.Cods.Brand) = oBrandGuid)
            If oDept IsNot Nothing Then
                oDeptGuid = Models.Min.Minifiable.Guid(oDept, Models.Min.ProductUrl.Cods.Dept)
                retval = New DTODept(oDeptGuid)
            End If
            Dim oCategory = oUrls.FirstOrDefault(Function(x) CType(x, Models.Min.ProductUrl).Matches(DTOProduct.SourceCods.Category, segments) And Models.Min.Minifiable.Guid(x, Models.Min.ProductUrl.Cods.Brand) = oBrandGuid)
            If oCategory IsNot Nothing Then
                oCategoryGuid = Models.Min.Minifiable.Guid(oCategory, Models.Min.ProductUrl.Cods.Category)
                retval = New DTOProductCategory(oCategoryGuid)
                Dim oSku = oUrls.FirstOrDefault(Function(x) CType(x, Models.Min.ProductUrl).Matches(DTOProduct.SourceCods.Sku, segments) And Models.Min.Minifiable.Guid(x, Models.Min.ProductUrl.Cods.Category) = oCategoryGuid)
                If oSku IsNot Nothing Then
                    oSkuGuid = Models.Min.Minifiable.Guid(oSku, Models.Min.ProductUrl.Cods.Sku)
                    retval = New DTOProductSku(oSkuGuid)
                End If
            End If
        End If

        Return retval
    End Function
End Class
