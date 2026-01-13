Public Class WebPageAlias
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOWebPageAlias)
        Return Await Api.Fetch(Of DTOWebPageAlias)(exs, "WebPageAlias", oGuid.ToString())
    End Function

    Shared Async Function FromUrl(exs As List(Of Exception), urlFrom As String, domain As DTOWebPageAlias.Domains) As Task(Of DTOWebPageAlias)
        Dim oWebPageAlias As New DTOWebPageAlias()
        oWebPageAlias.UrlFrom = urlFrom
        oWebPageAlias.domain = domain
        Return Await Api.Execute(Of DTOWebPageAlias, DTOWebPageAlias)(oWebPageAlias, exs, "WebPageAlias/FromUrl")
    End Function

    Shared Function Load(ByRef oWebPageAlias As DTOWebPageAlias, exs As List(Of Exception)) As Boolean
        If Not oWebPageAlias.IsLoaded And Not oWebPageAlias.IsNew Then
            Dim pWebPageAlias = Api.FetchSync(Of DTOWebPageAlias)(exs, "WebPageAlias", oWebPageAlias.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOWebPageAlias)(pWebPageAlias, oWebPageAlias, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oWebPageAlias As DTOWebPageAlias, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOWebPageAlias)(oWebPageAlias, exs, "WebPageAlias")
        oWebPageAlias.IsNew = False
    End Function


    Shared Async Function Delete(oWebPageAlias As DTOWebPageAlias, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOWebPageAlias)(oWebPageAlias, exs, "WebPageAlias")
    End Function
End Class

Public Class WebPagesAlias
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOWebPageAlias))
        Return Await Api.Fetch(Of List(Of DTOWebPageAlias))(exs, "WebPagesAlias")
    End Function

End Class
