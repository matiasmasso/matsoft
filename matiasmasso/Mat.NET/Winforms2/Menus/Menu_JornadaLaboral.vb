Public Class Menu_JornadaLaboral
    Inherits Menu_Base

    Private _JornadesLaborals As List(Of DTOJornadaLaboral)
    Private _JornadaLaboral As DTOJornadaLaboral

    Public Sub New(ByVal oJornadesLaborals As List(Of DTOJornadaLaboral))
        MyBase.New()
        _JornadesLaborals = oJornadesLaborals
        If _JornadesLaborals IsNot Nothing Then
            If _JornadesLaborals.Count > 0 Then
                _JornadaLaboral = _JornadesLaborals.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oJornadaLaboral As DTOJornadaLaboral)
        MyBase.New()
        _JornadaLaboral = oJornadaLaboral
        _JornadesLaborals = New List(Of DTOJornadaLaboral)
        If _JornadaLaboral IsNot Nothing Then
            _JornadesLaborals.Add(_JornadaLaboral)
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
        oMenuItem.Enabled = _JornadesLaborals.Count = 1
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
        Dim oFrm As New Frm_JornadaLaboral(_JornadaLaboral)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.JornadaLaboral.Delete(exs, _JornadesLaborals.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class


