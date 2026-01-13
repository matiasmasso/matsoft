Public Class Menu_ProductDownload
    Private _ProductDownload As DTOProductDownload

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event onImportRequest(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oProductDownload As DTOProductDownload)
        MyBase.New()
        _ProductDownload = oProductDownload
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
                                         MenuItem_Importar(), _
                                         MenuItem_CopyLink(), _
                                         MenuItem_Delete()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================


    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "fitxa"
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Importar() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "importar"
        AddHandler oMenuItem.Click, AddressOf Do_Importar
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
        Dim oFrm As New Frm_ProductDownload(_ProductDownload)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Importar(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDlg As New OpenFileDialog
        With oDlg
            .Title = "Importar fitxer"
            .Filter = "documents pdf (*.pdf)|*.pdf|tots els fitxers (*.*)|*.*"
            .DefaultExt = ".pdf"
            .Multiselect = False
            If .ShowDialog = DialogResult.OK Then
                Dim exs As New List(Of exception)
                Dim oDocFile As DTODocFile = BLL_DocFile.FromFile(.FileName, exs)
                If exs.Count = 0 Then
                    Dim rc As MsgBoxResult = MsgBox("importem '" & .FileName & "'" & vbCrLf & "a " & _ProductDownload.DocFile.Nom & "?", MsgBoxStyle.OkCancel)
                    If rc = vbOK Then
                        _ProductDownload.DocFile = oDocFile
                        If BLL.BLLProductDownload.Update(_ProductDownload, exs) Then
                            RaiseEvent AfterUpdate(Me, New MatEventArgs(_ProductDownload))
                        Else
                            UIHelper.WarnError(exs, "error al importar el fitxer")
                        End If
                    End If
                Else
                    UIHelper.WarnError(exs, "error al importar fitxer")
                End If
            End If
        End With
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_ProductDownload.DocFile)
    End Sub

    Private Sub Do_Delete()
        Dim rc As MsgBoxResult = MsgBox("eliminem aquest fitxer?", MsgBoxStyle.OkCancel)
        If rc = vbOK Then
            Dim exs As New List(Of Exception)
            If BLL.BLLProductDownload.Delete(_ProductDownload, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el fitxer")
            End If
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(Me, e)
    End Sub



End Class
