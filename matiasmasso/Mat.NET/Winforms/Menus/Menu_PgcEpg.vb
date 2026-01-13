Public Class Menu_PgcEpg

    Private _Epg As DTOPgcEpgBase

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oEpg As DTOPgcEpgBase)
        MyBase.New()
        _Epg = oEpg
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
        MenuItem_Delete()})
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

    Private Function MenuItem_Delete() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Eliminar"
        oMenuItem.Image = My.Resources.del
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        If TypeOf (_Epg) Is DTOPgcEpg0 Then
            Dim oFrm As New Frm_PgcEpg0(_Epg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (_Epg) Is DTOPgcEpg1 Then
            Dim oFrm As New Frm_PgcEpg1(_Epg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (_Epg) Is DTOPgcEpg2 Then
            Dim oFrm As New Frm_PgcEpg2(_Epg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        ElseIf TypeOf (_Epg) Is DTOPgcEpg3 Then
            Dim oFrm As New Frm_PgcEpg3(_Epg)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sNom As String = BLL_PgcEpgBase.Nom(_Epg, BLL.BLLSession.Current.Lang)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & sNom & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If TypeOf (_Epg) Is DTOPgcEpg0 Then
                If BLL_PgcEpg0.Delete(_Epg, exs) Then
                    MsgBox(" " & sNom & " eliminat", MsgBoxStyle.Information, "M+O")
                    RaiseEvent AfterUpdate(Me, New System.EventArgs)
                Else
                    UIHelper.WarnError( exs, "error al eliminar el compte")
                End If
            ElseIf TypeOf (_Epg) Is DTOPgcEpg1 Then
                If BLL_PgcEpg1.Delete(_Epg, exs) Then
                    MsgBox(" " & sNom & " eliminat", MsgBoxStyle.Information, "M+O")
                    RaiseEvent AfterUpdate(Me, New System.EventArgs)
                Else
                    UIHelper.WarnError( exs, "error al eliminar el compte")
                End If
            ElseIf TypeOf (_Epg) Is DTOPgcEpg2 Then
                If BLL_PgcEpg2.Delete(_Epg, exs) Then
                    MsgBox(" " & sNom & " eliminat", MsgBoxStyle.Information, "M+O")
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Else
                    UIHelper.WarnError( exs, "error al eliminar el compte")
                End If
            ElseIf TypeOf (_Epg) Is DTOPgcEpg3 Then
                If BLL_PgcEpg3.Delete(_Epg, exs) Then
                    MsgBox(" " & sNom & " eliminat", MsgBoxStyle.Information, "M+O")
                    RaiseEvent AfterUpdate(Me, MatEventArgs.Empty)
                Else
                    UIHelper.WarnError( exs, "error al eliminar el compte")
                End If
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class

