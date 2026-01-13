Public Class Mod145

End Class
Public Class Mods145
    Inherits _FeblBase

    Shared Async Function GetValues(exs As List(Of Exception), emp As DTOEmp) As Task(Of List(Of DTOMod145))
        Return Await Api.Fetch(Of List(Of DTOMod145))(exs, "Mods145", CInt(emp.Id))
    End Function

End Class
