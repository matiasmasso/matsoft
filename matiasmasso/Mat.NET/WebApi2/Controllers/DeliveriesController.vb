Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class DeliveryController
    Inherits _BaseController

    <HttpPost>
    <Route("api/delivery")>
    Public Function Delivery(oDelivery As DTODelivery) As DTODelivery
        BEBL.Delivery.Load(oDelivery)
        Return oDelivery
    End Function

    <HttpGet>
    <Route("api/delivery/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Delivery.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'albarà")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/delivery/withtracking/{guid}")>
    Public Async Function withtracking(guid As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oDelivery = BEBL.Delivery.Find(guid)
            Dim value = Await BEBL.VivaceTracking.Fetch(exs, oDelivery)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'albarà")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/delivery/fromNum/{emp}/{year}/{id}")>
    Public Function FromNum(emp As Integer, year As Integer, id As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim value As DTODelivery = BEBL.Delivery.FromNum(oEmp, year, id)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir l'albarà")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Delivery/Update")>
    Public Function Update(<FromBody> value As DTODelivery) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Delivery.Update(value, exs) Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, value.Id)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar l'albarà")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar l'albarà")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Delivery/delete/{showWarnings}")>
    Public Function Delete(showWarnings As Integer, <FromBody> value As DTODelivery) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Delivery.isAllowedToDelete(exs, value) Then
                If BEBL.Delivery.Delete(exs, value) Then
                    retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'albarà")
                End If
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar l'albarà")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar l'albarà")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/Delivery/cobraPerVisa/{emp}")>
    Public Function cobraPerVisa(emp As DTOEmp.Ids, <FromBody> value As DTOTpvLog) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            If BEBL.Delivery.CobraPerVisa(oEmp, value, exs) Then
                retval = Request.CreateResponse(Of DTOTpvLog)(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al cobrar l'albarà per Visa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cobrar l'albarà per Visa")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Delivery/cobraPerTransferencia")>
    Public Function CobraPerTransferencia(<FromBody> value As DTOCobramentPerTransferencia) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oCca = BEBL.Delivery.CobraPerTransferenciaPrevia(exs, value)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of DTOCca)(HttpStatusCode.OK, oCca)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al cobrar l'albarà per transferencia")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al cobrar l'albarà per transferencia")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/delivery/UpdateJustificante/{delivery}/{cod}")>
    Public Function UpdateJustificante(delivery As Guid, cod As DTODelivery.JustificanteCodes) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDelivery As New DTODelivery(delivery)
            If BEBL.Delivery.UpdateJustificante(exs, oDelivery, cod) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al registrar el justificant")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al registrar el justificant")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/delivery/Total/{delivery}")>
    Public Function Total(delivery As Guid) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDelivery As New DTODelivery(delivery)
            Dim value = BEBL.Delivery.Total(oDelivery)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el total de l'albarà")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/delivery/SetCurExchangeRate/{delivery}/{cur}")>
    Public Function SetCurExchangeRate(delivery As Guid, cur As String, <FromBody> oRate As DTOCurExchangeRate) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oDelivery As New DTODelivery(delivery)
            Dim oCur = DTOCur.Factory(cur)
            If BEBL.Delivery.SetCurExchangeRate(exs, oDelivery, oCur, oRate) Then
                retval = Request.CreateResponse(HttpStatusCode.OK)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al aplicar la divisa")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al aplicar la divisa")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/delivery/pdf/{delivery}/{proforma}")>
    Public Function pdf(delivery As Guid, proforma As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oDelivery As New DTODelivery(delivery)
            Dim oPdfStream = BEBL.Delivery.PdfStream(oDelivery, (proforma = 1), exs)
            If exs.Count = 0 Then
                retval = MyBase.HttpPdfResponseMessage(oPdfStream, DTODelivery.FileName(oDelivery))
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al generar el pdf de l'albarà")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al generar el pdf de l'albarà")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/delivery/mailtoSubscriptors/{emp}/{delivery}")>
    Public Async Function mailtoSubscriptors(emp As Integer, delivery As Guid) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oDelivery = BEBL.Delivery.Find(delivery)
            Dim exs As New List(Of Exception)
            If Await BEBL.Delivery.MailToSubscriptors(exs, oEmp, oDelivery) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al enviar l'albarà per email")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al enviar l'albarà per email")
        End Try
        Return retval
    End Function

End Class

