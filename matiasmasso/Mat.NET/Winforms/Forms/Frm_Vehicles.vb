Public Class Frm_Vehicles
    Private _DefaultValue As DTOVehicle
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVehicle = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Vehicles_Load(sender As Object, e As EventArgs) Handles Me.Load
        refresca()
    End Sub

    Private Sub Xl_Vehicles1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_Vehicles1.onItemSelected
        RaiseEvent itemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_Vehicles1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_Vehicles1.RequestToAddNew
        Dim oVehicle = DTOVehicle.Factory(Current.Session.Emp)
        Dim oFrm As New Frm_Vehicle(oVehicle)
        AddHandler oFrm.AfterUpdate, AddressOf refresca
        oFrm.Show()
    End Sub

    Private Sub Xl_Vehicles1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_Vehicles1.RequestToRefresh
        refresca()
    End Sub

    Private Async Sub refresca()
        Dim oUser As DTOUser = Current.Session.User
        Dim exs As New List(Of Exception)
        Dim oVehicles = Await FEB2.Vehicles.All(oUser, exs)
        If exs.Count = 0 Then
            Xl_Vehicles1.Load(oVehicles, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

End Class