Public Class RepController
    Inherits _MatController

    '
    Function Neighbours() As ActionResult

        Dim retval As ActionResult = Nothing
        Select Case MyBase.Authorize({DTORol.Ids.SuperUser,
                                               DTORol.Ids.Admin,
                                               DTORol.Ids.SalesManager,
                                               DTORol.Ids.Comercial,
                                               DTORol.Ids.Rep})
            Case AuthResults.success
                retval = View()
            Case AuthResults.login
                retval = LoginOrView()
            Case AuthResults.denied
                retval = MyBase.UnauthorizedView()
        End Select

        Return retval
    End Function

    Async Function FromGeoLocation(latitud As String, longitud As String) As Threading.Tasks.Task(Of PartialViewResult)
        Dim retval As PartialViewResult = Nothing
        Dim exs As New List(Of Exception)
        Dim culture As IFormatProvider = Globalization.CultureInfo.CreateSpecificCulture("en-US")
        Dim dcLatitud, dcLongitud As Decimal

        If Decimal.TryParse(latitud, Globalization.NumberStyles.Float, culture, dcLatitud) Then
            If Decimal.TryParse(longitud, Globalization.NumberStyles.Float, culture, dcLongitud) Then
                Dim oUser = ContextHelper.GetUser()
                Dim oCoordenadas As New GeoHelper.Coordenadas(dcLatitud, dcLongitud)
                Dim Model = Await FEB.Neighbours.NearestNeighbours(exs, oUser, oCoordenadas, 200, includeSellout:=True)
                retval = PartialView("_Neighbours", Model)
            End If
        End If
        Return retval
    End Function
End Class
