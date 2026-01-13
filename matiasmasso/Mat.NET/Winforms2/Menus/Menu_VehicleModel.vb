Public Class Menu_VehicleModel
    Inherits Menu_Base

    Private _VehicleModels As List(Of DTOVehicle.ModelClass)
    Private _VehicleModel As DTOVehicle.ModelClass


    Public Sub New(ByVal oVehicleModels As List(Of DTOVehicle.ModelClass))
        MyBase.New()
        _VehicleModels = oVehicleModels
        If _VehicleModels IsNot Nothing Then
            If _VehicleModels.Count > 0 Then
                _VehicleModel = _VehicleModels.First
            End If
        End If
        AddMenuItems()
    End Sub

    Public Sub New(ByVal oVehicleModel As DTOVehicle.ModelClass)
        MyBase.New()
        _VehicleModel = oVehicleModel
        _VehicleModels = New List(Of DTOVehicle.ModelClass)
        If _VehicleModel IsNot Nothing Then
            _VehicleModels.Add(_VehicleModel)
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
        oMenuItem.Enabled = _VehicleModels.Count = 1
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
        Dim oFrm As New Frm_VehicleModel(_VehicleModel)
        AddHandler oFrm.AfterUpdate, AddressOf RefreshRequest
        oFrm.Show()
    End Sub

    Private Async Sub Do_Delete(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim rc As MsgBoxResult = MsgBox("Eliminem ?", MsgBoxStyle.OkCancel)
        If rc = MsgBoxResult.Ok Then
            Dim exs As New List(Of Exception)
            If Await FEB.VehicleModel.Delete(_VehicleModels.First, exs) Then
                MyBase.RefreshRequest(Me, MatEventArgs.Empty)
            Else
                UIHelper.WarnError(exs, "error al eliminar el document")
            End If
        Else
            MsgBox("Operació cancelada", MsgBoxStyle.Exclamation, "M+O")
        End If
    End Sub


End Class
