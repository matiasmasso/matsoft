Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RepBasketController
    Inherits _BaseController

    <HttpPost>
    <Route("api/repbasket/update")>
    Public Function Update(<FromBody> oOrder As DTOPurchaseOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUsr = BEBL.User.Find(oOrder.UsrLog.usrCreated.Guid)
            If oUsr Is Nothing OrElse oUsr.Rol.id = DTORol.Ids.denied Then
                retval = MyBase.HttpErrorResponseMessage(exs, "usuario no autorizado")
            Else
                oOrder.Emp = oUsr.Emp
                If BEBL.RepBasket.update(exs, oOrder) Then
                    Dim value = oOrder.Num
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, oUsr.Lang.Tradueix("error al registrar el pedido", "error al desar la comanda", "error on saving the order"))
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la comanda")
        End Try
        Return retval
    End Function

End Class
