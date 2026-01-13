Public Class Menu_BudgetItem
    Inherits Menu_Base

    Private _BudgetItems As List(Of DTOBudget.Item)
    Private _BudgetItem As DTOBudget.Item

    Public Sub New(ByVal oBudgetItems As List(Of DTOBudget.Item))
        MyBase.New()
        _BudgetItems = oBudgetItems
        If _BudgetItems IsNot Nothing Then
            If _BudgetItems.Count > 0 Then
                _BudgetItem = _BudgetItems.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBudgetItem As DTOBudget.Item)
        MyBase.New()
        _BudgetItem = oBudgetItem
        _BudgetItems = New List(Of DTOBudget.Item)
        If _BudgetItem IsNot Nothing Then
            _BudgetItems.Add(_BudgetItem)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_AplicarFra())
        MyBase.AddMenuItem(MenuItem_AplicarDocument())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BudgetItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_AplicarFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Aplicar factura"
        oMenuItem.Enabled = _BudgetItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_AplicarFactura
        Return oMenuItem
    End Function
    Private Function MenuItem_AplicarDocument() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Aplicar altres documents"
        oMenuItem.Enabled = _BudgetItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_AplicarDocument
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BudgetItem(_BudgetItem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AplicarFactura(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim value As New DTOBudgetItemTicket
        With value
            .BudgetItem = _BudgetItem
        End With
        Dim oFrm As New Frm_BudgetItemTicket(value)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AplicarDocument(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim value As New DTOBudgetItemTicket
        With value
            .BudgetItem = _BudgetItem
        End With
        Dim oFrm As New Frm_BudgetItemTicket(value)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub



End Class


