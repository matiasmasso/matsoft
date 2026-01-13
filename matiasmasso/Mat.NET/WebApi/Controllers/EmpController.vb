Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EmpController
    Inherits _BaseController


    <HttpGet>
    <Route("api/emp/{empId}")>
    Public Function Find(empId As Integer) As DTOEmp
        Dim retval As DTOEmp = BEBL.Emp.Find(empId)
        Return retval
    End Function

    <HttpGet>
    <Route("api/emp/value/{emp}/{cod}")>
    Public Function GetEmpValue(emp As Integer, cod As Integer) As String
        Dim oEmp As New DTOEmp(emp)
        Dim retval As String = BEBL.Emp.GetValue(oEmp, cod)
        Return retval
    End Function

    <HttpPost>
    <Route("api/Emp")>
    Public Function Update(<FromBody> oEmp As DTOEmp) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Emp.Update(exs, oEmp) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Emp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Emp")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Emp/Create")>
    Public Function Create(<FromBody> oUser As DTOUser) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Emp.Create(exs, oUser) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Emp")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Emp")
        End Try
        Return retval
    End Function
End Class


Public Class EmpsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/emps/{user}")>
    Public Function Compact(user As Guid) As HttpResponseMessage
        'per Mat.Net
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user) 'per extreure emailAddress
            Dim values = BEBL.Emps.Compact(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les empreses habilitades per l'usuari")
        End Try
        Return retval
    End Function

End Class