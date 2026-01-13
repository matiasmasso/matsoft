Public Class WtbolController
    Inherits _MatController

    Public Function Index() As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = MyBase.User()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.cliFull, DTORol.Ids.cliLite
                    retval = View()
                Case Else
                    retval = UnauthorizedView()
            End Select
        Else
            retval = LoginOrView()
        End If
        Return retval
    End Function


    <HttpGet>
    Public Async Function LandingPages(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model = Await FEB.Product.Find(exs, guid)
        Return View(Model)
    End Function

    <HttpGet>
    Public Async Function Site(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oModel As New DTOWtbolSiteModel
        oModel.Site = Await FEB.WtbolSite.Find(exs, guid)
        oModel.Cataleg = Await FEB.Cataleg.FromContact(oModel.Site.Customer, exs)
        Return View("Site", oModel)
    End Function

    Public Async Function LandingPage(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLandingPage = Await FEB.WtbolLandingPage.Find(exs, guid)
        Dim myData As Object
        If oLandingPage Is Nothing Then
            myData = New With {.success = False}
        Else
            Await FEB.WtbolCtr.Log(exs, oLandingPage, Request.UserHostName)
            myData = New With {.success = True, .url = oLandingPage.Uri.AbsoluteUri}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


    <HttpPost>
    Public Async Function ClickThroughLog(landingpage As Guid) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim oLandingPage = Await FEB.WtbolLandingPage.Find(exs, landingpage)
        Dim myData As Object
        If oLandingPage Is Nothing Then
            myData = New With {.success = False}
        Else
            If Not MatHelperStd.UrlHelper.IsOurOwnIp(Request.UserHostName) Then
                Await FEB.WtbolCtr.Log(exs, oLandingPage, Request.UserHostName)
            End If
            myData = New With {.success = True, .url = oLandingPage.Uri.AbsoluteUri}
        End If
        Dim retval As JsonResult = Json(myData, JsonRequestBehavior.AllowGet)
        Return retval

    End Function


    <HttpGet>
    Public Async Function Baskets(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim Model = Await FEB.WtbolSite.Find(exs, guid)
        If exs.Count = 0 Then
            Model.Baskets = Await FEB.WtbolBaskets.All(exs, Model)
            If exs.Count = 0 Then
                retval = View(Model)
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function Excel(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oSite = Await FEB.WtbolSite.Find(exs, guid)
        Dim oCataleg = Await FEB.Cataleg.CustomerBasicTree(exs, oSite.Customer, ContextHelper.Lang)
        Dim oSheet = DTOWtbolSite.ExcelLandingPages(oCataleg, oSite, ContextHelper.Lang, exs)
        Return Await LoginOrFile(oSheet)
    End Function

End Class

