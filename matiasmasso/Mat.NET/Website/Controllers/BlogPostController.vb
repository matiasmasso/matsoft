'Imports DocumentFormat.OpenXml.InkML
Imports System.Threading


Public Class BlogPostController
    Inherits _MatController


    Async Function CatchAll() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Redirect("/")
        Return retval
        'Dim exs As New List(Of Exception)
        'Dim oModel As DTOBlogPostModel = Nothing
        'Dim oDomain As DTOWebDomain = ContextHelper.Domain
        'Dim oLang As DTOLang = ContextHelper.Lang
        'Dim oMainSegment = New DTOLangText("blog")
        'Dim sFriendlySegment As String = ""
        'If MyBase.UrlSplit(HttpContext.Request.Url, oMainSegment, ".html", oDomain, oLang, sFriendlySegment) Then
        '    If String.IsNullOrEmpty(sFriendlySegment) Then
        '        oModel = Await FEB.BlogPost.LastModel(exs, oLang)

        '        If exs.Count = 0 Then
        '            Dim oContent = Await FEB.ProductPlugins.Deploy(exs, New DTOLangText(oModel.Text))
        '            oModel.Text = oContent.Tradueix(oLang)
        '            ViewBag.Lang = oLang
        '            ViewBag.Title = oModel.Title()
        '            ViewBag.MetaDescription = oModel.Excerpt()
        '            ViewBag.Canonical = oModel.Url
        '            Return View("Blogpost", oModel)
        '        Else
        '            Return Await ErrorResult(exs)
        '        End If
        '    Else
        '        oModel = Await FEB.BlogPost.FromFriendlySegment(exs, sFriendlySegment, oLang)
        '        If exs.Count = 0 Then
        '            If oModel Is Nothing Then
        '                Return Await ErrorNotFoundResult()
        '            Else
        '                Dim oContent = Await FEB.ProductPlugins.Deploy(exs, New DTOLangText(oModel.Text))
        '                oModel.Text = oContent.Tradueix(oLang)
        '                ViewBag.Lang = oLang
        '                ViewBag.Title = oModel.Title()
        '                ViewBag.MetaDescription = oModel.Excerpt()
        '                ViewBag.Canonical = oModel.Url
        '                Return View("Blogpost", oModel)
        '            End If
        '        Else
        '            Return Await ErrorResult(exs)
        '        End If
        '    End If
        'Else
        '    Return Await ErrorNotFoundResult()
        'End If

    End Function


    Async Function Index(guid As Nullable(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang
        ViewBag.Title = "Blog"
        Dim model As DTOBlogPostModel = Nothing
        If guid Is Nothing Then
            model = Await FEB.BlogPost.LastModel(exs, oLang)
        Else
            Dim oPost As New DTOBlogPost(guid)
            model = Await FEB.BlogPost.Model(exs, oPost, oLang)
        End If

        If exs.Count = 0 Then
            ViewBag.Title = model.Title
            ViewBag.Canonical = model.Url
            retval = View("BlogPost", model)
        Else
            retval = Await MyBase.ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function Posts() As Threading.Tasks.Task(Of ActionResult)
        Dim retval As ActionResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang
        Dim model = Await FEB.BlogPosts.Models(exs, oLang)
        Dim a = model.Where(Function(x) x.Url.AbsoluteUrl(DTOLang.ESP()).Contains("tensado")).ToList()
        If exs.Count = 0 Then
            ViewBag.Lang = oLang
            ViewBag.Title = oLang.Tradueix("Últimas publicaciones:", "Darreres publicacions:", "Last posts:", "Últimas publicações:")
            ViewBag.MetaDescription = oLang.Tradueix("Consulta los últimos posts publicados en el blog", "Consulta els darrers posts publicats en aquest blog", "Browse for last posts published in this blog")
            ViewBag.Canonical = DTOUrl.Factory("Blog/posts")
            retval = PartialView("BlogPosts", model)
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function

    Async Function LastPostsPartial() As Threading.Tasks.Task(Of PartialViewResult)
        Dim retval As PartialViewResult = Nothing
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang
        Dim model = Await FEB.BlogPosts.Models(exs, oLang)
        If exs.Count = 0 Then
            Return PartialView("_BlogPosts", model)
        Else
            Return PartialView("_Error", exs)
        End If
    End Function


    Async Function SignUp(returnPostGuid As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        ViewBag.Title = ContextHelper.Tradueix("Formulario de registro", "Formulari de registre", "Sign up form", "Formulário de inscrição")

        Dim oBlogPost = Await FEB.BlogPost.Find(exs, returnPostGuid)
        If exs.Count = 0 Then
            Dim oModel As New LoginViewModel
            With oModel
                .ReturnUrl = oBlogPost.Url.RelativeUrl(ContextHelper.Lang)
                .Src = DTOUser.Sources.blog
            End With

            Dim oUser = ContextHelper.GetUser()
            If oUser Is Nothing Then
                retval = View("SignUp", oModel)
            Else
                oModel.EmailAddress = oUser.EmailAddress
                retval = View("SignedUpAlready", oModel)
            End If
        Else
            retval = Await ErrorResult(exs)
        End If

        Return retval
    End Function

    Async Function SignedUp(returnUrl As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim oUser = ContextHelper.GetUser()
        Await ContextHelper.SetNavViewModel(oUser)
        Return PartialView("_SignUp_Thanks", returnUrl)
    End Function

    Async Function About() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang()
        Dim oContent = DTOContent.Wellknown(DTOContent.Wellknowns.BlogAboutUs)
        With oContent
            .Title = Await FEB.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentTitle)
            .Text = Await FEB.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentText)
        End With
        Dim model = DTOBlogPostModel.Factory(oContent, ContextHelper.Lang)
        model.Posts = Await FEB.BlogPosts.Models(exs, oLang)

        ViewBag.Title = model.Title
        ViewBag.Canonical = DTOUrl.Factory("blog/about")
        Return View(model)
    End Function

    Async Function Normasdeuso() As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oLang = ContextHelper.Lang()
        Dim oContent = DTOContent.Wellknown(DTOContent.Wellknowns.BlogNormasDeUso)
        With oContent
            .Title = Await FEB.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentTitle)
            .Text = Await FEB.LangText.Find(exs, oContent.Guid, DTOLangText.Srcs.ContentText)
        End With
        Dim model = DTOBlogPostModel.Factory(oContent, ContextHelper.Lang)
        model.Posts = Await FEB.BlogPosts.Models(exs, oLang)

        ViewBag.Title = model.Title
        ViewBag.Canonical = DTOUrl.Factory("blog/normasdeuso")
        Return View(model)
    End Function

    Async Function Consultas(returnPostGuid As Nullable(Of Guid)) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser As DTOUser = Session("User")
        Dim oTree As DTOPostComment.TreeModel = Nothing
        If returnPostGuid Is Nothing Then
            oTree = Await FEB.PostComments.Tree(exs, DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog), DTOPostComment.ParentSources.Blog, ContextHelper.Lang, 15, 0)
        Else
            Dim oPostComment As New DTOPostComment(returnPostGuid)
            With oPostComment
                .Parent = DTOContent.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid
                .ParentSource = DTOPostComment.ParentSources.Blog
            End With
            oTree = Await FEB.PostComments.Tree(exs, oPostComment)
        End If
        Dim model As New BlogConsultaModel
        With model
            If oUser IsNot Nothing Then
                .EmailAddress = oUser.EmailAddress
                .Nickname = oUser.NicknameOrElse
            End If
            .Comments = oTree
        End With
        'model.Posts = Await FEB.BlogPosts.Models(exs, oLang)
        ViewBag.Title = ContextHelper.Tradueix("Consultas", "Consultes", "Any questions?")
        ViewBag.Canonical = DTOUrl.Factory("blog/consultas")
        Return View("Consultas", model)
    End Function

    Async Function WithComment(id As Guid) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing
        Dim oLang = ContextHelper.Lang

        Dim oComment = Await FEB.PostComment.Find(exs, id)
        Dim oPost As New DTOBlogPost(oComment.Parent)
        Dim oModel As DTOBlogPostModel = Await FEB.BlogPost.Model(exs, oPost, oLang)
        oModel.Comments = Await FEB.PostComments.Tree(exs, oPost, DTOPostComment.ParentSources.Blog, ContextHelper.Lang, 15, 0)
        If exs.Count = 0 Then
            If oModel Is Nothing Then
                Return Await ErrorNotFoundResult()
            Else
                ViewBag.Lang = oLang
                ViewBag.Title = oModel.Title()
                ViewBag.MetaDescription = oModel.Excerpt()
                ViewBag.Canonical = oModel.Url
                retval = View("Blogpost", oModel)
            End If
        Else
            Return Await ErrorResult(exs)
        End If
        Return retval
    End Function


    <HttpPost>
    Async Function Consulta(model As BlogConsultaModel) As Threading.Tasks.Task(Of PartialViewResult)
        Dim exs As New List(Of Exception)
        Dim retval As PartialViewResult = Nothing
        If ContextHelper.GetUser Is Nothing Then
            Dim oUser As DTOUser = Await FEB.User.FromEmail(exs, New DTOEmp(DTOEmp.Ids.MatiasMasso), model.EmailAddress) ' TO DEPRECATE
            If oUser Is Nothing Then
                oUser = New DTOUser
                oUser.Emp = Website.GlobalVariables.Emp
                oUser.EmailAddress = model.EmailAddress
                oUser.NickName = model.Nickname
                oUser.Lang = ContextHelper.Lang
                oUser.Source = DTOUser.Sources.wpComment
                oUser.Rol = New DTORol(DTORol.Ids.lead)
                Await FEB.User.Update(exs, oUser)
                If exs.Count > 0 Then
                    retval = PartialView("_Error", exs)
                    Return retval
                    Exit Function
                End If
            End If
            Session("User") = oUser
            ContextHelper.SetUserCookie(oUser, True)
        End If

        Dim oComment = New DTOPostComment
        With oComment
            .User = User()
            .Lang = ContextHelper.Lang
            .Parent = DTOBlogPost.Wellknown(DTOContent.Wellknowns.consultasBlog).Guid
            .ParentSource = DTOPostComment.ParentSources.Blog
            .Text = model.Text
            .ParentTitle = New DTOLangText("Consulta del Blog", "Consulta del Blog", "Blog query")
            .Fch = DTO.GlobalVariables.Now()
        End With

        If Await FEB.PostComment.Update(exs, oComment) Then
            Await FEB.PostComment.EmailPendingModeration(exs, Website.GlobalVariables.Emp, oComment)
            retval = PartialView("_ConsultaResult", oComment)
        Else
            retval = PartialView("_Error", exs)
        End If
        Return retval
    End Function

    Async Function Registro(returnPostGuid As String) As Threading.Tasks.Task(Of ActionResult)
        If String.IsNullOrEmpty(returnPostGuid) Then
            Return Await SignUp(Guid.Empty)
        ElseIf returnPostGuid.Contains("comment-page") Then
            Return Await Consultas(Nothing)
        Else
            Return Await SignUp(Guid.Empty)
        End If
    End Function



End Class

