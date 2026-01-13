Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class EdiversaSalesReportItemsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/EdiversaSalesReportItems/Cataleg/{year}/{customer}/{proveidor}")>
    Public Function Cataleg(year As Integer, customer As Guid, proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As DTOCustomer = Nothing
            If customer <> Nothing Then oCustomer = New DTOCustomer(customer)
            Dim oProveidor As DTOProveidor = Nothing
            If proveidor <> Nothing Then oProveidor = New DTOProveidor(proveidor)
            Dim values = BEBL.EdiversaSalesReportItems.Cataleg(year, oCustomer, oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaSalesReportItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaSalesReportItems/StatItems/{year}/{user}/{product}/{customer}")>
    Public Function StatItems(year As Integer, user As Guid, product As Guid, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oCustomer As DTOCustomer = Nothing
            If customer <> Nothing Then oCustomer = New DTOCustomer(customer)
            Dim oProduct As DTOProduct = Nothing
            If product <> Nothing Then oProduct = New DTOProduct(product)
            Dim values = BEBL.EdiversaSalesReportItems.StatItems(year, oUser, oProduct, oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les EdiversaSalesReportItems")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaSalesReportItems/StatItems/{year}/{user}/{holding}")>
    Public Function StatItems2(year As Integer, user As Guid, holding As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oHolding As New DTOHolding(holding)
            Dim value = BEBL.EdiversaSalesReportItems.StatItems2(oUser, year, oHolding)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les StatItems2")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/EdiversaSalesReportItems/Excel/{year}/{user}/{holding}/{units}/{brand}/{category}/{sku}")>
    Public Function Excel(year As Integer, user As Guid, holding As Guid, units As DTOStat2.Units, brand As Guid, category As Guid, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oHolding As New DTOHolding(holding)
            Dim oBrand As DTOBaseGuid = Nothing
            Dim oCategory As DTOBaseGuid = Nothing
            Dim oSku As DTOBaseGuid = Nothing
            If Not brand.Equals(Guid.Empty) Then oBrand = New DTOBaseGuid(brand)
            If Not category.Equals(Guid.Empty) Then oCategory = New DTOBaseGuid(category)
            If Not sku.Equals(Guid.Empty) Then oSku = New DTOBaseGuid(sku)
            Dim value = BEBL.EdiversaSalesReportItems.Excel(oUser, year, oHolding, units, oBrand, oCategory, oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al redactar l'Excel dels Sales reports")
        End Try
        Return retval
    End Function


End Class
