Public Class Xl_Eur
    Inherits TextBox

    Private _Amt As DTOAmt
    Private _SwitchPeriodToComma As Boolean
    Private _PeriodToCommaPos As Integer
    Private _Dirty As Boolean
    Private _Allowevents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Property Amt As DTOAmt
        Get
            MyBase.TextAlign = HorizontalAlignment.Right
            If _Amt Is Nothing Then _Amt = DTOAmt.Empty
            Return _Amt
        End Get
        Set(value As DTOAmt)
            _Allowevents = False
            If value Is Nothing Then
                _Amt = DTOAmt.Empty
                MyBase.Clear()
            Else
                _Amt = value
                MyBase.Text = DTOAmt.CurFormatted(_Amt)
            End If
            _Allowevents = True
        End Set
    End Property

    Private Sub Xl_Eur_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        If _Allowevents Then
            Dim DcEur As Decimal

            Try
                DcEur = Decimal.Parse(MyBase.Text, Globalization.NumberStyles.Currency, Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                DcEur = 0
            End Try
            _Amt = DTOAmt.Factory(DcEur)
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Amt))
        End If
    End Sub

    Private Sub Xl_Eur_Validated(sender As Object, e As EventArgs) Handles Me.Validated
        _Allowevents = False
        MyBase.Text = DTOAmt.CurFormatted(_Amt)
        _Allowevents = True
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
            Case ","
                e.KeyChar = "."
                CancelEvent = False
            Case "-"
                CancelEvent = False
        End Select
        e.Handled() = CancelEvent
    End Sub


    Private Sub Xl_Amt_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
    End Sub
End Class
