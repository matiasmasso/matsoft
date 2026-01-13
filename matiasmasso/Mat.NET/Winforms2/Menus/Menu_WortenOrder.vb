Public Class Menu_WortenOrder
    Inherits Menu_Base

    Private _Orders As List(Of DTO.Integracions.Worten.OrderClass)
    Private _Order As DTO.Integracions.Worten.OrderClass

    Public Sub New(ByVal oOrders As List(Of DTO.Integracions.Worten.OrderClass))
        MyBase.New()
        _Orders = oOrders
        If _Orders IsNot Nothing Then
            If _Orders.Count > 0 Then
                _Order = _Orders.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oOrder As DTO.Integracions.Worten.OrderClass)
        MyBase.New()
        _Order = oOrder
        _Orders = New List(Of DTO.Integracions.Worten.OrderClass)
        If _Order IsNot Nothing Then
            _Orders.Add(_Order)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_AcceptAllOrderLines())
        MyBase.AddMenuItem(MenuItem_PdfDoc())
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

    Private Function MenuItem_AcceptAllOrderLines() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Acceptar totes les linies"
        oMenuItem.Enabled = _Orders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_AcceptAllOrderLines
        Return oMenuItem
    End Function

    Private Function MenuItem_ConsumerTicket() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Ticket consumidor"
        oMenuItem.Enabled = _Orders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_ConsumerTicketFactory
        Return oMenuItem
    End Function

    Private Function MenuItem_PdfDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Document Pdf"
        oMenuItem.Enabled = _Orders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_PdfDoc
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_WortenOrder(_Order)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_PdfDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oByteArray = Await FEB.Worten.OrderDocument(exs, _Order)
        If exs.Count = 0 Then
            UIHelper.ShowPdf(oByteArray)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_AcceptAllOrderLines(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)

        If Await FEB.Worten.AcceptAllOrderLines(exs, _Order) Then
            MyBase.ToggleProgressBarRequest(False)
            MsgBox("Comanda acceptada correctament", MsgBoxStyle.Information)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_ConsumerTicketFactory(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        MyBase.ToggleProgressBarRequest(True)
        Dim oConsumerTicket = Await FEB.Worten.ConsumerTicket(exs, _Order, Current.Session.User)
        If exs.Count = 0 Then
            MyBase.ToggleProgressBarRequest(False)
            Dim oFrm As New Frm_ConsumerTicket(oConsumerTicket)
            oFrm.Show()
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub



End Class


