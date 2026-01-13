Public Class Xl_EAN
    Inherits TextBox
    Private _Tooltip As New ToolTip
    Private _AllowEvents As Boolean

    Public Function EAN13() As DTOEan
        Dim retval As DTOEan = Nothing
        If Not String.IsNullOrEmpty(MyBase.Text) Then
            retval = DTOEan.Factory(MyBase.Text)
        End If
        Return retval
    End Function

    Public Shadows Sub Load(value As DTOEan)
        If value Is Nothing Then
            MyBase.Clear()
        Else
            MyBase.Text = DTOEan.CleanDigits(value.Value.Trim)
        End If
        _AllowEvents = True
    End Sub

    Private Sub Xl_EAN13_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        If _AllowEvents Then

            Dim oEan = DTOEan.Factory(MyBase.Text)
            Dim oResult = DTOEan.validate(oEan)
            Select Case oResult
                Case DTOEan.ValidationResults.Ok
                    MyBase.BackColor = Color.LightBlue
                    _Tooltip.SetToolTip(Me, "")
                Case DTOEan.ValidationResults.Empty
                    MyBase.BackColor = Color.White
                    _Tooltip.SetToolTip(Me, "")
                Case DTOEan.ValidationResults.WrongCheckDigit
                    MyBase.BackColor = Color.LightSalmon
                    _Tooltip.SetToolTip(Me, DTOEan.ValidationResultString(oResult, Current.Session.Lang))
                Case Else
                    MyBase.BackColor = Color.LightYellow
                    _Tooltip.SetToolTip(Me, DTOEan.ValidationResultString(oResult, Current.Session.Lang))
            End Select
        End If
    End Sub
End Class
