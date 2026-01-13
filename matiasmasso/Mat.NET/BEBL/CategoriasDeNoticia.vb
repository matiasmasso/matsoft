
Public Class CategoriaDeNoticia

    Shared Function Find(oGuid As Guid) As DTOCategoriaDeNoticia
        Dim retval As DTOCategoriaDeNoticia = CategoriaDeNoticiaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function FromNom(sNom As String) As DTOCategoriaDeNoticia
        Dim retval As DTOCategoriaDeNoticia = CategoriaDeNoticiaLoader.FromNom(sNom)
        Return retval
    End Function

    Shared Function Update(oCategoriaDeNoticia As DTOCategoriaDeNoticia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CategoriaDeNoticiaLoader.Update(oCategoriaDeNoticia, exs)
        Return retval
    End Function

    Shared Function Delete(oCategoriaDeNoticia As DTOCategoriaDeNoticia, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CategoriaDeNoticiaLoader.Delete(oCategoriaDeNoticia, exs)
        Return retval
    End Function

End Class



Public Class CategoriasDeNoticia
    Shared Function All() As List(Of DTOCategoriaDeNoticia)
        Dim retval As List(Of DTOCategoriaDeNoticia) = CategoriasDeNoticiaLoader.All()
        Return retval
    End Function

    Shared Function ForSitemap() As List(Of DTOCategoriaDeNoticia)
        Dim retval As List(Of DTOCategoriaDeNoticia) = CategoriasDeNoticiaLoader.ForSitemap()
        Return retval
    End Function
End Class

