Public Class Xl_LookupCountry

    Inherits Xl_LookupTextboxButton

    Private _Country As DTOCountry

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Country() As DTOCountry
        Get
            Return _Country
        End Get
        Set(ByVal value As DTOCountry)
            _Country = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Country = Nothing
    End Sub

    Private Sub Xl_LookupCountry_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim exs As New List(Of Exception)
        If _Country IsNot Nothing Then FEB.Country.Load(_Country, exs)
        Dim oFrm As New Frm_Countries(_Country, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onCountrySelected
        oFrm.Show()
    End Sub

    Private Sub onCountrySelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Country = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Country Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = DTOCountry.NomTraduit(_Country, Current.Session.Lang)
            Dim oMenu_Country As New Menu_Country(_Country)
            AddHandler oMenu_Country.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Country.Range)
        End If
    End Sub


End Class

