Public Class Evento
    Shared Function UrlFriendly(oEvento As DTOEvento, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Factory(AbsoluteUrl, "eventos", oEvento.UrlFriendlySegment)
    End Function

    Shared Function UrlThumbnail(oEvento As DTOEvento, Optional AbsoluteUrl As Boolean = False) As String
        Return UrlHelper.Image(DTO.Defaults.ImgTypes.News265x150, oEvento.Guid, AbsoluteUrl)
    End Function

End Class
Public Class Eventos
    Inherits _FeblBase

    Shared Function HeadersSync(exs As List(Of Exception), oUser As DTOUser) As List(Of DTOEvento)
        Return Api.FetchSync(Of List(Of DTOEvento))(exs, "Eventos", OpcionalGuid(oUser))
    End Function

    Shared Async Function Headers(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOEvento))
        Return Await Api.Fetch(Of List(Of DTOEvento))(exs, "Eventos", OpcionalGuid(oUser))
    End Function

    Shared Function NextEventoSync(exs As List(Of Exception), oUser As DTOUser) As DTOEvento
        Return Api.FetchSync(Of DTOEvento)(exs, "Eventos/nextEvento", OpcionalGuid(oUser))
    End Function
End Class
