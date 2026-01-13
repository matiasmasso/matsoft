Public Class VehicleController
    Inherits _BaseController

    <HttpPost>
    <Route("api/vehicles")>
    Public Function Vehicles(user As DTOGuidNom) As List(Of DUI.Vehicle)
        Dim retval As New List(Of DUI.Vehicle)
        Dim oUser As DTOUser = BLLUser.Find(user.Guid)
        If oUser IsNot Nothing Then
            Dim oVehicles As List(Of DTOVehicle) = BLLVehicles.All(oUser)
            For Each oVehicle As DTOVehicle In oVehicles
                Dim item As New DUI.Vehicle
                With item
                    .Guid = oVehicle.Guid.ToString
                    .Model = BLLVehicle.MarcaYModel(oVehicle)
                    .Matricula = oVehicle.Matricula
                    .Conductor = oVehicle.Conductor.FullNom
                    .Alta = oVehicle.Alta.Date
                    If oVehicle.Baixa <> Nothing Then
                        .Baixa = oVehicle.Baixa.Date
                    End If

                End With
                retval.Add(item)
            Next
        End If
        Return retval
    End Function
End Class
