Public Class Menu_PriceListItem_Supplier
    Private _Item As DTOPriceListItem_Supplier
    Private _Items As List(Of DTOPriceListItem_Supplier)

    'per entrades a cataleg
    Property LastPricelistCustomer As DTOPricelistCustomer
    Property LastCategory As DTOProductCategory


    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal value As DTOPriceListItem_Supplier)
        MyBase.New()
        _Item = value
        _Items = New List(Of DTOPriceListItem_Supplier)
        _Items.Add(_Item)
    End Sub

    Public Sub New(ByVal values As List(Of DTOPriceListItem_Supplier))
        MyBase.New()
        _Items = values
        _Item = _Items(0)
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_ShowTarifa(),
        MenuItem_Art(),
        MenuItem_UpdateCustomerPriceList(),
        MenuItem_AddToCataleg()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_ShowTarifa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Tarifa completa"
        AddHandler oMenuItem.Click, AddressOf Do_ShowTarifa
        Return oMenuItem
    End Function

    Private Function MenuItem_Art() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "article"
        If _Item.Sku Is Nothing Then
            oMenuItem.Enabled = False
        Else
            Dim oSku As DTOProductSku = _Item.Sku
            oMenuItem.DropDownItems.AddRange(New Menu_ProductSku(oSku).Range)
        End If
        Return oMenuItem
    End Function

    Private Function MenuItem_AddToCataleg() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "afegir al cataleg"
        oMenuItem.Enabled = (_Item.Sku Is Nothing)
        AddHandler oMenuItem.Click, AddressOf Do_AddToCataleg
        Return oMenuItem
    End Function

    Private Function MenuItem_UpdateCustomerPriceList() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "actualitzar tarifes de venda"
        oMenuItem.Enabled = (_Item.Sku IsNot Nothing)
        AddHandler oMenuItem.Click, AddressOf Do_UpdateCustomerPriceList
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceListItem_Supplier(_Item)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_ShowTarifa()
        Dim oFrm As New Frm_PriceList_Supplier(_Item.Parent)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AddToCataleg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceListSupplierToCataleg(_Item, _LastPricelistCustomer, _LastCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_UpdateCustomerPriceList(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceLists_Customers(DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.OnItemSelected, AddressOf OnCustomerPriceListSelected
        oFrm.Show()
    End Sub


    Private Async Sub OnCustomerPriceListSelected(sender As Object, e As MatEventArgs)
        Dim exs As New List(Of Exception)
        Dim oPriceList As DTOPricelistCustomer = e.Argument
        If FEB2.PriceListCustomer.Load(exs, oPriceList, True) Then
            For Each src As DTOPriceListItem_Supplier In _Items
                Await FEB2.PriceListItemCustomer.AddFromPriceListItemSupplier(oPriceList, src, exs)
            Next

            If exs.Count = 0 Then
                If Await FEB2.PriceListCustomer.Update(exs, oPriceList) Then
                    MsgBox("actualitzats " & _Items.Count & " productes", MsgBoxStyle.Information)
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


