Public Class DTOCurExchangeRate
    Property Fch As Date
    Property Rate As Decimal

    Shared Function Factory(DtFch As Date, DcRate As Decimal) As DTOCurExchangeRate
        Dim retval As New DTOCurExchangeRate
        With retval
            .Fch = DtFch
            .Rate = DcRate
        End With
        Return retval
    End Function




End Class
