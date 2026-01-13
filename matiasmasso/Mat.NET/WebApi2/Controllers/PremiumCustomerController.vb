Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class premiumCustomerController
    Inherits _BaseController

    <HttpGet>
    <Route("api/premiumCustomer/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PremiumCustomer.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la premiumCustomer")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/premiumCustomer")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOPremiumCustomer)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el PremiumCustomer")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If DAL.PremiumCustomerLoader.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.PremiumCustomerLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.PremiumCustomerLoader")
        End Try

        Return result
    End Function



    <HttpPost>
    <Route("api/premiumCustomer/delete")>
    Public Function Delete(<FromBody> value As DTOPremiumCustomer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PremiumCustomer.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la premiumCustomer")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la premiumCustomer")
        End Try
        Return retval
    End Function

End Class

Public Class premiumCustomersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/premiumCustomers/FromPremiumLine/{guid}")>
    Public Function FromPremiumLine(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oPremiumLine As New DTOPremiumLine(guid)
            Dim values = BEBL.PremiumCustomers.All(oPremiumLine)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les premiumCustomers")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/premiumCustomers/FromContact/{guid}")>
    Public Function FromContact(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(guid)
            Dim values = BEBL.PremiumCustomers.All(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les premiumCustomers")
        End Try
        Return retval
    End Function

End Class
