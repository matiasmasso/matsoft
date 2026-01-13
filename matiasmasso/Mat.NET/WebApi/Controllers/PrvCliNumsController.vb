Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PrvCliNumsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/prvclinums/{proveidor}")>
    Public Function All(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor = BEBL.Proveidor.Find(proveidor)
            If oProveidor IsNot Nothing Then
                Dim values = BEBL.PrvCliNums.All(oProveidor)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les PrvCliNums")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/prvclinums/{proveidor}/{clinum}")>
    Public Function GetCustomer(proveidor As Guid, clinum As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor = BEBL.Proveidor.Find(proveidor)
            If oProveidor IsNot Nothing Then
                Dim value = BEBL.PrvCliNum.Customer(oProveidor, clinum)
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PrvCliNum")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/prvclinums/{proveidor}")>
    Public Function Update(ByVal proveidor As Guid, <FromBody> ByVal oPrvCliNums As List(Of DTOPrvCliNum)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oProveidor = BEBL.Proveidor.Find(proveidor)
            If oProveidor Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage(exs, "proveidor no reconegut al desar els PrvCliNum")
            Else
                If BEBL.PrvCliNums.Update(oProveidor, oPrvCliNums, exs) Then
                    retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar els PrvCliNum")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar els PrvCliNum")
        End Try
        Return retval
    End Function
End Class
