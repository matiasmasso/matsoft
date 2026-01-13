Public Class DTOCustomerRanking
    Property User As DTOUser
    Property Product As DTOProduct
    Property Area As DTOArea
    Property FchFrom As Date
    Property FchTo As Date

    Property items As List(Of DTOCustomerRankingItem)
    Property Sum As Decimal

    Public Sub New()
        MyBase.New()
        _items = New List(Of DTOCustomerRankingItem)
    End Sub

    Shared Function Factory(oUser As DTOUser, Optional oProduct As DTOProduct = Nothing, Optional oArea As DTOArea = Nothing, Optional DtFchFrom As Date = Nothing, Optional DtFchTo As Date = Nothing) As DTOCustomerRanking
        Dim retval As New DTOCustomerRanking
        With retval
            .User = oUser
            .Area = oArea
            .Product = oProduct
            .FchFrom = If(DtFchFrom = Nothing, DateTime.Today.AddYears(-1), DtFchFrom)
            .FchTo = If(DtFchTo = Nothing, DateTime.Today, DtFchTo)
        End With
        Return retval
    End Function

    Public Sub AddItem(oCustomer As DTOCustomer, DcEur As Decimal)
        Dim item As New DTOCustomerRankingItem(oCustomer, DcEur)
        _items.Add(item)
        _Sum = _items.Sum(Function(x) x.Eur)
    End Sub

    Public Function Quota(DcEur As Decimal) As Decimal
        Dim retval As Decimal
        If _Sum <> 0 Then
            retval = DcEur / _Sum
        End If
        Return retval
    End Function

    Public Function Accumulated(ByRef Arrastre As Decimal, DcEur As Decimal) As Decimal
        Arrastre = Arrastre + DcEur / _Sum
        Dim retval As Decimal = Arrastre
        Return retval
    End Function

    Public Function Rank(item As DTOCustomerRankingItem) As String
        Dim i As Integer = _items.IndexOf(item) + 1
        Dim retval As String = TextHelper.VbFormat(i, "0000")
        Return retval
    End Function

    Public Function Csv() As DTOCsv
        Dim sFilename As String = String.Format("M+O Customer ranking {0:yyyy.MM.dd}-{1:yyyy.MM.dd}.csv", _FchFrom, _FchTo)
        Dim retval As New DTOCsv(sFilename)
        For Each item As DTOCustomerRankingItem In _items
            Dim oRow = retval.AddRow()
            oRow.AddCell(item.Customer.FullNom)
            oRow.AddCell(item.Eur)
        Next
        Return retval
    End Function
End Class

Public Class DTOCustomerRankingItem
    Property Customer As DTOCustomer
    Property Eur As Decimal

    Public Sub New(oCustomer As DTOCustomer, DcEur As Decimal)
        MyBase.New
        _Customer = oCustomer
        _Eur = DcEur
    End Sub
End Class
