Public Class Menu_EdiRemadv
    Private _EdiRemadv As EdiRemadv

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEdiRemadv As EdiRemadv)
        MyBase.New()
        _EdiRemadv = oEdiRemadv
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Procesa(), _
        MenuItem_Delete()})
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

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Retrocedeix a pendent de processar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As MatEventArgs)
        'Dim oFrm As New Frm_EdiRemadv(_EdiRemadv)
        'AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        'oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Retrocedim la remesa " & _EdiRemadv.DocNum & " a pendent de procesar?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If EdiRemadvloader.Delete(_EdiRemadv, exs) Then
                RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError( exs, "error al eliminar el document de correspondencia")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub Do_Procesa(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cobrament(_EdiRemadv)
        AddHandler oFrm.AfterUpdate, AddressOf AfterProcesa
        oFrm.Show()
    End Sub

    Private Sub AfterProcesa(ByVal sender As Object, ByVal e As MatEventArgs)
        Dim oCca As Cca = e.Argument
        _EdiRemadv.Result = oCca.Guid
        Dim exs as New List(Of exception)
        If EdiRemadvLoader.Update(_EdiRemadv, exs) Then
            RaiseEvent AfterUpdate(sender, New MatEventArgs(_EdiRemadv))
        Else
            UIHelper.WarnError( exs, "error al desar la remesa")
        End If
    End Sub

    Private Sub RefreshRequest()
    End Sub
End Class
