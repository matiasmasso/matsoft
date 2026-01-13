Public Class Menu_DocFile
    Private _DocFile As DTODocFile

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event RequestToDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oDocFile As DTODocFile)
        MyBase.New()
        _DocFile = oDocFile
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_ShowDoc(),
                                         MenuItem_Zoom(),
                                         MenuItem_CopyLink(),
                                         MenuItem_Export(),
                                         MenuItem_Advance()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_ShowDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "document"
        If _DocFile Is Nothing Then
            oMenuItem.Enabled = False
        Else
            If _DocFile.Mime <> MimeCods.NotSet Then
                oMenuItem.Image = IconHelper.GetIconFromMimeCod(_DocFile.mime)
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_ShowDoc
        Return oMenuItem
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "fitxa"
        oMenuItem.Enabled = _DocFile IsNot Nothing
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _DocFile IsNot Nothing
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    Private Function MenuItem_Export() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Enabled = _DocFile IsNot Nothing
        'oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_Exportar
        Return oMenuItem
    End Function


    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        'oMenuItem.Enabled = False ' _DocFile IsNot Nothing
        oMenuItem.Text = "Avançats..."
        Dim oUser As DTOUser = Current.Session.User
        If oUser IsNot Nothing Then
            Select Case Current.Session.User.Rol.Id
                Case DTORol.Ids.SuperUser, DTORol.Ids.Admin
                    oMenuItem.DropDownItems.Add(SubMenuItem_ShowHash)
                    oMenuItem.DropDownItems.Add(SubMenuItem_CopyHash)
            End Select
        End If
        Return oMenuItem
    End Function

    Private Function SubMenuItem_ShowHash() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "mostrar hash"
        AddHandler oMenuItem.Click, AddressOf Do_ShowHash
        Return oMenuItem
    End Function

    Private Function SubMenuItem_CopyHash() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "copiar hash"
        AddHandler oMenuItem.Click, AddressOf Do_CopyHash
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Async Sub Do_ShowDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, _DocFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_DocFile(_DocFile)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_DocFile)
    End Sub


    Private Async Sub Do_Exportar(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        If _DocFile Is Nothing Then
            MsgBox("el document está buit", MsgBoxStyle.Exclamation)
        Else
            Dim sMime As String = _DocFile.Mime.ToString
            Dim oDlg As New SaveFileDialog
            With oDlg
                .Title = "exportar document"
                .FileName = _DocFile.Nom
                .Filter = "documents " & sMime & " (*." & sMime & ")|*." & sMime & "| tots els documents (*.*)|*.*"
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Dim oStream As Byte() = Await FEB2.DocFile.StreamOrDownload(_DocFile, exs)
                    If exs.Count = 0 Then
                        If Not FileSystemHelper.SaveStream(oStream, exs, .FileName) Then
                            UIHelper.WarnError(exs, "error al desar el fitxer")
                        End If
                    Else
                        UIHelper.WarnError(exs)
                    End If
                End If
            End With
        End If
    End Sub


    Private Sub Do_ShowHash(sender As Object, e As EventArgs)
        MsgBox("Hash: " & _DocFile.Hash, MsgBoxStyle.Information)
    End Sub

    Private Sub Do_CopyHash(sender As Object, e As EventArgs)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, _DocFile.Hash)
        Clipboard.SetDataObject(data_object, True)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class

