

Public Class Frm_Proveidor_PriceLists

    Private _Proveidor As DTOProveidor
    Private _AllowEvents As Boolean

    Public Sub New(oProveidor As DTOProveidor)
        MyBase.New()
        Me.InitializeComponent()
        _Proveidor = oProveidor
        Me.Text = BLL.BLLContact.NomComercialOrDefault(_Proveidor) & ": Tarifa de preus"
        refresca()
        _AllowEvents = True
    End Sub

    Private Sub refresca()
        Dim oPriceLists As List(Of DTOPriceList_Supplier) = BLL.BLLPriceLists_Supplier.FromProveidor(_Proveidor)
        Xl_PriceLists_Suppliers1.Load(oPriceLists)
    End Sub

    Private Sub Xl_PriceLists_Suppliers1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Suppliers1.RequestToAddNew
        Dim oPriceList As DTOPriceList_Supplier = BLL.BLLPriceList_Supplier.NewPriceList(_Proveidor)
        Dim oFrm As New Frm_PriceList_Supplier(oPriceList)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_PriceLists_Suppliers1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PriceLists_Suppliers1.RequestToRefresh
        refresca()
    End Sub

End Class