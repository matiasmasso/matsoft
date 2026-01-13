

Public Class Menu_PriceList_Supplier

    Private _PriceLists As List(Of DTOPriceList_Supplier)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPriceList As DTOPriceList_Supplier)
        MyBase.New()
        _PriceLists = New List(Of DTOPriceList_Supplier)
        _PriceLists.Add(oPriceList)
    End Sub

    Public Sub New(ByVal oPriceLists As List(Of DTOPriceList_Supplier))
        MyBase.New()
        _PriceLists = oPriceLists
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_Compara(), _
                                         MenuItem_SalePriceList(), _
                                         MenuItem_Excel(), _
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
        oMenuItem.Text = "Compara 2 tarifes"
        If _PriceLists.Count <> 2 Then oMenuItem.Enabled = False
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
        oFrm.show()
    End Sub

    Private Sub Do_Compara(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_PriceLists_Compare(_PriceLists)
        'oFrm.Show()
    End Sub

    Private Sub GeneraSalePriceList(ByVal sender As Object, ByVal e As System.EventArgs)
        BLL.BLLPriceList_Supplier.Load(_PriceLists(0))
        Dim oPriceList AS DTOPricelistCustomer = BLL.BLLPriceList_Supplier.GenerateCustomerPriceList(_PriceLists(0))
        Dim oFrm As New Frm_PriceList_Customer(oPriceList)
        oFrm.Show()
    End Sub

    Private Sub Do_Export_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New SaveFileDialog
        With oDlg
            .DefaultExt = "csv"
            .AddExtension = True
            .FileName = "tarifa proveidor " & _PriceLists(0).Proveidor.Nom & ".csv"
            .Filter = "fitxers csv (*.csv)|*.csv|tots els fitxers (*.*)|*.*"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .Title = "exportar tarifa proveidor"
            If .ShowDialog Then
                Dim exs as New List(Of exception)
                If Not BLL.BLLPriceList_Supplier.ExportToCsv(_PriceLists(0), .FileName, exs) Then
                    MsgBox("error al exportar tarifa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub

    Private Sub Do_Delete()
        Dim rc As MsgBoxResult = MsgBox("eliminem la tarifa seleccionada?", MsgBoxStyle.OkCancel, "MAT.NET")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BLL.BLLPriceList_Supplier.Delete(_PriceLists(0), exs) Then
                RefreshRequest(Nothing, EventArgs.Empty)
            Else
                MsgBox("error al eliminar la tarifa" & vbCrLf & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class

