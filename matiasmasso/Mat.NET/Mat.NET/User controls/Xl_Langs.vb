Public Class Xl_Langs
    Private _AllowEvents As Boolean

    Public Event AfterUpdate(sender As Object, e As MatEventArgs)

    Public Property Value As DTOLang
        Get
            Dim sLang As String = ComboBox1.SelectedItem
            Dim retval As DTOLang = DTOLang.FromTag(sLang)
            Return retval
        End Get
        Set(value As DTOLang)
            If value IsNot Nothing Then
                ComboBox1.SelectedItem = value.Id.ToString
                _AllowEvents = True
            End If
        End Set
    End Property

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If _AllowEvents Then
            Dim oLang As DTOLang = Value
            RaiseEvent AfterUpdate(Me, New MatEventArgs(oLang))
        End If
    End Sub
End Class
