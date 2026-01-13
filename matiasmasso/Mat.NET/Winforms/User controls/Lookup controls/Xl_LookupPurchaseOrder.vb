Public Class Xl_LookupPurchaseOrder
    Inherits Xl_LookupTextboxButton

    Private _PurchaseOrder As DTOPurchaseOrder

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event RequestToLookup(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property PurchaseOrder() As DTOPurchaseOrder
        Get
            Return _PurchaseOrder
        End Get
        Set(ByVal value As DTOPurchaseOrder)
            _PurchaseOrder = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.PurchaseOrder = Nothing
    End Sub

    Private Sub Xl_LookupPurchaseOrder_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        RaiseEvent RequestToLookup(Me, New MatEventArgs(_PurchaseOrder))
    End Sub

    Private Sub onPurchaseOrderSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _PurchaseOrder = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _PurchaseOrder Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = String.Format("{0} del {1:dd/MM/yy}", _PurchaseOrder.Num, _PurchaseOrder.Fch)
            Dim oOrders As New List(Of DTOPurchaseOrder)
            oOrders.Add(_PurchaseOrder)
            Dim oMenu_PurchaseOrder As New Menu_Pdc(oOrders)
            AddHandler oMenu_PurchaseOrder.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_PurchaseOrder.Range)
        End If
    End Sub


End Class
