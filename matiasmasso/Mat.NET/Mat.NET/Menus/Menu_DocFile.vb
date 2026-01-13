Public Class Menu_DocFile
    Private _DocFile As DTODocFile

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As EventArgs)
    Public Event RequestToDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oDocFile As DTODocFile)
        MyBase.New()
        _DocFile = oDocFile
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_ShowDoc(), _
                                         MenuItem_Zoom(), _
                                         MenuItem_CopyLink(), _
                                         MenuItem_Import(), _
                                         MenuItem_Export(), _
                                         MenuItem_Delete(), _
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
            If _DocFile.Mime <> DTOEnums.MimeCods.NotSet Then
                oMenuItem.Image = BLL.MediaHelper.GetIconFromMimeCod(_DocFile.Mime)
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

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar"
        oMenuItem.Image = My.Resources.disk
        AddHandler oMenuItem.Click, AddressOf Do_Importar
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

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Enabled = _DocFile IsNot Nothing
        oMenuItem.Image = My.Resources.aspa
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function

    Private Function MenuItem_Advance() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = False ' _DocFile IsNot Nothing
        oMenuItem.Text = "Avançats..."
        'Select Case BLL.BLLSession.Current.User.Rol.Id
        '    Case Rol.Ids.SuperUser, Rol.Ids.Admin
        'oMenuItem.DropDownItems.Add(SubMenuItem_ShowHash)
        'oMenuItem.DropDownItems.Add(SubMenuItem_CopyHash)
        'End Select
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

    Private Sub Do_ShowDoc(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowStream(_DocFile)
    End Sub

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_DocFile(_DocFile)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_DocFile)
    End Sub

    Private Sub Do_Importar(sender As Object, e As EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "importar document"
            .Filter = "documents pdf (*.pdf)|*.pdf| tots els documents (*.*)|*.*"
            If .ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim exs As New List(Of exception)
                _DocFile = BLL_DocFile.FromFile(.FileName, exs)
                If exs.Count = 0 Then
                    Dim DtFch As Date = Nothing
                    If BLL.BLLDocFile.Exists(_DocFile.Hash, DtFch) Then
                        'recupera data si ja estava registrat
                        _DocFile.Fch = DtFch
                    End If
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_DocFile))
                Else
                    MsgBox("error al importar document" & Environment.NewLine & BLL.Defaults.ExsToMultiline(exs), MsgBoxStyle.Exclamation)
                End If
            End If
        End With
    End Sub

    Private Sub Do_Exportar(sender As Object, e As EventArgs)
        If _DocFile Is Nothing Then
            MsgBox("el document está buit", MsgBoxStyle.Exclamation)
        Else
            Dim sMime As String = _DocFile.Mime.ToString
            Dim oDlg As New SaveFileDialog
            With oDlg
                .Title = "exportar document"
                .Filter = "documents " & sMime & " (*." & sMime & ")|*." & sMime & "| tots els documents (*.*)|*.*"
                If .ShowDialog = Windows.Forms.DialogResult.OK Then
                    BLL.BLLDocFile.Load(_DocFile, True)
                    Dim exs As New List(Of Exception)
                    If Not BLL.FileSystemHelper.SaveStream(_DocFile.Stream, exs, .FileName) Then
                        UIHelper.WarnError(exs, "error al desar el fitxer")
                    End If
                End If
            End With
        End If
    End Sub


    Private Sub Do_Delete(sender As Object, e As EventArgs)
        Dim oArgs As New MatEventArgs(_DocFile)
        RaiseEvent RequestToDelete(Me, oArgs)
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

