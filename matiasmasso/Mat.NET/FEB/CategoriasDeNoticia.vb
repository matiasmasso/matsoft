Public Class CategoriaDeNoticia

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOCategoriaDeNoticia)
        Return Await Api.Fetch(Of DTOCategoriaDeNoticia)(exs, "CategoriaDeNoticia", oGuid.ToString())
    End Function

    Shared Async Function FromNom(nom As String, exs As List(Of Exception)) As Task(Of DTOCategoriaDeNoticia)
        Return Await Api.Fetch(Of DTOCategoriaDeNoticia)(exs, "CategoriaDeNoticia/FromNom", nom)
    End Function

    Shared Function Load(ByRef oCategoriaDeNoticia As DTOCategoriaDeNoticia, exs As List(Of Exception)) As Boolean
        If Not oCategoriaDeNoticia.IsLoaded And Not oCategoriaDeNoticia.IsNew Then
            Dim pCategoriaDeNoticia = Api.FetchSync(Of DTOCategoriaDeNoticia)(exs, "CategoriaDeNoticia", oCategoriaDeNoticia.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOCategoriaDeNoticia)(pCategoriaDeNoticia, oCategoriaDeNoticia, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oCategoriaDeNoticia As DTOCategoriaDeNoticia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOCategoriaDeNoticia)(oCategoriaDeNoticia, exs, "CategoriaDeNoticia")
        oCategoriaDeNoticia.IsNew = False
    End Function

    Shared Async Function Delete(oCategoriaDeNoticia As DTOCategoriaDeNoticia, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOCategoriaDeNoticia)(oCategoriaDeNoticia, exs, "CategoriaDeNoticia")
    End Function

    Shared Function Url(oCategoriaDeNoticia As DTOCategoriaDeNoticia, Optional oDomain As DTOWebDomain = Nothing) As String
        If oDomain Is Nothing Then oDomain = DTOWebDomain.Default()
        Dim retval As String = oDomain.Url("noticias", TextHelper.RemoveDiacritics(oCategoriaDeNoticia.Nom)).ToLower
        Return retval
    End Function

End Class

Public Class CategoriasDeNoticia

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOCategoriaDeNoticia))
        Return Await Api.Fetch(Of List(Of DTOCategoriaDeNoticia))(exs, "CategoriasDeNoticia")
    End Function

    Shared Async Function ForSiteMap(exs As List(Of Exception)) As Task(Of List(Of DTOCategoriaDeNoticia))
        Return Await Api.Fetch(Of List(Of DTOCategoriaDeNoticia))(exs, "CategoriasDeNoticia/ForSiteMap")
    End Function

End Class
