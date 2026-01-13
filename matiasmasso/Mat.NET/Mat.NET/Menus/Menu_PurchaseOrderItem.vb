Public Class Menu_PurchaseOrderItem

    Private _PurchaseOrderItems As List(Of DTOPurchaseOrderItem)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPurchaseOrderItem As DTOPurchaseOrderItem)
        MyBase.New()
        _PurchaseOrderItems = New List(Of DTOPurchaseOrderItem)
        _PurchaseOrderItems.Add(oPurchaseOrderItem)
    End Sub

    Public Sub New(ByVal oPurchaseOrderItems As List(Of DTOPurchaseOrderItem))
        MyBase.New()
        _PurchaseOrderItems = oPurchaseOrderItems
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
                MenuItem_Order(), _
                MenuItem_ShowSku()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_ShowSku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "article"
        If _PurchaseOrderItems.First.Sku Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = True
            Dim oArt As New Art(_PurchaseOrderItems.First.Sku.Guid)
            Dim oMenu_Art As New Menu_Art(oArt)
            oMenuItem.DropDownItems.AddRange(oMenu_Art.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Order() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "comanda"
        If _PurchaseOrderItems.First.PurchaseOrder Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Enabled = True
            Dim oMenu As New Menu_PurchaseOrder(_PurchaseOrderItems.First.PurchaseOrder)
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_PurchaseOrderItem(_PurchaseOrderItem)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub



    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

