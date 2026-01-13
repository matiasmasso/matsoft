Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class SalesQueryController
    Inherits _BaseController


    <HttpPost>
    <Route("api/SalesQuery")>
    Public Function Diari(<FromBody> value As DTOSalesQuery) As HttpResponseMessage 'iMat
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            BEBL.User.Load(value.User)
            Dim items = BEBL.CompactDiari.Items(exs, value)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of List(Of DTOSalesQuery.Item))(HttpStatusCode.OK, items)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al carregar el diari")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al carregar el diari")
        End Try
        Return retval
    End Function



End Class
