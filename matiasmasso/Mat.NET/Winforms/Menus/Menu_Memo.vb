

Public Class Menu_Memo
    Private mMem As Mem

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As System.EventArgs)

    Public Sub New(ByVal oMem As Mem)
        MyBase.New()
        mMem = oMem
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() { _
        MenuItem_Zoom(), _
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
        Dim oFrm As New Frm_Contact_Memo(mMem)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Del(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem memo " & mMem.Id & "?", MsgBoxStyle.OkCancel, "M+O")
        If rc = MsgBoxResult.OK Then
            mMem.delete()
            MsgBox("memo " & mMem.Id & " eliminat", MsgBoxStyle.Information, "M+O")
            RaiseEvent AfterUpdate(Me, New System.EventArgs)
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
