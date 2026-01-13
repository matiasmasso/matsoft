Public Class Menu_BudgetItemTicket
    Inherits Menu_Base

    Private _BudgetItemTickets As List(Of DTOBudgetItemTicket)
    Private _BudgetItemTicket As DTOBudgetItemTicket

    Public Sub New(ByVal oBudgetItemTickets As List(Of DTOBudgetItemTicket))
        MyBase.New()
        _BudgetItemTickets = oBudgetItemTickets
        If _BudgetItemTickets IsNot Nothing Then
            If _BudgetItemTickets.Count > 0 Then
                _BudgetItemTicket = _BudgetItemTickets.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oBudgetItemTicket As DTOBudgetItemTicket)
        MyBase.New()
        _BudgetItemTicket = oBudgetItemTicket
        _BudgetItemTickets = New List(Of DTOBudgetItemTicket)
        If _BudgetItemTicket IsNot Nothing Then
            _BudgetItemTickets.Add(_BudgetItemTicket)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _BudgetItemTickets.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
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
        Dim oFrm As New Frm_BudgetItemTicket(_BudgetItemTicket)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.BudgetItemTicket.Delete(_BudgetItemTickets.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


