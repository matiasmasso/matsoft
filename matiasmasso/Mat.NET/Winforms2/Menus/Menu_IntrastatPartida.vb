Public Class Menu_IntrastatPartida
    Inherits Menu_Base

    Private _IntrastatPartidas As List(Of DTOIntrastat.Partida)
    Private _IntrastatPartida As DTOIntrastat.Partida

    Public Sub New(ByVal oIntrastatPartidas As List(Of DTOIntrastat.Partida))
        MyBase.New()
        _IntrastatPartidas = oIntrastatPartidas
        If _IntrastatPartidas IsNot Nothing Then
            If _IntrastatPartidas.Count > 0 Then
                _IntrastatPartida = _IntrastatPartidas.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oIntrastatPartida As DTOIntrastat.Partida)
        MyBase.New()
        _IntrastatPartida = oIntrastatPartida
        _IntrastatPartidas = New List(Of DTOIntrastat.Partida)
        If _IntrastatPartida IsNot Nothing Then
            _IntrastatPartidas.Add(_IntrastatPartida)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())

        If TypeOf _IntrastatPartida.Tag Is DTOInvoice Then
            Dim oInvoice As DTOInvoice = _IntrastatPartida.Tag
            Dim oMenu_Fra As New Menu_Invoice({oInvoice}.ToList)
            Dim t As New ToolStripMenuItem("factura...")
            MyBase.AddMenuItem(t)
            t.DropDownItems.AddRange(oMenu_Fra.Range)

        ElseIf TypeOf _IntrastatPartida.Tag Is DTODelivery Then
            Dim oDelivery As DTODelivery = _IntrastatPartida.Tag
            Dim oDeliveries As New List(Of DTODelivery)
            oDeliveries.Add(oDelivery)
            Dim oMenu_Delivery As New Menu_Delivery(oDeliveries)
            Dim t As New ToolStripMenuItem("albarà...")
            MyBase.AddMenuItem(t)
            t.DropDownItems.AddRange(oMenu_Delivery.Range)
        End If

        MyBase.AddMenuItem(MenuItem_CodiMercancia())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _IntrastatPartidas.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_CodiMercancia() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Codi Mercancia"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_CodiMercancia
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_IntrastatPartida(_IntrastatPartida)
        oFrm.Show()
    End Sub


    Private Sub Do_CodiMercancia(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_CodiMercancia(_IntrastatPartida.CodiMercancia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub




End Class
