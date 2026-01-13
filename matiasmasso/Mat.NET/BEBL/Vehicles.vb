Public Class Vehicle

    Shared Function Find(oGuid As Guid) As DTOVehicle
        Return VehicleLoader.Find(oGuid)
    End Function

    Shared Function Image(oGuid As Guid) As Byte()
        Return VehicleLoader.Image(oGuid)
    End Function

    Shared Function Update(oVehicle As DTOVehicle, exs As List(Of Exception)) As Boolean
        Return VehicleLoader.Update(oVehicle, exs)
    End Function

    Shared Function Delete(oVehicle As DTOVehicle, exs As List(Of Exception)) As Boolean
        Return VehicleLoader.Delete(oVehicle, exs)
    End Function

End Class


Public Class Vehicles
    Shared Function All(oUser As DTOUser) As List(Of DTOVehicle)
        Dim retval As List(Of DTOVehicle) = VehiclesLoader.All(oUser)
        Return retval
    End Function
End Class


Public Class VehicleMarca

    Shared Function Find(oGuid As Guid) As DTOVehicle.Marca
        Dim retval As DTOVehicle.Marca = VehicleMarcaLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oVehicleMarca As DTOVehicle.Marca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleMarcaLoader.Update(oVehicleMarca, exs)
        Return retval
    End Function

    Shared Function Delete(oVehicleMarca As DTOVehicle.Marca, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleMarcaLoader.Delete(oVehicleMarca, exs)
        Return retval
    End Function

End Class



Public Class VehicleMarcas
    Shared Function All() As List(Of DTOVehicle.Marca)
        Dim retval As List(Of DTOVehicle.Marca) = VehicleMarcasLoader.All()
        Return retval
    End Function
End Class

Public Class VehicleModel

    Shared Function Find(oGuid As Guid) As DTOVehicle.ModelClass
        Dim retval As DTOVehicle.ModelClass = VehicleModelLoader.Find(oGuid)
        Return retval
    End Function

    Shared Function Update(oVehicleModel As DTOVehicle.ModelClass, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleModelLoader.Update(oVehicleModel, exs)
        Return retval
    End Function

    Shared Function Delete(oVehicleModel As DTOVehicle.ModelClass, exs As List(Of Exception)) As Boolean
        Dim retval As Boolean = VehicleModelLoader.Delete(oVehicleModel, exs)
        Return retval
    End Function

End Class




