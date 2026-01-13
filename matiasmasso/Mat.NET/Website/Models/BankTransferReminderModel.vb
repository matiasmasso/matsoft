Public Class BankTransferReminderModel
    Property Lang As DTOLang
    Property Pnds As List(Of DTOPnd)
    Property Iban As DTOIban
    Property Beneficiario As String
    Property CliNum As Integer

    Function Total() As DTOAmt
        Return DTOAmt.Factory(_Pnds.Sum(Function(x) x.amt.Eur))
    End Function
End Class
