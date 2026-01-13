Imports System.Xml

Public Class DTOPaymentTerms
    Property Cod As CodsFormaDePago
    Property Months As Integer
    Property PaymentDays As List(Of Integer)
    Property PaymentDayCod As PaymentDayCods
    Property Vacaciones As List(Of DTOVacacion)
    Property Iban As DTOIban
    Property NBanc As DTOBanc
    Property Plazos As List(Of Plazo)

    Public Enum CodsFormaDePago
        NotSet = 0
        Rebut = 1
        ReposicioFons = 2
        Comptat = 3
        Xerocopia = 4
        DomiciliacioBancaria = 5
        Transferencia = 6
        RebutARep = 7
        aNegociar = 9
        EfteAndorra = 10
        TransfPrevia = 11
        Diposit = 12
    End Enum

    Public Enum PaymentDayCods
        MonthDay
        WeekDay
    End Enum

    Public Sub New()
        MyBase.New
        _PaymentDays = New List(Of Integer)
        _Vacaciones = New List(Of DTOVacacion)
        _Plazos = New List(Of Plazo)
    End Sub

    Shared Function Vto(oPaymentTerms As DTOPaymentTerms, FromFch As Date) As Date
        Dim FchStep1 As Date = VtoCheckMesos(oPaymentTerms, FromFch)
        Dim FchStep2 As Date = VtoCheckDiasDePago(oPaymentTerms, FchStep1)
        Dim FchStep3 As Date = VtoCheckVacances(oPaymentTerms, FchStep2)
        Return FchStep3
    End Function

    Protected Shared Function VtoCheckMesos(oPaymentTerms As DTOPaymentTerms, ByVal FromFch As Date) As Date
        Dim retval As Date = FromFch.AddMonths(oPaymentTerms.Months)
        Return retval
    End Function

    Protected Shared Function VtoCheckDiasDePago(oPaymentTerms As DTOPaymentTerms, ByVal FromFch As Date) As Date
        Dim iPaymentDays As List(Of Integer) = oPaymentTerms.PaymentDays
        Dim retval As Date = FromFch
        If iPaymentDays IsNot Nothing Then
            If iPaymentDays.Count > 0 Then
                Dim nextPaymentDay As Integer
                Dim nextPaymentDays As List(Of Integer) = iPaymentDays.Where(Function(x) x >= FromFch.Day).ToList
                If nextPaymentDays.Count = 0 Then
                    nextPaymentDay = iPaymentDays.Min
                    retval = FromFch.AddMonths(1).AddDays(nextPaymentDay - FromFch.Day)
                Else
                    nextPaymentDay = nextPaymentDays.Min
                    Dim lastDayOfMonth As Integer = FromFch.AddMonths(1).AddDays(-FromFch.Day).Day
                    If nextPaymentDay > lastDayOfMonth Then nextPaymentDay = lastDayOfMonth
                    retval = FromFch.AddDays(nextPaymentDay - FromFch.Day)
                End If
            End If
        End If

        Return retval
    End Function

    Protected Shared Function VtoCheckVacances(oPaymentTerms As DTOPaymentTerms, ByVal SrcVto As Date) As Date
        Dim retval As Date = DTOVacacion.Result(oPaymentTerms.Vacaciones, SrcVto)
        Return retval
    End Function



    Shared Function CfpText(oCod As DTOPaymentTerms.CodsFormaDePago, oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        Dim oValueNom As DTOValueNom = Cods(oLang).Find(Function(x) x.Value = oCod)
        If oValueNom IsNot Nothing Then
            sb.Append(Cods(oLang).Find(Function(x) x.Value = oCod).Nom)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function VacacionsText(oVacacions As List(Of DTOVacacion), oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        For Each item As DTOVacacion In oVacacions
            sb.AppendLine(VacacionText(item, oLang))
        Next
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function VacacionText(item As DTOVacacion, oLang As DTOLang) As String
        Dim sb As New Text.StringBuilder
        sb.AppendFormat("{0} {1:00}/{2:00}", oLang.tradueix("del", "del", "from"), item.MonthDayFrom.Day, item.MonthDayFrom.Month)
        sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.tradueix("al", "al", "to"), item.MonthDayTo.Day, item.MonthDayTo.Month)
        If item.MonthDayResult.Month = 0 And item.MonthDayResult.Day = 0 Then
            sb.Append(oLang.tradueix(" aplaza 30 dias", " aplaça 30 dies", " add 30 days"))
        Else
            sb.AppendFormat(" {0} {1:00}/{2:00}", oLang.tradueix("aplaza al", "aplaça al", "delay to"), item.MonthDayResult.Day, item.MonthDayResult.Month)
        End If
        Dim retval As String = sb.ToString
        Return retval
    End Function

    Shared Function PaymentDaysText(oDays As List(Of Integer), ByVal oLang As DTOLang) As String
        Dim i As Integer
        Dim s As String = ""
        For i = 0 To oDays.Count - 1
            Select Case i
                Case 0
                    If oDays.Count = 1 Then
                        s = oLang.tradueix("dia", "dia", "day") & " "
                    Else
                        s = oLang.tradueix("dias", "dies", "days") & " "
                    End If
                Case oDays.Count - 1
                    s = s & oLang.tradueix(" y ", " i ", " and ")
                Case Else
                    s = s & ", "
            End Select
            If oDays(i) = 31 Then
                s = s & oLang.tradueix("final de mes", "fi de mes", "end month")
            Else
                s = s & oDays(i)
            End If
        Next
        Return s
    End Function

    Shared Function PlazoText(ByVal oPlazo As DTOPaymentTerms.Plazo, Optional ByVal oLang As DTOLang = Nothing) As String
        Dim s As String = ""
        If oLang Is Nothing Then oLang = DTOApp.current.lang()
        Select Case oPlazo.Period
            Case DTOPaymentTerms.Plazo.Periods.d000
                s = oLang.tradueix("a la vista", "a la vista", "at sight")
            Case DTOPaymentTerms.Plazo.Periods.d030
                s = oLang.tradueix("30 dias", "30 dies", "30 days")
            Case DTOPaymentTerms.Plazo.Periods.d060
                s = oLang.tradueix("60 dias", "60 dies", "60 days")
            Case DTOPaymentTerms.Plazo.Periods.d090
                s = oLang.tradueix("90 dias", "90 dies", "90 days")
            Case DTOPaymentTerms.Plazo.Periods.d120
                s = oLang.tradueix("120 dias", "120 dies", "120 days")
            Case DTOPaymentTerms.Plazo.Periods.d150
                s = oLang.tradueix("150 dias", "150 dies", "150 days")
            Case DTOPaymentTerms.Plazo.Periods.d180
                s = oLang.tradueix("180 dias", "180 dies", "180 days")
        End Select
        Return s
    End Function

    Shared Function Cods(oLang As DTOLang) As List(Of DTOValueNom)
        Dim retval As New List(Of DTOValueNom)
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.NotSet, oLang.tradueix("(por asignar)", "(per asignar)", "(Not set)")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.Rebut, oLang.tradueix("recibo", "rebut", "receipt")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.ReposicioFons, oLang.tradueix("reposición fondos", "reposició de fons", "your check")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.Comptat, oLang.tradueix("contado", "comptat", "cash")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.Xerocopia, oLang.tradueix("efecto sin domiciliación", "efecte sense domiciliació", "bank draft")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.DomiciliacioBancaria, oLang.tradueix("efecto domiciliado Sepa Core", "efecte domiciliat Sepa Core", "Sepa Core bank draft")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.Transferencia, oLang.tradueix("transferencia", "transferència", "bank transfer")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.RebutARep, oLang.tradueix("recibo a representante", "rebut a representant", "rep receipt")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.aNegociar, oLang.tradueix("a convenir", "per convindre", "to be agreed")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.EfteAndorra, oLang.tradueix("efecto domiciliado en Andorra", "efecte domiciliat a Andorra", "Andorra bank draft")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.TransfPrevia, oLang.tradueix("transferencia previa", "transferència prèvia", "cash in advance")))
        retval.Add(New DTOValueNom(DTOPaymentTerms.CodsFormaDePago.Diposit, oLang.tradueix("a deducir de depósito", "a deduir de diposit", "to deduct from deposit")))
        Return retval
    End Function

    Shared Shadows Function TextDias(oPaymentTerms As DTOPaymentTerms, ByVal oLang As DTOLang) As String
        Dim s As String = ""
        If oPaymentTerms.PaymentDays.Count > 0 Then
            Select Case oPaymentTerms.PaymentDayCod
                Case DTOPaymentTerms.PaymentDayCods.MonthDay
                    s = TextDiasDelMes(oPaymentTerms.PaymentDays, oLang)
                Case DTOPaymentTerms.PaymentDayCods.WeekDay
                    s = TextDiasDeLaSemana(oPaymentTerms.PaymentDays, oLang)
            End Select
        End If
        Return s
    End Function

    Shared Function TextDiasDelMes(values As List(Of Integer), ByVal oLang As DTOLang) As String
        Dim i As Integer
        Dim s As String = ""
        For i = 0 To values.Count - 1
            Select Case i
                Case 0
                    If values.Count = 1 Then
                        s = oLang.tradueix("dia", "dia", "day") & " "
                    Else
                        s = oLang.tradueix("dias", "dies", "days") & " "
                    End If
                Case values.Count - 1
                    s = s & oLang.tradueix(" y ", " i ", " and ")
                Case Else
                    s = s & ", "
            End Select
            s = s & values(i)
        Next
        Return s
    End Function

    Shared Function TextDiasDeLaSemana(values As List(Of Integer), ByVal oLang As DTOLang) As String
        Dim i As Integer
        Dim s As String = ""
        For i = 0 To values.Count - 1
            Select Case i
                Case 0
                    s = oLang.tradueix("dias", "dies", "days") & " "
                Case values.Count - 1
                    s = s & oLang.tradueix(" y ", " i ", " and ")
                Case Else
                    s = s & ", "
            End Select

            Dim iNumero As Integer = CInt(i / 7) + 1
            Select Case iNumero
                Case 0
                    s = s & oLang.tradueix("primer", "primer", "first")
                Case 0
                    s = s & oLang.tradueix("segundo", "segon", "second")
                Case 0
                    s = s & oLang.tradueix("tercer", "tercer", "third")
                Case 0
                    s = s & oLang.tradueix("cuarto", "quart", "fourth")
                Case 0
                    s = s & oLang.tradueix("último", "darrer", "last")
            End Select

            Dim iWeekDay As Integer = i Mod 7
            s = s & " " & oLang.WeekDay(iWeekDay)
            s = s & " " & oLang.tradueix("del mes", "del mes", "each month")
        Next
        Return s
    End Function

    Shared Function XMLEncoded(oPaymentTerms As DTOPaymentTerms) As String
        Dim retval As String = ""

        If oPaymentTerms IsNot Nothing Then
            Dim oDoc As XmlElement = Nothing
            Dim oNodePlazos As XmlElement = Nothing
            Dim oNodeDias As XmlElement = Nothing
            Dim oNodeItm As XmlElement = Nothing

            Dim oXMLDoc As New XmlDocument
            oDoc = oXMLDoc.CreateElement("FPG")
            oDoc.SetAttribute("MODO", oPaymentTerms.Cod)
            If oPaymentTerms.Iban IsNot Nothing Then
                oDoc.SetAttribute("IBAN", oPaymentTerms.Iban.Digits)
            End If

            If oPaymentTerms.NBanc IsNot Nothing Then
                oDoc.SetAttribute("NBANC", oPaymentTerms.NBanc.Guid.ToString())
            End If

            oXMLDoc.AppendChild(oDoc)

            If oPaymentTerms.Plazos.Count > 0 Then
                oNodePlazos = oXMLDoc.CreateElement("PLAZO")
                oDoc.AppendChild(oNodePlazos)

                Dim oPlazo As DTOPaymentTerms.Plazo
                For Each oPlazo In oPaymentTerms.Plazos
                    oNodeItm = oXMLDoc.CreateElement("ITM")
                    oNodeItm.InnerText = oPlazo.Period.ToString
                    oNodePlazos.AppendChild(oNodeItm)
                Next oPlazo
            End If
            retval = oXMLDoc.OuterXml
        End If
        Return retval
    End Function

    Shared Function Match(oPaymentTerms1 As DTOPaymentTerms, oPaymentTerms2 As DTOPaymentTerms) As Boolean
        Dim retval As Boolean = (DTOPaymentTerms.XMLEncoded(oPaymentTerms1) = DTOPaymentTerms.XMLEncoded(oPaymentTerms2))
        Return retval
    End Function


    Public Class Plazo
        Property Period As Periods
        Public Enum Periods
            d000
            d030
            d060
            d090
            d120
            d150
            d180
        End Enum

        Public Sub New(Optional oPeriod As Periods = Periods.d000)
            MyBase.New
            _Period = oPeriod
        End Sub
    End Class
End Class

