Public Class RaportsController
    Inherits _MatController

    Private _PageSize As Integer = 5

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Informe de visita comercial", "Raport de visita comercial", "Sales visit report")
        ViewData("PageSize") = _PageSize

        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep})
            Case AuthResults.success
                Select Case oUser.Rol.id
                    Case DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.SalesManager
                        Dim Model As New DTOMem
                        retval = View("Raport", Model)
                    Case Else
                        Dim Model As List(Of DTOMem) = Await FEB2.Mems.All(exs, oCod:=DTOMem.Cods.Rep, oUser:=oUser, pagesize:=_PageSize, onlyfromLast24H:=True)
                        ViewData("RecordsCount") = Await FEB2.Mems.Count(DTOMem.Cods.Rep, oUser, exs)
                        retval = View("Raports", Model)
                End Select

            Case AuthResults.login
                retval = MyBase.Login
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView
        End Select

        Return retval
    End Function

    Async Function Raport(Optional Guid As Guid = Nothing) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Informe de visita comercial", "Raport de visita comercial", "Sales visit report")
        ViewData("PageSize") = _PageSize

        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep})
            Case AuthResults.success
                Select Case oUser.Rol.id
                    Case DTORol.Ids.Rep, DTORol.Ids.Comercial, DTORol.Ids.SalesManager
                        Dim Model = Await FEB2.Mem.Find(Guid, exs)
                        If exs.Count = 0 Then
                            retval = View("Raport", Model)
                        Else
                            retval = View("Error")
                        End If
                    Case Else
                        Dim Model = Await FEB2.Mems.All(exs, oCod:=DTOMem.Cods.Rep, oUser:=oUser, pagesize:=_PageSize, onlyfromLast24H:=True)
                        ViewData("RecordsCount") = Await FEB2.Mems.Count(DTOMem.Cods.Rep, oUser, exs)
                        retval = View("Raports", Model)
                End Select

            Case AuthResults.login
                retval = MyBase.Login
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView
        End Select

        Return retval
    End Function

    Async Function forCustomer(customer As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        ViewBag.Title = Mvc.ContextHelper.Tradueix("Informe de visita comercial", "Raport de visita comercial", "Sales visit report")
        ViewData("PageSize") = _PageSize

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep})
            Case AuthResults.success
                Dim Model As New DTOMem
                Model.Contact = Await FEB2.Contact.Find(customer, exs)
                retval = View("Raport", Model)

            Case AuthResults.login
                retval = MyBase.Login
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView
        End Select

        Return retval
    End Function

    Async Function customerRaports(customer As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Select Case MyBase.Authorize({DTORol.Ids.SuperUser, DTORol.Ids.Admin, DTORol.Ids.SalesManager, DTORol.Ids.Comercial, DTORol.Ids.Rep})
            Case AuthResults.success
                Dim Model = Await FEB2.Contact.Find(customer, exs)
                retval = View(Model)

            Case AuthResults.login
                retval = MyBase.Login
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView
        End Select

        Return retval
    End Function

    Async Function Atlas() As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim myData As List(Of DTOGuidNode) = Await FEB2.RepProducts.Customers(exs, oUser)
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Update(customer As Guid, text As String, fch As Date) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oMem As New DTOMem
        With oMem
            .UsrLog = DTOUsrLog2.Factory(oUser)
            .fch = fch
            .cod = DTOMem.Cods.Rep
            .Contact = New DTOGuidNom(customer)
            .text = text
        End With

        Dim myData As Object
        If Await FEB2.Mem.Update(exs, oMem) Then
            myData = New With {.resultcod = "1"}
        Else
            myData = New With {.resultcod = "2"}
            myData.resultmsg = ExceptionsHelper.ToFlatString(exs)
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Async Function Archive(customer As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oContact As New DTOContact(customer)
        Dim oUser = ContextHelper.GetUser()
        Dim oLast3Mems As List(Of DTOMem) = Await FEB2.Mems.All(exs, oContact:=oContact, oCod:=DTOMem.Cods.Rep, oUser:=oUser, pagesize:=_PageSize)
        Dim retval As PartialViewResult = PartialView("_RaportArxive", oLast3Mems)
        Return retval
    End Function

    Async Function JobDone() As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oMems As List(Of DTOMem) = Await FEB2.Mems.All(exs, oCod:=DTOMem.Cods.Rep, oUser:=oUser, pagesize:=_PageSize, onlyfromLast24H:=True)
        Dim retval As PartialViewResult = PartialView("_RaportJobDone", oMems)
        Return retval
    End Function

    Async Function pageindexchanged(pageindex As Integer, pagesize As Integer, guid As Guid) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim offset As Integer = pageindex * pagesize
        Dim oUser = ContextHelper.GetUser()
        Dim Model As List(Of DTOMem) = Await FEB2.Mems.All(exs, oCod:=DTOMem.Cods.Rep, oUser:=oUser, offset:=offset, pagesize:=pagesize)
        Dim retval As PartialViewResult = PartialView("Raports_", Model)
        Return retval
    End Function
End Class
