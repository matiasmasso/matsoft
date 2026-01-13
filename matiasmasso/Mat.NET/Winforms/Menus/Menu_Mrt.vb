

Public Class Menu_Mrt
    Private mMrt As Mrt

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMrt As Mrt)
        MyBase.New()
        mMrt = oMrt
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), _
        MenuItem_Pdf(), _
        MenuItem_CopyLink(), _
        MenuItem_Baixa()})
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

    Private Function MenuItem_Baixa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Enabled = mMrt.Baixa.Id = 0
        oMenuItem.Text = "Baixa per obsolet"
        oMenuItem.Image = My.Resources.GoDown
        AddHandler oMenuItem.Click, AddressOf Do_Baixa
        Return oMenuItem
    End Function

    Private Function MenuItem_Pdf() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Pdf"
        oMenuItem.Image = My.Resources.pdf
        'oMenuItem.Enabled = mMrt.Alta IsNot Nothing
        AddHandler oMenuItem.Click, AddressOf Do_Pdf
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

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Mrt
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        With oFrm
            .Mrt = mMrt
            .Show()
        End With
    End Sub

    Private Sub Do_Baixa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sFch As String = InputBox(mMrt.Nom & vbCrLf & "Data de baixa de l'inventari (DD/MM/AA):", "MAT.NET", Today.ToShortDateString)
        Dim sErr As String = ""
        If IsDate(sFch) Then
            If mMrt.BaixaDelInventari(CDate(sFch), sErr) Then
                MsgBox(mMrt.Nom & vbCrLf & "Partida donada de baixa de l'inventari", MsgBoxStyle.Information)
                RaiseEvent AfterUpdate(sender, e)
            Else
                MsgBox("Operació fallida:" & vbCrLf & sErr, MsgBoxStyle.Exclamation, "MAT.NET")
            End If
        Else
            MsgBox(mMrt.Nom & vbCrLf & "Data no validada. No s'ha donat de baixa aquesta partida de l'inventari", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Do_Pdf(ByVal sender As Object, ByVal e As System.EventArgs)
        If mMrt.Alta.DocExists Then
            UIHelper.ShowStream(mMrt.Alta.DocFile)
        Else
            MsgBox("copia no disponible en PDF", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub Do_CopyLink(ByVal sender As Object, ByVal e As System.EventArgs)
        If mMrt.Alta.DocExists Then
            UIHelper.CopyLink(mMrt.Alta.DocFile)
        Else
            MsgBox("copia no disponible en PDF", MsgBoxStyle.Exclamation, "MAT.NET")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub


End Class
