Public Class DTOAmt

    Property cur As DTOCur
    Property eur As Decimal
    Property val As Decimal

    Public Sub New()
        MyBase.New()
        '_Cur = DTOApp.Current.Cur
    End Sub

    Shared Function Factory(Optional DcEur As Decimal = 0) As DTOAmt
        Dim retval = Factory(DcEur, DTOCur.Eur, DcEur)
        Return retval
    End Function

    Shared Function Factory(oCur As DTOCur, Optional DcDivisa As Decimal = 0) As DTOAmt
        If oCur Is Nothing Then oCur = DTOApp.Current.Cur
        Dim retval = oCur.AmtFromDivisa(DcDivisa)
        Return retval
    End Function

    Shared Function Factory(DcEur As Decimal, sCur As String, Optional DcVal As Decimal = 0) As DTOAmt
        Dim retval As New DTOAmt
        With retval
            .Eur = DcEur
            .Cur = DTOCur.Factory(sCur)
            .Val = DcVal
        End With
        Return retval
    End Function


    Shared Function Factory(DcEur As Decimal, oCur As DTOCur, DcVal As Decimal) As DTOAmt
        If oCur Is Nothing Then oCur = DTOApp.Current.Cur
        Dim retval As New DTOAmt
        With retval
            .Eur = DcEur
            .Val = DcVal
            .Cur = oCur
        End With
        Return retval
    End Function

    Shared Function Factory(ParamArray oAmts() As DTOAmt) As DTOAmt
        Dim retval = DTOAmt.Factory
        For Each oAmt In oAmts
            retval.Add(oAmt)
        Next
        Return retval
    End Function

    Shared Function Empty(Optional oCur As DTOCur = Nothing) As DTOAmt
        Dim retval = DTOAmt.Factory(oCur)
        Return retval
    End Function

    Shared Function FromQtyPriceDto(iQty As Integer, oPrice As DTOAmt, Optional DcDto As Decimal = 0) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oPrice IsNot Nothing Then
            retval = oPrice.Times(iQty)
            If DcDto <> 0 Then
                Dim oDto As DTOAmt = retval.Percent(DcDto)
                retval = retval.Substract(oDto)
            End If
        End If
        Return retval
    End Function

    Shared Function FromBaseImponible(ByVal oBase As DTOAmt, DcIva As Decimal, Optional DcReq As Decimal = 0, Optional DcIrpf As Decimal = 0) As DTOAmt
        Dim oIVAAmt As DTOAmt = Nothing
        Dim oReqAmt As DTOAmt = Nothing
        Dim oIRPFAmt As DTOAmt = Nothing

        If DcIva <> 0 Then
            oIVAAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcIva / 100, 2, MidpointRounding.AwayFromZero))
            If DcReq <> 0 Then
                oReqAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcReq / 100, 2, MidpointRounding.AwayFromZero))
            End If
        End If
        If DcIrpf <> 0 Then
            oIRPFAmt = DTOAmt.Factory(oBase.Cur, Math.Round(oBase.Eur * DcIrpf / 100, 2, MidpointRounding.AwayFromZero))
        End If

        Dim retval As DTOAmt = FromBaseImponible(oBase, oIVAAmt, oReqAmt, oIRPFAmt)
        Return retval
    End Function

    Shared Function FromBaseImponible(ByVal oBase As DTOAmt, oIva As DTOAmt, Optional oReq As DTOAmt = Nothing, Optional oIrpf As DTOAmt = Nothing) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oBase Is Nothing Then
            retval = DTOAmt.Factory
        Else
            retval = oBase.Clone
            If oIva IsNot Nothing Then
                retval.Add(oIva)
                If oReq IsNot Nothing Then
                    retval.Add(oReq)
                End If
            End If
            If oIrpf IsNot Nothing Then
                retval.Substract(oIrpf)
            End If
        End If
        Return retval
    End Function

    Public Function Times(DcFactor As Decimal) As DTOAmt
        Dim retval As New DTOAmt
        retval.Eur = _Eur * DcFactor
        retval.Val = _Val * DcFactor
        retval.Cur = _Cur
        Return retval
    End Function

    Public Function DividedBy(DcFactor As Decimal) As DTOAmt
        Dim retval As New DTOAmt
        retval.Eur = _Eur / DcFactor
        retval.Val = _Val / DcFactor
        retval.Cur = _Cur
        Return retval
    End Function

    Public Function Percent(DcTipus As Decimal) As DTOAmt
        Dim retval As New DTOAmt
        retval.Eur = Math.Round(_Eur * DcTipus / 100, _Cur.Decimals, MidpointRounding.AwayFromZero)
        retval.Val = Math.Round(_Val * DcTipus / 100, _Cur.Decimals, MidpointRounding.AwayFromZero)
        retval.Cur = _Cur
        Return retval
    End Function

    Public Sub AddPercent(ByVal DcPercentatge As Decimal)
        If DcPercentatge <> 0 Then
            Dim DcEur As Decimal = Math.Round(_Eur * DcPercentatge / 100, _Cur.Decimals, MidpointRounding.AwayFromZero)
            Dim DcVal As Decimal = Math.Round(_Val * DcPercentatge / 100, _Cur.Decimals, MidpointRounding.AwayFromZero)
            _Eur += DcEur
            _Val += DcVal
        End If
    End Sub

    Public Function DeductPercent(ByVal DcPercentatge As Decimal) As DTOAmt
        AddPercent(-DcPercentatge)
        Return Me
    End Function

    Public Function Clone() As DTOAmt
        Dim retval As New DTOAmt
        With retval
            .Eur = _Eur
            .Val = _Val
            .Cur = _Cur
        End With
        Return retval
    End Function

    Public Function Add(oAmtToAdd As DTOAmt) As DTOAmt
        If oAmtToAdd IsNot Nothing Then
            If _Eur = 0 And _Val = 0 Then
                _Cur = oAmtToAdd.Cur
            End If
            _Eur += oAmtToAdd.Eur
            _Val += oAmtToAdd.Val

        End If
        Return Me.Clone
    End Function

    Public Function Substract(oSubstraend As DTOAmt) As DTOAmt
        If oSubstraend IsNot Nothing Then
            If _Eur = 0 And _Val = 0 Then
                _Cur = oSubstraend.Cur
            End If
            _Eur -= oSubstraend.Eur
            _Val -= oSubstraend.Val
        End If
        Return Me.Clone
    End Function

    Public Function Inverse() As DTOAmt
        Dim retval = DTOAmt.Factory(-_Eur, _Cur, -_Val)
        Return retval
    End Function

    Public Function IsPositive() As Boolean
        Dim retval As Boolean = _Eur > 0
        Return retval
    End Function

    Public Function IsNegative() As Boolean
        Dim retval As Boolean = _Eur < 0
        Return retval
    End Function

    Public Function IsZero() As Boolean
        Dim retval As Boolean = _Eur = 0
        Return retval
    End Function

    Public Function IsNotZero() As Boolean
        Dim retval As Boolean = _Eur <> 0
        Return retval
    End Function

    Public Function IsGreaterThan(ByVal oComparedAmt As DTOAmt) As Boolean
        Dim retVal As Boolean
        If oComparedAmt IsNot Nothing Then
            Dim CurrentEur As Decimal = _Eur
            Dim ComparedEur As Decimal = oComparedAmt.Eur
            retVal = (CurrentEur > ComparedEur)
        End If
        Return retVal
    End Function

    Public Function IsGreaterOrEqualThan(ByVal oComparedAmt As DTOAmt) As Boolean
        Dim retVal As Boolean
        If oComparedAmt IsNot Nothing Then
            Dim CurrentEur As Decimal = _Eur
            Dim ComparedEur As Decimal = oComparedAmt.Eur
            retVal = (CurrentEur >= ComparedEur)
        End If
        Return retVal
    End Function

    Shared Function CurFormatted(oAmt As DTOAmt, Optional BlankIfZero As Boolean = True) As String
        Dim retval As String = ""
        If oAmt IsNot Nothing Then
            If oAmt.cur IsNot Nothing Then
                If oAmt.val <> 0 Or Not BlankIfZero Then
                    retval = Format(oAmt.val, oAmt.cur.FormatString)
                End If
            End If
        End If
        Return retval
    End Function

    Shared Function CurFormatted(dcEur As Decimal, Optional BlankIfZero As Boolean = True) As String
        Dim oAmt = DTOAmt.Factory(dcEur)
        Dim retval As String = CurFormatted(oAmt, BlankIfZero)
        Return retval
    End Function


    Public Function Formatted() As String
        Dim retval As String = Format(_Val, "#,##0.00;-#,##0.00;#")
        Return retval
    End Function

    Public Function Absolute() As DTOAmt
        Dim retVal As DTOAmt = Nothing
        If IsNegative() Then
            retVal = Inverse()
        Else
            retVal = Me
        End If
        Return retVal
    End Function

    Public Shadows Function Equals(oCandidate As Object) As Boolean
        Dim retval As Boolean
        If TypeOf oCandidate Is DTOAmt Then
            If oCandidate IsNot Nothing Then
                If _Cur.Equals(CType(oCandidate, DTOAmt).Cur) Then
                    retval = (_Eur = CType(oCandidate, DTOAmt).Eur AndAlso _Val = CType(oCandidate, DTOAmt).Val)
                End If
                End If
            End If
        Return retval
    End Function

    Public Function unEquals(oCandidate As Object) As Boolean
        Dim retval As Boolean = Not Equals(oCandidate)
        Return retval
    End Function

    Shared Function Import(iQty As Integer, ByVal oPrice As DTOAmt, DcDto As Decimal) As DTOAmt
        Dim retval As DTOAmt = Nothing
        If oPrice IsNot Nothing Then
            retval = oPrice.Times(iQty)
            If DcDto <> 0 Then
                Dim oDto As DTOAmt = retval.Percent(DcDto)
                retval = retval.Substract(oDto)
            End If
        End If
        Return retval
    End Function

    Shared Function EurOrDefault(oAmt As DTOAmt) As Decimal
        Dim retval As Decimal = 0
        If oAmt IsNot Nothing Then
            retval = oAmt.Eur
        End If
        Return retval
    End Function

    Public Function Trimmed() As DTOAmt
        Dim oCur = DTOCur.Factory(_Cur.Tag)
        Dim retval = DTOAmt.Factory(_Eur, oCur, _Val)
        Return retval
    End Function
End Class
