Public Class Menu_EdiOrder
    Inherits Menu_Base

    Private _EdiOrders As List(Of DTOEdiOrder)
    Private _EdiOrder As DTOEdiOrder

    Public Sub New(ByVal oEdiOrders As List(Of DTOEdiOrder))
        MyBase.New()
        _EdiOrders = oEdiOrders
        If _EdiOrders IsNot Nothing Then
            If _EdiOrders.Count > 0 Then
                _EdiOrder = _EdiOrders.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oEdiOrder As DTOEdiOrder)
        MyBase.New()
        _EdiOrder = oEdiOrder
        _EdiOrders = New List(Of DTOEdiOrder)
        If _EdiOrder IsNot Nothing Then
            _EdiOrders.Add(_EdiOrder)
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
        oMenuItem.Enabled = _EdiOrders.Count = 1
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
        Dim oFrm As New Frm_EdiOrder(_EdiOrder)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.EdiOrder.Delete(exs, _EdiOrders.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

