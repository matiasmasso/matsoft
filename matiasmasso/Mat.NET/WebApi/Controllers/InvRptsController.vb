Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class InvRptController
    Inherits _BaseController


    <HttpGet>
    <Route("api/invrpt/raport/{customer}/{sku}/{fch}")>
    Public Function Raport(customer As Guid, sku As Guid, fch As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim format = "yyyyMMddTHHmmss"
            Dim fch2 = DateTime.ParseExact(fch, format, System.Globalization.CultureInfo.InvariantCulture)
            Dim value As String = BEBL.Integracions.Edi.Invrpt.Raport(customer, sku, fch2)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al reportar l'inventari de client")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al reportar l'inventari de client")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/invrpt/delete")>
    Public Function Delete(<FromBody> value As DTO.Integracions.Edi.Invrpt) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Integracions.Edi.Invrpt.Delete(exs, value) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al retrocedir el raport de inventari de client")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al retrocedir el raport de inventari de client")
        End Try
        Return retval
    End Function

End Class
Public Class InvRptsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/invrpts/{holding}/{user}/{fch?}")>
    Public Function Model(holding As Guid, user As Guid, Optional fch As Nullable(Of Date) = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oHolding As New DTOHolding(holding)
            Dim value = BEBL.Integracions.Edi.Invrpts.Model(oHolding, oUser, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el raport de inventari de client")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/invrpts/exceptions")>
    Public Function Exceptions() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.Integracions.Edi.Invrpts.Exceptions()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir les excepcions dels raports de inventari de client")
        End Try
        Return retval
    End Function
End Class
