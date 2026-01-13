Public Class Xl_NumSegSocial
    Inherits TextBox

    ReadOnly Property Value As String
        Get
            Return MyBase.Text
        End Get
    End Property

    Public Shadows Sub Load(sValue As String)
        MyBase.Text = sValue
    End Sub
End Class
