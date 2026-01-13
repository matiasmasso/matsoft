Public Class DTORemittanceAdvice
    Property Proveidor As DTOProveidor
    Property DocFile As DTODocFile
    Property Iban As DTOIban
    Property Items As List(Of DTORemittanceAdviceItem)

    Shared Function Total(value As DTORemittanceAdvice) As DTOAmt
        Dim retval = DTOAmt.Empty
        For Each oItem As DTORemittanceAdviceItem In value.Items
            retval.Add(oItem.Amt)
        Next
        Return retval
    End Function
End Class

Public Class DTORemittanceAdviceItem
    Property Fch As Date
    Property Fra As String
    Property Amt As DTOAmt
    Property Url As String

    Property DocFile As DTODocFile

    Public Function Text(oLang As DTOLang) As String
        Dim retval As String = oLang.Tradueix("factura", "factura", "invoice") & " " & _Fra & " " & oLang.Tradueix("del", "del", "from") & " " & _Fch.ToShortDateString
        Return retval
    End Function
End Class