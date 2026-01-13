Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class AecocController
    Inherits _BaseController


    <HttpGet>
    <Route("api/aecoc/NextEanToContact/{emp}/{contact}")>
    Public Function NextEanToContact(emp As Integer, contact As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oContact As New DTOContact(contact)
            Dim value = BEBL.Aecoc.NextEanToContact(oEmp, oContact, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, value)
            Else
                retval = MyBase.HttpErrorResponseMessage("Error al assignar un nou Ean")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al assignar un nou Ean")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/aecoc/NextEanToSku/{emp}/{sku}")>
    Public Function NextEanToSku(emp As Integer, sku As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            Dim oEmp = MyBase.GetEmp(emp)
            Dim oSku As New DTOProductSku(sku)
            Dim value = BEBL.Aecoc.NextEanToSku(oEmp, oSku, exs)
            If exs.Count = 0 Then
                retval = Request.CreateResponse(HttpStatusCode.OK, oSku.ean13)
            Else
                retval = MyBase.HttpErrorResponseMessage("Error al assignar un nou Ean")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les RepCustomers")
        End Try
        Return retval
    End Function

End Class

