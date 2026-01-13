Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StaffCategoryController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffCategory/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.StaffCategory.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StaffCategory")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/StaffCategory")>
    Public Function Update(<FromBody> value As DTOStaffCategory) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffCategory.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la StaffCategory")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StaffCategory")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffCategory/delete")>
    Public Function Delete(<FromBody> value As DTOStaffCategory) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffCategory.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la StaffCategory")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la StaffCategory")
        End Try
        Return retval
    End Function

End Class

Public Class StaffCategoriesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffCategories")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.StaffCategories.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffCategories")
        End Try
        Return retval
    End Function

End Class
