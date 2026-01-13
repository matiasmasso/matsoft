
Imports System.Net
Imports System.Net.Http
Imports System.Web.Http

Public Class PriceListSupplierController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceList_Supplier/{guid}")>
    Public Function Find(guid As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim value = BEBL.PriceListSupplier.Find(guid)
            retval = Request.CreateResponse(HttpStatusCode.OK, value)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al llegir la PriceList_Supplier")
        End Try
        Return retval
    End Function



    <HttpPost>
    <Route("api/PriceList_Supplier/upload")>
    Public Function Upload() As HttpResponseMessage
        Dim oHelper As New ApiHelper.HttpRequestHelper(HttpContext.Current.Request)
        Dim result As HttpResponseMessage = Nothing

        Try
            Dim exs As New List(Of Exception)
            Dim json As String = oHelper.GetValue("serialized")
            Dim value = ApiHelper.Client.DeSerialize(Of DTOPriceListSupplier)(json, exs)
            If value Is Nothing Then
                result = MyBase.HttpErrorResponseMessage(exs, "Error al deserialitzar la PriceList_Supplier")
            Else
                If value.DocFile IsNot Nothing Then
                    value.DocFile.Thumbnail = oHelper.GetImage("docfile_thumbnail")
                    value.DocFile.Stream = oHelper.GetFileBytes("docfile_stream")
                End If

                If BEBL.PriceListSupplier.Update(value, exs) Then
                    result = Request.CreateResponse(HttpStatusCode.OK)
                Else
                    result = MyBase.HttpErrorResponseMessage(exs, "Error al pujar el docfile a DAL.PriceList_SupplierLoader")
                End If
            End If

        Catch ex As Exception
            result = MyBase.HttpErrorResponseMessage(ex, "Error a DAL.PriceList_SupplierLoader")
        End Try

        Return result
    End Function


    <HttpPost>
    <Route("api/PriceList_Supplier/delete")>
    Public Function Delete(<FromBody> value As DTOPriceListSupplier) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim exs As New List(Of Exception)
            If BEBL.PriceListSupplier.Delete(value, exs) Then
                retval = Request.CreateResponse(Of Boolean)(HttpStatusCode.OK, True)
            Else
                retval = MyBase.HttpErrorResponseMessage(exs, "error al eliminar la PriceList_Supplier")
            End If
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "error al eliminar la PriceList_Supplier")
        End Try
        Return retval
    End Function

End Class

Public Class PriceListSuppliersController
    Inherits _BaseController

    <HttpGet>
    <Route("api/PriceListSuppliers/FromProveidor/{proveidor}")>
    Public Function FromProveidor(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.PriceListsSupplier.FromProveidor(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les tarifes")
        End Try
        Return retval
    End Function

    <HttpGet>
    <Route("api/PriceListSuppliers/Costs/{proveidor}")>
    Public Function Costs(proveidor As Guid) As HttpResponseMessage
        Dim retval As HttpResponseMessage = Nothing
        Try
            Dim oProveidor As New DTOProveidor(proveidor)
            Dim values = BEBL.PriceListsSupplier.Costs(oProveidor)
            retval = Request.CreateResponse(HttpStatusCode.OK, values)
        Catch ex As Exception
            retval = MyBase.HttpErrorResponseMessage(ex, "Error al llegir les tarifes")
        End Try
        Return retval
    End Function

End Class

