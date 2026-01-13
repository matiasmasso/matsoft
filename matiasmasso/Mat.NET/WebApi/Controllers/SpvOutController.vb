
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Namespace Controllers
    Public Class SpvOutController
        Inherits _BaseController

        <HttpPost>
        <Route("api/spvout/{emp}")>
        Public Function Update(emp As Integer, <FromBody> oSpvOut As DTOSpvOut) As HttpResponseMessage
            Dim retval As HttpResponseMessage = Nothing
            Try
                Dim oEmp = GlobalVariables.Emps.FirstOrDefault(Function(x) x.Id = emp)
                Dim oDelivery As DTODelivery = BEBL.Delivery.Factory(oEmp, oSpvOut)

                Dim exs As New List(Of Exception)
                If BEBL.Delivery.Update(oDelivery, exs) Then
                    oSpvOut.Delivery = oDelivery
                    retval = Request.CreateResponse(Of DTOSpvOut)(HttpStatusCode.OK, oSpvOut)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error de servidor al generar l'albarà")
                End If

            Catch ex As Exception
                retval = MyBase.HttpErrorResponseMessage(ex, "error de servidor al generar l'albarà")
            End Try
            Return retval
        End Function

    End Class
End Namespace
