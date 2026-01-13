Public Class Menu_Immoble

    Inherits Menu_Base

    Private _Immobles As List(Of DTOImmoble)
    Private _Immoble As DTOImmoble

    Public Sub New(ByVal oImmobles As List(Of DTOImmoble))
        MyBase.New()
        _Immobles = oImmobles
        If _Immobles IsNot Nothing Then
            If _Immobles.Count > 0 Then
                _Immoble = _Immobles.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oImmoble As DTOImmoble)
        MyBase.New()
        _Immoble = oImmoble
        _Immobles = New List(Of DTOImmoble)
        If _Immoble IsNot Nothing Then
            _Immobles.Add(_Immoble)
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
        oMenuItem.Enabled = _Immobles.Count = 1
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
        Dim oFrm As New Frm_Immoble(_Immoble)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim exs As New List(Of Exception)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest immoble?", MsgBoxStyle.Exclamation)
        If rc = MsgBoxResult.Ok Then
            If Await FEB.Immoble.Delete(_Immoble, exs) Then
                MyBase.RefreshRequest(Me, New MatEventArgs(_Immoble))
            Else
                UIHelper.WarnError(exs, "error al eliminar l'immoble")
            End If
        End If
    End Sub


End Class


