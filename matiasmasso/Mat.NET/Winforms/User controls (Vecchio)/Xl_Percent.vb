Public Class Xl_Percent
    Inherits TextBox

    Private mNumberFormatInfo As System.Globalization.NumberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat
    Private mDecimalSeparator As String = mNumberFormatInfo.NumberDecimalSeparator
    Private mGroupSeparator As String = mNumberFormatInfo.NumberGroupSeparator
    Private mNegativeSign As String = mNumberFormatInfo.NegativeSign
    Private mDirty As Boolean

    Private _formatDecimalPlaces As Integer

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    ' Restricts the entry of characters to digits (including hex),
    ' the negative sign, the e decimal point, and editing keystrokes (backspace).

    Public Shadows Sub Load(value As Decimal, Optional formatDecimalPlaces As Integer = 2)
        _formatDecimalPlaces = formatDecimalPlaces
        Dim formatSpecifier = String.Format("P{0}", _formatDecimalPlaces)
        MyBase.Text = (value / 100).ToString(formatSpecifier)
    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)


        Dim keyInput As String = e.KeyChar.ToString()

        If [Char].IsDigit(e.KeyChar) Then
            ' Digits are OK
            mDirty = True
        ElseIf keyInput.Equals(mNegativeSign) Then
            If MyBase.SelectionStart = 0 Then
                'negative sign Ok only if left position
                Dim sText As String = MyBase.Text
                Dim sTextToCheck As String = sText.Remove(MyBase.SelectionStart, MyBase.SelectionLength)
                If sTextToCheck.Contains(mNegativeSign) Then
                    Beep()
                    e.Handled = True
                Else
                    mDirty = True
                End If
            Else
                Beep()
                e.Handled = True
            End If
        ElseIf keyInput.Equals(mDecimalSeparator) Then
            If CheckPreviousDecimalSeparator() Then
                Beep()
                e.Handled = True
            Else
                mDirty = True
            End If
        ElseIf keyInput.Equals(mGroupSeparator) Then
            If CheckPreviousDecimalSeparator() Then
                Beep()
                e.Handled = True
            Else
                e.KeyChar = mDecimalSeparator
                mDirty = True
            End If

        ElseIf e.KeyChar = vbBack Then
            ' Backspace key is OK
            mDirty = True
        Else
            ' Swallow this invalid key and beep
            Beep()
            e.Handled = True
        End If

    End Sub

    Private Function CheckPreviousDecimalSeparator() As Boolean
        Dim retval As Boolean
        Dim s As String = MyBase.Text
        retval = s.Contains(mDecimalSeparator)
        Return retval
    End Function

    Public Property Value() As Decimal
        Get
            Dim retval As Decimal
            Dim source As String = MyBase.Text
            Dim cleanSource As String = System.Text.RegularExpressions.Regex.Replace(source, "[^\d-,\.]", "")

            If IsNumeric(cleanSource) Then
                retval = [Decimal].Parse(cleanSource)
                'retval = DcTextValue / 100
            End If
            Return retval
        End Get
        Set(DcValue As Decimal)
            Dim formatSpecifier = String.Format("P{0}", _formatDecimalPlaces)
            MyBase.Text = (DcValue / 100).ToString(formatSpecifier)
            'RaiseEvent AfterUpdate(DcValue, EventArgs.Empty)
        End Set
    End Property

    Public Overloads Property [ReadOnly] As Boolean
        Get
            Return MyBase.ReadOnly
        End Get
        Set(value As Boolean)
            MyBase.ReadOnly = value
            If value Then
                MyBase.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Control)
            Else
                MyBase.BackColor = System.Drawing.Color.White
            End If
        End Set
    End Property

    Protected Overrides Sub OnLeave(e As System.EventArgs)
        MyBase.OnLeave(e)
        If mDirty Then
            Dim DcValue As Decimal = Me.Value
            Dim formatSpecifier = String.Format("P{0}", _formatDecimalPlaces)
            MyBase.Text = (DcValue / 100).ToString(formatSpecifier)

            RaiseEvent AfterUpdate(DcValue, MatEventArgs.Empty)
        End If
    End Sub

End Class

