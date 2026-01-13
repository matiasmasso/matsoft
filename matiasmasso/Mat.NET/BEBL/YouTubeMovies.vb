Public Class YouTubeMovie

    Shared Function Find(oGuid As Guid) As DTOYouTubeMovie
        Dim retval As DTOYouTubeMovie = YouTubeMovieLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Load(ByRef oYouTubeMovie As DTOYouTubeMovie) As Boolean
        Dim retval As Boolean = YouTubeMovieLoader.Load(oYouTubeMovie)
        Return retval
    End Function

    Shared Function Thumbnail(oGuid As Guid) As ImageMime
        Return YouTubeMovieLoader.Thumbnail(oGuid)
    End Function

    Shared Function Update(oYouTubeMovie As DTOYouTubeMovie, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = YouTubeMovieLoader.Update(oYouTubeMovie, exs)
        Return retval
    End Function

    Shared Function Delete(oYouTubeMovie As DTOYouTubeMovie, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = YouTubeMovieLoader.Delete(oYouTubeMovie, exs)
        Return retval
    End Function


End Class

Public Class YouTubeMovies

    Shared Function ProductModel(oEmp As DTOEmp, oLang As DTOLang, oUser As DTOUser) As List(Of DTOYouTubeMovie)
        Dim retval = YouTubeMoviesLoader.ProductModel(oEmp, oLang, oUser)
        Return retval
    End Function

    Shared Function All(Optional oProduct As DTOProduct = Nothing, Optional oUser As DTOUser = Nothing, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim retval As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.All(oProduct, oUser, oLang)
        Return retval
    End Function

    Shared Function Last(oProduct As DTOProduct, Optional oUser As DTOUser = Nothing, Optional oLang As DTOLang = Nothing) As DTOYouTubeMovie
        Dim retval As DTOYouTubeMovie = YouTubeMoviesLoader.Last(oProduct, oUser, oLang)
        If retval Is Nothing Then retval = YouTubeMoviesLoader.Last
        Return retval
    End Function

    Shared Function FromChildrenOrSelf(oProduct As DTOProduct, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim retval As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.FromChildrenOrSelf(oProduct, oLang)
        Return retval
    End Function

    Shared Function FromProduct(oProduct As DTOProduct, Optional oLang As DTOLang = Nothing) As List(Of DTOYouTubeMovie)
        Dim retval As List(Of DTOYouTubeMovie) = YouTubeMoviesLoader.FromChildrenOrSelf(oProduct, oLang)
        Return retval
    End Function

    Shared Function ExistFromProduct(oGuid As Guid, Optional oLang As DTOLang = Nothing) As Boolean
        Return YouTubeMoviesLoader.ExistFromProduct(oGuid, oLang)
    End Function
End Class
