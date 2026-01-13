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
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Art(), _
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

    Private Function MenuItem_Art() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "article"
        If _Item.Sku Is Nothing Then
            oMenuItem.Enabled = False
        Else
            Dim oSku As DTOProductSku = _Item.Sku
            oMenuItem.DropDownItems.AddRange(New Menu_Art(oSku).Range)
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


    Private Sub Do_AddToCataleg(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceListSupplierToCataleg(_Item, _LastPricelistCustomer, _LastCategory)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_UpdateCustomerPriceList(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceLists_Customers(BLL.Defaults.SelectionModes.Selection)
        AddHandler oFrm.OnItemSelected, AddressOf OnCustomerPriceListSelected
        oFrm.Show()
    End Sub

    Private Sub onNewArtStpSelected(sender As Object, e As EventArgs)
        Dim oProduct As Product = sender
        If oProduct.ValueType = Product.ValueTypes.Stp Then
            Dim oStp As Stp = oProduct.Value
            'Dim oArt As Art = _Item.GetNewArt(oStp)

            'Dim oFrm As New Frm_Art(oArt, Frm_Art.Modes.Edit)
            'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            'oFrm.Show()
        Else
            MsgBox("cal seleccionar una categoría per el producte!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub OnCustomerPriceListSelected(sender As Object, e As MatEventArgs)
        Dim oPriceList As DTOPricelistCustomer = e.Argument
        BLL.BLLPricelistCustomer.Load(oPriceList, True)

        Dim exs As New List(Of Exception)
        For Each src As DTOPriceListItem_Supplier In _Items
            BLL.BLLPricelistItemCustomer.AddFromPriceListItemSupplier(oPriceList, src, exs)
        Next

        If exs.Count > 0 Then
            UIHelper.WarnError(exs)
        Else
            If BLL.BLLPricelistCustomer.Update(oPriceList, exs) Then
                MsgBox("actualitzats " & _Items.Count & " productes", MsgBoxStyle.Information)
            Else
                UIHelper.WarnError(exs)
            End If

        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


