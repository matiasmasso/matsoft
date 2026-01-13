Public Class Menu_VehicleMarca
    Inherits Menu_Base

    Private _VehicleMarcas As List(Of DTOVehicleMarca)
    Private _VehicleMarca As DTOVehicleMarca


    Public Sub New(ByVal oVehicleMarcas As List(Of DTOVehicleMarca))
        MyBase.New()
        _VehicleMarcas = oVehicleMarcas
        If _VehicleMarcas IsNot Nothing Then
            If _VehicleMarcas.Count > 0 Then
                _VehicleMarca = _VehicleMarcas.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oVehicleMarca As DTOVehicleMarca)
        MyBase.New()
        _VehicleMarca = oVehicleMarca
        _VehicleMarcas = New List(Of DTOVehicleMarca)
        If _VehicleMarca IsNot Nothing Then
            _VehicleMarcas.Add(_VehicleMarca)
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
        oMenuItem.Enabled = _VehicleMarcas.Count = 1
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
        Dim oFrm As New Frm_VehicleMarca(_VehicleMarca)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB2.VehicleMarca.Delete(_VehicleMarcas.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class


