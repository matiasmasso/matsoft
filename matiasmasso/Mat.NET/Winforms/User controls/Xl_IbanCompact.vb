Public Class Xl_IbanCompact
    Inherits TextBox

    ReadOnly Property Iban As DTOIban
        Get
            Return DTOIban.Factory(MyBase.Text)
        End Get
    End Property

    Public Shadows Sub Load(oIban As DTOIban)
        MyBase.Text = DTOIban.Formated(oIban)
    End Sub
End Class
