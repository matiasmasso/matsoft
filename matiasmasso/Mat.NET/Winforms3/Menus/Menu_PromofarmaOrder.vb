Public Class Menu_PromofarmaOrder
    Inherits Menu_Base

    Private _Orders As List(Of DTO.Integracions.Promofarma.Order)
    Private _Order As DTO.Integracions.Promofarma.Order

    Public Sub New(ByVal oOrders As List(Of DTO.Integracions.Promofarma.Order))
        MyBase.New()
        _Orders = oOrders
        If _Orders IsNot Nothing Then
            If _Orders.Count > 0 Then
                _Order = _Orders.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oOrder As DTO.Integracions.Promofarma.Order)
        MyBase.New()
        _Order = oOrder
        _Orders = New List(Of DTO.Integracions.Promofarma.Order)
        If _Order IsNot Nothing Then
            _Orders.Add(_Order)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_ConsumerTicket())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Orders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_ConsumerTicket() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ticket de consumidor"
        AddHandler oMenuItem.Click, AddressOf Do_ConsumerTicket
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PromofarmaOrder(_Order)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_ConsumerTicket()
        Dim exs As New List(Of Exception)
        Dim value = Await FEB.PromofarmaOrder.ConsumerTicket(exs, _Order, Current.Session.User)
        Dim oFrm As New Frm_ConsumerTicket(value)
        oFrm.Show()
    End Sub



End Class


