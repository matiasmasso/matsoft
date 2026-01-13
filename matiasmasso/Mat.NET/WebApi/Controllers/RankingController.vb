Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class RankingController
    Inherits _BaseController

    <HttpGet>
    <Route("api/ranking/CustomerRanking/{user}/{product}")>
    Public Function CustomerRanking(user As Guid, product As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oProduct = DTOBaseGuid.Opcional(Of DTOProduct)(product)
            Dim value = BEBL.Ranking.CustomerRanking(oUser, oProduct)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ranking")
        End Try
        Return retval
    End Function

    <HttpPost>
    <Route("api/ranking/LoadItems")>
    Public Function LoadItems() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTORanking)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar el ranking")
            Else
                BEBL.Ranking.LoadItems(value)
                result = Request.CreateResponse(HttpStatusCode.OK, value)
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.TemplateLoader")
        End Try

        Return result

    End Function

    <HttpPost>
    <Route("api/ranking/LoadCatalog")>
    Public Function LoadCatalog(<FromBody> oRanking As DTORanking) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            BEBL.Ranking.LoadCatalog(oRanking)
            BEBL.Ranking.LoadItems(oRanking)
            retval = Request.CreateResponse(HttpStatusCode.OK, oRanking)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ranking")
        End Try
        Return retval
    End Function

End Class

Public Class RankingsController
    Inherits _BaseController



End Class
