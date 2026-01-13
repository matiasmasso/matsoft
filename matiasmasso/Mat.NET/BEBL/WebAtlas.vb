Public Class WebAtlas


    Shared Function distribuidors(oProveidor As DTOProveidor) As List(Of DTOProductDistributor)
        Dim retval = StoreLocatorLoader.Distributors(DTOLang.ENG, proveidor:=oProveidor)
        Return retval
    End Function

    Shared Function distribuidors(Optional product As DTOProduct = Nothing, Optional oProvinciaOrZona As DTOArea = Nothing, Optional location As DTOLocation = Nothing, Optional proveidor As DTOProveidor = Nothing, Optional lang As DTOLang = Nothing, Optional includeItems As Boolean = False, Optional LatestPdcFchFrom As Date = Nothing) As List(Of DTOProductDistributor)
        Dim oArea = If(location, oProvinciaOrZona)
        Dim oDistributors = StoreLocatorLoader.Distributors(lang, product:=DTOProduct.CategoryOrbrand(product), oArea:=oArea, proveidor:=proveidor, includeItems:=includeItems)
        'Dim oDistributorsDeprecated As List(Of DTOProductDistributor) = WebAtlasLoader.Distributors(product:=DTOProduct.CategoryOrbrand(product), oProvinciaOrZona:=oProvinciaOrZona, proveidor:=proveidor, lang:=lang, includeItems:=includeItems, oLocation:=location).ToList
        If LatestPdcFchFrom <> Nothing Then
            oDistributors = oDistributors.Where(Function(x) x.LastFch >= LatestPdcFchFrom).ToList
        End If

        Dim retval As List(Of DTOProductDistributor) = Nothing
        If location Is Nothing Then
            retval = oDistributors
        Else
            retval = DTOProductDistributor.PremiumOrSpareDistributors(oDistributors, location)
        End If
        Return retval
    End Function


    Shared Function NearestNeighbours(oProduct As DTOProduct, oCoordenadas As GeoHelper.Coordenadas, oLang As DTOLang, Optional iCount As Integer = 7) As List(Of DTONeighbour)
        Dim retval As List(Of DTONeighbour) = WebAtlasLoader.NearestNeighbours(oProduct, oCoordenadas, oLang, iCount).ToList
        Return retval
    End Function

    Shared Function Update(oEmp As DTOEmp, exs As List(Of Exception)) As Boolean
        Return WebAtlasLoader.UpdateWebAtlas(oEmp, exs)
    End Function


End Class
