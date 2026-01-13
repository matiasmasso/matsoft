Imports System.Net
Imports System.Net.Http
Imports System.Web.Http


Public Class NeighboursController
    Inherits _BaseController

    <HttpGet>
    <Route("api/Neighbours/{user}/{latitud}/{longitud}/{iCount}/{includeSellout}")>
    Public Function All(user As Guid, latitud As String, longitud As String, iCount As Integer, includeSellout As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim dcLatitud, dcLongitud As Decimal
            Dim culture As IFormatProvider = Globalization.CultureInfo.CreateSpecificCulture("en-US")
            If Decimal.TryParse(latitud, Globalization.NumberStyles.Float, culture, dcLatitud) Then
                If Decimal.TryParse(longitud, Globalization.NumberStyles.Float, culture, dcLongitud) Then
                    Dim oUser = BEBL.User.Find(user)
                    Dim oCoordenadas As New GeoHelper.Coordenadas(dcLatitud, dcLongitud)
                    Dim values = BEBL.Neighbours.NearestNeighbours(oUser, oCoordenadas, iCount, (includeSellout <> 0))
                    retval = Request.CreateResponse(HttpStatusCode.OK, values)
                Else
                    retval = MyBase.HttpErrorResponseMessage("Error al llegir lla longitud")
                End If
            Else
                retval = MyBase.HttpErrorResponseMessage("Error al llegir la latitud")
            End If

        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les Neighbours")
        End Try
        Return retval
    End Function

End Class

