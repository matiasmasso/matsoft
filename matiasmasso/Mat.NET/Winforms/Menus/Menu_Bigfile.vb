

Public Class Menu_Bigfile
    Private mBigfileSrc As BigFileSrc

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oBigfileSrc As BigFileSrc)
        MyBase.New()
        mBigfileSrc = oBigfileSrc
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_ShowDoc(), MenuItem_Zoom(), MenuItem_CopyLink()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_ShowDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "document"
        oMenuItem.Image = root.GetIcoFromMime(mBigfileSrc.BigFile.MimeCod)
        AddHandler oMenuItem.Click, AddressOf Do_ShowDoc
        Return oMenuItem
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "fitxa"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_ShowDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        root.ShowBigFile(mBigfileSrc.BigFile)
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_BigFile(mBigfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = mBigfileSrc.RoutingUrl(True)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
