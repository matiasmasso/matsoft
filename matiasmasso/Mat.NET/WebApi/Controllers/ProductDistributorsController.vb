Imports System.Net
Imports System.Net.Http
Imports System.Web.Http
Public Class ProductDistributorsController
    Inherits _BaseController

    <HttpGet>
    <Route("api/productdistributors/{manufacturer}")> 'DEPRECATED --------------------------
    Public Function FromManufacturer_Deprecated(manufacturer As Guid) As List(Of DTOProductRetailer)
        Dim oProveidor As New DTOProveidor(manufacturer)
        Dim retval As List(Of DTOProductRetailer) = BEBL.ProductDistributors.List(oProveidor)
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/FromManufacturer/{manufacturer}")>
    Public Function FromManufacturer(manufacturer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(manufacturer)
            Dim values As List(Of DTOProductRetailer) = BEBL.ProductDistributors.List(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/FromBrand/{brand}")>
    Public Function FromBrand(brand As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oBrand As New DTOProductBrand(brand)
            Dim values As List(Of DTOProductRetailer) = BEBL.ProductDistributors.List(oBrand)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/FromUser/{user}")>
    Public Function FromUser(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values As List(Of DTOProductDistributor) = BEBL.ProductDistributors.List(oUser)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/FromRep/{rep}")>
    Public Function FromRep(rep As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oRep As New DTORep(rep)
            Dim values As List(Of DTOProductDistributor) = BEBL.ProductDistributors.List(oRep)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/DistribuidorsOficials/{user}/{brand}/{incentiu}")>
    Public Function DistribuidorsOficials(user As Guid, brand As Guid, incentiu As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oBrand As New DTOProductBrand(brand)
            Dim oIncentiu = DTOBaseGuid.Opcional(Of DTOIncentiu)(incentiu)
            Dim values = BEBL.ProductDistributors.DistribuidorsOficials(oUser, oBrand, oIncentiu)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir els distribuidors")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/Zonas/{product}/{country}")>
    Public Function Zonas(product As Guid, country As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim oCountry As New DTOCountry(country)
            Dim values = BEBL.ProductDistributors.Zonas(oProduct, oCountry)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les  zones")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/BestLocation/{product}/{parentArea}")>
    Public Function BestLocation(product As Guid, parentArea As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProduct As New DTOProduct(product)
            Dim oParentArea = DTOBaseGuid.Opcional(Of DTOArea)(parentArea)
            Dim values = BEBL.ProductDistributors.BestLocation(oProduct, oParentArea)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la millor ubicació")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/productdistributors/PerChannel/{user}/{year}")>
    Public Function PerChannel(user As Guid, year As Integer) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim values = BEBL.ProductDistributors.PerChannel(oUser, year)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir la millor ubicació")
        End Try
        Return retval
    End Function

End Class
