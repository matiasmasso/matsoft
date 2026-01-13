Public Class Xl_LookupVehicle
    Inherits Xl_LookupTextboxButton

    Private _Vehicle As DTOVehicle

    Public Event AfterUpdate(ByVal sender As Object, ByVal e As MatEventArgs)

    Public Shadows Property Vehicle() As DTOVehicle
        Get
            Return _Vehicle
        End Get
        Set(ByVal value As DTOVehicle)
            _Vehicle = value
            refresca()
        End Set
    End Property

    Public Sub Clear()
        Me.Vehicle = Nothing
    End Sub

    Private Sub Xl_LookupVehicle_onLookUpRequest(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.onLookUpRequest
        If _Vehicle IsNot Nothing Then
            Dim exs As New List(Of Exception)
            If FEB2.Vehicle.Load(_Vehicle, exs) Then
                Dim oFrm As New Frm_Vehicles(_Vehicle, DTO.Defaults.SelectionModes.Selection)
                AddHandler oFrm.itemSelected, AddressOf onVehicleSelected
                oFrm.Show()
            Else
                UIHelper.WarnError(exs)
            End If
        End If
    End Sub

    Private Sub onVehicleSelected(ByVal sender As Object, ByVal e As MatEventArgs)
        _Vehicle = e.Argument
        refresca()
        RaiseEvent AfterUpdate(Me, e)
    End Sub

    Private Sub refresca()
        If _Vehicle Is Nothing Then
            MyBase.Text = ""
            MyBase.ClearContextMenu()
        Else
            MyBase.Text = _Vehicle.Nom
            Dim oMenu_Vehicle As New Menu_Vehicle(_Vehicle)
            AddHandler oMenu_Vehicle.AfterUpdate, AddressOf refresca
            MyBase.SetContextMenuRange(oMenu_Vehicle.Range)
        End If
    End Sub


End Class




