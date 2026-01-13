

Public Class Menu_QrCampaign
    Private mQrCampaign As QrCampaign

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oQrCampaign As QrCampaign)
        MyBase.New()
        mQrCampaign = oQrCampaign
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_YouTube(), _
        MenuItem_CopyLink(), _
        MenuItem_Save()})
    End Function


        '==========================================================================
        '                            MENU ITEMS BUILDER
        '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
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

    Private Function MenuItem_YouTube() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "ver video"
        AddHandler oMenuItem.Click, AddressOf Do_Display
        Return oMenuItem
    End Function

    Private Function MenuItem_Save() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Guardar imatge alta resolució"
        AddHandler oMenuItem.Click, AddressOf Do_Save
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_QrCampaign(mQrCampaign)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Display(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sAnchorUrl As String = mQrCampaign.AnchorUrl(True)
        UIHelper.ShowHtml(sAnchorUrl)
    End Sub


    Private Sub Do_Save(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_QR(mQrCampaign.QR)
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sAnchorUrl As String = mQrCampaign.AnchorUrl(True)
        Clipboard.SetDataObject(sAnchorUrl, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

End Class
