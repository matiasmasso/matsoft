Public Class Vehicle


    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOVehicle)
        Return Await Api.Fetch(Of DTOVehicle)(exs, "Vehicle", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oVehicle As DTOVehicle, exs As List(Of Exception)) As Boolean
        If Not oVehicle.IsLoaded And Not oVehicle.IsNew Then
            Dim pVehicle = Api.FetchSync(Of DTOVehicle)(exs, "Vehicle", oVehicle.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOVehicle)(pVehicle, oVehicle, exs)
            End If
            oVehicle.Image = Api.FetchImageSync(exs, "Vehicle/image", oVehicle.Guid.ToString())
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Upload(oVehicle As DTOVehicle, exs As List(Of Exception)) As Task(Of Boolean)
        Dim retval As Boolean
        Dim serialized = Api.Serialize(oVehicle, exs)
        If exs.Count = 0 Then
            Dim oMultipart As New ApiHelper.MultipartHelper()
            oMultipart.AddStringContent("Serialized", serialized)
            oMultipart.AddFileContent("Image", oVehicle.Image)
            retval = Await Api.Upload(oMultipart, exs, "Vehicle")
        End If
        Return retval
    End Function

    Shared Async Function Delete(oVehicle As DTOVehicle, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOVehicle)(oVehicle, exs, "Vehicle")
    End Function
End Class

Public Class Vehicles

    Shared Async Function All(oUser As DTOUser, exs As List(Of Exception)) As Task(Of List(Of DTOVehicle))
        Return Await Api.Fetch(Of List(Of DTOVehicle))(exs, "Vehicles", oUser.Guid.ToString())
    End Function

End Class


Public Class VehicleMarca

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOVehicle.Marca)
        Return Await Api.Fetch(Of DTOVehicle.Marca)(exs, "VehicleMarca", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oVehicleMarca As DTOVehicle.Marca, exs As List(Of Exception)) As Boolean
        If Not oVehicleMarca.IsLoaded And Not oVehicleMarca.IsNew Then
            Dim pVehicleMarca = Api.FetchSync(Of DTOVehicle.Marca)(exs, "VehicleMarca", oVehicleMarca.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOVehicle.Marca)(pVehicleMarca, oVehicleMarca, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oVehicleMarca As DTOVehicle.Marca, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOVehicle.Marca)(oVehicleMarca, exs, "VehicleMarca")
        oVehicleMarca.IsNew = False
    End Function

    Shared Async Function Delete(oVehicleMarca As DTOVehicle.Marca, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOVehicle.Marca)(oVehicleMarca, exs, "VehicleMarca")
    End Function
End Class

Public Class VehicleMarcas

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOVehicle.Marca))
        Dim retval = Await Api.Fetch(Of List(Of DTOVehicle.Marca))(exs, "VehicleMarcas")
        If retval IsNot Nothing Then
            For Each oMarca In retval
                For Each oModel In oMarca.Models
                    oModel.Marca = oMarca
                Next
            Next
        End If
        Return retval
    End Function

End Class



Public Class VehicleModel

    Shared Async Function Find(oGuid As Guid, exs As List(Of Exception)) As Task(Of DTOVehicle.ModelClass)
        Return Await Api.Fetch(Of DTOVehicle.ModelClass)(exs, "VehicleModel", oGuid.ToString())
    End Function

    Shared Function Load(ByRef oVehicleModel As DTOVehicle.ModelClass, exs As List(Of Exception)) As Boolean
        If Not oVehicleModel.IsLoaded And Not oVehicleModel.IsNew Then
            Dim pVehicleModel = Api.FetchSync(Of DTOVehicle.ModelClass)(exs, "VehicleModel", oVehicleModel.Guid.ToString())
            If exs.Count = 0 Then
                DTOBaseGuid.CopyPropertyValues(Of DTOVehicle.ModelClass)(pVehicleModel, oVehicleModel, exs)
            End If
        End If
        Dim retval As Boolean = exs.Count = 0
        Return retval
    End Function

    Shared Async Function Update(oVehicleModel As DTOVehicle.ModelClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Update(Of DTOVehicle.ModelClass)(oVehicleModel, exs, "VehicleModel")
        oVehicleModel.IsNew = False
    End Function

    Shared Async Function Delete(oVehicleModel As DTOVehicle.ModelClass, exs As List(Of Exception)) As Task(Of Boolean)
        Return Await Api.Delete(Of DTOVehicle.ModelClass)(oVehicleModel, exs, "VehicleModel")
    End Function
End Class

Public Class VehicleModels

    Shared Async Function All(exs As List(Of Exception)) As Task(Of List(Of DTOVehicle.ModelClass))
        Return Await Api.Fetch(Of List(Of DTOVehicle.ModelClass))(exs, "VehicleModels")
    End Function

End Class
