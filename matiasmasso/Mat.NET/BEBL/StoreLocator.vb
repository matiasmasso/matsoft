Public Class StoreLocator

    Shared Function Fetch(oProduct As DTOProduct, oMgz As DTOMgz, oLang As DTOLang, IncludeBlocked As Boolean) As DTOStoreLocator3

        'per botigues fisiques no busquis les que han comprat la sku sino les que han comprat la categoria
        Dim oStoreLookupProduct As DTOProduct = oProduct
        If oProduct.sourceCod = DTOProduct.SourceCods.Sku Then
            oStoreLookupProduct = CType(oProduct, DTOProductSku).Category
        End If

        'temporalment si es mamaroo 4 passar a 4moms
        If oStoreLookupProduct.Equals(DTOProductCategory.Wellknown(DTOProductCategory.Wellknowns.mamaroo5)) Then
            oStoreLookupProduct = CType(oStoreLookupProduct, DTOProductCategory).Brand
        End If

        Dim retval = StoreLocatorLoader.Fetch(oStoreLookupProduct, oLang, IncludeBlocked)

        'per les botigues online busca les landingpage encara que sigui de sku
        Dim oWtbolLandingPages As List(Of DTOWtbolLandingPage) = WtbolLandingpagesLoader.All(oProduct, oMgz)
        For Each oWtbolLandingPage In oWtbolLandingPages
            Dim oLandingPage As New DTOStoreLocator3.LandingPage(oWtbolLandingPage.Guid)
            With oLandingPage
                .Nom = oWtbolLandingPage.Site.Nom
                .Url = oWtbolLandingPage.Site.Web
                .CustomerStock = oWtbolLandingPage.Stock
                .MgzStock = oWtbolLandingPage.MgzStock
            End With
            retval.Online.LandingPages.Add(oLandingPage)
        Next
        retval.setDefaults(oLang) 'busca el millor distribuidor per posar-lo per defecte
        Return retval
    End Function

    Shared Function Fetch(oPremiumLine As DTOPremiumLine, oMgz As DTOMgz, oLang As DTOLang) As DTOStoreLocator3

        'per botigues fisiques 
        Dim retval = StoreLocatorLoader.Fetch(oPremiumLine, oLang)

        'per les botigues online busca les landingpage encara que sigui de sku
        Dim oWtbolLandingPages As List(Of DTOWtbolLandingPage) = WtbolLandingpagesLoader.All(oPremiumLine, oMgz)
        For Each oWtbolLandingPage In oWtbolLandingPages
            Dim oLandingPage As New DTOStoreLocator3.LandingPage(oWtbolLandingPage.Guid)
            With oLandingPage
                .Nom = oWtbolLandingPage.Site.Nom
                .Url = oWtbolLandingPage.Site.Web
                .CustomerStock = oWtbolLandingPage.Stock
                .MgzStock = oWtbolLandingPage.MgzStock
            End With
            retval.Online.LandingPages.Add(oLandingPage)
        Next
        retval.setDefaults(oLang) 'busca el millor distribuidor per posar-lo per defecte
        Return retval
    End Function

    Shared Function Fetch(oRaffleParticipant As DTORaffleParticipant, oLang As DTOLang) As DTOStoreLocator3
        Dim retval = StoreLocatorLoader.Fetch(oRaffleParticipant.Raffle, oLang)
        retval.setDefaults(oLang) 'busca el millor distribuidor per posar-lo per defecte
        Return retval
    End Function

    Shared Function Fetch(oRaffle As DTORaffle) As DTOStoreLocator3
        Dim retval = StoreLocatorLoader.Fetch(oRaffle)
        Return retval
    End Function


    Shared Function Load(value As DTOStoreLocator) As DTOStoreLocator
        Dim oLocation = value.Query.Location
        Dim oProduct = value.Query.Product
        BEBL.Product.Load(oProduct)
        Dim oCategoryOrBrand = DTOProduct.CategoryOrbrand(oProduct)
        Dim oAllDistributors = BEBL.StoreLocator.Distributors(value.Lang, oCategoryOrBrand)

        If oAllDistributors.Count > 0 Then
            If oLocation Is Nothing Then
                Dim oBestLocation = DTOProductDistributor.BestLocation(oAllDistributors)
                oLocation = New DTOLocation(oBestLocation.Guid)
            End If

            LocationLoader.Load(oLocation)
            value.Location = DTOArea.Factory(oLocation.Guid, DTOArea.Cods.location, oLocation.nom)

            value.Countries = oAllDistributors.Select(Function(x) x.Country).Distinct.
            OrderBy(Function(y) y.nom).
            ToList

            Dim oCountry = DTOLocation.Country(oLocation)
            value.Country = DTOArea.Factory(oCountry.Guid, DTOArea.Cods.Country, oCountry.LangNom.Tradueix(value.Lang))

            Dim oDefaultCountryGuid = oCountry.Guid
            value.Zonas = oAllDistributors.Where(Function(x) x.Country.Guid.Equals(oDefaultCountryGuid)).
            Select(Function(y) y.Zona).
            Distinct.
            OrderBy(Function(z) z.nom).
            ToList

            If oLocation.Zona.Provincia Is Nothing Then
                value.Zona = value.Zonas.FirstOrDefault(Function(x) x.Guid.Equals(oLocation.Zona.Guid))
            Else
                value.Zona = value.Zonas.FirstOrDefault(Function(x) x.Guid.Equals(oLocation.Zona.Provincia.Guid))
            End If

            If value.Zona Is Nothing Then
                value.Zona = oAllDistributors.
                    GroupBy(Function(g) New With {Key g.Zona}).
                    Select(Function(group) New With {.Zona = group.Key.Zona, .DistCount = group.Count}).
                    OrderByDescending(Function(x) x.DistCount).
                    First.Zona
            End If

            Dim oDefaultZonaGuid = value.Zona.Guid
            value.Locations = oAllDistributors.Where(Function(x) x.Zona.Guid.Equals(oDefaultZonaGuid)).
            Select(Function(y) y.Location).
            Distinct.
            OrderBy(Function(z) z.nom).
            ToList

            If Not value.Locations.Any(Function(x) x.Guid.Equals(oLocation.Guid)) Then
                value.Location = oAllDistributors.
                    Where(Function(x) x.Zona.Guid.Equals(oDefaultZonaGuid)).
                    GroupBy(Function(g) New With {Key g.Location}).
                    Select(Function(group) New With {.location = group.Key.Location, .DistCount = group.Count}).
                    OrderByDescending(Function(x) x.DistCount).
                    First.location
            End If

            Dim oDefaultLocationGuid = value.Location.Guid
            value.Distributors = oAllDistributors.
                Where(Function(x) x.Location.Guid.Equals(oDefaultLocationGuid)).
                ToList
        End If

        'value.Countries = New List(Of DTOArea)
        Return value
    End Function

    Shared Function Countries(oProduct As DTOProduct, Optional oLang As DTOLang = Nothing) As List(Of DTOArea)
        Dim oDistributors = BEBL.StoreLocator.Distributors(oLang, oProduct)
        Dim retval = oDistributors.Select(Function(x) x.Country).Distinct.
            OrderBy(Function(y) y.nom).
            ToList
        Return retval
    End Function

    Shared Function Zonas(oProduct As DTOProduct, oCountry As DTOArea, Optional oLang As DTOLang = Nothing) As List(Of DTOArea)
        Dim oDistributors = BEBL.StoreLocator.Distributors(oLang, oProduct, oCountry)
        Dim retval = oDistributors.Select(Function(x) x.Zona).Distinct.
            OrderBy(Function(y) y.nom).
            ToList
        Return retval
    End Function

    Shared Function Locations(oProduct As DTOProduct, oProvinciaOrZona As DTOArea, Optional oLang As DTOLang = Nothing, Optional LatestPdcFchFrom As Date = Nothing) As List(Of DTOArea)
        Dim oDistributors = BEBL.StoreLocator.Distributors(oLang, oProduct, oProvinciaOrZona, LatestPdcFchFrom)
        Dim retval = oDistributors.Select(Function(x) x.Location).Distinct.
            OrderBy(Function(y) y.nom).
            ToList
        Return retval
    End Function


    Shared Function Distributors(oLang As DTOLang, oProduct As DTOProduct, Optional oArea As DTOArea = Nothing, Optional LatestPdcFchFrom As Date = Nothing) As List(Of DTOProductDistributor)
        Dim oCategoryOrBrand As DTOProduct = DTOProduct.CategoryOrBrand(oProduct)
        Dim retval = StoreLocatorLoader.Distributors(oLang, oCategoryOrBrand, oArea)
        If LatestPdcFchFrom <> Nothing Then
            retval = retval.Where(Function(x) x.LastFch >= LatestPdcFchFrom).ToList
        End If
        Return retval
    End Function

    Shared Function Distributors(oProveidor As DTOProveidor, oLang As DTOLang, Optional includeItems As Boolean = False) As List(Of DTOProductDistributor)
        Return StoreLocatorLoader.Distributors(oProveidor, oLang, includeItems)
    End Function

    Shared Function BestZona(oLang As DTOLang, oProduct As DTOProduct, oCountry As DTOArea) As DTOBaseGuid
        Dim oDistributors = BEBL.StoreLocator.Distributors(oLang, oProduct, oCountry)
        Dim retval = DTOProductDistributor.BestZona(oDistributors)
        Return retval
    End Function

    Shared Function BestLocation(oLang As DTOLang, oProduct As DTOProduct, oProvinciaOrZona As DTOArea) As DTOBaseGuid
        Dim oDistributors = BEBL.StoreLocator.Distributors(oLang, oProduct, oProvinciaOrZona)
        Dim retval = DTOProductDistributor.BestLocation(oDistributors)
        Return retval
    End Function

    Shared Function NearestNeighbours(oProduct As DTOProduct, oCoordenadas As GeoHelper.Coordenadas, oLang As DTOLang, Optional iCount As Integer = 7) As List(Of DTONeighbour)
        Dim retval As List(Of DTONeighbour) = WebAtlasLoader.NearestNeighbours(oProduct, oCoordenadas, oLang, iCount).ToList
        Return retval
    End Function
End Class
