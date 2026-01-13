Public Class Atlas
    Shared Function Full(user As DTOUser) As List(Of DTOCountry)
        Dim retval As New List(Of DTOCountry)
        If user IsNot Nothing AndAlso BEBL.User.Load(user) Then
            retval = AtlasLoader.Full(user)
        End If
        Return retval
    End Function

    Shared Function Compact(user As DTOUser, onlysales As Boolean) As List(Of DTOCompactNode)
        Dim retval As New List(Of DTOCompactNode)
        If user IsNot Nothing AndAlso BEBL.User.Load(user) Then
            retval = AtlasLoader.Compact(user, onlysales)
        End If
        Return retval
    End Function
End Class
