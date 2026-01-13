

Public Class Menu_Zip
    Private _Zip As DTOZip

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)
    Public Event AfterDelete(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oZip As DTOZip)
        MyBase.New()
        _Zip = oZip
    End Sub

    Public Function Range() As ToolStripMenuItem()
        Return (New ToolStripMenuItem() {MenuItem_Zoom(), MenuItem_Del()})
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
        AddHandler oMenuItem.Click, AddressOf Do_Delete
        Return oMenuItem
    End Function



    '==========================================================================
    '                               EVENT HANDLERS
    '==========================================================================

    Private Sub Do_Zoom(ByVal sender As Object, ByVal e As System.EventArgs)
        If _Zip Is Nothing Then
            Dim oFrm As New Frm_Zip(_Zip)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        Else
            Dim oFrm As New Frm_Zip(_Zip)
            AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
            oFrm.Show()
        End If
    End Sub


    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        If _Zip IsNot Nothing Then
            Dim rc As MsgBoxResult = MsgBox("Eliminem codi postal " & _Zip.ZipCod & "?", MsgBoxStyle.OkCancel, "M+O")
            If rc = MsgBoxResult.Ok Then
                Dim exs As New List(Of Exception)
                If Await FEB2.Zip.Delete(_Zip, exs) Then
                    RaiseEvent AfterUpdate(Me, New MatEventArgs(_Zip))
                Else
                    UIHelper.WarnError(exs)
                End If
            Else
                MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
            End If
        End If
    End Sub

End Class
