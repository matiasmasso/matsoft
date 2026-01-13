Public Class Menu_DocfileSrc

    Inherits Menu_Base

    Private _DocfileSrcs As List(Of DTODocFileSrc)
    Private _DocfileSrc As DTODocFileSrc

    Public Sub New(ByVal oDocfileSrcs As List(Of DTODocFileSrc))
        MyBase.New()
        _DocfileSrcs = oDocfileSrcs
        If _DocfileSrcs IsNot Nothing Then
            If _DocfileSrcs.Count > 0 Then
                _DocfileSrc = _DocfileSrcs.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oDocfileSrc As DTODocFileSrc)
        MyBase.New()
        _DocfileSrc = oDocfileSrc
        _DocfileSrcs = New List(Of DTODocFileSrc)
        If _DocfileSrc IsNot Nothing Then
            _DocfileSrcs.Add(_DocfileSrc)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_ShowDoc())
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_CopyLink())
        MyBase.AddMenuItem(MenuItem_Export())
        MyBase.AddMenuItem(MenuItem_Advance())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================
    Private Function MenuItem_ShowDoc() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "document"
        If _DocfileSrc.Docfile Is Nothing Then
            oMenuItem.Enabled = False
        Else
            If _DocfileSrc.Docfile.Mime <> MimeCods.NotSet Then
                oMenuItem.Image = IconHelper.GetIconFromMimeCod(_DocfileSrc.Docfile.mime)
            End If
        End If
        AddHandler oMenuItem.Click, AddressOf Do_ShowDoc
        Return oMenuItem
    End Function

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "fitxa"
        oMenuItem.Enabled = _DocfileSrc.Docfile IsNot Nothing
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Enabled = _DocfileSrc.Docfile IsNot Nothing
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function


    Private Function MenuItem_Export() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Exportar"
        oMenuItem.Enabled = _DocfileSrc.Docfile IsNot Nothing
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
        If Not Await UIHelper.ShowStreamAsync(exs, _DocfileSrc.Docfile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_DocfileSrc(_DocfileSrc)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_DocfileSrc.Docfile)
    End Sub


    Private Async Sub Do_Exportar(sender As Object, e As EventArgs)
        Dim exs As New List(Of Exception)
        If _DocfileSrc.Docfile Is Nothing Then
            MsgBox("el document está buit", MsgBoxStyle.Exclamation)
        Else
            Dim sMime As String = _DocfileSrc.Docfile.Mime.ToString
            Dim oDlg As New SaveFileDialog
            With oDlg
                .Title = "exportar document"
                .Filter = "documents " & sMime & " (*." & sMime & ")|*." & sMime & "| tots els documents (*.*)|*.*"
                If .ShowDialog = System.Windows.Forms.DialogResult.OK Then
                    Dim oStream As Byte() = Await FEB.DocFile.StreamOrDownload(_DocfileSrc.Docfile, exs)
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
        MsgBox("Hash: " & _DocfileSrc.Docfile.Hash, MsgBoxStyle.Information)
    End Sub

    Private Sub Do_CopyHash(sender As Object, e As EventArgs)
        Dim data_object As New DataObject
        data_object.SetData(DataFormats.Text, True, _DocfileSrc.Docfile.Hash)
        Clipboard.SetDataObject(data_object, True)
    End Sub


End Class


