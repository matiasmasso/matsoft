Public Class Menu_MarketplaceSku

    Inherits Menu_Base

    Private _MarketplaceSkus As List(Of DTOMarketplaceSku)
    Private _MarketplaceSku As DTOMarketplaceSku

    Public Event EnabledChanged(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oMarketplaceSkus As List(Of DTOMarketplaceSku))
        MyBase.New()
        _MarketplaceSkus = oMarketplaceSkus
        If _MarketplaceSkus IsNot Nothing Then
            If _MarketplaceSkus.Count > 0 Then
                _MarketplaceSku = _MarketplaceSkus.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oMarketplaceSku As DTOMarketplaceSku)
        MyBase.New()
        _MarketplaceSku = oMarketplaceSku
        _MarketplaceSkus = New List(Of DTOMarketplaceSku)
        If _MarketplaceSku IsNot Nothing Then
            _MarketplaceSkus.Add(_MarketplaceSku)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_LangText())
        MyBase.AddMenuItem(MenuItem_Enable())
        MyBase.AddMenuItem(MenuItemDisable())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _MarketplaceSkus.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Enable() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Activar"
        oMenuItem.Enabled = _MarketplaceSkus.Any(Function(x) x.Enabled = False)
        AddHandler oMenuItem.Click, AddressOf Do_Enable
        Return oMenuItem
    End Function

    Private Function MenuItemDisable() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desactivar"
        oMenuItem.Enabled = _MarketplaceSkus.Any(Function(x) x.Enabled = True)
        AddHandler oMenuItem.Click, AddressOf Do_Disable
        Return oMenuItem
    End Function


    Private Function MenuItem_LangText() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = Current.Session.Tradueix("nombres y descripciones", "noms i descripcions", "names & descriptions")
        AddHandler oMenuItem.Click, AddressOf Do_LangText
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_MarketplaceSku(_MarketplaceSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub


    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(New DTOProductSku(_MarketplaceSku.Sku.Guid))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Enable()
        Await Enable(True)
    End Sub
    Private Async Sub Do_Disable()
        Await Enable(False)
    End Sub


    Private Async Function Enable(blEnable As Boolean) As Task
        Dim exs As New List(Of Exception)
        If Await FEB.MarketPlace.EnableSkus(exs, _MarketplaceSkus, blEnable) Then
            For Each item In _MarketplaceSkus
                item.Enabled = blEnable
            Next
            RaiseEvent EnabledChanged(Me, New MatEventArgs(_MarketplaceSkus))
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class


