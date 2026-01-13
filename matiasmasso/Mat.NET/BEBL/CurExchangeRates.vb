Imports System.Xml
Public Class CurExchangeRate
    Public Const SourceUrl As String = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"

    Shared Function Contravalor(oAmt As DTOAmt, ByRef oRate As DTOCurExchangeRate, Optional DtFch As Date = Nothing) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oAmt IsNot Nothing Then
            If DtFch = Nothing Then DtFch = DTO.GlobalVariables.Today()
            oRate = Closest(oAmt.Cur, DtFch)
            Dim Eur As Decimal = Math.Round(oAmt.Val / oRate.Rate, 2, MidpointRounding.AwayFromZero)
            retval = DTOAmt.Factory(Eur)
        End If
        Return retval
    End Function

    Shared Function Closest(oCur As DTOCur, DtFch As Date) As DTOCurExchangeRate
        Dim retval As DTOCurExchangeRate = Nothing
        Select Case oCur.Tag
            Case DTOCur.Ids.EUR.ToString
                retval = New DTOCurExchangeRate()
                With retval
                    .Fch = DTO.GlobalVariables.Today()
                    .Rate = 1
                End With
            Case DTOCur.Ids.ESP.ToString
                retval = New DTOCurExchangeRate()
                With retval
                    .Fch = "1/1/1999"
                    .Rate = 0.00601012
                End With
            Case Else
                retval = CurExchangeRateLoader.Closest(oCur, DtFch, CurExchangeRateLoader.Directions.PreviousDate)
                If retval Is Nothing Then
                    retval = CurExchangeRateLoader.Closest(oCur, DtFch, CurExchangeRateLoader.Directions.NextDate)
                End If
        End Select
        Return retval
    End Function

End Class

Public Class CurExchangeRates

    Shared Function All() As String
        Dim sb As New System.Text.StringBuilder
        For Each nfo As System.Globalization.CultureInfo In System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.SpecificCultures)

            Dim region As New System.Globalization.RegionInfo(nfo.LCID)
            sb.AppendLine("ISO: " + region.ISOCurrencySymbol)
            sb.AppendLine("Symbol: " + region.CurrencySymbol)
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function UpdateRates(exs As List(Of Exception)) As Boolean
        Dim oDownloadedRates As DTOCur.Collection = DownloadRates()
        If oDownloadedRates.Count = 0 Then
            exs.Add(New Exception("No s'han pogut descarregar els canvis de divisa de '" & BEBL.CurExchangeRate.SourceUrl & "'"))
        Else
            Dim oLastRates As DTOCur.Collection = LastRates()
            Dim oRatesToInsert = oDownloadedRates.Except(oLastRates).ToList
            If oRatesToInsert.Count > 0 Then
                CurExchangeRatesLoader.Save(oRatesToInsert, exs)
            End If
        End If
        Return exs.Count = 0
    End Function

    Shared Function LastRates(Optional DtFch As Date = Nothing) As DTOCur.Collection
        Dim retval = CurExchangeRatesLoader.LastRates(DtFch)
        Return retval
    End Function

    Protected Shared Function DownloadRates() As DTOCur.Collection
        Dim Url As String = BEBL.CurExchangeRate.SourceUrl
        Dim culture As New System.Globalization.CultureInfo("en-US")

        Dim doc As New XmlDocument()
        doc.Load(Url)

        Dim oNode As XmlNode = doc.SelectSingleNode("//*[@time]")
        Dim sFch As String = oNode.Attributes("time").InnerText
        Dim DtFch As Date = DateTime.Parse(sFch, culture)

        Dim retval As New DTOCur.Collection
        Dim oNodes As XmlNodeList = doc.SelectNodes("//*[@currency]")
        For Each node As XmlNode In oNodes
            Dim ISO As String = node.Attributes("currency").InnerText
            Dim sRate As String = node.Attributes("rate").InnerText
            Dim oCur = DTOCur.Factory(ISO)
            If oCur IsNot Nothing Then
                oCur.ExchangeRate = DTOCurExchangeRate.Factory(DtFch, Convert.ToDecimal(sRate, culture))
            End If
            retval.Add(oCur)
        Next
        Return retval
    End Function

End Class

