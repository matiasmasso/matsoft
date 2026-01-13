Imports System.ComponentModel

Public Class Xl_TextBoxNum

    Private mDecimalSeparator As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator
    Private mGroupSeparator As String = System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator
    Private mFormatString As String = ""
    Private mCustomFormat As Formats

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)
    Public Shadows Event TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Enum Formats
        NotSet
        CustomFormatString
        Numeric
        [decimal]
        Eur
        Kg
        M3
        mm
        percent
        M2
    End Enum

    Public Property Value() As Decimal
        Get
            Dim RetVal As Decimal = 0
            Dim s As String = CleanValue()
            If IsNumeric(s) Then RetVal = CDec(s)
            Return RetVal
        End Get
        Set(ByVal value As Decimal)
            FormatValue(value)
        End Set
    End Property

    <DefaultValue(Formats.NotSet), Description("format de la caixa de texte"), Category("MatCustomProperties")> _
    Public Property Mat_CustomFormat() As Formats
        Get
            Return mCustomFormat
        End Get
        Set(ByVal value As Formats)
            mCustomFormat = value
        End Set
    End Property

    Public Property Mat_FormatString() As String
        Get
            Return mFormatString
        End Get
        Set(ByVal value As String)
            mFormatString = value
        End Set
    End Property

    Public Property [ReadOnly] As Boolean
        Get
            Return TextBox1.ReadOnly
        End Get
        Set(value As Boolean)
            TextBox1.ReadOnly = value
        End Set
    End Property

    Public Sub Clear()
        TextBox1.Clear()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Select Case e.KeyChar
            Case mGroupSeparator
                If InStr(TextBox1.Text, mDecimalSeparator) <> 0 Then
                    WarnDobleComa()
                    e.Handled = True
                Else
                    e.KeyChar = mDecimalSeparator
                End If
            Case mDecimalSeparator
                If InStr(TextBox1.Text, mDecimalSeparator) <> 0 Then
                    WarnDobleComa()
                    e.Handled = True
                End If
            Case "-"
                If InStr(TextBox1.Text, "-") <> 0 Then e.Handled = True
                If TextBox1.SelectionStart <> 0 Then e.Handled = True
            Case "0", "1", "2", "3", "4", "5", "6", "7", "8", "9"
            Case Chr(8), Chr(13)
                'backspace, carriage return
            Case Else
                e.Handled = True
        End Select
    End Sub

    Private Sub WarnDobleComa()
        MsgBox("no s'admeten els punts dels milers al introdui dades", MsgBoxStyle.Exclamation, "MAT.NET")
    End Sub

    Private Function CleanValue() As String
        Dim s As String = TextBox1.Text
        Dim sTarget As String = "0123456789.,-"
        Dim sb As New System.Text.StringBuilder
        For i As Integer = 0 To s.Length - 1
            If sTarget.IndexOf(s.Substring(i, 1)) >= 0 Then
                sb.Append(s.Substring(i, 1))
            Else
                Exit For
            End If
        Next
        s = sb.ToString
        If Not IsNumeric(s) Then s = "0"
        Return s
    End Function

    Private Sub FormatValue(ByVal Value As Decimal)
        If Value = 0 Then
            TextBox1.Clear()
        Else
            Dim sFormatString As String = ""
            Select Case mCustomFormat
                Case Formats.CustomFormatString
                    sFormatString = mFormatString
                Case Formats.Kg
                    sFormatString = "0.### Kg;-0.### Kg;Kg"
                Case Formats.M2
                    sFormatString = "#,##0.00 m2;-#,##0.00 m2;m2"
                Case Formats.M3
                    sFormatString = "0.### m3;-0.### m3;m3"
                Case Formats.Eur
                    sFormatString = "#,##0.00 €;-#,##0.00 €;€"
                Case Formats.decimal
                    sFormatString = "0.##;-0.##;#"
                Case Formats.Numeric
                    sFormatString = "#"
                Case Formats.mm
                    sFormatString = "#,### mm;-#,### mm;mm"
                Case Formats.percent
                    sFormatString = "# \%"
                Case Else
                    sFormatString = "#.#"
            End Select

            TextBox1.Text = Format(Value, sFormatString)
        End If
    End Sub

    Private Sub TextBox1_DragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragEnter
        If (e.Data.GetDataPresent(DataFormats.Text)) Then
            Dim s As String = e.Data.GetData(DataFormats.Text)
            If IsNumeric(s) Then
                e.Effect = DragDropEffects.Copy
            End If
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    Private Sub TextBox1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles TextBox1.DragDrop
        Try
            If e.Data.GetDataPresent(DataFormats.Text, False) Then
                Dim sValue As String = e.Data.GetData(DataFormats.Text)
                FormatValue(sValue)
            End If
        Catch ex As Exception
            MsgBox("Error in DragDrop function: " + ex.Message)
        Finally
            'TextBox1.BackColor = mDefaultBackColor
        End Try
    End Sub

    Private Sub TextBox1_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.Validated
        FormatValue(CleanValue)
        RaiseEvent AfterUpdate(CleanValue, EventArgs.Empty)
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        RaiseEvent TextChanged(CleanValue, EventArgs.Empty)
    End Sub
End Class

