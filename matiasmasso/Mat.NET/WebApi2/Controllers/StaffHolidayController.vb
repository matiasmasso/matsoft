Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class StaffHolidayController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffHoliday/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.StaffHoliday.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la StaffHoliday")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/StaffHoliday")>
    Public Function Update(<FromBody> value As DTOStaffHoliday) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffHoliday.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la StaffHoliday")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la StaffHoliday")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/StaffHoliday/delete")>
    Public Function Delete(<FromBody> value As DTOStaffHoliday) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.StaffHoliday.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la StaffHoliday")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la StaffHoliday")
        End Try
        Return retval
    End Function

End Class

Public Class StaffHolidaysController
    Inherits _BaseController

    <HttpGet>
    <Route("api/StaffHolidays/FromEmp/{emp}")>
    Public Function All(emp As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.StaffHolidays.All(oEmp)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffHolidays")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/StaffHolidays/FromStaff/{emp}/{staff}")>
    Public Function All(emp As Integer, staff As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oStaff As New DTOStaff(staff)
            Dim values = BEBL.StaffHolidays.All(oEmp, oStaff)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StaffHolidays")
        End Try
        Return retval
    End Function

End Class

