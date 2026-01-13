Public Class DeliveriesController
    Inherits _MatController

    Async Function SingleDelivery(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model = Await FEB.Delivery.Find(guid, exs)
        Return View(Model)
    End Function

    Async Function Tracking(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oDelivery As New DTODelivery(guid)
        Dim model = Await FEB.Delivery.DeliveryWithTracking(exs, oDelivery)
        If exs.Count = 0 Then
            If model Is Nothing Then
                Return Await ErrorNotFoundResult()
            Else
                ViewBag.Title = ContextHelper.Tradueix("Seguimiento de envío", "Seguiment de enviament", "Shipment tracking")
                Return View("Tracking", model)
            End If
        Else
            Return Await ErrorResult(exs)
        End If
    End Function

    Public Async Function Deliveries(customer As Nullable(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim Model As List(Of DTODelivery) = Nothing
        Dim oUser = ContextHelper.GetUser()
        ViewBag.Title = ContextHelper.Tradueix("Mis albaranes", "Els meus albarans", "My deliveries")

        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Dim clientCodis As New List(Of DTOPurchaseOrder.Codis)
            clientCodis.Add(DTOPurchaseOrder.Codis.client)
            clientCodis.Add(DTOPurchaseOrder.Codis.reparacio)
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, user:=oUser, codis:=clientCodis)
                    If exs.Count = 0 Then
                        retval = View(Model)
                    Else
                        retval = View("Error")
                    End If
                Case DTORol.Ids.comercial, DTORol.Ids.rep, DTORol.Ids.salesManager, DTORol.Ids.admin, DTORol.Ids.superUser
                    If customer = Nothing Then
                        Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, user:=oUser, codis:=clientCodis)
                    Else
                        Dim oCustomer As New DTOCustomer(customer)
                        FEB.Contact.Load(oCustomer, exs)
                        Select Case oCustomer.Rol.id
                            Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                                Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, contact:=oCustomer, codis:=clientCodis)
                                retval = View(Model)
                            Case DTORol.Ids.manufacturer
                                Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, contact:=oCustomer, codis:=clientCodis)
                                retval = View(Model)
                        End Select
                    End If
                Case DTORol.Ids.unregistered
                    retval = LoginOrView()
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        Else
            retval = LoginOrView()
        End If
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oContact = Await FEB.Contact.Find(guid, exs)
        Dim oItems As List(Of DTODelivery) = Nothing
        Select Case oContact.Rol.Id
            Case DTORol.Ids.Manufacturer
                Dim prvCodis As New List(Of DTOPurchaseOrder.Codis)
                prvCodis.Add(DTOPurchaseOrder.Codis.Proveidor)
                oItems = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, contact:=oContact, codis:=prvCodis)
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                Dim clientCodis As New List(Of DTOPurchaseOrder.Codis)
                clientCodis.Add(DTOPurchaseOrder.Codis.Client)
                clientCodis.Add(DTOPurchaseOrder.Codis.Reparacio)

                oItems = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, contact:=oContact, codis:=clientCodis)
        End Select

        Dim Model As New List(Of DTODelivery)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= oItems.Count Then Exit For
            Model.Add(oItems(i))
        Next
        Dim retval As PartialViewResult = PartialView("Deliveries_", Model)
        Return retval
    End Function


    Async Function StoreChanged(guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim Model As List(Of DTODelivery) = Nothing
        Dim oUser = ContextHelper.GetUser()
        Dim clientCodis As New List(Of DTOPurchaseOrder.Codis)
        clientCodis.Add(DTOPurchaseOrder.Codis.Client)
        clientCodis.Add(DTOPurchaseOrder.Codis.Reparacio)

        If guid.Equals(System.Guid.Empty) Then
            Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, user:=oUser, codis:=clientCodis)
        Else
            Dim oContact As New DTOContact(guid)
            Model = Await FEB.Deliveries.Headers(exs, Website.GlobalVariables.Emp, codis:=clientCodis, contact:=oContact)
        End If
        Dim retval As PartialViewResult = PartialView("Deliveries_", Model)
        Return retval
    End Function

End Class