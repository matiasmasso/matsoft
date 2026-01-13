Public Class ProductDistributorsController
    Inherits _MatController

    '
    ' GET: /ProductDistributors

    Function Index() As ActionResult
        Return View()
    End Function

    Async Function Csv() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
            Select Case oUser.Rol.id
                Case DTORol.Ids.manufacturer
                    Dim oProveidors = Await FEB.User.BrandManufacturers(exs, oUser)
                    If oProveidors.Count > 0 Then
                        Dim oProveidor As DTOProveidor = oProveidors(0)
                        Dim src As String = Await FEB.ProductDistributors.ManufacturerCsv(oProveidor)
                        HttpContext.Response.AddHeader("content-disposition", "attachment; filename='MATIAS MASSO sale points network.csv'")
                        Dim oStream As Byte() = System.Text.Encoding.Unicode.GetBytes(src)
                        retval = New FileContentResult(oStream, "text/csv")
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select

        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If

        Return retval
    End Function

    Public Async Function RepList(Optional rep As Nullable(Of Guid) = Nothing) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oRep As DTORep = Nothing
        If rep Is Nothing Then
            Dim oUser = ContextHelper.GetUser()
            oRep = Await FEB.User.GetRep(oUser, exs)
        Else
            oRep = Await FEB.Rep.Find(rep, exs)
        End If

        Dim Model = Await FEB.ProductDistributors.FromRep(exs, oRep)
        retval = View(Model)
        Return retval
    End Function

    Public Async Function RepDistribuidoresOficialesList(brandnom As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oBrand = Await FEB.ProductBrand.FromNom(exs, Website.GlobalVariables.Emp, brandnom)
        Dim oUser = ContextHelper.GetUser()

        Dim Model As List(Of DTOProductDistributor) = Await FEB.ProductDistributors.DistribuidorsOficials(exs, oUser, oBrand)
        retval = LoginOrView("RepList", Model, {DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.salesManager, DTORol.Ids.comercial, DTORol.Ids.rep, DTORol.Ids.operadora, DTORol.Ids.manufacturer})
        Return retval
    End Function


    Shadows Function LoginOrView(sView As String, Model As Object, Optional oRols As DTORol.Ids() = Nothing) As ActionResult
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            If oRols Is Nothing Then
                retval = View(sView, Model)
            ElseIf oRols.Contains(oUser.Rol.id) Then
                retval = View(sView, Model)
            Else
                retval = MyBase.UnauthorizedView()
            End If
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