Public Class Frm_StoreLocator

    Private _Product As DTOProduct
    Private _Storelocator As DTOStoreLocator3
    Public Sub New(oProduct As DTOProduct)
        MyBase.New
        InitializeComponent()
        _Product = oProduct
    End Sub

    Private Async Sub Frm_StoreLocator_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        If Not _Product.IsLoaded Then
            _Product = Await FEB.Product.Find(exs, _Product.Guid)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        End If

        Me.Text = String.Format("Store locator {0}", _Product.FullNom(Current.Session.Lang))
        If _Product.sourceCod = DTOProduct.SourceCods.Sku Then
            Dim oSku = Await FEB.ProductSku.Find(exs, _Product.Guid)
            If exs.Count = 0 Then
                _Product = oSku.Category
            Else
                UIHelper.WarnError(exs)
                Me.Close()
            End If
        End If

        LabelCaption.Text = String.Format("Clients que han comprat alguna vegada {0}", _Product.FullNom(Current.Session.Lang))
        _Storelocator = Await FEB.StoreLocator.All(exs, _Product, Current.Session.Lang)
        ProgressBar1.Visible = False
        If exs.Count = 0 Then
            LoadCountries()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub LoadCountries()
        Dim oCountries = _Storelocator.Offline.Countries
        With ComboBoxCountry
            .DataSource = oCountries
            .DisplayMember = "Nom"
            .SelectedItem = oCountries.FirstOrDefault(Function(x) x.Guid.Equals(_Storelocator.Offline.DefaultCountry.Guid))
        End With
    End Sub

    Private Sub LoadZonas()
        Dim oCountry As DTOStoreLocator3.Country = ComboBoxCountry.SelectedItem
        With ComboBoxZona
            .DataSource = oCountry.Zonas
            .DisplayMember = "Nom"
            .SelectedItem = oCountry.Zonas.FirstOrDefault(Function(x) x.Guid.Equals(oCountry.DefaultZona.Guid))
        End With
    End Sub

    Private Sub LoadLocations()
        Dim oZona As DTOStoreLocator3.Zona = ComboBoxZona.SelectedItem
        With ComboBoxLocation
            .DataSource = oZona.Locations
            .DisplayMember = "Nom"
            .SelectedItem = oZona.Locations.FirstOrDefault(Function(x) x.Guid.Equals(oZona.DefaultLocation.Guid))
        End With
    End Sub

    Private Sub LoadDistributors()
        Dim oLocation As DTOStoreLocator3.Location = ComboBoxLocation.SelectedItem
        Xl_StoreLocatorDistributors1.Load(oLocation.Distributors)
    End Sub

    Private Sub ComboBoxCountry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxCountry.SelectedIndexChanged
        LoadZonas()
    End Sub

    Private Sub ComboBoxZona_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxZona.SelectedIndexChanged
        LoadLocations()
    End Sub

    Private Sub ComboBoxLocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxLocation.SelectedIndexChanged
        LoadDistributors()
    End Sub
End Class