Public Class DTOCur
    Property tag As String
    Property nom As DTOLangText
    Property symbol As String
    Property decimals As Integer = 2
    Property exchangeRate As DTOCurExchangeRate
    Property obsoleto As Boolean


    Public Enum Ids
        NotSet
        EUR
        USD
        GBP
        ESP
    End Enum

    Public Sub New()
        MyBase.New
    End Sub

    Shared Function Factory(sTag As String) As DTOCur
        Return DTOApp.Current.Curs.FirstOrDefault(Function(x) x.Tag = sTag.ToUpper)
    End Function


    Public Function Clon() As DTOCur
        Dim retval = DTOCur.Factory(_Tag)
        With retval
            .Nom = _Nom
            .Symbol = _Symbol
            .Decimals = _Decimals
            .ExchangeRate = _ExchangeRate
            .Obsoleto = _Obsoleto
        End With
        Return retval
    End Function

    Shared Function NomOrTag(oCur As DTOCur, oLang As DTOLang) As String
        Dim retval As String = oCur.Nom.Tradueix(oLang)
        If retval = "" Then retval = oCur.Tag
        Return retval
    End Function


    Shared Function Eur() As DTOCur
        Return DTOCur.Factory(DTOCur.Ids.EUR.ToString)
    End Function

    Shared Function Usd() As DTOCur
        Return DTOCur.Factory(DTOCur.Ids.USD.ToString)
    End Function

    Shared Function Gbp() As DTOCur
        Return DTOCur.Factory(DTOCur.Ids.GBP.ToString)
    End Function

    Public Function FormatString() As String
        Dim sb As New System.Text.StringBuilder
        sb.Append("#,##0")
        If _Decimals > 0 Then sb.Append("." & New String("0", _Decimals))
        If _Symbol > "" Then
            sb.Append(" " & _Symbol)
        Else
            sb.Append(" " & _Tag)
        End If
        sb.Append(";-" & sb.ToString & ";#")
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Shadows Function Equals(ByVal oCur As DTOCur) As Boolean
        Dim retVal As Boolean = False
        If oCur IsNot Nothing Then
            If _Tag = oCur.Tag Then retVal = True
        End If
        Return retVal
    End Function

    Public Shadows Function UnEquals(ByVal oCur As DTOCur) As Boolean
        Dim retVal As Boolean = Not Equals(oCur)
        Return retVal
    End Function


    Public Function AmtFromEuros(Euros As Decimal, Optional oExchangeRate As DTOCurExchangeRate = Nothing) As DTOAmt
        If oExchangeRate Is Nothing Then oExchangeRate = _ExchangeRate
        Dim Val As Decimal = Math.Round(Euros * oExchangeRate.Rate, 2, MidpointRounding.AwayFromZero)
        Dim retval = DTOAmt.Factory(Euros, Me, Val)
        Return retval
    End Function

    Public Function ExchangeText(Optional oExchangeRate As DTOCurExchangeRate = Nothing) As String
        If oExchangeRate Is Nothing Then oExchangeRate = _ExchangeRate
        Dim retval As String = String.Format("{0} {1}/€ {2:dd/MM/yy}", oExchangeRate.Rate, _Symbol, oExchangeRate.Fch)
        Return retval
    End Function


    Public Function AmtFromDivisa(Divisa As Decimal, Optional oExchangeRate As DTOCurExchangeRate = Nothing) As DTOAmt
        If oExchangeRate Is Nothing Then oExchangeRate = _ExchangeRate
        Dim Eur As Decimal = Math.Round(Divisa / oExchangeRate.Rate, 2, MidpointRounding.AwayFromZero)
        Dim retval = DTOAmt.Factory(Eur, Me, Divisa)
        Return retval
    End Function


    Public Function isEur() As Boolean
        Return _Tag = "EUR"
    End Function

    Public Function isUSD() As Boolean
        Return _Tag = "USD"
    End Function
End Class