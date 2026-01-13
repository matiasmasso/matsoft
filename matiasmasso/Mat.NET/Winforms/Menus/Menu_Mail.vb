Public Class Menu_Mail
    Private mMail As Mail

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMail As Mail)
        MyBase.New()
        mMail = oMail
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
        Dim oCorrespondencia As DTOCorrespondencia = FromOldMail(mMail)
        'Dim oFrm As New Frm_Correspondencia(oCorrespondencia)
        Dim oFrm As New Frm_Contact_Mail(mMail)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Shared Function FromOldMail(oMail As Mail) As DTOCorrespondencia
        Dim retval As New DTOCorrespondencia(oMail.Guid)
        With retval
            .Id = oMail.Id
            .Fch = oMail.Fch
            .Emp = oMail.Emp
            .DocFile = oMail.DocFile
            .Subject = oMail.Subject
            .Atn = oMail.Atn
            .Cod = oMail.Cod
        End With
        Return retval
    End Function

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem correspondencia " & mMail.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If mMail.Delete( exs) Then
                MsgBox("Correspondencia " & mMail.Id & " eliminada", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
            Else
                UIHelper.WarnError( exs, "error al eliminar el document de correspondencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_PdfAdd(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oDocFile As DTODocFile = Nothing
        If UIHelper.LoadPdfDialog(oDocFile, "importar correspondencia") Then
            mMail.DocFile = oDocFile
            Dim exs as New List(Of exception)
            If mMail.Update( exs) Then
                RaiseEvent AfterUpdate(mMail, New System.EventArgs)
            Else
                UIHelper.WarnError( exs, "error al desar el document")
            End If
        End If
    End Sub

    Private Sub Do_PdfShow(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.ShowStream(mMail.DocFile)
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        UIHelper.CopyLink(mMail.DocFile)
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class


