Public Class Menu_DeliveryTraspas
    Inherits Menu_Base

    Private _DeliveryTraspass As List(Of DTODeliveryTraspas)
    Private _DeliveryTraspas As DTODeliveryTraspas

    Public Sub New(ByVal oDeliveryTraspass As List(Of DTODeliveryTraspas))
        MyBase.New()
        _DeliveryTraspass = oDeliveryTraspass
        If _DeliveryTraspass IsNot Nothing Then
            If _DeliveryTraspass.Count > 0 Then
                _DeliveryTraspas = _DeliveryTraspass.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oDeliveryTraspas As DTODeliveryTraspas)
        MyBase.New()
        _DeliveryTraspas = oDeliveryTraspas
        _DeliveryTraspass = New List(Of DTODeliveryTraspas)
        If _DeliveryTraspas IsNot Nothing Then
            _DeliveryTraspass.Add(_DeliveryTraspas)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Excel())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _DeliveryTraspass.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Excel() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Excel"
        oMenuItem.Enabled = _DeliveryTraspass.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Excel
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
        Dim oFrm As New Frm_DeliveryTraspas(_DeliveryTraspas)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.DeliveryTraspas.Delete(_DeliveryTraspass.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Excel()
        Dim exs As New List(Of Exception)
        Dim oSheet = FEB2.DeliveryTraspas.Excel(_DeliveryTraspas, exs)
        If exs.Count = 0 Then
            If Not UIHelper.ShowExcel(oSheet, exs) Then
                UIHelper.WarnError(exs)
            End If
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class


