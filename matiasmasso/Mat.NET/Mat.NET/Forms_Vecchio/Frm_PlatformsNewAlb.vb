Public Class Frm_PlatformsNewAlb
    Private _Customer As DTOCustomer
    Private _PurchaseOrders As List(Of DTOPurchaseOrder)
    Private _StocksAvailable As List(Of DTOStockAvailable)
    Private _AllowEvents As Boolean

    Public Sub New(oCustomer As DTOCustomer)
        MyBase.New()
        Me.InitializeComponent()
        _Customer = oCustomer
        ButtonOk.Enabled = True
    End Sub


    Private Sub Frm_PlatformsNewAlb_Load(sender As Object, e As EventArgs) Handles Me.Load
        _PurchaseOrders = BLL.BLLPurchaseOrders.PendingForPlatforms(_Customer)
        _StocksAvailable = BLL.BLLPurchaseOrders.StocksAvailableForPlatforms(_Customer)
        Xl_PurchaseOrderItemsForPlatforms1.Load(_PurchaseOrders, _StocksAvailable)
        Xl_StocksAvailable1.Load(Xl_PurchaseOrderItemsForPlatforms1.StocksAvailable)
        Xl_PlatformAlbsSumary1.Load(Xl_PurchaseOrderItemsForPlatforms1.Platforms)
        Xl_PlatformCentersSumary1.Load(Xl_PurchaseOrderItemsForPlatforms1.Deliveries)
        _AllowEvents = True
    End Sub


    Private Sub Xl_StocksAvailable1_ValueChanged(sender As Object, e As MatEventArgs) Handles Xl_StocksAvailable1.ValueChanged
        Xl_PurchaseOrderItemsForPlatforms1.selectSku(e.Argument)
    End Sub


    Private Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        Dim oDeliveries As List(Of DTODelivery) = Xl_PurchaseOrderItemsForPlatforms1.Deliveries
        Dim exs as New List(Of exception)
        If BLL_Deliveries.Update(oDeliveries, BLL.BLLSession.Current, exs) Then
            Dim sText As String = ""
            If oDeliveries.Count > 1 Then
                sText = "registrats albarans " & oDeliveries(0).Id & " - " & oDeliveries(oDeliveries.Count - 1).Id.ToString
            Else
                sText = "registrat albará " & oDeliveries(0).Id.ToString
            End If
            MsgBox(sText, MsgBoxStyle.Information)
            Me.Close()
        Else
            UIHelper.WarnError( exs, "error al desar els albarans")
        End If
    End Sub

    Private Sub Xl_PurchaseOrderItemsForPlatforms1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PurchaseOrderItemsForPlatforms1.AfterUpdate
        If _AllowEvents Then
            Xl_PlatformAlbsSumary1.Load(Xl_PurchaseOrderItemsForPlatforms1.Platforms)
        End If
    End Sub

    Private Sub Xl_PlatformAlbsSumary1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PlatformAlbsSumary1.AfterUpdate
        'update centers
        'Xl_PlatformCentersSumary1.Load(Xl_PurchaseOrderItemsForPlatforms1.Deliveries)


        Dim oItems As New List(Of DTOPurchaseOrder)
        Dim oPlatforms As List(Of DTOCustomerPlatform) = Xl_PlatformAlbsSumary1.ItemsChecked
        For Each oItem As DTOPurchaseOrder In _PurchaseOrders
            If oPlatforms.Exists(Function(x) x.Guid.Equals(oItem.Platform.Guid)) Then
                oItems.Add(oItem)
            End If
        Next

        For Each oStockAvailable As DTOStockAvailable In _StocksAvailable
            oStockAvailable.AvailableStock = oStockAvailable.OriginalStock
        Next

        Xl_PurchaseOrderItemsForPlatforms1.Load(oItems, _StocksAvailable)
        Xl_StocksAvailable1.Load(Xl_PurchaseOrderItemsForPlatforms1.StocksAvailable)
    End Sub

    Private Sub Xl_PlatformAlbsSumary1_SelectionChanged(sender As Object, e As MatEventArgs) Handles Xl_PlatformAlbsSumary1.SelectionChanged
        Xl_PurchaseOrderItemsForPlatforms1.selectPlatform(e.Argument)
    End Sub

    Private Sub Xl_PlatformCentersSumary1_AfterUpdate(sender As Object, e As MatEventArgs) Handles Xl_PlatformCentersSumary1.AfterUpdate
        Dim oItems As New List(Of DTOPurchaseOrder)
        Dim oCenters As List(Of DTOContact) = Xl_PlatformCentersSumary1.ItemsChecked
        For Each oItem As DTOPurchaseOrder In _PurchaseOrders
            If oCenters.Exists(Function(x) x.Guid.Equals(oItem.Customer.Guid)) Then
                oItems.Add(oItem)
            End If
        Next

        For Each oStockAvailable As DTOStockAvailable In _StocksAvailable
            oStockAvailable.AvailableStock = oStockAvailable.OriginalStock
        Next

        Xl_PurchaseOrderItemsForPlatforms1.Load(oItems, _StocksAvailable)
        Xl_StocksAvailable1.Load(Xl_PurchaseOrderItemsForPlatforms1.StocksAvailable)
    End Sub


End Class