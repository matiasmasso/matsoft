Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class FilterTargetsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/FilterTargets/{target}")>
    Public Function All(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oTarget As New DTOBaseGuid(target)
            Dim values = BEBL.FilterTargets.All(oTarget)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els filtres del target")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/FilterTargets/Filters")>
    Public Function Filters(<FromBody> guids As Guid()) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values As New List(Of DTOFilter)
            If guids IsNot Nothing Then
                values = BEBL.FilterTargets.Filters(guids.ToList())
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els filtres del target")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/FilterTargets/{target}")>
    Public Function Update(target As Guid, <FromBody> values As List(Of DTOFilter.Item)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTarget As New DTOBaseGuid(target)
            If BEBL.FilterTargets.Update(exs, oTarget, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la FilterTarget")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la FilterTarget")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/FilterTargets/delete/{target}")>
    Public Function Delete(target As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oTarget As New DTOBaseGuid(target)
            If BEBL.FilterTargets.Delete(exs, oTarget) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar els filtres del target")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar els filtres del target")
        End Try
        Return retval
    End Function

End Class

