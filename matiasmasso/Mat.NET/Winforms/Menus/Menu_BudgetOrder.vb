Public Class Menu_BudgetOrder

    Inherits Menu_Base

    Private _BudgetOrders As List(Of DTOBudgetOrder)
    Private _BudgetOrder As DTOBudgetOrder

    Public Sub New(ByVal oBudgetOrders As List(Of DTOBudgetOrder))
        MyBase.New()
        _BudgetOrders = oBudgetOrders
        If _BudgetOrders IsNot Nothing Then
            If _BudgetOrders.Count > 0 Then
                _BudgetOrder = _BudgetOrders.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBudgetOrder As DTOBudgetOrder)
        MyBase.New()
        _BudgetOrder = oBudgetOrder
        _BudgetOrders = New List(Of DTOBudgetOrder)
        If _BudgetOrder IsNot Nothing Then
            _BudgetOrders.Add(_BudgetOrder)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_AplicarFra)
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BudgetOrders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_AplicarFra() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Aplicar factura"
        oMenuItem.Enabled = _BudgetOrders.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_AplicarFactura
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BudgetOrder(_BudgetOrder)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_AplicarFactura(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim value As New DTOBudgetOrderFra
        With value
            .BudgetOrder = _BudgetOrder
        End With
        Dim oFrm As New Frm_BudgetOrderFra(value)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.BudgetOrder.Delete(_BudgetOrders.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


