Public Class Menu_Plantilla
    Inherits Menu_Base

    Private _Plantillas As List(Of DTOPlantilla)
    Private _Plantilla As DTOPlantilla

    Public Sub New(ByVal oPlantillas As List(Of DTOPlantilla))
        MyBase.New()
        _Plantillas = oPlantillas
        If _Plantillas IsNot Nothing Then
            If _Plantillas.Count > 0 Then
                _Plantilla = _Plantillas.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oPlantilla As DTOPlantilla)
        MyBase.New()
        _Plantilla = oPlantilla
        _Plantillas = New List(Of DTOPlantilla)
        If _Plantilla IsNot Nothing Then
            _Plantillas.Add(_Plantilla)
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
        oMenuItem.Enabled = _Plantillas.Count = 1
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
        Dim oFrm As New Frm_Plantilla(_Plantilla)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem aquest registre?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.Plantilla.Delete(exs, _Plantillas.First) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el registre")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub

End Class

