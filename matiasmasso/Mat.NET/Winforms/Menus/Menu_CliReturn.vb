Public Class Menu_CliReturn
    Inherits Menu_Base

    Private _CliReturns As List(Of DTOCliReturn)
    Private _CliReturn As DTOCliReturn

    Public Sub New(ByVal oCliReturns As List(Of DTOCliReturn))
        MyBase.New()
        _CliReturns = oCliReturns
        If _CliReturns IsNot Nothing Then
            If _CliReturns.Count > 0 Then
                _CliReturn = _CliReturns.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCliReturn As DTOCliReturn)
        MyBase.New()
        _CliReturn = oCliReturn
        _CliReturns = New List(Of DTOCliReturn)
        If _CliReturn IsNot Nothing Then
            _CliReturns.Add(_CliReturn)
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
        oMenuItem.Enabled = _CliReturns.Count = 1
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
        Dim oFrm As New Frm_CliReturn(_CliReturn)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.CliReturn.Delete(_CliReturns.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


