Public Class Xl_Lookup_Zona
    Inherits Xl_LookupTextboxButton

    Private _Zona As DTOZona

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Zona() As DTOZona
        Get
            Return _Zona
        End Get
        Set(ByVal value As DTOZona)
            _Zona = value
            If _Zona Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Zona.FullNom(DTOApp.current.lang)
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Zona = Nothing
    End Sub

    Private Sub Xl_Lookup_Zona_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_Zona(_Zona)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupZona_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Geo(DTOArea.SelectModes.SelectZona, _Zona)
        AddHandler oFrm.onItemSelected, AddressOf onZonaSelected
        oFrm.Show()
    End Sub

    Private Sub onZonaSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oZona As DTOZona = e.Argument
        If oZona IsNot Nothing Then
            _Zona = oZona
            MyBase.Text = _Zona.FullNom(DTOApp.current.lang)
            RaiseEvent AfterUpdate(Me, e)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        MyBase.Text = _Zona.FullNom(DTOApp.current.lang)
    End Sub
End Class
