Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StaffSchedController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffSched/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.StaffSched.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StaffSched")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StaffSched/vigent/FromEmp/{emp}")>
    Public Function VigentFromEmp(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.StaffSched.Vigent(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffScheds")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffSched")>
    Public Function Update(<FromBody> value As DTOStaffSched) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffSched.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la StaffSched")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StaffSched")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffSched/delete")>
    Public Function Delete(<FromBody> value As DTOStaffSched) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffSched.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la StaffSched")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la StaffSched")
        End Try
        Return retval
    End Function

End Class

Public Class StaffSchedsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/StaffScheds/FromEmp/{emp}")>
    Public Function FromEmp(emp As DTOEmp.Ids) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.StaffScheds.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffScheds")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StaffScheds/FromStaff/{staff}")>
    Public Function FromStaff(staff As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oStaff As New DTOStaff(staff)
            Dim values = BEBL.StaffScheds.All(oStaff)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffScheds")
        End Try
        Return retval
    End Function

End Class
