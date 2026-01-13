Public Class Menu_VivaceStock
    Private _Items As List(Of DTOVivaceStock)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(values As List(Of DTOVivaceStock))
        MyBase.New()
        _Items = values
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Excel()})
        'MenuItem_Zoom(), _
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

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Image = My.Resources.Excel
        AddHandler oMenuItem.Click, AddressOf Do_Excel
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim oFrm As New Frm_Template(_Template)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_Excel(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oSheet As New MatHelperStd.ExcelHelper.Sheet
        Dim oRow As MatHelperStd.ExcelHelper.Row = oSheet.AddRow()
        oRow.AddCell("Referencia")
        oRow.AddCell("Producte")
        oRow.AddCell("Stock")
        oRow.AddCell("Ubicacio")
        For Each item As DTOVivaceStock In _Items
            oRow = oSheet.AddRow()
            oRow.AddCell(item.Referencia)
            oRow.AddCell(item.Descripcio)
            oRow.AddCell(item.Stock)
            oRow.AddCell(item.Ubicacio)
        Next
        Dim exs As New List(Of Exception)
        If Not UIHelper.ShowExcel(oSheet, exs) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

