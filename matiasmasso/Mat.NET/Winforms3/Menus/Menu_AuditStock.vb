Public Class Menu_AuditStock

    Inherits Menu_Base

    Private _AuditStocks As List(Of DTOAuditStock)
    Private _AuditStock As DTOAuditStock


    Public Sub New(ByVal oAuditStocks As List(Of DTOAuditStock))
        MyBase.New()
        _AuditStocks = oAuditStocks
        If _AuditStocks IsNot Nothing Then
            If _AuditStocks.Count > 0 Then
                _AuditStock = _AuditStocks.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oAuditStock As DTOAuditStock)
        MyBase.New()
        _AuditStock = oAuditStock
        _AuditStocks = New List(Of DTOAuditStock)
        If _AuditStock IsNot Nothing Then
            _AuditStocks.Add(_AuditStock)
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
        oMenuItem.Enabled = _AuditStocks.Count = 1
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
        Dim oFrm As New Frm_Art(_AuditStock.Sku)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.AuditStock.Delete(_AuditStock, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

