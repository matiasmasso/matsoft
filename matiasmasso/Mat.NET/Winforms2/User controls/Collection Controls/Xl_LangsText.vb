Public Class Xl_LangsText
    Private _value As DTOLangText

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Shadows Sub Load(value As DTOLangText)
        _value = value
        Xl_LangsToTranslate1.Load(_value, DTOLang.ESP)
        Xl_LangsToTranslate2.Load(_value, DTOLang.POR)

        Dim oLang As DTOLang = Xl_LangsToTranslate1.Value
        If value Is Nothing Then
            TextBox1.Clear()
        Else
            TextBox1.Text = _value.Text(oLang)
        End If

        oLang = Xl_LangsToTranslate2.Value
        If value Is Nothing Then
            TextBox2.Clear()
        Else
            TextBox2.Text = _value.Text(oLang)
        End If
    End Sub

    Public ReadOnly Property Value As DTOLangText
        Get
            Return _value
        End Get
    End Property

    Private Sub Xl_LangsToTranslateDTOLangText1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LangsToTranslate1.ValueChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate1.Value
        If _value IsNot Nothing Then
            TextBox1.Text = _value.Text(oLang)
        End If
    End Sub

    Private Sub Xl_LangsToTranslate2_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_LangsToTranslate2.ValueChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate2.Value
        If _value IsNot Nothing Then
            TextBox2.Text = _value.Text(oLang)
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate1.Value
        DTOLangText.SetText(_value, oLang, TextBox1.Text)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
    End Sub

    Private Sub TextBox2_TextChanged(sender As Object, e As EventArgs) Handles TextBox2.TextChanged
        Dim oLang As DTOLang = Xl_LangsToTranslate2.Value
        DTOLangText.SetText(_value, oLang, TextBox2.Text)
        RaiseEvent AfterUpdate(Me, New MatEventArgs(_value))
    End Sub

End Class
