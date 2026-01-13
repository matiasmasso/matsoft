Public Class NumBox
    Inherits Windows.Forms.TextBox

    Private mOldAmt As New maxisrvr.Amt()
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New()
        MyBase.New()
        TextAlign = HorizontalAlignment.Right
    End Sub

    Public Property Amt() As maxisrvr.Amt
        Get
            Dim oAmt As maxisrvr.Amt
            Dim s As String = Text.Replace(".", "")
            If s = "" Then s = "0"
            If mOldAmt.Cur.Id = "EUR" Then
                oAmt = New maxisrvr.Amt(s)
            Else
                Dim SngExchange As Decimal = mOldAmt.Eur / mOldAmt.Val
                oAmt = New maxisrvr.Amt(CDbl(s) * SngExchange, mOldAmt.Cur, CDbl(s))
            End If
            Return oAmt
        End Get
        Set(ByVal value As maxisrvr.Amt)
            mOldAmt = value
            Text = mOldAmt.Formatted
        End Set
    End Property

    Private Sub NumBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        Select Case e.KeyChar
            Case "0" To "9", "-", ""
            Case ","
                If Text.IndexOf(",") >= 0 Then
                    e.KeyChar = ""
                End If
            Case "."
                If Text.IndexOf(",") >= 0 Then
                    e.KeyChar = ""
                Else
                    e.KeyChar = ","
                End If
            Case Else
                e.KeyChar = ""
        End Select

    End Sub


    Private Sub NumBox_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Validated
        Text = Me.Amt.Formatted
        RaiseEvent AfterUpdate(Me, New System.EventArgs)
    End Sub
End Class
