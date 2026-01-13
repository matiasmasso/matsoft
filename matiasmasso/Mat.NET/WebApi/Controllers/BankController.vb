Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BankController

    Inherits _BaseController

    <HttpGet>
    <Route("api/Bank/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Bank.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Bank")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/Bank/FromCodi/{country}/{codi}")>
    Public Function FromCodi(country As Guid, codi As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.Bank.FromCodi(oCountry, codi)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Bank")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Bank/logo/{guid}")>
    Public Function GetLogo(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Bank.Logo(guid)
            retval = MyBase.HttpImageMimeResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el logo del Bank")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Bank")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOBank)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el Bank")
            Else
                Dim ologo = oHelper.GetImage("logo")
                If ologo IsNot Nothing Then
                    value.logo = ologo
                End If

                If DAL.BankLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el bank a DAL.BankLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.BankLoader")
        End Try

        Return result
    End Function



    <HttpPost>
    <Route("api/Bank/delete")>
    Public Function Delete(<FromBody> value As DTOBank) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Bank.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Bank")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Bank")
        End Try
        Return retval
    End Function

End Class

Public Class BanksController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Banks/{country}")>
    Public Function All(country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.Banks.All(oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els Banks")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Banks/countries/{lang}")>
    Public Function All(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim values = BEBL.Banks.Countries(oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els paisos dels Banks")
        End Try
        Return retval
    End Function

End Class
