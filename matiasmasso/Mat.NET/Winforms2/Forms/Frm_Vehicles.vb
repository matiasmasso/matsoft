Public Class Frm_Vehicles
    Private _Values As List(Of DTOVehicle)
    Private _DefaultValue As DTOVehicle
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse

    Public Event itemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVehicle = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
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
        _Values = Await FEB.Vehicles.All(oUser, exs)
        If exs.Count = 0 Then
            Xl_Vehicles1.Load(_Values, _DefaultValue, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub MostrarObsoletosToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MostrarObsoletosToolStripMenuItem.Click
        Xl_Vehicles1.DisplayObsolets = Not Xl_Vehicles1.DisplayObsolets
        Xl_Vehicles1.Load(_Values, _DefaultValue, _SelectionMode)
    End Sub
End Class