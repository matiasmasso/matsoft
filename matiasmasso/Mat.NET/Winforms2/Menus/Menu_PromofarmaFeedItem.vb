Public Class Menu_PromofarmaFeedItem
    Inherits Menu_Base

    Private _Items As List(Of Integracions.Promofarma.Feed.Item)
    Private _Item As Integracions.Promofarma.Feed.Item

    Public Event EnabledChanged(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oItems As List(Of Integracions.Promofarma.Feed.Item))
        MyBase.New()
        _Items = oItems
        If _Items IsNot Nothing Then
            If _Items.Count > 0 Then
                _Item = _Items.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oItem As Integracions.Promofarma.Feed.Item)
        MyBase.New()
        _Item = oItem
        _Items = New List(Of Integracions.Promofarma.Feed.Item)
        If _Item IsNot Nothing Then
            _Items.Add(_Item)
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
        oMenuItem.Enabled = _Items.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Enable() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Activar"
        oMenuItem.Enabled = _Items.Any(Function(x) x.Enabled = False)
        AddHandler oMenuItem.Click, AddressOf Do_Enable
        Return oMenuItem
    End Function

    Private Function MenuItemDisable() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Desactivar"
        oMenuItem.Enabled = _Items.Any(Function(x) x.Enabled = True)
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
        Dim oFrm As New Frm_PromofarmaFeedItem(_Item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_LangText()
        Dim oFrm As New Frm_ProductDescription(New DTOProductSku(_Item.Guid))
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
        If Await FEB.PromofarmaFeedItems.Enable(exs, _Items, blEnable) Then
            For Each item In _Items
                item.Enabled = blEnable
            Next
            RaiseEvent EnabledChanged(Me, New MatEventArgs(_Items))
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

End Class
