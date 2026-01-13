Public Class PurchaseOrderController
    Inherits _MatController

    '
    ' GET: /PurchaseOrder

    Function Index() As ActionResult
        Return View()
    End Function

    Async Function SingleOrder(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oOrder = Await FEB2.PurchaseOrder.Find(guid, exs)
        If exs.Count = 0 Then
            Return View(oOrder)
        Else
            Return View("Error")
        End If
    End Function

    Public Async Function PurchaseOrderItem(guid As Guid, lin As Integer) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oOrder = Await FEB2.PurchaseOrder.Find(guid, exs)
        If oOrder IsNot Nothing Then
            Dim item As DTOPurchaseOrderItem = oOrder.items.Find(Function(x) x.lin = lin)
            If item IsNot Nothing Then
                item.deliveries = Await FEB2.PurchaseOrderItem.DeliveryItems(exs, item)
                retval = View(item)
            End If
        End If
        Return retval
    End Function

    Public Async Function RepSortides(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oOrder = Await FEB2.PurchaseOrder.OrderWithDeliveryItems(exs, guid)
        If oOrder IsNot Nothing Then
            retval = View("RepSortides", oOrder)
        End If
        Return retval
    End Function

    Public Async Function FromContact(customer As Guid) As Threading.Tasks.Task(Of ActionResult)
        Return Await PurchaseOrders(New DTOContact(customer), ContextHelper.GetUser())
    End Function

    Public Async Function FromUser() As Threading.Tasks.Task(Of ActionResult)
        Return Await PurchaseOrders(Nothing, ContextHelper.GetUser())
    End Function

    Public Async Function PurchaseOrders(oContact As DTOContact, oUser As DTOUser) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim Model As List(Of DTOPurchaseOrder) = Nothing
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    If oContact Is Nothing Then oContact = Await ContextHelper.Contact(exs)
                    If exs.Count = 0 Then
                        Model = Await FEB2.PurchaseOrders.Headers(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.proveidor, oContact, Year:=Today.Year)
                        Model = Model.Where(Function(x) x.Hide = False).ToList()
                        If exs.Count = 0 Then
                            retval = View("PurchaseOrders", Model)
                        Else
                            Return View("Error")
                        End If
                    Else
                        Return View("Error")
                    End If
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    If oContact Is Nothing Then oContact = Await ContextHelper.Contact(exs)
                    If exs.Count = 0 Then
                        Model = Await FEB2.PurchaseOrders.All(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.client, 0, oContact)
                        If exs.Count = 0 Then
                            retval = View("PurchaseOrders", Model)
                        Else
                            Return View("Error")
                        End If
                    Else
                        Return View("Error")
                    End If

                Case DTORol.Ids.comercial, DTORol.Ids.rep, DTORol.Ids.salesManager, DTORol.Ids.admin, DTORol.Ids.superUser
                    oContact = Await FEB2.Contact.Find(oContact.Guid, exs)
                    Select Case oContact.Rol.id
                        Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                            Model = Await FEB2.PurchaseOrders.All(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.client, 0, oContact)
                            If exs.Count = 0 Then
                                retval = View("PurchaseOrders", Model)
                            Else
                                Return View("Error")
                            End If
                        Case DTORol.Ids.manufacturer
                            Model = Await FEB2.PurchaseOrders.All(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.proveidor, 0, oContact)
                            If exs.Count = 0 Then
                                retval = View("PurchaseOrders", Model)
                            Else
                                Return View("Error")
                            End If
                    End Select
            End Select
        Else
            retval = LoginOrView()
        End If
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oContact = Await FEB2.Contact.Find(guid, exs)
        Dim oItems As List(Of DTOPurchaseOrder) = Nothing
        Select Case oContact.Rol.Id
            Case DTORol.Ids.Manufacturer
                oItems = Await FEB2.PurchaseOrders.All(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.Proveidor, 0, oContact)
            Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                oItems = Await FEB2.PurchaseOrders.All(exs, GlobalVariables.Emp, DTOPurchaseOrder.Codis.Client, 0, oContact)
        End Select

        Dim Model As New List(Of DTOPurchaseOrder)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= oItems.Count Then Exit For
            Model.Add(oItems(i))
        Next
        Dim retval As PartialViewResult = PartialView("PurchaseOrders_", Model)
        Return retval
    End Function

    Async Function CustomerPending(Optional customer As Nullable(Of Guid) = Nothing) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model As List(Of DTOPurchaseOrderItem) = Nothing

        If customer Is Nothing Then
            Dim oUser = ContextHelper.GetUser()
            If oUser Is Nothing Then
            Else
                Select Case oUser.Rol.Id
                    Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                        Model = Await FEB2.PurchaseOrderItems.Pending(exs, oUser, DTOPurchaseOrder.Codis.Client, GlobalVariables.Emp.Mgz)
                    Case DTORol.Ids.Manufacturer
                        Model = Await FEB2.PurchaseOrderItems.Pending(exs, oUser, DTOPurchaseOrder.Codis.Proveidor, GlobalVariables.Emp.Mgz)
                End Select
            End If
        Else
            Dim oCustomer = Await FEB2.Customer.Find(exs, customer)
            Select Case oCustomer.Rol.Id
                Case DTORol.Ids.CliFull, DTORol.Ids.CliLite
                    Model = Await FEB2.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.Client, GlobalVariables.Emp.Mgz)
                Case DTORol.Ids.Manufacturer
                    Model = Await FEB2.PurchaseOrderItems.Pending(exs, oCustomer, DTOPurchaseOrder.Codis.Proveidor, GlobalVariables.Emp.Mgz)
            End Select
        End If
        Return LoginOrView(, Model)
    End Function
End Class