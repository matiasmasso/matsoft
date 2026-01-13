Public Class Frm_Customer_PurchaseOrders

    Private _Contact As DTOContact

    Public Sub New(oContact As DTOContact)
        MyBase.New()
        InitializeComponent()
        _Contact = oContact
    End Sub

    Private Sub Frm_Customer_PurchaseOrders_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub refresca()
        Dim oOrders As List(Of DTOPurchaseOrder) = BLL.BLLPurchaseOrders.Headers(DTOPurchaseOrder.Codis.Client, _Contact)
        Xl_PurchaseOrders1.Load(oOrders, Xl_PurchaseOrders.Modes.SingleCustomer)
    End Sub

    Private Sub Xl_PurchaseOrders1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrders1.RequestToRefresh
        refresca()
    End Sub
End Class