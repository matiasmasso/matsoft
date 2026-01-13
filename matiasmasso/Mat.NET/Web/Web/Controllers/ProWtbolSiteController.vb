Public Class ProWtbolSiteController
    Inherits _MatController

    Public Async Function Index(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim model = Await FEB2.WtbolSite.Find(exs, id)
                    If exs.Count = 0 Then 'filtrar per usuari autoritzat
                        If model Is Nothing Then
                            retval = Await ErrorResult("Wtbol with Guid '" & id.ToString & "' Not found")
                        Else
                            ViewBag.Title = String.Format("Wtbol {0}", model.Nom)
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            With ViewBag.SideMenuItems
                                .add(New DTOMenuItem("", ""))
                                .add(New DTOMenuItem(model.Nom, ""))
                                .add(New DTOMenuItem(Tradueix("Ficha", "Fitxa", "Properties"), "/pro/proWtbolSite/Index/" & model.Guid.ToString))
                                .add(New DTOMenuItem("LandingPages", "/pro/proWtbolSite/LandingPages/" & model.Guid.ToString))
                                .add(New DTOMenuItem("Stocks", "/pro/proWtbolSite/Stocks/" & model.Guid.ToString))
                            End With
                            retval = View("Site", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function UpdateSite(model As DTOWtbolSite) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim result As Object = Nothing
        Try
            If Await FEB2.WtbolSite.Update(model, exs) Then
                result = New With {.success = True}
            Else
                result = New With {.success = False, .reason = ExceptionsHelper.ToFlatString(exs)}
            End If
        Catch ex As Exception
            result = New With {.success = False, .reason = ex.Message}
        End Try
        Dim retval As JsonResult = Json(result, JsonRequestBehavior.AllowGet)
        Return retval
    End Function


    Public Async Function LandingPages(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim model = Await FEB2.WtbolSite.Find(exs, id)
                    If exs.Count = 0 Then 'filtrar per usuari autoritzat
                        If model Is Nothing Then
                            retval = Await ErrorResult("Wtbol site With Guid '" & id.ToString & "' Not found")
                        Else
                            ViewBag.Title = String.Format("Wtbol LandingPages {0}", model.Nom)
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            With ViewBag.SideMenuItems
                                .add(New DTOMenuItem("", ""))
                                .add(New DTOMenuItem(model.Nom, ""))
                                .add(New DTOMenuItem(Tradueix("Ficha", "Fitxa", "Properties"), "/pro/proWtbolSite/LandingPage/" & model.Guid.ToString))
                                .add(New DTOMenuItem(Tradueix("Añadir Landing Page", "Afegir Landing Page", "Add new Landing Page"), "/pro/proWtbolSite/LandingPageFactory/" & model.Guid.ToString))
                                .add(New DTOMenuItem("Excel", "/pro/proWtbolSite/LandingPagesUpload/" & model.Guid.ToString))
                                .add(New DTOMenuItem("Stocks", "/pro/proWtbolSite/Stocks/" & model.Guid.ToString))
                            End With
                            retval = View("LandingPages", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function LandingPage(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim oLandingPage = Await FEB2.WtbolLandingPage.Find(exs, id)
                    If exs.Count = 0 Then 'filtrar per usuari autoritzat
                        If oLandingPage Is Nothing Then
                            retval = Await ErrorResult("Landing page with Guid '" & id.ToString & "' Not found")
                        Else
                            Dim model = oLandingPage.model()
                            ViewBag.Title = String.Format("Wtbol LandingPage de {0}", oLandingPage.Site.Nom)
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            With ViewBag.SideMenuItems
                                .add(New DTOMenuItem("", ""))
                                .add(New DTOMenuItem(oLandingPage.Site.Nom, ""))
                                .add(New DTOMenuItem("LandingPages", "/pro/proWtbolSite/LandingPages/" & oLandingPage.Site.Guid.ToString))
                                .add(New DTOMenuItem("Stocks", "/pro/proWtbolSite/Stocks/" & oLandingPage.Site.Guid.ToString))
                            End With

                            retval = View("LandingPage", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function


    Public Async Function LandingPageFactory(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        If oUser Is Nothing Then
            retval = MyBase.UnauthorizedView()
        Else
            Select Case oUser.Rol.id
                Case DTORol.Ids.superUser, DTORol.Ids.admin, DTORol.Ids.marketing, DTORol.Ids.logisticManager, DTORol.Ids.taller
                    Dim oSite = Await FEB2.WtbolSite.Find(exs, id)
                    If exs.Count = 0 Then 'filtrar per usuari autoritzat
                        If oSite Is Nothing Then
                            retval = Await ErrorResult("WtbolSite with Guid '" & id.ToString & "' Not found")
                        Else
                            oSite.LandingPages = Nothing 'prevent circular references
                            Dim model = DTOWtbolLandingPage.Factory(oSite, oUser).model()
                            ViewBag.Title = String.Format("Wtbol LandingPage {0}", oSite.Nom)
                            ViewBag.SideMenuItems = MyBase.SideMenuItems()
                            retval = View("LandingPage", model)
                        End If
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                Case Else
                    retval = MyBase.UnauthorizedView()
            End Select
        End If
        Return retval
    End Function

    Public Async Function UpdateLandingPage(model As DTOWtbolLandingPage.LandingPageModel) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim result As Object = Nothing
        Try
            Dim oLandingPage = DTOWtbolLandingPage.Factory(model)

            If Await FEB2.WtbolLandingPage.Update(exs, oLandingPage) Then
                result = New With {.success = True}
            Else
                result = New With {.success = False, .reason = ExceptionsHelper.ToFlatString(exs)}
            End If
        Catch ex As Exception
            result = New With {.success = False, .reason = ex.Message}
        End Try
        Dim retval As JsonResult = Json(result, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Public Async Function LandingPageStatus(id As Guid, cod As String) As Threading.Tasks.Task(Of JsonResult)
        Dim exs As New List(Of Exception)
        Dim result As Object = Nothing
        Try
            Dim oLandingPage = Await FEB2.WtbolLandingPage.Find(exs, id)
            If exs.Count = 0 Then
                oLandingPage.Status = DirectCast([Enum].Parse(GetType(DTOWtbolLandingPage.StatusEnum), cod), DTOWtbolLandingPage.StatusEnum)
                oLandingPage.UsrStatus = MyBase.User
                oLandingPage.FchStatus = Now
                If Await FEB2.WtbolLandingPage.Update(exs, oLandingPage) Then
                    result = New With {.success = True}
                Else
                    result = New With {.success = False, .reason = ExceptionsHelper.ToFlatString(exs)}
                End If
            Else
                result = New With {.success = False, .reason = ExceptionsHelper.ToFlatString(exs)}
            End If

        Catch ex As Exception
            result = New With {.success = False, .reason = ex.Message}
        End Try
        Dim retval As JsonResult = Json(result, JsonRequestBehavior.AllowGet)
        Return retval
    End Function

    Public Async Function DeleteLandingPage(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oLandingPage = Await FEB2.WtbolLandingPage.Find(exs, id)
        If exs.Count = 0 Then
            If oLandingPage Is Nothing Then
                retval = Await ErrorResult("Landing page not found")
            Else
                If Await FEB2.WtbolLandingPage.Delete(exs, oLandingPage) Then
                    retval = Await LandingPages(oLandingPage.Site.Guid)
                Else
                    retval = Await ErrorResult(exs)
                End If
            End If
        End If
        Return retval
    End Function

    Async Function LandingPagesUpload(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim model = Await FEB2.WtbolSite.Find(exs, id)
        If exs.Count = 0 Then
            If model Is Nothing Then
                retval = Await ErrorResult("Site not found")
            Else
                ViewBag.Title = String.Format("Wtbol LandingPage {0}", model.Nom)
                ViewBag.SideMenuItems = MyBase.SideMenuItems()
                With ViewBag.SideMenuItems
                    .add(New DTOMenuItem("", ""))
                    .add(New DTOMenuItem(model.Nom, ""))
                    .add(New DTOMenuItem(Tradueix("Ficha", "Fitxa", "Properties"), "/pro/proWtbolSite/LandingPage/" & model.Guid.ToString))
                    .add(New DTOMenuItem("LandingPages", "/pro/proWtbolSite/LandingPages/" & model.Guid.ToString))
                    .add(New DTOMenuItem("Stocks", "/pro/proWtbolSite/Stocks/" & model.Guid.ToString))
                End With

                retval = View("LandingPagesUpload", model)
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function LandingPagesExcel(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oSite = Await FEB2.WtbolSite.Find(exs, id)
        Dim oCataleg = Await FEB2.Cataleg.CustomerBasicTree(exs, oSite.Customer, ContextHelper.Lang)
        Dim oSheet = DTOWtbolSite.ExcelLandingPages(oCataleg, oSite, ContextHelper.Lang, exs)
        Return LoginOrFile(oSheet)
    End Function

    <HttpGet>
    Public Async Function Baskets(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim Model = Await FEB2.WtbolSite.Find(exs, guid)
        If exs.Count = 0 Then
            Model.Baskets = Await FEB2.WtbolBaskets.All(exs, Model)
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
End Class

Public Class ProWtbolSitesController
    Inherits _MatController
    Public Async Function Index() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = MyBase.User()
        Dim model = Await FEB2.WtbolSites.All(exs)
        If exs.Count = 0 Then
            ViewBag.Title = ContextHelper.Lang.Tradueix("Where to buy online (Wtbol)")
            ViewBag.SideMenuItems = MyBase.SideMenuItems()
            Return View("Sites", model)
        Else
            Return Await ErrorResult(exs)
        End If
    End Function
End Class


