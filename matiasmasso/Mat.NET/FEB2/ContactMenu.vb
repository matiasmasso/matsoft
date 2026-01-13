Public Class ContactMenu
    Inherits _FeblBase

    Shared Async Function Find(exs As List(Of Exception), oContact As DTOContact) As Task(Of DTOContactMenu)
        Dim retval As DTOContactMenu = Nothing
        If oContact IsNot Nothing Then
            retval = Await Api.Fetch(Of DTOContactMenu)(exs, "ContactMenu", oContact.Guid.ToString())
        End If
        Return retval
    End Function
    Shared Function FindSync(exs As List(Of Exception), oContact As DTOContact) As DTOContactMenu
        Dim retval As DTOContactMenu = Nothing
        If oContact IsNot Nothing Then
            retval = Api.FetchSync(Of DTOContactMenu)(exs, "ContactMenu", oContact.Guid.ToString())
        End If
        Return retval
    End Function

    Shared Async Function Find(exs As List(Of Exception), oGuid As Guid) As Task(Of DTOContactMenu)
        Return Await Api.Fetch(Of DTOContactMenu)(exs, "ContactMenu", oGuid.ToString())
    End Function

End Class
