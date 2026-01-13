Public Class ProController
    Inherits _MatController

    Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            retval = View()
        Else
            retval = RedirectToAction("Login", "Account")
        End If
        Return retval
    End Function

    Async Function Guid(targetUser As Guid, requestUser As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oRequestUser = Await FEB2.User.Find(requestUser, exs)
        If exs.Count = 0 And oRequestUser IsNot Nothing Then
            Dim oTargetUser = Await FEB2.User.Find(targetUser, exs)
            If exs.Count = 0 And oTargetUser IsNot Nothing Then
                If DTOUser.IsUserAllowedToRead(oRequestUser, oTargetUser) Then
                    Dim oMenuGroups = Await FEB2.MenuItems.Fetch(exs, oTargetUser)
                    If oMenuGroups Is Nothing Then
                        'no s'ha trobat l'usuari amb aquesta cookie
                        ContextHelper.RemoveCookie(ContextHelper.Cookies.User)
                        oTargetUser = Nothing
                        oMenuGroups = FEB2.MenuItems.FetchSync(exs, oTargetUser)
                        retval = View()
                    Else
                        'Session("User") = oUser
                        Await MyBase.SignInUser(oTargetUser, persist:=False)
                        retval = Me.Redirect(DTOWebDomain.Default(False).RootUrl())
                    End If
                    Await ContextHelper.SetNavViewModel(oTargetUser)
                End If
            Else
                retval = Await ErrorNotFoundResult()
            End If
        Else
            retval = Await ErrorNotFoundResult()
        End If

        Return retval
    End Function


    Async Function Retenciones(Optional pageIndex As Integer = 1) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        ViewBag.Title = ContextHelper.Tradueix("Retenciones a cuenta del IRPF", "Retencions a compte de l'IRPF", "Personal Income Tax withholdings")

        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        Select Case MyBase.Authorize(oUser, {DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.Accounts,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.LogisticManager,
                                               DTORol.Ids.Marketing,
                                               DTORol.Ids.Operadora,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Taller})
            Case AuthResults.success
                Dim model = Await FEB2.ContactDocs.All(exs, oUser, DTOContactDoc.Types.Retencions)
                If exs.Count = 0 Then
                    retval = View(model)
                Else
                    retval = Await MyBase.ErrorResult(exs)
                End If
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Public Function eBooks() As ActionResult
        Return LoginOrView()
    End Function

    Function pageindexchanged(returnurl As String, guid As Guid, pageindex As Integer) As PartialViewResult
        ViewBag.Guid = guid
        ViewBag.PageIndex = pageindex

        Dim retval As PartialViewResult = PartialView(returnurl)
        Return retval
    End Function


    Async Function StatQuotaOnline() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oBreadCrumb As New DTOBreadCrumb(ContextHelper.Lang())
        oBreadCrumb.AddItem(ContextHelper.Tradueix("Consultas", "Consultes", "Requests"), "/pro")
        oBreadCrumb.AddItem(ContextHelper.Tradueix("Quota online ",
                                                "Quota online ",
                                                "Online share"))
        ViewBag.BreadCrumb = oBreadCrumb

        Dim oStat As DTOStat = Await GetStat()
        Dim oModel As List(Of DTOStatQuotaOnline) = Await FEB2.StatQuotasOnline.All(exs, oStat.Proveidor)
        Return LoginOrView(oModel)
    End Function

    '-------------------------------------------------------------------- utilities -----------------------------------------------

    Async Function GetStat() As Threading.Tasks.Task(Of DTOStat)
        Dim exs As New List(Of Exception)
        Dim retval As New DTOStat()
        retval.Lang = ContextHelper.Lang()

        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.id = DTORol.Ids.Manufacturer Then
            Dim oContact = Await ContextHelper.Contact(exs)
            If oContact IsNot Nothing Then
                retval.Proveidor = New DTOProveidor(oContact.Guid)
            End If
        End If

        Return retval
    End Function


    Shadows Function LoginOrView(sViewName As String) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            retval = View(sViewName)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Shadows Function LoginOrView(oModel As Object) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            retval = View(oModel)
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnUrl = SelfUrl})
        End If
        Return retval
    End Function

    Shadows Function LoginOrView() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            retval = View(New With {.pageIndex = 1})
        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If
        Return retval
    End Function



End Class