Public Class DeliveriesController
    Inherits _BaseController


    <HttpGet>
    <Route("api/deliveries/years/{emp}/{contact}")>
    Public Function Years(emp As DTOEmp.Ids, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact = DTOBaseGuid.opcional(Of DTOContact)(contact)
            Dim values = BEBL.Deliveries.Years(oEmp, oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els anys dels albarans")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/deliveries/fromContact/{contact}")>
    Public Function fromContact(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim oEmp As New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim values = BEBL.Deliveries.Headers(oEmp, contact:=oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/deliveries/headers/{emp}/{user}/{contact}/{group}/{year}/{pendentsDeCobro}/{altresPorts}")>
    Public Function headers(emp As DTOEmp.Ids, user As Guid, contact As Guid, group As Integer, year As Integer, pendentsDeCobro As DTODelivery.RetencioCods, altresPorts As Integer, <FromBody> codis As List(Of DTOPurchaseOrder.Codis)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oUser = DTOBaseGuid.opcional(Of DTOUser)(user)
            If oUser IsNot Nothing Then BEBL.User.Load(oUser)
            Dim oContact = DTOBaseGuid.opcional(Of DTOContact)(contact)
            Dim values = BEBL.Deliveries.Headers(oEmp, oUser, oContact, (group = 1), codis, year, pendentsDeCobro, (altresPorts = 1))
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/deliveries/{emp}/{year}")>
    Public Function PendentsDeTransmetre(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oExercici As New DTOExercici(oEmp, year)
            Dim values = BEBL.Deliveries.All(oExercici)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans de l'exercici")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/fromInvoice/{invoice}")>
    Public Function FromInvoice(invoice As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oInvoice As New DTOInvoice(invoice)
            Dim values = BEBL.Deliveries.All(oInvoice)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans d'una factura")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/deliveries/fromUser/{user}")> ' compact per iMat 3.0
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.Deliveries.Headers(user:=oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/fromCustomer/{customer}")> ' compact per iMat 3.0
    Public Function FromCustomer(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.Deliveries.Headers(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/fromEmp/{emp}/{year}")> ' compact per iMat 3.0 + Mat.Net TO DEPRECATE
    Public Function FromEmp(emp As DTOEmp.Ids, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp As New DTOEmp(emp)
            'Dim values = BEBL.Deliveries.Headers(oEmp, year) verificar si IOS segueix funcionant
            Dim values = BEBL.Deliveries.Minified(oEmp, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/deliveries/fromArea/{emp}/{area}")>
    Public Function fromArea(emp As DTOEmp.Ids, area As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oArea As New DTOArea(area)
            Dim values = BEBL.Deliveries.All(oEmp, oArea)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans d'un area")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/last/{contact}")>
    Public Function last(contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Deliveries.Last(oContact)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el darrer albarà de un client")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/centros/{user}")>
    Public Function Centros(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser As New DTOUser(user)
            Dim values = BEBL.Deliveries.Centros(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els centres de un usuari")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/deliveries/update")>
    Public Function Update(<FromBody> oDeliveries As List(Of DTODelivery)) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.Deliveries.Update(exs, oDeliveries) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, oDeliveries)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al desar els albarans")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al desar els albarans")
        End Try
        Return retval
    End Function






    <HttpPost>
    <Route("api/deliveries/pdf/{valorat}")>
    Public Function pdf(valorat As DTODelivery.CodsValorat, <FromBody> oDeliveries As List(Of DTODelivery)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            For Each oDelivery In oDeliveries
                oDelivery.RestoreObjects()
            Next
            Dim oPdfStream = BEBL.Deliveries.PdfStream(exs, oDeliveries, False, valorat)
            If exs.Count = 0 Then
                retval = MyBase.HttpPdfResponseMessage(oPdfStream, DTODelivery.FileName(oDeliveries, False))
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al generar el pdf dels albarans")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al generar el pdf dels albarans")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/deliveries/pdf/proforma")>
    Public Function proformaPdf(<FromBody> oDeliveries As List(Of DTODelivery)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oPdfStream = BEBL.Deliveries.PdfStream(exs, oDeliveries, True)
            If exs.Count = 0 Then
                retval = MyBase.HttpPdfResponseMessage(oPdfStream, DTODelivery.FileName(oDeliveries, True))
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al generar el pdf dels albarans")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al generar el pdf dels albarans")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/deliveries/pendentsDeTransmetre/{mgz}")>
    Public Function PendentsDeTransmetre(mgz As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oMgz As New DTOMgz(mgz)
            Dim values = BEBL.Deliveries.PendentsDeTransmetre(oMgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans pendents de transmetre")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/pendentsDeFacturar/{emp}/{customer}")>
    Public Function PendentsDeFacturar(emp As DTOEmp.Ids, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oCustomer As DTOCustomer = Nothing
            If customer <> Nothing Then oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.Deliveries.PendentsDeFacturar(oEmp, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans pendents de transmetre")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/IntrastatPending/{emp}/{year}/{month}")>
    Public Function IntrastatPending(emp As Integer, year As Integer, month As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oYearMonth As New DTOYearMonth(year, month)
            Dim values = BEBL.Deliveries.IntrastatPending(oEmp, oYearMonth)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans pendents de transmetre")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/deliveries/Entrades/{proveidor}")>
    Public Function Entrades(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.Deliveries.Entrades(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les entrades")
        End Try
        Return retval
    End Function



    <HttpGet>
    <Route("api/deliveries/NumsToRecycle/{emp}/{fch}")>
    Public Function NumsToRecycle(emp As Integer, fch As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oEmp = MyBase.GetEmp(emp)
            Dim values = BEBL.Deliveries.NumsToRecycle(oEmp, fch)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els albarans pendents de transmetre")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/deliveries/rezip/{zipTo}")>
    Public Function reZip(zipTo As Guid, <FromBody> deliveries() As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oZip As New DTOZip(zipTo)
            Dim oDeliveries As New List(Of DTODelivery)
            For Each guid In deliveries
                oDeliveries.Add(New DTODelivery(guid))
            Next
            Dim value = BEBL.Deliveries.reZip(exs, oZip, oDeliveries)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els centres de un usuari")
        End Try
        Return retval
    End Function
End Class
