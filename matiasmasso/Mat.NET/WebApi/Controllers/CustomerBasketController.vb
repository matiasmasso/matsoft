Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerBasketController
    Inherits _BaseController


    <HttpGet>
    <Route("api/CustomerBasket/{customer}")>
    Public Function Model(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Dim exs As New List(Of Exception)
        Try
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim value = BEBL.CustomerBasket.Model(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerBasket/Catalog/{user}/{customer}")>
    Public Function Catalog(user As Guid, customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oEmp = MyBase.GetEmp(oUser.Emp.Id)
            Dim oCustomer = BEBL.Customer.Find(customer)
            Dim value = BEBL.CustomerBasket.Catalog(oUser, oCustomer, oEmp.Mgz)
            value.Customer = oCustomer 'reposa perque no surti Ccx
            If value.Customer.Nom = "" Then value.Customer.Nom = value.Customer.NomComercial

            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerBasket/Catalog/{user}")>
    Public Function Catalog(user As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oEmp = MyBase.GetEmp(oUser.Emp.Id)
            Dim oCustomer = DTOCustomer.fromContact(oUser.Contact)
            Dim value = BEBL.CustomerBasket.Catalog(oUser, oCustomer, oEmp.Mgz)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerBasket/Catalog2/{user}/{lang}")>
    Public Function Catalog2(user As Guid, lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oUser = BEBL.User.Find(user)
            Dim oLang = DTOLang.Factory(lang)
            Dim oEmp = MyBase.GetEmp(oUser.Emp.Id)
            Dim oCustomer = DTOCustomer.fromContact(oUser.Contact)
            Dim value = BEBL.CustomerBasket.Catalog(oUser, oCustomer, oEmp.Mgz, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerBasket/Catalog/consumer/{lang}")>
    Public Function Catalog2(lang As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oLang = DTOLang.Factory(lang)
            Dim oEmp = New DTOEmp(DTOEmp.Ids.MatiasMasso)
            Dim value = BEBL.CustomerBasket.Catalog(Nothing, Nothing, Nothing, oLang)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir el cataleg")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/customerBasket/update")>
    Public Function Update(<FromBody> oOrder As DTOPurchaseOrder) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oUsr = BEBL.User.Find(oOrder.UsrLog.usrCreated.Guid)
            If oUsr Is Nothing OrElse oUsr.Rol.id = DTORol.Ids.denied Then
                retval = MyBase.HttpErrorResponseMessage(exs, "usuario no autorizado")
            Else
                oOrder.Emp = oUsr.Emp
                If BEBL.CustomerBasket.update(exs, oOrder) Then
                    Dim value = oOrder.Num
                    retval = Request.CreateResponse(HttpStatusCode.OK, value)
                Else
                    retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la comanda")
                End If
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la comanda")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/customerBasket/updateAndEmail")> 'for SwiftUI
    Public Async Function UpdateAndEmail(<FromBody> oOrder As DTOPurchaseOrder) As Threading.Tasks.Task(Of HttpResponseMessage)
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            If BEBL.CustomerBasket.update(exs, oOrder) Then
                Dim oMailMessage = DTOPurchaseOrder.MailMessageConfirmation(oOrder)
                Await BEBL.MailMessageHelper.Send(oOrder.emp, oMailMessage, exs)
            End If

            If exs.Count = 0 Then
                retval = Request.CreateResponse(Of Integer)(HttpStatusCode.OK, oOrder.num)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la comanda")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la comanda")
        End Try
        Return retval
    End Function




End Class

