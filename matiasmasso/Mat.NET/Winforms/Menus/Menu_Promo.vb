Public Class Menu_Promo
    Inherits Menu_Base

    Private _Promo As DTOPromo

    Public Sub New(ByVal oPromo As DTOPromo)
        MyBase.New()
        _Promo = oPromo
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Browse(),
        MenuItem_CopyGuid(),
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

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        AddHandler oMenuItem.Click, AddressOf Do_Browse
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

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = BLLPromo.Url(_Promo, True)
        UIHelper.ShowHtml(url)
    End Sub


    Private Sub Do_CopyGuid()
        Clipboard.SetDataObject(_Promo.Guid.ToString, True)
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _Promo.Caption & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPromo.Delete(_Promo, exs) Then
                MyBase.RefreshRequest(Me, New System.EventArgs)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequestOrders(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oFrm As Frm_Customer_PurchaseOrders = sender
        oFrm.Orders = BLL.BLLPromo.PurchaseOrders(_Promo, BLLSession.Current.User)
        MyBase.RefreshRequest(sender, e)
    End Sub


End Class



