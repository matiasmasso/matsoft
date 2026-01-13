Public Class Cobrament

    Shared Function Update(oCobrament As DTOCobrament, ByRef exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = CobramentLoader.Update(oCobrament.Cca, oCobrament.Pnds, oCobrament.Impagats, exs)
        Return retval
    End Function

End Class
