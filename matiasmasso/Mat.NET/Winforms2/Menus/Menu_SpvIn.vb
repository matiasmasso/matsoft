Public Class Menu_SpvIn

    Inherits Menu_Base

    Private _SpvIns As List(Of DTOSpvIn)
    Private _SpvIn As DTOSpvIn


    Public Sub New(ByVal oSpvIns As List(Of DTOSpvIn))
        MyBase.New()
        _SpvIns = oSpvIns
        If _SpvIns IsNot Nothing Then
            If _SpvIns.Count > 0 Then
                _SpvIn = _SpvIns.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oSpvIn As DTOSpvIn)
        MyBase.New()
        _SpvIn = oSpvIn
        _SpvIns = New List(Of DTOSpvIn)
        If _SpvIn IsNot Nothing Then
            _SpvIns.Add(_SpvIn)
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
        oMenuItem.Enabled = _SpvIns.Count = 1
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
        Dim oFrm As New Frm_SpvIn(_SpvIn)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim sQuery As String = DTOSpvIn.DeleteQuery(_SpvIns)
        Dim rc As MsgBoxResult = MsgBox(sQuery, MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            For Each item As DTOSpvIn In _SpvIns
                Dim ext As New List(Of Exception)
                If Not Await FEB.SpvIn.Delete(item, ext) Then
                    exs.Add(New Exception("error al eliminar la entrada " & item.Id & " del " & Format(item, "dd/MM/yy") & ":"))
                    exs.Add(ext.First)
                End If

            Next

            MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            If exs.Count > 0 Then
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If

        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


