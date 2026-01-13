Public Class ContentController
    Inherits _MatController
    Async Function Index(id As String) As Threading.Tasks.Task(Of ActionResult)
        Dim exs As New List(Of Exception)
        Dim retval As ActionResult = Nothing

        Dim model As DTOContent = Nothing
        Dim oDomain As DTOWebDomain = ContextHelper.Domain
        Dim oLang As DTOLang = ContextHelper.Lang
        Dim oMainSegment = New DTOLangText("content")
        Dim sFriendlySegment As String = ""
        If MyBase.UrlSplit(HttpContext.Request.Url, oMainSegment, ".html", oDomain, oLang, sFriendlySegment) Then
            If GuidHelper.IsGuid(sFriendlySegment) Then
                model = Await FEB2.Content.Find(exs, New Guid(sFriendlySegment))
                retval = View("Content", model)
            Else
                model = Await FEB2.Content.SearchByUrl(exs, sFriendlySegment)
                If model Is Nothing Then
                    'retval = Await ErrorResult(String.Format("Content '{0}' not found", id))

                    Dim sAlias = HttpContext.Request.Url.AbsolutePath.Trim("/").ToLower()
                    Dim oWebPageAlias = Await FEB2.WebPageAlias.FromUrl(exs, sAlias, DTOWebPageAlias.Domains.All)
                    If oWebPageAlias Is Nothing Then
                        retval = Await ErrorNotFoundResult()
                    Else
                        Dim sUrlTo = oWebPageAlias.UrlTo
                        If oWebPageAlias.UrlTo.StartsWith("http") Then
                            'pagina externa
                        ElseIf oWebPageAlias.UrlTo.StartsWith("/") Then
                            'url relativa
                        Else
                            'url relativa; afegir barra al principi
                            sUrlTo = "/" & sUrlTo
                        End If
                        retval = Redirect(sUrlTo)
                    End If

                Else
                    If exs.Count = 0 Then
                        ViewBag.Title = model.Title.Tradueix(ContextHelper.Lang)
                        retval = View("Content", model)
                    Else
                        retval = Await ErrorResult(exs)
                    End If
                End If
            End If
        Else
            retval = Await ErrorResult(exs)
        End If
        Return retval
    End Function
End Class
