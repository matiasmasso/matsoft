Public Class Menu_TrpZon

    Inherits Menu_Base

    Private _TrpZons As List(Of DTOTrpZon)
    Private _TrpZon As DTOTrpZon


    Public Sub New(ByVal oTrpZons As List(Of DTOTrpZon))
        MyBase.New()
        _TrpZons = oTrpZons
        If _TrpZons IsNot Nothing Then
            If _TrpZons.Count > 0 Then
                _TrpZon = _TrpZons.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oTrpZon As DTOTrpZon)
        MyBase.New()
        _TrpZon = oTrpZon
        _TrpZons = New List(Of DTOTrpZon)
        If _TrpZon IsNot Nothing Then
            _TrpZons.Add(_TrpZon)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _TrpZons.Count = 1
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
        Dim oFrm As New Frm_TrpZon(_TrpZon)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox(String.Format("Eliminem {0}?", _TrpZon.Nom), MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.TrpZon.Delete(_TrpZons.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancel·lada per l'usuari", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


