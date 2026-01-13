Public Class Menu_Correspondencia

    Private _Correspondencia As DTOCorrespondencia

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oCorrespondencia As DTOCorrespondencia)
        MyBase.New()
        _Correspondencia = oCorrespondencia
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Import(), _
        MenuItem_Pdf(), _
        MenuItem_CopyLink(), _
        MenuItem_Del()})
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

    Private Function MenuItem_Import() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Importar Pdf"
        oMenuItem.Image = My.Resources.clip
        AddHandler oMenuItem.Click, AddressOf Do_PdfAdd
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Visualitzar Pdf"
        oMenuItem.Image = My.Resources.pdf
        AddHandler oMenuItem.Click, AddressOf Do_PdfShow
        Return oMenuItem
    End Function


    Private Function MenuItem_CopyLink() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Copiar enllaç"
        oMenuItem.Image = My.Resources.Copy
        AddHandler oMenuItem.Click, AddressOf Do_CopyLink
        Return oMenuItem
    End Function

    Private Function MenuItem_Del() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Del
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Correspondencia(_Correspondencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem correspondencia " & _Correspondencia.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Correspondencia.Delete(_Correspondencia, exs) Then
                MsgBox("Correspondencia " & _Correspondencia.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New MatEventArgs(_Correspondencia))
            Else
                UIHelper.WarnError(exs, "error al eliminar el document de correspondencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, "importar correspondencia") Then
            _Correspondencia.DocFile = oDocFile

            Dim exs As New List(Of Exception)
            If Not Await FEB2.Correspondencia.Upload(_Correspondencia, exs) Then
                UIHelper.WarnError(exs, "error al desar el document a correspondencia")
            End If

        End If
    End Sub

    Private Async Sub Do_PdfShow(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If Not Await UIHelper.ShowStreamAsync(exs, _Correspondencia.DocFile) Then
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(_Correspondencia.DocFile)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class



