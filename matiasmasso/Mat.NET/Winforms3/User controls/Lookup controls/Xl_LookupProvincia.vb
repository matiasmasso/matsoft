Public Class Xl_LookupProvincia
    Inherits Xl_LookupTextboxButton

    Private _Country As DTOCountry
    Private _AreaProvincia As DTOAreaProvincia
    Private _SelMode As DTOArea.SelectModes = DTOArea.SelectModes.SelectAny
    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property AreaProvincia() As DTOAreaProvincia
        Get
            Return _AreaProvincia
        End Get
        Set(ByVal value As DTOAreaProvincia)
            _AreaProvincia = value
            If _AreaProvincia IsNot Nothing AndAlso _AreaProvincia.Regio IsNot Nothing Then
                _Country = _AreaProvincia.Regio.Country
            End If
            refresca()
        End Set
    End Property

    Public Shadows Property Country() As DTOCountry
        Get
            Return _Country
        End Get
        Set(ByVal value As DTOCountry)
            _Country = value
            refresca()
        End Set
    End Property

    Public Property SelMode As DTOArea.SelectModes
        Get
            Return _SelMode
        End Get
        Set(value As DTOArea.SelectModes)
            _SelMode = value
        End Set
    End Property


    Public Sub Clear()
        Me.AreaProvincia = Nothing
    End Sub



    Private Sub onAreaProvinciaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _AreaProvincia = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _AreaProvincia Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _AreaProvincia.Nom
            Dim oMenu_AreaProvincia As New Menu_AreaProvincia(_AreaProvincia)
            AddHandler oMenu_AreaProvincia.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_AreaProvincia.Range)
        End If
    End Sub

End Class
