Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerTarifaController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerTarifa/{userOrCustomer}")> ' SwiftUI
    Public Function LoadForIMat3(userOrCustomer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUserorCustomer As DTOBaseGuid = BEBL.User.Find(userOrCustomer)
            If oUserorCustomer Is Nothing Then
                oUserorCustomer = BEBL.Customer.Find(userOrCustomer)
            End If
            Dim oTarifa = BEBL.CustomerTarifa.Load(oUserorCustomer)
            Dim value = oTarifa.Brands
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
            'retval = MyBase.HttpIgnoreNullsResponseMessage(Of List(Of DTOProductBrand.Treenode))(value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Tarifa de client")
        End Try
        Return retval
    End Function


    <HttpGet>
    <Route("api/CustomerTarifa/Load/{userOrCustomer}/{fch}/{includeObsoletos?}")>
    Public Function Find(userOrCustomer As Guid, fch As Date, Optional includeObsoletos As Integer? = Nothing) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUserorCustomer As DTOBaseGuid = BEBL.User.Find(userOrCustomer)
            If oUserorCustomer Is Nothing Then
                oUserorCustomer = BEBL.Customer.Find(userOrCustomer)
            End If
            Dim blIncludeObsoletos As Boolean = If(includeObsoletos Is Nothing, False, includeObsoletos <> 0)
            Dim value As DTOCustomerTarifa.Compact
            value = BEBL.CustomerTarifa.Load(oUserorCustomer, fch, IncludeObsoletos:=blIncludeObsoletos)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la Tarifa de client")
        End Try
        Return retval
    End Function




End Class
