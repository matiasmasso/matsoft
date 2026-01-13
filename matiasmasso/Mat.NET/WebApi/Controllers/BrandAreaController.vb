Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class BrandAreaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BrandArea/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.BrandArea.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la BrandArea")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BrandArea")>
    Public Function Update(<FromBody> value As DTOBrandArea) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BrandArea.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la BrandArea")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la BrandArea")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/BrandArea/delete")>
    Public Function Delete(<FromBody> value As DTOBrandArea) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.BrandArea.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la BrandArea")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la BrandArea")
        End Try
        Return retval
    End Function

End Class

Public Class BrandAreasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/BrandAreas/{brand}")>
    Public Function All(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values = BEBL.BrandAreas.All(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les BrandAreas")
        End Try
        Return retval
    End Function

End Class

