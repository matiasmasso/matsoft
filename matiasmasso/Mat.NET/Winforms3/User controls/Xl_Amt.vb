Public Class Xl_Amt
    Inherits TextBox

    Private _Amt As DTOAmt
    Private _SwitchPeriodToComma As Boolean
    Private _PeriodToCommaPos As Integer
    Private _Dirty As Boolean

    Private _AllowEvents As Boolean

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)


    Public Property Value() As DTOAmt
        Get
            Dim retval As DTOAmt = Nothing
            If _Amt IsNot Nothing Then
                retval = _Amt.Clone
            End If
            Return retval
        End Get
        Set(ByVal value As DTOAmt)
            _Amt = value
            If Not _Amt Is Nothing Then Display()
        End Set
    End Property

    Private Sub Display()
        _AllowEvents = False
        MyBase.Text = _Amt.Formatted
        _AllowEvents = True
    End Sub

    Public Shadows Sub Clear()
        If _Amt IsNot Nothing Then
            If _Amt.Cur IsNot Nothing Then
                _Amt = DTOAmt.Factory(_Amt.Cur)
            Else
                _Amt = DTOAmt.Empty
            End If
        Else
            _Amt = DTOAmt.Empty
        End If
    End Sub

    Private Sub Xl_Amt_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        Dim CancelEvent As Boolean
        If Char.IsLetter(e.KeyChar) Then CancelEvent = True
        If Char.IsPunctuation(e.KeyChar) Then CancelEvent = True
        Select Case e.KeyChar
            Case "."
                CancelEvent = False
                _SwitchPeriodToComma = True
                _PeriodToCommaPos = MyBase.SelectionStart
            Case ",", "-", "+", "*", "/"
                CancelEvent = False
        End Select
        e.Handled() = CancelEvent
    End Sub


    Private Sub Xl_Amt_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Dim s As String
        If _SwitchPeriodToComma Then
            _SwitchPeriodToComma = False
            s = MyBase.Text.Substring(0, _PeriodToCommaPos) & ","
            If MyBase.Text.Length > s.Length Then s = s & MyBase.Text.Substring(_PeriodToCommaPos + 1)
            MyBase.Text = s
            MyBase.SelectionStart = _PeriodToCommaPos + 1
            MyBase.SelectionLength = 0
        End If

        s = MyBase.Text
        If s = "" Then s = "0"
        If IsNumeric(s) Then

            Dim oCur As DTOCur = Nothing
            If _Amt Is Nothing Then
                _Amt = DTOAmt.Empty
            Else
                oCur = _Amt.Cur
            End If
            If oCur Is Nothing Then oCur = DTOCur.Eur
            Dim DecEur As Decimal
            Dim DecVal As Decimal = CDec(s)
            If oCur.Tag = DTOCur.Eur.Tag Then
                DecEur = DecVal
            Else
                If _Amt.Eur <> 0 Then
                    Dim factor As Decimal = _Amt.Val / _Amt.Eur
                    DecEur = Math.Round(DecVal * factor, 2, MidpointRounding.AwayFromZero)
                End If
            End If
            _Amt = DTOAmt.Factory(DecEur, oCur.Tag, DecVal)
        End If

        _Dirty = True

    End Sub

    Private Sub Xl_Amt_Validated(sender As Object, e As EventArgs) Handles Me.Validated
        If _Dirty Then
            _Dirty = False
            RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Value))
        End If

    End Sub
End Class
