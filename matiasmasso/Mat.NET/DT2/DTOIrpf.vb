Public Class DTOIrpf
    Property emp As DTOEmp
    Property yearMonth As DTOYearMonth
    Property items As List(Of Item)
    Property saldos As List(Of DTOPgcSaldo)

    Public Sub New()
        MyBase.New
        _Items = New List(Of Item)
        _Saldos = New List(Of DTOPgcSaldo)
    End Sub

    Shared Function Factory(oEmp As DTOEmp, year As Integer, month As Integer) As DTOIrpf
        Dim retval As New DTOIrpf
        With retval
            .emp = oEmp
            .yearMonth = New DTOYearMonth(year, month)
        End With
        Return retval
    End Function

    Public Function fch() As Date
        Return _yearMonth.LastFch
    End Function

    Public Class Item
        Property Ccb As DTOCcb
        Property Base As DTOAmt
    End Class


End Class
