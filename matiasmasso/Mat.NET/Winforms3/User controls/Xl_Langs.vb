Public Class Xl_Langs
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property Value As DTOLang
        Get
            Dim retval As DTOLang = Nothing
            If ComboBox1.SelectedItem IsNot Nothing Then
                Dim sLang As String = ComboBox1.SelectedItem
                retval = DTOLang.Factory(sLang)
            End If
            Return retval
        End Get
        Set(value As DTOLang)
            If value IsNot Nothing Then
                ComboBox1.SelectedItem = value.Id.ToString
            End If
            _AllowEvents = True
        End Set
    End Property

    Public Shadows Property Enabled As Boolean
        Get
            Return ComboBox1.Enabled
        End Get
        Set(value As Boolean)
            ComboBox1.Enabled = value
        End Set
    End Property

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            Dim oLang As DTOLang = Value
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oLang))
        End If
    End Sub
End Class
