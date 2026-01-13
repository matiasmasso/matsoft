Public Class Xl_Ean13
    Inherits TextBox
    Private _Tooltip As ToolTip
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Property Ean As DTOEan
        Get
            Dim retval As DTOEan = Nothing
            If Not String.IsNullOrEmpty(MyBase.Text) Then
                retval = DTOEan.Factory(MyBase.Text)
            End If
            Return retval
        End Get
        Set(value As DTOEan)
            If value Is Nothing Then
                MyBase.Clear()
            Else
                MyBase.Text = DTOEan.CleanDigits(value.Value.Trim)
            End If
            _AllowEvents = True
        End Set
    End Property

    Private Sub Xl_EAN13_TextChanged(sender As Object, e As EventArgs) Handles Me.TextChanged
        Dim oEan = DTOEan.Factory(MyBase.Text)
        Dim oResult = DTOEan.validate(oEan)
        If _Tooltip Is Nothing Then _Tooltip = New ToolTip
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
        RaiseEvent AfterUpdate(Me, New MatEventArgs(Me.Ean))
    End Sub
End Class
