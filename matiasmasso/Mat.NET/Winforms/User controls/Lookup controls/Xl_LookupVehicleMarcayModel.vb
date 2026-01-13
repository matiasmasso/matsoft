Public Class Xl_LookupVehicleMarcayModel

    Inherits Xl_LookupTextboxButton

    Private _VehicleModelValue As DTOVehicleModel

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property VehicleModelValue() As DTOVehicleModel
        Get
            Return _VehicleModelValue
        End Get
        Set(ByVal value As DTOVehicleModel)
            _VehicleModelValue = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.VehicleModelValue = Nothing
    End Sub

    Private Sub Xl_LookupVehicleModel_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        Dim oFrm As New Frm_Vehicle_MarcasiModels(_VehicleModelValue, DTO.Defaults.SelectionModes.Selection)
        AddHandler oFrm.onItemSelected, AddressOf onVehicleModelSelected
        oFrm.Show()
    End Sub

    Private Sub onVehicleModelSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _VehicleModelValue = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _VehicleModelValue Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _VehicleModelValue.FullNom
            Dim oMenu_VehicleModel As New Menu_VehicleModel(_VehicleModelValue)
            AddHandler oMenu_VehicleModel.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_VehicleModel.Range)
        End If
    End Sub


End Class



