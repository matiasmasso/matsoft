Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaOrderController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaOrder/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaOrder.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaOrder")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaOrder/loadFromSrc")>
    Public Function LoadFromEdiversaFlatFile(<FromBody> src As String) As HttpResponseMessage
        'importa un fitxer pla de texte de Ediversa i retorna un DTOEdiversaOrder per debug
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.EdiversaOrder.Factory(src)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaOrder")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/EdiversaOrder/searchByDocNum")>
    Public Function Find(<FromBody> DocNum As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EdiversaOrder.searchByDocNum(DocNum)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la EdiversaOrder")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/ediversaorder")>
    Public Function Update(<FromBody> value As DTOEdiversaOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaOrder.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la EdiversaOrder")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la EdiversaOrder")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ediversaorder/delete")>
    Public Function Delete(<FromBody> value As DTOEdiversaOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EdiversaOrder.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiversaOrder")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiversaOrder")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversaorder/setresult/{edi}/{pdc}")>
    Public Function SetResult(edi As Guid, pdc As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEdiOrder = BEBL.EdiversaOrder.Find(edi)
            Dim oPdc As New DTOPurchaseOrder(pdc)
            If BEBL.EdiversaOrder.SetResult(exs, oEdiOrder, oPdc) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la EdiversaOrder")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la EdiversaOrder")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ediversaorder/validate")>
    Public Function validateOrder(<FromBody> oOrder As DTOEdiversaOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            BEBL.EdiversaOrder.Validate(exs, oOrder)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al validar la comanda Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al validar la comanda Edi")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/ediversaorder/procesa/{ediversaorder}/{user}")>
    Public Function procesa(ediversaOrder As Guid, user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim exs2 As New List(Of DTOEdiversaException)
            Dim oUser = BEBL.User.Find(user)
            Dim oEdiversaOrder = BEBL.EdiversaOrder.Find(ediversaOrder)
            Dim value = BEBL.EdiversaOrder.Procesa(oEdiversaOrder, oUser, exs2)
            retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al validar la comandes Edi")
        End Try
        Return retval
    End Function


End Class

Public Class EdiversaOrdersController
    Inherits _BaseController


    <HttpGet>
    <Route("api/ediversaorders/headers/{emp}/{year}")>
    Public Function Headers(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = New DTOEmp(emp)
            Dim values = BEBL.EDiversaOrders.headers(oEmp, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les comandes")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ediversaorders/validate")>
    Public Function validateOrders(<FromBody> oOrders As List(Of DTOEdiversaOrder)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim value = BEBL.EDiversaOrders.Validate(oOrders)
            retval = Request.CreateResponse(Of DTOTaskResult)(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al validar les comandes Edi")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ediversaorders/procesa/{user}")>
    Public Function procesa(user As Guid, <FromBody> oOrders As List(Of DTOEdiversaOrder)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim exs2 As New List(Of DTOEdiversaException)
            Dim oUser = BEBL.User.Find(user)
            Dim value = BEBL.EDiversaOrders.Procesa(oOrders, oUser, exs2)
            If exs2.Count = 0 Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, value)
            Else
                For Each ex In exs2
                    exs.Add(New Exception(ex.Msg))
                Next
                retval = MyBase.HttpErrorResponseMessage(exs, "error al validar les comandes Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al validar les comandes Edi")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/ediversaorders/descarta")>
    Public Function Descarta(<FromBody> oOrders As List(Of DTOEdiversaOrder)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.EDiversaOrders.Descarta(exs, oOrders) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al descartar les comandes Edi")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descartar les comandes Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversaorders/ConfirmationPending")>
    Public Function ConfirmationPending() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EDiversaOrders.ConfirmationPending()
            retval = Request.CreateResponse(Of List(Of DTOEdiversaOrder))(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al descartar les comandes Edi")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversaorders/openfiles")>
    Public Function OpenFiles() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.EDiversaOrders.All(OnlyOpenFiles:=True)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les comandes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/ediversaorders/ProcessAllValidated/{user}")>
    Public Function ProcessAllValidated(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim exs As New List(Of Exception)
            Dim value = BEBL.EDiversaOrders.ProcessAllValidated(oUser, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al processar les comandes")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al processar les comandes")
        End Try
        Return retval
    End Function

End Class
