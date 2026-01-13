Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class FilterController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Filter/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Filter.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Filter")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/Filter")>
    Public Function Update(<FromBody> value As DTOFilter) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Filter.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Filter")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Filter")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Filter/delete")>
    Public Function Delete(<FromBody> value As DTOFilter) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Filter.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Filter")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Filter")
        End Try
        Return retval
    End Function

End Class

Public Class FiltersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Filters")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Filters.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els filtres")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Filters/fromBrand/{brand}")>
    Public Function FromBrand(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.Filters.All(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els filtres")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Filters")>
    Public Function Update(<FromBody> values As List(Of DTOFilter)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Filters.Update(values, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els filtres")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els filtres")
        End Try
        Return retval
    End Function

End Class

