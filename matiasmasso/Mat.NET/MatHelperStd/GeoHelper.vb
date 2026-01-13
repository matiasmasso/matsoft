Public Class GeoHelper

    Public Class Coordenadas
        Property longitud As Double 'lowercase due to dependency on IOS iMat 24/3/2022
        Property latitud As Double  'lowercase due to dependency on IOS iMat 24/3/2022

        Public Sub New(DcLatitud As Double, DcLongitud As Double)
            MyBase.New
            _longitud = DcLongitud
            _latitud = DcLatitud
        End Sub

        Shared Function Factory(Lat As Object, Lng As Object) As GeoHelper.Coordenadas
            Dim retval As GeoHelper.Coordenadas = Nothing
            If Convert.IsDBNull(Lat) Or Convert.IsDBNull(Lng) Then
            Else
                retval = New GeoHelper.Coordenadas(Lat, Lng)
            End If
            Return retval
        End Function

        Shared Function Text(lat As Double, Lng As Double) As String
            Dim retval As String = String.Format("lat {0:#.000000}, lng {1:#.000000}", lat, Lng)
            Return retval
        End Function

        Public Function distance(ByVal oCoordFrom As GeoHelper.Coordenadas, oCoordTo As GeoHelper.Coordenadas) As Double
            Dim retval = distance(oCoordFrom.latitud, oCoordFrom.longitud, oCoordTo.latitud, oCoordTo.longitud)
            Return retval
        End Function

        Public Function distance(ByVal lat1 As Double, ByVal lon1 As Double, ByVal lat2 As Double, ByVal lon2 As Double, Optional ByVal unit As Char = "K") As Double
            If lat1 = lat2 And lon1 = lon2 Then
                Return 0
            Else
                Dim theta As Double = lon1 - lon2
                Dim dist As Double = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta))
                dist = Math.Acos(dist)
                dist = rad2deg(dist)
                dist = dist * 60 * 1.1515
                If unit = "K" Then
                    dist = dist * 1.609344
                ElseIf unit = "N" Then
                    dist = dist * 0.8684
                End If
                Return dist
            End If
        End Function

        Private Function deg2rad(ByVal deg As Double) As Double
            Return (deg * Math.PI / 180.0)
        End Function

        Private Function rad2deg(ByVal rad As Double) As Double
            Return rad / Math.PI * 180.0
        End Function



    End Class
End Class
