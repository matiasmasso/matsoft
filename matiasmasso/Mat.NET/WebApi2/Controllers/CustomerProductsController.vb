Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class CustomerProductController
    Inherits _BaseController

    <HttpGet>
    <Route("api/CustomerProduct/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CustomerProduct.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerProduct")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerProduct/{customer}/{sku}/{ref}")>
    Public Function Find2(customer As Guid, sku As Guid, ref As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer As New DTOCustomer(customer)
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.CustomerProduct.Find(oCustomer, oSku, ref)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerProduct")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerProduct")>
    Public Function Update(<FromBody> value As DTOCustomerProduct) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerProduct.Update(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al desar la CustomerProduct")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al desar la CustomerProduct")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerProduct/delete")>
    Public Function Delete(<FromBody> value As DTOCustomerProduct) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerProduct.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la CustomerProduct")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la CustomerProduct")
        End Try
        Return retval
    End Function

    '<HttpPost>
    '<Route("api/CustomerProduct/SaveIfMissing/{customer}/{sku}")>
    'Public Function SaveIfMissing(customer As Guid, sku As Guid, <FromBody> ref As String) As HttpResponseMessage
    '    Dim retval As HttpResponseMessage = Nothing
    '    Try
    '        Dim oCustomer As New DTOCustomer(customer)
    '        Dim oSku As New DTOProductSku(sku)
    '        Dim exs As New List(Of Exception)
    '        If BEBL.CustomerProduct.SaveIfMissing(oCustomer, oSku, ref, exs) Then
    '            retval = Request.CreateResponse(HttpStatusCode.OK, True)
    '        Else
    '            retval = MyBase.HttpErrorResponseMessage("error al llegir la CustomerProduct")
    '        End If
    '    Catch ex As Exception
    '        retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerProduct")
    '    End Try
    '    Return retval
    'End Function

    <HttpPost>
    <Route("api/CustomerProduct/UpdateElCorteIngles")>
    Public Function UpdateElCorteIngles(<FromBody> item As DTO.Integracions.ElCorteIngles.Cataleg) As HttpResponseMessage
        Dim exs As New List(Of Exception)
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.CustomerProduct.UpdateElCorteIngles(exs, item)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "Error al desar la referència " & item.Ref & " de El Corte Ingles")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al desar la referència " & item.Ref & " de El Corte Ingles")
        End Try
        Return retval
    End Function

End Class

Public Class CustomerProductsController
    Inherits _BaseController


    <HttpGet>
    <Route("api/CustomerProducts/{customer}/{sku}")>
    Public Function GetCustomerProducts2(customer As Guid, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = DTOBaseGuid.opcional(Of DTOCustomer)(customer)
            Dim oSku = DTOBaseGuid.opcional(Of DTOProductSku)(sku)
            Dim values = BEBL.CustomerProducts.All(oCustomer, oSku)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les referencies custom")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerProducts/{customer}")>
    Public Function FromRef(customer As Guid, <FromBody> ref As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.CustomerProducts.All(oCustomer, sRef:=ref)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les referencies custom")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerProducts/Compact/{customer}")>
    Public Function Compact(customer As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = New DTOCustomer(customer)
            Dim values = BEBL.CustomerProducts.Compact(oCustomer)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les referencies custom")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/CustomerProducts/{customer}/{sku}/{ref}")>
    Public Function GetCustomerProducts2(customer As Guid, sku As Guid, ref As String) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oCustomer = DTOBaseGuid.Opcional(Of DTOCustomer)(customer)
            Dim oSku = DTOBaseGuid.Opcional(Of DTOProductSku)(sku)
            Dim values = BEBL.CustomerProducts.All(oCustomer, oSku, ref)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les referencies custom")
        End Try
        Return retval
    End Function


    <HttpPost>
    <Route("api/CustomerProducts/SaveIfMissing")>
    Public Function SaveIfMissing(<FromBody> values As List(Of DTOCustomerProduct)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerProducts.SaveIfMissing(values, exs) Then
                retval = Request.CreateResponse(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage("error al llegir la CustomerProduct")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la CustomerProduct")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/CustomerProducts/delete")>
    Public Function Delete(<FromBody> values As List(Of Guid)) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.CustomerProducts.Delete(exs, values) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar les referencies de client")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar les referencies de client")
        End Try
        Return retval
    End Function

End Class
