Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class VehicleController
    Inherits _BaseController


    <HttpGet>
    <Route("api/Vehicle/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Vehicle.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Vehicle")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/Vehicle/image/{guid}")>
    Public Function Image(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.Vehicle.Image(guid)
            retval = MyBase.HttpImageResponseMessage(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el image del Vehicle")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/Vehicle")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)

        Dim resultHash As String = ""
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim oVehicle = ApiHelper.Client.DeSerialize(Of DTOVehicle)(json, exs)
            If oVehicle Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el vehicle")
            Else
                oVehicle.Image = oHelper.GetImage("image")
                If DAL.VehicleLoader.Update(oVehicle, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar la imatge del vehicle")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error al desar el vehicle")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/Vehicle/delete")>
    Public Function Delete(<FromBody> value As DTOVehicle) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.Vehicle.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Vehicle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Vehicle")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/vehicles/{user}")>
    Public Function All(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            If oUser Is Nothing Then
                retval = MyBase.HttpErrorResponseMessage("Usuari desconegut")
            Else
                Dim values As List(Of DTOVehicle) = BEBL.Vehicles.All(oUser)
                retval = Request.CreateResponse(HttpStatusCode.OK, values)
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els vehicles")
        End Try
        Return retval
    End Function


End Class



Public Class VehicleMarcaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VehicleMarca/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.VehicleMarca.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Marca de Vehicle")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/VehicleMarca")>
    Public Function Update(<FromBody> value As DTOVehicle.Marca) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VehicleMarca.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la Marca de Vehicle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la Marca de Vehicle")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VehicleMarca/delete")>
    Public Function Delete(<FromBody> value As DTOVehicle.Marca) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VehicleMarca.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la Marca de Vehicle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la Marca de Vehicle")
        End Try
        Return retval
    End Function

End Class

Public Class VehicleMarcasController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VehicleMarcas")>
    Public Function All() As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim values = BEBL.VehicleMarcas.All()
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Marques de Vehicle")
        End Try
        Return retval
    End Function

End Class


Public Class VehicleModelController
    Inherits _BaseController

    <HttpGet>
    <Route("api/VehicleModel/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.VehicleModel.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el Model de Vehicle")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VehicleModel")>
    Public Function Update(<FromBody> value As DTOVehicle.ModelClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VehicleModel.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar el Model de Vehicle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar el Model de Vehicle")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/VehicleModel/delete")>
    Public Function Delete(<FromBody> value As DTOVehicle.ModelClass) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.VehicleModel.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar el Model de Vehicle")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar el Model de Vehicle")
        End Try
        Return retval
    End Function

End Class


