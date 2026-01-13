Public Class Menu_Cod
    Inherits Menu_Base

    Private _Cods As List(Of DTOCod)
    Private _Cod As DTOCod

    Public Sub New(ByVal oCods As List(Of DTOCod))
        MyBase.New()
        _Cods = oCods
        If _Cods IsNot Nothing Then
            If _Cods.Count > 0 Then
                _Cod = _Cods.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCod As DTOCod)
        MyBase.New()
        _Cod = oCod
        _Cods = New List(Of DTOCod)
        If _Cod IsNot Nothing Then
            _Cods.Add(_Cod)
        End If
        AddMenuItems()
    End Sub

    Private Sub AddMenuItems()
        MyBase.AddMenuItem(MenuItem_Zoom())
        If _Cod.Parent Is Nothing Then
            MyBase.AddMenuItem(MenuItem_Codis())
        End If
        MyBase.AddMenuItem(MenuItem_Delete())
    End Sub


    '==========================================================================
    '                            MENU ITEMS BUILDER
    '==========================================================================

    Private Function MenuItem_Zoom() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Zoom"
        oMenuItem.Enabled = _Cods.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Zoom
        Return oMenuItem
    End Function

    Private Function MenuItem_Codis() As ToolStripMenuItem
        Dim oMenuItem As New ToolStripMenuItem
        oMenuItem.Text = "Codis"
        oMenuItem.Enabled = _Cods.Count = 1
        AddHandler oMenuItem.Click, AddressOf Do_Codis
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
        Dim oFrm As New Frm_Cod(_Cod)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Codis(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim oFrm As New Frm_Cods(_Cod)
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.Cod.Delete(exs, _Cods.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

