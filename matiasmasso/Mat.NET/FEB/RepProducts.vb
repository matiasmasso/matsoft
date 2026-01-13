Public Class RepProduct
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTORepProduct)
        Dim retval = Await Api.Fetch(Of DTORepProduct)(exs, "RepProduct", oGuid.ToString())
        retval.RestoreObjects()
        Return retval
    End Function

    Shared Async Function Find(exs As List(Of Exception), oArea As DTOArea, oProduct As DTOProduct, oChannel As DTODistributionChannel, DtFch As Date) As Task(Of DTORepProduct)
        Dim retval = Await Api.Fetch(Of DTORepProduct)(exs, "RepProduct", oArea.Guid.ToString, oProduct.Guid.ToString, oChannel.Guid.ToString, FormatFch(DtFch))
        If retval IsNot Nothing Then
            retval.RestoreObjects()
        End If
        Return retval
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oRepProduct As DTORepProduct) As Boolean
        If Not oRepProduct.IsLoaded And Not oRepProduct.IsNew Then
            Dim pRepProduct = Api.FetchSync(Of DTORepProduct)(exs, "RepProduct", oRepProduct.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTORepProduct)(pRepProduct, oRepProduct, exs)
                oRepProduct.RestoreObjects()
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRepProduct As DTORepProduct) As Task(Of Boolean)
        Return Await Api.Update(Of DTORepProduct)(oRepProduct, exs, "RepProduct")
        oRepProduct.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRepProduct As DTORepProduct) As Task(Of Boolean)
        Return Await Api.Delete(Of DTORepProduct)(oRepProduct, exs, "RepProduct")
    End Function
End Class

Public Class RepProducts
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception), oEmp As DTOEmp, Optional oRep As DTORep = Nothing, Optional IncludeObsoletos As Boolean = False) As Task(Of List(Of DTORepProduct))
        Dim retval = Await Api.Fetch(Of List(Of DTORepProduct))(exs, "RepProducts/FromRep", oEmp.id, OpcionalGuid(oRep), If(IncludeObsoletos, 1, 0))
        For Each item In retval
            item.RestoreObjects()
            If oRep IsNot Nothing Then item.rep = oRep
        Next
        Return retval
    End Function

    Shared Function AllSync(exs As List(Of Exception), oEmp As DTOEmp, Optional oRep As DTORep = Nothing, Optional IncludeObsoletos As Boolean = False) As List(Of DTORepProduct)
        Dim retval = Api.FetchSync(Of List(Of DTORepProduct))(exs, "RepProducts/FromRep", oEmp.id, OpcionalGuid(oRep), If(IncludeObsoletos, 1, 0))
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct, Optional IncludeObsoletos As Boolean = False) As Task(Of List(Of DTORepProduct))
        Dim retval As List(Of DTORepProduct) = Await Api.Fetch(Of List(Of DTORepProduct))(exs, "RepProducts/FromProduct", oProduct.Guid.ToString, If(IncludeObsoletos, 1, 0))
        For Each item In retval
            item.RestoreObjects()
            item.product = oProduct
        Next
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oArea As DTOArea, Optional IncludeObsoletos As Boolean = False) As Task(Of List(Of DTORepProduct))
        Dim retval = Await Api.Fetch(Of List(Of DTORepProduct))(exs, "RepProducts/FromArea", oArea.Guid.ToString, If(IncludeObsoletos, 1, 0))
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oChannel As DTODistributionChannel, oArea As DTOArea, oProduct As DTOProduct, DtFch As Date) As Task(Of List(Of DTORepProduct))
        Dim retval = Await Api.Fetch(Of List(Of DTORepProduct))(exs, "RepProducts", oChannel.Guid.ToString, oArea.Guid.ToString, oProduct.Guid.ToString, FormatFch(DtFch))
        For Each item In retval
            'item.product = DTOProduct.fromJObject(item.product)
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Catalogue(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTOProductSku))
        Return Await Api.Fetch(Of List(Of DTOProductSku))(exs, "RepProducts/Catalogue/FromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function CatalogueTree(exs As List(Of Exception), oRep As DTORep) As Task(Of List(Of DTOProductBrand))
        Return Await Api.Fetch(Of List(Of DTOProductBrand))(exs, "RepProducts/CatalogueTree/FromRep", oRep.Guid.ToString())
    End Function

    Shared Async Function Customers(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOGuidNode))
        Return Await Api.Fetch(Of List(Of DTOGuidNode))(exs, "RepProducts/Customers", oUser.Guid.ToString())
    End Function

    Shared Async Function RepsxAreaWithMobiles(exs As List(Of Exception), Optional BlIncludeObsolets As Boolean = False) As Task(Of List(Of DTORepProduct))
        Dim retval = Await Api.Fetch(Of List(Of DTORepProduct))(exs, "RepProducts/RepsxAreaWithMobiles", If(BlIncludeObsolets, 1, 0))
        For Each item In retval
            item.RestoreObjects()
        Next
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oRepProducts As List(Of DTORepProduct)) As Task(Of Boolean)
        Return Await Api.Update(Of List(Of DTORepProduct))(oRepProducts, exs, "RepProducts")
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oRepProducts As List(Of DTORepProduct)) As Task(Of Boolean)
        Return Await Api.Delete(Of List(Of DTORepProduct))(oRepProducts, exs, "RepProducts")
    End Function

End Class
