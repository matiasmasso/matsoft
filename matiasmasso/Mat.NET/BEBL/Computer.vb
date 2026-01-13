Public Class Computer

    Shared Function Find(oGuid As Guid) As DTOComputer
        Return ComputerLoader.Find(oGuid)
    End Function

    Shared Function Update(oComputer As DTOComputer, exs As List(Of Exception)) As Boolean
        Return ComputerLoader.Update(oComputer, exs)
    End Function

    Shared Function Delete(oComputer As DTOComputer, exs As List(Of Exception)) As Boolean
        Return ComputerLoader.Delete(oComputer, exs)
    End Function

End Class



Public Class Computers
    Shared Function All() As List(Of DTOComputer)
        Return ComputersLoader.All()
    End Function
End Class
