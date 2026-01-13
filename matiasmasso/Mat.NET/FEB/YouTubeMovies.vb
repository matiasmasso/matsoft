Public Class YouTubeMovie
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOYouTubeMovie)
        Return Await Api.Fetch(Of DTOYouTubeMovie)(exs, "YouTubeMovie", oGuid.ToString())
    End Function

    Shared Async Function Thumbnail(oGuid As Guid, exs As List(Of Exception)) As Task(Of Byte())
        Return Await Api.FetchImage(exs, "YouTubeMovie/Thumbnail", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oYouTubeMovie As DTOYouTubeMovie, exs As List(Of Exception)) As Boolean
        If Not oYouTubeMovie.IsLoaded And Not oYouTubeMovie.IsNew Then
            Dim pYouTubeMovie = Api.FetchSync(Of DTOYouTubeMovie)(exs, "YouTubeMovie", oYouTubeMovie.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOYouTubeMovie)(pYouTubeMovie, oYouTubeMovie, exs)
            End If
        End If

        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update_Old(oYouTubeMovie As DTOYouTubeMovie, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOYouTubeMovie)(oYouTubeMovie, exs, "YouTubeMovie")
        oYouTubeMovie.IsNew = False
    End Function

    Shared Async Function Update(value As DTOYouTubeMovie, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(value, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            If value.Thumbnail IsNot Nothing Then
                oMultipart.AddFileContent("image", value.Thumbnail)
            End If
            retval = Await Api.Upload(oMultipart, exs, "YouTubeMovie")
        End If
        Return retval
    End Function


    Shared Async Function Delete(oYouTubeMovie As DTOYouTubeMovie, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOYouTubeMovie)(oYouTubeMovie, exs, "YouTubeMovie")
    End Function
End Class

Public Class YouTubeMovies
    Inherits _FeblBase
    Shared Async Function Model(exs As List(Of Exception), oEmp As DTOEmp, oLang As DTOLang, oUser As DTOUser) As Task(Of DTOYouTubeMovie.ProductModel)
        Dim retval = Await Api.Fetch(Of DTOYouTubeMovie.ProductModel)(exs, "YouTubeMovies/productmodel", oEmp.Id, oLang.Tag, OpcionalGuid(oUser))
        Return retval
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, oLang As DTOLang) As Task(Of List(Of DTOYouTubeMovie))
        Return Await Api.Fetch(Of List(Of DTOYouTubeMovie))(exs, "YouTubeMovies/all", oLang.Tag, OpcionalGuid(oUser))
    End Function

    Shared Async Function All(exs As List(Of Exception), oUser As DTOUser, Optional oProduct As DTOProduct = Nothing, Optional oLang As DTOLang = Nothing) As Task(Of List(Of DTOYouTubeMovie))
        Dim retval As List(Of DTOYouTubeMovie)
        If oLang Is Nothing Then
            retval = Await Api.Fetch(Of List(Of DTOYouTubeMovie))(exs, "YouTubeMovies", OpcionalGuid(oProduct), OpcionalGuid(oUser))
        Else
            retval = Await Api.Fetch(Of List(Of DTOYouTubeMovie))(exs, "YouTubeMovies", OpcionalGuid(oProduct), OpcionalGuid(oUser), oLang.Tag)
        End If
        Return retval
    End Function

    Shared Async Function Last(oProduct As DTOProduct, oUser As DTOUser, oLang As DTOLang, exs As List(Of Exception)) As Task(Of DTOYouTubeMovie)
        Return Await Api.Fetch(Of DTOYouTubeMovie)(exs, "YouTubeMovies/last", oProduct.Guid.ToString, oUser.Guid.ToString, oLang.Tag)
    End Function

    Shared Function LastSync(oProduct As DTOProduct, oUser As DTOUser, oLang As DTOLang, exs As List(Of Exception)) As DTOYouTubeMovie
        Dim oProductGuid As Guid = Nothing
        Dim oUserGuid As Guid = Nothing
        If oProduct IsNot Nothing Then oProductGuid = oProduct.Guid
        If oUser IsNot Nothing Then oUserGuid = oUser.Guid
        Return Api.FetchSync(Of DTOYouTubeMovie)(exs, "YouTubeMovies/last", oProductGuid.ToString, oUserGuid.ToString, oLang.Tag)
    End Function

    Shared Async Function FromProductGuid(oProductGuid As Guid, oLang As DTOLang, exs As List(Of Exception)) As Task(Of List(Of DTOYouTubeMovie))
        Return Await Api.Fetch(Of List(Of DTOYouTubeMovie))(exs, "YouTubeMovies/FromProductGuid", oProductGuid.ToString, oLang.Tag)
    End Function

    Shared Function FromProductGuidSync(oProductGuid As Guid, oLang As DTOLang, exs As List(Of Exception)) As List(Of DTOYouTubeMovie)
        Return Api.FetchSync(Of List(Of DTOYouTubeMovie))(exs, "YouTubeMovies/FromProductGuid", oProductGuid.ToString, oLang.Tag)
    End Function

    Shared Function ExistFromProductSync(exs As List(Of Exception), oProductGuid As Guid, oLang As DTOLang) As Boolean
        Return Api.FetchSync(Of Boolean)(exs, "YouTubeMovies/ExistFromProduct", oProductGuid.ToString, oLang.Id)
    End Function

End Class

