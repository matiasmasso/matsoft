Public Class DTOVehicleModel
    Inherits DTOBaseGuid

    Property marca As DTO.DTOVehicleMarca
    Property nom As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(oGuid As Guid)
        MyBase.New(oGuid)
    End Sub

    Shared Function factory(oMarca As DTOVehicleMarca) As DTOVehicleModel
        Dim retval As New DTOVehicleModel
        retval.marca = oMarca
        Return retval
    End Function

    Public Function fullNom() As String
        Dim sb As New Text.StringBuilder
        If _marca IsNot Nothing Then
            sb.Append(_marca.nom & " ")
        End If
        sb.Append(_nom)
        Dim retval As String = sb.ToString
        Return retval
    End Function
End Class
