

Public Class Menu_ContactDoc
    Inherits Menu_Base

    Private mContactDoc As DTOContactDoc

    Public Sub New(ByVal oContactDoc As DTOContactDoc)
        MyBase.New()
        mContactDoc = oContactDoc
        AddMenuItems()
    End Sub


    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Pdf())
        MyBase.AddMenuItem(MenuItem_CopyLink())
    End Sub

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        oMenuItem.Enabled = mContactDoc.DocFile IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
        Return oMenuItem
    End Function

    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        oMenuItem.Enabled = mContactDoc.DocFile IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_ContactDoc(mContactDoc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB.DocFile.DownloadUrl(mContactDoc.DocFile, True)
        Process.Start(sUrl)
    End Sub


    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = FEB.DocFile.DownloadUrl(mContactDoc.DocFile, True)

        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, sUrl)
        Clipboard.SetDataObject(data_object, True)
        MsgBox("enllaç copiat al portapapers", MsgBoxStyle.Information, "MAT.NET")
    End Sub



End Class

