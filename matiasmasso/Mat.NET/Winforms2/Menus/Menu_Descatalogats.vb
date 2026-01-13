Public Class Menu_Descatalogats
    Inherits Menu_Base

    Private _Descatalogats As List(Of DTODescatalogat)
    Private _Descatalogat As DTODescatalogat

    Public Sub New(ByVal oDescatalogats As List(Of DTODescatalogat))
        MyBase.New()
        _Descatalogats = oDescatalogats
        If _Descatalogats IsNot Nothing Then
            If _Descatalogats.Count > 0 Then
                _Descatalogat = _Descatalogats.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oDescatalogat As DTODescatalogat)
        MyBase.New()
        _Descatalogat = oDescatalogat
        _Descatalogats = New List(Of DTODescatalogat)
        If _Descatalogat IsNot Nothing Then
            _Descatalogats.Add(_Descatalogat)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Sku())
        MyBase.AddMenuItem(MenuItem_Confirm())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Sku() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Article..."
        oMenuItem.Enabled = _Descatalogats.Count = 1
        If _Descatalogats.Count = 1 Then
            Dim oSku As New DTOProductSku(_Descatalogats.First().Guid)
            Dim oMenuSku As New Menu_ProductSku(oSku)
            oMenuItem.DropDownItems.AddRange(oMenuSku.Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_Confirm() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Reconfirmar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Confirm
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSku As New DTOProductSku(_Descatalogat.Guid)
        Dim oFrm As New Frm_ProductSku(oSku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Confirm(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSkuGuids = _Descatalogats.Select(Function(x) x.Guid).ToList()
        MyBase.ToggleProgressBarRequest(True)
        If Await FEB.ProductSkus.ConfirmDescatalogats(exs, oSkuGuids) Then
            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
        Else
            MyBase.ToggleProgressBarRequest(False)
            UIHelper.WarnError(exs)
        End If
    End Sub



End Class


