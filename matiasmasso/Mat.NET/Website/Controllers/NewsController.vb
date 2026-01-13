Public Class NewsController
    Inherits _MatController


    '<ValidateInput(False)>
    Async Function Index(Catchall As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        'logueja l'usuari si ve de una circular
        Dim sUserGuid As String = Request.QueryString("UserGuid")
        If GuidHelper.IsGuid(sUserGuid) Then
            Dim pUser = Await FEB.User.Find(New Guid(sUserGuid), exs)
            Await MyBase.SignInUser(pUser, persist:=False)
        End If

        Dim uri = HttpContext.Request.Url

        Dim sAlias = uri.AbsolutePath.Trim("/").ToLower()
        Dim oWebPageAlias = Await FEB.WebPageAlias.FromUrl(exs, sAlias, DTOWebPageAlias.Domains.All)
        If oWebPageAlias IsNot Nothing Then
            Dim sUrlTo = oWebPageAlias.UrlTo
            Dim uriBuilder = New UriBuilder(uri.Scheme, uri.Host, uri.Port, sUrlTo)
            uri = uriBuilder.Uri
        End If


        Dim oModel As DTONoticia = Nothing
        Dim oDomain As DTOWebDomain = ContextHelper.Domain
        Dim oLang As DTOLang = ContextHelper.Lang
        Dim oMainSegment = New DTOLangText("noticias", "noticies", "news")
        Dim sFriendlySegment As String = ""
        ContextHelper.NavViewModel.ResetCustomMenu()
        If MyBase.UrlSplit(uri, oMainSegment, ".html", oDomain, oLang, sFriendlySegment) Then
            If String.IsNullOrEmpty(sFriendlySegment) Then
                retval = Await Noticias()
            Else
                If GuidHelper.IsGuid(sFriendlySegment) Then
                    Dim oGuid = New Guid(sFriendlySegment)
                    If oGuid.Equals(DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid) Then
                        Return Redirect("/blog/Consultas") ', model)
                    Else
                        oModel = Await FEB.Noticia.Find(exs, oGuid)
                    End If
                Else
                    oModel = Await FEB.Noticia.SearchByUrl(exs, sFriendlySegment)
                End If

                If oModel Is Nothing Then
                    Dim oBlogPost = If(GuidHelper.IsGuid(sFriendlySegment), Await FEB.BlogPost.Find(exs, New Guid(sFriendlySegment)), Await FEB.BlogPost.FromFriendlySegment(exs, sFriendlySegment, oLang))
                    If oBlogPost Is Nothing Then
                        Return Await ErrorNotFoundResult()
                    Else
                        Return RedirectToAction("Index", "BlogPost", New With {.guid = oBlogPost.Guid})
                    End If
                    Exit Function
                End If

                Dim oUser = MyBase.User
                If FEB.Noticia.IsAllowed(Website.GlobalVariables.Emp, oUser, oModel) Then
                    Await FEB.Session.Log(exs, oUser, oModel.Guid)

                    oModel.Text = Await FEB.ProductPlugins.Deploy(exs, oModel.Text)
                    ViewBag.Lang = oLang
                    ViewBag.Title = oModel.Title.Tradueix(oLang)
                    ViewBag.MetaDescription = oModel.Excerpt.Tradueix(oLang)
                    ViewBag.Canonical = oModel.Url()
                    retval = View("SingleNoticia", oModel)
                Else
                    If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
                        retval = MyBase.UnauthorizedView()
                    Else
                        Dim url = oMainSegment.Tradueix(ContextHelper.Lang) & "/" & Catchall
                        retval = LoginOrView(url)
                        'Dim oContext As ControllerContext = Me.ControllerContext
                        'Dim actionName As String = oContext.RouteData.Values("action")
                        'Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
                        'Dim SelfUrl As String = u.Action(actionName)
                        'Dim s As String = HttpContext.Request.RawUrl

                        'retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
                    End If
                End If
            End If
        Else
            retval = Await ErrorNotFoundResult()
        End If
        Return retval
    End Function

    Async Function NoticiaView(oLang As DTOLang, Catchall As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oNoticia As DTONoticia = Nothing

        If Catchall = "" Then
            retval = Await Noticias()
        Else
            If GuidHelper.IsGuid(Catchall) Then
                Dim oGuid As New Guid(Catchall)
                oNoticia = Await FEB.Noticia.Find(exs, oGuid)
            Else
                oNoticia = Await FEB.Noticia.SearchByUrl(exs, Catchall)
            End If

            If oNoticia Is Nothing Then
                retval = Await ErrorNotFoundResult()
            Else
                oNoticia.Comments = Await FEB.PostComments.Tree(exs, oNoticia, DTOPostComment.ParentSources.News, oLang, 15, 0)

                Dim sUserGuid As String = Request.QueryString("UserGuid")
                If GuidHelper.IsGuid(sUserGuid) Then
                    Dim pUser = Await FEB.User.Find(New Guid(sUserGuid), exs)
                    Await MyBase.SignInUser(pUser, persist:=False)
                End If

                Dim oUser = ContextHelper.GetUser()
                If oNoticia Is Nothing Then
                    retval = View("Index", DTONoticia.Srcs.News)
                Else
                    If FEB.Noticia.IsAllowed(Website.GlobalVariables.Emp, oUser, oNoticia) Then
                        Await FEB.Session.Log(exs, oUser, oNoticia.Guid)

                        ViewBag.Lang = oLang
                        ViewBag.Title = oNoticia.Title.Tradueix(oLang)
                        ViewBag.MetaDescription = oNoticia.Excerpt.Tradueix(oLang)
                        ViewBag.Canonical = oNoticia.Url()
                        retval = View("SingleNoticia", oNoticia)
                    Else
                        If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
                            retval = MyBase.UnauthorizedView()
                        Else
                            Dim oContext As ControllerContext = Me.ControllerContext
                            Dim actionName As String = oContext.RouteData.Values("action")
                            Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
                            Dim SelfUrl As String = u.Action(actionName)
                            Dim s As String = HttpContext.Request.RawUrl

                            retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
                        End If
                    End If
                End If

            End If


        End If

        Return retval
    End Function

    Async Function Noticias() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        If exs.Count = 0 Then
            Dim oLang = ContextHelper.Lang()
            Dim model = Await FEB.Noticias.LastNoticias(exs, oUser, Lang)
            If exs.Count = 0 Then
                ViewBag.Lang = oLang
                ViewBag.Title = oLang.Tradueix("Ultimas Noticias", "Darreres Noticies", "Last News", "Ultimas Noticias")
                ViewBag.MetaDescription = oLang.Tradueix("Consulta las últimas noticias publicadas", "Consulta les darreres noticies publicades", "Browse for last published news")
                retval = View("Noticias", model)
            Else
                retval = Await MyBase.ErrorResult(exs)
            End If
        Else
            retval = Await MyBase.ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function PartialNoticias(lang As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        Dim oLang = If(lang = "", ContextHelper.Lang, DTOLang.Factory(lang))
        ViewBag.Lang = oLang
        If exs.Count = 0 Then
            Dim model = Await FEB.Noticias.LastNoticias(exs, oUser, oLang)
            If exs.Count = 0 Then
                retval = PartialView("_Noticias", model)
            Else
                retval = PartialView("_Error", exs)
            End If
        Else
            retval = PartialView("_Error", exs)
        End If
        Return retval
    End Function

    <ValidateInput(False)>
    Async Function Eventos(Catchall As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oNoticia As DTONoticia = Nothing
        Dim oLang As DTOLang = ContextHelper.Lang()

        If Catchall = "" Then
            ViewBag.BreadCrumb = DTOBreadCrumb.FromEvento(oLang)
            retval = View("Index", DTONoticia.Srcs.Eventos)
        Else
            If GuidHelper.IsGuid(Catchall) Then
                Dim oGuid As New Guid(Catchall)
                oNoticia = Await FEB.Noticia.Find(exs, oGuid)
            Else
                oNoticia = Await FEB.Noticia.SearchByUrl(exs, Catchall)
            End If

            Dim sUserGuid As String = Request.QueryString("UserGuid")
            If GuidHelper.IsGuid(sUserGuid) Then
                Dim pUser = Await FEB.User.Find(New Guid(sUserGuid), exs)
                Await MyBase.SignInUser(pUser, persist:=True)
            End If

            Dim oUser = ContextHelper.GetUser()
            If FEB.Noticia.IsAllowed(Website.GlobalVariables.Emp, oUser, oNoticia) Then
                Await FEB.Session.Log(exs, oUser, oNoticia.Guid)

                ViewBag.Title = MatHelperStd.UrlHelper.Title(oNoticia.Title.Tradueix(oLang))
                ViewBag.Canonical = oNoticia.Url()
                'ViewBag.LeftSideConfig = DTO.Defaults.SideConfigs.Social
                'ViewBag.RightSideConfig = DTO.Defaults.SideConfigs.LastEvents
                ViewBag.BreadCrumb = DTOBreadCrumb.FromEvento(ContextHelper.Lang())
                Select Case oNoticia.Src
                    Case DTONoticia.Srcs.News
                        retval = View("SingleNoticia", oNoticia)
                    Case DTONoticia.Srcs.Eventos
                        retval = View("SingleEvento", oNoticia)
                End Select
            Else
                If oUser IsNot Nothing AndAlso oUser.Rol.isAuthenticated Then
                    retval = MyBase.UnauthorizedView()
                Else
                    Dim oContext As ControllerContext = Me.ControllerContext
                    Dim actionName As String = oContext.RouteData.Values("action")
                    Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
                    Dim SelfUrl As String = u.Action(actionName)
                    Dim s As String = HttpContext.Request.RawUrl

                    retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
                End If
            End If

        End If

        Return retval
    End Function

    <ValidateInput(False)>
    Async Function iMat(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oNoticia = Await FEB.Noticia.Find(exs, id)
        Dim oUser = ContextHelper.GetUser()
        Await FEB.Session.Log(exs, oUser, oNoticia.Guid)

        ViewBag.Canonical = oNoticia.Url()
        Dim retval As ActionResult = View(oNoticia)
        Return retval
    End Function

    <ValidateInput(False)>
    Async Function LastNoticiaForMobile() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser
        Dim oNoticia = Await FEB.Noticias.LastNoticia(exs, oUser, ContextHelper.Lang())
        Await FEB.Session.Log(exs, oUser, oNoticia.Guid)

        ViewBag.Canonical = oNoticia.Url()
        Dim retval As ActionResult = View("iMat", oNoticia)
        Return retval
    End Function




    <ValidateInput(False)>
    Overloads Async Function Content(Catchall As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim oNoticia As DTONoticia = Nothing
        Dim oLang As DTOLang = ContextHelper.Lang()

        If Catchall = "" Then
            ViewBag.BreadCrumb = DTOBreadCrumb.FromNoticia(oLang)
            retval = View(DTONoticia.Srcs.Content)
        Else
            If GuidHelper.IsGuid(Catchall) Then
                Dim oGuid As New Guid(Catchall)
                oNoticia = Await FEB.Noticia.Find(exs, oGuid)
            Else
                oNoticia = Await FEB.Noticia.SearchByUrl(exs, Catchall)
            End If

            Dim sUserGuid As String = Request.QueryString("UserGuid")
            If GuidHelper.IsGuid(sUserGuid) Then
                Dim pUser = Await FEB.User.Find(New Guid(sUserGuid), exs)
                Await MyBase.SignInUser(pUser, persist:=True)
            End If

            If oNoticia Is Nothing Then
                retval = View("Index", DTONoticia.Srcs.News)
            Else
                Dim oUser = ContextHelper.GetUser()
                If FEB.Noticia.IsAllowed(Website.GlobalVariables.Emp, oUser, oNoticia) Then
                    Await FEB.Session.Log(exs, oUser, oNoticia.Guid)

                    ViewBag.Title = MatHelperStd.UrlHelper.Title(oNoticia.Title.Tradueix(oLang))
                    ViewBag.Canonical = oNoticia.Url()
                    Select Case oNoticia.Src
                        Case DTONoticia.Srcs.News
                            ViewBag.RightSideConfig = DTO.Defaults.SideConfigs.lastnews
                            ViewBag.LeftSideConfig = DTO.Defaults.SideConfigs.social
                        Case DTONoticia.Srcs.Eventos
                            ViewBag.RightSideConfig = DTO.Defaults.SideConfigs.lastevents
                            ViewBag.LeftSideConfig = DTO.Defaults.SideConfigs.social
                    End Select
                    ViewBag.BreadCrumb = DTOBreadCrumb.FromNoticia(ContextHelper.Lang())
                    retval = View("SingleNoticia", oNoticia)
                Else
                    If oUser IsNot Nothing AndAlso oUser.Rol.IsAuthenticated Then
                        retval = MyBase.UnauthorizedView()
                    Else
                        Dim oContext As ControllerContext = Me.ControllerContext
                        Dim actionName As String = oContext.RouteData.Values("action")
                        Dim u As New System.Web.Mvc.UrlHelper(oContext.RequestContext)
                        Dim SelfUrl As String = u.Action(actionName)
                        Dim s As String = HttpContext.Request.RawUrl

                        retval = RedirectToAction("Login", "Account", New With {.returnurl = SelfUrl})
                    End If
                End If
            End If

        End If

        Return retval
    End Function

    <ValidateInput(False)>
    Async Function FromGuid(guid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim Model = Await FEB.Noticia.Find(exs, guid)
        Dim retval As ActionResult = View("SingleNoticia", Model)
        Return retval
    End Function



    <ValidateInput(False)>
    Async Function Categoria(Nom As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim model As IEnumerable(Of DTONoticiaBase) = Nothing
        If Nom = "" Then
            retval = View("Index", DTONoticia.Srcs.News)
            ' ViewBag.Title = ContextHelper.Tradueix("Ultimas Noticias", "Darreres Noticies", "Last News", "Ultimas Noticias")
            ' Dim oUser = ContextHelper.GetUser()
            ' If exs.Count = 0 Then
            ' Dim showPro = oUser.Rol.isProfesional
            ' Dim oNoticias = Await FEB.Noticias.Headers(exs, DTOContent.Srcs.News, Not showPro, OnlyVisible:=True)
            ' model = oNoticias.ToArray()
            ' retval = View("Index", model)
            ' Else
            '     retval = MyBase.ErrorResult(exs)
            ' End If
        Else
            Dim srcNom As String = TextHelper.RemoveDiacritics(Nom)
            Dim oCategoria = Await FEB.CategoriaDeNoticia.FromNom(srcNom, exs)
            If oCategoria IsNot Nothing Then
                ViewBag.LeftSideConfig = DTO.Defaults.SideConfigs.social
                ViewBag.RightSideConfig = DTO.Defaults.SideConfigs.lastnews
                ViewBag.BreadCrumb = DTOBreadCrumb.FromCategoriaDeNoticia(oCategoria)
                ViewBag.MetaDescription = oCategoria.Excerpt
                ViewBag.Title = MatHelperStd.UrlHelper.Title(ContextHelper.Tradueix("noticias clasificadas", "noticies per categoría", "classified news"))
                retval = View("xCategoria", oCategoria)
            End If
        End If

        Return retval
    End Function



End Class