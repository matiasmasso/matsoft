Public Class UserDefaults
    Inherits _FeblBase

    Shared Async Function GetValue(exs As List(Of Exception), oUser As DTOUser, oCod As DTOUserDefault.Cods) As Task(Of String)
        Return Await Api.Fetch(Of String)(exs, "UserDefaults", oUser.Guid.ToString, oCod)
    End Function


    Shared Async Function SetValue(exs As List(Of Exception), oUser As DTOUser, oCod As DTOUserDefault.Cods, sValue As String) As Task(Of Boolean)
        Dim oUserDefault = DTOUserDefault.Factory(oUser, oCod, sValue)
        Return Await Api.Update(Of DTOUserDefault)(oUserDefault, exs, "UserDefaults")
    End Function

    Shared Async Function GetInt(exs As List(Of Exception), oUser As DTOUser, oCod As DTOUserDefault.Cods) As Task(Of Integer)
        Dim retval As Integer
        Dim tmp As String = Await GetValue(exs, oUser, oCod)
        If TextHelper.VbIsNumeric(tmp) Then retval = CInt(tmp)
        Return retval
    End Function

End Class
