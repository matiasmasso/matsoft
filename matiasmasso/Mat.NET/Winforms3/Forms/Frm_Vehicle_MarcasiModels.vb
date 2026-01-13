Public Class Frm_Vehicle_MarcasiModels
    Private _VehicleMarcas As List(Of DTOVehicle.Marca)
    Private _DefaultMarca As DTOVehicle.Marca
    Private _DefaultModel As DTOVehicle.ModelClass
    Private _SelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse
    Private _AllowEvents As Boolean

    Public Event onItemSelected(sender As Object, e As MatEventArgs)

    Public Sub New(Optional oDefaultModel As DTOVehicle.ModelClass = Nothing, Optional oSelectionMode As DTO.Defaults.SelectionModes = DTO.Defaults.SelectionModes.browse)
        MyBase.New()
        _DefaultModel = oDefaultModel
        If _DefaultModel IsNot Nothing Then _DefaultMarca = _DefaultModel.Marca
        _SelectionMode = oSelectionMode
        Me.InitializeComponent()
    End Sub

    Private Async Sub Frm_Load(sender As Object, e As EventArgs) Handles Me.Load
        _AllowEvents = False
        Await reloadMarcas()
        refrescaModels()
        _AllowEvents = True

    End Sub

    Private Function CurrentMarca() As DTOVehicle.Marca
        Return Xl_VehicleMarcas1.Value
    End Function

    Private Function CurrentModel() As DTOVehicle.ModelClass
        Return Xl_VehicleModels1.Value
    End Function

    Private Async Sub reloadMarcas(sender As Object, e As MatEventArgs)
        Await reloadMarcas()
    End Sub
    Private Async Function reloadMarcas() As Task
        Dim exs As New List(Of Exception)
        _VehicleMarcas = Await FEB.VehicleMarcas.All(exs)
        If exs.Count = 0 Then
            Xl_VehicleMarcas1.Load(_VehicleMarcas, _DefaultMarca, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub refrescaMarcas()
        Xl_VehicleMarcas1.Load(_VehicleMarcas, _DefaultMarca, _SelectionMode)
    End Sub

    Private Async Function refrescaModels(sender As Object, e As MatEventArgs) As Task
        Dim exs As New List(Of Exception)
        _DefaultModel = e.Argument
        _VehicleMarcas = Await FEB.VehicleMarcas.All(exs)
        If exs.Count = 0 And CurrentMarca() IsNot Nothing Then
            Dim oMarca = _VehicleMarcas.FirstOrDefault(Function(x) x.Guid.Equals(CurrentMarca.Guid))
            Xl_VehicleModels1.Load(oMarca.Models, _DefaultModel, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If
    End Function

    Private Sub refrescaModels()
        If CurrentMarca() IsNot Nothing Then
            Xl_VehicleModels1.Load(CurrentMarca.Models, _DefaultModel, _SelectionMode)
        End If
    End Sub

    Private Sub Xl_VehicleMarcas1_RequestToRefresh(sender As Object, e As MatEventArgs) Handles Xl_VehicleMarcas1.RequestToRefresh
        _DefaultMarca = e.Argument
        refrescaMarcas()
        refrescaModels()
    End Sub

    Private Sub Xl_VehicleModels1_onItemSelected(sender As Object, e As MatEventArgs) Handles Xl_VehicleModels1.onItemSelected
        RaiseEvent onItemSelected(Me, e)
        Me.Close()
    End Sub

    Private Sub Xl_VehicleMarcas1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VehicleMarcas1.RequestToAddNew
        Dim oMarca As New DTOVehicle.Marca
        Dim oFrm As New Frm_VehicleMarca(oMarca)
        AddHandler oFrm.AfterUpdate, AddressOf reloadMarcas
        oFrm.Show()
    End Sub

    Private Sub Xl_VehicleModels1_RequestToAddNew(sender As Object, e As MatEventArgs) Handles Xl_VehicleModels1.RequestToAddNew
        Dim oModel = DTOVehicle.ModelClass.Factory(Xl_VehicleMarcas1.Value)
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
        Dim oModels = Await FEB.VehicleModels.All(exs)
        If exs.Count = 0 Then
            Dim oBrandModels = oModels.Where(Function(x) x.marca.Equals(Xl_VehicleMarcas1.Value))
            Xl_VehicleModels1.Load(oBrandModels,, _SelectionMode)
        Else
            UIHelper.WarnError(exs)
        End If

    End Sub
End Class