Public Class Menu_FileDocument
    Private _FileDocument As FileDocument

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As AfterUpdateEventArgs)
    Public Event onImportRequest(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oFileDocument As FileDocument)
        MyBase.New()
        _FileDocument = oFileDocument
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_ShowDoc(), _
                                         MenuItem_Zoom(), _
                                         MenuItem_CopyLink(), _
                                         MenuItem_Import(), _
                                         MenuItem_Export()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_ShowDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "document"
        If _FileDocument Is Nothing Then
            oMenuItem.Enabled = False
        Else
            oMenuItem.Image = root.GetIcoFromMime(_FileDocument.MediaObject.Mime)
        End If
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

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Importar
        Return oMenuItem
    End Function

    Private Function MenuItem_Export() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Exportar
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_ShowDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case _FileDocument.MediaObject.Mime
            Case DTOEnums.MimeCods.Pdf
                root.ShowPdf(_FileDocument.MediaObject.Stream)
        End Select
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_FileDocument(_FileDocument)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sUrl As String = _FileDocument.RoutingUrl(True)
        Clipboard.SetDataObject(sUrl, True)
    End Sub

    Private Sub Do_Importar(sender As Object, e As EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer"
            .Filter = "documents pdf (*.pdf)|*.pdf|tots els fitxers (*.*)|*.*"
            If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                Dim oEventArgs As New MatEventArgs(.FileName)
                RaiseEvent onImportRequest(Me, oEventArgs)
            End If
        End With

    End Sub

    Private Sub Do_Exportar(sender As Object, e As EventArgs)
        Dim oDlg As New SaveFileDialog
        With oDlg
            '.DefaultExt = 
            '.FileName = oAeat.DefaultFileName
            If .ShowDialog = DialogResult.OK Then
                'oBigFile.BigFile.Save(.FileName)
            End If
        End With

    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class

