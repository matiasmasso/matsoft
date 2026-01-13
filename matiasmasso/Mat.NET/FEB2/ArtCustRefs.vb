Public Class ArtCustRefs

    Inherits _FeblBase

    Shared Async Function FromUser(exs As List(Of Exception), oUser As DTOUser) As Task(Of List(Of DTOGuidNom.Compact))
        Return Await Api.Fetch(Of List(Of DTOGuidNom.Compact))(exs, "ArtCustRef/fromUsr", oUser.Guid.ToString)
    End Function

    Shared Async Function ElCorteIngles(exs As List(Of Exception)) As Threading.Tasks.Task(Of List(Of DTO.Integracions.ElCorteIngles.Cataleg))
        Dim retval = Await Api.Fetch(Of List(Of DTO.Integracions.ElCorteIngles.Cataleg))(exs, "ArtCustRef/ElCorteIngles")

        Return retval
    End Function
End Class

