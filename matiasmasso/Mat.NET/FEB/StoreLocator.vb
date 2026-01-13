Public Class StoreLocator
    Inherits _FeblBase

    Shared Async Function Fetch(exs As List(Of Exception), oProduct As DTOProduct, oLang As DTOLang) As Task(Of DTOStoreLocator3)
        Dim retval = Await Api.Fetch(Of DTOStoreLocator3)(exs, "StoreLocator", oProduct.Guid.ToString, oLang.Tag)
        Return retval
    End Function

    Shared Async Function ForRaffle(exs As List(Of Exception), oRaffle As DTORaffle) As Task(Of DTOStoreLocator3)
        Dim retval = Await Api.Fetch(Of DTOStoreLocator3)(exs, "StoreLocator/forRaffle", oRaffle.Guid.ToString)
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oProduct As DTOProduct, oLang As DTOLang) As Task(Of DTOStoreLocator3)
        Dim retval = Await Api.Fetch(Of DTOStoreLocator3)(exs, "StoreLocator/all", oProduct.Guid.ToString, oLang.Tag)
        Return retval
    End Function

    Shared Async Function FetchPremium(exs As List(Of Exception), oPremiumLine As DTOPremiumLine, oLang As DTOLang) As Task(Of DTOStoreLocator3)
        Dim retval = Await Api.Fetch(Of DTOStoreLocator3)(exs, "StoreLocator/premium", oPremiumLine.Guid.ToString, oLang.Tag)
        Return retval
    End Function

    Shared Async Function Countries(exs As List(Of Exception), oProduct As DTOProduct, oLang As DTOLang) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "StoreLocator/countries", oProduct.Guid.ToString, oLang.Tag)
    End Function

    Shared Async Function Zonas(exs As List(Of Exception), oProduct As DTOProduct, oCountry As DTOArea, oLang As DTOLang) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "StoreLocator/zonas", oProduct.Guid.ToString, oCountry.Guid.ToString, oLang.Tag)
    End Function

    Shared Async Function Locations(exs As List(Of Exception), oProduct As DTOProduct, oArea As DTOArea, oLang As DTOLang) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "StoreLocator/locations", oProduct.Guid.ToString, oArea.Guid.ToString, oLang.Tag)
    End Function

    Shared Async Function Locations(exs As List(Of Exception), oProduct As DTOProduct, oArea As DTOArea, oLang As DTOLang, LatestPdcFchFrom As Date) As Task(Of List(Of DTOArea))
        Return Await Api.Fetch(Of List(Of DTOArea))(exs, "StoreLocator/locations", oProduct.Guid.ToString, oArea.Guid.ToString, oLang.Tag, FormatFch(LatestPdcFchFrom))
    End Function


    Shared Async Function Distributors(exs As List(Of Exception), oProduct As DTOProduct, oLocation As DTOArea, oLang As DTOLang) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "StoreLocator/distributors", oProduct.Guid.ToString, oLocation.Guid.ToString, oLang.Tag)
    End Function

    Shared Async Function Distributors(exs As List(Of Exception), oProveidor As DTOProveidor) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "StoreLocator/distributors/FromProveidor", oProveidor.Guid.ToString())
    End Function

    Shared Async Function RaffleLocationDistributors(exs As List(Of Exception), oProduct As DTOProduct, oLocation As DTOArea, oLang As DTOLang, LatestPdcFchFrom As Date) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "StoreLocator/distributors/fromLocation", oProduct.Guid.ToString, oLocation.Guid.ToString, oLang.Tag, FormatFch(LatestPdcFchFrom))
    End Function

    Shared Async Function RaffleZonaDistributors(exs As List(Of Exception), oProduct As DTOProduct, oProvinciaOrZona As DTOArea, oLang As DTOLang, LatestPdcFchFrom As Date) As Task(Of List(Of DTOProductDistributor))
        Return Await Api.Fetch(Of List(Of DTOProductDistributor))(exs, "StoreLocator/distributors/fromZona", oProduct.Guid.ToString, oProvinciaOrZona.Guid.ToString, oLang.Tag, FormatFch(LatestPdcFchFrom))
    End Function

    Shared Async Function NearestNeighbours(exs As List(Of Exception), oProduct As DTOProduct, oCoordenadas As GeoHelper.Coordenadas, oLang As DTOLang) As Task(Of List(Of DTONeighbour))
        Return Await Api.Fetch(Of List(Of DTONeighbour))(exs, "StoreLocator/NearestNeighbours", oProduct.Guid.ToString, oCoordenadas.Latitud, oCoordenadas.Longitud, oLang.Tag)
    End Function

End Class
