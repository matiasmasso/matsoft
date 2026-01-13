Public Class Template

    Shared Function Find(oGuid As Guid) As DTOTemplate
        Return TemplateLoader.Find(oGuid)
    End Function

    Shared Function Update(oTemplate As DTOTemplate, exs As List(Of Exception)) As Boolean
        Return TemplateLoader.Update(oTemplate, exs)
    End Function

    Shared Function Delete(oTemplate As DTOTemplate, exs As List(Of Exception)) As Boolean
        Return TemplateLoader.Delete(oTemplate, exs)
    End Function

End Class



Public Class Templates
    Shared Function All() As List(Of DTOTemplate)
        Return TemplatesLoader.All()
    End Function
End Class
