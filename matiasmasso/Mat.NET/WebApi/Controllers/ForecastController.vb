Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ForecastController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Forecast/Excel/{user}/{mgz}")>
    Public Function Excel(user As Guid, mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser.Rol.Id = DTORol.Ids.Manufacturer Then
                Dim oMgz As New DTOMgz(mgz)
                Dim oProveidor = BEBL.User.GetProveidor(oUser)
                Dim oForecasts = BEBL.ProductSkuForecasts.All(oMgz, oProveidor:=oProveidor, FromNowOn:=True)
                oForecasts = oForecasts.Where(Function(x) x.Forecasts.Any(Function(y) y.Target > 0)).ToList
                Dim value = DTOProductSkuForecast.Excel(oForecasts, DTOLang.ENG)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage("usuari no autoritzat")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la RepCustomer")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/Forecast/FromProveidor/{Mgz}/{Proveidor}")>
    Public Function FromProveidor(Mgz As Guid, Proveidor As Guid) As DTOProductSkuForecast.Collection
        'Public Function PostValue(<FromBody()> ByVal user As DTOUser) As List(Of DTOWinMenuItem)
        Dim oProveidor As New DTOProveidor(Proveidor)
        Dim oMgz As New DTOMgz(Mgz)
        Dim retval As DTOProductSkuForecast.Collection = BEBL.ProductSkuForecasts.All(oMgz, oProveidor:=oProveidor)
        Return retval
    End Function

    <HttpGet>
    <Route("api/Forecast/FromProduct/{Mgz}/{Product}")>
    Public Function FromProduct(Mgz As Guid, Product As Guid) As DTOProductSkuForecast.Collection
        'Public Function PostValue(<FromBody()> ByVal user As DTOUser) As List(Of DTOWinMenuItem)
        Dim oProduct As New DTOProduct(Product)
        Dim oMgz As New DTOMgz(Mgz)
        Dim retval As DTOProductSkuForecast.Collection = BEBL.ProductSkuForecasts.All(oMgz, oProduct:=oProduct)
        Return retval
    End Function

    <HttpPost>
    <Route("api/Forecasts/insert")>
    Public Function Insert(<FromBody()> ByVal values As DTOProductSkuForecast.Collection) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.ProductSkuForecasts.Insert(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el forecast")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el forecast")
        End Try
        Return retval
    End Function
End Class
