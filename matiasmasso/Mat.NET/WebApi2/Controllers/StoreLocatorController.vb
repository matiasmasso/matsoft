Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StoreLocatorController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StoreLocator/{product}/{lang}")>
    Public Function Fetch(product As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oLang = DTOLang.Factory(lang)
            Dim oMgz = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso).Mgz
            Dim value = BEBL.StoreLocator.Fetch(oProduct, oMgz, oLang, IncludeBlocked:=False)
            retval = Request.CreateResponse(Of DTOStoreLocator3)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/all/{product}/{lang}")>
    Public Function All(product As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oLang = DTOLang.Factory(lang)
            Dim oMgz = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso).Mgz
            Dim value = BEBL.StoreLocator.Fetch(oProduct, oMgz, oLang, IncludeBlocked:=True)
            retval = Request.CreateResponse(Of DTOStoreLocator3)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/premium/{premiumline}/{lang}")>
    Public Function FetchPremium(premiumline As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPremiumLine As New DTOPremiumLine(premiumline)
            Dim oLang = DTOLang.Factory(lang)
            Dim oMgz = MyBase.GetEmp(DTOEmp.Ids.MatiasMasso).Mgz
            Dim value = BEBL.StoreLocator.Fetch(oPremiumLine, oMgz, oLang)
            retval = Request.CreateResponse(Of DTOStoreLocator3)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/StoreLocator")>
    Public Function Load(<FromBody> value As DTOStoreLocator) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim loadedValue = BEBL.StoreLocator.Load(value)
            retval = Request.CreateResponse(Of DTOStoreLocator)(HttpStatusCode.OK, loadedValue)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/StoreLocator/forRaffle/{raffle}")>
    Public Function ForRaffle(raffle As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oRaffle As New DTORaffle(raffle)
            Dim value = BEBL.StoreLocator.Fetch(oRaffle)
            retval = Request.CreateResponse(Of DTOStoreLocator3)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Countries/{product}/{lang}")>
    Public Function Countries(product As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Countries(oProduct, oLang)
            retval = Request.CreateResponse(Of List(Of DTOArea))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Zonas/{product}/{area}/{lang}")>
    Public Function Zonas(product As Guid, area As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oArea As New DTOArea(area)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Zonas(oProduct, oArea, oLang)
            retval = Request.CreateResponse(Of List(Of DTOArea))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/BestZona/{product}/{country}/{lang}")>
    Public Function BestZona(product As Guid, country As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCountry = DTOArea.Factory(country, DTOArea.Cods.Country)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.StoreLocator.BestZona(oLang, oProduct, oCountry)
            retval = Request.CreateResponse(Of DTOBaseGuid)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Locations/{product}/{provinciaOrZona}/{lang}")>
    Public Function Locations(product As Guid, provinciaOrZona As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oProvinciaOrZona As New DTOArea(provinciaOrZona)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Locations(oProduct, oProvinciaOrZona, oLang)
            retval = Request.CreateResponse(Of List(Of DTOArea))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/BestLocation/{product}/{provinciaOZona}/{lang}")>
    Public Function BestLocation(product As Guid, provinciaOZona As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oProvinciaOZona = DTOArea.Factory(provinciaOZona, DTOArea.Cods.Zona)
            Dim oLang = DTOLang.Factory(lang)
            Dim value = BEBL.StoreLocator.Bestlocation(oLang, oProduct, oProvinciaOZona)
            retval = Request.CreateResponse(Of DTOBaseGuid)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Distributors/{product}/{location}/{lang}")>
    Public Function Distribuidors(product As Guid, location As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCategoryOrBrand = DTOProduct.CategoryOrBrand(oProduct)
            Dim oLocation As New DTOLocation(location)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Distributors(oLang, oCategoryOrBrand, oLocation)
            retval = Request.CreateResponse(Of List(Of DTOProductDistributor))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la xarxa de distribució")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Distributors/FromProveidor/{proveidor}")>
    Public Function Distribuidors(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor = BEBL.Proveidor.Find(proveidor)
            Dim values = BEBL.StoreLocator.Distributors(oProveidor, oProveidor.Lang)
            retval = Request.CreateResponse(Of List(Of DTOProductDistributor))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la xarxa de distribució")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/StoreLocator/Locations/{product}/{provinciaOrZona}/{lang}/{latestPdcfchFrom}")>
    Public Function RaffleDistribuidors(product As Guid, provinciaOrZona As Guid, lang As String, latestPdcfchFrom As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCategoryOrBrand = DTOProduct.CategoryOrBrand(oProduct)
            Dim oprovinciaOrZona As New DTOArea(provinciaOrZona)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Locations(oCategoryOrBrand, oprovinciaOrZona, oLang, latestPdcfchFrom)
            retval = Request.CreateResponse(Of List(Of DTOArea))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/StoreLocator/Distributors/fromLocation/{product}/{location}/{lang}/{latestPdcfchFrom}")>
    Public Function LocationDistribuidors(product As Guid, location As Guid, lang As String, latestPdcfchFrom As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCategoryOrBrand = DTOProduct.CategoryOrBrand(oProduct)
            Dim oLocation As New DTOLocation(location)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Distributors(oLang, oCategoryOrBrand, oLocation, latestPdcfchFrom)
            retval = Request.CreateResponse(Of List(Of DTOProductDistributor))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StoreLocator/Distributors/fromZona/{product}/{zona}/{lang}/{latestPdcfchFrom}")>
    Public Function ZonaDistribuidors(product As Guid, zona As Guid, lang As String, latestPdcfchFrom As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCategoryOrBrand = DTOProduct.CategoryOrBrand(oProduct)
            Dim oProvinciaOrZona As New DTOArea(zona)
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.StoreLocator.Distributors(oLang, oCategoryOrBrand, oProvinciaOrZona, latestPdcfchFrom)
            retval = Request.CreateResponse(Of List(Of DTOProductDistributor))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StoreLocator")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/StoreLocator/NearestNeighbours/{product}/{lat}/{lng}/{lang}")>
    Public Function NearestNeighbours(product As Guid, lat As String, lng As String, lang As String) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct = BEBL.Product.Find(product)
            Dim oCategoryOrBrand = DTOProduct.CategoryOrBrand(oProduct)
            Dim DblLat = NumericHelper.ParseDouble(lat, exs)
            Dim DblLng = NumericHelper.ParseDouble(lng, exs)
            If exs.Count = 0 Then
                Dim oCoordenadas = GeoHelper.Coordenadas.Factory(DblLat, DblLng)
                Dim oLang = DTOLang.Factory(lang)
                Dim values = BEBL.StoreLocator.NearestNeighbours(oCategoryOrBrand, oCoordenadas, oLang)
                retval = Request.CreateResponse(Of List(Of DTONeighbour))(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al buscar els punts de venda mes propers")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al buscar els punts de venda mes propers")
        End Try
        Return retval
    End Function



End Class
