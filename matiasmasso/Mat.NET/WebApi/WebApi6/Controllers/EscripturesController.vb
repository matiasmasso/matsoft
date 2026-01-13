Public Class EscripturesController
    Inherits _BaseController

    <HttpGet>
    <Route("api/escriptures/")>
    Public Function EscripturesGet() As List(Of DTOCompactEscriptura)
        Dim oUser As DTOUser = BLLUser.WellKnown(BLLUser.WellKnowns.toni)
        Dim retval As List(Of DTOCompactEscriptura) = Escriptures(oUser)
        Return retval
    End Function

    <HttpPost>
    <Route("api/escriptures")>
    Public Function Escriptures(user As DTOGuidNom) As List(Of DTOCompactEscriptura)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        Dim retval As List(Of DTOCompactEscriptura) = Escriptures(oUser)
        Return retval
    End Function

    Private Function Escriptures(oUser As DTOUser) As List(Of DTOCompactEscriptura)
        Dim retval As New List(Of DTOCompactEscriptura)
        If oUser IsNot Nothing Then
            Dim oEscriptures As List(Of DTOEscriptura) = BLLEscriptures.All(oUser)
            For Each oEscriptura As DTOEscriptura In oEscriptures
                Dim item As New DTOCompactEscriptura
                With item
                    .guid = oEscriptura.Guid
                    .nom = oEscriptura.Nom
                    If oEscriptura.Notari IsNot Nothing Then
                        .notari = oEscriptura.Notari.FullNom
                    End If
                    .protocolo = oEscriptura.NumProtocol
                    .fchFrom = oEscriptura.FchFrom
                    If oEscriptura.FchTo <> Nothing Then
                        .fchTo = oEscriptura.FchTo
                    End If
                    If oEscriptura.DocFile IsNot Nothing Then
                        .docfile = New DTOCompactDocfile
                        With .docfile
                            .thumbnailUrl = BLLDocFile.ThumbnailUrl(oEscriptura.DocFile, True)
                            .downloadUrl = BLLDocFile.DownloadUrl(oEscriptura.DocFile, True)
                            .mediaFeatures = BLLDocFile.MediaFeatures(oEscriptura.DocFile)
                        End With
                    End If
                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function
End Class
