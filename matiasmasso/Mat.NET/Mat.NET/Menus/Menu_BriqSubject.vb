Public Class Menu_BriqSubject

    Private _BriqSubject As BriqSubject

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Sub New(ByVal oBriqSubject As BriqSubject)
        MyBase.New()
        _BriqSubject = oBriqSubject
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
        'oMenuItem.Image = My.Resources.prismatics
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
        Dim oFrm As New Frm_BriqSubject(_BriqSubject)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem " & _BriqSubject.Title & "?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs as New List(Of exception)
            If BriqSubjectloader.Delete(_BriqSubject, exs) Then
                MsgBox("Tema " & _BriqSubject.Title & " eliminat", MsgBoxStyle.Information, "M+O")
                RaiseEvent AfterUpdate(Me, New System.EventArgs)
            Else
                UIHelper.WarnError( exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

    Private Sub RefreshRequest(ByVal sender As Object, ByVal e As MatEventArgs)
        RaiseEvent AfterUpdate(sender, e)
    End Sub
End Class
