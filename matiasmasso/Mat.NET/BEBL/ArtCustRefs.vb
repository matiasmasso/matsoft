Public Class ArtCustRefs
    Shared Function FromUser(oUser As DTOUser) As List(Of DTOGuidNom.Compact)
        Return ArtCustRefsLoader.All(oUser)
    End Function

    Shared Function ElCorteIngles() As List(Of DTO.Integracions.ElCorteIngles.Cataleg)
        Return ArtCustRefsLoader.ElCorteIngles()
    End Function

    Shared Function Append(exs As List(Of Exception), items As List(Of DTO.Integracions.ElCorteIngles.Cataleg))
        Return ArtCustRefsLoader.Append(exs, items)
    End Function

End Class
