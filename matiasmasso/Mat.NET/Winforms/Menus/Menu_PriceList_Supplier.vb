

Public Class Menu_PriceList_Supplier

    Private _PriceLists As List(Of DTOPriceListSupplier)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPriceList As DTOPriceListSupplier)
        MyBase.New()
        _PriceLists = New List(Of DTOPriceListSupplier)
        _PriceLists.Add(oPriceList)
    End Sub

    Public Sub New(ByVal oPriceLists As List(Of DTOPriceListSupplier))
        MyBase.New()
        _PriceLists = oPriceLists
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
                                         MenuItem_Compara(),
                                         MenuItem_SalePriceList(),
                                         MenuItem_Excel(),
                                         MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Compara() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compara amb tarifa vigent"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Compara
        Return oMenuItem
    End Function

    Private Function MenuItem_SalePriceList() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Genera tarifa de venda"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf GeneraSalePriceList
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "exportar a Excel"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Export_Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = _PriceLists.Count = 1
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceList_Supplier(_PriceLists(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Compara(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceLists_Compare(_PriceLists.First)
        oFrm.Show()
    End Sub

    Private Sub GeneraSalePriceList(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.PriceListSupplier.Load(exs, _PriceLists.First) Then
            Dim oPriceList As DTOPricelistCustomer = DTOPriceListSupplier.GenerateCustomerPriceList(_PriceLists(0))
            Dim oFrm As New Frm_PriceList_Customer(oPriceList)
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Export_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.PriceListSupplier.Load(exs, _PriceLists.First) Then
            Dim oSheet = DTOPriceListSupplier.Excel(_PriceLists.First)
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete()
        Dim rc As MsgBoxResult = MsgBox("eliminem la tarifa seleccionada?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.PriceListSupplier.Delete(exs, _PriceLists(0)) Then
                RefreshRequest(Nothing, EventArgs.Empty)
            Else
                MsgBox("error al eliminar la tarifa" & vbCrLf & ExceptionsHelper.ToFlatString(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(Me, New MatEventArgs)
    End Sub

End Class

