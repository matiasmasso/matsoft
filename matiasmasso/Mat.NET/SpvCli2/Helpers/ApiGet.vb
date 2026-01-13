Public Class ApiGet

    Shared Function Spv(year As Integer, id As Integer)
        Dim oSpv As New DTOSpv
        With oSpv
            .FchAvis = New Date(year, 1, 1)
            .Id = id
        End With
        Return ApiHelper.PostRequest(Of DTOSpv, DTOSpv)("spv", oSpv)
    End Function

End Class
