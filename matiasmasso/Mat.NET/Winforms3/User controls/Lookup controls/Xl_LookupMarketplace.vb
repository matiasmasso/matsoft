Public Class Xl_LookupMarketplace
    Inherits Xl_LookupTextboxButton

    Private _Marketplace As DTOMarketPlace

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Property Marketplace() As DTOMarketPlace
        Get
            Return _Marketplace
        End Get
        Set(ByVal value As DTOMarketPlace)
            _Marketplace = value
            If _Marketplace Is Nothing Then
                MyBase.Text = ""
            Else
                MyBase.Text = _Marketplace.Nom
            End If
        End Set
    End Property

    Public Sub Clear()
        Me.Marketplace = Nothing
    End Sub

    Private Sub Xl_Lookup_Marketplacee_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Doubleclick
        Dim oFrm As New Frm_MarketPlace(_Marketplace)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Xl_LookupMarketplace_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_MarketPlaces(_Marketplace, DTO.Defaults.SelectionModes.selection)
        AddHandler oFrm.itemSelected, AddressOf onMarketplaceSelected
        oFrm.Show()
    End Sub

    Private Sub onMarketplaceSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        If TypeOf (e.Argument) Is DTOMarketPlace Then
            _Marketplace = e.Argument
            MyBase.Text = _Marketplace.Nom
            RaiseEvent AfterUpdate(Me, New MatEventArgs(_Marketplace))
        End If
    End Sub

    Private Sub RefreshRequest()
        MyBase.Text = _Marketplace.Nom
    End Sub
End Class

