Public Class SkuStocksController
    Inherits _MatController

    Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oBreadCrumb As New DTOBreadCrumb(ContextHelper.Lang())
        oBreadCrumb.AddItem(ContextHelper.Tradueix("Consultas", "Consultes", "Requests"), "/pro")
        oBreadCrumb.AddItem("Stocks")
        ViewBag.BreadCrumb = oBreadCrumb

        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Dim oCatalog As DTOCatalog = Await FEB2.SkuStocks.ForWeb(oUser, exs)
            If exs.Count = 0 Then
                Dim Model As New StocksModel
                Model.Lang = Request.Lang
                Model.User = oUser
                Model.IsSubscribed = Await FEB2.Subscriptor.IsSubscribed(exs, DTOSubscription.Wellknowns.Stocks, oUser)
                Model.Brands = oCatalog
                Select Case oUser.Rol.id
                    Case DTORol.Ids.manufacturer
                        retval = View("ManufacturerStocks", Model)
                    Case Else
                        Dim oArtCustRefs = Await FEB2.ArtCustRefs.FromUser(exs, oUser)
                        If exs.Count = 0 Then
                            If oArtCustRefs.count > 0 Then
                                For Each oBrand In Model.Brands
                                    For Each oCategory In oBrand.Categories
                                        For Each oSku In oCategory.Skus
                                            Dim oSkuGuid = oSku.Guid
                                            Dim oArtCustRef = oArtCustRefs.FirstOrDefault(Function(x) x.Guid.Equals(oSkuGuid))
                                            If oArtCustRef IsNot Nothing Then oSku.CustomRef = oArtCustRef.Nom
                                        Next
                                    Next
                                Next
                            End If
                            retval = View("CustomerStocks", Model)
                        Else
                            retval = Await MyBase.ErrorResult(exs)
                        End If
                End Select

            Else
                retval = Await MyBase.ErrorResult(exs)
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

    Async Function Csv() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
            Dim oCatalog As DTOProductCatalog = Await FEB2.Stocks.ForWeb(oUser, exs)
            Dim oCsv As DTOCsv = DTOProductCatalog.CsvStocks(oCatalog, oUser.Rol) ' FEB2.ProductStocks.Csv(oUser)
            Dim oLang As DTOLang = oUser.lang
            Dim src As String = oCsv.toString()
            HttpContext.Response.AddHeader("content-disposition", "attachment; filename='M+O Referencias en stock.csv'")
            Dim oStream As Byte() = System.Text.Encoding.Unicode.GetBytes(src)
            retval = New FileContentResult(oStream, "text/csv")

        Else
            Dim oContext As ControllerContext = Me.ControllerContext
            Dim actionName As String = oContext.RouteData.Values("action")
            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
            Dim SelfUrl As String = u.Action(actionName)

            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
        End If

        Return retval
    End Function

    Async Function Subscribe(value As Boolean) As Threading.Tasks.Task(Of Boolean)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oSubscription = DTOSubscription.Wellknown(DTOSubscription.Wellknowns.Stocks)
        Dim retval As Boolean
        If value Then
            retval = Await FEB2.Subscription.Subscribe(exs, GlobalVariables.Emp, oSubscription, oUser)
        Else
            retval = Await FEB2.Subscription.UnSubscribe(exs, GlobalVariables.Emp, oSubscription, oUser)
        End If
        Return retval
    End Function

    Async Function Download() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oUser = ContextHelper.GetUser()
        Dim oCatalog As DTOProductCatalog = Await FEB2.Stocks.ForWeb(oUser, exs)
        Dim oSheet As ExcelHelper.Sheet = DTOProductCatalog.ExcelStocks(oCatalog, oUser.Rol)

        If exs.Count = 0 Then
            retval = MyBase.ExcelResult(oSheet)
        Else
            retval = PartialView("Error")
        End If

        Return retval
    End Function
End Class

Public Class StocksModel
    Property Brands As List(Of DTOCatalog.Brand)
    Property Lang As DTOLang
    Property User As DTOUser
    Property IsSubscribed As Boolean
End Class


