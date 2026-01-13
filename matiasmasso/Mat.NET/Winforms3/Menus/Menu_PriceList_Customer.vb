Public Class Menu_PriceList_Customer
    Private _PriceLists As List(Of DTOPricelistCustomer)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oPriceList AS DTOPricelistCustomer)
        MyBase.New()
        _PriceLists = New List(Of DTOPricelistCustomer)
        _PriceLists.Add(oPriceList)
    End Sub

    Public Sub New(ByVal oPriceLists As List(Of DTOPricelistCustomer))
        MyBase.New()
        _PriceLists = oPriceLists
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Clon(),
        MenuItem_Excel(),
         MenuItem_Compare(),
         MenuItem_Advanced(),
       MenuItem_Del()})
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

    Private Function MenuItem_Clon() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Clon"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Clon
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function

    Private Function MenuItem_Compare() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compara amb la anterior"
        If _PriceLists.Count <> 1 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Compara
        Return oMenuItem
    End Function
    Private Function MenuItem_Advanced() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Avançat"
        oMenuItem.DropDownItems.Add("Copiar Guid", Nothing, AddressOf Do_CopyGuid)
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceList_Customer(_PriceLists(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        If _PriceLists.Count = 1 Then
            rc = MsgBox("Eliminem Tarifa del " & _PriceLists(0).Fch.ToShortDateString & "?", MsgBoxStyle.OkCancel, "M+O")
        Else
            rc = MsgBox("Eliminem les tarifes seleccionades?", MsgBoxStyle.OkCancel, "M+O")
        End If
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.PriceListsCustomer.Delete(exs, _PriceLists) Then
                If _PriceLists.Count = 1 Then
                    MsgBox("Tarifa del " & _PriceLists(0).Fch.ToShortDateString & " eliminada", MsgBoxStyle.OkCancel, "M+O")
                Else
                    MsgBox("Tarifes eliminades", MsgBoxStyle.OkCancel, "M+O")
                End If
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Clon(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oPriceList = _PriceLists.First
        If FEB.PriceListCustomer.Load(exs, oPriceList) Then
            Dim oClon As New DTOPricelistCustomer()
            For Each item In oPriceList.Items
                Dim oClonItem = item.Clon(oClon)
                oClon.Items.Add(oClonItem)
            Next
            Dim oFrm As New Frm_PriceList_Customer(oClon)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Compara(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim oSheet As MatHelper.Excel.Sheet = Await FEB.PriceListsCustomer.ExcelCompareSheet(exs, _PriceLists.First)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyGuid()
        UIHelper.CopyToClipboard(_PriceLists.First.Guid.ToString)
    End Sub

    Private Sub Do_Excel()
        Dim oSheet = DTOPricelistCustomer.ExcelSheet(_PriceLists)
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
