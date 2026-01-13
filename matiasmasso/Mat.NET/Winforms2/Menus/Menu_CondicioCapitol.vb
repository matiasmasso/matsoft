Public Class Menu_CondicioCapitol

    Inherits Menu_Base

    Private _CondicioCapitols As List(Of DTOCondicio.Capitol)
    Private _CondicioCapitol As DTOCondicio.Capitol


    Public Sub New(ByVal oCondicioCapitols As DTOCondicio.Capitol.Collection)
        MyBase.New()
        _CondicioCapitols = oCondicioCapitols
        If _CondicioCapitols IsNot Nothing Then
            If _CondicioCapitols.Count > 0 Then
                _CondicioCapitol = _CondicioCapitols.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oCondicioCapitol As DTOCondicio.Capitol)
        MyBase.New()
        _CondicioCapitol = oCondicioCapitol
        _CondicioCapitols = New DTOCondicio.Capitol.Collection
        If _CondicioCapitol IsNot Nothing Then
            _CondicioCapitols.Add(_CondicioCapitol)
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
        oMenuItem.Enabled = _CondicioCapitols.Count = 1
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
        Dim oFrm As New Frm_CondicioCapitol(_CondicioCapitol)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.CondicioCapitol.Delete(_CondicioCapitol, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


