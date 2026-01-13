Public Class FacturasController
    Inherits _MatController

    Function Index() As ActionResult
        Return View()
    End Function

    Async Function Facturas(Optional customer As Nullable(Of Guid) = Nothing) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            Dim oRaonsSocials As List(Of DTOCustomer) = Nothing
            If customer Is Nothing Then
                oRaonsSocials = Await FEB.Customers.RaonsSocialsWithInvoices(exs, oUser)
            Else
                oRaonsSocials = New List(Of DTOCustomer)
                Dim oCustomer = Await FEB.Customer.Find(exs, customer)
                oRaonsSocials.Add(oCustomer)
            End If
            Dim oBreadCrumb As New DTOBreadCrumb(ContextHelper.Lang())
            oBreadCrumb.AddItem(ContextHelper.Tradueix("Consultas", "Consultes", "Requests"), "/pro")
            oBreadCrumb.AddItem(ContextHelper.Tradueix("Facturas", "Factures", "Invoices"))
            ViewBag.BreadCrumb = oBreadCrumb
            retval = View("Facturas", oRaonsSocials)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If
        Return retval
    End Function

    Function ContactChanged(contact As Guid) As PartialViewResult
        Dim Model As New DTOCustomer(contact)

        Dim retval As PartialViewResult = PartialView("Facturas_", Model)
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim Model As New List(Of DTOInvoice)
        Dim oCustomer As New DTOCustomer(guid)
        Dim oItems = Await FEB.Invoices.Printed(exs, oCustomer)
        Dim indexFrom As Integer = pageindex * pagesize
        For i As Integer = indexFrom To indexFrom + pagesize - 1
            If i >= oItems.Count Then Exit For
            Model.Add(oItems(i))
        Next
        Dim retval As PartialViewResult = PartialView("Facturas__", Model)
        Return retval
    End Function

End Class