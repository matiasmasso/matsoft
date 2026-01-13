Public Class Menu_ConsumerTicket

    Inherits Menu_Base

    Private _ConsumerTickets As List(Of DTOConsumerTicket)
    Private _ConsumerTicket As DTOConsumerTicket

    Public Sub New(ByVal oConsumerTickets As List(Of DTOConsumerTicket))
        MyBase.New()
        _ConsumerTickets = oConsumerTickets
        If _ConsumerTickets IsNot Nothing Then
            If _ConsumerTickets.Count > 0 Then
                _ConsumerTicket = _ConsumerTickets.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oConsumerTicket As DTOConsumerTicket)
        MyBase.New()
        _ConsumerTicket = oConsumerTicket
        _ConsumerTickets = New List(Of DTOConsumerTicket)
        If _ConsumerTicket IsNot Nothing Then
            _ConsumerTickets.Add(_ConsumerTicket)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Return())
        MyBase.AddMenuItem(MenuItem_PurchaseOrder())
        MyBase.AddMenuItem(MenuItem_Spv())
        MyBase.AddMenuItem(MenuItem_Delivery())
        MyBase.AddMenuItem(MenuItem_Invoice())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _ConsumerTickets.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Return() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Registrar devolució"
        oMenuItem.Enabled = _ConsumerTickets.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Return
        Return oMenuItem
    End Function

    Private Function MenuItem_PurchaseOrder() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comanda"
        Dim oPurchaseOrders = _ConsumerTickets.Select(Function(x) x.PurchaseOrder).Where(Function(y) y IsNot Nothing).ToList()
        If oPurchaseOrders.Count = 0 Then
            oMenuItem.Enabled = False
        Else
            Dim oMenu As New Menu_PurchaseOrder(oPurchaseOrders)
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Spv() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reparació"
        Dim oSpvs = _ConsumerTickets.Select(Function(x) x.Spv).Where(Function(y) y IsNot Nothing).ToList()
        If oSpvs.Count = 0 Then
            oMenuItem.Enabled = False
        Else
            Dim oMenu As New Menu_Spv(oSpvs)
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Delivery() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Albarà"
        Dim oDeliveries = _ConsumerTickets.Select(Function(x) x.Delivery).Where(Function(y) y IsNot Nothing).ToList()
        If oDeliveries.Count = 0 Then
            oMenuItem.Enabled = False
        Else
            Dim oMenu As New Menu_Delivery(oDeliveries)
            AddHandler oMenu.AfterUpdate, AddressOf RefreshRequest
            oMenuItem.DropDownItems.AddRange(oMenu.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Invoice() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Factura"
        Dim oDeliveries = _ConsumerTickets.Select(Function(x) x.Delivery).ToList()
        If oDeliveries.Count = 0 Then
            oMenuItem.Enabled = False
        Else
            Dim oInvoices = oDeliveries.Select(Function(x) x.invoice).Where(Function(y) y IsNot Nothing).ToList()
            If oInvoices.Count = 0 Then
                oMenuItem.Enabled = False
            Else
                Dim oMenu As New Menu_Invoice(oInvoices)
                oMenuItem.DropDownItems.AddRange(oMenu.Range)
            End If
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ConsumerTicket(_ConsumerTicket)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub
    Private Sub Do_Return(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ConsumerTicketReturn(_ConsumerTicket)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.ConsumerTicket.Delete(exs, _ConsumerTickets.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

