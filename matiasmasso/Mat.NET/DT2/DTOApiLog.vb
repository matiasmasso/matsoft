Public Class DTOApiLog
    Public Enum Cods
        NotSet
        BritaxStoreLocator
    End Enum

    Public Property Cod As Cods
    Public Property Fch As Date
    Public Property Ip As String

    Shared Function Factory(oCod As DTOApiLog.Cods, Ip As String) As DTOApiLog
        Dim retval As New DTOApiLog
        With retval
            .Cod = oCod
            .Ip = Ip
        End With
        Return retval
    End Function
End Class
