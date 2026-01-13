Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class SpvController
    Inherits _BaseController

    <HttpGet>
    <Route("api/spv/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Spv.find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la reparació")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/spv/{emp}/{year}/{id}")>
    Public Function FromYearId(emp As Integer, year As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value = BEBL.Spv.FromId(oEmp, year, id)
            BEBL.Spv.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la reparació")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Spv")>
    Public Function Update(<FromBody> value As DTOSpv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            value.restoreObjects()
            If BEBL.Spv.Update(value, exs) Then
                retval = Request.CreateResponse(Of DTOSpv)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Spv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Spv")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Spv/delete")>
    Public Function Delete(<FromBody> value As DTOSpv) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Spv.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Spv")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Spv")
        End Try
        Return retval
    End Function

End Class

Public Class SpvsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/spvs/ReadPending/{user}")>
    Public Function ReadPending(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim values As New List(Of DTOSpv)
            Dim oUser = BEBL.User.Find(user)
            If oUser IsNot Nothing Then
                BEBL.Spvs.NotRead(oUser.Emp, values, exs)
            End If
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al llegir les reparacions")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/spvs/ArrivalPending/{user}")>
    Public Function ArrivalPending(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values As New List(Of DTOSpv)
            Dim oUser = BEBL.User.Find(user)
            If oUser IsNot Nothing Then
                values = BEBL.Spvs.ArrivalPending(oUser.Emp)
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/customer/spvs/{customer}")>
    Public Function CustomerSpvs(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values As New List(Of DTOSpv)
            Dim oCustomer = BEBL.Customer.Find(customer)
            If oCustomer IsNot Nothing Then
                values = BEBL.Spvs.All(oCustomer)
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/spvIn/spvs/{spvin}")>
    Public Function FromSpvIn(spvIn As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values As New List(Of DTOSpv)
            Dim oSpvIn = BEBL.SpvIn.Find(spvIn)
            If oSpvIn IsNot Nothing Then
                values = BEBL.Spvs.All(oSpvIn)
            End If
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/spvs/headers/{emp}/{customer}/{year}")>
    Public Function headers(emp As DTOEmp.Ids, customer As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As DTOCustomer = Nothing
            If customer <> Nothing Then oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.Spvs.Headers(oEmp, oCustomer, year:=year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/spvs/openHeaders/{emp}/{customer}")>
    Public Function openHeaders(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As DTOCustomer = Nothing
            If customer <> Nothing Then oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.Spvs.Headers(oEmp, oCustomer, onlyOpen:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les reparacions")
        End Try
        Return retval
    End Function


End Class
