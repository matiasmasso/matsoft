Public Class SitemapsController
    Inherits _MatController
    Async Function Index(id As String) As Threading.Tasks.Task(Of FileResult)
        Dim exs As New List(Of Exception)
        Dim retval As FileResult = Nothing
        Dim file As String = id 'regex "^([A-Za-z0-9_-]+/)*sitemap[A-Za-z0-9_-]*.xml$" any string begining with sitemaps, ended with .xml and preceded by any number of folders

        Dim oDomain = ContextHelper.Domain
        Dim oUser = DTOUser.Wellknown(DTOUser.Wellknowns.info)
        Dim oCache = Await FEB2.Cache.Fetch(exs, oUser)
        If exs.Count = 0 Then
            Dim oSitemap = Await FEB2.SiteMap.Load(exs, oCache, oDomain, file)
            If exs.Count = 0 Then
                If oSitemap Is Nothing Then
                    Dim msg = String.Format("sitemap request not found for'{0}'", file)
                    retval = Await ErrorNotFoundResult()
                Else
                    Dim oStream As Byte() = FEB2.SiteMap.Stream(oSitemap)
                    retval = New FileContentResult(oStream, "application/xml")
                End If
            Else
                retval = Await ErrorResult(exs)
            End If
        Else
            retval = Await ErrorResult(exs)
        End If

        Return retval
    End Function
End Class
