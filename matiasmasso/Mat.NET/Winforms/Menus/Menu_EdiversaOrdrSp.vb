Public Class Menu_EdiversaOrdrSp

    Inherits Menu_Base

    Private _EdiversaOrdrSps As List(Of DTOEdiversaOrdrsp)
    Private _EdiversaOrdrSp As DTOEdiversaOrdrsp


    Public Sub New(ByVal oEdiversaOrdrSps As List(Of DTOEdiversaOrdrsp))
        MyBase.New()
        _EdiversaOrdrSps = oEdiversaOrdrSps
        If _EdiversaOrdrSps IsNot Nothing Then
            If _EdiversaOrdrSps.Count > 0 Then
                _EdiversaOrdrSp = _EdiversaOrdrSps.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oEdiversaOrdrSp As DTOEdiversaOrdrsp)
        MyBase.New()
        _EdiversaOrdrSp = oEdiversaOrdrSp
        _EdiversaOrdrSps = New List(Of DTOEdiversaOrdrsp)
        If _EdiversaOrdrSp IsNot Nothing Then
            _EdiversaOrdrSps.Add(_EdiversaOrdrSp)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_EdiFile())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub

    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _EdiversaOrdrSps.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_EdiFile() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "desar Edi"
        oMenuItem.Enabled = _EdiversaOrdrSps.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_EdiFile
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
        Dim oFrm As New Frm_EDiversaOrdrSp(_EdiversaOrdrSp)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_EdiFile(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        If FEB2.EdiversaOrdrSp.Load(exs, _EdiversaOrdrSp) Then
            Dim oEdiversaFile = DTOEdiversaOrdrsp.EdiFile(_EdiversaOrdrSp)
            UIHelper.SaveTextFileDialog(oEdiversaFile.Stream, "Confirmació de comanda Edi", "ORDRSP.txt")
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.EdiversaOrdrSp.Delete(exs, _EdiversaOrdrSps.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


