Public Class Statement
    Shared Function Years(oContact As DTOContact) As List(Of Integer)
        Return StatementLoader.Years(oContact)
    End Function

    Shared Function Items(oContact As DTOContact, year As Integer) As DTOStatement
        Return StatementLoader.Items(oContact, year)
    End Function
End Class
