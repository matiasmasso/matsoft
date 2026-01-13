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
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Compara(), _
        MenuItem_Excel(), _
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

    Private Function MenuItem_Compara() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Compara 2 tarifes"
        If _PriceLists.Count <> 2 Then oMenuItem.Enabled = False
        AddHandler oMenuItem.Click, AddressOf Do_Compara
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        AddHandler oMenuItem.Click, AddressOf Do_Excel
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

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult
        If _PriceLists.Count = 1 Then
            rc = MsgBox("Eliminem Tarifa del " & _PriceLists(0).Fch.ToShortDateString & "?", MsgBoxStyle.OkCancel, "M+O")
        Else
            rc = MsgBox("Eliminem les tarifes seleccionades?", MsgBoxStyle.OkCancel, "M+O")
        End If
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPricelistsCustomer.Delete(_PriceLists, exs) Then
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

    Private Sub Do_Compara(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_PriceLists_Compare(_PriceLists)
        oFrm.Show()
    End Sub

    Private Sub Do_Excel()
        Dim oExcelSheet As DTOExcelSheet = BLL.BLLPricelistsCustomer.ExcelSheet(_PriceLists)
        UIHelper.ShowExcel(oExcelSheet)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
