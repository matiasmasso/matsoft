Public Class Menu_Insolvencia
    Inherits Menu_Base

    Private _Insolvencias As List(Of DTOInsolvencia)
    Private _Insolvencia As DTOInsolvencia

    Public Sub New(ByVal oInsolvencias As List(Of DTOInsolvencia))
        MyBase.New()
        _Insolvencias = oInsolvencias
        If _Insolvencias IsNot Nothing Then
            If _Insolvencias.Count > 0 Then
                _Insolvencia = _Insolvencias.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oInsolvencia As DTOInsolvencia)
        MyBase.New()
        _Insolvencia = oInsolvencia
        _Insolvencias = New List(Of DTOInsolvencia)
        If _Insolvencia IsNot Nothing Then
            _Insolvencias.Add(_Insolvencia)
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
        oMenuItem.Enabled = _Insolvencias.Count = 1
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
        Dim oFrm As New Frm_Insolvencia(_Insolvencia)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Insolvencia.Delete(_Insolvencias.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


