Public Class LocalizedString
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOLocalizedString)
        Return Await Api.Fetch(Of DTOLocalizedString)(exs, "LocalizedString", oGuid.ToString())
    End Function

    Shared Function Load(exs As List(Of Exception), ByRef oLocalizedString As DTOLocalizedString) As Boolean
        If Not oLocalizedString.IsLoaded And Not oLocalizedString.IsNew Then
            Dim pLocalizedString = Api.FetchSync(Of DTOLocalizedString)(exs, "LocalizedString", oLocalizedString.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOLocalizedString)(pLocalizedString, oLocalizedString, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(exs As List(Of Exception), oLocalizedString As DTOLocalizedString) As Task(Of Boolean)
        Return Await Api.Update(Of DTOLocalizedString)(oLocalizedString, exs, "LocalizedString")
        oLocalizedString.IsNew = False
    End Function

    Shared Async Function Delete(exs As List(Of Exception), oLocalizedString As DTOLocalizedString) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOLocalizedString)(oLocalizedString, exs, "LocalizedString")
    End Function
End Class

Public Class LocalizedStrings
    Inherits _FeblBase

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOLocalizedString))
        Return Await Api.Fetch(Of List(Of DTOLocalizedString))(exs, "LocalizedStrings")
    End Function

End Class

