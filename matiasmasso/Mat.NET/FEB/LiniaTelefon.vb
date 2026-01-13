Public Class LiniaTelefon
    Inherits _FeblBase

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOLiniaTelefon)
        Return Await Api.Fetch(Of DTOLiniaTelefon)(exs, "LiniaTelefon", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oLiniaTelefon As DTOLiniaTelefon, exs As List(Of Exception)) As Boolean
        If Not oLiniaTelefon.IsLoaded And Not oLiniaTelefon.IsNew Then
            Dim pLiniaTelefon = Api.FetchSync(Of DTOLiniaTelefon)(exs, "LiniaTelefon", oLiniaTelefon.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOLiniaTelefon)(pLiniaTelefon, oLiniaTelefon, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oLiniaTelefon As DTOLiniaTelefon, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOLiniaTelefon)(oLiniaTelefon, exs, "LiniaTelefon")
        oLiniaTelefon.IsNew = False
    End Function


    Shared Async Function Delete(oLiniaTelefon As DTOLiniaTelefon, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOLiniaTelefon)(oLiniaTelefon, exs, "LiniaTelefon")
    End Function
End Class

Public Class LiniaTelefons
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOLiniaTelefon))
        Return Await Api.Fetch(Of List(Of DTOLiniaTelefon))(exs, "LiniaTelefons")
    End Function

End Class
