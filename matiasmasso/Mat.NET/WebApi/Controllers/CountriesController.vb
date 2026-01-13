Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CountryController
    Inherits _BaseController

    <HttpGet>
    <Route("api/country/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Country.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el pais")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/country/fromIso/{iso}")>
    Public Function FromIso(iso As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Country.Find(iso)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el pais")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/country")>
    Public Function Update(<FromBody> value As DTOCountry) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Country.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el pais")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el pais")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Country/delete")>
    Public Function Delete(<FromBody> value As DTOCountry) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Country.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Country")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Country")
        End Try
        Return retval
    End Function

End Class


Public Class CountriesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/countries")>
    Public Function GetAllCountries() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Countries.All(DTOLang.ESP)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els països")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/countries/{lang}")>
    Public Function GetCountries(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Countries.All(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els països")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/countriesByUser/{user}")>
    Public Function GetCountriesByUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim values = BEBL.Countries.All(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els països")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/countries/guidnoms/{lang}")>
    Public Function Guidnoms(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Countries.GuidNoms(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els països")
        End Try
        Return retval
    End Function

End Class
