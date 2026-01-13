Public Class Xl_EANOld
    Public Event Changed(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Ean13() As DTOEan
        Get
            Return DTOEan.Factory(TextBox1.Text)
        End Get
        Set(ByVal value As DTOEan)
            If value Is Nothing Then
                TextBox1.Clear()
            Else
                TextBox1.Text = value.Value
            End If
        End Set
    End Property

    Public Overrides Property Text() As String
        Get
            Return TextBox1.Text
        End Get
        Set(ByVal value As String)
            If value <> Nothing Then
                TextBox1.Text = value
            End If
        End Set
    End Property

    Private Sub TextBox1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.DoubleClick
        Dim oEan As DTOEan = DTOEan.Factory(TextBox1.Text)
        Dim oValidationResult = DTOEan.validate(oEan)
        'MsgBox(DTOEan.ValidationResultString(oValidationResult, DTOApp.current.lang))
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Dim oEan13 As DTOEan = DTOEan.Factory(TextBox1.Text)
        PictureBoxWarn.Visible = Not DTOEan.isValid(oEan13)
        RaiseEvent Changed(Me, New EventArgs)
    End Sub
End Class
