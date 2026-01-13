Public Class Menu_EdiversaRemadv
    Private _EdiversaRemadv As DTOEdiversaRemadv

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEdiversaRemadv As DTOEdiversaRemadv)
        MyBase.New()
        _EdiversaRemadv = oEdiversaRemadv
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {
        MenuItem_Zoom(),
        MenuItem_Procesa(),
        MenuItem_Retrocedeix()})
    End Function


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Visible = False
        oMenuItem.Image = My.Resources.prismatics
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Procesa() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Registra recepció pagaré"
        oMenuItem.Image = My.Resources.clip
        AddHandler oMenuItem.Click, AddressOf Do_Procesa
        Return oMenuItem
    End Function

    Private Function MenuItem_Retrocedeix() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedeix a pendent de processar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Retrocedeix
        Return oMenuItem
    End Function


    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As MatEventArgs)
        'Dim oFrm As New Frm_EdiversaRemadv(_EdiversaRemadv)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub


    Private Sub Do_Procesa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cobrament(_EdiversaRemadv)
        AddHandler oFrm.AfterUpdate, AddressOf AfterProcesa
        oFrm.Show()
    End Sub

    Private Async Sub Do_Retrocedeix(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Retrocedim la remesa " & _EdiversaRemadv.DocNum & " a pendent de procesar?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.EdiversaRemadv.Retrocedeix(_EdiversaRemadv, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document de correspondencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Async Sub AfterProcesa(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oCca As DTOCca = e.Argument
        _EdiversaRemadv.Result = oCca.Guid
        Dim exs As New List(Of Exception)
        If Await FEB2.EdiversaRemadv.Update(_EdiversaRemadv, exs) Then
            RaiseEvent AfterUpdate(sender, New MatEventArgs(_EdiversaRemadv))
        Else
            UIHelper.WarnError(exs, "error al desar la remesa")
        End If
    End Sub

    Private Sub RefreshRequest()
    End Sub
End Class
