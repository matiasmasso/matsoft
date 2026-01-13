Public Class DTORepLiq
    Inherits DTOBaseGuid

    Property rep As DTORep
    Property id As Integer
    Property fch As Date

    Property baseFras As DTOAmt
    Property baseImponible As DTOAmt
    Property ivaPct As Decimal
    Property ivaAmt As DTOAmt
    Property irpfPct As Decimal
    Property irpfAmt As DTOAmt
    Property total As DTOAmt

    Property docFile As DTODocFile

    Property cca As DTOCca

    Property items As List(Of DTORepComLiquidable)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function Factory(oRep As DTORep, DtFch As Date) As DTORepLiq
        Dim retval As New DTORepLiq
        With retval
            .Rep = oRep
            .Fch = DtFch
            .IRPFpct = DTORep.Irpf(.Rep, DtFch)
            .IVApct = DTORep.IVAtipus(.Rep, DtFch)
            .Items = New List(Of DTORepComLiquidable)
        End With
        Return retval
    End Function

    Shared Function GetBaseFacturas(value As DTORepLiq) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If value IsNot Nothing Then
            If value.Items.Count = 0 Then
                retval = DTOAmt.Empty
            Else
                Dim DcEur As Decimal = value.Items.Sum(Function(x) x.Base.Eur)
                retval = DTOAmt.factory(DcEur)
            End If
        End If
        Return retval
    End Function

    Shared Function GetTotalComisions(value As DTORepLiq) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If value IsNot Nothing Then
            If value.Items.Count = 0 Then
                retval = DTOAmt.Empty
            Else
                Dim DcEur As Decimal = value.Items.Sum(Function(x) x.Comisio.Eur)
                retval = DTOAmt.factory(DcEur)
            End If
        End If
        Return retval
    End Function

    Shared Function GetLiquid(oRepLiq As DTORepLiq) As DTOAmt
        Dim retval As DTOAmt = oRepLiq.BaseImponible.Clone

        If oRepLiq.IVApct <> 0 Or oRepLiq.IRPFpct <> 0 Then
            If oRepLiq.IVApct <> 0 Then
                retval.Add(GetIVAAmt(oRepLiq))
            End If
            If oRepLiq.IRPFpct <> 0 Then
                retval.Substract(GetIRPFAmt(oRepLiq))
            End If
        End If
        Return retval
    End Function

    Shared Function GetIVAAmt(value As DTORepLiq) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If value IsNot Nothing Then
            If value.IVApct <> 0 Then
                Dim oBaseImponible As DTOAmt = value.BaseImponible
                retval = oBaseImponible.Percent(value.IVApct)
            End If
        End If
        Return retval
    End Function

    Shared Function GetIRPFAmt(value As DTORepLiq) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If value IsNot Nothing Then
            If value.IRPFpct <> 0 Then
                Dim oBaseImponible As DTOAmt = value.BaseImponible
                retval = oBaseImponible.Percent(value.IRPFpct)
            End If
        End If
        Return retval
    End Function


    Shared Function Caption(value As DTORepLiq, oLang As DTOLang) As String
        Dim sb As New System.Text.StringBuilder
        sb.Append(oLang.tradueix("Liquidación de Comisiones", "Liquidació de Comisions", "Commission Statement"))
        sb.Append(" " & value.Id)
        sb.Append(oLang.tradueix(" del ", " del ", " from "))
        sb.Append(value.Fch.ToShortDateString)

        Dim retval As String = sb.ToString
        Return retval
    End Function

    Public Function FormattedId() As String
        Dim retval As String = Format(_Fch.Year, "0000") & Format(_Id, "000")
        Return retval
    End Function

    Public Shadows Function UrlSegment() As String
        Return MyBase.UrlSegment("representante/liquidacion")
    End Function



End Class

