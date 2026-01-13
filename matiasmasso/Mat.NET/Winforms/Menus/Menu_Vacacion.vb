Public Class Menu_Vacacion
    Inherits Menu_Base

    Private _Vacacions As List(Of DTOVacacion)
    Private _Vacacion As DTOVacacion

    Public Event RequestToDelete(sender As Object, e As MatEventArgs)

    Public Sub New(ByVal oVacacions As List(Of DTOVacacion))
        MyBase.New()
        _Vacacions = oVacacions
        If _Vacacions IsNot Nothing Then
            If _Vacacions.Count > 0 Then
                _Vacacion = _Vacacions.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oVacacion As DTOVacacion)
        MyBase.New()
        _Vacacion = oVacacion
        _Vacacions = New List(Of DTOVacacion)
        If _Vacacion IsNot Nothing Then
            _Vacacions.Add(_Vacacion)
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
        oMenuItem.Enabled = _Vacacions.Count = 1
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
        Dim oFrm As New Frm_Vacacion(_Vacacion)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        RaiseEvent RequestToDelete(Me, New MatEventArgs(_Vacacion))
    End Sub


End Class


