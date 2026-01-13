Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class IbanStructureController
    Inherits _BaseController

    <HttpGet>
    <Route("api/IbanStructure/{country}")>
    Public Function Find(country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCountry As New DTOCountry(country)
            Dim value = BEBL.IbanStructure.Find(oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la IbanStructure")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/IbanStructure/FromCountryIso/{iso}")>
    Public Function Find(iso As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.IbanStructure.Find(iso)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la IbanStructure")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/IbanStructure")>
    Public Function Update(<FromBody> value As DTOIban.Structure) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.IbanStructure.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la IbanStructure")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la IbanStructure")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/IbanStructure/delete")>
    Public Function Delete(<FromBody> value As DTOIban.Structure) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.IbanStructure.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la IbanStructure")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la IbanStructure")
        End Try
        Return retval
    End Function

End Class

Public Class IbanStructuresController
    Inherits _BaseController

    <HttpGet>
    <Route("api/IbanStructures")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.IbanStructures.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les IbanStructures")
        End Try
        Return retval
    End Function

End Class
