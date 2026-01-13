Public Class UserDefaults
    Shared Function GetValue(oUser As DTOUser, oCod As DTOUserDefault.Cods) As String
        Dim retval As String = UserDefaultLoader.GetValue(oUser, oCod)
        Return retval
    End Function

    Shared Function SetValue(oUserDefault As DTOUserDefault, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = UserDefaultLoader.SetValue(oUserDefault.User, oUserDefault.Cod, oUserDefault.Value, exs)
        Return retval
    End Function

End Class
