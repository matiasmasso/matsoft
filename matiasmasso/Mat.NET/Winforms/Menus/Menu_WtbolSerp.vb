Public Class Menu_WtbolSerp
    Inherits Menu_Base

    Private _WtbolSerps As List(Of DTOWtbolSerp)
    Private _WtbolSerp As DTOWtbolSerp

    Public Sub New(ByVal oWtbolSerps As List(Of DTOWtbolSerp))
        MyBase.New()
        _WtbolSerps = oWtbolSerps
        If _WtbolSerps IsNot Nothing Then
            If _WtbolSerps.Count > 0 Then
                _WtbolSerp = _WtbolSerps.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oWtbolSerp As DTOWtbolSerp)
        MyBase.New()
        _WtbolSerp = oWtbolSerp
        _WtbolSerps = New List(Of DTOWtbolSerp)
        If _WtbolSerp IsNot Nothing Then
            _WtbolSerps.Add(_WtbolSerp)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        MyBase.AddMenuItem(MenuItem_Browse())
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _WtbolSerps.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Browse() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Navegar"
        oMenuItem.Enabled = _WtbolSerps.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Browse
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
        Dim oFrm As New Frm_WtbolSerp(_WtbolSerp)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Browse(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = FEB2.WtbolSerp.Url(_WtbolSerp.Product, Current.Session.Lang, True)
        UIHelper.ShowHtml(url)
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.WtbolSerp.Delete(_WtbolSerps.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


