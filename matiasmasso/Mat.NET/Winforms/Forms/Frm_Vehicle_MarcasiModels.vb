Public Class Frm_Vehicle_MarcasiModels
    Private _VehicleMarcas As List(Of DTOVehicleMarca)
    Private _DefaultValue As DTOVehicleModel
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultValue As DTOVehicleModel = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.Browse)
        MyBase.New()
        _DefaultValue = oDefaultValue
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        _AllowEvents = False
        refrescaMarcas()
        refrescaModels()
        _AllowEvents = True

    End Sub

    Private Async Sub refrescaMarcas()
        Dim exs As New List(Of Exception)
        _VehicleMarcas = Await FEB2.VehicleMarcas.All(exs)
        If exs.Count = 0 Then
            Dim oDefaultMarca As DTOVehicleMarca = Nothing
            If _DefaultValue IsNot Nothing Then oDefaultMarca = _DefaultValue.Marca
            Xl_VehicleMarcas1.Load(_VehicleMarcas, oDefaultMarca, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Sub

    Private Sub refrescaModels()
        If Xl_VehicleMarcas1.Value IsNot Nothing Then
            Dim oModels = Xl_VehicleMarcas1.Value.Models
            Xl_VehicleModels1.Load(oModels, _DefaultValue, _SelectionMode)
        End If
    End Sub



    Private Sub Xl_VehicleMarcas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VehicleMarcas1.RequestToRefresh
        refrescaMarcas()
        refrescaModels()
    End Sub

    Private Sub Xl_VehicleModels1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_VehicleModels1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_VehicleMarcas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VehicleMarcas1.RequestToAddNew
        Dim oMarca As New DTOVehicleMarca
        Dim oFrm As New Frm_VehicleMarca(oMarca)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaMarcas
        oFrm.Show()
    End Sub

    Private Sub Xl_VehicleModels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VehicleModels1.RequestToAddNew
        Dim oModel = DTOVehicleModel.Factory(Xl_VehicleMarcas1.Value)
        Dim oFrm As New Frm_VehicleModel(oModel)
        AddHandler oFrm.AfterUpdate, AddressOf refrescaModels
        oFrm.Show()
    End Sub

    Private Sub Xl_VehicleMarcas1_SelectionChanged(sender As Object, e As EventArgs) Handles Xl_VehicleMarcas1.SelectionChanged
        If _AllowEvents Then
            refrescaModels()
        End If
    End Sub

    Private Async Sub Xl_VehicleModels1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VehicleModels1.RequestToRefresh
        Dim exs As New List(Of Exception)
        Dim oModels = Await FEB2.VehicleModels.All(exs)
        If exs.Count = 0 Then
            Dim oBrandModels = oModels.Where(Function(x) x.marca.Equals(Xl_VehicleMarcas1.Value))
            Xl_VehicleModels1.Load(oBrandModels,, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class