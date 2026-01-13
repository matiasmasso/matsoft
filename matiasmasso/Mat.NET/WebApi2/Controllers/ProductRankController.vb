Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class ProductRankController
    Inherits _BaseController


    <HttpGet>
    <Route("api/ProductRank/{user}/{period}/{area}/{unit}")>
    Public Function Load(user As Guid, period As DTOProductRank.Periods, area As Guid, unit As DTOProductRank.Units) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oArea As DTOArea = Nothing
            If Not area.Equals(Guid.Empty) Then oArea = New DTOArea(area)
            Dim value = BEBL.ProductRank.Load(oUser, period, oArea, unit)

            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el ranking de productes")
        End Try
        Return retval
    End Function

End Class
