Public Class ApiGet

    Shared Function Spv(year As Integer, id As Integer)
        Dim exs As New List(Of Exception)
        Dim oSpv As New DTOSpv
        With oSpv
            .fchAvis = New Date(year, 1, 1)
            .id = id
        End With
        FEB2.Spv.Update(oSpv, exs)
        'Return ApiHelper.PostRequest(Of DTOSpv, DTOSpv)("spv", oSpv)
    End Function

End Class
