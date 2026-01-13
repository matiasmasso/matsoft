Public Class DTOVehicleMarca
    Inherits DTOBaseGuid
    Property nom As String
    Property models As List(Of DTOVehicleModel)

    Public Sub New()
        MyBase.New()
        _Models = New List(Of DTOVehicleModel)
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
        _Models = New List(Of DTOVehicleModel)
    End Sub
End Class
