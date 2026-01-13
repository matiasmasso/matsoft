Public Class Menu_LiniaTelefon

    Inherits Menu_Base

    Private _LiniaTelefons As List(Of DTOLiniaTelefon)
    Private _LiniaTelefon As DTOLiniaTelefon

    Public Sub New(ByVal oLiniaTelefons As List(Of DTOLiniaTelefon))
        MyBase.New()
        _LiniaTelefons = oLiniaTelefons
        If _LiniaTelefons IsNot Nothing Then
            If _LiniaTelefons.Count > 0 Then
                _LiniaTelefon = _LiniaTelefons.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oLiniaTelefon As DTOLiniaTelefon)
        MyBase.New()
        _LiniaTelefon = oLiniaTelefon
        _LiniaTelefons = New List(Of DTOLiniaTelefon)
        If _LiniaTelefon IsNot Nothing Then
            _LiniaTelefons.Add(_LiniaTelefon)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Consums())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _LiniaTelefons.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Consums() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Consums"
        oMenuItem.Enabled = _LiniaTelefons.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Consums
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
        Dim oFrm As New Frm_LiniaTelefon(_LiniaTelefon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Consums(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_LiniaTelConsumsXMes(_LiniaTelefon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.LiniaTelefon.Delete(_LiniaTelefons.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


