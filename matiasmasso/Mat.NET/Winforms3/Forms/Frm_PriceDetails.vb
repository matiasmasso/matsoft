Public Class Frm_PriceDetails
    Private _Customer As DTOCustomer
    Private _Sku As DTOProductSku
    Private _Fch As Date
    Private _Allowevents As Boolean

    Public Sub New(oCustomer As DTOCustomer, oSku As DTOProductSku, Optional DtFch As Date = Nothing)
        MyBase.New
        InitializeComponent()
        _Customer = oCustomer
        _Sku = oSku
        _Fch = IIf(DtFch = Nothing, DTO.GlobalVariables.Today(), DtFch)
    End Sub

    Private Async Sub Frm_PriceDetails_Load(sender As Object, e As EventArgs) Handles Me.Load
        Dim exs As New List(Of Exception)
        Await Xl_Contact21.Load(exs, _Customer)
        Dim oProducts As New List(Of DTOProduct)
        If _Sku IsNot Nothing Then oProducts.Add(_Sku)
        Xl_LookupProduct1.Load(oProducts, DTOProduct.SelectionModes.SelectSku)
        DateTimePicker1.Value = _Fch

        refresca()
        _Allowevents = True
    End Sub

    Private Sub refresca()
        Dim oCustomer = Xl_Contact21.Customer
        Dim oSku = Xl_LookupProduct1.Product
        Dim dtFch = DateTimePicker1.Value
        Xl_PriceAnalysis1.Load(oCustomer, oSku, dtFch)
    End Sub

    Private Sub ControlChanged(sender As Object, e As EventArgs) Handles _
        Xl_Contact21.AfterUpdate,
         Xl_LookupProduct1.AfterUpdate,
          DateTimePicker1.ValueChanged

        If _Allowevents Then refresca()
    End Sub
End Class