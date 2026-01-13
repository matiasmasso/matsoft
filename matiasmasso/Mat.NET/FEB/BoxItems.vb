Public Class BoxItems

    Shared Function BrandVideosSync(oLang As DTOLang, exs As List(Of Exception)) As List(Of DTOBoxItem)
        Return Api.FetchSync(Of List(Of DTOBoxItem))(exs, "BoxItems/BrandVideos", oLang.Tag)
    End Function

    Shared Function FromEBooksSync(oEmp As DTOEmp, exs As List(Of Exception)) As List(Of DTOBoxItem)
        Return Api.FetchSync(Of List(Of DTOBoxItem))(exs, "BoxItems/FromEBooks", oEmp.Id)
    End Function


End Class
