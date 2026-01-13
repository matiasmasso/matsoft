Public Class LangController
    Inherits _MatController

    Public Function Index() As ActionResult
        Dim model As DTOLang = ContextHelper.Lang
        ViewBag.Title = ContextHelper.Tradueix("Selección de idioma", "Selecció d'idioma", "Language selection", "Seleção de idioma")
        Return View("Lang", model)
    End Function

    Public Function Switch() As ActionResult
        Dim url = HttpContext.Request.Url.AbsolutePath
        Dim langId = url.Substring(url.LastIndexOf("/") + 1)
        Dim oLang = DTOLang.Factory(langId)
        ContextHelper.SetLangCookie(oLang)
        Return RedirectToAction("Index", "Home")
    End Function

    <HttpPost>
    Public Async Function Index(oLang As DTOLang) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim oUser = ContextHelper.GetUser()
        If oUser IsNot Nothing AndAlso Not oUser.lang.Equals(oLang) Then
            oUser.lang = oLang
            Await FEB2.User.Update(exs, oUser)
        End If
        Return Redirect("/" & oLang.ISO6391)
    End Function
End Class
