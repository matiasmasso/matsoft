Public Class Xl_LookupGuidnom
    Inherits Xl_LookupTextboxButton

    Private _Guidnom As DTOGuidNom

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oGuidNom As DTOGuidNom)
        _Guidnom = oGuidNom
        Refresca()
    End Sub

    Public ReadOnly Property GuidNom() As DTOGuidNom
        Get
            Return _Guidnom
        End Get
    End Property


    Public Sub Clear()
        _Guidnom = Nothing
        Refresca()
    End Sub


    Private Sub Refresca()
        If _Guidnom Is Nothing Then
            MyBase.Text = ""
        Else
            MyBase.Text = _Guidnom.Nom
        End If
    End Sub

End Class
