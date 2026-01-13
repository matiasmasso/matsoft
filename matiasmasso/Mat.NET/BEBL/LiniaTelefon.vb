Public Class LiniaTelefon

    Shared Function Find(oGuid As Guid) As DTOLiniaTelefon
        Dim retval As DTOLiniaTelefon = LiniaTelefonLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oLiniaTelefon As DTOLiniaTelefon, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = LiniaTelefonLoader.Update(oLiniaTelefon, exs)
        Return retval
    End Function

    Shared Function Delete(oLiniaTelefon As DTOLiniaTelefon, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = LiniaTelefonLoader.Delete(oLiniaTelefon, exs)
        Return retval
    End Function

End Class



Public Class LiniaTelefons
    Shared Function All() As List(Of DTOLiniaTelefon)
        Dim retval As List(Of DTOLiniaTelefon) = LiniaTelefonsLoader.All()
        Return retval
    End Function
End Class

