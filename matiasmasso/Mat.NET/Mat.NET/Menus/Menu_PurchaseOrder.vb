Public Class Menu_PurchaseOrder
    Inherits Menu_Base

    Private _PurchaseOrder As DTOPurchaseOrder

    Public Sub New(ByVal oPurchaseOrder As DTOPurchaseOrder)
        MyBase.New()
        _PurchaseOrder = oPurchaseOrder
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Web(), _
        MenuItem_CopyLink(), _
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

    Private Function MenuItem_Web() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Web"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Web
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        'oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
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
        Dim exs As New List(Of Exception)
        If Not BLL.BLLAlbBloqueig.BloqueigStart(BLL.BLLSession.Current.User, _PurchaseOrder.Customer, DTOAlbBloqueig.Codis.PDC, exs) Then
            UIHelper.WarnError(exs)
        Else
            Dim oFrm As New Frm_PurchaseOrder(_PurchaseOrder)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_Web()
        Dim sUrl As String = BLL.BLLPurchaseOrder.Url(_PurchaseOrder, True)
        UIHelper.ShowHtml(sUrl)
    End Sub

    Private Sub Do_CopyLink()
        Dim sUrl As String = BLL.BLLPurchaseOrder.Url(_PurchaseOrder, True)
        Clipboard.SetText(sUrl)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem comanda " & _PurchaseOrder.Id & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If BLL.BLLPurchaseOrder.Delete(_PurchaseOrder, exs) Then
                MsgBox("comanda " & _PurchaseOrder.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RefreshRequest(Me, New System.EventArgs)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class

