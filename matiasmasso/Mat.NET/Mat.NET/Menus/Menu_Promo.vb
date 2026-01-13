Public Class Menu_Promo
    Private _Promo As DTOPromo

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oPromo As DTOPromo)
        MyBase.New()
        _Promo = oPromo
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Orders(), _
        MenuItem_CopyGuid(), _
        MenuItem_Delete()})
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

    Private Function MenuItem_Orders() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Comandes"
        AddHandler oMenuItem.Click, AddressOf Do_Orders
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyGuid() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar Guid"
        If BLL.BLLSession.Current.User.Rol.Id <> DTORol.Ids.SuperUser Then oMenuItem.Visible = False
        AddHandler oMenuItem.Click, AddressOf Do_CopyGuid
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
        Dim oFrm As New Frm_Promo(_Promo)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Orders()
        Dim oOrders As List(Of DTOPurchaseOrder) = BLL.BLLPromo.PurchaseOrders(_Promo)
        Dim oFrm As New Frm_PurchaseOrders(oOrders)
        oFrm.Show()
    End Sub

    Private Sub Do_CopyGuid()
        Clipboard.SetDataObject(_Promo.Guid.ToString, True)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Promo.Caption & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPromo.Delete(_Promo, exs) Then
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class



