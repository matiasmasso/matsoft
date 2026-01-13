Public Class Menu_GalleryItem
    Private _GalleryItems As List(Of DTOGalleryItem)

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oGalleryItems As List(Of DTOGalleryItem))
        MyBase.New()
        _GalleryItems = oGalleryItems
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(),
                                         MenuItem_Browse(),
                                         MenuItem_Export(),
                                         MenuItem_CopyLink(),
                                         MenuItem_Delete()
        })
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        oMenuItem.Enabled = _GalleryItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Export() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Image = My.Resources.download
        oMenuItem.Enabled = _GalleryItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Export
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Navegar"
        oMenuItem.Image = My.Resources.browse
        oMenuItem.Enabled = _GalleryItems.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.aspa
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_GalleryItem(_GalleryItems(0))
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Export(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.SavefileDialogFilter(_GalleryItems.First.Mime)
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowHtml(FEB.GalleryItem.ImageUrl(_GalleryItems(0).Guid))
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sb As New System.Text.StringBuilder
        For Each Itm In _GalleryItems
            sb.AppendLine(String.Format("<img src='{0}' alt='{1}' width='100%' style='max-width:{2}px;'/>", FEB.GalleryItem.ImageUrl(Itm.Guid), Itm.Nom, Itm.Width))
        Next
        Clipboard.SetDataObject(sb.ToString, True)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _GalleryItems(0).Nom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.GalleryItem.Delete(_GalleryItems.First, exs) Then
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_GalleryItems.First))
            Else
                UIHelper.WarnError(exs, "error al eliminar la imatge")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest()
        RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
    End Sub

End Class
