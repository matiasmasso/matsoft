Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerRankingController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerRanking/{user}/{product}/{area}/{fchFrom}/{fchTo}")>
    Public Function Find(user As Guid, product As Guid, area As Guid, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oProduct As DTOProduct = Nothing
            If Not product.Equals(Guid.Empty) Then oProduct = New DTOProduct(product)
            Dim oArea As DTOArea = Nothing
            If Not area.Equals(Guid.Empty) Then oArea = New DTOArea(area)
            Dim value = DTOCustomerRanking.Factory(oUser, oProduct, oArea, fchfrom, fchto)
            BEBL.CustomerRanking.Load(value)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ranking de clients")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerRanking/Csv/{user}/{product}/{area}/{fchFrom}/{fchTo}")>
    Public Function Csv(user As Guid, product As Guid, area As Guid, fchfrom As Date, fchto As Date) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user) 'doncs necessita el rol
            Dim oProduct As DTOProduct = Nothing
            If Not product.Equals(Guid.Empty) Then oProduct = New DTOProduct(product)
            Dim oArea As DTOArea = Nothing
            If Not area.Equals(Guid.Empty) Then oArea = New DTOArea(area)
            Dim value = DTOCustomerRanking.Factory(oUser, oProduct, oArea, fchfrom, fchto)
            BEBL.CustomerRanking.Load(value)
            retval = MyBase.HttpFileAttachmentResponseMessage(value.Csv.toByteArray, "M+O Customer Ranking")
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir el ranking de clients")
        End Try
        Return retval
    End Function

End Class
