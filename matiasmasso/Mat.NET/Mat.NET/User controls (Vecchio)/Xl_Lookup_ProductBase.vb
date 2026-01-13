Public Class Xl_Lookup_ProductBase
    Inherits Xl_LookupTextboxButton

    Private _Catalog As ProductCatalog
    Private _Product As DTOProduct0

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Sub Load(oCatalog As ProductCatalog, value As DTOProduct0)
        _Catalog = oCatalog
        _Product = value
        refresca()
    End Sub

    Public Shadows Sub LoadOld(oCatalog As ProductCatalog, value As Product)
        _Catalog = oCatalog
        Dim oSourceCod As DTOProduct0.SourceCods = DTOProduct0.SourceCods.NotSet
        If value Is Nothing Then
        Else
            Select Case value.ValueType
                Case MaxiSrvr.Product.ValueTypes.Tpa
                    oSourceCod = DTOProduct0.SourceCods.Brand
                Case MaxiSrvr.Product.ValueTypes.Stp
                    oSourceCod = DTOProduct0.SourceCods.Category
                Case MaxiSrvr.Product.ValueTypes.Art
                    oSourceCod = DTOProduct0.SourceCods.SKU
            End Select
            _Product = BLL_Product.FromGuid(value.Guid, oSourceCod, value.Nom(BLL.BLLSession.Current.Lang))
        End If
        refresca()
    End Sub

    Public Property Product() As DTOProduct0
        Get
            Return _Product
        End Get
        Set(ByVal value As DTOProduct0)
            _Product = value
            refresca()
        End Set
    End Property

    Private Sub refresca()

        If _Product Is Nothing Then
            MyBase.Text = BLL.BLLSession.Current.Lang.Tradueix("(todas las marcas)", "(totes les marques)", "(any brand)")
        Else
            MyBase.Text = BLL_Product.FullNom(_Product)
        End If
    End Sub

    Public Sub Clear()
        Me.Product = Nothing
        refresca()
    End Sub

    Private Sub Xl_LookupProduct_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Catalog(_Catalog, _Product)
        AddHandler oFrm.OnItemSelected, AddressOf OnItemSelected
        oFrm.Show()
    End Sub

    Private Sub OnItemSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Product = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub


End Class

